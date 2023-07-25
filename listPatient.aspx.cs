using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Morgue_Tracker
{
    public partial class listPatient : System.Web.UI.Page
    {
        private SqlConnection conn;
        private string selectAll = "SELECT Patient_Name, Patient_ID, In_Employee_Name, In_Employee_ID, Created_Date, Out_Employee_Name, Out_Employee_ID, Location_In_Morgue, Funeral_Home, Funeral_Home_Employee, Picked_Up_Date ";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn = CreateConnection();
                BindGridView();
            }
        }
        protected void SearchByDate_Click(object sender, EventArgs e)
        {



            string dtStartDate = txtStartDate.Text;
            string dtEndDate = txtEndDate.Text;



            string query = selectAll + "FROM MorgueTrackerTest ";
            // Check if the start date or end date is empty
            if (!string.IsNullOrEmpty(dtStartDate) || !string.IsNullOrEmpty(dtEndDate))
            {
                // If only the start date is provided
                if (!string.IsNullOrEmpty(dtStartDate) && string.IsNullOrEmpty(dtEndDate))
                {
                    query += "WHERE CAST(Created_Date AS DATE) >= @startDate ";
                }
                // If only the end date is provided
                else if (string.IsNullOrEmpty(dtStartDate) && !string.IsNullOrEmpty(dtEndDate))
                {
                    query += "WHERE CAST(Created_Date AS DATE) <= @endDate ";
                }
                // If both the start date and the end date are provided
                else
                {
                    query += "WHERE CAST(Created_Date AS DATE) BETWEEN @startDate AND @endDate ";
                }

                if (PickUpCheck.Checked == false)
                {
                    query += "AND Funeral_Home IS NULL ";
                }
                else
                {
                    query += "AND Funeral_Home IS NOT NULL ";
                }
            }



            else if (PickUpCheck.Checked == false)
            {
                query += " WHERE Funeral_Home IS NULL ";
            }
            else
            {
                query += " WHERE Funeral_Home IS NOT NULL ";
            }



            query += "ORDER BY Patient_Name ASC";




            conn = CreateConnection();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (!string.IsNullOrEmpty(dtStartDate))
                {
                    cmd.Parameters.AddWithValue("@startDate", dtStartDate);
                }
                if (!string.IsNullOrEmpty(dtEndDate))
                {
                    cmd.Parameters.AddWithValue("@endDate", dtEndDate);
                }



                conn.Open();



                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    gvList.DataSource = reader;
                    gvList.DataBind();
                }



                conn.Close();

                if (gvList.Rows.Count == 0)
                {
                    string successMessage = "No Data Found";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{successMessage}');", true);
                }
            }
        }


        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            BindGridView();
        }


        protected void PickUp_Check(object sender, EventArgs e)
        {
            string query;
            if (PickUpCheck.Checked == false)
            {
                query = selectAll + "FROM MorgueTrackerTest WHERE Funeral_Home IS NULL ORDER BY Patient_Name ASC";
            }
            else
            {
                query = selectAll + "FROM MorgueTrackerTest WHERE Funeral_Home IS NOT NULL ORDER BY Patient_Name ASC";
            }



            ExecuteQuery(query);
        }
        // Insert on pick up



        // Search "locationInnMorgue"



        //ADD DROP DOWN FOR LOCATIONS IN SEACH PATIENT
        //NEED WAY TO SORT BY DATE
        // On search, have way to verify that the patient is going to the right place
        // input employee ID
        // two person validation
        //search split into two funcitonalitys, viewing record, and exporting






        private void BindGridView()
        {
            string query = selectAll + "FROM MorgueTrackerTest WHERE Funeral_Home IS NULL ORDER BY Patient_Name ASC";
            ExecuteQuery(query);
        }





        private void ExecuteQuery(string query)
        {
            conn = CreateConnection();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();



                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    gvList.DataSource = reader;
                    gvList.DataBind();
                }



            }
            conn.Close();
        }

        protected void Date_TextChanged(object sender, EventArgs e)
        {
            // Perform search here
            SearchByDate_Click(sender, e);
        }


        private SqlConnection CreateConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["MorgueTrackerConn"].ConnectionString);
        }










        // string format date

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvList.Rows.Count == 0)
            {
                string successMessage = "Cannot export empty dataset";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{successMessage}');", true);
                return;
            }
            else
            {
                DateTime time = DateTime.Now;
                Response.Clear();
                Response.Buffer = true;

                String pickedUp = "";

                if (PickUpCheck.Checked == false)
                {
                    pickedUp = "Not_PickedUp";
                }
                else
                {
                    pickedUp = "PickedUp";
                }



                if (txtStartDate.Text.IsNullOrWhiteSpace() && txtEndDate.Text.IsNullOrWhiteSpace())
                {
                    string dateTimeWithHyphens = DateTime.Now.ToString("MM-dd-yyyy");

                    Response.AddHeader("content-disposition", "attachment;filename=" + pickedUp + "MorguePatients_as_of(" + dateTimeWithHyphens + ").xls");
                }
                else if (txtEndDate.Text.IsNullOrWhiteSpace())
                {
                    // Original date string in "yyyy-mm-dd" format
                    string startOriginalDateString = txtStartDate.Text;

                    // Parse the original date string into a DateTime object
                    DateTime startDate = DateTime.ParseExact(startOriginalDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    // Format the DateTime object into the "MM-dd-yyyy" format
                    string startFormattedDateString = startDate.ToString("MM-dd-yyyy");

                    Response.AddHeader("content-disposition", "attachment;filename=" + pickedUp + "MorguePatients_startingFrom(" + startFormattedDateString + ").xls");
                }
                else if (txtStartDate.Text.IsNullOrWhiteSpace())
                {
                    // Original date string in "yyyy-mm-dd" format
                    string endOriginalDateString = txtEndDate.Text;

                    // Parse the original date string into a DateTime object
                    DateTime endDate = DateTime.ParseExact(endOriginalDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    // Format the DateTime object into the "MM-dd-yyyy" format
                    string endFormattedDateString = endDate.ToString("MM-dd-yyyy");
                    Response.AddHeader("content-disposition", "attachment;filename=" + pickedUp + "MorguePatients_until(" + endFormattedDateString + ").xls");
                }
                else
                {
                    // Original date string in "yyyy-mm-dd" format
                    string startOriginalDateString = txtStartDate.Text;

                    // Parse the original date string into a DateTime object
                    DateTime startDate = DateTime.ParseExact(startOriginalDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    // Format the DateTime object into the "MM-dd-yyyy" format
                    string startFormattedDateString = startDate.ToString("MM-dd-yyyy");

                    // Original date string in "yyyy-mm-dd" format
                    string endOriginalDateString = txtEndDate.Text;

                    // Parse the original date string into a DateTime object
                    DateTime endDate = DateTime.ParseExact(endOriginalDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    // Format the DateTime object into the "MM-dd-yyyy" format
                    string endFormattedDateString = endDate.ToString("MM-dd-yyyy");

                    Response.AddHeader("content-disposition", "attachment;filename=" + pickedUp + "MorguePatients(" + startFormattedDateString + " to " + endFormattedDateString + ").xls");
                }
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {

                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    // Change the Header Row back to white color
                    gvList.HeaderRow.Style.Add("background-color", "#FFFFFF");

                    // Apply styles to each Header cell
                    foreach (TableCell cell in gvList.HeaderRow.Cells)
                    {
                        cell.Attributes.Add("background-color", "#edfbfb;");
                    }

                    foreach (GridViewRow row in gvList.Rows)
                    {
                        // Apply styles to each row
                        row.BackColor = System.Drawing.Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            cell.CssClass = "textmode";
                            cell.Attributes.Add("style", "mso-number-format:\\@");
                        }
                    }

                    gvList.RenderControl(hw);

                    // Write the output to the response
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}