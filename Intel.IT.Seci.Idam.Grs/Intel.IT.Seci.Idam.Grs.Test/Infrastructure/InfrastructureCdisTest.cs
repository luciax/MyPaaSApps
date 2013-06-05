using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.Cdis;
using Intel.IT.Seci.Idam.Grs.Domain.ObjectValues.Comparers;
using System.Configuration;
using System.Data.SqlClient;
namespace Intel.IT.Seci.Idam.Grs.Test
{
    /// <summary>
    /// CDIs test class.
    /// </summary>
    [TestClass]
    public class InfrastructureCdisTest
    {
        private IntelWorker worker;
        private CdisHelper cdisHelper;
        private IntelWorkerComparer comparer;
        private string _CdisConnectionString;
        /// <summary>
        /// Test initialize
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            worker = new IntelWorker
            {
                BadgeType = "BB",
                CdisShortId = "lcastell",
                DepartmentNumber = "36973",
                Domain = "amr",
                Email = "luis.daniel.castellanos@intel.com",
                FirstName = "LUIS",
                FullName = "CASTELLANOS BARBA, LUIS D (DANI)",
                Idsid = "LCASTELL",
                LastName = "CASTELLANOS BARBA",
                ManagerWwid = "11412953",
                MiddleInitial = "D",
                PhoneNumber = "52 33 22824099x4099",
                Site = "GM",
                Wwid = "11380344"
            };
            this.cdisHelper = CdisHelper.Instance;
            this.cdisHelper.CdisConnectionString = ConfigurationManager.ConnectionStrings["CDISConnection"].ConnectionString;
            this.comparer = new IntelWorkerComparer();
            this._CdisConnectionString = ConfigurationManager.ConnectionStrings["CDISConnection"].ConnectionString;
        }
        
        /// <summary>
        /// Tests the GetWorkerData Method from the CdisHelper class
        /// </summary>
        [TestMethod]
        [TestCategory("Infrastructure")]
        public void InfrastructureCdisGetWorkerDataTest()
        {            
            IntelWorker workerFromCdis = this.cdisHelper.GetWorkerData(worker.Wwid);
            Assert.IsTrue(this.comparer.Equals(worker,workerFromCdis));
        }
        
        /// <summary>
        /// Tests the GetCampuses Method from the CdisHelper class. The test assumes  that there's always at least one Campus on CDIS and its campus code is ABC
        /// </summary>
        [TestMethod]
        [TestCategory("Infrastructure")]
        public void InfrastructureCdisGetCampusesTest()
        {
            KeyValuePair<string, string> campus = new KeyValuePair<string,string>("ABC ", "Airport Business Ctr, Munich ");
            IDictionary<string, string> returnedCampuses = this.cdisHelper.GetCampuses();

            Assert.IsTrue(returnedCampuses.Count > 0);

            Assert.IsTrue(returnedCampuses.ContainsKey(campus.Key));
        }

        /// <summary>
        /// Tests the GetBuildings Method from the CdisHelper class. 
        /// The test assumes that there's always at least one building code by the given campus code and its code is ABC1
        /// </summary>
        [TestMethod]
        [TestCategory("Infrastructure")]
        public void InfrastructureCdisGetBuildingsTest()
        {
            string campusCode = "ABC ";
            string buildingCode = "ABC1 ";
            IDictionary<string, string> returnedBuildings = this.cdisHelper.GetBuildings(campusCode);

            Assert.IsTrue(returnedBuildings.Count > 0);
            Assert.IsTrue(returnedBuildings.ContainsKey(buildingCode));

        }

         /// <summary>
        /// Tests the GetSites Method from the CdisHelper class. 
        /// The test assumes that there's always at least one Site its code is AR
        /// </summary>
        [TestMethod]
        [TestCategory("Infrastructure")]
        public void InfrastructureCdisGetSitesTest()
        {
            string siteCode = "AR";

            IDictionary<string, string> returnedSites = this.cdisHelper.GetSites();

            Assert.IsTrue(returnedSites.Count > 0);

            Assert.IsTrue(returnedSites.ContainsKey(siteCode));

        }

        /// <summary>
        /// Tests tje GetManagementChain Method 
        /// </summary>
        [TestMethod]
        [TestCategory("Infrastructure")]
        public void InfrastructureCdisGetManagementChainTest()
        {
            string fakeWwid = "00000000";

            System.Collections.Generic.List<string> returnedManagementChain = (List<string>)this.cdisHelper.GetManagementChain(fakeWwid);

            Assert.IsTrue(returnedManagementChain.Count == 0);

            returnedManagementChain = (List<string>)this.cdisHelper.GetManagementChain(worker.Wwid);

            Assert.IsTrue(returnedManagementChain.Count > 0);
        }

        /// <summary>
        /// Tests the GetSigAuthorityByWWID method from the CdisHelper class. The test will always take the top 1 wwid if the SigAuthorityPublic CDIS view to take the result
        /// </summary>
        [TestMethod]
        [TestCategory("Infrastructure")]
        public void InfrastructureCdisGetSigAuthorityByWWID()
        {
            string wwid = GetTop1WwidSigAuthority();
            int sigAuthority = this.cdisHelper.GetSigAuthorityByWWID(wwid);

            Assert.IsTrue(sigAuthority > 0);
        }

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetTop1WwidSigAuthority()
        {
            string wwid = string.Empty;

            using (SqlConnection cdisConnection = new SqlConnection(this._CdisConnectionString))
            using (SqlCommand selectCommand = new SqlCommand())
            {
                selectCommand.CommandText = @"SELECT TOP 1 WWID FROM x500.[dbo].[SigAuthorityPublic]";                
                cdisConnection.Open();
                selectCommand.Connection = cdisConnection;

                wwid = (string)selectCommand.ExecuteScalar();
            }
            return wwid;
        }
        #endregion
    }


}
