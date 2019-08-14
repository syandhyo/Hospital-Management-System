using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web.Services;

public partial class RADIOLOGY_radiology_requisation_reports : System.Web.UI.Page
{
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
            FillRequisn();
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
    public void FillRequisn()
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_GRID_REPORT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RADIO_VIEW_REQUISATION", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                grvrequsion.DataSource = Ds2;
                grvrequsion.DataKeyNames = new string[] { "ID" };
                grvrequsion.DataBind();
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    protected void grvrequsion_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = grvrequsion.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        Session["RADID"] = slno;
        string dtop = grvrequsion.Rows[e.NewSelectedIndex].Cells[2].Text;

        string Opid = dtop;
        Opid = Opid.Substring(0, 2);
        if (Opid == "OP")
        {
            Session["mode"] = "true";
            Response.Redirect("~/RADIOLOGY/Radio_Resultrequbill.aspx");

        }
        else if (Opid == "IP")
        {
            Session["mode"] = "false";
            Response.Redirect("~/RADIOLOGY/Radio_Resultrequbill.aspx");

        }
    }
    protected void grvrequsion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_GRID_REPORT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RADIO_VIEW_REQUISATION", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                grvrequsion.DataSource = Ds2;
                grvrequsion.PageIndex = e.NewPageIndex;
                grvrequsion.DataKeyNames = new string[] { "ID" };
                grvrequsion.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        FillRequisn();
    }
}