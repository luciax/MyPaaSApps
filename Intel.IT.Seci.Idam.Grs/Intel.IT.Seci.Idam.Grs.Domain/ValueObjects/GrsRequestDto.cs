using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;


namespace Intel.IT.Seci.Idam.Grs.Domain.ValueObjects
{
    /// <summary>
    /// GRS base class.
    /// </summary>
    public abstract class GrsRequestDto
    {
        #region Properties
        
        /// <summary>
        /// The request approver
        /// </summary>
        public string ApproverWwid { get; set; }
        /// <summary>
        /// The request submitter
        /// </summary>
        public string SubmitterWwid { get; set; }
        /// <summary>
        /// The manager of the requester
        /// </summary>
        public string ManagerWwid { get; set; }
        /// <summary>
        /// The requester
        /// </summary>
        public string RequesterWwid { get; set; }
        /// <summary>
        /// The effective date
        /// </summary>
        public DateTime? EffectiveDate { get; set; }      
        /// <summary>
        /// The request identifier
        /// </summary>
        public int RequestId { get; set; }
        /// <summary>
        /// The transaction type
        /// </summary>
        public abstract TransactionType TransactionType { get; }
        /// <summary>
        /// TODO
        /// </summary>
        public RequestStatus RequestStatus  { get;set;}
        /// <summary>
        /// TODO
        /// </summary>
        public string RequestStatusName { get { return this.RequestStatus.ToString() ;} }
        #endregion
    }
}
