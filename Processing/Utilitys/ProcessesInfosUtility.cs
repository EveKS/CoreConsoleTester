using Processing.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Processing.Utilitys
{
    class ProcessesInfosUtility
    {
        private ProcessDetails[] _processDetails;

        private void ProcessesNames()
        {
            IEnumerable<Process> procList = Process.GetProcesses()
                .OrderBy(n => n.ProcessName);

            _processDetails = procList.Select(process =>
            {
                using (process)
                {
                    return new ProcessDetails
                    {
                        ID = process.Id,
                        ProcessName = process.ProcessName,
                        PrivateMemorySize64 = process.PrivateMemorySize64
                    };
                }
            })
            .ToArray();
        }

        public int Length => _processDetails.Length;

        public ProcessDetails this[int index]
        {
            get
            {
                if(_processDetails!= null && index >= 0 
                    && index < _processDetails.Length)
                {
                    return _processDetails[index];
                }

                return null;
            }
        }
    }
}
