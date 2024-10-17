using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISLibrary.Exception
{
    public class InvalidStudentDataException : System.Exception
    {
        public InvalidStudentDataException(string message) : base(message) { }
    }
}
