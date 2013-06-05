using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using Intel.IT.Seci.Idam.Grs.Domain.ObjectValues.Comparers;

namespace Intel.IT.Seci.Idam.Grs.Domain.Entities
{
    /// <summary>
    /// Create computer requst.
    /// </summary>
    public class CreateComputerRequest : GrsRequest
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CreateComputerRequest()
        {

        }
        #endregion

        #region Private Variables



        #endregion

        #region Public properties

        #region Overrided
        /// <summary>       
        /// True if there is a need to check if the approver is requester.
        /// </summary>              
        protected override bool CheckApproverIsRequester
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// Transaction type.
        /// </summary>
        public override TransactionType TransactionType
        {
            get
            {
                return TransactionType.CreateComputer;
            }
        }

        #endregion

        /// <summary>
        /// Computer Domain
        /// </summary>
        public string ComputerDomain { get; set; }
        /// <summary>
        /// the Computer Role (Mobile client, Server It standard, etc.)
        /// </summary>
        public ComputerRoleType ComputerRole { get; set; }

        /// <summary>
        /// The Computer Name
        /// </summary>
        public string ComputerName { get; set; }

        #endregion

        #region Public Methods       
       
        #region Overrided
        /// <summary>
        /// Validates the request
        /// </summary>
        /// <returns>The validation result.</returns>
        public override ValidationResult Validate()
        {
            ValidationResult result = new ValidationResult();
            result.Valid = true;
            result.Message = ValidationMessages.ValidRequestMessage;
            //We validate the requester is valid.          
            if (this.Requester.CdisStatus == IntelEmployeeStatus.None || this.Requester.CdisStatus == IntelEmployeeStatus.T)
            {
                result.Valid = false;
                result.Message = ValidationMessages.InvalidRequesterCdisStatusMessage;
            }
            else if (string.IsNullOrEmpty(this.ComputerDomain))
            {
                result.Valid = false;
                result.Message = ValidationMessages.InvalidNullComputerDomainMessage;
            }
            else if (this.ComputerRole == ComputerRoleType.ServerItStandard || this.ComputerRole == ComputerRoleType.ServerNonstandard ||
                    this.ComputerRole == ComputerRoleType.DesktopNetworkCentricBullpen || this.ComputerRole == ComputerRoleType.DesktopNetworkCentricStandardUser)
            {
                IEnumerable<string> locations;
                if (this.AdHelper.AdObjectExists(this.ComputerName, ADObjectType.Computer, out locations))
                {
                    result.Valid = false;
                    result.Message = ValidationMessages.InvalidComputerExistsMessage;
                }
            }
            else
            {
                bool generated = this.GenerateName();
                if(generated)
                {
                    result.Message = ValidationMessages.ValidComputerNameGenerated;
                }                 
            }
            if (this.RequiresApprover())
            {
                if (!this.IsApproverValid)
                {
                    result.Valid = false;
                    result.Message = ValidationMessages.InvalidApprover;
                }
            }
            return result;
        }
        /// <summary>
        /// Validates if requires approver. 
        /// </summary>
        /// <returns>True if requires approver.</returns>
        public override bool RequiresApprover()
        {
            return (this.ComputerRole == ComputerRoleType.NotebookNonstandard ||
                    this.ComputerRole == ComputerRoleType.ServerNonstandard ||
                    this.ComputerRole == ComputerRoleType.DesktopNonstandard);


        }

        #endregion

        #endregion

        private bool GenerateName()
        {
            IEnumerable<string> locations;
            bool generated = false;
            int iterations = 1;
            string computerType = string.Empty;
            if (this.ComputerRole == ComputerRoleType.DesktopNonstandard || this.ComputerRole == ComputerRoleType.DesktopSingleUser)
                computerType = "DESK";
            else
                if (this.ComputerRole == ComputerRoleType.NotebookMobileClient || this.ComputerRole == ComputerRoleType.NotebookNonstandard)
                    computerType = "MOBL";
            this.ComputerName = string.Format("{0}-{1}", this.Requester.Idsid, computerType);
            if (this.AdHelper.AdObjectExists(this.ComputerName, ADObjectType.Computer, out locations))
            {
                this.ComputerName = this.ComputerName + iterations.ToString();
                while (this.AdHelper.AdObjectExists(this.ComputerName, ADObjectType.Computer, out locations))
                {
                    iterations++;
                    this.ComputerName = this.ComputerName.Remove(this.ComputerName.Length - 1, 1);
                    this.ComputerName = this.ComputerName + iterations.ToString();                  
                }
                generated = true;
            }
            return generated;
        }       
    }

      
}
