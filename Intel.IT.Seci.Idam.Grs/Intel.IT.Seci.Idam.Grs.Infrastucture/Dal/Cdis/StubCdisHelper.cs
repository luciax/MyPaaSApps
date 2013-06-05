using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.ObjectModel;
using Intel.IT.Seci.Idam.Grs.Domain.Dal.Cdis;

namespace Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.Cdis
{
    /// <summary>
    /// CDIS helper stub
    /// </summary>
    public class StubCdisHelper:ICdisHelper
    {
        private string _ConnectionString;
        /// <summary>
        /// Cdis connection string.
        /// </summary>
        public string CdisConnectionString 
        { 
            set
            {
                this._ConnectionString = value;
            }
        }
        /// <summary>
        /// Gets the data of a worker.
        /// </summary>
        /// <param name="wwid">The WWID.</param>
        /// <returns>An <see cref="IntelWorker"/> object</returns>
        public IntelWorker GetWorkerData(string wwid)
        {
            IntelWorker worker = new IntelWorker();
            worker.DepartmentNumber = "D01";
            worker.Domain = "AMR";          
            int wwidNumber = 1;
            int.TryParse(wwid, out wwidNumber);
            if (wwidNumber > StubCdisEmployees.GBIndex)  
                worker.BadgeType = "GB";
            else
                worker.BadgeType = "BB";
            if ((wwidNumber >= StubCdisEmployees.InactiveFirstStartIndex && wwidNumber <= StubCdisEmployees.InactiveFirstEndIndex) || (wwidNumber >= StubCdisEmployees.InactiveSecondStartIndex))  
                worker.CdisStatus = IntelEmployeeStatus.T;
            else if(wwidNumber == StubCdisEmployees.NonStatusIndex)
                worker.CdisStatus = IntelEmployeeStatus.None;
            else
                worker.CdisStatus = IntelEmployeeStatus.A;
            worker.CdisShortId = "idsid" + wwidNumber.ToString();
            worker.Email = "email" + wwidNumber.ToString() + "@intel.com";
            worker.FirstName = "Name " + wwidNumber.ToString();
            worker.FullName = "Name " + wwidNumber.ToString() +" Last Name";
            worker.Idsid = "idsid" + wwidNumber.ToString();
            worker.LastName = "Last Name ";
            worker.ManagerWwid = "000000000";
            worker.MiddleInitial = "A";
            worker.PhoneNumber = "000000000" + wwidNumber.ToString();
            worker.Site = "SOTE";
            worker.Wwid = wwid;
            return worker;
        }

        /// <summary>
        /// Gets the Dictionary of the available campuses from CDIS. Where the  key is the campus code and the vaule is the campus name
        /// </summary>
        /// <returns>A dictionary with the campuses</returns>
        public IDictionary<string, string> GetCampuses()
        {
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the Dictionary of the available buildings for an specific campus code
        /// </summary>
        /// <param name="campusCode">The campus code</param>
        /// <returns>A dictionary with the buildings</returns>
        public IDictionary<string, string> GetBuildings(string campusCode)
        {
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the Dictornary of the available Sites on CDIS DB Where the  key is the Site code and site name the value
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetSites()
        {
            return new Dictionary<string, string>();
        }
        /// <summary>
        /// Gets the signature Authority by WWID.
        /// </summary>
        /// <param name="wwid">The WWID</param>
        /// <returns></returns>
        public int GetSigAuthorityByWWID(string wwid)
        {
            if (string.Compare(wwid, StubCdisEmployees.BBActiveManagerWithSig, true, CultureInfo.CurrentCulture) == 0)
            {
                return StubCdisEmployees.ApproverSign;
            }
            else
                return StubCdisEmployees.NonApproverSign;
        }
        /// <summary>
        /// Gets the management chain for the given WWID.
        /// </summary>
        /// <param name="wwid">The WWID.</param>
        /// <returns></returns>
        public IEnumerable<string> GetManagementChain(string wwid)
        {
            Collection<string> managementChain = new Collection<string>();
            if (string.Compare(wwid, StubCdisEmployees.BBActiveRequesterUserWwid, true, CultureInfo.CurrentCulture) == 0)
            {
                managementChain.Add(StubCdisEmployees.BBActiveManagerUserWwid);
                managementChain.Add(StubCdisEmployees.BBActiveSecondManager);
            }
            return managementChain;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<IntelWorker> FindWorkers(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
