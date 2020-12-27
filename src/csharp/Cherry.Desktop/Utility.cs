using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Cherry.Desktop
{
    public class Utility
    {
        /// <summary>
        /// Set tab size for edit control.
        /// </summary>
        /// <remarks>
        /// EM_SETTABSTOPS - http://msdn.microsoft.com/en-us/library/bb761663%28VS.85%29.aspx
        /// </remarks>
        /// <param name="control">TextBox</param>
        /// <param name="size">int</param>
        public static void SetTabSize(Control control, int size)
        {
            int lParam = size * 4;
            WinApi.SendMessage(control.Handle, WinApi.EM_SETTABSTOPS, new IntPtr(1), ref lParam);
            control.Invalidate();
        }

        /// <summary>
        /// Go to the end of text box and scroll to.
        /// </summary>
        /// <param name="control"></param>
        public static void TextBoxGoEnd(TextBoxBase control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(() => TextBoxGoEnd(control)));
                return;
            }
            try
            {
                control.SelectionLength = 0;
                control.SelectionStart = control.Text.Length;
                control.ScrollToCaret();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <summary>
        /// Go to the begining of text box and scroll to.
        /// </summary>
        /// <param name="control"></param>
        public static void TextBoxGoUp(TextBoxBase control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(() => TextBoxGoUp(control)));
                return;
            }
            try
            {
                control.SelectionLength = 0;
                control.SelectionStart = 0;
                control.ScrollToCaret();
            }
            catch (ObjectDisposedException)
            {
            }
        }
    }
}
