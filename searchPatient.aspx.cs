using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;





namespace Morgue_Tracker
{
    public partial class searchPatient : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MorgueTrackerConn"].ConnectionString);




        protected void Page_Load(object sender, EventArgs e)
        {





        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {




            try
            {
                conn.Open();
                string searchPatientID = txtSearch.Text;



                // Create a SQL query to retrieve patient information based on the provided patient ID
                string query = "SELECT * FROM MorgueTrackerTest WHERE Patient_ID = @searchPatientID";



                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@searchPatientID", searchPatientID);



                SqlDataReader reader = cmd.ExecuteReader();



                if (reader.Read())
                {
                    // Fill the textboxes with the retrieved data
                    txtPatientName.Text = reader["Patient_Name"].ToString();
                    txtInEmpName.Text = reader["In_Employee_Name"].ToString();
                    txtMorgueLoc.Text = reader["Location_In_Morgue"].ToString();
                    txtInEmpID.Text = reader["In_Employee_ID"].ToString();
                    txtCreateDate.Text = reader["Created_Date"].ToString();



                    // Hide or show buttons and textboxes as needed
                    txtPatientName.Visible = true;
                    lblPatientName.Visible = true;
                    lblInEmpName.Visible = true;
                    txtInEmpName.Visible = true;
                    lblMorgueLoc.Visible = true;
                    txtMorgueLoc.Visible = true;
                    lblInEmpID.Visible = true;
                    txtInEmpID.Visible = true;
                    lblCreateDate.Visible = true;
                    txtCreateDate.Visible = true;
                    releaseButton.Visible = true;
                    Button1.Visible = true;
                    releaseButton.Attributes.Add("style", "width: 200px; margin-top: 22.5px");



                    object Funeral_Home = reader["Funeral_Home"];



                    if (Funeral_Home != DBNull.Value && !string.IsNullOrEmpty(Funeral_Home.ToString()))
                    {
                        lblFunEmpName.Visible = true;
                        txtFunEmpName.Visible = true;
                        lblOutEmpName.Visible = true;
                        txtOutEmpName.Visible = true;
                        lblFunHome.Visible = true;
                        txtFunHome.Visible = true;
                        lblOutEmpID.Visible = true;
                        txtOutEmpID.Visible = true;
                        lblReleaseDate.Visible = true;
                        txtReleaseDate.Visible = true;
                        updateButton.Visible = true;
                        Button1.Visible = false;
                        releaseButton.Attributes.Add("style", "width: 400px; margin-top: 22.5px");




                        // Fill the textboxes with the retrieved data
                        txtFunEmpName.Text = reader["Funeral_Home_Employee"].ToString();
                        txtOutEmpName.Text = reader["Out_Employee_Name"].ToString();
                        txtFunHome.Text = reader["Funeral_Home"].ToString();
                        txtOutEmpID.Text = reader["Out_Employee_ID"].ToString();
                        //txtReleaseDate.Text = reader["Pick_Up_Date"].ToString();
                    }
                    else
                    {
                        lblFunEmpName.Visible = false;
                        txtFunEmpName.Visible = false;
                        lblOutEmpName.Visible = false;
                        txtOutEmpName.Visible = false;
                        lblFunHome.Visible = false;
                        txtFunHome.Visible = false;
                        lblOutEmpID.Visible = false;
                        txtOutEmpID.Visible = false;
                        lblReleaseDate.Visible = false;
                        txtReleaseDate.Visible = false;
                        updateButton.Visible = false;
                    }



                }
                else
                {
                    // If no data is found for the given patient ID, you can handle the case here.
                    // For example, you can display a message saying "Patient ID not found" or clear the textboxes.
                    // It depends on your specific use case.
                }



                reader.Close();
            }
            finally
            {
                conn.Close();
            }
        }




        protected void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                lblFunEmpName.Visible = true;
                txtFunEmpName.Visible = true;
                lblOutEmpName.Visible = true;
                txtOutEmpName.Visible = true;
                lblFunHome.Visible = true;
                txtFunHome.Visible = true;
                lblOutEmpID.Visible = true;
                txtOutEmpID.Visible = true;
                lblReleaseDate.Visible = true;
                txtReleaseDate.Visible = true;
                updateButton.Visible = true;
                Button1.Visible = false;
                releaseButton.Attributes.Add("style", "width: 400px; margin-top: 22.5px");

                DateTime time = DateTime.Now;
                txtReleaseDate.Text = time.ToString();


                // get current date and put it into textbox and database
            }
            catch { }
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string searchPatientID = txtSearch.Text;



                // Create a SQL query to update the patient information in the database based on the provided patient ID
                string query = "UPDATE MorgueTrackerTest SET Patient_Name = @PatientName, In_Employee_Name = @InEmpName, " +
                               "Location_In_Morgue = @MorgueLoc, In_Employee_ID = @InEmpID, Funeral_Home_Employee = @FunEmpName, " +
                               "Out_Employee_Name = @OutEmpName, Funeral_Home = @FunHome, Out_Employee_ID = @OutEmpID, " +
                               "Picked_Up_Date = @ReleaseDate " +
                               "WHERE Patient_ID = @searchPatientID";



                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientName", txtPatientName.Text);
                cmd.Parameters.AddWithValue("@InEmpName", txtInEmpName.Text);
                cmd.Parameters.AddWithValue("@MorgueLoc", txtMorgueLoc.Text);
                cmd.Parameters.AddWithValue("@InEmpID", txtInEmpID.Text);
                cmd.Parameters.AddWithValue("@FunEmpName", txtFunEmpName.Text);
                cmd.Parameters.AddWithValue("@OutEmpName", txtOutEmpName.Text);
                cmd.Parameters.AddWithValue("@FunHome", txtFunHome.Text);
                cmd.Parameters.AddWithValue("@OutEmpID", txtOutEmpID.Text);
                cmd.Parameters.AddWithValue("@ReleaseDate", txtReleaseDate.Text);
                cmd.Parameters.AddWithValue("@searchPatientID", searchPatientID);



                int rowsAffected = cmd.ExecuteNonQuery();



                if (rowsAffected > 0)
                {
                    // Display a success message or perform any other actions after successful update.
                }
                else
                {
                    // Display an error message or perform any other actions if the update fails.
                }
            }
            finally
            {
                conn.Close();
            }
        }



    }
}

