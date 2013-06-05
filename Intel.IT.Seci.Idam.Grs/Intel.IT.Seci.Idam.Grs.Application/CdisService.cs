using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.Cdis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Application
{
    /// <summary>
    /// Cdis Service.
    /// </summary>
    public class CdisService
    {
        #region Private Members
        
        private CdisHelper helper = CdisHelper.Instance;

        private string _CdisConnectionString;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Cdis Connection String
        /// </summary>
        public string CdisConnectionString 
        {
            get
            { 
                return this._CdisConnectionString; 
            }
            set 
            {
                this._CdisConnectionString = value;
                this.helper.CdisConnectionString = this._CdisConnectionString;
            }
        }
        
        #endregion      
        /// <summary>
        /// Get Campuses
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetCampuses()
        {
            return helper.GetCampuses();
        }
        /// <summary>
        /// Get Buildings
        /// </summary>
        /// <param name="campusCode"></param>
        /// <returns></returns>
        public IDictionary<string, string> GetBuildings(string campusCode)
        {
            return helper.GetBuildings(campusCode);
        }
        /// <summary>
        /// Get sites
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetSites()
        {
            return helper.GetSites();
        }

        /// <summary>
        /// Gets a Collection of IntelWorkers, the filter either can be the employee WWID or the employee Name (e.g Castellanos, Luis)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<IntelWorker> FindWorkers(string filter)
        {
            return helper.FindWorkers(filter);
        }
        
        /// <summary>
        /// Gets an IntelWorker object by the Worker Idsid
        /// </summary>
        /// <param name="idsid"></param>
        /// <returns></returns>
        public IntelWorker GetWorkerDataByIdsidOrWwid(string idsid)
        {
            return helper.GetWorkerData(idsid);
        }
    }
}
