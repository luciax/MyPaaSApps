using Intel.IT.Seci.Idam.Grs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.DataAccess
{
    /// <summary>
    /// ComputerRole Interface
    /// </summary>
    public interface IComputerRoleDal
    {
        /// <summary>
        /// Gets all the available computer roles
        /// </summary>
        /// <returns>A collection of ComputerRole Objects</returns>
        IEnumerable<ComputerRole> GetAllComputerRoles();
    }
}
