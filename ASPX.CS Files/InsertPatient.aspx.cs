using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Text.RegularExpressions;


namespace MorgueTracker3
{
    public partial class InsertPatient : Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MorgueTrackerConn"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            lbStatus.Visible = false;
            txtPatientID.Focus();

        }

        public static string stripStringPatient(string str)
        {
            return str.Replace("\\", "");

        }
        public static string stripStringEmployee(string input)
        {
            // Remove slashes before and after the string
            input = input.Trim('\\');

            // Remove leading zeros
            int leadingZeros = 0;
            while (leadingZeros < input.Length && input[leadingZeros] == '0')
            {
                leadingZeros++;
            }

            // Check if the leading digit is followed by exactly four zeros
            if (leadingZeros > 0 && leadingZeros + 4 < input.Length && input[leadingZeros + 1] == '0' &&
                input[leadingZeros + 2] == '0' && input[leadingZeros + 3] == '0' && input[leadingZeros + 4] == '0')
            {
                leadingZeros += 5; // Skip the leading digit and the following four zeros
            }

            input = input.Substring(leadingZeros);

            // Remove trailing zeros
            int trailingZeros = 0;
            while (trailingZeros < input.Length && input[input.Length - 1 - trailingZeros] == '0')
            {
                trailingZeros++;
            }

            // Check if the last digit is preceded by exactly four zeros
            if (trailingZeros > 0 && trailingZeros + 4 <= input.Length && input[input.Length - trailingZeros - 5] == '0' &&
                input[input.Length - trailingZeros - 4] == '0' && input[input.Length - trailingZeros - 3] == '0' && input[input.Length - trailingZeros - 2] == '0')
            {
                trailingZeros += 5; // Skip the trailing four zeros and the last digit
            }

            input = input.Substring(0, input.Length - trailingZeros);

            return input;
        }

        private string checkIfDigits(string input)
        {
            // Match any non-digit characters using regex
            Match match = Regex.Match(input, @"^(?:\d+|\\(\d+)\\)$");


            // If there are any non-digit characters, display an error message
            if (!match.Success)
            {
                //string errorMessage = "Patient ID and Employee ID should only contain numbers (0-9).";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", $"alert('{errorMessage}');", true);

                lbStatus.Text = "Invalid Patient ID or Employee ID";
                lbStatus.Visible = true;
                lbStatus.Attributes.Add("style", "border-color: red;");
                return string.Empty; // Return an empty string to indicate an error
            }

            // If there are no non-digit characters, return the cleaned string
            return input;
        }


        protected void Submit_OnClick(object sender, EventArgs e)
        {
            string PatientID = stripStringPatient(txtPatientID.Text.ToString());
            string PatientName = txtPatientName.Text.ToString();
            string EmployeeID = stripStringEmployee(txtEmployeeID.Text.ToString());
            string EmployeeName = txtEmployeeName.Text.ToString();

            PatientID = checkIfDigits(PatientID);
            EmployeeID = checkIfDigits(EmployeeID);

            // Check that Patient ID that already exists
            SqlCommand cmdCheckPatientExists = new SqlCommand("SELECT COUNT(*) FROM MorgueTracker WHERE Patient_ID = @Patient_ID", conn);
            cmdCheckPatientExists.Parameters.AddWithValue("@Patient_ID", PatientID);

            conn.Open();
            int count = (int)cmdCheckPatientExists.ExecuteScalar();
            conn.Close();

            if (string.IsNullOrEmpty(txtPatientID.Text))
            {
                lbStatus.Text = "Please Input a Patient ID";
                lbStatus.Visible = true;
                lbStatus.Attributes.Add("style", "border-color: red;");
            }

            else if (count > 0)
            {
                lbStatus.Text = "Patient ID already exists";
                lbStatus.Visible = true;
            }
            else
            {
                SqlCommand cmdInsertMorguePatient = new SqlCommand("InsertMorguePatient", conn);
                cmdInsertMorguePatient.CommandType = System.Data.CommandType.StoredProcedure;

                if (int.TryParse(PatientID, out int parsedPatientID) && int.TryParse(EmployeeID, out int parsedEmployeeID))
                {
                    cmdInsertMorguePatient.Parameters.AddWithValue("@Patient_ID", parsedPatientID);
                    cmdInsertMorguePatient.Parameters.AddWithValue("@Patient_Name", PatientName);
                    cmdInsertMorguePatient.Parameters.AddWithValue("@In_Employee_ID", parsedEmployeeID);
                    cmdInsertMorguePatient.Parameters.AddWithValue("@In_Employee_Name", EmployeeName);
                    cmdInsertMorguePatient.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString());

                    try
                    {
                        conn.Open();
                        cmdInsertMorguePatient.ExecuteNonQuery();

                        lbStatus.Text = "Patient Added Successfuly";
                        lbStatus.Visible = true;
                        lbStatus.Attributes.Add("style", "border-color: green;");
                    }
                    catch
                    {
                        lbStatus.Text = "Patient Upload Failed";
                        lbStatus.Visible = true;
                        lbStatus.Attributes.Add("style", "border-color: red;");
                    }
                    finally
                    {
                        conn.Close();

                    }
                }
            }
        }
    }
}
