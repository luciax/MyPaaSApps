using Intel.IT.Seci.Idam.Grs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.EF
{
    /// <summary>
    /// Entity Framework Catalogs Data Access
    /// </summary>
    public class EfCatalogsDal : IEfCatalogsDal
    {
        /// <summary>
        /// Gets all the computer roles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ComputerRole> GetAllComputerRoles()
        {
            using (GRSEntities entities = new GRSEntities())
            {
                return entities.ComputerRoles.Select(computerRoles => computerRoles).ToList();
            }
        }
    }
}
