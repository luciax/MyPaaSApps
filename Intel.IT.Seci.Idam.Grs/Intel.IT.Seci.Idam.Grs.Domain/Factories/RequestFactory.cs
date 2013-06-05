using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Domain.Dal.AD;
using Intel.IT.Seci.Idam.Grs.Domain.Dal.Cdis;
using Intel.IT.Seci.Idam.Grs.Domain.Entities;
using Intel.IT.Seci.Idam.Grs.Domain.Factories;
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
    public class RequestFactory:IRequestFactory
    {
        /// <summary>
        /// Creates a request factory.
        /// </summary>
        /// <param name="cdisHelper">CDIS helper</param>
        /// <param name="adHelper">AD helper</param>
        public RequestFactory(ICdisHelper cdisHelper, IAdHelper adHelper)
        {
            this.CdisHelper = cdisHelper;
            this.AdHelper = adHelper;
        }
        /// <summary>
        /// AD Helper
        /// </summary>
        public IAdHelper AdHelper { get; set; }
        /// <summary>
        /// Cdis Helper
        /// </summary>
        public ICdisHelper CdisHelper { get; set; }

        /// <summary>
        /// Gets a request by Id.
        /// </summary>
        /// <param name="requestId">The request Id</param>
        /// <returns></returns>
        public GrsRequest GetRequest(int requestId)
        {
            throw new InvalidOperationException();
        }
        /// <summary>
        /// Gets a new request.
        /// </summary>
        /// <param name="requestDto">The request data transfer object</param>
        /// <returns></returns>      
        public GrsRequest GetRequestFromDto(GrsRequestDto requestDto)
        {
            GrsRequest request = null;
            switch (requestDto.TransactionType)
            {
                case TransactionType.CreateComputer:
                    CreateComputerRequest createComputerRequest = new CreateComputerRequest();
                    CreateComputerRequestDto createComputerRequestdto = (CreateComputerRequestDto)requestDto;
                    createComputerRequest.ComputerRole = createComputerRequestdto.ComputerRole;
                    createComputerRequest.ComputerDomain = createComputerRequestdto.ComputerDomain;
                    createComputerRequest.ComputerName = createComputerRequestdto.ComputerName; 
                    request = createComputerRequest;
                break;
            }
            FillWorkersData(ref request, requestDto);
            request.RequestId = requestDto.RequestId;
            request.EffectiveDate = requestDto.EffectiveDate;
            request.AdHelper = this.AdHelper;
            request.CdisHelper = this.CdisHelper;
            request.Status = requestDto.RequestStatus;
            return request;            
        }
        /// <summary>
        /// Gets a DTO from a Request object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GrsRequestDto GetRequestDto(GrsRequest request)
        {
            GrsRequestDto requestDto = null;
            switch (request.TransactionType)
            {
                case TransactionType.CreateComputer:
                    CreateComputerRequest createComputerRequest = (CreateComputerRequest)request;
                    CreateComputerRequestDto createComputerRequestDto = new CreateComputerRequestDto();
                    createComputerRequestDto.ComputerRole =   createComputerRequest.ComputerRole;
                    createComputerRequestDto.ComputerDomain = createComputerRequest.ComputerDomain;
                    createComputerRequestDto.ComputerName = createComputerRequest.ComputerName ;
                   
                    requestDto = (GrsRequestDto)(createComputerRequestDto);
                    break;
            }
            requestDto.RequestStatus = request.Status;
            requestDto.ApproverWwid = request.Approver.Wwid;
            requestDto.RequesterWwid = request.Requester.Wwid;
            requestDto.ManagerWwid = request.Manager.Wwid;
            requestDto.SubmitterWwid = request.Submitter.Wwid;
            requestDto.RequestId = request.RequestId;
            requestDto.EffectiveDate = request.EffectiveDate;
            return requestDto;            
        }
        private void FillWorkersData(ref GrsRequest request, GrsRequestDto requestDto)
        {
            request.Approver = this.CdisHelper.GetWorkerData(requestDto.ApproverWwid);
            request.Manager = this.CdisHelper.GetWorkerData(requestDto.ManagerWwid);
            request.Requester = this.CdisHelper.GetWorkerData(requestDto.RequesterWwid);
            request.Submitter = this.CdisHelper.GetWorkerData(requestDto.SubmitterWwid);            
        }
    }
}
