using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace WKeyTiming
{
    public partial class KeyTimingForm : Form
    {

        Stopwatch _stop_watch = new Stopwatch();

        private GlobalKeyboardHook _globalKeyboardHook;

        int _last_key_press = -1;
        TimeSpan _last_timespan = new TimeSpan(0);

        delegate void setTextCallback(string txt);

        KeysConverter _key_converter = new KeysConverter();

        public KeyTimingForm()
        {
            InitializeComponent();
        }

        private void KeyTimingForm_Load(object sender, EventArgs e)
        {
            SetupKeyboardHooks();

            _stop_watch.Start();
        }

        public void SetupKeyboardHooks()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                //e.Handled = true;
                string msg;
                if (e.KeyboardData.VirtualCode != _last_key_press)
                {
                    _last_key_press = e.KeyboardData.VirtualCode;

                    msg = string.Format("Key {0}\r\n", _key_converter.ConvertToString(e.KeyboardData.VirtualCode));
                    setOutputStatusText(msg);
                }

                msg = string.Format("Time {0}\r\n", _stop_watch.Elapsed.TotalMilliseconds);
                setOutputStatusText(msg);

                _stop_watch.Restart();

            }

            void setOutputStatusText(string text)
            {
                if (Status_textBox.InvokeRequired)
                {
                    setTextCallback d = new setTextCallback(setOutputStatusText);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    Status_textBox.AppendText(text);
                    Status_textBox.Update();
                }
            }
        }

    }
}
