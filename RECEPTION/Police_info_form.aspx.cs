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
public partial class RECEPTION_Police_info_form : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from POLICE_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("PO{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

        dr.Close();
        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
        }
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            binddata();
           
        }
        con.Close();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");

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
        using (SqlCommand cmd = new SqlCommand("RECP_POLICE_INFO", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_POLICE";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,DATE,PNAME,BEDNO,PID FROM POLICE_TABLE  ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
        }
        using (SqlCommand cmd1 = new SqlCommand("RECP_POLICE_INFO", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT BEDNO FROM BED_TABLE", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            //dropbedno.SelectedIndex = 0;
            dropbedno.DataSource = dt1;
            dropbedno.DataTextField = "BEDNO";
            dropbedno.DataBind();
            dropbedno.Items.Insert(0, "Please Select");
        }

   
        con.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropbedno.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Bed No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtnationality.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtperson.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (lblipno.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlCommand com = new SqlCommand("select * from POLICE_TABLE where PID='" + lblipno.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string message = "alert('*Form alredy genereted for this patient.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            dr.Close();

            auto();
            using (SqlCommand cmd1 = new SqlCommand("RECP_POLICE_INFO_INSERT", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                //SqlCommand cmd1 = new SqlCommand("insert into POLICE_TABLE (ID,PID,BEDNO,DATE,PNAME,AGE,GENDER,RELIGION,NATIONALITY,PADDRESS,CAUSE,CADDRESS,IDMARK,ADDATE,DEATHDATE,PERSONE,RELATION,CASEHISTORY,CAUSEOFDEATH,UID,USERNAME) VALUES (@ID,@PID,@BEDNO,@DATE,@PNAME,@AGE,@GENDER,@RELIGION,@NATIONALITY,@PADDRESS,@CAUSE,@CADDRESS,@IDMARK,@ADDATE,@DEATHDATE,@PERSONE,@RELATION,@CASEHISTORY,@CAUSEOFDEATH,@UID,@USERNAME)", con);

                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipno.Text;
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                cmd1.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = lblpname.Text;
                cmd1.Parameters.Add("@AGE", SqlDbType.VarChar).Value = lblage.Text;
                cmd1.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = lblgender.Text;
                cmd1.Parameters.Add("@RELIGION", SqlDbType.VarChar).Value = txtreligion.Text;
                cmd1.Parameters.Add("@NATIONALITY", SqlDbType.VarChar).Value = txtnationality.Text;
                cmd1.Parameters.Add("@PADDRESS", SqlDbType.VarChar).Value = txtpaddress.Text;
                cmd1.Parameters.Add("@CAUSE", SqlDbType.VarChar).Value = dropcause.Text;
                cmd1.Parameters.Add("@CADDRESS", SqlDbType.VarChar).Value = txtcaddress.Text;
                cmd1.Parameters.Add("@IDMARK", SqlDbType.VarChar).Value = txtidmark.Text;
                cmd1.Parameters.Add("@ADDATE", SqlDbType.VarChar).Value = lbladdate.Text;
                cmd1.Parameters.Add("@DEATHDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtdeathdate.Text).ToString("dd-MM-yyyy hh:mm:tt");
                cmd1.Parameters.Add("@PERSONE", SqlDbType.VarChar).Value = txtperson.Text;
                cmd1.Parameters.Add("@RELATION", SqlDbType.VarChar).Value = txtrelationpatient.Text;
                cmd1.Parameters.Add("@CASEHISTORY", SqlDbType.VarChar).Value = txtcasehis.Text;
                cmd1.Parameters.Add("@CAUSEOFDEATH", SqlDbType.VarChar).Value = txtcauseofdeath.Text;
                cmd1.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = lblid.Text;
                cmd1.Parameters.Add("@UID", SqlDbType.VarChar).Value = lbluid.Text;
                cmd1.ExecuteNonQuery();
            }


            binddata();
            Session["POID"] = TXTID.Text;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
         Response.Redirect("~/RECEPTION/police_reciept.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd1 = new SqlCommand("RECP_POLICE_INFO", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ID";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //SqlCommand com = new SqlCommand("select * from POLICE_TABLE where ID='" + slno + "'", con);
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {

                btncreate.Visible = false;
                btnupdate.Visible = true;
                TXTID.Text = dr["ID"].ToString();
                lblipno.Text = dr["PID"].ToString();
                dropbedno.SelectedItem.Text = dr["BEDNO"].ToString();
                txtdate.Text = dr["DATE"].ToString();
                lblpname.Text = dr["PNAME"].ToString();
                lblage.Text = dr["AGE"].ToString();
                lblgender.Text = dr["GENDER"].ToString();
                txtreligion.Text = dr["RELIGION"].ToString();
                txtnationality.Text = dr["NATIONALITY"].ToString();
                txtpaddress.Text = dr["PADDRESS"].ToString();
                dropcause.Text = dr["CAUSE"].ToString();
                txtcaddress.Text = dr["CADDRESS"].ToString();
                txtidmark.Text = dr["IDMARK"].ToString();
                lbladdate.Text = dr["ADDATE"].ToString();
                //String sdate = dr["DEATHDATE"].ToString();
                //String conv = sdate;
                //DateTime suDateTime = new DateTime();
                //suDateTime = DateTime.ParseExact(conv, "dd/MM/yyyy hh:mm tt", null);
                //String converted_date = suDateTime.ToString("dd/MM/yyyy hh:mm tt");
                //txtdeathdate.Text = converted_date;
                txtdeathdate.Text =  (Convert.ToDateTime(dr["DEATHDATE"]).ToString("dd/MM/yyyy hh:mm tt"));
                //dr["DEATHDATE"].ToString("dd-MM-yyyy hh:mm t.\\M."));
                txtperson.Text = dr["PERSONE"].ToString();
                txtrelationpatient.Text = dr["RELATION"].ToString();
                txtcasehis.Text = dr["CASEHISTORY"].ToString();
                txtcauseofdeath.Text = dr["CAUSEOFDEATH"].ToString();

            }
            dr.Close();
        }
        con.Close();
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
        using (SqlCommand cmd1 = new SqlCommand("RECP_POLICE_INFO", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno.ToString();
            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            cmd1.ExecuteReader();
            //SqlDataAdapter da = new SqlDataAdapter("delete from POLICE_TABLE where ID='" + slno.ToString() + "'", con);
            //ds = new DataSet();
            //da.Fill(ds);
        }
        binddata();
        con.Close();
        Response.Redirect("~/RECEPTION/Police_info_form.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_POLICE_INFO", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_POLICE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,DATE,PNAME,BEDNO,PID FROM POLICE_TABLE  ORDER BY ID DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtreligion.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtnationality.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtperson.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (lblipno.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_POLICE_INFO_INSERT", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                //SqlCommand cmd1 = new SqlCommand("update POLICE_TABLE set PID=@PID,BEDNO=@BEDNO,DATE=@DATE,PNAME=@PNAME,AGE=@AGE,GENDER=@GENDER,RELIGION=@RELIGION,NATIONALITY=@NATIONALITY,PADDRESS=@PADDRESS,CAUSE=@CAUSE,CADDRESS=@CADDRESS,IDMARK=@IDMARK,ADDATE=@ADDATE,DEATHDATE=@DEATHDATE,PERSONE=@PERSONE,RELATION=@RELATION,CASEHISTORY=@CASEHISTORY,CAUSEOFDEATH=@CAUSEOFDEATH,UID=@UID,USERNAME=@USERNAME where ID=@ID", con);
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipno.Text;
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                cmd1.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = lblpname.Text;
                cmd1.Parameters.Add("@AGE", SqlDbType.VarChar).Value = lblage.Text;
                cmd1.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = lblgender.Text;
                cmd1.Parameters.Add("@RELIGION", SqlDbType.VarChar).Value = txtreligion.Text;
                cmd1.Parameters.Add("@NATIONALITY", SqlDbType.VarChar).Value = txtnationality.Text;
                cmd1.Parameters.Add("@PADDRESS", SqlDbType.VarChar).Value = txtpaddress.Text;
                cmd1.Parameters.Add("@CAUSE", SqlDbType.VarChar).Value = dropcause.Text;
                cmd1.Parameters.Add("@CADDRESS", SqlDbType.VarChar).Value = txtcaddress.Text;
                cmd1.Parameters.Add("@IDMARK", SqlDbType.VarChar).Value = txtidmark.Text;
                cmd1.Parameters.Add("@ADDATE", SqlDbType.VarChar).Value = lbladdate.Text;
                cmd1.Parameters.Add("@DEATHDATE", SqlDbType.VarChar).Value =  Convert.ToDateTime(txtdeathdate.Text).ToString("dd-MM-yyyy hh:mm:t");
                cmd1.Parameters.Add("@PERSONE", SqlDbType.VarChar).Value = txtperson.Text;
                cmd1.Parameters.Add("@RELATION", SqlDbType.VarChar).Value = txtrelationpatient.Text;
                cmd1.Parameters.Add("@CASEHISTORY", SqlDbType.VarChar).Value = txtcasehis.Text;
                cmd1.Parameters.Add("@CAUSEOFDEATH", SqlDbType.VarChar).Value = txtcauseofdeath.Text;
                cmd1.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = lblid.Text;
                cmd1.Parameters.Add("@UID", SqlDbType.VarChar).Value = lbluid.Text;
                cmd1.ExecuteNonQuery();
            }

            binddata();
            Session["POID"] = TXTID.Text;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/police_reciept.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        Response.Redirect("~/RECEPTION/Police_info_form.aspx");
    }
   
    protected void dropbedno_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd = new SqlCommand("RECP_POLICE_INFO", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROP";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlCommand COM = new SqlCommand("SELECT A.VN,A.PNAME,B.FAMILY,B.AGE,B.GENDER,B.PADDRESS,B.DATE AS ADDATE FROM BED_TABLE A,ADMISSION_TABLE B WHERE  A.BEDNO='" + dropbedno.Text + "' AND B.VN=A.VN", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblipno.Text = dr["VN"].ToString();
                lblpname.Text = dr["PNAME"].ToString();
                lblage.Text = dr["AGE"].ToString();
                lblgender.Text = dr["GENDER"].ToString();
                txtpaddress.Text = dr["PADDRESS"].ToString();
                txtperson.Text = dr["FAMILY"].ToString();
                lbladdate.Text = dr["ADDATE"].ToString();
            }
            dr.Close();
        }
        con.Close();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["POID"] = gr.Cells[0].Text;
        Response.Redirect("~/RECEPTION/police_reciept.aspx");
    }
}