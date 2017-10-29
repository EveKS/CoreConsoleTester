using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Processing.Services;
using Processing.Models;

namespace Processing.UserControls
{
    public partial class ProcessInfoControl : UserControl
    {
        private ProcessDetails processDetails;
        private bool toggle;

        private ProcessesInfoService _processesInfoService;

        public ProcessInfoControl()
        {
            InitializeComponent();

            this.Size = new Size(250, 25);
            sSStatusBar.Visible = false;

            bProcessName.Click += ProcessInfoControl_Click;
        }

        private async void ProcessInfoControl_Click(object sender, EventArgs e)
        {
            toggle ^= true;

            if (toggle)
            {
                this.Height = 50;
                sSStatusBar.Visible = true;

                _processesInfoService = new ProcessesInfoService(processDetails);
                await _processesInfoService.LoadProcessesInfo();

                tSCPU.Text = _processesInfoService.ProcessInfo.ProcessCPU;
                tSRAM.Text = _processesInfoService.ProcessInfo.ProcessRAM;
                tSPage.Text = _processesInfoService.ProcessInfo.ProcessPage;
            }
            else
            {
                this.Height = 25;
                sSStatusBar.Visible = false;

                _processesInfoService.Dispose();
                _processesInfoService = null;
            }
        }

        public int ControlWidth
        {
            set
            {
                var settextAction = new Action(() => { this.Width = value; });

                if (this.InvokeRequired)
                    this.Invoke(settextAction);
                else
                    settextAction();
            }
        }

        public ProcessDetails ProcessName
        {
            set
            {
                processDetails = value;
                var settextAction = new Action(() => { bProcessName.Text = value.ProcessName; });

                if (bProcessName.InvokeRequired)
                    bProcessName.Invoke(settextAction);
                else
                    settextAction();
            }
        }

        //public string CPU
        //{
        //    set
        //    {
        //        var settextAction = new Action(() => { lCPU.Text = value; });

        //        if (lCPU.InvokeRequired)
        //            lCPU.Invoke(settextAction);
        //        else
        //            settextAction();
        //    }
        //}

        //public string Memory
        //{
        //    set
        //    {
        //        var settextAction = new Action(() => { lMemory.Text = value; });

        //        if (lMemory.InvokeRequired)
        //            lMemory.Invoke(settextAction);
        //        else
        //            settextAction();
        //    }
        //}

        //public string Disk
        //{
        //    set
        //    {
        //        var settextAction = new Action(() => { lDisk.Text = value; });

        //        if (lDisk.InvokeRequired)
        //            lDisk.Invoke(settextAction);
        //        else
        //            settextAction();
        //    }
        //}

        //public string Net
        //{
        //    set
        //    {
        //        var settextAction = new Action(() => { lNet.Text = value; });

        //        if (lNet.InvokeRequired)
        //            lNet.Invoke(settextAction);
        //        else
        //            settextAction();
        //    }
        //}
    }
}
