using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums
{
    /// <summary>
    /// Validation Messages.
    /// </summary>
    public class ValidationMessages
    {
        /// <summary>
        /// Invalid message for invalid requester in CDIS.
        /// </summary>
        public static readonly string InvalidRequesterCdisStatusMessage = "The requester CDIS status is not valid. The CDIS status should be Active (A) or Hired (H).";
        /// <summary>
        /// Invalid messsage for null domain.
        /// </summary>
        public static readonly string InvalidNullComputerDomainMessage= "Please provide a domain where the computer will be created.";
        /// <summary>
        /// Invalid message for computer exists.
        /// </summary>
        public static readonly string InvalidComputerExistsMessage = "The computer name already exits. Please select a unique name";
        /// <summary>
        /// Valid message for rquest.
        /// </summary>
        public static readonly string ValidRequestMessage = "The request is valid.";
        /// <summary>
        /// Valid message for request when a new name was generated.
        /// </summary>
        public static readonly string ValidComputerNameGenerated = "The original computer name was not unique, the computer name was auto-generated.";
        /// <summary>
        /// Invalid approver message.
        /// </summary>
        public static readonly string InvalidApprover = "Invalid Approver, please select an active approver that is in the Requested For's management chain or has 5K signature authority.";
        /// <summary>
        /// Invalid create transition
        /// </summary>
        public static readonly string InvalidCreateTransition = "The 'Create' transition can only be called if the request status is 'New'.";
        /// <summary>
        /// Invalid approve transition
        /// </summary>        
        public static readonly string InvalidApproveTransition = "The 'Approve/Auto-Approve' transition can only be called if the request status is 'Waiting For Approval'.";
        /// <summary>
        /// Invalid cancel transition
        /// </summary>        
        public static readonly string InvalidCancelTransition = "The 'Cancel' transition can only be called if the request status is 'Waiting For Approval', 'Waiting For Effective Date' or 'Manual Provision'.";
        /// <summary>
        /// Invalid send to AP transition
        /// </summary>        
        public static readonly string InvalidSendToAPTransition = "The 'Send to automatic provision' transition can only be called if the request status is 'Wating For Effective Date'.";
        /// <summary>
        /// Invalid send to MP transition
        /// </summary>
        public static readonly string InvalidSendToMPTransition = "The 'Send to manual provision' transition can only be called if the request status is 'Wating For Effective Date' or 'Manual Provision'.";
        /// <summary>
        /// Invalid provission transition
        /// </summary>
        public static readonly string InvalidProvisionTransition = "The 'Provision' transition can only be called if the request status is 'Manual Provision' or 'Automatic Provision'.";
        /// <summary>
        /// Invalid timeout transition
        /// </summary>
        public static readonly string InvalidTimeoutTransition = "The 'Timeout' transition can only be called if the request status is 'Waiting For Approval' or 'Manual Provision'.";


    }
}
