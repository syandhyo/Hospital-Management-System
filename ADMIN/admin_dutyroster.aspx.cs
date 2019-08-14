using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;


public partial class ADMIN_admin_dutyroster : System.Web.UI.Page
{
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["out"] == "INACTIVE")
        {
            Response.Redirect("~/index.aspx");
        }
        Response.Buffer = true;

        Response.CacheControl = "no-cache";
        if (Session["NAME"] == null)
        {
            Response.Redirect("~/index.aspx");
        }
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {

            binddata();

            DataSet Dt = OBJ_METHOD.Get_DataSet("select * from tblStaff", false, false);
            dropstaff.DataSource = Dt;
            dropstaff.DataTextField = "Sname";
            dropstaff.DataValueField = "id";
            dropstaff.DataBind();
            dropstaff.Items.Insert(0, new ListItem("Please Select", "0"));


            DataSet Dt1 = OBJ_METHOD.Get_DataSet("select Distinct NAME,ID from WARD_TABLE", false, false);
            dropward.DataSource = Dt1;
            dropward.DataTextField = "NAME";
            dropward.DataValueField = "ID";
            dropward.DataBind();
            dropward.Items.Insert(0, new ListItem("Please Select", "0"));

        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[6].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this staff info ?');";
        }
    }
    public void binddata()
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }


            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());

            DataSet Ds = OBJ_METHOD.Get_DataSet("ADMIN_Duty_RosterSelect", false, true, SQL_PARAMS1);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
            ErrorLog.Write(ex);

        }

       
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
           // SqlCommand cm = new SqlCommand("delete from tblDutyRoster where id='" + slno + "'", con);
           // cm.ExecuteNonQuery();
            DataSet Dt = OBJ_METHOD.Get_DataSet("delete from tblDutyRoster where id='" + slno + "'", false, false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                binddata();
            }
           // binddata();
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());

            DataSet Ds = OBJ_METHOD.Get_DataSet("ADMIN_Duty_RosterSelect", false, true, SQL_PARAMS1);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
            
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            DataSet Dt = OBJ_METHOD.Get_DataSet("SELECT id,FDate,TDate,Shift,StaffId,WardName from tblDutyRoster where id='" + slno + "'", false, false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = Dt.Tables[0].Rows[0]["ID"].ToString();
                txtfdate.Text = Convert.ToDateTime(Dt.Tables[0].Rows[0]["FDate"]).ToString("yyyy-MM-dd");
                txttdate.Text = Convert.ToDateTime(Dt.Tables[0].Rows[0]["TDate"]).ToString("yyyy-MM-dd");
                dropshift.Text = Dt.Tables[0].Rows[0]["Shift"].ToString();
                dropstaff.SelectedValue = Dt.Tables[0].Rows[0]["StaffId"].ToString();
                dropward.SelectedValue = Dt.Tables[0].Rows[0]["WardName"].ToString();
            }
           
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtfdate.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txttdate.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropshift.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropstaff.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropward.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@Shift", SqlDbType.VarChar, 500, dropshift.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@StaffId", SqlDbType.Int, 0, dropstaff.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@WardName", SqlDbType.VarChar, 500, dropward.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@FDate", SqlDbType.DateTime, 0, Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@TDate", SqlDbType.DateTime, 0, Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("sp_DutyRoster", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
            }

            message1 = "alert('" + OBJ_METHOD._objOut + "')";
           
        }
        catch (Exception ex)
        {

            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    public void clearcontrol()
    {
        txtfdate.Text = "";
        txttdate.Text = "";
        dropshift.SelectedIndex = 0;
        dropstaff.SelectedIndex = 0;
        dropward.SelectedIndex = 0;
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtfdate.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txttdate.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropshift.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropstaff.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropward.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }


            SqlParameter[] SQL_PARAMS = new SqlParameter[8];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@Shift", SqlDbType.VarChar, 500, dropshift.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@StaffId", SqlDbType.Int, 0, dropstaff.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@WardName", SqlDbType.VarChar, 500, dropward.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@FDate", SqlDbType.DateTime, 0, Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@TDate", SqlDbType.DateTime, 0, Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@id", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("sp_DutyRoster", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                btnupdate.Visible = false;
                btncreate.Visible = true;
            }

            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            
        }
        catch (Exception ex)
        {

            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
       
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        clearcontrol();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //string s = "../ExcelFillData/" + flu_Image.FileName;

        //flu_Image.SaveAs(Server.MapPath(s));
        //string message = "alert('Excel Uploaded Sucessfully.')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //============================================================================================
        if (flu_Image.HasFile)
        {
            string FileName = Path.GetFileName(flu_Image.PostedFile.FileName);
            string Extension = Path.GetExtension(flu_Image.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string FilePath = Server.MapPath(FolderPath + FileName);
            flu_Image.SaveAs(FilePath);
            Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
        }
        grd_temp.Visible = true;
        GridView1.Visible = false;


    }
    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        try
        {
            string conStr = "";

            switch (Extension)
            {

                case ".xls": //Excel 97-03

                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]

                             .ConnectionString;
                    break;

                case ".xlsx": //Excel 07

                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]

                              .ConnectionString;
                    break;

            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();
            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;

            oda.Fill(dt);

            connExcel.Close();
            //Bind Data to GridView

            grd_temp.Caption = Path.GetFileName(FilePath);

            grd_temp.DataSource = dt;

            grd_temp.DataBind();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            foreach (GridViewRow row in grd_temp.Rows)
            {
                var StaffId = row.FindControl("lblStaffId") as Label;
                var WardName = row.FindControl("lblWardName") as Label;
                var Shift = row.FindControl("lblShift") as Label;
                var FDate = row.FindControl("lblFDate") as Label;
                var TDate = row.FindControl("lblTDate") as Label;

                // var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
                using (SqlCommand cm = new SqlCommand("sp_DutyRoster", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    cm.Parameters.Add("@StaffId", SqlDbType.Int).Value = StaffId.Text.ToString();
                    cm.Parameters.Add("@WardName", SqlDbType.VarChar).Value = WardName.Text.ToString();
                    cm.Parameters.Add("@Shift", SqlDbType.VarChar).Value = Shift.Text.ToString();
                    cm.Parameters.Add("@FDate", SqlDbType.DateTime).Value = FDate.Text.ToString();
                    cm.Parameters.Add("@TDate", SqlDbType.DateTime).Value = TDate.Text.ToString();
                    cm.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text.ToString();
                    cm.Parameters.Add("@id", SqlDbType.Int).Value = 1;
                    cm.ExecuteNonQuery();
                }
            }

            //File.Delete(Server.MapPath("../ExcelFillData/" + flu_Image));
            //FileInfo fInfoEvent;
            //fInfoEvent = new FileInfo(fInfoEvent);
            //fInfoEvent.Delete();
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }

    }
    protected void btn_update_Click(object sender, EventArgs e)
    {

    }
}