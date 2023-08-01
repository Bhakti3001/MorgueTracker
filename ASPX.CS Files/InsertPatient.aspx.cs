using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Security.Policy;


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

        public static string removeSlashesPatient(string str)
        {
            return str.Replace("\\", "");
        }

        public static string removeSlashesEmployee(string input)
        {
            // Remove slashes before and after the string
            input = input.Trim('\\');

            // Remove leading and trailing zeros
            input = Regex.Replace(input, @"^0+|0+$", "");

            // Remove numbers followed by exactly four zeros
            input = Regex.Replace(input, @"(\d)0000", "$1");

            return input;
        }

        private string idValidation(string input)
        {
            // Match any non-digit characters using regex, allows digits to be surrounded by a backward slash
            Match match = Regex.Match(input, @"^(?:\d+|\\(\d+)\\)$");

            // if there are any non-digit characters, display an error message and return emtpy string
            if (!match.Success)
            {
                lbStatus.Text = "Invalid Patient ID or Employee ID";
                lbStatus.Visible = true;
                lbStatus.Attributes.Add("style", "border-color: red;");
                return string.Empty; // Return an empty string to indicate an error
            }

            // if number is too big, display error and return emtpy string
            else if (double.Parse(input) > int.MaxValue)
            {
                lbStatus.Text = "Invalid Patient ID or Employee ID";
                lbStatus.Visible = true;
                lbStatus.Attributes.Add("style", "border-color: red;");
                return string.Empty;
            }

            // if input is valid, return the cleaned string
            return input;
        }

        protected void Submit_OnClick(object sender, EventArgs e)
        {
            string PatientID = removeSlashesPatient(txtPatientID.Text.ToString());
            string PatientName = txtPatientName.Text.ToString();
            string EmployeeID = removeSlashesEmployee(txtEmployeeID.Text.ToString());
            string EmployeeName = txtEmployeeName.Text.ToString();

            PatientID = idValidation(PatientID);
            EmployeeID = idValidation(EmployeeID);

            // Check that Patient ID that already exists
            SqlCommand cmdCheckPatientExists = new SqlCommand("SELECT COUNT(*) FROM MorgueTracker WHERE Patient_ID = @Patient_ID", conn);
            cmdCheckPatientExists.Parameters.AddWithValue("@Patient_ID", PatientID);

            conn.Open();
            int count = (int)cmdCheckPatientExists.ExecuteScalar();
            conn.Close();

            // input validation for empty patient ID
            if (string.IsNullOrEmpty(txtPatientID.Text))
            {
                lbStatus.Text = "Please Input a Patient ID";
                lbStatus.Visible = true;
                lbStatus.Attributes.Add("style", "border-color: red;");
            }

            // input validation for names
            // allows normal letters, numbers, accented letters, spaces, commas, apotrophes, and dashes
            // does not allow ×Þß÷þø
            Regex nameRegex = new Regex("(?i)^(?:(?![×Þß÷þø])[- .'0-9a-zÀ-ÿ])+$");

            if (!nameRegex.IsMatch(EmployeeName) && !nameRegex.IsMatch(PatientName))
            {
                lbStatus.Text = "Please input a valid patient and employee name.";
                lbStatus.Visible = true;
                lbStatus.Attributes.Add("style", "border-color: red;");
            }

            else if (!nameRegex.IsMatch(PatientName))
            {
                lbStatus.Text = "Please input a valid patient name.";
                lbStatus.Visible = true;
                lbStatus.Attributes.Add("style", "border-color: red;");
            }

            else if (!nameRegex.IsMatch(EmployeeName))
            {
                lbStatus.Text = "Please input a valid employee name.";
                lbStatus.Visible = true;
                lbStatus.Attributes.Add("style", "border-color: red;");
            }

            // insert patient into database
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
                        lbStatus.Attributes.Add("style", "border-color: lightseagreen;");
                    }
                    catch

                    {
                        lbStatus.Visible = true;
                        lbStatus.Attributes.Add("style", "border-color: red;");

                        // input validation for existing patient ID
                        if (count > 0)
                        {
                            lbStatus.Text = "Patient ID already exists";
                        }
                        else
                        {
                            lbStatus.Text = "Patient Upload Failed";
                        }
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