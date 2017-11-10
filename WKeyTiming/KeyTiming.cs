using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace WKeyTiming
{
    public class KeyTiming
    {
        public KeyTiming(int virtualcode, TimeSpan ts, GlobalKeyboardHook.KeyboardState keystate)
        {
            VirtualCode = virtualcode;
            TimeSpan = ts;
            Keystate = keystate;
        }

        public int VirtualCode { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public GlobalKeyboardHook.KeyboardState Keystate { get; set; }

        public string toString()
        {
            KeysConverter kc = new KeysConverter();
            return kc.ConvertToString(VirtualCode);
        }
    }
}
