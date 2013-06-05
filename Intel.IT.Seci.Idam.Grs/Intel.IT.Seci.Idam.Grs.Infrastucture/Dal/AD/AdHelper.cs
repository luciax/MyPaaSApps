using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Domain.Dal.AD;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.AD
{
    /// <summary>
    /// AD helper class.
    /// </summary>
    public class AdHelper : IAdHelper
    {
        /// <summary>
        /// True if the object exists.
        /// </summary>
        /// <param name="samAccountName">The SAM Account Name.</param>
        /// <param name="type">The type.</param>
        /// <param name="locations">The locations where the object was found. (If exists).</param>
        /// <returns></returns>      
        public bool AdObjectExists(string samAccountName,  ADObjectType type, out IEnumerable<string> locations)
        {
            locations = new Collection<string>();
            /*
            string[] locationsArray;
            ADWSClient client = new ADWSClient();
            bool exists = client.ExistsADObjectInGC(samAccountName, type, out locationsArray);
            if (locationsArray != null)
            {
                locations = new Collection<string>(locationsArray);
            }
            return exists;*/
            return true;
        }

        /// <summary>
        /// Gets all the available DomainControllers where a user can Create a new computer object
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DomainController> GetAdDomains()
        {
            return new List<DomainController>();
        }
    }
}
