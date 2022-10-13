using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace PRG455._140206194.LabProject
{
    public partial class frmMainCode : Form
    {
        public static frmMainCode form1;
        public frmMainCode()
        {
            InitializeComponent();
        }

        private void frmMainCode_Load(object sender, EventArgs e)
        {
            form1 = this;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Core.btnStart_Click(sender,e);
            grpUserData.Visible = true;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Core.btnAddUser_Click(sender, e);
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            Core.btnEditUser_Click(sender, e);
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            Core.btnDeleteUser_Click(sender, e);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Core.btnSubmitScreening_Click(sender, e);
        }

        private void btnSearchUId_Click_1(object sender, EventArgs e)
        {
            Core.btnSearchUIdNo_Click(sender, e);
        }

        private void bttnSearchFailPass_Click(object sender, EventArgs e)
        {
            Core.btnSearchPassFail_Click(sender, e);
        }
    }
}
