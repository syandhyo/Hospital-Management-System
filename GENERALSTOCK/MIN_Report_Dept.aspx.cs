using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
public partial class GENERALSTOCK_GRN_Report_Dept : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataReader dr;
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
            lbluid.Text = Session["UID"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();

            if (!IsPostBack)
            {
                binddata();
            }
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
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
            Session["MINNO"] = gr.Cells[1].Text;
            //var UB = gr.Cells[1].Text;
            //string name = Session["IPDNO"].Text.Trim();
            //Session["name"] = name;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBox1.Text == "")
            {
                string message = "alert('Please!! Enter From Dates..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (TextBox2.Text == "")
            {
                string message = "alert('Please!! Enter To Dates..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(10),A.MINDATE,103) AS MINDATE,A.ID,B.DeptName FROM DEPT_MIN_TABLE A,tblDepartment B WHERE B.id=A.ISSUEDTO AND A.MINDATE BETWEEN '" + Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd") + "'", con);
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_MATISSUERPT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@FDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
                SqlDataAdapter da = new SqlDataAdapter(cmd);               
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    // grSearchPatient.SelectedIndex = 0;  
                    grSearchPatient.DataSource = dt;
                    grSearchPatient.DataBind();
                }
                else
                {
                    string message = "alert('No Record Found !')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}