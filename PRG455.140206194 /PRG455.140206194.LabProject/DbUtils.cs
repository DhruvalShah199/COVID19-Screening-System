using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace PRG455._140206194.LabProject
{
    public class DbUtils : frmMainCode
    {
        public static frmMainCode form3 = new frmMainCode();
        public static SqlConnection conc = new SqlConnection(Properties.Settings.Default.connString);
        public static string CheckUserId(string UserId)
        {
            string iD = null;
            try
            {
                using (SqlConnection conc = new SqlConnection(Properties.Settings.Default.connString))
                {
                    conc.Open();

                    using (SqlCommand cmd = conc.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM User WHERE UserId=@UserId";
                        cmd.Parameters.Add("@UserId", SqlDbType.Int);
                        cmd.Parameters["@UserId"].Value = Convert.ToInt32(UserId);

                        cmd.ExecuteNonQuery();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            iD = dr["UserId"].ToString();
                        }

                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }

            return iD;
        }

        public static string CheckUserNm(string UserName)
        {
            string nM = null;
            try
            {
                using (SqlConnection conc = new SqlConnection(Properties.Settings.Default.connString))
                {
                    conc.Open();

                    using (SqlCommand cmd = conc.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM User WHERE UserName=@UserName";
                        cmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
                        cmd.Parameters["@UserName"].Value = UserName;

                        cmd.ExecuteNonQuery();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            nM = dr["UserName"].ToString();
                        }

                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }

            return nM;
        }

        public static string CheckUserType(string UserType)
        {
            string tP = null;
            try
            {
                using (SqlConnection conc = new SqlConnection(Properties.Settings.Default.connString))
                {
                    conc.Open();

                    using (SqlCommand cmd = conc.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM User WHERE UserType=@UserType";
                        cmd.Parameters.Add("@UserType", SqlDbType.NVarChar);
                        cmd.Parameters["@UserType"].Value = UserType;

                        cmd.ExecuteNonQuery();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            tP = dr["UserType"].ToString();
                        }

                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }

            return tP;
        }

        public static string CheckUserFg(string UserFlagged)
        {
            string fG = null;
            try
            {
                using (SqlCommand cmd = conc.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM User WHERE UserFlagged=@UserFlagged";
                    cmd.Parameters.Add("@UserFlagged", SqlDbType.NVarChar);
                    cmd.Parameters["@UserFlagged"].Value = UserFlagged;

                    cmd.ExecuteNonQuery();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        fG = dr["UserFlagged"].ToString();
                    }

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }

            return fG;
        }

        public static void InputUserData(string UsrId, string UsrNm, string UsrTyp, string UsrFlg)
        {
            int Id;
            int.TryParse(UsrId, out Id);
            try
            {
                if (conc.State == ConnectionState.Closed)
                    conc.Open();

                SqlCommand cmd = new SqlCommand("AddUsers", conc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", Id);
                cmd.Parameters.AddWithValue("@UserName", UsrNm);
                cmd.Parameters.AddWithValue("@UserType", UsrTyp);
                cmd.Parameters.AddWithValue("@UserFlagged", UsrFlg);

                cmd.ExecuteNonQuery();
                MessageBox.Show("User ADDED successfully! Please proceed further.", "Info", MessageBoxButtons.OK);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
            finally
            {
                conc.Close();
            }
        }

        public static void EditUserData(string UsrId, string UsrNm, string UsrTyp, string UsrFlg)
        {
            int Id;
            int.TryParse(UsrId, out Id);
            try
            {
                if (conc.State == ConnectionState.Closed)
                    conc.Open();

                SqlCommand cmd = new SqlCommand("EditUsers", conc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", Id);
                cmd.Parameters.AddWithValue("@UserName", UsrNm);
                cmd.Parameters.AddWithValue("@UserType", UsrTyp);
                cmd.Parameters.AddWithValue("@UserFlagged", UsrFlg);

                cmd.ExecuteNonQuery();
                MessageBox.Show("User data EDITED successfully! Please proceed further.", "Info", MessageBoxButtons.OK);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
            finally
            {
                conc.Close();
            }
        }

        public static void DeleteUserData(string UsrId)
        {
            int Id;
            int.TryParse(UsrId, out Id);
            try
            {
                if (conc.State == ConnectionState.Closed)
                    conc.Open();

                SqlCommand cmd = new SqlCommand("DeleteUsers", conc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", Id);

                cmd.ExecuteNonQuery();
                MessageBox.Show("User data DELETED successfully! Please proceed further.", "Info", MessageBoxButtons.OK);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
            finally
            {
                conc.Close();
            }
        }

        public static void SubmitScreening(string UsrId, string CloseCon, string Travel, string Symp, string Dt)
        {
            int Id;
            int.TryParse(UsrId, out Id);
            DateTime d1 = DateTime.ParseExact(Dt, "dd/MM/yyyy", null);
            try
            {
                if (conc.State == ConnectionState.Closed)
                    conc.Open();

                SqlCommand cmd = new SqlCommand("ScreeningTab", conc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", Id);
                cmd.Parameters.AddWithValue("@CloseContact", CloseCon);
                cmd.Parameters.AddWithValue("@Travelled", Travel);
                cmd.Parameters.AddWithValue("@Symptoms", Symp);
                cmd.Parameters.AddWithValue("@Date", d1);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Screening data SUBMITTED! Please proceed further.", "Info", MessageBoxButtons.OK);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
            finally
            {
                conc.Close();
            }
        }

        public static void UserFlagged()
        {
            try
            {
                if (conc.State == ConnectionState.Closed)
                    conc.Open();
                using (SqlCommand cmd = conc.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE User SET User.UserFlagged = Yes FROM  User INNER JOIN  Screenings ON User.UserId = Screenings.UserId";

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! " + exp.Message);
            }
            finally
            {
                conc.Close();
            }
        }

        public static void SearchUserId(string UsrId)
        {
            int Id;
            int.TryParse(UsrId, out Id);
            if (conc.State == ConnectionState.Closed)
                conc.Open();
            SqlDataAdapter da = new SqlDataAdapter("SearchUserId", conc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@UserId", Id);
            DataTable dtbl = new DataTable();
            da.Fill(dtbl);
            form3.dgvReport.DataSource = dtbl;
            conc.Close();
            form3.lblTotalRecords.Text = form3.dgvReport.RowCount.ToString();
        }
        
        public static void SearchPassFail(string UsrFlag)
        {
            if (conc.State == ConnectionState.Closed)
                conc.Open();
            SqlDataAdapter da = new SqlDataAdapter("SearchFailPass", conc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@UserFlagged", UsrFlag);
            DataTable dtbl = new DataTable();
            da.Fill(dtbl);
            form3.dgvReport.DataSource = dtbl;
            conc.Close();
            form3.lblTotalRecords.Text = form3.dgvReport.RowCount.ToString();
        }

        public static void SearchDateRange(string sql, DataGridView dtg)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt;
            try
            {
                conc.Open();
                cmd = new SqlCommand();
                cmd.Connection = conc;
                cmd.CommandText = sql;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                dtg.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conc.Close();
                da.Dispose();
            }
            form3.lblTotalRecords.Text = form3.dgvReport.RowCount.ToString();
        }
    }
}    

