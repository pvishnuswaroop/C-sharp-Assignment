using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISLibrary.Exception
{
    public class PaymentValidationException : System.Exception
    {
        public PaymentValidationException(string message) : base(message) { }
    }
}

