using System;
using System.Diagnostics;
using System.Windows;

namespace CorpManagement.ToolBox
{
    class MyErrorException : Exception
    {
        /// <summary>
        /// Save app error to file and display a message box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="className"></param>
        /// <param name="method"></param>
        /// <param name="line"></param>
        public MyErrorException(string message, string title, string className, string method, int line)
        {
            if (Debugger.IsAttached)
            {
                MessageBox.Show(message + string.Format("\r\nClass '{0}', method '{1}', line '{2}'", className, method, line), title);
            }
            else
            {
                MessageBox.Show(message, title);
            }
        }

        /// <summary>
        /// Save database error to file
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="method"></param>
        /// <param name="line"></param>
        public MyErrorException(string message, string className, string method, int line, bool showMessageBox = false)
        {
            if (Debugger.IsAttached)
            {
                if (showMessageBox)
                    MessageBox.Show(message + string.Format("\r\nClass '{0}', method '{1}', line '{2}'", className, method, line), "Erreur avec la base de données");
            }
            else
            {
                MessageBox.Show(message, "Erreur avec la base de données");
            }
        }
    }
}
