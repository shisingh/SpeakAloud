using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace GetWindowsText_Winform
{
    public partial class Form1 : Form
    {
        private const int WM_DRAWCLIPBOARD      = 0x0308;
        private const int WM_CHANGECBCHAIN      = 0x030D;
        private const int MOD_ALT = 0x1;
        private const int MOD_CONTROL = 0x2;
        private const int MOD_SHIFT = 0x4;
        private const int MOD_WIN = 0x8;
        private const int WM_HOTKEY = 0x312;
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(
            IntPtr hWndRemove,  // handle to window to remove
            IntPtr hWndNewNext  // handle to next window
            );

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id); 
        
        IntPtr _clipboardViewerNext = IntPtr.Zero;
        private bool _getCopyValue = false;
        private bool _hotkeyRegistered = false;
        Timer _timer = new Timer();

        private delegate void UpdateMsgDel(string msg);
        public Form1()
        {
            InitializeComponent();

            this.FormClosing += Form1_FormClosing;
            RegisterClipboardViewer();
            _hotkeyRegistered = RegisterHotKey(this.Handle, 1, MOD_CONTROL, (int)Keys.D1);
            if (!_hotkeyRegistered)
            {
                MessageBox.Show("Error registering hotkey");
            }

            _timer.Tick += _timer_Tick;
            _timer.Interval = 2000;
            _timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            //CopyFromActiveProgram();
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Stop();
            UnregisterClipboardViewer();
            if (_hotkeyRegistered)
            {
                UnregisterHotKey(this.Handle, 1);
            }
        }

        public void RegisterClipboardViewer()
        {
            _clipboardViewerNext = SetClipboardViewer(this.Handle);
        }

        public void UnregisterClipboardViewer()
        {
            ChangeClipboardChain(this.Handle, _clipboardViewerNext);
        }

        private void CopyFromActiveProgram()
        {
            string selText = null;
            if (GetSelectedText(out selText))
            {
                if (!string.IsNullOrEmpty(selText))
                {
                    string tmpSelText = selText.Trim(new char[] { '\r', '\n', ' ', '\t' });
                    if (selText.Length > 2)
                    {
                        UpdateMessage(selText);
                        return;
                    }
                }
            }
            _getCopyValue = true;
            SendKeys.SendWait("^c");
        }

        private void UpdateMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                UpdateMsgDel del = new UpdateMsgDel(this.UpdateMessage);
                this.Invoke(del, msg);
                return;
            }

            this.textBox1.AppendText(msg + "\r\n");
        }
        
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    if (_getCopyValue && Clipboard.ContainsText())
                    {
                        _getCopyValue = false;
                        this.textBox1.AppendText(Clipboard.GetText());
                    }
                    SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == _clipboardViewerNext)
                    {
                        _clipboardViewerNext = m.LParam;
                    }
                    else
                    {
                        SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }
                    break;

                case WM_HOTKEY:
                    CopyFromActiveProgram();
                    break;
            }

            base.WndProc(ref m);
        }

        private void cmdGet_Click(object sender, EventArgs e)
        {
            CopyFromActiveProgram();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
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
                        Console.WriteLine("element.TryGetCurrentPattern failed");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("exception = {0}", e.Message);
                    Console.WriteLine("stack = {0}", e.StackTrace);
                }
            }
            else
            {
                Console.WriteLine("element is null");
            }
            return bRet;
        }

    }
}
