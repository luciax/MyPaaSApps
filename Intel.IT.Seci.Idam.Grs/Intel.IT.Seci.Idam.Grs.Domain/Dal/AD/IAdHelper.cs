using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.Dal.AD
{
    /// <summary>
    /// AdHelper interface
    /// </summary>
    public interface IAdHelper
    {
        /// <summary>
        /// True if the object exists.
        /// </summary>
        /// <param name="samAccountName">The SAM Account Name.</param>
        /// <param name="type">The type.</param>
        /// <param name="locations">The locations where the object was found. (If exists).</param>
        /// <returns></returns>
        bool AdObjectExists(string samAccountName, ADObjectType type, out IEnumerable<string> locations);

        /// <summary>
        /// Gets all the available domain Controllers 
        /// </summary>
        /// <returns></returns>
        IEnumerable<DomainController> GetAdDomains();                

    }   
}
