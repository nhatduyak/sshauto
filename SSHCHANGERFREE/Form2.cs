using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSHCHANGERFREE
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }        
        public string linkssh
        {
            get
            {
                return txtLinkssh.Text;
            }
            set
            {
                txtLinkssh.Text = value;
            }
        }
        public int scheduleddelay
        {
            get
            {
                return Convert.ToInt32(nmrScheduled.Value);
            }
            set
            {
                nmrScheduled.Value = Convert.ToDecimal(value);                
            }
        }
        public bool randomssh
        {
            get
            {
                return cbRandomssh.Checked;
            }
            set
            {
                cbRandomssh.Checked = value;
            }
        }
        public bool location
        {
            get
            {
                return cblocation.Checked;
            }
            set
            {
                cblocation.Checked = value;
            }
        }
        public bool hidebvs
        {
            get
            {
                return cbhidebvs.Checked;
            }
            set
            {
                cbhidebvs.Checked = value;
            }
        }
        public bool autosave
        {
            get
            {
                return cbSavessh.Checked;
            }
            set
            {
                cbSavessh.Checked = value;
            }
        }
        public bool whoershowinfo
        {
            get
            {
                return cbShowinfo.Checked;
            }
            set
            {
                cbShowinfo.Checked = value;
            }
        }
        public bool whoercheckbl
        {
            get
            {
                return cbcheckbl.Checked;
            }
            set
            {
                cbcheckbl.Checked = value;
            }
        }
        public bool whoercheckproxy
        {
            get
            {
                return cbcheckproxy.Checked;
            }
            set
            {
                cbcheckproxy.Checked = value;
            }
        }
        public bool loopssh
        {
            get
            {
                return cbLoopssh.Checked;
            }
            set
            {
                cbLoopssh.Checked = value;
            }
        }
        public bool changeby247
        {
            get
            {
                return rbAuto247.Checked;
            }
            set
            {
                rbAuto247.Checked = value;
            }
        }
        public bool changebyscheduled
        {
            get
            {
                return rbAutobytime.Checked;
            }
            set
            {
                rbAutobytime.Checked = value;
            }
        }
        public bool changebymanual
        {
            get
            {
                return rbManual.Checked;
            }
            set
            {
                rbManual.Checked = value;
            }
        }
        public bool importsshfromfile
        {
            get
            {
                return rbSSHfile.Checked;
            }
            set
            {
                rbSSHfile.Checked = value;
            }
        }
        private void rbSSHfile_CheckedChanged(object sender, EventArgs e)
        {
            txtLinkssh.Enabled = rbSSHserver.Checked;            
        }       
        private void rbSSHserver_CheckedChanged(object sender, EventArgs e)
        {
            txtLinkssh.Enabled = rbSSHserver.Checked;           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(txtLinkssh, "Link of ssh (text file)");
            toolTip1.SetToolTip(rbAuto247, "Automatically change when ssh die");
        }

        private void rbManual_CheckedChanged(object sender, EventArgs e)
        {
            nmrScheduled.Enabled = rbAutobytime.Checked;            
        }

        private void rbAutobytime_CheckedChanged(object sender, EventArgs e)
        {
            nmrScheduled.Enabled = rbAutobytime.Checked;            
        }

        private void cbShowinfo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowinfo.Checked)
            {
                cbcheckbl.Enabled = true;
                cbcheckproxy.Enabled = true;
            }
            else
            {
                cbcheckbl.Checked = false;
                cbcheckproxy.Checked = false;
                cbcheckbl.Enabled = false;
                cbcheckproxy.Enabled = false;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(importsshfromfile == false && txtLinkssh.Text == "")
            {
                MessageBox.Show("Link SSH_FILE can't empty!", "ERROR input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                e.Cancel = true;
            }           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mmo4me.com/threads/share-ssh-changer-free-by-c.311561/");
        }
    }
}
