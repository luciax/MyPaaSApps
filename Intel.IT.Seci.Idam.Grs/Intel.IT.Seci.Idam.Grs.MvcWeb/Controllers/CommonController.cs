using Intel.IT.Seci.Idam.Grs.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intel.IT.Seci.Idam.Grs.MvcWeb.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonController : Controller
    {
        /// <summary>
        /// Gets a Json Object array with all the found employees
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FindWorkers(string filter)
        {
            CdisService service = new CdisService();

            return Json(new { foundWorkers = service.FindWorkers(filter) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the worker data by Wwid or Idsid
        /// </summary>
        /// <param name="idsidOrWwid">The employee WWID or Idsid</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWorkerByIdsidOrWwid(string idsidOrWwid)
        {
            CdisService service = new CdisService();

            return Json(new { worker = service.GetWorkerDataByIdsidOrWwid(idsidOrWwid) }, JsonRequestBehavior.AllowGet);
        }
    }
}
