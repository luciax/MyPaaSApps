using Intel.IT.Seci.Idam.Grs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.DataAccess
{
    /// <summary>
    /// The computer role data access class
    /// </summary>
    public class ComputerRoleDal : IComputerRoleDal
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
