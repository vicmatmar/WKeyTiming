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

        Queue<KeyTiming> _key_queue = new Queue<KeyTiming>();

        Task _dequeue_task;
        delegate void setTextCallback(string txt);

        public KeyTimingForm()
        {
            InitializeComponent();
        }

        private void KeyTimingForm_Load(object sender, EventArgs e)
        {
            SetupKeyboardHooks();
            _stop_watch.Start();

            _dequeue_task = new Task(() => dequeue());
            _dequeue_task.Start();
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
                _key_queue.Enqueue(
                    new KeyTiming(e.KeyboardData.VirtualCode, _stop_watch.Elapsed));

                _stop_watch.Restart();

            }
        }

        void dequeue()
        {
            while (true)
            {
                while (_key_queue.Count > 0)
                {
                    KeyTiming kt = _key_queue.Dequeue();
                    string msg = string.Format("{0},{1}\r\n", kt.toString(), kt.TimeSpan.TotalMilliseconds);
                    setOutputStatusText(msg);
                }
            }
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

