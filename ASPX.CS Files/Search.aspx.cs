using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Web.UI;

namespace MorgueTracker3
{
    public partial class Search : Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MorgueTrackerConn"].ConnectionString);
        bool isFullDetails = false; // Flag to track if all details are available
        bool isMorgueLocation = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            HideLabels();
            Page.SetFocus(txtPatientID);

        }

        protected string purgeSearch (string str)
        {
            str = str.Trim('\\');
            return str;
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string patientID = purgeSearch(txtPatientID.Text.Trim());

            // Checks that ID is int
            if (!int.TryParse(patientID, out int parsedPatientID))
            {
                lblSuccessStatus.Text = "Invalid Patient ID";
                lblSuccessStatus.Visible = true;
                return;
            }

            // Gets and executes stored procedure
            SqlCommand cmdSearchPatientByID = new SqlCommand("SearchPatientByID", conn);
            cmdSearchPatientByID.CommandType = CommandType.StoredProcedure;
            cmdSearchPatientByID.Parameters.AddWithValue("@Patient_ID", parsedPatientID);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdSearchPatientByID);
            DataTable dataTable = new DataTable();

            conn.Open();
            dataAdapter.Fill(dataTable);
            conn.Close();

            if (dataTable.Rows.Count <= 0)
            {
                lblSuccessStatus.Text = "No results found.";
                lblSuccessStatus.Style.Add("border-color", "red");
                lblSuccessStatus.Visible = true;
            }
            else
            {
                DataRow row = dataTable.Rows[0];

                // 3 cases: 
                // 1: Patient has no morgue location and no funeral home
                // 1: Patient info should be shown, location should show "select location",
                // update should update all info listed above release should allow data entry for funeral home
                // Case 2:  Patient has no funeral home but has morgue location

                isFullDetails = !row.IsNull("Out_Employee_Name");
                isMorgueLocation = !row.IsNull("Location_In_Morgue");

                // Make all fields visible
                ShowLabels();

                txtPatientName.Text = row["Patient_Name"].ToString();
                txtEmployeeName.Text = row["In_Employee_Name"].ToString();
                txtEmployeeID.Text = row["In_Employee_ID"].ToString();
                pCreatedDate.Text = row["Created_Date"].ToString();

                if (isMorgueLocation)
                {
                    ddlLocationInMorgue.Text = row["Location_In_Morgue"].ToString();
                }

                else
                {
                    // Patient has no location, show "Select Location" in the dropdown
                    ddlLocationInMorgue.SelectedIndex = 0; // Assuming "Select Location" is the first item
                }

                if (isFullDetails)
                {
                    // Patient is already released
                    btnRelease.Visible = false;
                    btnUpdate.Attributes.Add("style", "width: 100%;");
                    ShowReleaseLabels();

                    txtFuneralHome.Text = row["Funeral_Home"].ToString();
                    txtOutEmployeeName.Text = row["Out_Employee_Name"].ToString();
                    txtFuneralHomeEmployee.Text = row["Funeral_Home_Employee"].ToString();
                    txtOutEmployeeID.Text = row["Out_Employee_ID"].ToString();
                }
            }
        }

        protected void clearFuneralFields()
        {
            txtFuneralHome.Text = "";
            txtFuneralHomeEmployee.Text = "";
            txtOutEmployeeName.Text = "";
            txtOutEmployeeID.Text = "";
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (!isFullDetails)
            {
                string patientID = txtPatientID.Text.Trim();
                string funeralHome = txtFuneralHome.Text.Trim().ToString();
                string funeralEmployee = txtFuneralHomeEmployee.Text.Trim().ToString();
                string morgueEmployee = txtOutEmployeeName.Text.Trim().ToString();
                string morgueEmployeeID = txtOutEmployeeID.Text.Trim();
                string locationInMorgue = ddlLocationInMorgue.SelectedItem.Text.ToString();

                // Input Validation
                if (string.IsNullOrWhiteSpace(funeralHome))
                {
                    lblSuccessStatus.Text = "Funeral Home is required.";
                    lblSuccessStatus.Style.Add("border-color", "red");
                    lblSuccessStatus.Visible = true;
                    clearFuneralFields();

                    return;
                }

                if (!int.TryParse(morgueEmployeeID, out int parsedMorgueEmployeeID) )
                {
                    lblSuccessStatus.Text = "Invalid Employee ID";
                    lblSuccessStatus.Style.Add("border-color", "red");
                    lblSuccessStatus.Visible = true;
                    clearFuneralFields();

                    return;
                }
                if (!int.TryParse(purgeSearch(patientID), out int parsedPatientID))
                {
                    lblSuccessStatus.Text = "Invalid Patient ID";
                    lblSuccessStatus.Style.Add("border-color", "red");
                    lblSuccessStatus.Visible = true;
                    clearFuneralFields();
                    return;
                }

                SqlCommand cmdInsertPickedUpInfo = new SqlCommand("dbo.InsertPickedUpInfo", conn);
                cmdInsertPickedUpInfo.CommandType = CommandType.StoredProcedure;

                cmdInsertPickedUpInfo.Parameters.AddWithValue("@Patient_ID", parsedPatientID);
                cmdInsertPickedUpInfo.Parameters.AddWithValue("@Funeral_Home", funeralHome);
                cmdInsertPickedUpInfo.Parameters.AddWithValue("@Funeral_Home_Employee", funeralEmployee);
                cmdInsertPickedUpInfo.Parameters.AddWithValue("@Out_Employee_Name", morgueEmployee);
                cmdInsertPickedUpInfo.Parameters.AddWithValue("@Out_Employee_ID", parsedMorgueEmployeeID);
                cmdInsertPickedUpInfo.Parameters.AddWithValue("@Picked_Up_Date", DateTime.Now.ToString());
                cmdInsertPickedUpInfo.Parameters.AddWithValue("@Location_In_Morgue", locationInMorgue);


                try
                {
                    conn.Open();
                    cmdInsertPickedUpInfo.ExecuteNonQuery();
                    
                }
                catch (SqlException ex)
                {
                    lblSuccessStatus.Text = "An error occurred: " + ex.Message;
                    lblSuccessStatus.Visible = true;
                    clearFuneralFields();

                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            string patientID = txtPatientID.Text.ToString();
            string patientName = txtPatientName.Text.ToString();
            string inEmployeeID = txtEmployeeID.Text.ToString();
            string inEmployeeName = txtEmployeeName.Text.ToString();
            string locationInMorgue = ddlLocationInMorgue.SelectedItem.Text;

                

            if (locationInMorgue.Equals("Select Location") || ddlLocationInMorgue.SelectedValue.Equals("-1"))
            {
                lblSuccessStatus.Visible = true;
                lblSuccessStatus.Style.Add("border-color", "red");
                lblSuccessStatus.Text = "Location Required";
                return;
            }

            if (int.TryParse(patientID, out int parsedPatientID) && int.TryParse(inEmployeeID, out int parsedEmployeeID))
            {
                // Check if the patient has no location in the morgue
             

                SqlCommand cmdUpdatePatientInfo = new SqlCommand("dbo.UpdatePatientInfo", conn);
                cmdUpdatePatientInfo.CommandType = CommandType.StoredProcedure;

                cmdUpdatePatientInfo.Parameters.AddWithValue("@Patient_ID", parsedPatientID);
                cmdUpdatePatientInfo.Parameters.AddWithValue("@Patient_Name", patientName);
                cmdUpdatePatientInfo.Parameters.AddWithValue("@In_Employee_ID", parsedEmployeeID);
                cmdUpdatePatientInfo.Parameters.AddWithValue("@In_Employee_Name", inEmployeeName);
                cmdUpdatePatientInfo.Parameters.AddWithValue("@Location_In_Morgue", locationInMorgue);

                try
                {
                    conn.Open();
                    cmdUpdatePatientInfo.ExecuteScalar();
                    lblSuccessStatus.Style.Add("border-color", "lightseagreen");
                    lblSuccessStatus.Text = "Information Updated!";
                    lblSuccessStatus.Visible = true;
                }
                catch (SqlException ex)
                {
                    lblSuccessStatus.Text = "An error occurred: " + ex.Message;
                    lblSuccessStatus.Visible = true;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    
        protected void Release_Click(object sender, EventArgs e)
        {
            txtOutEmployeeID.Visible = true;
            txtOutEmployeeName.Visible = true;
            txtFuneralHome.Visible = true;
            txtFuneralHomeEmployee.Visible = true;

            lblOutEmployeeID.Visible = true;
            lblOutEmployeeName.Visible = true;
            lblFuneralHome.Visible = true;
            lblFuneralHomeEmployee.Visible = true;

            btnSubmit.Visible = true;
            ShowLabels();
        }



        private void HideLabels()
        {
            txtPatientName.Visible = false;
            txtEmployeeID.Visible = false;
            txtEmployeeName.Visible = false;
            txtFuneralHome.Visible = false;
            txtOutEmployeeName.Visible = false;
            lblSuccessStatus.Visible = false;
            btnUpdate.Visible = false;
            txtFuneralHomeEmployee.Visible = false;
            pCreatedDate.Visible = false;
            txtOutEmployeeID.Visible = false;

            lblPatientName.Visible = false;
            lblEmployeeID.Visible = false;
            lblEmployeeName.Visible = false;
            lblFuneralHome.Visible = false;
            lblFuneralHomeEmployee.Visible = false;
            lblOutEmployeeName.Visible = false;
            lblCreatedDate.Visible = false;
            lblOutEmployeeID.Visible = false;
            ddlLocationInMorgue.Visible = false;
            btnSubmit.Visible = false;
            btnRelease.Visible = false;
            lblLocationInMorgue.Visible = false;
        }

        private void ShowLabels()
        {
            txtPatientName.Visible = true;
            txtEmployeeID.Visible = true;
            txtEmployeeName.Visible = true;
            pCreatedDate.Visible = true;

            btnUpdate.Visible = true;

            lblPatientName.Visible = true;
            lblEmployeeID.Visible = true;
            lblEmployeeName.Visible = true;
            lblCreatedDate.Visible = true;
            hrResult.Visible = true;
            ddlLocationInMorgue.Visible = true;
            btnRelease.Visible = true;
            lblLocationInMorgue.Visible= true;
        }

        private void ShowReleaseLabels()
        {
            txtFuneralHome.Visible = true;
            txtFuneralHomeEmployee.Visible = true;
            txtOutEmployeeName.Visible = true;
            txtOutEmployeeID.Visible = true;

            lblFuneralHome.Visible = true;
            lblFuneralHomeEmployee.Visible = true;
            lblOutEmployeeName.Visible = true;
            lblOutEmployeeID.Visible = true;

            btnSubmit.Visible =true;
        }
    }
}
