using ACM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    static class WindowsFormsExceptionHandler
    {
        public static void GloblalExceptionSetup()
        {
            //for ui thread exceptions
            Application.ThreadException += new ThreadExceptionEventHandler(GlobalExceptionHandler);

            //force all windows forms errors to
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            //for non-ui thread exceptions
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalExceptionHandler);
        }

        private static void GlobalExceptionHandler(object sender, EventArgs args)
        {
            //Log the issue
            MessageBox.Show("There was a problem with this application. Please contact support");
            Application.Exit();
        }
    }
}
