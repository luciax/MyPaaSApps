using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Domain.Dal.AD;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.AD
{
    /// <summary>
    /// AdHelper Stub.
    /// </summary>
    public class StubADHelper:IAdHelper
    {
        /// <summary>
        /// True if the object exists.
        /// </summary>
        /// <param name="samAccountName">The SAM Account Name.</param>
        /// <param name="type">The type.</param>
        /// <param name="locations">The locations where the object was found. (If exists).</param>
        /// <returns></returns>
        public bool AdObjectExists(string samAccountName, ADObjectType type, out IEnumerable<string> locations)
        {
            locations = new Collection<string>();
            switch (type)
            {
                case ADObjectType.Computer:
                    return (string.Compare(samAccountName, StubADHelperData.NonUniqueDesktopComputerName1, true, CultureInfo.CurrentCulture) == 0)
                        || (string.Compare(samAccountName, StubADHelperData.NonUniqueOtherDesktopComputerName1, true, CultureInfo.CurrentCulture) == 0)
                        || (string.Compare(samAccountName, StubADHelperData.NonUniqueDesktopComputerName2, true, CultureInfo.CurrentCulture) == 0)
                        || (string.Compare(samAccountName, StubADHelperData.NonUniqueOtherDesktopComputerName2, true, CultureInfo.CurrentCulture) == 0)
                        || (string.Compare(samAccountName, StubADHelperData.NonUniqueMobileComputerName1, true, CultureInfo.CurrentCulture) == 0)
                        || (string.Compare(samAccountName, StubADHelperData.NonUniqueOtherMobileComputerName1, true, CultureInfo.CurrentCulture) == 0)
                        || (string.Compare(samAccountName, StubADHelperData.NonUniqueMobileComputerName2, true, CultureInfo.CurrentCulture) == 0)
                        || (string.Compare(samAccountName, StubADHelperData.NonUniqueOtherMobileComputerName2, true, CultureInfo.CurrentCulture) == 0)                        
                        || (string.Compare(samAccountName, StubADHelperData.NonUniqueServerName, true, CultureInfo.CurrentCulture) == 0);                    

            }
            return false;
        }

        /// <summary>
        /// Gets all teh available domains from AD
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DomainController> GetAdDomains()
        {
            DomainController[] domainControllers = new DomainController[]
            {
                new DomainController 
                { 
                    FullName="isedev.addev.intel.com", 
                    FriendlyName = "ISEDEV", 
                    ParentFriendlyName = string.Empty,
                    ChildDomains = new DomainController[] 
                    {
                        new DomainController { FriendlyName = "ISEDEVAMR", FullName="isedevamr.isedev.addev.intel.com", ParentFriendlyName="ISEDEV", ChildDomains = null, ParentFullName = "isedev.addev.intel.com"},
                        new DomainController { FriendlyName = "ISEDEVGER", FullName="isedevger.isedev.addev.intel.com", ParentFriendlyName="ISEDEV", ChildDomains = null, ParentFullName = "isedev.addev.intel.com"}
                    }
                },
                new DomainController 
                { 
                    FullName="corp.intel.com", 
                    FriendlyName = "CORP", 
                    ParentFriendlyName = string.Empty,
                    ChildDomains = new DomainController[] 
                    {
                        new DomainController { FriendlyName = "AMR", FullName="amr.corp.intel.com", ParentFriendlyName="CORP", ChildDomains = null, ParentFullName = "corp.intel.com"},
                        new DomainController { FriendlyName = "GAR", FullName="gar.corp.intel.com", ParentFriendlyName="CORP", ChildDomains = null, ParentFullName = "corp.intel.com"},
                        new DomainController { FriendlyName = "GER", FullName="ger.corp.intel.com", ParentFriendlyName="CORP", ChildDomains = null, ParentFullName = "corp.intel.com"},
                        new DomainController { FriendlyName = "CCR", FullName="ccr.corp.intel.com", ParentFriendlyName="CORP", ChildDomains = null, ParentFullName = "corp.intel.com"}
                    }
                }
            };

            return domainControllers;
        }
    }
}
