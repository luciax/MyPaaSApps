using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums
{
    /// <summary>
    /// Stub CDIS Employees
    /// </summary>
    public static class StubCdisEmployees
    {
        /// <summary>
        /// Invalid Employee WWID
        /// </summary>
        public const string NonStatus = "00000000";
        /// <summary>
        /// BB and Active Employee WWID
        /// </summary>
        public const string BBActiveRequesterUserWwid = "00000001";
        /// <summary>
        /// BB and Active Manager Employee WWID
        /// </summary>
        public const string BBActiveManagerUserWwid = "00000002";
        /// <summary>
        /// BB and Active Submitter Employee WWID
        /// </summary>
        public const string BBActiveSubmitterUserWwid = "00000003";
        /// <summary>
        /// BB and Active Manager With good signature Employee WWID
        /// </summary>
        public const string BBActiveManagerWithSig = "0000004";
        /// <summary>
        /// BB and Active Second Manager Employee WWID
        /// </summary>
        public const string BBActiveSecondManager = "0000005";
        /// <summary>
        /// BB and Active ApproverUser Employee WWID
        /// </summary>
        public const string BBActiveApproverUserWwid = "00000006";
        /// <summary>
        /// BB and Active Employee WWID
        /// </summary>
        public const string BBInactiveRequesterUserWwid = "00000007";
        /// <summary>
        /// BB and Active Employee WWID
        /// </summary>
        public const string BBInactiveManagerUserWwid = "00000008";
        /// <summary>
        /// BB and Inactive Employee WWID
        /// </summary>
        public const string BBInactiveSubmitterUserWwid = "00000009";
        /// <summary>
        /// BB and Inactive approver Employee WWID
        /// </summary>
        public const string BBInactiveApproverUserWwid = "00000010";
        /// <summary>
        /// GB and Active Employee WWID
        /// </summary>
        public const string GBActiveRequesterUserWwid = "00000011";
        /// <summary>
        /// GB and Active Manager Employee WWID
        /// </summary>
        public const string GBActiveManagerUserWwid = "000000012";
        /// <summary>
        /// GB and Active Submitter WWID
        /// </summary>
        public const string GBActiveSubmitterUserWwid = "00000013";
        /// <summary>
        /// GB and Active Approver WWID
        /// </summary>
        public const string GBActiveApproverUserWwid = "00000014";
        /// <summary>
        /// GB and Inactive Requester WWID
        /// </summary>
        public const string GBInactiveRequesterUserWwid = "00000015";
        /// <summary>
        /// GB and Inactive Manager User WWID
        /// </summary>
        public const string GBInactiveManagerUserWwid = "00000016";
        /// <summary>
        /// GB and Inactive Submitter WWID
        /// </summary>
        public const string GBInactiveSubmitterUserWwid = "00000017";
        /// <summary>
        /// GB and Inactive Approver User WWID
        /// </summary>
        public const string GBInactiveApproverUserWwid = "00000018";
        /// <summary>
        /// BB None Status Employee WWID.
        /// </summary>
        public const string BBNoneStatusWwid = "00000019";
        /// <summary>
        /// GB None Status Employee WWID.
        /// </summary>
        public const string GBNoneStatusWwid = "00000020";
        /// <summary>
        /// GB Start Index.
        /// </summary>
        public const int GBIndex = 11;
        /// <summary>
        /// Start Index for inactive users.
        /// </summary>
        public const int InactiveFirstStartIndex = 7;
        /// <summary>
        /// End Index for inactive users.
        /// </summary>
        public const int InactiveFirstEndIndex = 10;
        /// <summary>
        /// Second Start Index for inactive users.
        /// </summary>
        public const int InactiveSecondStartIndex = 15;
        /// <summary>
        /// Non status index.
        /// </summary>
        public const int NonStatusIndex = 0;
        /// <summary>
        /// Invalid Approver Signature
        /// </summary>
        public const int NonApproverSign = 5000;
        /// <summary>
        /// Valid Approver Signature
        /// </summary>
        public const int ApproverSign = 10000;       
    }
}
