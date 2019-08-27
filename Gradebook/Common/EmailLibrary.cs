using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EmailLibrary
    {
        /// <summary>
        /// If a valid email address is provided, send the message
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="message"></param>
        public void SendEmail(string emailAddress, string message)
        {
            try
            {
                //send an email
            }
            catch (InvalidOperationException exception)
            {

                //log
                throw;
            }
        }
    }
}
