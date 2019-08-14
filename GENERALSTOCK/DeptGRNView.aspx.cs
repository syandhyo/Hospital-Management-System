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

public partial class GENERALSTOCK_DeptGRNView : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    GridViewRow gr;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
            binddata();

            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
   
    public void binddata()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataReader dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
            //SqlDataAdapter da = new SqlDataAdapter("SELECT a.ID,a.MRNO,a.MINDATE,b.DeptName FROM MIN_TABLE a,tblDepartment b where a.ISSUEDTO=b.id and a.MRNO not in(select MRNO from DEPTGRN_TABLE) ORDER BY a.slno DESC", con);
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_MATRECVIEW", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";               
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                //if(dt.Rows.Count>0 && dt.Rows.Count==null)
                //{ DTSTOCK_MATRECVIEW
                // grdDeptgrn.SelectedIndex = 0;
                grdDeptgrn.DataSource = dt;
                grdDeptgrn.DataKeyNames = new string[] { "ID" };
                grdDeptgrn.DataBind();

            }
            //}
            //else
            //{
            //    Response.Write(@"<script language='javascript'>alert('No Record Found !')</script>");
            //    grdDeptgrn.DataSource = dt;
            //    grdDeptgrn.DataKeyNames = new string[] { "ID" };
            //    grdDeptgrn.DataBind();
            //    string message = "alert('*Please select Department.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //}

            //GridView2.Columns[04].Visible = false;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grdDeptgrn_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grdDeptgrn.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            Session["MRINDID"] = slno;
            Response.Redirect("~/GENERALSTOCK/DepartmentGRN.aspx");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}