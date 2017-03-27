namespace RFIDReader
{
    partial class RFIDInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.connect_button = new System.Windows.Forms.Button();
            this.disconnect_button = new System.Windows.Forms.Button();
            this.readerIPTextField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clientsCountLabel = new System.Windows.Forms.Label();
            this.readFramesLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.logfileTextBox = new System.Windows.Forms.TextBox();
            this.logButton = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // connect_button
            // 
            this.connect_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.connect_button.Location = new System.Drawing.Point(365, 13);
            this.connect_button.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(98, 22);
            this.connect_button.TabIndex = 0;
            this.connect_button.Text = "Connect";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // disconnect_button
            // 
            this.disconnect_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.disconnect_button.Location = new System.Drawing.Point(365, 39);
            this.disconnect_button.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.disconnect_button.Name = "disconnect_button";
            this.disconnect_button.Size = new System.Drawing.Size(98, 22);
            this.disconnect_button.TabIndex = 1;
            this.disconnect_button.Text = "Disconnect";
            this.disconnect_button.UseVisualStyleBackColor = true;
            this.disconnect_button.Click += new System.EventHandler(this.disconnect_button_Click);
            // 
            // readerIPTextField
            // 
            this.readerIPTextField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.readerIPTextField.Location = new System.Drawing.Point(131, 25);
            this.readerIPTextField.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.readerIPTextField.Name = "readerIPTextField";
            this.readerIPTextField.Size = new System.Drawing.Size(212, 24);
            this.readerIPTextField.TabIndex = 2;
            this.readerIPTextField.Text = "192.168.1.3";
            this.readerIPTextField.TextChanged += new System.EventHandler(this.readerIPTextField_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(20, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Reader IP name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(22, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(443, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "When connected to reader, it would stream RF Parameters to port 14";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // clientsCountLabel
            // 
            this.clientsCountLabel.AutoSize = true;
            this.clientsCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.clientsCountLabel.Location = new System.Drawing.Point(22, 91);
            this.clientsCountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.clientsCountLabel.Name = "clientsCountLabel";
            this.clientsCountLabel.Size = new System.Drawing.Size(138, 17);
            this.clientsCountLabel.TabIndex = 17;
            this.clientsCountLabel.Text = "Connected Clients: 0";
            // 
            // readFramesLabel
            // 
            this.readFramesLabel.AutoSize = true;
            this.readFramesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.readFramesLabel.Location = new System.Drawing.Point(172, 90);
            this.readFramesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.readFramesLabel.Name = "readFramesLabel";
            this.readFramesLabel.Size = new System.Drawing.Size(105, 17);
            this.readFramesLabel.TabIndex = 23;
            this.readFramesLabel.Text = "Read frames: 0";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.statusLabel.Location = new System.Drawing.Point(329, 90);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(142, 17);
            this.statusLabel.TabIndex = 24;
            this.statusLabel.Text = "Status: Disconnected";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(20, 127);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "Filename";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // logfileTextBox
            // 
            this.logfileTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.logfileTextBox.Location = new System.Drawing.Point(83, 125);
            this.logfileTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.logfileTextBox.Name = "logfileTextBox";
            this.logfileTextBox.Size = new System.Drawing.Size(57, 23);
            this.logfileTextBox.TabIndex = 26;
            this.logfileTextBox.Text = "log";
            this.logfileTextBox.TextChanged += new System.EventHandler(this.logfileTextBox_TextChanged);
            // 
            // logButton
            // 
            this.logButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.logButton.AutoSize = true;
            this.logButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.logButton.Location = new System.Drawing.Point(146, 124);
            this.logButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(42, 27);
            this.logButton.TabIndex = 27;
            this.logButton.Text = "Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.CheckedChanged += new System.EventHandler(this.logButton_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(194, 126);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "Antenna #";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox1.Location = new System.Drawing.Point(266, 124);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(65, 23);
            this.textBox1.TabIndex = 29;
            this.textBox1.Text = "1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox2.Location = new System.Drawing.Point(394, 124);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(50, 23);
            this.textBox2.TabIndex = 23;
            this.textBox2.Text = "30";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(340, 125);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 31;
            this.label5.Text = "Power";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(22, 155);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 17);
            this.label6.TabIndex = 32;
            this.label6.Text = "Filter";
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.checkBox1.Location = new System.Drawing.Point(176, 153);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(37, 27);
            this.checkBox1.TabIndex = 33;
            this.checkBox1.Text = "On";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox3.Location = new System.Drawing.Point(62, 153);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(110, 23);
            this.textBox3.TabIndex = 34;
            this.textBox3.Text = "E2003098191500771234";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox4.Location = new System.Drawing.Point(422, 156);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(25, 23);
            this.textBox4.TabIndex = 35;
            this.textBox4.Text = "1";
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(346, 157);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 17);
            this.label7.TabIndex = 36;
            this.label7.Text = "Delay(ms)";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(227, 157);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 17);
            this.label8.TabIndex = 37;
            this.label8.Text = "Mode";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox5.Location = new System.Drawing.Point(287, 154);
            this.textBox5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(25, 23);
            this.textBox5.TabIndex = 38;
            this.textBox5.Text = "1";
            this.textBox5.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // RFIDInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 183);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.logfileTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.readFramesLabel);
            this.Controls.Add(this.clientsCountLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.readerIPTextField);
            this.Controls.Add(this.disconnect_button);
            this.Controls.Add(this.connect_button);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "RFIDInterface";
            this.Text = "RFID Reader Interface";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RFIDInterface_FormClosed);
            this.Load += new System.EventHandler(this.RFIDInterface_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Button disconnect_button;
        private System.Windows.Forms.TextBox readerIPTextField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label clientsCountLabel;
        private System.Windows.Forms.Label readFramesLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox logfileTextBox;
        private System.Windows.Forms.CheckBox logButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox5;
    }
}