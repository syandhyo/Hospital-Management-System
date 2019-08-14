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

public partial class RECEPTION_SearchPatientData : System.Web.UI.Page
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtname.Text == "")
            {
                string message1 = "alert('Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                return;
            }
            else
            {
                using (SqlCommand cm = new SqlCommand("RECP_SEARCH_PATIENT", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "insert_advnce";
                    cm.Parameters.Add("@name", SqlDbType.VarChar).Value = txtname.Text;

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cm);

                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        string message = "alert(' No Record Found')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                    else
                    {
                        //SqlDataAdapter da = new SqlDataAdapter("select A.ID AS OPDNO,A.VN AS IPDNO,A.NAME AS NAME,A.PHONE,A.PACNAME AS PACKAGE,A.DATE AS DATEOFADMISSION,B.DATE AS DATEOFDISCHARGE from  ADMISSION_TABLE A,DISCHARGE_TABLE B where  A.VN=B.PID AND A.NAME LIKE '" + txtname.Text + "%'", con);
                        //DataTable dt = new DataTable();
                        //da.Fill(dt);
                        //grSearchPatient.SelectedIndex = 0;
                        grSearchPatient.DataSource = dt;
                        grSearchPatient.DataBind();
                        con.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        {
           // throw ex;
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
            Session["IPDNO"] = gr.Cells[1].Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //var UB = gr.Cells[1].Text;
        //string name = Session["IPDNO"].Text.Trim();
        //Session["name"] = name;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
           
            using (SqlCommand cm = new SqlCommand("RECP_SEARCH_PATIENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "advnce";
                cm.Parameters.Add("@name", SqlDbType.VarChar).Value = "";

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cm);

                da.Fill(dt);
                //SqlDataAdapter da = new SqlDataAdapter("select A.ID AS OPDNO,A.VN AS IPDNO,A.NAME AS NAME,A.PHONE,A.PACNAME AS PACKAGE,A.DATE AS DATEOFADMISSION,B.DATE AS DATEOFDISCHARGE from  ADMISSION_TABLE A,DISCHARGE_TABLE B where  A.VN=B.PID AND A.NAME LIKE '" + txtname.Text + "%'", con);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //grSearchPatient.SelectedIndex = 0;
                grSearchPatient.DataSource = dt;
                grSearchPatient.DataBind();
                con.Close();
            }
            
        }
        catch (Exception ex)
        {
            // throw ex;
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}