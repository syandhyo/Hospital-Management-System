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
using System.Web.ClientServices;
using System.Net;


public partial class RECEPTION_Patient_registration : System.Web.UI.Page
{
    string num1 = "SJ000";
    string num2 = "SJ00000000000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
         Session["PID"]= gr.Cells[0].Text;
        Response.Redirect("~/RECEPTION/preg_reciept.aspx");
    }
    public void autouhid()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select UD from PATIENT_REG_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("VH{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        lbluhid.Text = num1;

        dr.Close();
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
           
            binddata();
        }
        con.Close();
        //auto();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
       

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID,NAME,PHONE FROM PATIENT_REG_TABLE WHERE ID= '" + txtop.Text + "' and ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count<1)
        {
            string message = "alert('* No Data Found.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        GridView1.SelectedIndex = 0;
        GridView1.DataSource = dt;
        GridView1.DataKeyNames = new string[] { "ID" };
        GridView1.DataBind();
        con.Close();
    }
    protected void btnshowph_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID,NAME,PHONE FROM PATIENT_REG_TABLE WHERE PHONE= '" + txtmob.Text + "' and ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count < 1)
        {
            string message = "alert('* No Data Found.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        GridView1.SelectedIndex = 0;
        GridView1.DataSource = dt;
        GridView1.DataKeyNames = new string[] { "ID" };
        GridView1.DataBind();
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

        SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT CITY FROM CITYMASTER_TABLE", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        //dropbedno.SelectedIndex = 0;
        dropcity.DataSource = dt1;
        dropcity.DataTextField = "CITY";
        dropcity.DataValueField = "CITY";
        dropcity.DataBind();
        dropcity.Items.Insert(0, "Other");
      
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID,NAME,PHONE FROM PATIENT_REG_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.SelectedIndex = 0;
        GridView1.DataSource = dt;
        GridView1.DataKeyNames = new string[] { "ID" };
        GridView1.DataBind();
        con.Close();
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select max(ID) as ID from PATIENT_REG_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        if (dr.Read() && dr["ID"].ToString() != "")
        {
            num1 = dr["ID"].ToString();
            num2 = dr["ID"].ToString();
            num1 = string.Format("OP{0}", (Convert.ToUInt64(num1.Substring(2)) + 1).ToString("D4"));
            num2 = string.Format("VVKH-{0}", (Convert.ToUInt32(num2.Substring(2)) + 1).ToString("D10"));

        }
        else
        {
            string qry2 = "select OPNO from IDGENERATE_TABLE";
            cmd = new SqlCommand(qry2, con);
            dr1 = null;
            dr.Close();
            dr1 = cmd.ExecuteReader();

            if (dr1.Read())
            {
                num1 = dr1["OPNO"].ToString();
                num2 = dr["ID"].ToString();
                num1 = string.Format("OP{0}", (Convert.ToUInt64(num1.Substring(2)) + 1).ToString("D4"));
                num2 = string.Format("VH{0}", (Convert.ToUInt32(num2.Substring(2)) + 1).ToString("D10"));
            }
            else
            {
                num1 = string.Format("OP{0}", (Convert.ToUInt64(num1.Substring(2)) + 1).ToString("D4"));
            }
        }


      
        lbluhid.Text = num2;
       // num1 = string.Format("OP{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        lblpid.Text = num1;

        dr.Close();
        con.Close();
    }
    public void clear_control()
    {
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        lblpid.Text = "";
        txtname.Text = "";
        txtphone.Text = "0";
        txtemail.Text = "0";
        txtecontact.Text = "0";
        txtage.Text = "0";
        txtdate.Text = "0";
        Txtperad.Text = "0";
        txtpref.Text = "0";
        txtregfee.Text = "0";
        txttempad.Text = "0";
        txtdiease.Text = "0";
        btncreate.Visible = true;
        btnupdate.Visible = false;


    }
    public void sms()
    { 
        
    //    using (WebClient client = new WebClient())
    //{
    //    string url = "http://smsc.vianett.no/v3/send.ashx?src='" + txtphone.Text + "&dst=" + txtphone.Text + "&msg=Thank You For Choosing Zemusi Hospital.Your UHID:" + lbluhid.Text + "&username=007aneesh007@gmail.com&password=9eusb";
    //        string result = client.DownloadString(url);
    //        result.Contains("OK");
    //}
        
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
            if (txtname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtdate.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtage.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (Convert.ToDecimal(txtage.Text) > 110)
            {
                string message = "alert('* Fields are mandatory- AGE.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            //else if (txtecontact.Text == "")
            //{
            //    string message = "alert('* Fields are mandatory.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            //else if (txtphone.Text == "")
            //{
            //    string message = "alert('* Fields are mandatory.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
         
            else if (txtregfee.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (Txtperad.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            //SqlCommand com = new SqlCommand("select * from PATIENT_REG_TABLE where NAME='" + txtname.Text.ToUpper() + "' and ORGID='" + lblorgid.Text + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    Response.Write("<script LANGUAGE='JavaScript' >alert('Alredy Exist.')</script>");

            //    return;
            //}
            //dr.Close();

            auto();

            SqlCommand cmd1 = new SqlCommand("insert into PATIENT_REG_TABLE (ID,PTYPE,NAME,AGE,GENDER,PHONE,ECONTACT,EMAIL,PREF,DATE,PADDRESS,TADDRESS,RGFEE,DISEASE,USERNAME,USERID,ORGID,CITY,UHID) values (@ID,@PTYPE,@NAME,@AGE,@GENDER,@PHONE,@ECONTACT,@EMAIL,@PREF,@DATE,@PADDRESS,@TADDRESS,@RGFEE,@DISEASE,@USERNAME,@USERID,@ORGID,@CITY,@UHID)", con);
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblpid.Text;
            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd1.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = lbloutpatient.Text;
            cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            cmd1.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
            cmd1.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = droprtype.Text;
            cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = txtphone.Text;
            cmd1.Parameters.Add("@ECONTACT", SqlDbType.VarChar).Value = txtecontact.Text;
            cmd1.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemail.Text;
            cmd1.Parameters.Add("@PREF", SqlDbType.VarChar).Value = txtpref.Text;
            cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text; 
            cmd1.Parameters.Add("@PADDRESS", SqlDbType.VarChar).Value = Txtperad.Text;
            cmd1.Parameters.Add("@TADDRESS", SqlDbType.VarChar).Value = txttempad.Text;
            cmd1.Parameters.Add("@RGFEE", SqlDbType.Decimal).Value = txtregfee.Text;
            cmd1.Parameters.Add("@DISEASE", SqlDbType.VarChar).Value = txtdiease.Text;
            cmd1.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = lblid.Text;
            cmd1.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lbluid.Text;
            cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = dropcity.Text;
            cmd1.Parameters.Add("@UHID", SqlDbType.VarChar).Value = lbluhid.Text;
            cmd1.ExecuteNonQuery();
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_REGISTRATION",con))
            {            
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = lblpid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT REGISTRATION";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtregfee.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtregfee.Text;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cmd = new SqlCommand("usp_UserCollection", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["UID"].ToString();
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
                cmd.Parameters.Add("@Collection", SqlDbType.Decimal).Value = txtregfee.Text;
                cmd.Parameters.Add("@Paid", SqlDbType.Decimal).Value = 0.00;
                cmd.ExecuteNonQuery();
            }
            binddata();
           // sms();
            Session["PID"] = lblpid.Text;

            con.Close();
            Response.Redirect("~/RECEPTION/preg_reciept.aspx");
        
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select * from PATIENT_REG_TABLE where ID='" + slno + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            btncreate.Visible = false;
            btnupdate.Visible = true;
            lblpid.Text = dr["ID"].ToString();
            lbloutpatient.Text = dr["PTYPE"].ToString();
            txtname.Text = dr["NAME"].ToString();
            txtage.Text = dr["AGE"].ToString();
            droprtype.Text = dr["GENDER"].ToString();
            txtphone.Text = dr["PHONE"].ToString();
            txtecontact.Text = dr["ECONTACT"].ToString();
            txtemail.Text = dr["EMAIL"].ToString();
            txtpref.Text = dr["PREF"].ToString();
            txtdate.Text = dr["DATE"].ToString();
            Txtperad.Text = dr["PADDRESS"].ToString();
            txttempad.Text = dr["TADDRESS"].ToString();
            txtregfee.Text = dr["RGFEE"].ToString();
            txtdiease.Text = dr["DISEASE"].ToString();
            dropcity.Text = dr["CITY"].ToString();
       
        }
        dr.Close();
        con.Close();
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();

        SqlDataAdapter da1 = new SqlDataAdapter("delete from PATIENT_REG_TABLE WHERE ID='" + slno + "'", con);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "PATIENT_REG_TABLE");
        using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_REGISTRATION", con))
        {
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = "2018-02-13";
            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = lblpid.Text;
            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = slno;
            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT REGISTRATION";
            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = "0.00";
            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0.00";
            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0.00";
            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
            cm.ExecuteNonQuery();
        }
        binddata();
        con.Close();
        Response.Redirect("~/RECEPTION/Patient_registration.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,PHONE FROM PATIENT_REG_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.SelectedIndex = 0;
        GridView1.DataSource = dt;
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataKeyNames = new string[] { "ID" };
        GridView1.DataBind();
        con.Close();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (txtname.Text == "")
        {
            string message = "alert('* Fields are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        else if (txtage.Text == "")
        {
            string message = "alert('* Fields are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        else if (Convert.ToDecimal(txtage.Text) > 110)
        {
            string message = "alert('* Fields are mandatory- AGE.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        //else if (txtecontact.Text == "")
        //{
        //    string message = "alert('* Fields are mandatory.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

        //    return;
        //}
        //else if (txtphone.Text == "")
        //{
        //    string message = "alert('* Fields are mandatory.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

        //    return;
        //}

        else if (txtregfee.Text == "")
        {
            string message = "alert('* Fields are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        SqlCommand cmd1 = new SqlCommand("update PATIENT_REG_TABLE set CITY=@CITY,PTYPE=@PTYPE,NAME=@NAME,AGE=@AGE,GENDER=@GENDER,PHONE=@PHONE,ECONTACT=@ECONTACT,EMAIL=@EMAIL,PREF=@PREF,DATE=@DATE,PADDRESS=@PADDRESS,TADDRESS=@TADDRESS,RGFEE=@RGFEE,DISEASE=@DISEASE where ID=@ID AND ORGID=@ORGID", con);
        cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblpid.Text;
        cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        cmd1.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = lbloutpatient.Text;
        cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
        cmd1.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
        cmd1.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = droprtype.Text;
        cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = txtphone.Text;
        cmd1.Parameters.Add("@ECONTACT", SqlDbType.VarChar).Value = txtecontact.Text;
        cmd1.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemail.Text;
        cmd1.Parameters.Add("@PREF", SqlDbType.VarChar).Value = txtpref.Text;
        cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
        cmd1.Parameters.Add("@PADDRESS", SqlDbType.VarChar).Value = Txtperad.Text;
        cmd1.Parameters.Add("@TADDRESS", SqlDbType.VarChar).Value = txttempad.Text;
        cmd1.Parameters.Add("@RGFEE", SqlDbType.Decimal).Value = txtregfee.Text;
        cmd1.Parameters.Add("@DISEASE", SqlDbType.VarChar).Value = txtdiease.Text;
        cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = dropcity.Text;
        cmd1.ExecuteNonQuery();

      
        binddata();
        Session["PID"] = lblpid.Text;
        con.Close();
        Response.Redirect("~/RECEPTION/preg_reciept.aspx");
       
    }
   
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/Patient_registration.aspx");
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            txttempad.Text = Txtperad.Text;
        }
        else
        {
            txttempad.Text = "NA";
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
        }
    }
}