using Intel.IT.Seci.Idam.Grs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.IT.Seci.Idam.Grs.Domain.ObjectValues.Comparers
{
    /// <summary>
    /// Intel worker comparer.
    /// </summary>
    public class IntelWorkerComparer:IEqualityComparer<IntelWorker>
    {
        /// <summary>
        /// The equals operator.        
        /// </summary>
        public bool Equals(IntelWorker x, IntelWorker y)
        {
            if (x == null)
            {
                if (y == null) return true;
                else return false;
            }
            if (y == null) return false;
            if (x.Wwid == y.Wwid) return true;
            else return false;
        }

        /// <summary>
        /// The hash code.
        /// </summary>
        public int GetHashCode(IntelWorker obj)
        {
            if (obj == null)
                return 0;
            int hashCode = obj.LastName.Length ^ obj.FirstName.Length;
            return hashCode.GetHashCode();
        }
    }
}
