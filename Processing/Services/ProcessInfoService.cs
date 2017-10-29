using Processing.Managers;
using Processing.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Services
{
    class ProcessesInfoService : IDisposable
    {
        private string machineName = Environment.MachineName;
        private string instanceName;
        private bool total;

        private ProcessInfo _processInfo;

        private ProcessInfoManager _infoManager = new ProcessInfoManager();

        private PerformanceCounter _performanceCPU;
        private PerformanceCounter _performanceRAM;
        private PerformanceCounter _performancePage;
        private PerformanceCounter[] _performancesNic;

        public ProcessInfo ProcessInfo
        {
            get { return _processInfo; }
        }

        public ProcessesInfoService()
        {
            total = true;
            instanceName = "_Total";

            _performancePage = new PerformanceCounter("Paging File", "% Usage", instanceName, machineName);
            _performanceCPU = new PerformanceCounter("Processor", "% Processor Time", instanceName, machineName);
            _performanceRAM = new PerformanceCounter("Memory", "Available MBytes", String.Empty, machineName);
            //_performanceRAM = new PerformanceCounter("Memory", "% Committed Bytes In Use", String.Empty, machineName);

            Initial();
        }

        public ProcessesInfoService(ProcessDetails processDetails)
        {
            instanceName = processDetails.ProcessName;

            _performancePage = new PerformanceCounter("Process", "IO Data Bytes/sec", instanceName);
            _performanceCPU = new PerformanceCounter("Process", "% Processor Time", instanceName);
            _performanceRAM = new PerformanceCounter("Process", "Working Set - Private", instanceName);
            Initial();
        }

        private void Initial()
        {
            _performancesNic = GetNICCounters();
        }

        public Task LoadProcessesInfo()
        {
            return Task.Factory.StartNew(() =>
            {
                _processInfo = new ProcessInfo
                {
                    ProcessName = instanceName,
                    ProcessCPU = String.Format("{0:##0}%", _performanceCPU.NextValue()),
                };

                var length = _performancesNic.Length;
                _processInfo.ProcessNics = new List<ProcessNic>(length);

                for (int i = 0; i < length; i++)
                {
                    _processInfo.ProcessNics.Add(new ProcessNic
                    {
                        Name = _performancesNic[i].InstanceName,
                        Value = String.Format("{0:####0KB/s}", _performancesNic[i].NextValue() / 1024)
                    });
                }

                if (total)
                {
                    _processInfo.ProcessPage = String.Format("{0:##0}%", _performancePage.NextValue());
                    _processInfo.ProcessRAM = String.Format("{0} MB", _performanceRAM.NextValue());
                    //_processInfo.ProcessRAM = String.Format("{0:##0}%", ramCounter.NextValue());
                }
                else
                {
                    _processInfo.ProcessPage = String.Format("{0} MB/s", _performancePage.NextValue());
                    _processInfo.ProcessRAM = String.Format("{0:0.0} MB",
                        Convert.ToDouble(_performanceRAM.NextValue()) / 1024 / 1024);
                }
            });

            //IEnumerable<Process> procList = Process.GetProcesses();

            //_processInfo = procList.OfType<Process>().Select(process =>
            //{
            //    using (process)
            //    using (PerformanceCounter performanceProcess = new PerformanceCounter("Process", "% Processor Timet", process.ProcessName))
            //    using (PerformanceCounter performanceMemory = new PerformanceCounter("Process", "Working Set - Private", process.ProcessName))
            //    {
            //        return new ProcessInfo
            //        {
            //            ProcessName = process.ProcessName,
            //            ProcessMemory = (Convert.ToInt32(performanceMemory.NextValue()) / 1024d).ToString("0.0"),
            //            ProcessCPU = Convert.ToDouble(performanceProcess.NextValue()).ToString("0.0")
            //        };
            //    }
            //}).ToList();
        }

        private PerformanceCounter[] GetNICCounters()
        {
            string[] nics = GetNICInstances();
            List<PerformanceCounter> nicCounters = new List<PerformanceCounter>();
            foreach (string nicInstance in nics)
            {
                nicCounters.Add(new PerformanceCounter("Network Interface", "Bytes Total/sec", nicInstance, machineName));
            }

            return nicCounters.ToArray();
        }

        private string[] GetNICInstances()
        {
            string filter = "MS TCP Loopback interface";
            List<string> nics = new List<string>();
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface", machineName);
            if (category.GetInstanceNames() != null)
            {
                foreach (string nic in category.GetInstanceNames())
                {
                    if (!nic.Equals(filter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        nics.Add(nic);
                    }
                }
            }

            return nics.ToArray();
        }

        public void Dispose()
        {
            if (_performanceCPU != null)
            {
                _performanceCPU.Dispose();
            }
            if (_performanceRAM != null)
            {
                _performanceRAM.Dispose();
            }
            if (_performancePage != null)
            {
                _performancePage.Dispose();
            }

            if (_performancesNic != null)
            {
                for (int i = 0; i < _performancesNic.Length; i++)
                {
                    _performancesNic[i].Dispose();

                }
            }
        }
    }
}
