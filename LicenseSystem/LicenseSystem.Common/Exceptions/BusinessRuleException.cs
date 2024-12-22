using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Common.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public string RuleCode { get; }

        public BusinessRuleException(string message, string ruleCode) : base(message)
        {
            RuleCode = ruleCode;
        }
    }
}
