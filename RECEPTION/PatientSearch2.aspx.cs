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

public partial class RECEPTION_PatientSearch2 : System.Web.UI.Page
{
    SqlDataReader dr;
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            binddata();
            div1.Visible = true;
            div2.Visible = true;
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

        SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT BEDNO FROM BED_TABLE", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        //dropbedno.SelectedIndex = 0;
        DropDownList1.DataSource = dt1;
        DropDownList1.DataTextField = "BEDNO";
        DropDownList1.DataValueField = "BEDNO";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "Please Select");
        con.Close();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            if (txtspid.Text == "")
            {
                string message = "alert('* Please Select Patient OPD Number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtspid.Text != "")
            {
                SqlCommand cmd2 = new SqlCommand("select * from ADMISSION_TABLE where ID='" + txtspid.Text + "'", con);
                SqlDataReader dr1 = cmd2.ExecuteReader();
                if (dr1.Read() == false)
                {
                    string message = "alert('Invalid OPD Number.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                dr1.Close();
            }

            // SqlDataAdapter da = new SqlDataAdapter("SELECT ID,PNAME,TELPHNO,MOBNO,DATETIME FROM REGISTRATION_TBL WHERE ID= '" + txtspid.Text + "'  ORDER BY ID DESC", con);
            using (SqlCommand cmd = new SqlCommand("SP_OT_OP_PatientSrch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_OP";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtspid.Text;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {

                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    GridView1.SelectedIndex = 0;
                    GridView1.DataSource = dt;
                    GridView1.DataKeyNames = new string[] { "ID" };
                    GridView1.DataBind();
                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnshowph_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtsmobile.Text == "")
            {
                string message = "alert('* Please Select Patient Mobile Number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,PNAME,TELPHNO,MOBNO,DATETIME FROM REGISTRATION_TBL WHERE TELPHNO= '" + txtsmobile.Text + "' ORDER BY ID DESC", con);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SP_OT_OP_PatientSrch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "Select_Phn";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = txtsmobile.Text;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {

                        string message = "alert(' No Records is there..')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                    else
                    {
                        GridView1.SelectedIndex = 0;
                        GridView1.DataSource = dt;
                        GridView1.DataKeyNames = new string[] { "ID" };
                        GridView1.DataBind();
                    }
                }
                else
                {
                    string message = "alert('No Record Found..')";
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
    protected void btnshowip_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            if (txtip.Text == "")
            {
                string message = "alert('* Please Select Patient IP No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtip.Text != "")
            {
                SqlCommand cmd2 = new SqlCommand("select * from BED_TABLE where VN='" + txtip.Text + "'", con);
                SqlDataReader dr1 = cmd2.ExecuteReader();
                if (dr1.Read() == false)
                {
                    string message = "alert('Invalid IPD Number.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                dr1.Close();
            }

            //SqlDataAdapter da = new SqlDataAdapter("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.VN= '" + txtip.Text + "' and A.ORGID='" + lblorgid.Text + "' ", con);

            using (SqlCommand cmd = new SqlCommand("SP_OT_IP_PatientSrch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_IP";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtip.Text;
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = DropDownList1.SelectedValue;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {

                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    GridView2.SelectedIndex = 0;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnshowbed_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('* Please Select BED NO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.BEDNO= '" + DropDownList1.Text + "' and A.ORGID='" + lblorgid.Text + "'", con);
            using (SqlCommand cmd = new SqlCommand("SP_OT_IP_PatientSrch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = DropDownList1.Text;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    string message = "alert('No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    GridView2.SelectedIndex = 0;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
            }
            con.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView2.SelectedIndex = 0;
            GridView2.DataSource = dt;
            GridView2.PageIndex = e.NewPageIndex;
            //GridView2.DataKeyNames = new string[] { "ID" };
            GridView2.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["VN"].ToString();
        Session["s_id"] = slno;
        Response.Redirect("~/RECEPTION/IP_Charges.aspx");
    }
}