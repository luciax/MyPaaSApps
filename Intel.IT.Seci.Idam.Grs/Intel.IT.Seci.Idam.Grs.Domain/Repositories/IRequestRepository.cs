using Intel.IT.Seci.Idam.Grs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.Repositories
{
    /// <summary>
    /// The IRequestRepository interface
    /// </summary>
    public interface IRequestRepository
    {
        /// <summary>
        /// Saves the request.
        /// </summary>
        /// <param name="request">The request</param>
        void SaveRequest(ref GrsRequest request);
        /// <summary>
        /// Gets a request.
        /// </summary>
        /// <param name="requestId">The request Id.</param>
        /// <returns></returns>
        GrsRequest GetRequest(int requestId);
    }
}
