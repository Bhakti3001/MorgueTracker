using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Morgue_Tracker
{
    public partial class insertPatient : System.Web.UI.Page
    {
       private  SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MorgueTrackerConn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get the input values from the form controls
            try
            {
                conn.Open();
                string patientName = txtPatientName.Text;
                string patientID = txtPatientID.Text;
                string employeeName = txtEmpName.Text;
                string employeeID = txtEmpID.Text;

                string query = "INSERT INTO MorgueTrackerTest (Patient_Name, Patient_ID, In_Employee_Name, In_Employee_ID, Created_Date) " +
                   "VALUES (@patientName, @patientID, @employeeName, @employeeID, GETDATE())";

                // Create a SqlCommand object to execute the query
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@patientName", patientName);
                    cmd.Parameters.AddWithValue("@patientID", patientID);
                    cmd.Parameters.AddWithValue("@employeeName", employeeName);
                    cmd.Parameters.AddWithValue("@employeeID", employeeID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        string successMessage = "Patient successfully inserted!";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{successMessage}');", true);
                        txtPatientName.Text = "";
                        txtPatientID.Text = "";
                        txtEmpName.Text = "";
                        txtEmpID.Text = "";
                    }
                    conn.Close();
                }
            }

            catch (SqlException ex)
            {
                if (ex.Number == 2627) // 2627 is the error number for a unique constraint violation (duplicate key).
                {
                    string errorMessage = "Patient ID must be unique. A patient with the same ID already exists!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{errorMessage}');", true);
                }
                else
                {
                    string error = ex.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{error}');", true);
                }
            }

            catch (Exception ex)
            {
                string error = ex.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{error}');", true);
            }
        }
    }
}