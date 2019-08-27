using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    /// <summary>
    /// Manages a payment
    /// </summary>
    public class Payment : EntityBase
    {
        public int PaymentType { get; set; }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }

        internal void ProcessPayment()
        {
            PaymentTypeOption paymentTypeOption;

            if(!Enum.TryParse(this.PaymentType.ToString(), out paymentTypeOption))
            {
                throw new InvalidEnumArgumentException
                    ("Payment type", (int)this.PaymentType, typeof(PaymentTypeOption));
            }

            switch (paymentTypeOption)
            {
                case PaymentTypeOption.CreditCard:
                    //process credit card
                    break;
                case PaymentTypeOption.PayPal:
                    //process paypal
                    break;
                default:
                    throw new ArgumentException();
            }

            //open a connection
            //set stored procedure parameters with the payment data
            //Call the save stored procedure
        }
    }
}
