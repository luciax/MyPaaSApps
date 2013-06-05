using Intel.IT.Seci.Idam.Grs.Domain.Entities;
using Intel.IT.Seci.Idam.Grs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Infrastructure.Repositories
{
    /// <summary>
    /// Request repository
    /// </summary>
    public class RequestSqlRepository:IRequestRepository
    {
        /// <summary>
        /// Saves the request.
        /// </summary>
        /// <param name="request"></param>
        public void SaveRequest(ref GrsRequest request)
        {
            //throw new NotImplementedException();
        }
        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <param name="requestId">The request Id</param>
        /// <returns></returns>
        public GrsRequest GetRequest(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
