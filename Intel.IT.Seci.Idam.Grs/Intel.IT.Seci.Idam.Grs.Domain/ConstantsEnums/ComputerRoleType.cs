using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums
{
    /// <summary>
    /// Computer Roles enumeration
    /// </summary>
    public enum ComputerRoleType
    {   
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Notebook - Mobile Client
        /// </summary>       
        NotebookMobileClient,

        /// <summary>
        /// Notebook - NonStandard
        /// </summary>        
        NotebookNonstandard,

        /// <summary>
        /// Desktop - Single User
        /// </summary>       
        DesktopSingleUser,

        /// <summary>
        /// Desktop - Network Centric Standard User
        /// </summary>      
        DesktopNetworkCentricStandardUser,

        /// <summary>
        /// Desktop - NonStandard
        /// </summary>     
        DesktopNonstandard,

        /// <summary>
        /// Desktop - Network Centric Bullpen
        /// </summary>        
        DesktopNetworkCentricBullpen,

        /// <summary>
        /// Server - IT Standard
        /// </summary>        
        ServerItStandard,

        /// <summary>
        /// Server - NonStandard
        /// </summary>       
        ServerNonstandard
        
    }
}
