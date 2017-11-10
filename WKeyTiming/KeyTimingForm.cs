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
            if (//e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown ||
                e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
            {
                //e.Handled = true;
                _key_queue.Enqueue(
                    new KeyTiming(e.KeyboardData.VirtualCode, _stop_watch.Elapsed, e.KeyboardState));

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

                    // Calulate frames at 60fps
                    double frames = kt.TimeSpan.TotalSeconds * 60;

                    string msg = string.Format("{0,8} ms, {1,6} frames, {2}\r\n", 
                        kt.TimeSpan.TotalMilliseconds.ToString("0.00"), frames.ToString("0.00"), kt.toString());
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

