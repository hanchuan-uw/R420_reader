using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Impinj.OctaneSdk;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace RFIDReader
{
    public partial class RFIDInterface : Form
    {
        StreamWriter log;
        bool logging = false;
        string logfile = "";
        int maxBufferSize = 5000;
        ImpinjReader rdr; 
        int port = 14;
        int reader_port  = 5084;
        string reader_ip = "SPEEDWAY02.DRP.CS.CMU.EDU";
        string antenna = "1";
        Settings rdr_settings;
        List<NetworkStream> clients = new List<NetworkStream>();        
        private TcpListener tcpListener;
        private Thread listenThread;
        int seq = 0;
        static string mode = "default";
        bool running = true;
        BindingList<RFIDResult> results = new BindingList<RFIDResult>();
        System.Threading.Thread sendingThread;
        private bool wantToStart;
     
        public RFIDInterface()
        {
            wantToStart = false;
            InitializeComponent();
        }
        private void ReadSettingsFromFile()
        {

            Settings rdr_settings = Settings.Load("settings.xml");
            try
            {

                rdr_settings.Antennas.DisableAll();
                antenna = textBox1.Text;
                string[] words = antenna.Split(' ');
                foreach (string word in words)
                {
                    rdr_settings.Antennas.GetAntenna(Convert.ToUInt16(word)).IsEnabled = true;
                    rdr_settings.Antennas.GetAntenna(Convert.ToUInt16(word)).TxPowerInDbm = Convert.ToInt16(textBox2.Text);
                    rdr_settings.Antennas.GetAntenna(Convert.ToUInt16(word)).MaxRxSensitivity = true;
                    if (Convert.ToInt16(textBox5.Text) == 1)
                    {
                        rdr_settings.ReaderMode = ReaderMode.MaxThroughput;
                    }
                    else if (Convert.ToInt16(textBox5.Text) == 2)
                    {
                        rdr_settings.ReaderMode = ReaderMode.Hybrid;
                    }
                    else if (Convert.ToInt16(textBox5.Text) == 3)
                    {
                        rdr_settings.ReaderMode = ReaderMode.DenseReaderM4;
                    }
                    else if (Convert.ToInt16(textBox5.Text) == 4)
                    {
                        rdr_settings.ReaderMode = ReaderMode.DenseReaderM8;
                    }

                    //rdr_settings.Antennas.GetAntenna(Convert.ToUInt16(word)).MaxTxPower = true;
                }

                rdr_settings.Report.IncludeAntennaPortNumber = true;

                // Send a tag report for every tag read.
                rdr_settings.Report.Mode = ReportMode.Individual;

                if (checkBox1.Checked)
                {

                    rdr_settings.Filters.TagFilter1.MemoryBank = MemoryBank.Epc;

                    rdr_settings.Filters.TagFilter1.BitPointer = BitPointers.Epc;
                    // Only match tags with EPCs that start with "xxxx"
                    rdr_settings.Filters.TagFilter1.TagMask = textBox3.Text;
                    // This filter is 16 bits long (one word).
                    //rdr_settings.Filters.TagFilter1.BitCount = 16;
                    rdr_settings.Filters.Mode = TagFilterMode.OnlyFilter1;

                }


                rdr.ApplySettings(rdr_settings);
                Console.WriteLine("Settings Applied");
                rdr_settings = rdr.QuerySettings();
                Console.WriteLine(rdr_settings.Report);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ListenForClients()
        {
       
           this.tcpListener.Start();

           while (true)
            {
                if (running)
                {
                    Console.WriteLine("Waiting for a connection...");
                    //blocks until a client has connected to the server
                    TcpClient client = this.tcpListener.AcceptTcpClient();

                    //create a thread to handle communication 
                    //with connected client
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                    if (InvokeRequired)
                    {
                        Invoke(new MethodInvoker(updateInterface));
                    }
                }
                else
                {
                    return;
                }
            }
        }


        private void HandleClientComm(object client)
        {
            try
            {
            TcpClient tcpClient = (TcpClient)client;
            tcpClient.SendBufferSize = 100000000;
            NetworkStream clientStream = tcpClient.GetStream();
            clients.Add(clientStream);
            Console.WriteLine("client received. total clients:" + clients.Count);
            bool bClosed = false;
            byte[] buff = new byte[1];
            byte[] buff2 = new byte[100];


            //disconnect();

            //Thread.Sleep(1000);
            string[] antennaInfoChange = {"1","30"};

            while (bClosed == false && this.running == true)
            {
                if (tcpClient.Client.Poll(0, SelectMode.SelectRead))
                {
                    if (tcpClient.Client.Receive(buff, SocketFlags.Peek) == 0)
                    {
                        // Client disconnected
                        bClosed = true;
                    }
                    byte[] myReadBuffer = new byte[1024];
                    StringBuilder myCompleteMessage = new StringBuilder();
                    int numberOfBytesRead = 0;

                    // Incoming message may be larger than the buffer size. 
                    do
                    {
                        numberOfBytesRead = clientStream.Read(myReadBuffer, 0, myReadBuffer.Length);

                        myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                        Debug.WriteLine(myCompleteMessage.ToString());
                        antennaInfoChange = myCompleteMessage.ToString().Split(' ');
                        myCompleteMessage = new StringBuilder();
                        Debug.WriteLine(antennaInfoChange[0]);
                        Debug.WriteLine(antennaInfoChange[1]);
                        if (antennaInfoChange[0] != textBox1.Text || antennaInfoChange[1] != textBox2.Text)
                        {
                            textBox2.Text = antennaInfoChange[1];
                            textBox1.Text = antennaInfoChange[0];
                            start();
                        }

                        //disconnect();
                        Thread.Sleep(1000);
                        
                    } while (clientStream.DataAvailable);

                }
            }


            clients.Remove(clientStream);
            tcpClient.Client.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public void sendToClients(String msg)
        {
            for (int i = 0; i < clients.Count; i++)
            {

                NetworkStream clientStream = clients[i];
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(msg);

                try
                {
                    clientStream.Write(buffer, 0, buffer.Length);
                    clientStream.Flush();
                }
                catch (System.IO.IOException ex)
                {
                    clients.Remove(clientStream);

                }
            }
        }

        public void setPower(double power, int antenna)
        {
            
        }

        public void createServer()
        {
            if (tcpListener == null)
            {
                this.tcpListener = new TcpListener(IPAddress.Any, port);
                this.listenThread = new Thread(new ThreadStart(ListenForClients));
                this.listenThread.Start();
            }
            // Start a socket to listen for connections.

        }
        private void OnTagsReported(ImpinjReader sender, TagReport report)
        {
            foreach (Tag tag in report)
            {
                if (results.Count == maxBufferSize) results.RemoveAt(0);
                RFIDResult r = new RFIDResult(sender.Name, seq++, mode, tag);
                r.AnalysisEPC();
                results.Add(r);
                if (logging)
                    log.WriteLine(r.makeMeAString());
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(updateInterface));
                }
            }
        }
        public void updateInterface()
        {
            readFramesLabel.Text = "Read Frames: " + seq;
            clientsCountLabel.Text = "Connected Clients: " + clients.Count;
        }

        public void disconnect()
        {
            if (rdr != null)
            {
                rdr.Stop();
                rdr.Disconnect();
                rdr = null;
                statusLabel.ForeColor = Color.Red;
                statusLabel.Text = "Status: Disconnected";                 
            }
        }

        public void connect()
        {
            rdr = new ImpinjReader();
            rdr.Name = reader_ip + ":" + reader_port;
            Console.WriteLine("connecting to: " + reader_ip + ":" + reader_port);
            //rdr.Connect(reader_ip, reader_port);
            rdr.Connect(reader_ip);

            rdr.ApplyDefaultSettings();
            rdr.TagsReported += OnTagsReported;
            ReadSettingsFromFile();
            rdr.Start();
            statusLabel.ForeColor = Color.Green;
            statusLabel.Text = "Status: Connected";          
        }

        public void start()
        {
            CheckForIllegalCrossThreadCalls = false; 
            if (rdr != null) disconnect();
            reader_ip = readerIPTextField.Text;
            connect();
            
            createServer();
            if (sendingThread == null)
            {
                sendingThread = new System.Threading.Thread(new System.Threading.ThreadStart(
                 () =>
                 {
                     while (true)
                     {
                         if (running)
                         {
                             if (results.Count > 0)
                             {
                                 RFIDResult result = results[0];
                                 if (result != null)
                                 {
                                     results.Remove(result);
                                     sendToClients(";" + result.makeMeAString() + "\n");
                                     Thread.Sleep(Convert.ToInt16(textBox4.Text));

                                 }
                             }
                         }
                         else
                         {
                             return;
                         }
                     }
                 }
             ));
             sendingThread.Start();
            }
        }

        private void connect_button_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (logButton.Checked)
                {
                    logging = true;
                    logfile = logfileTextBox.Text + ".txt";
                    openLogFile();

                }
                this.start();
            }
    
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void openLogFile()
        {
            log = new StreamWriter(logfile, true);
        }

        private void disconnect_button_Click(object sender, System.EventArgs e)
        {
            disconnect();
            if (logging)
            log.Close();
        }

        private void RFIDInterface_Load(object sender, System.EventArgs e)
        {

        }

        private void RFIDInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            running = false;
            if (tcpListener != null)
            {
                disconnect();
                tcpListener = null;
                listenThread = null;
            }
            
            foreach (NetworkStream n in clients)
            {
                n.Close();
                clients.Remove(n);
            }
            Environment.Exit(1);
        }

        private void readerIPTextField_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void label2_Click(object sender, System.EventArgs e)
        {

        }

        private void logfileTextBox_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void logButton_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        private void label4_Click(object sender, System.EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
