using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Repositories;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.EF;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.AD;
using Intel.IT.Seci.Idam.Grs.Entities;

namespace Intel.IT.Seci.Idam.Grs.Application
{
    /// <summary>
    /// The Entity Framework Catalogs Service
    /// </summary>
    public class CatalogsService
    {
        private IEfCatalogsDal catalogsDal;

        private StubADHelper adHelper;
        /// <summary>
        /// Constructor
        /// </summary>
        public CatalogsService()
        {
            this.catalogsDal = new EfCatalogsDal();
            this.adHelper = new StubADHelper();
        }

        /// <summary>
        /// Gets a List of anonimus objects with all the available computer roles except the default (NONE) 
        /// The anonimus objects have two properties ComputerRoleId and RoleDescription; these properties are mapped to a View Model "class" in the 
        /// CreateComputerViewModels.js File 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetComputerRoles()
        {
            IEnumerable<ComputerRole> computerRoles = this.catalogsDal.GetAllComputerRoles().Where(role => role.RoleDescription != "None").ToArray();            
            List<object> computerRolesList = new List<object>();

            foreach (ComputerRole item in computerRoles)
            {
                computerRolesList.Add(new { ComputerRoleId = item.ComputerRoleId, RoleDescription = item.RoleDescription });
            }


            return computerRolesList;
        }

        /// <summary>
        /// Returns all the available AD Domain Controllers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetAdDomains()
        {
            return this.adHelper.GetAdDomains();
        }
    }
}
