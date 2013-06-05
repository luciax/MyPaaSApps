using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums
{
    /// <summary>
    /// Request status.
    /// </summary>
    public enum RequestStatus
    {
        /// <summary>
        /// New
        /// </summary>
        New,
        /// <summary>
        /// WaitingForApproval
        /// </summary>
        WaitingForApproval,
        /// <summary>
        /// WaitingForEffectiveDate
        /// </summary>
        WaitingForEffectiveDate,
        /// <summary>
        ///AutomationProvisionning,
        /// </summary>
        AutomationProvisionning,
        /// <summary>
        /// ManualProvisionning
        /// </summary>
        ManualProvisionning,
        /// <summary>
        /// Completed
        /// </summary>
        Completed,
        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled,
        /// <summary>
        /// TimedOut
        /// </summary>
        TimedOut
    }
}
