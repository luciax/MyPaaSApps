using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using Intel.IT.Seci.Idam.Grs.Domain.Dal.AD;
using System.Globalization;
using Intel.IT.Seci.Idam.Grs.Domain.ObjectValues.Comparers;
using Intel.IT.Seci.Idam.Grs.Domain.Dal.Cdis;

[assembly: CLSCompliant(true)]
namespace Intel.IT.Seci.Idam.Grs.Domain.Entities
{
    /// <summary>
    /// GRS base class.
    /// </summary>
    public abstract class GrsRequest
    {
        private static readonly int minimumSigAuthority = 5000;

        #region Properties
        /// <summary>
        /// Request status
        /// </summary>
        public RequestStatus Status { get; set; }
        /// <summary>
        /// The request approver
        /// </summary>
        public IntelWorker Approver { get; set; }
        /// <summary>
        /// The request submitter
        /// </summary>
        public IntelWorker Submitter { get; set; }
        /// <summary>
        /// The manager of the requester
        /// </summary>
        public IntelWorker Manager { get; set; }
        /// <summary>
        /// The requester
        /// </summary>
        public IntelWorker Requester { get; set; }
        /// <summary>
        /// The effective date
        /// </summary>
        public DateTime? EffectiveDate { get; set; }      
        /// <summary>
        /// The request identifier
        /// </summary>
        public int RequestId { get; set; }        
        /// <summary>
        /// Ad helper
        /// </summary>
        public IAdHelper AdHelper { get;set; }

        /// <summary>
        /// CDIS helper
        /// </summary>
        public ICdisHelper CdisHelper { get; set; }

        /// <summary>
        /// Determines if the approver is valid by checking the following conditions:
        ///   The approver is active
        ///   The approver is the submitter or
        ///   the approver is the requester (optional) or
        ///   the approver is in the requester's management chain or
        ///   the approver has 5K signature authority
        ///   </summary>
        protected bool IsApproverValid
        {
            get
            {
                bool approverIsRequester;
                bool validCdisStatus;
                bool isApproverSubmitter;
                bool isApproverInManagementChain;
                bool validSigAuthority;
                approverIsRequester = (this.CheckApproverIsRequester && string.Compare(this.Approver.Wwid, this.Requester.Wwid, true, CultureInfo.CurrentCulture) == 0);
                validCdisStatus = (this.Approver.CdisStatus == IntelEmployeeStatus.A || this.Approver.CdisStatus == IntelEmployeeStatus.H);
                isApproverSubmitter = (new IntelWorkerComparer().Equals(this.Approver, this.Submitter));
                isApproverInManagementChain = (this.CdisHelper.GetManagementChain(this.Requester.Wwid).Contains(this.Approver.Wwid));
                validSigAuthority = (this.CdisHelper.GetSigAuthorityByWWID(this.Approver.Wwid) > minimumSigAuthority); //TODO REMOVE MAGIC NUMBER
                return (validCdisStatus) && (isApproverSubmitter || approverIsRequester || isApproverInManagementChain || validSigAuthority);
            }
        }
        
        #region Abstract
        /// <summary>
        /// The transaction type
        /// </summary>
        public abstract TransactionType TransactionType { get; }

        /// <summary>
        /// True if there is a need to check if the approver is requester.
        /// </summary>
        protected abstract bool CheckApproverIsRequester
        {
            get;
        }

        #endregion     
        #endregion

        #region Methods
        /// <summary>
        /// Validates
        /// </summary>
        /// <returns>Return a validation result</returns>
        public abstract ValidationResult Validate();        
        
        #region Abstract 
        /// <summary>
        /// True if requires approver.
        /// </summary>
        /// <returns></returns>        
        public abstract bool RequiresApprover();
        #endregion

        #region Transitions
        /// <summary>
        /// Create transition
        /// </summary>
        public void Create()
        {
            if (this.Status != RequestStatus.New)
                throw new InvalidOperationException(ValidationMessages.InvalidCreateTransition);
            ValidationResult result = new ValidationResult();
            result = this.Validate();
            if (result.Valid)
            {
                this.Status = RequestStatus.WaitingForApproval;
            }
            else
                throw new InvalidOperationException("The request is not valid. " + result.Message);
            
        }
        /// <summary>
        /// Approve transition
        /// </summary>        
        public void Approve()
        {
            if (this.Status != RequestStatus.WaitingForApproval)
                throw new InvalidOperationException(ValidationMessages.InvalidApproveTransition);
            this.Status = RequestStatus.WaitingForEffectiveDate;
        }
        /// <summary>
        /// AutoApprove transition
        /// </summary>        
        public void AutoApprove()
        {
            if (this.Status != RequestStatus.WaitingForApproval)
                throw new InvalidOperationException(ValidationMessages.InvalidApproveTransition);
            this.Status = RequestStatus.WaitingForEffectiveDate;
        }
        /// <summary>
        /// Cancel transition
        /// </summary>        
        public void Cancel()
        {
            if (this.Status == RequestStatus.New ||
                this.Status == RequestStatus.AutomationProvisionning || 
                this.Status == RequestStatus.Completed ||
                this.Status == RequestStatus.TimedOut ||
                this.Status == RequestStatus.Cancelled
                )                
                throw new InvalidOperationException(ValidationMessages.InvalidCancelTransition);
            this.Status = RequestStatus.Cancelled;
        }
        /// <summary>
        /// Timeout transition
        /// </summary> 
        public void Timeout()
        {
            if (this.Status == RequestStatus.New ||
                this.Status == RequestStatus.AutomationProvisionning ||
                this.Status == RequestStatus.Completed ||
                this.Status == RequestStatus.TimedOut ||
                this.Status == RequestStatus.WaitingForEffectiveDate ||
                this.Status == RequestStatus.Cancelled
                )   
                throw new InvalidOperationException(ValidationMessages.InvalidTimeoutTransition);
            this.Status = RequestStatus.TimedOut;
        }
        /// <summary>
        /// SendToManualProvision transition
        /// </summary> 
        public void SendToManualProvision()
        {
            if (this.Status == RequestStatus.AutomationProvisionning ||
                 this.Status == RequestStatus.WaitingForEffectiveDate)
                this.Status = RequestStatus.ManualProvisionning;
            else
                throw new InvalidOperationException(ValidationMessages.InvalidSendToMPTransition);
        }
        /// <summary>
        /// SendToAutomaticProvision transition
        /// </summary> 
        public void SendToAutomaticProvision()
        {
            if (this.Status == RequestStatus.WaitingForEffectiveDate)
                this.Status = RequestStatus.AutomationProvisionning;
            else
                throw new InvalidOperationException(ValidationMessages.InvalidSendToAPTransition);
        }
        /// <summary>
        /// Provision transition
        /// </summary> 
        public void Provision()
        {

            if (this.Status == RequestStatus.ManualProvisionning || this.Status == RequestStatus.AutomationProvisionning)
            {
                ValidationResult result = new ValidationResult();
                result = this.Validate();
                if (result.Valid)
                {
                    //this.AdHelper.CreateComputer();
                    this.Status = RequestStatus.Completed;
                }
                else
                    throw new InvalidOperationException("The request is not valid. " + result.Message);
            }
            else
            {
                throw new InvalidOperationException(ValidationMessages.InvalidProvisionTransition);
            }
        }

        #endregion


        #endregion
    }
}
