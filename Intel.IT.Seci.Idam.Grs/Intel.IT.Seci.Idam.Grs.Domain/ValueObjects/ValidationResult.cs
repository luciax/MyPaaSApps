using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.ValueObjects
{   
    /// <summary>
    /// The validation result.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get;set;}
        /// <summary>
        /// True if the request is valid.
        /// </summary>
        public bool Valid { get;set;}
    }    
}
