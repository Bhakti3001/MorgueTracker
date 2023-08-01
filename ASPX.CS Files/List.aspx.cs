using System;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MorgueTracker3
{
    public partial class List : Page
    {
        private string selectAll = "SELECT Patient_Name, Patient_ID, In_Employee_Name, In_Employee_ID, Created_Date, Out_Employee_Name, Out_Employee_ID, Location_In_Morgue, Funeral_Home, Funeral_Home_Employee, Picked_Up_Date ";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        protected void SearchByDate_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        // changes table based on the checkbox status, provided dates, and current date
        private void BindGridView()
        {
            bool isPickedUp = PickUpCheck.Checked;
            string dtStartDate = txtStartDate.Text;
            string dtEndDate = txtEndDate.Text;

            string query = BuildQuery(isPickedUp, dtStartDate, dtEndDate);
            string connectionString = ConfigurationManager.ConnectionStrings["MorgueTrackerConn"].ConnectionString;

            // searches database 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
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

                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    gvList.DataSource = dt;
                    gvList.DataBind();

                    lblStatus.Visible = dt.Rows.Count == 0;
                    if (dt.Rows.Count == 0)
                    {
                        lblStatus.Text = "No Data Found";
                        lblStatus.Attributes.Add("style", "border-color: red;");
                    }
                }
            }
        }

        // query changes based on dates searched by and the picked up checkbox
        private string BuildQuery(bool isPickedUp, string startDate, string endDate)
        {
            string query = selectAll + "FROM MorgueTracker ";

            // if there is a date selected (either start or end date)
            if (!string.IsNullOrEmpty(startDate) || !string.IsNullOrEmpty(endDate))
            {
                // if only start date is selected
                if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
                {
                    query += "WHERE CAST(Created_Date AS DATE) >= @startDate ";
                }
                // if only end date is selected
                else if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                {
                    query += "WHERE CAST(Created_Date AS DATE) <= @endDate ";
                }
                // if both dates are selected
                else
                {
                    query += "WHERE CAST(Created_Date AS DATE) BETWEEN @startDate AND @endDate ";
                }
                // changes query based on if the picked up checkbox is checked or not
                query += isPickedUp ? "AND Funeral_Home IS NOT NULL " : "AND Funeral_Home IS NULL ";
            }

            // if there is no date selected
            else
            {
                query += isPickedUp ? "WHERE Funeral_Home IS NOT NULL " : "WHERE Funeral_Home IS NULL ";
            }

            // always put in descending order
            query += "ORDER BY Created_Date DESC";

            return query;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            int pageSize = gvList.PageSize;
            int pageIndex = gvList.PageIndex;

            // turns allow paging off so the exported file has data of all pages
            gvList.AllowPaging = false;
            BindGridView();

            string dateTimeWithHyphens = DateTime.Now.ToString("MM-dd-yyyy");

            if (gvList.Rows.Count == 0)
            {
                lblStatus.Text = "Cannot export empty dataset.";
                lblStatus.Visible = true;
            }
            else
            {
                ExportGridToExcel(dateTimeWithHyphens);
            }

            // turns paging back on for the viewable table 
            gvList.AllowPaging = true;
            gvList.PageSize = pageSize;
            gvList.PageIndex = pageIndex;
            BindGridView();
        }

        // exports the GridView data to an Excel file
        private void ExportGridToExcel(string dateTimeWithHyphens)
        {
            // determines whether the table is showing picked up or not picked up based on the checkbox value
            string pickedUp = PickUpCheck.Checked ? "Picked_Up" : "Not_Picked_Up";

            // gets the file name for the export based on the "pickedUp" status and current date
            string fileName = GetExportFileName(pickedUp, dateTimeWithHyphens);

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                StyleGridForExport(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        // generates file name based on dates searched by and the picked up checkbox, as well as current date
        private string GetExportFileName(string pickedUp, string dateTimeWithHyphens)
        {
            string startDateText = txtStartDate.Text;
            string endDateText = txtEndDate.Text;

            // if no dates given 
            if (string.IsNullOrEmpty(startDateText) && string.IsNullOrEmpty(endDateText))
            {
                return $"{pickedUp}_MorguePatients_as_of({dateTimeWithHyphens}).xls";
            }
            // if no end date is given
            else if (string.IsNullOrEmpty(endDateText))
            {
                // formats start date to MM-dd-yyyy
                DateTime startDate = DateTime.ParseExact(startDateText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string startFormattedDateString = startDate.ToString("MM-dd-yyyy");

                return $"{pickedUp}_MorguePatients({startFormattedDateString}_to_{dateTimeWithHyphens}).xls";
            }
            // if no start date is given
            else if (string.IsNullOrEmpty(startDateText))
            {
                // formats end date to MM-dd-yyyy
                DateTime endDate = DateTime.ParseExact(endDateText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string endFormattedDateString = endDate.ToString("MM-dd-yyyy");

                return $"{pickedUp}_MorguePatients_until({endFormattedDateString}).xls";
            }
            // if both dates are given
            else
            {
                // formats dates from yyyy-MM-dd to MM-dd-yyyy
                DateTime startDate = DateTime.ParseExact(startDateText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string startFormattedDateString = startDate.ToString("MM-dd-yyyy");
                DateTime endDate = DateTime.ParseExact(endDateText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string endFormattedDateString = endDate.ToString("MM-dd-yyyy");

                return $"{pickedUp}_MorguePatients({startFormattedDateString}_to_{endFormattedDateString}).xls";
            }
        }

        private void StyleGridForExport(HtmlTextWriter hw)
        {
            // changes header color on exported file
            gvList.HeaderRow.Style.Add("background-color", "#edfbfb");

            foreach (GridViewRow row in gvList.Rows)
            {
                row.BackColor = System.Drawing.Color.White;

                // makes text left-aligned
                foreach (TableCell cell in row.Cells)
                {
                    cell.CssClass = "textmode";
                    cell.Attributes.Add("style", "mso-number-format:\\@");
                }
            }

            gvList.RenderControl(hw);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time */
        }
    }
}