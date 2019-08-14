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


public partial class RADIOLOGY_ViewRequstion : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl, F;
    int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    public string id_hist;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b;
    decimal amount = 0;
    decimal amount1 = 0;
    decimal gstamount = 0;
    DateTime DT;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;

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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
      
        con.Close();
    }
    public void FillRequisn()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand COM = new SqlCommand("RADIO_VIEW_REQUISATION", con))
        {
            COM.CommandType = CommandType.StoredProcedure;
            COM.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRID_PAGE";
            COM.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //SqlCommand COM = new SqlCommand("select ID,OPDNO,NAME,DATE from  TBL_RADIOLGYREQ where ID not in(select REQID from TBL_RESULTREQN)  order by ID desc", con);
            // da = new SqlDataAdapter(" SELECT a.*,b.*  FROM PO_TABLE a,PO_ITEM_TABLE b  WHERE a.PONO=b.ID and a.PONO=b.ID and a.PONO='"+lblPOId.Text+"'   ORDER BY A.PONO DESC='" + lblPOId.Text + "'", con);
            //DataTable ds = new DataTable();
            //da.Fill(ds);
            SqlDataAdapter da = new SqlDataAdapter(COM);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            grvrequsion.DataSource = dt1;
            grvrequsion.DataBind();
            dr = COM.ExecuteReader();
            if (dr.Read())
            {
                //txtpono.Text = dr["PONO"].ToString();
                //txtpodate.Text = dr["PODATE"].ToString();         
            }
            dr.Close();
        }
        con.Close();
    }
    protected void grvrequsion_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = grvrequsion.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
       
       // GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        //Session["SID"] = gr.Cells[1].Text;
         //GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
      //  string cell_1_Value = grvrequsion.Rows[gr.RowIndex].Cells[2].Text; 
        // string dtop = gr.Cells[2].Text;
        string dtop=grvrequsion.Rows[e.NewSelectedIndex].Cells[2].Text;

         string Opid = dtop;
         Opid = Opid.Substring(0,2);
        if(Opid=="OP")
        {
            Session["mode"] = "true";
            Session["REQID"] = slno;
            Response.Redirect("~/RADIOLOGY/RequistnResult.aspx");
             
        }
        else if(Opid=="IP")
        {
            Session["mode"] = "false";
            Session["REQID"] = slno;
            Response.Redirect("~/RADIOLOGY/RequistnResult.aspx");
           
        }


        // Session["mode"] = "true";
      //  Session["REQID"] = slno;
      //  Response.Redirect("~/RADIOLOGY/RequisitonResult.aspx");
    }
    protected void grvrequsion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //TextBox tb = (TextBox)GridView1.Rows[CurrentRow.RowIndex].FindControl("TextBox1");
        //string asdfasdf = tb.Text.ToString();
        //Label label1 = (Label)e.Item.FindControl("label1");
       // Label lblopdn=(Label)grvrequsion.Rows[e.new
           

          //Label label1 = (Label)GridViewID.Rows[e.NewEditIndex].FindControl("label1");
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        FillRequisn();
        //grvrequsion.DataBind();
    }
    protected void grvrequsion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand COM = new SqlCommand("RADIO_VIEW_REQUISATION", con))
        {
            COM.CommandType = CommandType.StoredProcedure;
            COM.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRID_PAGE";
            COM.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter Adp = new SqlDataAdapter(COM);
            //SqlDataAdapter Adp = new SqlDataAdapter("select ID,OPDNO,NAME,DATE from  TBL_RADIOLGYREQ where ID not in(select REQID from TBL_RESULTREQN)  order by ID desc", con);
            DataTable dt = new DataTable();
            Adp.Fill(dt);
            grvrequsion.DataSource = dt;
            grvrequsion.PageIndex = e.NewPageIndex;
            grvrequsion.DataKeyNames = new string[] { "ID" };
            grvrequsion.DataBind();
        }
        con.Close();
    }
}