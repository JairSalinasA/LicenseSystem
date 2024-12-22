using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Common.Constants
{
    public enum LicenseStatus
    {
        Active,
        Expired,
        Suspended,
        Revoked
    }

    public enum ActionType
    {
        Created,
        Updated,
        Deleted,
        Activated,
        Deactivated,
        Expired,
        Renewed,
        Revoked
    }

}
