using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Models
{
    class ProcessInfo
    {
        public int ID { get; set; }
        public string ProcessName { get; set; }
        public string ProcessCPU { get; set; }
        public string ProcessRAM { get; set; }
        public string ProcessPage { get; set; }
        public IList<ProcessNic> ProcessNics { get; set; }
    }

    class ProcessNic
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
