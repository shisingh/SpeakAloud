namespace SpeakAloud
{
    partial class SpeakAloud
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeakAloud));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitleBar = new System.Windows.Forms.Label();
            this.cbKeyClipboard = new System.Windows.Forms.ComboBox();
            this.chkSHIFTClipboard = new System.Windows.Forms.CheckBox();
            this.chkALTClipboard = new System.Windows.Forms.CheckBox();
            this.chkCTRLClipboard = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.cbSpeed = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStopKey = new System.Windows.Forms.ComboBox();
            this.chkSHIFTStop = new System.Windows.Forms.CheckBox();
            this.cbSpeakKey = new System.Windows.Forms.ComboBox();
            this.chkALTStop = new System.Windows.Forms.CheckBox();
            this.chkShiftSpeak = new System.Windows.Forms.CheckBox();
            this.chkCTRLStop = new System.Windows.Forms.CheckBox();
            this.chkALTSpeak = new System.Windows.Forms.CheckBox();
            this.chkCTRLSpeak = new System.Windows.Forms.CheckBox();
            this.cbVoice = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkCTRLPause = new System.Windows.Forms.CheckBox();
            this.chkALTPause = new System.Windows.Forms.CheckBox();
            this.chkCTRLResume = new System.Windows.Forms.CheckBox();
            this.chkSHIFTPause = new System.Windows.Forms.CheckBox();
            this.chkALTResume = new System.Windows.Forms.CheckBox();
            this.cbPauseKey = new System.Windows.Forms.ComboBox();
            this.chkSHIFTResume = new System.Windows.Forms.CheckBox();
            this.cbResumeKey = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Speak Aloud";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cancel.png");
            this.imageList1.Images.SetKeyName(1, "001-cancel-2.png");
            // 
            // panelMain
            // 
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.cbKeyClipboard);
            this.panelMain.Controls.Add(this.chkSHIFTClipboard);
            this.panelMain.Controls.Add(this.chkALTClipboard);
            this.panelMain.Controls.Add(this.chkCTRLClipboard);
            this.panelMain.Controls.Add(this.label6);
            this.panelMain.Controls.Add(this.txtVolume);
            this.panelMain.Controls.Add(this.cbSpeed);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.cbResumeKey);
            this.panelMain.Controls.Add(this.cbStopKey);
            this.panelMain.Controls.Add(this.chkSHIFTResume);
            this.panelMain.Controls.Add(this.chkSHIFTStop);
            this.panelMain.Controls.Add(this.cbPauseKey);
            this.panelMain.Controls.Add(this.cbSpeakKey);
            this.panelMain.Controls.Add(this.chkALTResume);
            this.panelMain.Controls.Add(this.chkALTStop);
            this.panelMain.Controls.Add(this.chkSHIFTPause);
            this.panelMain.Controls.Add(this.chkShiftSpeak);
            this.panelMain.Controls.Add(this.chkCTRLResume);
            this.panelMain.Controls.Add(this.chkCTRLStop);
            this.panelMain.Controls.Add(this.chkALTPause);
            this.panelMain.Controls.Add(this.chkALTSpeak);
            this.panelMain.Controls.Add(this.chkCTRLPause);
            this.panelMain.Controls.Add(this.chkCTRLSpeak);
            this.panelMain.Controls.Add(this.cbVoice);
            this.panelMain.Controls.Add(this.label8);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.label7);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(275, 487);
            this.panelMain.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblTitleBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 30);
            this.panel1.TabIndex = 43;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SpeakAloud.Properties.Resources.cancel;
            this.pictureBox1.Location = new System.Drawing.Point(239, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // lblTitleBar
            // 
            this.lblTitleBar.AutoSize = true;
            this.lblTitleBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleBar.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitleBar.Location = new System.Drawing.Point(2, 5);
            this.lblTitleBar.Name = "lblTitleBar";
            this.lblTitleBar.Size = new System.Drawing.Size(188, 20);
            this.lblTitleBar.TabIndex = 0;
            this.lblTitleBar.Text = "Speak Aloud Settings";
            // 
            // cbKeyClipboard
            // 
            this.cbKeyClipboard.FormattingEnabled = true;
            this.cbKeyClipboard.Location = new System.Drawing.Point(188, 343);
            this.cbKeyClipboard.Name = "cbKeyClipboard";
            this.cbKeyClipboard.Size = new System.Drawing.Size(81, 24);
            this.cbKeyClipboard.TabIndex = 42;
            this.cbKeyClipboard.SelectedIndexChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkSHIFTClipboard
            // 
            this.chkSHIFTClipboard.AutoSize = true;
            this.chkSHIFTClipboard.Location = new System.Drawing.Point(120, 346);
            this.chkSHIFTClipboard.Name = "chkSHIFTClipboard";
            this.chkSHIFTClipboard.Size = new System.Drawing.Size(69, 21);
            this.chkSHIFTClipboard.TabIndex = 41;
            this.chkSHIFTClipboard.Text = "SHIFT";
            this.chkSHIFTClipboard.UseVisualStyleBackColor = true;
            this.chkSHIFTClipboard.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkALTClipboard
            // 
            this.chkALTClipboard.AutoSize = true;
            this.chkALTClipboard.Location = new System.Drawing.Point(68, 346);
            this.chkALTClipboard.Name = "chkALTClipboard";
            this.chkALTClipboard.Size = new System.Drawing.Size(56, 21);
            this.chkALTClipboard.TabIndex = 40;
            this.chkALTClipboard.Text = "ALT";
            this.chkALTClipboard.UseVisualStyleBackColor = true;
            this.chkALTClipboard.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkCTRLClipboard
            // 
            this.chkCTRLClipboard.AutoSize = true;
            this.chkCTRLClipboard.Location = new System.Drawing.Point(4, 346);
            this.chkCTRLClipboard.Name = "chkCTRLClipboard";
            this.chkCTRLClipboard.Size = new System.Drawing.Size(66, 21);
            this.chkCTRLClipboard.TabIndex = 39;
            this.chkCTRLClipboard.Text = "CTRL";
            this.chkCTRLClipboard.UseVisualStyleBackColor = true;
            this.chkCTRLClipboard.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 317);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(268, 20);
            this.label6.TabIndex = 38;
            this.label6.Text = "Speak Clipboard Text";
            // 
            // txtVolume
            // 
            this.txtVolume.Location = new System.Drawing.Point(5, 112);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(263, 22);
            this.txtVolume.TabIndex = 37;
            this.txtVolume.TextChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // cbSpeed
            // 
            this.cbSpeed.FormattingEnabled = true;
            this.cbSpeed.Items.AddRange(new object[] {
            "10",
            "9",
            "8",
            "7",
            "6",
            "5",
            "4",
            "3",
            "2",
            "1",
            "0",
            "-1",
            "-2",
            "-3",
            "-4",
            "-5",
            "-6",
            "-7",
            "-8",
            "-9",
            "-10"});
            this.cbSpeed.Location = new System.Drawing.Point(0, 168);
            this.cbSpeed.Name = "cbSpeed";
            this.cbSpeed.Size = new System.Drawing.Size(265, 24);
            this.cbSpeed.TabIndex = 36;
            this.cbSpeed.SelectedIndexChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(268, 20);
            this.label5.TabIndex = 35;
            this.label5.Text = "Speed";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(268, 20);
            this.label4.TabIndex = 34;
            this.label4.Text = "Volume";
            // 
            // cbStopKey
            // 
            this.cbStopKey.FormattingEnabled = true;
            this.cbStopKey.Location = new System.Drawing.Point(187, 284);
            this.cbStopKey.Name = "cbStopKey";
            this.cbStopKey.Size = new System.Drawing.Size(81, 24);
            this.cbStopKey.TabIndex = 31;
            this.cbStopKey.SelectedIndexChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkSHIFTStop
            // 
            this.chkSHIFTStop.AutoSize = true;
            this.chkSHIFTStop.Location = new System.Drawing.Point(119, 287);
            this.chkSHIFTStop.Name = "chkSHIFTStop";
            this.chkSHIFTStop.Size = new System.Drawing.Size(69, 21);
            this.chkSHIFTStop.TabIndex = 29;
            this.chkSHIFTStop.Text = "SHIFT";
            this.chkSHIFTStop.UseVisualStyleBackColor = true;
            this.chkSHIFTStop.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // cbSpeakKey
            // 
            this.cbSpeakKey.FormattingEnabled = true;
            this.cbSpeakKey.Location = new System.Drawing.Point(187, 227);
            this.cbSpeakKey.Name = "cbSpeakKey";
            this.cbSpeakKey.Size = new System.Drawing.Size(81, 24);
            this.cbSpeakKey.TabIndex = 30;
            this.cbSpeakKey.SelectedIndexChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkALTStop
            // 
            this.chkALTStop.AutoSize = true;
            this.chkALTStop.Location = new System.Drawing.Point(67, 287);
            this.chkALTStop.Name = "chkALTStop";
            this.chkALTStop.Size = new System.Drawing.Size(56, 21);
            this.chkALTStop.TabIndex = 27;
            this.chkALTStop.Text = "ALT";
            this.chkALTStop.UseVisualStyleBackColor = true;
            this.chkALTStop.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkShiftSpeak
            // 
            this.chkShiftSpeak.AutoSize = true;
            this.chkShiftSpeak.Location = new System.Drawing.Point(119, 230);
            this.chkShiftSpeak.Name = "chkShiftSpeak";
            this.chkShiftSpeak.Size = new System.Drawing.Size(69, 21);
            this.chkShiftSpeak.TabIndex = 28;
            this.chkShiftSpeak.Text = "SHIFT";
            this.chkShiftSpeak.UseVisualStyleBackColor = true;
            this.chkShiftSpeak.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkCTRLStop
            // 
            this.chkCTRLStop.AutoSize = true;
            this.chkCTRLStop.Location = new System.Drawing.Point(3, 287);
            this.chkCTRLStop.Name = "chkCTRLStop";
            this.chkCTRLStop.Size = new System.Drawing.Size(66, 21);
            this.chkCTRLStop.TabIndex = 25;
            this.chkCTRLStop.Text = "CTRL";
            this.chkCTRLStop.UseVisualStyleBackColor = true;
            this.chkCTRLStop.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkALTSpeak
            // 
            this.chkALTSpeak.AutoSize = true;
            this.chkALTSpeak.Location = new System.Drawing.Point(67, 230);
            this.chkALTSpeak.Name = "chkALTSpeak";
            this.chkALTSpeak.Size = new System.Drawing.Size(56, 21);
            this.chkALTSpeak.TabIndex = 26;
            this.chkALTSpeak.Text = "ALT";
            this.chkALTSpeak.UseVisualStyleBackColor = true;
            this.chkALTSpeak.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkCTRLSpeak
            // 
            this.chkCTRLSpeak.AutoSize = true;
            this.chkCTRLSpeak.Location = new System.Drawing.Point(3, 230);
            this.chkCTRLSpeak.Name = "chkCTRLSpeak";
            this.chkCTRLSpeak.Size = new System.Drawing.Size(66, 21);
            this.chkCTRLSpeak.TabIndex = 24;
            this.chkCTRLSpeak.Text = "CTRL";
            this.chkCTRLSpeak.UseVisualStyleBackColor = true;
            this.chkCTRLSpeak.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // cbVoice
            // 
            this.cbVoice.FormattingEnabled = true;
            this.cbVoice.Location = new System.Drawing.Point(4, 56);
            this.cbVoice.Name = "cbVoice";
            this.cbVoice.Size = new System.Drawing.Size(265, 24);
            this.cbVoice.TabIndex = 23;
            this.cbVoice.SelectedIndexChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(268, 20);
            this.label3.TabIndex = 21;
            this.label3.Text = "Stop Shortcut";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(268, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "Speak Shortcut";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "Voice";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(2, 374);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(268, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "Pause Shortcut";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 429);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(268, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "Resume Shortcut";
            // 
            // chkCTRLPause
            // 
            this.chkCTRLPause.AutoSize = true;
            this.chkCTRLPause.Location = new System.Drawing.Point(4, 401);
            this.chkCTRLPause.Name = "chkCTRLPause";
            this.chkCTRLPause.Size = new System.Drawing.Size(66, 21);
            this.chkCTRLPause.TabIndex = 24;
            this.chkCTRLPause.Text = "CTRL";
            this.chkCTRLPause.UseVisualStyleBackColor = true;
            this.chkCTRLPause.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkALTPause
            // 
            this.chkALTPause.AutoSize = true;
            this.chkALTPause.Location = new System.Drawing.Point(68, 401);
            this.chkALTPause.Name = "chkALTPause";
            this.chkALTPause.Size = new System.Drawing.Size(56, 21);
            this.chkALTPause.TabIndex = 26;
            this.chkALTPause.Text = "ALT";
            this.chkALTPause.UseVisualStyleBackColor = true;
            this.chkALTPause.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkCTRLResume
            // 
            this.chkCTRLResume.AutoSize = true;
            this.chkCTRLResume.Location = new System.Drawing.Point(4, 458);
            this.chkCTRLResume.Name = "chkCTRLResume";
            this.chkCTRLResume.Size = new System.Drawing.Size(66, 21);
            this.chkCTRLResume.TabIndex = 25;
            this.chkCTRLResume.Text = "CTRL";
            this.chkCTRLResume.UseVisualStyleBackColor = true;
            this.chkCTRLResume.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkSHIFTPause
            // 
            this.chkSHIFTPause.AutoSize = true;
            this.chkSHIFTPause.Location = new System.Drawing.Point(120, 401);
            this.chkSHIFTPause.Name = "chkSHIFTPause";
            this.chkSHIFTPause.Size = new System.Drawing.Size(69, 21);
            this.chkSHIFTPause.TabIndex = 28;
            this.chkSHIFTPause.Text = "SHIFT";
            this.chkSHIFTPause.UseVisualStyleBackColor = true;
            this.chkSHIFTPause.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkALTResume
            // 
            this.chkALTResume.AutoSize = true;
            this.chkALTResume.Location = new System.Drawing.Point(68, 458);
            this.chkALTResume.Name = "chkALTResume";
            this.chkALTResume.Size = new System.Drawing.Size(56, 21);
            this.chkALTResume.TabIndex = 27;
            this.chkALTResume.Text = "ALT";
            this.chkALTResume.UseVisualStyleBackColor = true;
            this.chkALTResume.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // cbPauseKey
            // 
            this.cbPauseKey.FormattingEnabled = true;
            this.cbPauseKey.Location = new System.Drawing.Point(188, 398);
            this.cbPauseKey.Name = "cbPauseKey";
            this.cbPauseKey.Size = new System.Drawing.Size(81, 24);
            this.cbPauseKey.TabIndex = 30;
            this.cbPauseKey.SelectedIndexChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // chkSHIFTResume
            // 
            this.chkSHIFTResume.AutoSize = true;
            this.chkSHIFTResume.Location = new System.Drawing.Point(120, 458);
            this.chkSHIFTResume.Name = "chkSHIFTResume";
            this.chkSHIFTResume.Size = new System.Drawing.Size(69, 21);
            this.chkSHIFTResume.TabIndex = 29;
            this.chkSHIFTResume.Text = "SHIFT";
            this.chkSHIFTResume.UseVisualStyleBackColor = true;
            this.chkSHIFTResume.CheckedChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // cbResumeKey
            // 
            this.cbResumeKey.FormattingEnabled = true;
            this.cbResumeKey.Location = new System.Drawing.Point(188, 455);
            this.cbResumeKey.Name = "cbResumeKey";
            this.cbResumeKey.Size = new System.Drawing.Size(81, 24);
            this.cbResumeKey.TabIndex = 31;
            this.cbResumeKey.SelectedIndexChanged += new System.EventHandler(this.chkCTRLSpeak_CheckedChanged);
            // 
            // SpeakAloud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 487);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(291, 600);
            this.MinimumSize = new System.Drawing.Size(250, 378);
            this.Name = "SpeakAloud";
            this.ShowInTaskbar = false;
            this.Text = "Speak Aloud";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitleBar;
        private System.Windows.Forms.ComboBox cbKeyClipboard;
        private System.Windows.Forms.CheckBox chkSHIFTClipboard;
        private System.Windows.Forms.CheckBox chkALTClipboard;
        private System.Windows.Forms.CheckBox chkCTRLClipboard;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.ComboBox cbSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStopKey;
        private System.Windows.Forms.CheckBox chkSHIFTStop;
        private System.Windows.Forms.ComboBox cbSpeakKey;
        private System.Windows.Forms.CheckBox chkALTStop;
        private System.Windows.Forms.CheckBox chkShiftSpeak;
        private System.Windows.Forms.CheckBox chkCTRLStop;
        private System.Windows.Forms.CheckBox chkALTSpeak;
        private System.Windows.Forms.CheckBox chkCTRLSpeak;
        private System.Windows.Forms.ComboBox cbVoice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbResumeKey;
        private System.Windows.Forms.CheckBox chkSHIFTResume;
        private System.Windows.Forms.ComboBox cbPauseKey;
        private System.Windows.Forms.CheckBox chkALTResume;
        private System.Windows.Forms.CheckBox chkSHIFTPause;
        private System.Windows.Forms.CheckBox chkCTRLResume;
        private System.Windows.Forms.CheckBox chkALTPause;
        private System.Windows.Forms.CheckBox chkCTRLPause;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}