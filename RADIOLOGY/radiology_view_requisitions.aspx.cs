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

public partial class RADIOLOGY_radiology_view_requisitions : System.Web.UI.Page
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

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_GRID_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("RADIO_VIEW_REQUISATION", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                grvrequsion.DataSource = DS;
                grvrequsion.DataBind();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //using (SqlCommand COM = new SqlCommand("RADIO_VIEW_REQUISATION", con))
        //{
        //    COM.CommandType = CommandType.StoredProcedure;
        //    COM.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRID_PAGE";
        //    COM.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    //SqlCommand COM = new SqlCommand("select ID,OPDNO,NAME,DATE from  TBL_RADIOLGYREQ where ID not in(select REQID from TBL_RESULTREQN)  order by ID desc", con);
        //    // da = new SqlDataAdapter(" SELECT a.*,b.*  FROM PO_TABLE a,PO_ITEM_TABLE b  WHERE a.PONO=b.ID and a.PONO=b.ID and a.PONO='"+lblPOId.Text+"'   ORDER BY A.PONO DESC='" + lblPOId.Text + "'", con);
        //    //DataTable ds = new DataTable();
        //    //da.Fill(ds);
        //    SqlDataAdapter da = new SqlDataAdapter(COM);
        //    DataTable dt1 = new DataTable();
        //    da.Fill(dt1);
        //    grvrequsion.DataSource = dt1;
        //    grvrequsion.DataBind();
        //    dr = COM.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        //txtpono.Text = dr["PONO"].ToString();
        //        //txtpodate.Text = dr["PODATE"].ToString();         
        //    }
        //    dr.Close();
        //}
        //con.Close();
    }
    protected void grvrequsion_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = grvrequsion.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        
        string dtop = grvrequsion.Rows[e.NewSelectedIndex].Cells[2].Text;

        string Opid = dtop;
        Opid = Opid.Substring(0, 2);
        if (Opid == "OP")
        {
            Session["mode"] = "true";
            Session["REQID"] = slno;
            Response.Redirect("~/RADIOLOGY/Radio_RequistnResult.aspx");

        }
        else if (Opid == "IP")
        {
            Session["mode"] = "false";
            Session["REQID"] = slno;
            Response.Redirect("~/RADIOLOGY/Radio_RequistnResult.aspx");

        }
    }
    protected void grvrequsion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        FillRequisn();
    }
    protected void grvrequsion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_GRID_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("RADIO_VIEW_REQUISATION", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                grvrequsion.DataSource = DS;
                grvrequsion.PageIndex = e.NewPageIndex;
                grvrequsion.DataBind();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //using (SqlCommand COM = new SqlCommand("RADIO_VIEW_REQUISATION", con))
        //{
        //    COM.CommandType = CommandType.StoredProcedure;
        //    COM.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRID_PAGE";
        //    COM.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    SqlDataAdapter Adp = new SqlDataAdapter(COM);
        //    DataTable dt = new DataTable();
        //    Adp.Fill(dt);
        //    grvrequsion.DataSource = dt;
        //    grvrequsion.PageIndex = e.NewPageIndex;
        //    grvrequsion.DataKeyNames = new string[] { "ID" };
        //    grvrequsion.DataBind();
        //}
        //con.Close();
    }
}