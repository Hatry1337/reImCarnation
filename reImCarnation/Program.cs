using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reImCarnation
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var f1 = new Form1())
            {
                f1.HotKeyId = 0x32;
                f1.FormClosed += (s, e) => {
                    Hotkey.Unregister(f1, f1.HotKeyId);
                };

                Hotkey.Register(f1, f1.HotKeyId, Modifiers.ALT, Keys.F3);
                Application.Run(f1);
            }
        }
    }
}
