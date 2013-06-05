using Intel.IT.Seci.Idam.Grs.Application;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;

namespace Intel.IT.Seci.Idam.Grs.MvcWeb.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateComputerController : Controller
    {

        #region Action Methods
        /// <summary>
        /// The Index Action Method, returns the create computer main view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the Json Object with all the available Intel sites from Cdis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCdisSites() 
        { 
            CdisService service = new CdisService();
            service.CdisConnectionString = ConfigurationManager.ConnectionStrings["CDISConnection"].ToString();
            IDictionary<string, string> sitesDictionary = service.GetSites();

            List<object> siteList = new List<object>();            

            foreach (KeyValuePair<string,string> item in sitesDictionary)
            {
                siteList.Add(new { Value = item.Key, Name = item.Value });
            }

            return Json(new { sites = siteList}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the Json Object with all the available Intel campuses from Cdis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCdisCampuses()
        {
            CdisService service = new CdisService();
            service.CdisConnectionString = ConfigurationManager.ConnectionStrings["CDISConnection"].ToString();
            IDictionary<string, string> campusesDictionary = service.GetCampuses();

            List<object> campusList = new List<object>();

            foreach (KeyValuePair<string, string> item in campusesDictionary)
            {
                campusList.Add(new { Value = item.Key, Name = item.Value });
            }

            return Json(new { campuses = campusList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the Json Object with all the available Intel buildings for a given campus from Cdis
        /// </summary>
        /// <param name="campusCode"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCdisBuildings(string campusCode)
        {
            CdisService service = new CdisService();
            service.CdisConnectionString = ConfigurationManager.ConnectionStrings["CDISConnection"].ToString();
            IDictionary<string, string> buildingsDictionary = service.GetBuildings(campusCode);

            List<object> buildingsList = new List<object>();

            foreach (KeyValuePair<string, string> item in buildingsDictionary)
            {
                buildingsList.Add(new { Value = item.Key, Name = item.Value });
            }

            return Json(new { buildings = buildingsList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the Json Object Array for the available computer Roles
        /// </summary>
        /// <returns>Json Object</returns>
        [HttpGet]
        public JsonResult GetComputerRoles()
        {
            CatalogsService catalogsService = new CatalogsService();

            List<object> computerRolesList = catalogsService.GetComputerRoles().ToList();            

            return Json(new { computerRoles = computerRolesList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the Jason Object Array for the available AD Domains
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAdDomains()
        {
            CatalogsService catalogsService = new CatalogsService();

            return Json(new { domains = catalogsService.GetAdDomains() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="computerRequestDto"></param>
        [HttpPost]
        public JsonResult SubmitComputerRequest(CreateComputerRequestDto computerRequestDto)
        {
            try
            {
                RequestService service = new RequestService();
                computerRequestDto = (CreateComputerRequestDto)service.CreateRequest(computerRequestDto);

                return Json(new 
                {
                    computerRequest = computerRequestDto,
                    successMessage = "Your request has been sucessfully submitted."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = ex.Message });
            }
        }
        #endregion
    }
}
