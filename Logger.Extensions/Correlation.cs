using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Extensions
{
    public class Correlation
    {
        public string CorrelationId { get; set; }
        public string CorrelationCallerName { get; set;}
        public string CorrelationCallerMethod { get; set;}
    }
}
