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

public partial class RECEPTION_Search : System.Web.UI.Page
{
    SqlDataReader dr;
    SqlConnection con;

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
        using (SqlCommand cmd = new SqlCommand("RECP_STAFFSEARCH", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DESIGNATION";
            cmd.Parameters.Add("@Sname", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@DesgId", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "";
            SqlDataAdapter Adp2 = new SqlDataAdapter(cmd);
            DataTable Dt2 = new DataTable();
            Adp2.Fill(Dt2);
            dropdesg.DataSource = Dt2;
            dropdesg.DataTextField = "DesgName";
            dropdesg.DataValueField = "id";
            dropdesg.DataBind();
            dropdesg.Items.Insert(0, "---select---");
        }
        using (SqlCommand cmd1 = new SqlCommand("RECP_STAFFSEARCH", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DEPARTMENT";
            cmd1.Parameters.Add("@Sname", SqlDbType.VarChar).Value = "";
            cmd1.Parameters.Add("@DesgId", SqlDbType.VarChar).Value = "";
            cmd1.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "";
            SqlDataAdapter Adp1 = new SqlDataAdapter(cmd1);
            //SqlDataAdapter Adp1 = new SqlDataAdapter("select id,DeptName from tblDepartment ", con);
            DataTable Dt1 = new DataTable();
            Adp1.Fill(Dt1);
            dropdept.DataSource = Dt1;
            dropdept.DataTextField = "DeptName";
            dropdept.DataValueField = "id";
            dropdept.DataBind();
            dropdept.Items.Insert(0, "---select---");
        }
        con.Close();
    }

    protected void btnName_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!! Enter The Name And Search..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_STAFFSEARCH", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOW_NAME";
                cmd1.Parameters.Add("@Sname", SqlDbType.VarChar).Value = txtname.Text;
                cmd1.Parameters.Add("@DesgId", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "";
                SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
                //SqlDataAdapter Adp = new SqlDataAdapter("select id,Sname,Email,Contact from tblStaff where Sname like'" + txtname.Text + "%'", con);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                if (Dt.Rows.Count == 0)
                {

                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;

                }
                else
                {
                    GridView1.DataSource = Dt;
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
    protected void btnDesg_Click(object sender, EventArgs e)
    {
       
        try
        {
            if (dropdesg.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Designation And Search..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_STAFFSEARCH", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DESGID";
                cmd1.Parameters.Add("@Sname", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@DesgId", SqlDbType.VarChar).Value = dropdesg.SelectedValue;
                cmd1.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "";
                SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
                //SqlDataAdapter Adp = new SqlDataAdapter("select id,Sname,Email,Contact from tblStaff where DesgId='" + dropdesg.SelectedItem.Text + "'", con);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                if (Dt.Rows.Count == 0)
                {

                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else
                {
                    GridView1.DataSource = Dt;
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
    protected void btnDept_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropdept.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Department And Search..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_STAFFSEARCH", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DEPTID";
                cmd1.Parameters.Add("@Sname", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@DesgId", SqlDbType.VarChar).Value ="";
                cmd1.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = dropdept.Text;
                SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
                //SqlDataAdapter Adp = new SqlDataAdapter("select id,Sname,Email,Contact from tblStaff where Deptid='" + dropdept.Text + "'", con);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                if (Dt.Rows.Count == 0)
                {

                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else
                {
                    GridView1.DataSource = Dt;
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
}