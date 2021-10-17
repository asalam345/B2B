using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
	public partial class _Default : Page
	{
        string constr = null;
        bool isDateFormated = false;
        bool isTimeFormated = false;

        protected void Page_Load(object sender, EventArgs e)
		{
            constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            this.gvResult.Columns[0].Visible = false;
            Page.GetPostBackEventReference(btnSearch);

            System.Globalization.CultureInfo jpCInfo = new System.Globalization.CultureInfo("ja-JP");
            System.Globalization.CultureInfo defaultCInfo = new System.Globalization.CultureInfo("en-US");
         

            if (!this.IsPostBack)
			{
				this.BindGrid();
            }
		}
        protected void CustomValidatorDate_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            DateTime d;
            e.IsValid = DateTime.TryParseExact(e.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
            isDateFormated = e.IsValid;
        }
        protected void CustomValidatorTime_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            DateTime d;
            e.IsValid = DateTime.TryParseExact(e.Value, new[] { "HH:mm", "H:mm" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
            isTimeFormated = e.IsValid;
        }
        private void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM temp_U_Data_1"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            this.gvResult.Columns[7].Visible = true;
                            this.gvResult.Columns[8].Visible = false;
                            if (dt.Rows.Count == 0)
							{
                                //dt = new DataTable();
                                //dt.Columns.Add(new DataColumn("col1"));
                                //dt.Columns.Add(new DataColumn("col2"));
                                //dt.Columns.Add(new DataColumn("col3"));
                                this.gvResult.Columns[7].Visible = false;
                                this.gvResult.Columns[8].Visible = true;
                                for (int i = 0; i < 10; i++)
                                {
                                    DataRow dr = dt.NewRow();
                                    dt.Rows.Add(dr);
                                }
                            }
                            gvResult.DataSource = dt;
                            gvResult.DataBind();
                        }
                    }
                }
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResult.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        private void reset()
		{
            txtTitle.Text = "";
            txtTime.Text = "";
            txtAddress.Text = "";
            txtRemarks.Text = "";
            txtDate.Text = "";
            types.Text = "";
            txtSearch.Text = "";
		}

		protected void btnFormSubmit_Click(object sender, EventArgs e)
		{
            if (string.IsNullOrEmpty(txtDate.Text)
                || string.IsNullOrEmpty(txtTime.Text)
                || string.IsNullOrEmpty(txtTitle.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Title, Date, Time fields are mendetory');", true);
                return;
            }
            if (!isDateFormated || !isTimeFormated) return;
            using (SqlConnection con = new SqlConnection(constr))
            {
                
                string sql = "Insert INTO temp_U_Data_1(Title, Address, Types, Date, Time, Remarks) VALUES('" + txtTitle.Text + "', '" + txtAddress.Text + "','" + types.Text + "', '" + txtDate.Text + "','" + txtTime.Text + "' , '" + txtRemarks.Text + "')";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.InsertCommand = cmd;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        this.BindGrid();
                        reset();
                    }
                }
            }
        }
        protected void DateChange(object sender, EventArgs e)
        {
            //txtDate.Text = Calendar1.SelectedDate.ToShortDateString() + '.';
        }
        
        public void gvResult_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvResult.Rows[e.RowIndex];
            //var rowId = gvResult.Rows[e.RowIndex].Cells[1].Text;
            string rowId = (row.FindControl("gv_lblId") as Label).Text;
            long id = Convert.ToInt64(rowId);
            DeleteRow(id);
        }
        
        private void DeleteRow(long id)
		{
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "Delete FROM temp_U_data_1 where Id = " + id;
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.InsertCommand = cmd;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        this.BindGrid();
                    }
                }
            }
        }

        private DataTable getRowsFromGV()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] { 
                        new DataColumn("Types", typeof(string)),
                        new DataColumn("Title", typeof(string)),
                        new DataColumn("Address", typeof(string)),
                        new DataColumn("Date", typeof(DateTime)),
                        new DataColumn("Time", typeof(string)),
                        new DataColumn("Remarks", typeof(string)) });
            foreach (GridViewRow row in gvResult.Rows)
            {
                //CheckBox chkbox = row.FindControl("chkbox") as CheckBox;
                string title = (row.FindControl("gv_lblTitle") as Label).Text;
                string address = (row.FindControl("gv_lblAddress") as Label).Text;
                string types = (row.FindControl("gv_lblTypes") as Label).Text;
                string date = (row.FindControl("gv_lblDate") as Label).Text;
                string time = (row.FindControl("gv_lblTime") as Label).Text;
                string remarks = (row.FindControl("gv_lblRemarks") as Label).Text;
                
                dt.Rows.Add(types, title, address, date, time, remarks);
            }

            return dt;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
		{
            DataTable dt = getRowsFromGV();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "U_data_1";

                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Types", "Types");
                        sqlBulkCopy.ColumnMappings.Add("Title", "Title");
                        sqlBulkCopy.ColumnMappings.Add("Address", "Address");
                        sqlBulkCopy.ColumnMappings.Add("Date", "Date");
                        sqlBulkCopy.ColumnMappings.Add("Time", "Time");
                        sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Successfully added');", true);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchString = txtSearch.Text;
            search(searchString);
        }
        private void search(string searchString)
		{
            using (SqlConnection con = new SqlConnection(constr))
            {
                //string sql = "SELECT * FROM temp_U_Data_1 u INNER JOIN Type t ON u.TypeId = t.Id where title like %% Or address like %% Or remarks like %% Or value like %%";
                string sql = "SELECT * FROM temp_U_Data_1";
                if (!string.IsNullOrEmpty(searchString))
                 sql = "SELECT * FROM temp_U_Data_1 u where title like '%"+ searchString + "%' Or address like '%" + searchString + "%' Or remarks like'%" + searchString + "%' Or Types like'%" + searchString + "%'";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            this.gvResult.Columns[7].Visible = true;
                            this.gvResult.Columns[8].Visible = false;
                            if (dt.Rows.Count == 0)
                            {
                                this.gvResult.Columns[7].Visible = false;
                                this.gvResult.Columns[8].Visible = true;
                                for (int i = 0; i < 10; i++)
                                {
                                    DataRow dr = dt.NewRow();
                                    dt.Rows.Add(dr);
                                }
                            }
                            gvResult.DataSource = dt;
                            gvResult.DataBind();
                        }
                    }
                }
            }
        }

		protected void txtSearch_TextChanged(object sender, EventArgs e)
		{
            string searchString = txtSearch.Text;
            search(searchString);
        }
	}
}

//
//