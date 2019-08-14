using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

public partial class RECEPTION_reception_discount_report : System.Web.UI.Page
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

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (txtfdate.Text == "")
            {
                string message = "alert('*Please!!Enter Both dates')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfdate.Focus();
                return;
            }
            else if (txttdate.Text == "")
            {
                string message = "alert('*Please!!Enter Both Dates..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txttdate.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfdate.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@LDATE", SqlDbType.DateTime, 0, txttdate.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("RECP_REPORT_DISCOUNT", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Button1.Visible = true;
                GridView1.DataSource = DS;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            else
            {
                Button1.Visible = false;
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
         
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {

        if (GridView1.Rows.Count > 0)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DiscountReport.xls"));
            Response.ContentType = "application/ms-excel";

            //Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;

            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfdate.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@LDATE", SqlDbType.DateTime, 0, txttdate.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("RECP_REPORT_DISCOUNT", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS;
                GridView1.DataBind();
            }

            //Change the Header Row back to white color
            GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
            {
                GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            GridView1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfdate.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@LDATE", SqlDbType.DateTime, 0, txttdate.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("RECP_REPORT_DISCOUNT", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Button1.Visible = true;
                GridView1.DataSource = DS;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}