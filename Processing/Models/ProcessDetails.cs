using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Models
{
    public class ProcessDetails
    {
        public int ID { get; set; }
        public string ProcessName { get; set; }
        public long PrivateMemorySize64 { get; set; }
    }
}
