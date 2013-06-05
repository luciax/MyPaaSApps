using Intel.IT.Seci.Idam.Grs.Domain.Entities;
using Intel.IT.Seci.Idam.Grs.Domain.Factories;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.AD;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.Cdis;
using Intel.IT.Seci.Idam.Grs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Repositories;

namespace Intel.IT.Seci.Idam.Grs.Application
{
    /// <summary>
    /// The Request service
    /// </summary>
    public class RequestService
    {
        /// <summary>
        /// Request repository
        /// </summary>
        public IRequestRepository Repository { get; set; }
        /// <summary>
        /// Creates a RequestService instance.
        /// </summary>
        public RequestService()
        {
            this.Repository = new RequestSqlRepository();
        }
        /// <summary>
        /// Creates the request
        /// </summary>
        /// <param name="requestDto"></param>
        public GrsRequestDto CreateRequest(GrsRequestDto requestDto)
        {
            RequestFactory factory = new RequestFactory(CdisHelper.Instance, new StubADHelper());            
            GrsRequest request = factory.GetRequestFromDto(requestDto);             
            request.Create();
            this.Repository.SaveRequest(ref request);
            requestDto = factory.GetRequestDto(request);
            requestDto.RequestId = new Random().Next(1, 100);
            return requestDto;            
        }
    }
}
