using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Domain.Dal.Cdis;

namespace Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.Cdis
{
    /// <summary>
    /// CDIS helper.
    /// </summary>
    public class CdisHelper : ICdisHelper
    {

        #region Constructors
        /// <summary>
        /// Private constructor for Singleton implementation.
        /// </summary>
        private CdisHelper()
        {

        }

        #endregion

        #region Public Properties
        /// <summary>
        /// CDIS connection string.
        /// </summary>
        public string CdisConnectionString
        {
            set
            {
                this._CdisConnectionString = value;
            }
        }

        /// <summary>
        /// CdisHelper instance for Singleton pattern.
        /// </summary>
        public static CdisHelper Instance
        {
            get
            {
                return _Instance;
            }
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Get Signature Authority by WWID.
        /// </summary>
        /// <param name="wwid"></param>
        /// <returns></returns>
        public int GetSigAuthorityByWWID(string wwid)
        {
            int sigAuthority = 0;
            using (SqlConnection cdisConnection = new SqlConnection(this._CdisConnectionString))
            using (SqlCommand selectCommand = new SqlCommand())
            {
                selectCommand.CommandText = @"SELECT auth_amount FROM x500.[dbo].[SigAuthorityPublic] WHERE WWID = @employeeWwid AND authority_type = 'standard'";
                selectCommand.Parameters.Add("@employeeWwid", SqlDbType.VarChar).Value = wwid;
                cdisConnection.Open();
                selectCommand.Connection = cdisConnection;

                object result = selectCommand.ExecuteScalar();
                if (result != null)
                    sigAuthority = (int)result;               
            }
            return sigAuthority;

        }
        /// <summary>
        /// Get Management Chain.
        /// </summary>
        /// <param name="wwid"></param>
        /// <returns></returns>
        public IEnumerable<string> GetManagementChain(string wwid)
        {
            List<string> managementChain = new List<string>();
            string managerWwid = wwid;
            while (!string.IsNullOrEmpty(managerWwid))
            {
                using (SqlConnection cdisConnection = new SqlConnection(this._CdisConnectionString))
                using (SqlCommand selectCommand = new SqlCommand())
                {
                    selectCommand.CommandText = @"SELECT MgrWWID FROM x500.[dbo].[WorkerPublicExtended] WHERE WWID = @employeeWwid";
                    selectCommand.Parameters.Add("@employeeWwid", SqlDbType.VarChar).Value = managerWwid;
                    cdisConnection.Open();
                    selectCommand.Connection = cdisConnection;

                    managerWwid = (string)selectCommand.ExecuteScalar();

                    if (!string.IsNullOrEmpty(managerWwid) && !managementChain.Contains(managerWwid))
                        managementChain.Add(managerWwid);
                    else
                        managerWwid = null;
                }
            }
            return managementChain;
        }
        /// <summary>
        /// Gets the data of a worker.
        /// </summary>
        /// <param name="wwidOrIdsid">The WWID.</param>
        /// <returns>An <see cref="IntelWorker"/> object</returns>
        public IntelWorker GetWorkerData(string wwidOrIdsid)
        {
            IntelWorker worker = new IntelWorker();
            using (SqlConnection cdisConnection = new SqlConnection(this._CdisConnectionString))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            using (DataTable employeeDataTable = new DataTable())
            {
                employeeDataTable.Locale = System.Globalization.CultureInfo.CurrentCulture;
                StringBuilder query = new StringBuilder();
                query.Append(@"SELECT CASE WHEN ccMailName IS NULL THEN LongID ELSE ccMailName END as FullName");
                query.Append(@", FirstName, LastName, MiddleInitial, BadgeType, ShortID, Department");
                query.Append(@", ccMailPO as Domain, DomainAddress as Email, upperIDSID as Idsid");
                query.Append(@", PhoneNum, SiteCode, WWID, MgrWWID");
                query.Append(@", StatCode");
                query.Append(@" FROM x500.[dbo].[WorkerPublicExtended]");
                query.Append(@" WHERE WWID = @employeeWwidOrIdsid");
                query.Append(@" OR UPPER(upperIDSID) = UPPER(@employeeWwidOrIdsid)");

                dataAdapter.SelectCommand = new SqlCommand();

                dataAdapter.SelectCommand.CommandText = query.ToString();
                dataAdapter.SelectCommand.Parameters.Add("@employeeWwidOrIdsid", System.Data.SqlDbType.VarChar).Value = wwidOrIdsid;

                cdisConnection.Open();

                dataAdapter.SelectCommand.Connection = cdisConnection;

                dataAdapter.Fill(employeeDataTable);

                if (employeeDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in employeeDataTable.Rows)
                    {
                        IntelEmployeeStatus status = IntelEmployeeStatus.None;
                        Enum.TryParse<IntelEmployeeStatus>((string)row["StatCode"], true, out status);
                        worker = new IntelWorker
                        {
                            BadgeType = (string)row["BadgeType"],
                            CdisShortId = (string)row["ShortId"],
                            DepartmentNumber = (string)row["Department"],
                            Domain = (string)row["Domain"],
                            Email = (string)row["Email"],
                            FirstName = (string)row["FirstName"],
                            FullName = (string)row["FullName"],
                            Idsid = (string)row["Idsid"],
                            LastName = (string)row["LastName"],
                            ManagerWwid = (string)row["MgrWWID"],
                            MiddleInitial = (string)row["MiddleInitial"],
                            PhoneNumber = (string)row["PhoneNum"],
                            Site = (string)row["SiteCode"],
                            Wwid = (string)row["WWID"],
                            CdisStatus = status
                        };
                    }
                }

            }
            return worker;
        }

        /// <summary>
        /// Gets the Dictionary of the available campuses from CDIS. Where the  key is the campus code and the vaule is the campus name
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetCampuses()
        {
            Dictionary<string, string> campuses = new Dictionary<string, string>();
            using (SqlConnection cdisConnection = new SqlConnection(this._CdisConnectionString))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            using (DataTable campusesDataTable = new DataTable())
            {
                campusesDataTable.Locale = System.Globalization.CultureInfo.CurrentCulture;
                StringBuilder query = new StringBuilder();
                query.Append(@"SELECT CampusCode, CampusDescr");
                query.Append(@" FROM x500.[dbo].[CampusPublicExt]");
                query.Append(@" ORDER BY CampusCode");

                dataAdapter.SelectCommand = new SqlCommand();

                dataAdapter.SelectCommand.CommandText = query.ToString();

                cdisConnection.Open();

                dataAdapter.SelectCommand.Connection = cdisConnection;

                dataAdapter.Fill(campusesDataTable);

                if (campusesDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in campusesDataTable.Rows)
                    {
                        campuses.Add((string)row["CampusCode"], (string)row["CampusDescr"]);
                    }
                }

            }
            return campuses;
        }

        /// <summary>
        /// Gets the Dictionary of the available buildings for an specific campus code
        /// </summary>
        /// <param name="campusCode">The campus code</param>
        /// <returns>A dictionary with the buildings</returns>
        public IDictionary<string, string> GetBuildings(string campusCode)
        {
            Dictionary<string, string> buildings = new Dictionary<string, string>();
            using (SqlConnection cdisConnection = new SqlConnection(this._CdisConnectionString))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            using (DataTable buildingsDataTable = new DataTable())
            {
                buildingsDataTable.Locale = System.Globalization.CultureInfo.CurrentCulture;
                StringBuilder query = new StringBuilder();
                query.Append(@"SELECT BldgCode, BldgDescr");
                query.Append(@" FROM x500.[dbo].[BuildingPublicExt]");
                query.Append(@" WHERE CampusCode = @CampusCode");

                dataAdapter.SelectCommand = new SqlCommand();

                dataAdapter.SelectCommand.CommandText = query.ToString();
                dataAdapter.SelectCommand.Parameters.Add("@CampusCode", SqlDbType.VarChar).Value = campusCode;
                cdisConnection.Open();

                dataAdapter.SelectCommand.Connection = cdisConnection;

                dataAdapter.Fill(buildingsDataTable);

                if (buildingsDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in buildingsDataTable.Rows)
                    {
                        buildings.Add((string)row["BldgCode"], (string)row["BldgDescr"]);
                    }
                }

            }
            return buildings;
        }

        /// <summary>
        /// Gets the Dictornary of the available Sites on CDIS DB Where the  key is the Site code and site name the value
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetSites()
        {
            Dictionary<string, string> sites = new Dictionary<string, string>();
            using (SqlConnection cdisConnection = new SqlConnection(this._CdisConnectionString))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            using (DataTable sitesDataTable = new DataTable())
            {
                sitesDataTable.Locale = System.Globalization.CultureInfo.CurrentCulture;
                StringBuilder query = new StringBuilder();
                query.Append(@"SELECT DISTINCT SiteCode, SiteName");
                query.Append(@" FROM x500.[dbo].[SitePublicExt]");
                query.Append(@" ORDER BY SiteName");

                dataAdapter.SelectCommand = new SqlCommand();

                dataAdapter.SelectCommand.CommandText = query.ToString();
                cdisConnection.Open();

                dataAdapter.SelectCommand.Connection = cdisConnection;

                dataAdapter.Fill(sitesDataTable);

                if (sitesDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in sitesDataTable.Rows)
                    {
                        sites.Add((string)row["SiteCode"], (string)row["SiteName"]);
                    }
                }

            }
            return sites;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<IntelWorker> FindWorkers(string filter)
        {
            List<IntelWorker> workers = new List<IntelWorker>();

            using (SqlConnection cdisConnection = new SqlConnection(this._CdisConnectionString))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            using (DataTable employeeDataTable = new DataTable())
            {
                employeeDataTable.Locale = System.Globalization.CultureInfo.CurrentCulture;
                StringBuilder query = new StringBuilder();
                query.Append(@"SELECT TOP 30 CASE WHEN ccMailName IS NULL THEN LongID ELSE ccMailName END as FullName");
                query.Append(@", FirstName, LastName, MiddleInitial, BadgeType, ShortID, Department");
                query.Append(@", ccMailPO as Domain, DomainAddress as Email, upperIDSID as Idsid");
                query.Append(@", PhoneNum, SiteCode, WWID, MgrWWID");
                query.Append(@", StatCode");
                query.Append(@" FROM x500.[dbo].[WorkerPublicExtended]");
                query.Append(@" WHERE (WWID = @filter");
                query.Append(@" OR UPPER(LongID) like UPPER(@filter + '%')");
                query.Append(@" OR UPPER([ccMailName]) like UPPER(@filter + '%'))");
                query.Append(@" AND StatCode = 'A'");

                dataAdapter.SelectCommand = new SqlCommand();

                dataAdapter.SelectCommand.CommandText = query.ToString();

                dataAdapter.SelectCommand.Parameters.Add("@filter", SqlDbType.VarChar).Value = filter;

                cdisConnection.Open();

                dataAdapter.SelectCommand.Connection = cdisConnection;

                dataAdapter.Fill(employeeDataTable);

                if (employeeDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in employeeDataTable.Rows)
                    {
                        IntelWorker worker = new IntelWorker();
                        IntelEmployeeStatus status = IntelEmployeeStatus.None;
                        Enum.TryParse<IntelEmployeeStatus>((string)row["StatCode"], true, out status);
                        worker = new IntelWorker
                        {
                            BadgeType = (string)row["BadgeType"],
                            CdisShortId = (string)row["ShortId"],
                            DepartmentNumber = (string)row["Department"],
                            //Domain = (string)row["Domain"],
                            //Email = (string)row["Email"],
                            FirstName = (string)row["FirstName"],
                            FullName = (string)row["FullName"],
                            Idsid = (string)row["Idsid"],
                            LastName = (string)row["LastName"],
                            ManagerWwid = (string)row["MgrWWID"],
                            MiddleInitial = (string)row["MiddleInitial"],
                            //PhoneNumber = (string)row["PhoneNum"],
                            Site = (string)row["SiteCode"],
                            Wwid = (string)row["WWID"],
                            CdisStatus = status
                        };
                        if (row["Domain"] != DBNull.Value)
                            worker.Domain = (string)row["Domain"];
                        if (row["Email"] != DBNull.Value)
                            worker.Email = (string)row["Email"];
                        if (row["PhoneNum"] != DBNull.Value)
                            worker.PhoneNumber = (string)row["PhoneNum"];
                        workers.Add(worker);
                    }
                }

            }
            return workers;
        }
        #endregion

        #region Private variables

        private static CdisHelper _Instance = new CdisHelper();
        private string _CdisConnectionString;
        #endregion

    }
}
