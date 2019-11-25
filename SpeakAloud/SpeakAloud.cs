using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace SpeakAloud
{
    public partial class SpeakAloud : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(
            IntPtr hWndRemove,  // handle to window to remove
            IntPtr hWndNewNext  // handle to next window
            );

        bool _initialized = false;
        bool _uiInputModified = false;
        bool _applicationExitInitiated = false;
        SpeakAloudInfo _speakAloudInfo = new SpeakAloudInfo();
        SpeechSynthesizer _speachSyn = null;
        SynthesizerState _speechState = SynthesizerState.Ready;
        bool _getSpeechTextUsingClipboard = false;
        IntPtr _clipboardViewerNext = IntPtr.Zero;
        DateTime _dtClipboardGetTextStart = DateTime.Now.AddDays(-1);
        int _sendKeysCounter = 0;

        delegate void OneArgumentDel(object info);
        delegate void NoArgumentDel();
        Point _moveStartPoint = new Point();
        public SpeakAloud()
        {
            InitializeComponent();
            this.FormClosing += SpeakAloud_FormClosing;
            this.LostFocus += SpeakAloud_LostFocus;
            this.Resize += SpeakAloud_Resize;
            cbSpeed.SelectedIndex = 9;

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                cbSpeakKey.Items.Add(key);
            }
            this.MaximizeBox = false;

            _speachSyn = new SpeechSynthesizer();
            _speachSyn.StateChanged += _speachSyn_StateChanged;
            foreach (InstalledVoice v in _speachSyn.GetInstalledVoices())
            {
                cbVoice.Items.Add(v.VoiceInfo.Name);
            }

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                cbSpeakKey.Items.Add(key);
                cbStopKey.Items.Add(key);
                cbKeyClipboard.Items.Add(key);
                cbPauseKey.Items.Add(key);
                cbResumeKey.Items.Add(key);
            }

            UpdateSpekaInfoUI(_speakAloudInfo);
            _initialized = true;

            _speakAloudInfo.RegisterHotKeys(this.Handle);

            RegisterClipboardViewer();
            InitNotifyIcon();
            InitTitleBar();
        }

        void _speachSyn_StateChanged(object sender, StateChangedEventArgs e)
        {
            _speechState = e.State;
            switch (e.State)
            {
                case SynthesizerState.Paused:
                    //cmdStop.Enabled = true;
                    //cmdPause.Text = "Resume";
                    //cmdPause.Enabled = true;
                    break;

                case SynthesizerState.Ready:
                    //cmdStop.Enabled = false;
                    //cmdPause.Enabled = false;
                    //cmdPause.Text = "Pause";
                    break;

                case SynthesizerState.Speaking:
                    //cmdStop.Enabled = true;
                    //cmdPause.Enabled = true;
                    //cmdPause.Text = "Pause";
                    break;
            }
        }

        void UpdateSpekaInfoUI(SpeakAloudInfo speakInfo)
        {
            // select the default voice
            for (int i = 0; i < cbVoice.Items.Count; i++)
            {
                string name = cbVoice.Items[i].ToString();
                if (string.Compare(name, speakInfo.Voice, true) == 0)
                {
                    cbVoice.SelectedIndex = i;
                }
            }

            if (cbVoice.SelectedItem == null)
            {
                cbVoice.SelectedIndex = 0;
                speakInfo.Voice = cbVoice.Items[0].ToString();
                _uiInputModified = true;
            }

            txtVolume.Text = speakInfo.Volume.ToString();
            for (int i = 0; i < cbSpeed.Items.Count; i++)
            {
                string strVal = cbSpeed.Items[i].ToString();
                if (string.Compare(strVal, speakInfo.Speed.ToString(), true) == 0)
                {
                    cbSpeed.SelectedIndex = i;
                }
            }

            if ((speakInfo.StartShortcut._mod1 & Consts.MOD_ALT) == Consts.MOD_ALT ||
                (speakInfo.StartShortcut._mod2 & Consts.MOD_ALT) == Consts.MOD_ALT ||
                (speakInfo.StartShortcut._mod3 & Consts.MOD_ALT) == Consts.MOD_ALT)
            {
                chkALTSpeak.Checked = true;
            }
            if ((speakInfo.StartShortcut._mod1 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL ||
                (speakInfo.StartShortcut._mod2 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL ||
                (speakInfo.StartShortcut._mod3 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL)
            {
                chkCTRLSpeak.Checked = true;
            }
            if ((speakInfo.StartShortcut._mod1 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT ||
                (speakInfo.StartShortcut._mod2 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT ||
                (speakInfo.StartShortcut._mod3 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT)
            {
                chkShiftSpeak.Checked = true;
            }
            if ((speakInfo.StartShortcut._mod1 & Consts.MOD_WIN) == Consts.MOD_WIN)
            {
            }
            cbSpeakKey.SelectedItem = speakInfo.StartShortcut._key;

            if ((speakInfo.StopShortcut._mod1 & Consts.MOD_ALT) == Consts.MOD_ALT ||
                (speakInfo.StopShortcut._mod2 & Consts.MOD_ALT) == Consts.MOD_ALT ||
                (speakInfo.StopShortcut._mod3 & Consts.MOD_ALT) == Consts.MOD_ALT)
            {
                chkALTStop.Checked = true;
            }
            if ((speakInfo.StopShortcut._mod1 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL ||
                (speakInfo.StopShortcut._mod2 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL ||
                (speakInfo.StopShortcut._mod3 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL)
            {
                chkCTRLStop.Checked = true;
            }
            if ((speakInfo.StopShortcut._mod1 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT ||
                (speakInfo.StopShortcut._mod2 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT ||
                (speakInfo.StopShortcut._mod3 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT)
            {
                chkSHIFTStop.Checked = true;
            }
            if ((speakInfo.StopShortcut._mod1 & Consts.MOD_WIN) == Consts.MOD_WIN)
            {
            }
            cbStopKey.SelectedItem = speakInfo.StopShortcut._key;

            if ((speakInfo.ReadClipboardShortcut._mod1 & Consts.MOD_ALT) == Consts.MOD_ALT ||
                (speakInfo.ReadClipboardShortcut._mod2 & Consts.MOD_ALT) == Consts.MOD_ALT ||
                (speakInfo.ReadClipboardShortcut._mod3 & Consts.MOD_ALT) == Consts.MOD_ALT)
            {
                chkALTClipboard.Checked = true;
            }
            if ((speakInfo.ReadClipboardShortcut._mod1 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL ||
                (speakInfo.ReadClipboardShortcut._mod2 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL ||
                (speakInfo.ReadClipboardShortcut._mod3 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL)
            {
                chkCTRLClipboard.Checked = true;
            }
            if ((speakInfo.ReadClipboardShortcut._mod1 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT ||
                (speakInfo.ReadClipboardShortcut._mod2 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT ||
                (speakInfo.ReadClipboardShortcut._mod3 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT)
            {
                chkSHIFTClipboard.Checked = true;
            }
            if ((speakInfo.ReadClipboardShortcut._mod1 & Consts.MOD_WIN) == Consts.MOD_WIN)
            {
            }
            cbKeyClipboard.SelectedItem = speakInfo.ReadClipboardShortcut._key;

            if ((speakInfo.PauseShortcut._mod1 & Consts.MOD_ALT) == Consts.MOD_ALT ||
                (speakInfo.PauseShortcut._mod2 & Consts.MOD_ALT) == Consts.MOD_ALT ||
                (speakInfo.PauseShortcut._mod3 & Consts.MOD_ALT) == Consts.MOD_ALT)
            {
                chkALTPause.Checked = true;
            }
            if ((speakInfo.PauseShortcut._mod1 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL ||
                (speakInfo.PauseShortcut._mod2 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL ||
                (speakInfo.PauseShortcut._mod3 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL)
            {
                chkCTRLPause.Checked = true;
            }
            if ((speakInfo.PauseShortcut._mod1 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT ||
                (speakInfo.PauseShortcut._mod2 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT ||
                (speakInfo.PauseShortcut._mod3 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT)
            {
                chkSHIFTPause.Checked = true;
            }
            if ((speakInfo.PauseShortcut._mod1 & Consts.MOD_WIN) == Consts.MOD_WIN)
            {
            }
            cbPauseKey.SelectedItem = speakInfo.PauseShortcut._key;

            if ((speakInfo.ResumeShortcut._mod1 & Consts.MOD_ALT) == Consts.MOD_ALT ||
                (speakInfo.ResumeShortcut._mod2 & Consts.MOD_ALT) == Consts.MOD_ALT ||
                (speakInfo.ResumeShortcut._mod3 & Consts.MOD_ALT) == Consts.MOD_ALT)
            {
                chkALTResume.Checked = true;
            }
            if ((speakInfo.ResumeShortcut._mod1 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL ||
                (speakInfo.ResumeShortcut._mod2 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL ||
                (speakInfo.ResumeShortcut._mod3 & Consts.MOD_CONTROL) == Consts.MOD_CONTROL)
            {
                chkCTRLResume.Checked = true;
            }
            if ((speakInfo.ResumeShortcut._mod1 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT ||
                (speakInfo.ResumeShortcut._mod2 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT ||
                (speakInfo.ResumeShortcut._mod3 & Consts.MOD_SHIFT) == Consts.MOD_SHIFT)
            {
                chkSHIFTResume.Checked = true;
            }
            if ((speakInfo.ResumeShortcut._mod1 & Consts.MOD_WIN) == Consts.MOD_WIN)
            {
            }
            cbResumeKey.SelectedItem = speakInfo.ResumeShortcut._key;

            if (_uiInputModified)
            {
                speakInfo.Save();
                _uiInputModified = false;
            }
        }

        void LoadSpeakInfoFromUI(SpeakAloudInfo speakInfo)
        {
            try
            {
                speakInfo.Clear();
                speakInfo.Voice = cbVoice.SelectedItem as string;
                if (chkALTSpeak.Checked) { speakInfo.StartShortcut._mod1 = Consts.MOD_ALT; }
                if (chkCTRLSpeak.Checked) { speakInfo.StartShortcut._mod2 = Consts.MOD_CONTROL; }
                if (chkShiftSpeak.Checked) { speakInfo.StartShortcut._mod1 = Consts.MOD_SHIFT; }
                speakInfo.StartShortcut._key = (Keys)cbSpeakKey.SelectedItem;

                if (chkALTStop.Checked) { speakInfo.StopShortcut._mod1 = Consts.MOD_ALT; }
                if (chkCTRLStop.Checked) { speakInfo.StopShortcut._mod2 = Consts.MOD_CONTROL; }
                if (chkSHIFTStop.Checked) { speakInfo.StopShortcut._mod1 = Consts.MOD_SHIFT; }
                speakInfo.StopShortcut._key = (Keys)cbStopKey.SelectedItem;

                if (chkALTClipboard.Checked) { speakInfo.ReadClipboardShortcut._mod1 = Consts.MOD_ALT; }
                if (chkCTRLClipboard.Checked) { speakInfo.ReadClipboardShortcut._mod2 = Consts.MOD_CONTROL; }
                if (chkSHIFTClipboard.Checked) { speakInfo.ReadClipboardShortcut._mod1 = Consts.MOD_SHIFT; }
                speakInfo.ReadClipboardShortcut._key = (Keys)cbKeyClipboard.SelectedItem;

                if (chkALTPause.Checked) { speakInfo.PauseShortcut._mod1 = Consts.MOD_ALT; }
                if (chkCTRLPause.Checked) { speakInfo.PauseShortcut._mod2 = Consts.MOD_CONTROL; }
                if (chkSHIFTPause.Checked) { speakInfo.PauseShortcut._mod1 = Consts.MOD_SHIFT; }
                speakInfo.PauseShortcut._key = (Keys)cbPauseKey.SelectedItem;

                if (chkALTResume.Checked) { speakInfo.ResumeShortcut._mod1 = Consts.MOD_ALT; }
                if (chkCTRLResume.Checked) { speakInfo.ResumeShortcut._mod2 = Consts.MOD_CONTROL; }
                if (chkSHIFTResume.Checked) { speakInfo.ResumeShortcut._mod1 = Consts.MOD_SHIFT; }
                speakInfo.ResumeShortcut._key = (Keys)cbResumeKey.SelectedItem;

                speakInfo.Volume = int.Parse(txtVolume.Text);
                speakInfo.Speed = int.Parse(cbSpeed.SelectedItem.ToString());
            }
            catch
            {
            }
        }

        void SpeakAloud_Resize(object sender, EventArgs e)
        {
            //this.Hide();
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.Visible = false;
                this.SendToBack();
                //this.ShowInTaskbar = false;
            }
        }

        void SpeakAloud_LostFocus(object sender, EventArgs e)
        {
            if (_uiInputModified)
            {
                _speakAloudInfo.Save();
            }
        }

        void SpeakAloud_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_applicationExitInitiated)
            {
                e.Cancel = true;
                //this.Visible = false;
                this.SendToBack();
                //this.ShowInTaskbar = false;
            }
        }

        private void ExitSpeakAloud()
        {
            _speakAloudInfo.UnregisterHotKeys(this.Handle);
            UnregisterClipboardViewer();
            if (_speachSyn != null)
            {
                _speachSyn.Dispose();
                _speachSyn = null;
            }
            _applicationExitInitiated = true;
            Application.Exit();
        }

        private void RegisterClipboardViewer()
        {
            if (this.InvokeRequired)
            {
                NoArgumentDel del = new NoArgumentDel(this.RegisterClipboardViewer);
                this.Invoke(del);
                return;
            }

            _clipboardViewerNext = SetClipboardViewer(this.Handle);
        }
        private void UnregisterClipboardViewer()
        {
            if (this.InvokeRequired)
            {
                NoArgumentDel del = new NoArgumentDel(this.UnregisterClipboardViewer);
                this.Invoke(del);
                return;
            }

            ChangeClipboardChain(this.Handle, _clipboardViewerNext);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                //myCp.ClassStyle = myCp.ClassStyle | Consts.CP_NOCLOSE_BUTTON;
                //myCp.Style &= ~WS_SYSMENU;
                return myCp;
            }
        }

        private void StartSpeakStub(object info)
        {
            string text = info as string;
            StartSpeak(text);
        }

        private void StartSpeak(object info)
        {
            if (this.InvokeRequired)
            {
                OneArgumentDel del = new OneArgumentDel(this.StartSpeak);
                this.Invoke(del, info);
                return;
            }
            string speechText = info as string;
            Program.OutputMessage("StartSpeek : {0}", speechText);

            if (string.IsNullOrEmpty(speechText))
            {
                Program.OutputMessage("StartSpeak: No text to speak");
                return;
            }

            //cmdPause.Enabled = false;
            //cmdPause.Text = "Pause";
            //cmdStop.Enabled = false;
            if (_speachSyn != null)
            {
                try
                {
                    _speachSyn.Dispose();
                }
                catch (Exception e)
                {
                    Program.OutputMessage("Exception thrown : {0}\r\n{1}", e.Message, e.StackTrace);
                }
            }

            _speechState = SynthesizerState.Ready;
            _speachSyn = new SpeechSynthesizer();
            _speachSyn.StateChanged += _speachSyn_StateChanged;

            int volume = 100;
            int speed = 0;
            int.TryParse(txtVolume.Text, out volume);
            int.TryParse(cbSpeed.SelectedItem as string, out speed);
            string voice = cbVoice.SelectedItem as string;
            if (string.IsNullOrEmpty(voice))
            {
                voice = cbVoice.Items[0].ToString();
            }

            _speachSyn.SelectVoice(voice);
            _speachSyn.Volume = volume;
            _speachSyn.Rate = speed;
            _speachSyn.SetOutputToDefaultAudioDevice();
            _speachSyn.SpeakAsync(speechText);
        }

        private void chkCTRLSpeak_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }
            _uiInputModified = true;
            SpeakAloudInfo newInfo = new SpeakAloudInfo();
            LoadSpeakInfoFromUI(newInfo);
            newInfo.Save();
            _uiInputModified = false;

            if (!_speakAloudInfo.StartShortcut.Equals(newInfo.StartShortcut))
            {
                _speakAloudInfo.StartShortcut.UnregisterKey(this.Handle);
                newInfo.StartShortcut.RegisterKey(this.Handle);
            }
            if (!_speakAloudInfo.StopShortcut.Equals(newInfo.StopShortcut))
            {
                _speakAloudInfo.StopShortcut.UnregisterKey(this.Handle);
                newInfo.StopShortcut.RegisterKey(this.Handle);
            }
            if (!_speakAloudInfo.ReadClipboardShortcut.Equals(newInfo.ReadClipboardShortcut))
            {
                _speakAloudInfo.ReadClipboardShortcut.UnregisterKey(this.Handle);
                newInfo.ReadClipboardShortcut.RegisterKey(this.Handle);
            }
            if (!_speakAloudInfo.PauseShortcut.Equals(newInfo.PauseShortcut))
            {
                _speakAloudInfo.PauseShortcut.UnregisterKey(this.Handle);
                newInfo.PauseShortcut.RegisterKey(this.Handle);
            }
            if (!_speakAloudInfo.ResumeShortcut.Equals(newInfo.ResumeShortcut))
            {
                _speakAloudInfo.ResumeShortcut.UnregisterKey(this.Handle);
                newInfo.ResumeShortcut.RegisterKey(this.Handle);
            }

            _speakAloudInfo = newInfo;
        }

        private void cmdPause_Click(object sender, EventArgs e)
        {
            if (_speechState == SynthesizerState.Speaking)
            {
                //cmdPause.Text = "Resume";
                //cmdPause.Enabled = true;
                _speachSyn.Pause();
            }
            else if (_speechState == SynthesizerState.Paused)
            {
                //cmdPause.Text = "Pause";
                //cmdPause.Enabled = true;
                _speachSyn.Resume();
            }
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            _speachSyn.Dispose();
            _speachSyn = null;
        }

        #region getting text to speack etc handling
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Consts.WM_QUERYENDSESSION:
                    {
                        _applicationExitInitiated = true;
                        string myPath = Application.ExecutablePath;
                        Consts.RegisterApplicationRestart(myPath, 0);
                    }
                    break;

                case Consts.WM_DRAWCLIPBOARD:
                    if (_getSpeechTextUsingClipboard)
                    {
                        _getSpeechTextUsingClipboard = false;
                        if (Clipboard.ContainsText())
                        {
                            if (_dtClipboardGetTextStart.AddSeconds(10) >= DateTime.Now)
                            {
                                System.Threading.ThreadPool.QueueUserWorkItem(this.StartSpeakStub, Clipboard.GetText());
                                //StartSpeak(Clipboard.GetText());
                            }
                            //this.textBox1.AppendText(Clipboard.GetText());
                        }
                        Clipboard.Clear();
                    }
                    else
                    {
                        SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }
                    break;

                case Consts.WM_CHANGECBCHAIN:
                    if (m.WParam == _clipboardViewerNext)
                    {
                        _clipboardViewerNext = m.LParam;
                    }
                    else
                    {
                        SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }
                    break;

                case Consts.WM_HOTKEY:
                    int low = m.LParam.ToInt32() & 0x0000FFFF;
                    int high = m.LParam.ToInt32() >> 16;

                    if (low == _speakAloudInfo.StartShortcut.Modifiers &&
                        //(low & _speakAloudInfo.StartShortcut._mod1) == _speakAloudInfo.StartShortcut._mod1 &&
                        //(low & _speakAloudInfo.StartShortcut._mod2) == _speakAloudInfo.StartShortcut._mod2 &&
                        //(low & _speakAloudInfo.StartShortcut._mod3) == _speakAloudInfo.StartShortcut._mod3 &&
                        (high == (int)_speakAloudInfo.StartShortcut._key))
                    {
                        System.Threading.ThreadPool.QueueUserWorkItem(this.InitiateTextToSpeech, null);
                        //InitiateTextToSpeech();
                        break;
                    }
                    if (low == _speakAloudInfo.StopShortcut.Modifiers && 
                        //(low & _speakAloudInfo.StopShortcut._mod1) == _speakAloudInfo.StopShortcut._mod1 &&
                        //(low & _speakAloudInfo.StopShortcut._mod2) == _speakAloudInfo.StopShortcut._mod2 &&
                        //(low & _speakAloudInfo.StopShortcut._mod3) == _speakAloudInfo.StopShortcut._mod3 &&
                        (high == (int)_speakAloudInfo.StopShortcut._key))
                    {
                        System.Threading.ThreadPool.QueueUserWorkItem(this.InitiateSpeechStop, null);
                        //InitiateSpeechStop();
                        break;
                    }
                    if (low == _speakAloudInfo.ReadClipboardShortcut.Modifiers &&
                        //(low & _speakAloudInfo.ReadClipboardShortcut._mod1) == _speakAloudInfo.ReadClipboardShortcut._mod1 &&
                        //(low & _speakAloudInfo.ReadClipboardShortcut._mod2) == _speakAloudInfo.ReadClipboardShortcut._mod2 &&
                        //(low & _speakAloudInfo.ReadClipboardShortcut._mod3) == _speakAloudInfo.ReadClipboardShortcut._mod3 &&
                        (high == (int)_speakAloudInfo.ReadClipboardShortcut._key))
                    {
                        System.Threading.ThreadPool.QueueUserWorkItem(this.InitiateReadClipboardText, null);
                        //InitiateSpeechStop();
                        break;
                    }
                    if (low == _speakAloudInfo.PauseShortcut.Modifiers &&
                        //(low & _speakAloudInfo.PauseShortcut._mod1) == _speakAloudInfo.PauseShortcut._mod1 &&
                        //(low & _speakAloudInfo.PauseShortcut._mod2) == _speakAloudInfo.PauseShortcut._mod2 &&
                        //(low & _speakAloudInfo.PauseShortcut._mod3) == _speakAloudInfo.PauseShortcut._mod3 &&
                        (high == (int)_speakAloudInfo.PauseShortcut._key))
                    {
                        System.Threading.ThreadPool.QueueUserWorkItem(this.InitiatePauseSpeech, null);
                        //InitiateSpeechStop();
                        break;
                    }
                    if (low == _speakAloudInfo.ResumeShortcut.Modifiers &&
                        //(low & _speakAloudInfo.ResumeShortcut._mod1) == _speakAloudInfo.ResumeShortcut._mod1 &&
                        //(low & _speakAloudInfo.ResumeShortcut._mod2) == _speakAloudInfo.ResumeShortcut._mod2 &&
                        //(low & _speakAloudInfo.ResumeShortcut._mod3) == _speakAloudInfo.ResumeShortcut._mod3 &&
                        (high == (int)_speakAloudInfo.ResumeShortcut._key))
                    {
                        System.Threading.ThreadPool.QueueUserWorkItem(this.InitiateResumeSpeech, null);
                        //InitiateSpeechStop();
                        break;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        private void InitiateReadClipboardText(object info)
        {
            if (this.InvokeRequired)
            {
                OneArgumentDel del = new OneArgumentDel(this.InitiateTextToSpeech);
                this.Invoke(del, info);
                return;
            }

            string readText = null;
            if (Clipboard.ContainsText())
            {
                readText = Clipboard.GetText();
                StartSpeak(readText);
            }
        }

        private void InitiatePauseSpeech(object info)
        {
            if (this.InvokeRequired)
            {
                OneArgumentDel del = new OneArgumentDel(this.InitiatePauseSpeech);
                this.Invoke(del, info);
                return;
            }

            if (_speechState == SynthesizerState.Speaking)
            {
                _speachSyn.Pause();
            }
        }

        private void InitiateResumeSpeech(object info)
        {
            if (this.InvokeRequired)
            {
                OneArgumentDel del = new OneArgumentDel(this.InitiateResumeSpeech);
                this.Invoke(del, info);
                return;
            }

            if (_speechState == SynthesizerState.Paused)
            {
                _speachSyn.Resume();
            }
        }
        
        private void InitiateSpeechStop(object info)
        {
            if (_speechState != SynthesizerState.Speaking)
            {
                return;
            }

            if (this.InvokeRequired)
            {
                OneArgumentDel del = new OneArgumentDel(this.InitiateSpeechStop);
                this.Invoke(del, info);
                return;
            }

            if (this._speachSyn != null)
            {
                this._speachSyn.Dispose();
                this._speachSyn = null;
            }
        }

        private void InitiateTextToSpeech(object info)
        {
            if (this.InvokeRequired)
            {
                OneArgumentDel del = new OneArgumentDel(this.InitiateTextToSpeech);
                this.Invoke(del, info);
                return;
            }

            string selText = null;
            if (GetSelectedText(out selText))
            {
                if (!string.IsNullOrEmpty(selText))
                {
                    string tmpSelText = selText.Trim(new char[] { '\r', '\n', ' ', '\t' });
                    if (selText.Length > 2)
                    {
                        //System.Threading.ThreadPool.QueueUserWorkItem(this.StartSpeakStub, selText);
                        StartSpeak(selText);
                        //UpdateMessage(selText);
                        return;
                    }
                }
            }
            _getSpeechTextUsingClipboard = true;
            _dtClipboardGetTextStart = DateTime.Now;
            SendKeys.SendWait("^c");
            _sendKeysCounter++;

            // sometime clipboard registration is stopping to work, this is to detect if clipboard registration is
            // no more working and trying to re-register it
            int localCounter = _sendKeysCounter;
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(this.CheckIfSpeechStarted), localCounter);
        }

        private void CheckIfSpeechStarted(object info)
        {
            int counter = (int)info;
            System.Threading.Thread.Sleep(10000);

            if (_getSpeechTextUsingClipboard && _sendKeysCounter == counter)
            {
                if (Clipboard.ContainsText())
                {
                    StartSpeak(Clipboard.GetText());
                }
                UnregisterClipboardViewer();
                RegisterClipboardViewer();
            }
        }

        private static bool GetSelectedText(out string selText)
        {
            bool bRet = false;
            selText = null;
            var element = AutomationElement.FocusedElement;

            if (element != null)
            {
                try
                {
                    object pattern = null;
                    if (element.TryGetCurrentPattern(TextPattern.Pattern, out pattern))
                    {
                        var tp = (TextPattern)pattern;
                        var sb = new StringBuilder();

                        foreach (var r in tp.GetSelection())
                        {
                            sb.AppendLine(r.GetText(-1));
                        }

                        var selectedText = sb.ToString();
                        selText = selectedText;
                        //Console.WriteLine("Text={0}", selectedText);
                        bRet = true;
                    }
                    else
                    {
                        Program.OutputMessage("element.TryGetCurrentPattern failed");
                    }
                }
                catch (Exception e)
                {
                    Program.OutputMessage("exception = {0}", e.Message);
                    Program.OutputMessage("stack = {0}", e.StackTrace);
                }
            }
            else
            {
                Program.OutputMessage("element is null");
            }
            return bRet;
        }
        #endregion

        #region notifyicon
        private void InitNotifyIcon()
        {
            //ContextMenu context = new ContextMenu();
            //MenuItem miOpen = new MenuItem("Open");
            //miOpen.Click += miContext_Click;
            //MenuItem miClose = new MenuItem("Exit");
            //miClose.Click += miContext_Click;

            //context.MenuItems.Add(miOpen);
            //context.MenuItems.Add(miClose);
            //this.notifyIcon1.ContextMenu = context;
            ToolStripButton open = new ToolStripButton("Open");
            open.DisplayStyle = ToolStripItemDisplayStyle.Text;
            open.Click += miContext_Click;
            ToolStripButton exit = new ToolStripButton("Exit");
            exit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            exit.Click += miContext_Click;
            contextMenuStrip1.Items.Add(open);
            contextMenuStrip1.Items.Add(exit);
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        }

        void miContext_Click(object sender, EventArgs e)
        {
            ToolStripButton mi = sender as ToolStripButton;
            if (mi == null)
            {
                return;
            }

            switch (mi.Text.ToUpper())
            {
                case "OPEN":
                    //this.ShowInTaskbar = true;
                    //this.Visible = true;
                    //this.TopMost = true;
                    //this.TopLevel = true;
                    this.WindowState = FormWindowState.Normal;
                    this.BringToFront();
                    //this.TopMost = false;
                    break;

                case "EXIT":
                    ExitSpeakAloud();
                    break;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //this.notifyIcon1.ContextMenu.Show(null, Control.MousePosition);
            this.contextMenuStrip1.Show(Control.MousePosition);
        }
        #endregion

        #region caption handling etc
        private void InitTitleBar()
        {
            pictureBox1.Image = imageList1.Images[1];
            lblTitleBar.MouseMove += lblTitleBar_MouseMove;
            lblTitleBar.MouseDown += lblTitleBar_MouseDown;
            lblTitleBar.MouseUp += lblTitleBar_MouseUp;
        }

        void lblTitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            _moveStartPoint = new Point(-1, -1);            
        }

        void lblTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                _moveStartPoint = e.Location;
            }
            else
            {
                _moveStartPoint = new Point(-1, -1);
            }
        }

        void lblTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left && _moveStartPoint.X > 0 && _moveStartPoint.Y > 0)
            {
                int dx = e.Location.X - _moveStartPoint.X;
                int dy = e.Location.Y - _moveStartPoint.Y;

                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            this.SendToBack();
            this.WindowState = FormWindowState.Minimized;
            //this.ShowInTaskbar = false;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.Image = imageList1.Images[0];
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = imageList1.Images[1];
        }
        #endregion
    }

    internal class Consts
    {
        internal const int WM_DRAWCLIPBOARD = 0x0308;
        internal const int WM_CHANGECBCHAIN = 0x030D;
        internal const int MOD_ALT = 0x1;
        internal const int MOD_CONTROL = 0x2;
        internal const int MOD_SHIFT = 0x4;
        internal const int MOD_WIN = 0x8;
        internal const int WM_HOTKEY = 0x312;
        internal const int CP_NOCLOSE_BUTTON = 0x200;
        internal const int WS_SYSMENU = 0x80000;
        internal const int WM_QUERYENDSESSION = 0x11;
        internal const int WM_ENDSESSION = 0x16;
        internal const int StartHotkeyId = 1;
        internal const int StopHotkeyId = 2;
        internal const int ReadClipboardHotkeyId = 3;
        internal const int PauseHotkeyId = 4;
        internal const int ResumeHotkeyId = 5;

        [DllImport("kernel32.dll", SetLastError = true)]
        static internal extern int RegisterApplicationRestart([MarshalAs(UnmanagedType.LPWStr)] string commandLineArgs, int Flags);
    }

    internal class SpeakAloudInfo
    {
        internal string Voice = null;
        internal int Volume = 100;
        internal int Speed = 0;
        internal ShortcutKey StartShortcut = null;
        internal ShortcutKey StopShortcut = null;
        internal ShortcutKey ReadClipboardShortcut = null;
        internal ShortcutKey PauseShortcut = null;
        internal ShortcutKey ResumeShortcut = null;

        internal SpeakAloudInfo()
        {
            Load();
        }

        public override bool Equals(object obj)
        {
            SpeakAloudInfo sai = obj as SpeakAloudInfo;

            if (sai == null)
            {
                return false;
            }

            if (string.Compare(this.Voice, sai.Voice, true) == 0 &&
                this.StartShortcut.Equals(sai.StartShortcut) &&
                this.StopShortcut.Equals(sai.StopShortcut) &&
                this.ReadClipboardShortcut.Equals(sai.ReadClipboardShortcut) &&
                this.PauseShortcut.Equals(sai.PauseShortcut) &&
                this.ResumeShortcut.Equals(sai.ResumeShortcut) &&
                this.Volume == sai.Volume &&
                this.Speed == sai.Speed)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        internal void Clear()
        {
            Voice = null;
            StartShortcut = new ShortcutKey();
            StopShortcut = new ShortcutKey();
            ReadClipboardShortcut = new ShortcutKey();
            PauseShortcut = new ShortcutKey();
            ResumeShortcut = new ShortcutKey();
            Speed = 0;
            Volume = 100;
        }

        internal void Load()
        {
            Voice = null;
            if (Properties.Settings.Default["SPEAKVOICDE"] != null)
            {
                Voice = Properties.Settings.Default["SPEAKVOICDE"].ToString();
            }
            string startSk = null;
            if (Properties.Settings.Default["STARTSHORTCUT"] != null)
            {
                startSk = Properties.Settings.Default["STARTSHORTCUT"].ToString();
            }
            string stopSk = null;
            if (Properties.Settings.Default["STOPSHORTCUT"] != null)
            {
                stopSk = Properties.Settings.Default["STOPSHORTCUT"].ToString();
            }
            string readClipboardSK = null;
            if (Properties.Settings.Default["READCLIPBOARDSHORTCUT"] != null)
            {
                readClipboardSK = Properties.Settings.Default["READCLIPBOARDSHORTCUT"].ToString();
            }
            string pauseSK = null;
            if (Properties.Settings.Default["PAUSESHORTCUT"] != null)
            {
                pauseSK = Properties.Settings.Default["PAUSESHORTCUT"].ToString();
            }
            string resumeSK = null;
            if (Properties.Settings.Default["RESUMESHORTCUT"] != null)
            {
                resumeSK = Properties.Settings.Default["RESUMESHORTCUT"].ToString();
            }
            string strVolume = null;
            if (Properties.Settings.Default["VOLUME"] != null)
            {
                strVolume = Properties.Settings.Default["VOLUME"].ToString();
            }
            string strSpeed= null;
            if (Properties.Settings.Default["SPEED"] != null)
            {
                strSpeed = Properties.Settings.Default["SPEED"].ToString();
            }

            StartShortcut = ShortcutKey.Parse(startSk);
            StopShortcut = ShortcutKey.Parse(stopSk);
            ReadClipboardShortcut = ShortcutKey.Parse(readClipboardSK);
            PauseShortcut = ShortcutKey.Parse(pauseSK);
            ResumeShortcut = ShortcutKey.Parse(resumeSK);

            if (StartShortcut == null)
            {
                StartShortcut = new ShortcutKey();
                StartShortcut._mod1 = 0x00;
                StartShortcut._mod2 = Consts.MOD_CONTROL;
                StartShortcut._mod3 = 0x00;
                StartShortcut._key = Keys.D1;
            }
            StartShortcut._hotkeyId = Consts.StartHotkeyId;

            if (StopShortcut == null)
            {
                StopShortcut = new ShortcutKey();
                StopShortcut._mod1 = 0x00;
                StopShortcut._mod2 = Consts.MOD_CONTROL;
                StopShortcut._mod3 = 0x00;
                StopShortcut._key = Keys.D2;
            }
            StopShortcut._hotkeyId = Consts.StopHotkeyId;

            if (ReadClipboardShortcut == null)
            {
                ReadClipboardShortcut = new ShortcutKey();
                ReadClipboardShortcut._mod1 = 0x00;
                ReadClipboardShortcut._mod2 = Consts.MOD_CONTROL;
                ReadClipboardShortcut._mod3 = 0x00;
                ReadClipboardShortcut._key = Keys.D3;
            }
            ReadClipboardShortcut._hotkeyId = Consts.ReadClipboardHotkeyId;

            if (PauseShortcut == null)
            {
                PauseShortcut = new ShortcutKey();
                PauseShortcut._mod1 = 0x00;
                PauseShortcut._mod2 = Consts.MOD_CONTROL;
                PauseShortcut._mod3 = Consts.MOD_SHIFT;
                PauseShortcut._key = Keys.D2;
            }
            PauseShortcut._hotkeyId = Consts.PauseHotkeyId;

            if (ResumeShortcut == null)
            {
                ResumeShortcut = new ShortcutKey();
                ResumeShortcut._mod1 = 0x00;
                ResumeShortcut._mod2 = Consts.MOD_CONTROL;
                ResumeShortcut._mod3 = Consts.MOD_SHIFT; ;
                ResumeShortcut._key = Keys.D1;
            }
            ResumeShortcut._hotkeyId = Consts.ResumeHotkeyId;

            if (string.IsNullOrEmpty(Voice))
            {
                SpeechSynthesizer speachSyn = new SpeechSynthesizer();
                Voice = speachSyn.GetInstalledVoices().FirstOrDefault().VoiceInfo.Name;
                speachSyn.Dispose();
            }

            if (!int.TryParse(strVolume, out this.Volume))
            {
                this.Volume = 100;
            }

            if (!int.TryParse(strSpeed, out this.Speed))
            {
                this.Speed = 0;
            }

            if (this.Volume > 100 || this.Volume < 0)
            {
                this.Volume = 100;
            }

            if (this.Speed > 10 || this.Speed < -10)
            {
                this.Speed = 0;
            }
        }

        internal void Save()
        {
            Properties.Settings.Default["SPEAKVOICDE"] = Voice;
            Properties.Settings.Default["VOLUME"] = Volume.ToString();
            Properties.Settings.Default["SPEED"] = Speed.ToString();
            Properties.Settings.Default["STARTSHORTCUT"] = StartShortcut.ToString();
            Properties.Settings.Default["STOPSHORTCUT"] = StopShortcut.ToString();
            Properties.Settings.Default["READCLIPBOARDSHORTCUT"] = ReadClipboardShortcut.ToString();
            Properties.Settings.Default["PAUSESHORTCUT"] = PauseShortcut.ToString();
            Properties.Settings.Default["RESUMESHORTCUT"] = ResumeShortcut.ToString();
            Properties.Settings.Default.Save();       
        }

        internal void RegisterHotKeys(IntPtr handle)
        {
            StartShortcut.RegisterKey(handle);
            StopShortcut.RegisterKey(handle);
            ReadClipboardShortcut.RegisterKey(handle);
            PauseShortcut.RegisterKey(handle);
            ResumeShortcut.RegisterKey(handle);
        }

        internal void UnregisterHotKeys(IntPtr handle)
        {
            StartShortcut.UnregisterKey(handle);
            StopShortcut.UnregisterKey(handle);
            ReadClipboardShortcut.UnregisterKey(handle);
            PauseShortcut.UnregisterKey(handle);
            ResumeShortcut.UnregisterKey(handle);
        }
    }

    internal class ShortcutKey
    {
        #region external functions
        [DllImport("user32.dll", SetLastError=true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll", SetLastError=true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll")]
        static extern uint GetLastError();
        #endregion

        internal int _mod1 = 0;
        internal int _mod2 = 0;
        internal int _mod3 = 0;
        internal Keys _key = Keys.D1;
        internal int _hotkeyId = 0;

        public override string ToString()
        {
            string ts = string.Format("{0}:{1}:{2}:{3}", _mod1, _mod2, _mod3, _key);
            return ts;
        }

        public override bool Equals(object obj)
        {
            ShortcutKey comp = obj as ShortcutKey;
            if (comp == null)
            {
                return false;
            }

            if (this._mod1 == comp._mod1 &&
                this._mod2 == comp._mod2 &&
                this._mod3 == comp._mod3 &&
                this._key == comp._key)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        internal int Modifiers { get { return _mod1 | _mod2 | _mod3; } }

        internal static ShortcutKey Parse(string strKey)
        {
            ShortcutKey shortcut = null;

            if (string.IsNullOrEmpty(strKey))
            {
                goto BAILOUT;
            }

            string[] strAr = strKey.Split(new char[] { ':' });
            int mod1 = 0;
            int mod2 = 0;
            int mod3 = 0;
            Keys key;

            if (strAr == null || strAr.Length != 4)
            {
                goto BAILOUT;
            }

            if (!int.TryParse(strAr[0], out mod1))
            {
                goto BAILOUT;
            }
            if (!int.TryParse(strAr[1], out mod2))
            {
                goto BAILOUT;
            }
            if (!int.TryParse(strAr[2], out mod3))
            {
                goto BAILOUT;
            }

            try
            {
                key = (Keys)Enum.Parse(typeof(Keys), strAr[3]);
            }
            catch
            {
                goto BAILOUT;
            }

            shortcut = new ShortcutKey();
            shortcut._mod1 = mod1;
            shortcut._mod2 = mod2;
            shortcut._mod3 = mod3;
            shortcut._key = key;

        BAILOUT:
            return shortcut;
        }

        internal bool RegisterKey(IntPtr handle)
        {
            bool bRet = RegisterHotKey(handle, _hotkeyId, this.Modifiers, (int)_key);
            if (bRet != true)
            {
                Program.OutputMessage("RegisterHotKey failed : {0}", GetLastError());
            }
            return bRet;
        }

        internal bool UnregisterKey(IntPtr handle)
        {
            bool bRet = UnregisterHotKey(handle, _hotkeyId);
            if (bRet != true)
            {
                Program.OutputMessage("UnregisterHotKey failed : {0}", GetLastError());
            }
            return bRet;
        }
    }
}
