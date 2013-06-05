using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.Dal.Cdis
{
    /// <summary>
    /// CDIS helper interface
    /// </summary>
    public interface ICdisHelper
    {
        /// <summary>
        /// Cdis connection string.
        /// </summary>
        string CdisConnectionString { set;  }
        /// <summary>
        /// Gets the data of a worker.
        /// </summary>
        /// <param name="wwid">The WWID.</param>
        /// <returns>An <see cref="IntelWorker"/> object</returns>
        IntelWorker GetWorkerData(string wwid);

        /// <summary>
        /// Gets the Dictionary of the available campuses from CDIS. Where the  key is the campus code and the vaule is the campus name
        /// </summary>
        /// <returns>A dictionary with the campuses</returns>
        IDictionary<string, string> GetCampuses();

        /// <summary>
        /// Gets the Dictionary of the available buildings for an specific campus code
        /// </summary>
        /// <param name="campusCode">The campus code</param>
        /// <returns>A dictionary with the buildings</returns>
        IDictionary<string, string> GetBuildings(string campusCode);

        /// <summary>
        /// Gets the Dictornary of the available Sites on CDIS DB Where the  key is the Site code and site name the value
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> GetSites();
        /// <summary>
        /// Gets the Sig Authority by WWID.
        /// </summary>
        /// <param name="wwid"></param>
        /// <returns></returns>
        int GetSigAuthorityByWWID(string wwid);
        /// <summary>
        /// Gets the management chain for the given WWID.
        /// </summary>
        /// <param name="wwid">The wwid.</param>
        /// <returns></returns>
        IEnumerable<string> GetManagementChain(string wwid);

        /// <summary>
        /// Gets a list of IntelWorkers
        /// </summary>
        /// <param name="filter">The filter can be either the WWID (e.g. 11380344 ) or the Worker name (e.g Castellanos, Luis Daniel) </param>
        /// <returns></returns>
        IEnumerable<IntelWorker> FindWorkers(string filter);
    }
}
