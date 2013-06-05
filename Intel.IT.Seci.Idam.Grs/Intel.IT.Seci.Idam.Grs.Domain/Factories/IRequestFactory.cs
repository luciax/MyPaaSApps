using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Domain.Dal.AD;
using Intel.IT.Seci.Idam.Grs.Domain.Dal.Cdis;
using Intel.IT.Seci.Idam.Grs.Domain.Entities;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.Factories
{
    /// <summary>
    /// RequestFactory interface
    /// </summary>
    public interface IRequestFactory
    {
        /// <summary>
        /// AD Helper
        /// </summary>
        IAdHelper AdHelper { get; set; }
        /// <summary>
        /// Cdis Helper
        /// </summary>
        ICdisHelper CdisHelper { get; set; }
        /// <summary>
        /// Gets a request by Id.
        /// </summary>
        /// <param name="requestId">The request Id</param>
        /// <returns></returns>
        GrsRequest GetRequest(int requestId);

        /// <summary>
        /// Gets a new request.
        /// </summary>
        /// <param name="requestDto">The request data transfer object</param>
        /// <returns></returns>
        GrsRequest GetRequestFromDto(GrsRequestDto requestDto);     
    }
}
