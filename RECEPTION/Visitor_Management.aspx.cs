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

public partial class RECEPTION_PatientSearch : System.Web.UI.Page
{
    SqlConnection con;
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
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        if (!IsPostBack)
        {
            binddata();
           
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
        using (SqlCommand cmd = new SqlCommand("RECP_VISITOR", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
            cmd.Parameters.Add("@UHID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
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
        con.Close();
    }
   
    protected void btnshowip_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_VISITOR", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_UHID";
                cmd.Parameters.Add("@UHID", SqlDbType.VarChar).Value = txtip.Text;
                cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                //SqlCommand COM = new SqlCommand("Select * from  ADMISSION_TABLE where UHID= '" + txtip.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblpname.Text = dr["NAME"].ToString();
                    lblbedno.Text = dr["BEDNO"].ToString();
                    lbluhid.Text = dr["UHID"].ToString();
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    string message = "alert('Incorrect UHID')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            using (SqlCommand cmd1 = new SqlCommand("RECP_VISITOR", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VISITOR_IPBED";
                cmd1.Parameters.Add("@UHID", SqlDbType.VarChar).Value = txtip.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                //SqlCommand COM1 = new SqlCommand("Select COUNT(VNO)+1 as NOOFVISIT from  VISTOR_TABLE where UHID= '" + lbluhid.Text + "' AND DATE='" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "'", con);
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    lblvisitorno.Text = dr["NOOFVISIT"].ToString();
                    if (Convert.ToInt32(lblvisitorno.Text) > 2)
                    {
                        string message = "alert('Maximum Visitor Number Reached.')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        return;
                    }

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
            using (SqlCommand cmd1 = new SqlCommand("RECP_VISITOR", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BEDNO";
                cmd1.Parameters.Add("@UHID", SqlDbType.VarChar).Value = txtip.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = DropDownList1.Text;
                //SqlCommand COM = new SqlCommand("Select * from  BED_TABLE where BEDNO= '" + DropDownList1.Text + "'", con);
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    lblpname.Text = dr["PNAME"].ToString();
                    lblbedno.Text = dr["BEDNO"].ToString();
                    lbluhid.Text = dr["UHID"].ToString();

                }
                dr.Close();
            }
            using (SqlCommand cmd = new SqlCommand("RECP_VISITOR", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VISITOR_IPBED";
                cmd.Parameters.Add("@UHID", SqlDbType.VarChar).Value = txtip.Text;
                cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                //SqlCommand COM1 = new SqlCommand("Select COUNT(VNO)+1 as NOOFVISIT from  VISTOR_TABLE where UHID= '" + lbluhid.Text + "' AND DATE='" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblvisitorno.Text = dr["NOOFVISIT"].ToString();
                    if (Convert.ToInt32(lblvisitorno.Text) > 2)
                    {
                        string message = "alert('Maximum Visitor Number Reached.')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        return;
                    }

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
            if (txtvisitorname.Text == "")
            {
                string message = "alert('* Enter Visitor Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtrelation.Text == "")
            {
                string message = "alert('* Enter Relation.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (lblbedno.Text == "0")
            {
                string message = "alert('* Select Patient First.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (Convert.ToInt32(lblvisitorno.Text) > 2)
            {
                string message = "alert('Maximum Visitor Number Reached.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cm = new SqlCommand("RECP_VISITOR_INSERT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                //SqlCommand cm = new SqlCommand("insert into VISTOR_TABLE(DATE,VNO,UHID,PNAME,VNAME,RELATION,BEDNO) VALUES (@DATE,@VNO,@UHID,@PNAME,@VNAME,@RELATION,@BEDNO)", con);
                cm.Parameters.Add("@VNO", SqlDbType.VarChar).Value = lblvisitorno.Text;
                cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cm.Parameters.Add("@VNAME", SqlDbType.VarChar).Value = lblpname.Text;
                cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = lbluhid.Text;
                cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtvisitorname.Text;
                cm.Parameters.Add("@RELATION", SqlDbType.VarChar).Value = txtrelation.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbedno.Text;
                cm.ExecuteNonQuery();
            }
            con.Close();

        
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/visitor_reciept.aspx");
    }
  
}