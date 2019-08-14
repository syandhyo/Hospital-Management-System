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

public partial class RECEPTION_BedTransfer : System.Web.UI.Page
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
            lbluid.Text = Session["UID"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();

            if (!IsPostBack)
            {
                binddata();
                div1.Visible = true;
                div2.Visible = true;
            }
            con.Close();
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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
        using (SqlCommand cmd = new SqlCommand("RECP_BED_TRANSFER", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";

            //DataTable dt = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT BEDNO FROM BED_TABLE", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            //dropbedno.SelectedIndex = 0;
            DropDownList1.DataSource = dt1;
            DropDownList1.DataTextField = "BEDNO";
            DropDownList1.DataValueField = "BEDNO";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "Please Select");
        }
        using (SqlCommand cmd1 = new SqlCommand("RECP_BED_TRANSFER", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BEDMATRIX";
            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
            //SqlDataAdapter da2 = new SqlDataAdapter("SELECT DISTINCT NAME FROM BED_MATRIX_TABLE WHERE ORGID='" + lblorgid.Text + "'", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            //dropbedno.SelectedIndex = 0;
            dropward.DataSource = dt2;
            dropward.DataTextField = "NAME";
            dropward.DataBind();
            dropward.Items.Insert(0, "Please Select");
        }

      
        con.Close();
    }
  
  
    protected void btnshowip_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtip.Text == "")
            {
                string message = "alert('Please!!Enter The IP Number.. ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            using (SqlCommand cmd1 = new SqlCommand("RECP_BED_TRANSFER", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VN";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtip.Text;
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                //SqlCommand cm = new SqlCommand("SELECT VN,PNAME,WARD,BEDNO,INSURANCE FROM BED_TABLE WHERE VN= '" + txtip.Text + "' and ORGID='" + lblorgid.Text + "' ", con);
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    lblpid.Text = dr["VN"].ToString();
                    lblname.Text = dr["PNAME"].ToString();
                    lblbed.Text = dr["BEDNO"].ToString();
                    lblward.Text = dr["WARD"].ToString();
                    lblinsurance.Text = dr["INSURANCE"].ToString();
                }
                else
                {
                    string message = "alert('* No Data Found.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            dr.Close();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The Bed..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            using (SqlCommand cmd1 = new SqlCommand("RECP_BED_TRANSFER", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BEDNO";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = DropDownList1.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                //SqlCommand cm = new SqlCommand("SELECT VN,PNAME,WARD,BEDNO,INSURANCE FROM BED_TABLE WHERE BEDNO= '" + DropDownList1.Text + "' and ORGID='" + lblorgid.Text + "'", con);
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    lblpid.Text = dr["VN"].ToString();
                    lblname.Text = dr["PNAME"].ToString();
                    lblbed.Text = dr["BEDNO"].ToString();
                    lblward.Text = dr["WARD"].ToString();
                    lblinsurance.Text = dr["INSURANCE"].ToString();
                }
                else
                {
                    string message = "alert('* No Data Found.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            dr.Close();
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
            if (lblpid.Text == "")
            {
                string message = "alert('Please!! Select A Bed Or Enter A Valid IP Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (lblname.Text == "")
            {
                string message = "alert('Please!! Select A Valid Patient..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (lblward.Text == "")
            {
                string message = "alert('Please!! Select A Valid Patient..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (lblbed.Text == "")
            {
                string message = "alert('Please!! Select A Valid Patient..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropward.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select Ward First....')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropbed.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select Bed For Transfer....')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_BED_TRANSFER", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STATUS";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbed.Text;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                //SqlCommand com1 = new SqlCommand("select * from BED_MATRIX_TABLE where BEDNO='" + dropbed.Text + "' AND STATUS='AVAILABLE' and ORGID='" + lblorgid.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();

                    using (SqlCommand cmd1 = new SqlCommand("usp_BEDTRANSFER", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                        cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                        cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
                        cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblname.Text;
                        cmd1.Parameters.Add("@WARDNAME", SqlDbType.VarChar).Value = lblward.Text;
                        cmd1.Parameters.Add("@NWARDNAME", SqlDbType.VarChar).Value = dropward.Text;
                        cmd1.Parameters.Add("@CBEDNO", SqlDbType.VarChar).Value = lblbed.Text;
                        cmd1.Parameters.Add("@NBEDNO", SqlDbType.VarChar).Value = dropbed.Text;
                        cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                        cmd1.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = lblinsurance.Text;
                        cmd1.ExecuteNonQuery();
                    }
                }
                else
                {
                    dr.Close();
                    string message = "alert('The selected bed is not available. Try new bed.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;

                }
            }

            binddata();
            clearcontrol();
            string message1 = "alert('Successfully Saved.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            con.Close();
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/BedTransfer.aspx");
    }
    public void clearcontrol()
    {
        lblpid.Text = "";
        lblname.Text = "";
        lblward.Text = "";
        lblpid.Text = "";
        lblbed.Text = "";
        txtdate.Text = "";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        Response.Redirect("~/RECEPTION/BedTransfer.aspx");
    }
    protected void dropward_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_BED_TRANSFER", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STATUS_NAME";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbed.Text;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropward.SelectedItem.Text;
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                //SqlDataAdapter da1 = new SqlDataAdapter("SELECT BEDNO FROM BED_MATRIX_TABLE WHERE STATUS='AVAILABLE' AND NAME='" + dropward.SelectedItem.Text + "' AND ORGID='" + lblorgid.Text + "' ORDER BY ID ASC", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                //dropbedno.SelectedIndex = 0;
                dropbed.DataSource = dt1;
                dropbed.DataTextField = "BEDNO";
                dropbed.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    
}