using Intel.IT.Seci.Idam.Grs.Domain.ConstantsEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.ValueObjects
{
    /// <summary>
    /// Intel worker class
    /// </summary>
    public class IntelWorker
    {
        /// <summary>
        /// Full name
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Middle Initial
        /// </summary>
        public string MiddleInitial { get; set; }
        /// <summary>
        /// Badge type
        /// </summary>
        public string BadgeType { get; set; }
        /// <summary>
        /// CDIS short identifier
        /// </summary>
        public string CdisShortId { get; set; }
        /// <summary>
        /// Department number
        /// </summary>
        public string DepartmentNumber { get; set; }
        /// <summary>
        /// Full name
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Full name
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Full name
        /// </summary>
        public string Idsid { get; set; }
        /// <summary>
        /// Full name
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Full name
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// Full name
        /// </summary>
        public string Wwid { get; set; }
        /// <summary>
        /// Manager wwid.
        /// </summary>
        public string ManagerWwid { get; set; }
        /// <summary>
        /// The employee status
        /// </summary>
        public IntelEmployeeStatus CdisStatus { get; set; }
    }
}
