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

public partial class LABORATORY_lab_view_recuisition : System.Web.UI.Page
{
    
    DataMathods OBJ_METHOD = new DataMathods();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
        con.Close();
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");

            DataSet DS = OBJ_METHOD.Get_DataSet("LAB_VIEW_REQUISATION", false, true, SQL_PARAMS);
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
        //using (SqlCommand COM = new SqlCommand("LAB_VIEW_REQUISATION", con))
        //{
        //    COM.CommandType = CommandType.StoredProcedure;
        //    COM.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
        //    //SqlCommand COM = new SqlCommand("select ID,OPDNO,NAME from  TBLRECLAB  order by ID asc", con);        
        //    SqlDataAdapter da = new SqlDataAdapter(COM);
        //    DataTable dt1 = new DataTable();
        //    da.Fill(dt1);
        //    Session["labid"] = dt1.Rows[0]["ID"].ToString();
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
            Session["labid"] = slno;
            Response.Redirect("~/LABORATORY/lab_Optestresult.aspx");
        }
        else if (Opid == "IP")
        {
            Session["mode"] = "false";
            Session["labid"] = slno;
            Response.Redirect("~/LABORATORY/lab_Optestresult.aspx");
        }
    }
}