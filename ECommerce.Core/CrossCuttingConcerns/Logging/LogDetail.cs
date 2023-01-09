using System;
using System.Collections.Generic;

namespace ECommerce.Core.CrossCuttingConcerns.Logging
{
    [Serializable]
    public class LogDetail
    {
        public string MethodName { get; set; }
        public List<LogParameter> LogParameters { get; set; }
    }
}
