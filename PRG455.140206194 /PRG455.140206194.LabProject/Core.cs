using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;

namespace PRG455._140206194.LabProject
{
    public class Core : frmMainCode
    {
        public static frmMainCode form2 = new frmMainCode();

        public static void btnStart_Click(object sender, EventArgs e)
        {
            string checkId = null;
            string Id = form2.txtUId.Text.Trim();

            checkId = DbUtils.CheckUserId(Id);

            if (checkId != null)
            {
                form2.grpUserData.Visible = true;
                form2.btnAddUser.Enabled = false;

                MessageBox.Show("User already exists. You can either choose the option to Edit User or Delete User.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                form2.txtUserId.Text = Id;
                form2.txtUserId.Enabled = false;
                form2.txtUserName.Text = DbUtils.CheckUserNm(form2.txtUserName.Text.Trim());
                form2.cboUserType.Text = DbUtils.CheckUserType(form2.cboUserType.SelectedText.Trim());
                form2.cboYesNo.Text = DbUtils.CheckUserFg(form2.cboYesNo.SelectedText.Trim()) ;
            }
            else
            {
                form2.grpUserData.Visible = true;
                form2.btnEditUser.Enabled = false;
                form2.btnDeleteUser.Enabled = false;

                MessageBox.Show("Please enter your data in the spaces provided below. Select Add User after filling up all the informations.", "", MessageBoxButtons.OK,MessageBoxIcon.Information);

                form2.txtUserId.Text = Id;
                form2.txtUserName.Text = "";
                form2.cboUserType.Text = "";
                form2.cboYesNo.Text = "";
            }
        }

        public static void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                DbUtils.InputUserData(form2.txtUId.Text.Trim(), form2.txtUserName.Text.Trim(), form2.cboUserType.SelectedText.Trim(), form2.cboYesNo.SelectedText.Trim());

            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
        }

        public static void btnEditUser_Click(object sender, EventArgs e)
        {
            try
            {
                DbUtils.EditUserData(form2.txtUserId.Text.Trim(), form2.txtUserName.Text.Trim(), form2.cboUserType.SelectedText.Trim(), form2.cboYesNo.SelectedText.Trim());

            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
        }

        public static void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                DbUtils.DeleteUserData(form2.txtUId.Text.Trim());
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
        }

        public static void btnSubmitScreening_Click(object sender, EventArgs e)
        {
            string dt = Convert.ToDateTime(form2.dtpScreening.Value).ToShortDateString();
            try
            {
                DbUtils.SubmitScreening(form2.txtUserId.Text.Trim(),form2.cboQ1.SelectedText.Trim(), form2.cboQ2.SelectedText.Trim(), form2.cboQ3.SelectedText.Trim(), dt);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
            if(form2.cboQ1.SelectedText=="Yes"&& form2.cboQ2.SelectedText=="Yes"&& form2.cboQ3.SelectedText=="Yes")
            {
                DbUtils.UserFlagged();
            }
        }

        public static void btnSearchUIdNo_Click(object sender, EventArgs e)
        {
            try
            {
                DbUtils.SearchUserId(form2.txtUserIdSearch.Text.Trim());
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
        }

        public static void btnSearchPassFail_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                DateTime from = form2.dtFrom.Value;
                DateTime to = form2.dtTo.Value;

                sql = "SELECT * FROM tblpayroll WHERE DATE(PayrollDate) BETWEEN '" + from.ToString("yyyy-MM-dd") + "' AND '" + to.ToString("yyyy-MM-dd") + "'";

                DbUtils.SearchDateRange((sql, form2.dgvReport));
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
        }

        public static void btnExport_Click(object sender, EventArgs e)
        {
            if (form2.dgvReport.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Output.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = form2.dgvReport.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[form2.dgvReport.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += form2.dgvReport.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < form2.dgvReport.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += form2.dgvReport.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }

        }
    }
}