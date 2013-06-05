using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intel.IT.Seci.Idam.Grs.Domain.Entities;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.AD;
using Intel.IT.Seci.Idam.Grs.Infrastructure.Dal.Cdis;
using Intel.IT.Seci.Idam.Grs.Domain.Factories;
using System.Globalization;

namespace Intel.IT.Seci.Idam.Grs.Test
{
  
    /// <summary>
    /// Test the request transitions
    /// </summary>
    [TestClass]
    public class DomainRequestTransitionTest
    {
        private RequestFactory factory = new RequestFactory(new StubCdisHelper(), new StubADHelper());
        private CreateComputerRequest request;
        private CreateComputerRequestDto requestDto;
        private string uniqueComputerName = "Computer Name";
        private string computerDomain = "Computer Domain";
        /// <summary>
        /// Test initialize method
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this.requestDto = new CreateComputerRequestDto();
            this.requestDto.ComputerRole = ComputerRoleType.ServerItStandard;
            this.requestDto.ComputerName = uniqueComputerName;
            this.requestDto.ComputerDomain = computerDomain;
            this.requestDto.RequesterWwid = StubCdisEmployees.BBActiveRequesterUserWwid;
            this.requestDto.ManagerWwid = StubCdisEmployees.BBActiveManagerUserWwid;
            this.requestDto.SubmitterWwid = StubCdisEmployees.BBActiveSubmitterUserWwid;
            this.requestDto.ApproverWwid = StubCdisEmployees.BBInactiveApproverUserWwid;
            this.request = (CreateComputerRequest)this.factory.GetRequestFromDto(this.requestDto);  
        }
        /// <summary>
        /// Test for Create transition
        /// </summary>
        [TestMethod]
        public void DomainRequestCreateTransitionTest()
        {
            //We test the allowed status
            Action transition = request.Create;
            this.request.Status = RequestStatus.New;
            transition();            
            Assert.AreEqual(RequestStatus.WaitingForApproval, request.Status);
            //We test the errors.
            string errorMessage = ValidationMessages.InvalidCreateTransition;
            CheckExceptionRaisedForansition(RequestStatus.AutomationProvisionning, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Cancelled, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Completed, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.ManualProvisionning, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.TimedOut, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.WaitingForApproval, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.WaitingForEffectiveDate, errorMessage, transition);
        }
        /// <summary>
        /// Test for Approve and AutoApprove transitions
        /// </summary>
        [TestMethod]
        public void DomainRequestApproveAndAutoApproveTransitionTest()
        {
            //We test the allowed status for Approve
            Action transition = request.Approve;      
            this.request.Status = RequestStatus.WaitingForApproval;
            transition();
            Assert.AreEqual(RequestStatus.WaitingForEffectiveDate, request.Status);
            //We test the errors  for Approve
            string errorMessage = ValidationMessages.InvalidApproveTransition;
            CheckExceptionRaisedForansition(RequestStatus.AutomationProvisionning, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Cancelled, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Completed, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.ManualProvisionning, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.TimedOut, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.New, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.WaitingForEffectiveDate, errorMessage, transition);
            //We test the allowed status for AutoApprove
            this.request.Status = RequestStatus.WaitingForApproval;         
            transition = request.AutoApprove;
            transition();
            //We test the errors   for AutoApprove
            CheckExceptionRaisedForansition(RequestStatus.AutomationProvisionning, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Cancelled, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Completed, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.ManualProvisionning, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.TimedOut, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.New, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.WaitingForEffectiveDate, errorMessage, transition);
        }
        /// <summary>
        /// Test for Send to AP transition
        /// </summary>
        [TestMethod]
        public void DomainRequestSendToApTransitionTest()
        {
            //We test the allowed status
            Action transition = request.SendToAutomaticProvision;
            this.request.Status = RequestStatus.WaitingForEffectiveDate;
            transition();
            Assert.AreEqual(RequestStatus.AutomationProvisionning, request.Status);
            //We test the errors  
            string errorMessage = ValidationMessages.InvalidSendToAPTransition;
            CheckExceptionRaisedForansition(RequestStatus.AutomationProvisionning, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Cancelled, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Completed, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.ManualProvisionning, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.TimedOut, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.New, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.WaitingForApproval, errorMessage, transition);           
        }
        /// <summary>
        /// Test for Send to MP transition
        /// </summary>
        [TestMethod]
        public void DomainRequestSendToMpTransitionTest()
        {   
            //We test the allowed status
            Action transition = request.SendToManualProvision;
            this.request.Status = RequestStatus.WaitingForEffectiveDate;
            transition();
            Assert.AreEqual(RequestStatus.ManualProvisionning, request.Status);
            this.request.Status = RequestStatus.AutomationProvisionning;
            transition();
            Assert.AreEqual(RequestStatus.ManualProvisionning, request.Status);
            //We test the errors  
            string errorMessage = ValidationMessages.InvalidSendToMPTransition;
            CheckExceptionRaisedForansition(RequestStatus.New, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.WaitingForApproval, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Completed, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Cancelled, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.TimedOut, errorMessage, transition);                        
        }

        /// <summary>
        /// Test for Provision transition
        /// </summary>
        [TestMethod]
        public void DomainRequestProvisionTransitionTest()
        {
            //We test the allowed status
            Action transition = request.Provision;
            this.request.Status = RequestStatus.ManualProvisionning;
            transition();
            Assert.AreEqual(RequestStatus.Completed, request.Status);
            this.request.Status = RequestStatus.AutomationProvisionning;
            transition();
            Assert.AreEqual(RequestStatus.Completed, request.Status);           
            //We test the errors                   
            request.ComputerDomain = string.Empty;
            this.request.Status = RequestStatus.ManualProvisionning;
            try
            { 
                //We test when the request is not valid
                transition();
            }
            catch(InvalidOperationException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The request is not valid."));
            }
            string errorMessage = ValidationMessages.InvalidProvisionTransition;
            CheckExceptionRaisedForansition(RequestStatus.New, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.WaitingForApproval, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.WaitingForEffectiveDate, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Completed, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Cancelled, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.TimedOut, errorMessage, transition);
        }

        /// <summary>
        /// Test for Cancel transition
        /// </summary>
        [TestMethod]
        public void DomainRequestCancelTransitionTest()
        {
            //We test the allowed status
            Action transition = request.Cancel;
            this.request.Status = RequestStatus.WaitingForApproval;
            transition();
            Assert.AreEqual(RequestStatus.Cancelled, request.Status);
            this.request.Status = RequestStatus.WaitingForEffectiveDate;
            transition();
            Assert.AreEqual(RequestStatus.Cancelled, request.Status);
            this.request.Status = RequestStatus.ManualProvisionning;
            transition();
            Assert.AreEqual(RequestStatus.Cancelled, request.Status);                   
            //We test the errors
            string errorMessage = ValidationMessages.InvalidCancelTransition;
            CheckExceptionRaisedForansition(RequestStatus.New, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.AutomationProvisionning, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Completed, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Cancelled, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.TimedOut, errorMessage, transition);
        }
        /// <summary>
        /// Test for Timeout transition
        /// </summary>
        [TestMethod]
        public void DomainRequestTimeoutTransitionTest()
        {
            //We test the allowed status
            Action transition = request.Timeout;
            this.request.Status = RequestStatus.WaitingForApproval;
            transition();
            Assert.AreEqual(RequestStatus.TimedOut, request.Status);
            this.request.Status = RequestStatus.ManualProvisionning;
            transition();
            Assert.AreEqual(RequestStatus.TimedOut, request.Status);            
            //We test the errors
            string errorMessage = ValidationMessages.InvalidTimeoutTransition;
            CheckExceptionRaisedForansition(RequestStatus.New, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.AutomationProvisionning, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.WaitingForEffectiveDate, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Completed, errorMessage, transition);
            CheckExceptionRaisedForansition(RequestStatus.Cancelled, errorMessage, transition);            
            CheckExceptionRaisedForansition(RequestStatus.TimedOut, errorMessage, transition);
        }  
        private void CheckExceptionRaisedForansition(RequestStatus status, string expectedMessage, Action transaction)
        {
            try
            {
                this.request.Status = status;
                transaction();
                Assert.Fail();
            }
            catch (InvalidOperationException exception)
            {
                Assert.IsTrue(string.Compare(exception.Message, expectedMessage, true, CultureInfo.CurrentCulture) == 0);
            }
        }
    }
}
