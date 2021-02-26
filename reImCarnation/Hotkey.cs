using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace reImCarnation
{
    [Flags]
    enum Modifiers : uint
    {
        ALT = 0x0001,
        CONTROL = 0x0002,
        SHIFT = 0x0004,
        WIN = 0x0008
    }

    static class Hotkey
    {

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RegisterHotKey(IntPtr hWnd, int Id, Modifiers fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnregisterHotKey(IntPtr hWnd, int Id);

        public static bool Register(Form frm, int id, Modifiers mod, Keys key)
        {

            if (frm == null || frm.IsDisposed)
                return false;
            if ((uint)mod == 0U)
                return false;
            if (key == Keys.None)
                return false;

            return RegisterHotKey(frm.Handle, id, mod, key);
        }

        public static bool Unregister(Form frm, int id)
        {
            if (frm == null || frm.IsDisposed)
                return false;

            return UnregisterHotKey(frm.Handle, id);
        }
    }
}
