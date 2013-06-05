using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intel.IT.Seci.Idam.Grs.Domain.Entities;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.AD;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.Cdis;
using Intel.IT.Seci.Idam.Grs.Domain.Factories;
using System.Globalization;



[assembly: CLSCompliant(true)]
namespace Intel.IT.Seci.Idam.Grs.Test
{
    
    /// <summary>
    /// Create computer test class.
    /// </summary>
    [TestClass]
    public class DomainCreateComputerTest
    {

        private enum ApproverType
        {
            ApproverIsSubmitter,
            IsInManagementChain,
            AuthoritySignature,
            ApproverIsInactive
        }
        private RequestFactory factory = new RequestFactory(new StubCdisHelper(), new StubADHelper());
        private CreateComputerRequest request;
        private CreateComputerRequestDto requestDto;
        private string uniqueComputerName = "Computer Name";
        private string nonUniqueDesktopComputerName = StubADHelperData.NonUniqueDesktopComputerName1;
        private string nonUniqueMobileComputerName = StubADHelperData.NonUniqueMobileComputerName1;        
        private string computerDomain = "Computer Domain";
        private void InitializeScenario(ComputerRoleType role, string computerName,   IntelEmployeeStatus requesterStatus = IntelEmployeeStatus.A,  ApproverType approverType = ApproverType.ApproverIsSubmitter)
        {
            this.requestDto = new CreateComputerRequestDto();
            this.requestDto.ComputerRole = role;
            this.requestDto.ComputerName = computerName;
            this.requestDto.ComputerDomain = computerDomain;
            switch (requesterStatus)
            {
                case IntelEmployeeStatus.A:
                case IntelEmployeeStatus.H:
                    this.requestDto.RequesterWwid = StubCdisEmployees.BBActiveRequesterUserWwid;
                    break;
                case IntelEmployeeStatus.T:
                    this.requestDto.RequesterWwid = StubCdisEmployees.BBInactiveRequesterUserWwid;
                    break;
                default:
                    this.requestDto.RequesterWwid = StubCdisEmployees.NonStatus;
                    break;
            }
            this.requestDto.ManagerWwid = StubCdisEmployees.BBActiveManagerUserWwid;
            this.requestDto.SubmitterWwid = StubCdisEmployees.BBActiveSubmitterUserWwid;
            switch (approverType)
            {
                case ApproverType.ApproverIsInactive:
                    this.requestDto.ApproverWwid = StubCdisEmployees.BBInactiveApproverUserWwid;
                    break;
                case ApproverType.ApproverIsSubmitter:
                    this.requestDto.ApproverWwid = this.requestDto.SubmitterWwid;
                    break;
                case ApproverType.AuthoritySignature:
                    this.requestDto.ApproverWwid = StubCdisEmployees.BBActiveManagerWithSig;
                    break;
                case ApproverType.IsInManagementChain:
                    this.requestDto.ApproverWwid = StubCdisEmployees.BBActiveSecondManager;
                    break;
            }
            this.request = (CreateComputerRequest)this.factory.GetRequestFromDto(this.requestDto);
        }

        /// <summary>
        /// Test initialize method
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            
        }
        /// <summary>
        /// Test for valid CDIS status.
        /// </summary>
        [TestMethod]
        public void DomainCreateComputerCdisStatusValidTest()
        {   
            //We test the Active Status
            this.InitializeScenario(ComputerRoleType.DesktopNetworkCentricBullpen, uniqueComputerName,   IntelEmployeeStatus.A);  
            ValidationResult result = this.request.Validate();
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidRequestMessage, true, CultureInfo.CurrentCulture) == 0);
            //We test the Hired Status
            this.InitializeScenario(ComputerRoleType.DesktopNetworkCentricBullpen, uniqueComputerName,   IntelEmployeeStatus.H);  
            result =  this.request.Validate();
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidRequestMessage, true, CultureInfo.CurrentCulture) == 0);
            //We test the Terminated Status
            this.InitializeScenario(ComputerRoleType.DesktopNetworkCentricBullpen, uniqueComputerName,   IntelEmployeeStatus.T);  
            result =  this.request.Validate();            
            Assert.IsFalse(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.InvalidRequesterCdisStatusMessage, true, CultureInfo.CurrentCulture) == 0);            
            //We test the None Status
            this.InitializeScenario(ComputerRoleType.DesktopNetworkCentricBullpen, uniqueComputerName,   IntelEmployeeStatus.None);  
            result =  this.request.Validate();            
            Assert.IsFalse(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.InvalidRequesterCdisStatusMessage, true, CultureInfo.CurrentCulture) == 0);            
        }
        /// <summary>
        /// Test for null domain.
        /// </summary>
        [TestMethod]
        public void DomainCreateComputerDomainNullTest()
        {
            //We test when the computer domain is null
            this.InitializeScenario(ComputerRoleType.DesktopNetworkCentricBullpen, uniqueComputerName,   IntelEmployeeStatus.A);
            ValidationResult result = request.Validate();
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidRequestMessage, true, CultureInfo.CurrentCulture) == 0);                   
            this.request.ComputerDomain = string.Empty;
            result = request.Validate();
            Assert.IsFalse(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.InvalidNullComputerDomainMessage, true, CultureInfo.CurrentCulture) == 0);                   
        }
        /// <summary>
        /// Test for valid computer name.
        /// </summary>
        [TestMethod]
        public void DomainCreateComputerValidNameTest()
        {         
            //We test for the types evaluate the uniquiness when computer does not exist.
            this.InitializeScenario(ComputerRoleType.ServerItStandard, this.uniqueComputerName, IntelEmployeeStatus.A);
            ValidationResult result = request.Validate();
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidRequestMessage, true, CultureInfo.CurrentCulture) == 0);
            this.request.ComputerRole = ComputerRoleType.ServerNonstandard;
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidRequestMessage, true, CultureInfo.CurrentCulture) == 0);
            this.request.ComputerRole = ComputerRoleType.DesktopNetworkCentricStandardUser;
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidRequestMessage, true, CultureInfo.CurrentCulture) == 0);
            this.request.ComputerRole = ComputerRoleType.DesktopNetworkCentricBullpen;
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidRequestMessage, true, CultureInfo.CurrentCulture) == 0);
            //We test for the types evaluate the uniquiness when computer exists.
            this.request.ComputerName = StubADHelperData.NonUniqueServerName;
            this.request.ComputerRole = ComputerRoleType.ServerItStandard;
            result = request.Validate();
            Assert.IsFalse(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.InvalidComputerExistsMessage, true, CultureInfo.CurrentCulture) == 0);
            this.request.ComputerRole = ComputerRoleType.ServerNonstandard;
            Assert.IsFalse(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.InvalidComputerExistsMessage, true, CultureInfo.CurrentCulture) == 0);
            this.request.ComputerRole = ComputerRoleType.DesktopNetworkCentricStandardUser;
            Assert.IsFalse(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.InvalidComputerExistsMessage, true, CultureInfo.CurrentCulture) == 0);
            this.request.ComputerRole = ComputerRoleType.DesktopNetworkCentricBullpen;
            Assert.IsFalse(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.InvalidComputerExistsMessage, true, CultureInfo.CurrentCulture) == 0);
        }
        /// <summary>
        /// Test for computer name generation.
        /// </summary>
        [TestMethod]
        public void DomainCreateComputerGenerateNameTest()
        {
            //We test for the types we auto-generate the uniquiness.
            this.InitializeScenario(ComputerRoleType.DesktopNonstandard, nonUniqueDesktopComputerName,   IntelEmployeeStatus.A);
            ValidationResult result = request.Validate();
            Assert.IsTrue(string.Compare(request.ComputerName, StubADHelperData.GeneratedDesktopComputerName1, true, CultureInfo.CurrentCulture) == 0);
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidComputerNameGenerated, true, CultureInfo.CurrentCulture) == 0);
            this.request.ComputerRole = ComputerRoleType.DesktopSingleUser;
            this.request.ComputerName = nonUniqueDesktopComputerName;
            request.Validate();
            Assert.IsTrue(string.Compare(request.ComputerName, StubADHelperData.GeneratedDesktopComputerName1, true, CultureInfo.CurrentCulture) == 0);
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidComputerNameGenerated, true, CultureInfo.CurrentCulture) == 0);
            this.request.ComputerRole = ComputerRoleType.NotebookMobileClient;
            this.request.ComputerName = nonUniqueMobileComputerName;
            request.Validate();
            Assert.IsTrue(string.Compare(request.ComputerName, StubADHelperData.GeneratedMobileComputerName1, true, CultureInfo.CurrentCulture) == 0);
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidComputerNameGenerated, true, CultureInfo.CurrentCulture) == 0);
            this.request.ComputerRole = ComputerRoleType.NotebookNonstandard;
            this.request.ComputerName = nonUniqueMobileComputerName;
            request.Validate();
            Assert.IsTrue(string.Compare(request.ComputerName, StubADHelperData.GeneratedMobileComputerName1, true, CultureInfo.CurrentCulture) == 0);
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidComputerNameGenerated, true, CultureInfo.CurrentCulture) == 0);
        }
        /// <summary>
        /// Test if create computer request requires approver.
        /// </summary>
        [TestMethod]
        public void DomainCreateComputerRequiresApprover()
        {
            //We test the types that do not require approver.
            this.InitializeScenario(ComputerRoleType.DesktopNetworkCentricBullpen, uniqueComputerName,   IntelEmployeeStatus.A);
            bool requiresApprover = request.RequiresApprover();
            Assert.IsFalse(requiresApprover);
            this.InitializeScenario(ComputerRoleType.DesktopNetworkCentricStandardUser, uniqueComputerName,   IntelEmployeeStatus.A);
            requiresApprover = request.RequiresApprover();
            Assert.IsFalse(requiresApprover);
            this.InitializeScenario(ComputerRoleType.DesktopSingleUser, uniqueComputerName,   IntelEmployeeStatus.A);
            requiresApprover = request.RequiresApprover();
            Assert.IsFalse(requiresApprover);
            this.InitializeScenario(ComputerRoleType.NotebookMobileClient, uniqueComputerName,   IntelEmployeeStatus.A);
            requiresApprover = request.RequiresApprover();
            Assert.IsFalse(requiresApprover);
            this.InitializeScenario(ComputerRoleType.ServerItStandard, uniqueComputerName,   IntelEmployeeStatus.A);
            requiresApprover = request.RequiresApprover();
            Assert.IsFalse(requiresApprover);
            //We test the types that require approver.
            this.InitializeScenario(ComputerRoleType.ServerNonstandard, uniqueComputerName,   IntelEmployeeStatus.A);
            requiresApprover = request.RequiresApprover();
            Assert.IsTrue(requiresApprover);
            this.InitializeScenario(ComputerRoleType.NotebookNonstandard, uniqueComputerName,   IntelEmployeeStatus.A);
            requiresApprover = request.RequiresApprover();
            Assert.IsTrue(requiresApprover);
            this.InitializeScenario(ComputerRoleType.DesktopNonstandard, uniqueComputerName,   IntelEmployeeStatus.A);
            requiresApprover = request.RequiresApprover();
            Assert.IsTrue(requiresApprover);
        }
        /// <summary>
        /// Test for valid approver in create computer request.
        /// </summary>
        [TestMethod]     
        public void DomainCreateComputeValidApproverTest()
        {
            //We test the combinations for approver
            this.InitializeScenario(ComputerRoleType.DesktopNonstandard, uniqueComputerName,   IntelEmployeeStatus.A, ApproverType.ApproverIsInactive);
            ValidationResult result =  this.request.Validate();
            Assert.IsFalse(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.InvalidApprover, true, CultureInfo.CurrentCulture) == 0);
            this.InitializeScenario(ComputerRoleType.DesktopNonstandard, uniqueComputerName,   IntelEmployeeStatus.A, ApproverType.ApproverIsSubmitter);
            result = this.request.Validate();
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidComputerNameGenerated, true, CultureInfo.CurrentCulture) == 0);
            this.InitializeScenario(ComputerRoleType.DesktopNonstandard, uniqueComputerName,   IntelEmployeeStatus.A, ApproverType.AuthoritySignature);
            result = this.request.Validate();
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidComputerNameGenerated, true, CultureInfo.CurrentCulture) == 0);
            this.InitializeScenario(ComputerRoleType.DesktopNonstandard, uniqueComputerName,   IntelEmployeeStatus.A, ApproverType.IsInManagementChain);
            result = this.request.Validate();
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.Compare(result.Message, ValidationMessages.ValidComputerNameGenerated, true, CultureInfo.CurrentCulture) == 0);        
        }
        /// <summary>
        /// Test the getter for TransactionType property
        /// </summary>
        [TestMethod]
        public void DomainCreateGetTransactionType()
        {
            this.InitializeScenario(ComputerRoleType.DesktopNonstandard, uniqueComputerName,   IntelEmployeeStatus.A);
            Assert.IsTrue(this.request.TransactionType == TransactionType.CreateComputer);
        }
    }
}
