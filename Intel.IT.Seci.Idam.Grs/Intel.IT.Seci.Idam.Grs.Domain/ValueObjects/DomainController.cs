using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.ValueObjects
{
    /// <summary>
    /// Class that represents a DomainController in AD, this class is mapped in the client side as a View Model
    /// you can find the View Model at the following path: Intel.IT.Seci.Idam.Grs\Intel.IT.Seci.Idam.Grs.MvcWeb\Scripts\CreateComputerViewModel.js 
    /// </summary>
    public class DomainController
    {
        /// <summary>
        /// Friendly name e.g. CORP, ISEDEV, CORPDEV
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// fulll name 
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Child domains
        /// </summary>
        public DomainController[] ChildDomains { get; set; }
        /// <summary>
        /// Parent Domain
        /// </summary>
        public string ParentFriendlyName { get; set; }
        /// <summary>
        /// Parent Domain Full Name
        /// </summary>
        public string ParentFullName { get; set; }
    }
}
