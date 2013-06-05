using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using Intel.IT.Seci.Idam.Grs.Domain.ObjectValues.Comparers;

namespace Intel.IT.Seci.Idam.Grs.Domain.ValueObjects
{
    /// <summary>
    /// reate computer request Data Transfer Object.
    /// </summary>
    public class CreateComputerRequestDto : GrsRequestDto
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CreateComputerRequestDto ()
        {
            this.EffectiveDate = DateTime.Now;
        }
        #region Public properties

        #region Overrided
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
    }
}