using System;

namespace ECommerce.Core.CrossCuttingConcerns.Logging
{
    [Serializable]
    public class LogParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string Type { get; set; }
    }
}
