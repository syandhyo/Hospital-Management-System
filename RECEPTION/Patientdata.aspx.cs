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

public partial class RECEPTION_Patientdata : System.Web.UI.Page
{
    SqlDataAdapter da, da1;
    SqlDataReader dr;
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
        lbluid.Text = Session["UID"].ToString();
        if (!IsPostBack)
        {
            binddata();
            using (SqlCommand cmd = new SqlCommand("RECP_INSURANCE_PATIENTDATA", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BEDTABLE";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = "NULL";
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                //da1 = new SqlDataAdapter("select DISTINCT INSURANCE FROM BED_TABLE where ORGID='" + lblorgid.Text + "'", con);
                DataTable ds1 = new DataTable();
                da1.Fill(ds1);
                dropinsurance.DataSource = ds1;
                dropinsurance.DataTextField = "CNAME";
                dropinsurance.DataValueField = "ID";
                dropinsurance.DataBind();
                dropinsurance.Items.Insert(0, "Please Select");
            }
        }
        con.Close();
    }
    protected void dropinsurance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_INSURANCE_PATIENTDATA", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROP";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = dropinsurance.SelectedValue;
                SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlDataAdapter Adp = new SqlDataAdapter("SELECT BED_TABLE.WARD,BED_TABLE.BEDNO,BED_TABLE.PNAME,BED_TABLE.VN,ADMISSION_TABLE.INSURANCENAME FROM ADMISSION_TABLE INNER JOIN BED_TABLE ON ADMISSION_TABLE.VN = BED_TABLE.VN WHERE BED_TABLE.INSURANCE='" + dropinsurance.SelectedValue + "'", con);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                GridView1.DataSource = Dt;
                GridView1.DataBind();
            }
            con.Close();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_INSURANCE_PATIENTDATA", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_SHOW";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = "";
                SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlDataAdapter Adp = new SqlDataAdapter("SELECT BED_TABLE.WARD,BED_TABLE.BEDNO,BED_TABLE.PNAME,BED_TABLE.VN,ADMISSION_TABLE.INSURANCENAME FROM ADMISSION_TABLE INNER JOIN BED_TABLE ON ADMISSION_TABLE.VN = BED_TABLE.VN", con);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                GridView1.DataSource = Dt;
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}