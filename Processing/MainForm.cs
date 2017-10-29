using Processing.Helpers;
using Processing.Managers;
using Processing.Services;
using Processing.UserControls;
using Processing.Utilitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Processing
{
    public partial class MainForm : Form
    {
        private const int INTERVAL = 1500;
        private static System.Timers.Timer _timer = new System.Timers.Timer(INTERVAL);

        private int smallLabelWidth = 40;
        private int bigLabelWidth = 60;

        private ProcessesInfosUtility _infoManager = new ProcessesInfosUtility();

        ProcessesInfoService _processInfo;

        public MainForm()
        {
            InitializeComponent();

            this.FormClosing += MainForm_FormClosing;
            this.Load += MainForm_Load;

            foreach (var item in _infoManager.ProcessesNames())
            {
                var processInfo = new ProcessInfoControl
                {
                    ProcessName = item,
                    //Memory = (Convert.ToInt32(performanceMemory.NextValue()) / 1024d).ToString("0.0"),
                    //CPU = Convert.ToDouble(performanceProcess.NextValue()).ToString("0.0")
                };

                processInfo.ControlWidth = fLPMainPanel.Width - 25;
                processInfo.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                fLPMainPanel.Controls.Add(processInfo);
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            _processInfo = new ProcessesInfoService();
            await _processInfo.LoadProcessesInfo();

            for (int i = 0; i < _processInfo.ProcessInfo.ProcessNics.Count; i++)
            {
                sSStatusBar.Items.Add(GetNICLabel(_processInfo.ProcessInfo.ProcessNics[i].Name, i));
            }

            _timer.Elapsed += TimerMainForm_Tick;
            _timer.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _processInfo.Dispose();
        }

        private void TimerMainForm_Tick(object sender, EventArgs e)
        {
            ShowDetail();
        }

        private async void ShowDetail()
        {
            await _processInfo.LoadProcessesInfo();

            var pi = _processInfo.ProcessInfo;

            tSCPU.Text = pi.ProcessCPU;
            tSRAM.Text = pi.ProcessRAM;
            tSPage.Text = pi.ProcessPage;

            for (int i = 0; i < pi.ProcessNics.Count; i++)
            {
                sSStatusBar.Items[String.Format("tsNIC{0}", i)].Text = pi.ProcessNics[i].Value;
            }
        }

        private PerformanceCounter[] GetNICCounters(string machineName)
        {
            string[] nics = GetNICInstances(machineName);
            List<PerformanceCounter> nicCounters = new List<PerformanceCounter>();
            foreach (string nicInstance in nics)
            {
                nicCounters.Add(new PerformanceCounter("Network Interface", "Bytes Total/sec", nicInstance, machineName));
            }
            return nicCounters.ToArray();
        }

        private string[] GetNICInstances(string machineName)
        {
            string filter = "MS TCP Loopback interface";
            List<string> nics = new List<string>();
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface", machineName);
            if (category.GetInstanceNames() != null)
            {
                foreach (string nic in category.GetInstanceNames())
                {
                    if (!nic.Equals(filter, StringComparison.InvariantCultureIgnoreCase))
                    { nics.Add(nic); }
                }
            }
            return nics.ToArray();
        }

        private ToolStripStatusLabel GetNICLabel(string instanceName, int index)
        {
            ToolStripStatusLabel newLabel = new ToolStripStatusLabel
            {
                AutoSize = false,
                Width = bigLabelWidth,
                ToolTipText = instanceName,
                Text = string.Empty,
                Name = String.Format("tsNIC{0}", index),
                TextAlign = ContentAlignment.MiddleRight,
                BorderSides = ToolStripStatusLabelBorderSides.All,
                BorderStyle = Border3DStyle.SunkenInner,
                Height = sSStatusBar.Items[0].Height
            };

            return newLabel;
        }
    }
}
