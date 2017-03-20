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
     
        public RFIDInterface()
        {
            InitializeComponent();
        }
        private void ReadSettingsFromFile()
        {
            //System.Xml.Serialization.XmlSerializer reader =
            //    new System.Xml.Serialization.XmlSerializer(typeof(Settings));
            //System.IO.StreamReader file = new System.IO.StreamReader(
            //    "settings.xml");
            //rdr_settings = (Settings)reader.Deserialize(file);

            Settings rdr_settings = Settings.Load("settings.xml");
            try
            {

                //foreach (AntennaConfig a in rdr_settings.Antennas)
                //{
                //    a.IsEnabled = true;
                //}


                rdr_settings.Antennas.DisableAll();
                antenna = textBox1.Text;
                string[] words = antenna.Split(' ');
                foreach (string word in words)
                {
                    rdr_settings.Antennas.GetAntenna(Convert.ToUInt16(word)).IsEnabled = true;
                    rdr_settings.Antennas.GetAntenna(Convert.ToUInt16(word)).TxPowerInDbm = Convert.ToInt16(textBox2.Text);
                    rdr_settings.Antennas.GetAntenna(Convert.ToUInt16(word)).MaxRxSensitivity = true;
                   
                }

                // Set the Transmit Power and 
                // Receive Sensitivity to the maximum.

                //rdr_settings.Report.IncludeAntennaPortNumber = true;
                //rdr_settings.Report.IncludePhaseAngle = true;
                //rdr_settings.Report.IncludePeakRssi = true;
                //rdr_settings.Report.IncludeFirstSeenTime = true;
                //rdr_settings.Report.IncludeChannel = true;
                //rdr_settings.Report.IncludeDopplerFrequency = true;

                //rdr_settings.ReaderMode = ReaderMode.MaxThroughput;
                //rdr_settings.SearchMode = SearchMode.DualTarget;


                // Tell the reader to include the antenna number
                // in all tag reports. Other fields can be added
                // to the reports in the same way by setting the 
                // appropriate Report.IncludeXXXXXXX property.
                rdr_settings.Report.IncludeAntennaPortNumber = true;

                // Send a tag report for every tag read.
                rdr_settings.Report.Mode = ReportMode.Individual;

                if (checkBox1.Checked)
                {
                    // Setup a tag filter.
                    // Only the tags that match this filter will respond.
                    // First, setup tag filter #1.
                    // We want to apply the filter to the EPC memory bank.
                    rdr_settings.Filters.TagFilter1.MemoryBank = MemoryBank.Epc;
                    // Start matching at the third word (bit 32), since the 
                    // first two words of the EPC memory bank are the
                    // CRC and control bits. BitPointers.Epc is a helper
                    // enumeration you can use, so you don't have to remember this.
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
            TcpClient tcpClient = (TcpClient)client;
            tcpClient.SendBufferSize = 100000000;
            NetworkStream clientStream = tcpClient.GetStream();
            clients.Add(clientStream);
            Console.WriteLine("client received. total clients:" + clients.Count);
             
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
    }
}
