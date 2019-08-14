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

public partial class RECEPTION_nicu : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlDataReader dr;
    SqlConnection con;
    SqlCommand com;
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
           
            SqlDataAdapter Adp2 = new SqlDataAdapter("select * from tblDepartment", con);
            DataTable Dt2 = new DataTable();
            Adp2.Fill(Dt2);
            dropdept.DataSource = Dt2;            
            dropdept.DataTextField = "DeptName";
            dropdept.DataValueField = "id";
            dropdept.DataBind();
            dropdept.Items.Insert(0, "---Please Select---");
        }


        con.Close();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from NICU_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        //num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtipno.Text = "IP-" + num1 + 1 + "-" + lblfyear.Text;
        dr.Close();
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

         using (SqlCommand cmd = new SqlCommand("RECP_NICU", con))
         {
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_NICU_PAGE";
             cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
             
             SqlDataAdapter Adp = new SqlDataAdapter(cmd);
             //SqlDataAdapter Adp = new SqlDataAdapter("SELECT * from NICU_TABLE", con);
             DataTable Dt = new DataTable();
             Adp.Fill(Dt);
             GridView1.DataSource = Dt;
             GridView1.DataKeyNames = new string[] { "id" };
             GridView1.DataBind();
         }
        con.Close();
    }
    public void clearcontrol()
    {
        
        //txtbookingfor.Text = "";
        txtregn.Text = "";
        txtname.Text = "";
        txtpatientid.Text = "";
        txtminorpatient.Text = "";
        txtpatientcond.Text = "";
        dropgender.Text = "";
        dropbloodgroup.Text = "";
        txtdateofbirth.Text = "";
        txtaddress.Text = "";
        txtpin.Text = "";
        txtdistrict.Text = "";
        txtstate.Text = "";
        txtmobileno.Text = "";
        txtemailid.Text = "";
        txtfrom.Text = "";
        txtrefname.Text = "";
        txtcomplaint.Text = "";
        dropdept.SelectedIndex = 0;
        dropdoctor.SelectedIndex = 0;
        txttreatment.Text = "";
        droproomtype.Text = "";
        txtoperationdate.Text = "";
        txtbookingdate.Text = "";
        txtdays.Text = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            // Validation
            if (txtpatientid.Text == "")
            {
                string message = "alert('* Patient Id is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtname.Text == "")
            {
                string message = "alert('* Name mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropgender.Text == "")
            {
                string message = "alert('* Gender is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('* Address is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtmobileno.Text == "")
            {
                string message = "alert('* Mobile No. mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (droproomtype.Text == "")
            {
                string message = "alert('* Room Type is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtbookingdate.Text == "")
            {
                string message = "alert('* Booking Date is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cmd = new SqlCommand("USP_NICU", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                // cmd.Parameters.Add("@ID", SqlDbType.Int).Value = txtid.Text;
                cmd.Parameters.Add("@REGN_NO", SqlDbType.VarChar).Value = txtregn.Text;
                cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtipno.Text;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                cmd.Parameters.Add("@MOTHER_EXIT", SqlDbType.VarChar).Value = dropmotherexit.Text;
                cmd.Parameters.Add("@COMBINE_BILL", SqlDbType.VarChar).Value = dropcombinebill.Text;
                cmd.Parameters.Add("@MINOR_PATIENT", SqlDbType.VarChar).Value = txtminorpatient.Text;
                cmd.Parameters.Add("@EMERGENCY", SqlDbType.VarChar).Value = txtemergency.Text;
                cmd.Parameters.Add("@PATIENT_CONDITION", SqlDbType.VarChar).Value = txtpatientcond.Text;
                cmd.Parameters.Add("@PATIENT_ID", SqlDbType.VarChar).Value = txtpatientid.Text;
                cmd.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.Text;
                cmd.Parameters.Add("@BLOODGROUP", SqlDbType.VarChar).Value = dropbloodgroup.Text;
                cmd.Parameters.Add("@DATEOFBIRTH", SqlDbType.Date).Value = txtdateofbirth.Text;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
                cmd.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
                cmd.Parameters.Add("@DISTRICT", SqlDbType.VarChar).Value = txtdistrict.Text;
                cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                cmd.Parameters.Add("@MOBILE", SqlDbType.VarChar).Value = txtmobileno.Text;
                cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemailid.Text;
                cmd.Parameters.Add("@FROM", SqlDbType.VarChar).Value = txtfrom.Text;
                cmd.Parameters.Add("@REFERALNAME", SqlDbType.VarChar).Value = txtrefname.Text;
                cmd.Parameters.Add("@COMPLAINT", SqlDbType.VarChar).Value = txtcomplaint.Text;
                cmd.Parameters.Add("@CONSULTINGDEPT", SqlDbType.VarChar).Value = dropdept.SelectedValue;
                cmd.Parameters.Add("@DOCTORNAME", SqlDbType.VarChar).Value = dropdoctor.SelectedValue;
                cmd.Parameters.Add("@TREATEMENT", SqlDbType.VarChar).Value = txttreatment.Text;
                cmd.Parameters.Add("@ROOMTYPE", SqlDbType.VarChar).Value = droproomtype.Text;
                cmd.Parameters.Add("@OPERATIONDATE", SqlDbType.Date).Value = txtoperationdate.Text;
                cmd.Parameters.Add("@BOOKINGFORDATE", SqlDbType.Date).Value = txtbookingdate.Text;
                cmd.Parameters.Add("@DAYS", SqlDbType.VarChar).Value = txtdays.Text;
                cmd.ExecuteNonQuery();
            }
            binddata();
            con.Close();
            clearcontrol();
            string message1 = "alert('Successfully Saved.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/NICU.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            // Validation
            if (txtpatientid.Text == "")
            {
                string message = "alert('* Patient Id is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtname.Text == "")
            {
                string message = "alert('* Name mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropgender.Text == "")
            {
                string message = "alert('* Gender is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('* Address is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtmobileno.Text == "")
            {
                string message = "alert('* Mobile No. mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (droproomtype.Text == "")
            {
                string message = "alert('* Room Type is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtbookingdate.Text == "")
            {
                string message = "alert('* Booking Date is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cmd = new SqlCommand("USP_NICU", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                // cmd.Parameters.Add("@ID", SqlDbType.Int).Value = txtid.Text;
                cmd.Parameters.Add("@REGN_NO", SqlDbType.VarChar).Value = txtregn.Text;
                cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtipno.Text;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                cmd.Parameters.Add("@MOTHER_EXIT", SqlDbType.VarChar).Value = dropmotherexit.Text;
                cmd.Parameters.Add("@COMBINE_BILL", SqlDbType.VarChar).Value = dropcombinebill.Text;
                cmd.Parameters.Add("@MINOR_PATIENT", SqlDbType.VarChar).Value = txtminorpatient.Text;
                cmd.Parameters.Add("@EMERGENCY", SqlDbType.VarChar).Value = txtemergency.Text;
                cmd.Parameters.Add("@PATIENT_CONDITION", SqlDbType.VarChar).Value = txtpatientcond.Text;
                cmd.Parameters.Add("@PATIENT_ID", SqlDbType.VarChar).Value = txtpatientid.Text;
                cmd.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.Text;
                cmd.Parameters.Add("@BLOODGROUP", SqlDbType.VarChar).Value = dropbloodgroup.Text;
                cmd.Parameters.Add("@DATEOFBIRTH", SqlDbType.Date).Value = txtdateofbirth.Text;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
                cmd.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
                cmd.Parameters.Add("@DISTRICT", SqlDbType.VarChar).Value = txtdistrict.Text;
                cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                cmd.Parameters.Add("@MOBILE", SqlDbType.VarChar).Value = txtmobileno.Text;
                cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemailid.Text;
                cmd.Parameters.Add("@FROM", SqlDbType.VarChar).Value = txtfrom.Text;
                cmd.Parameters.Add("@REFERALNAME", SqlDbType.VarChar).Value = txtrefname.Text;
                cmd.Parameters.Add("@COMPLAINT", SqlDbType.VarChar).Value = txtcomplaint.Text;
                cmd.Parameters.Add("@CONSULTINGDEPT", SqlDbType.VarChar).Value = dropdept.SelectedValue;
                cmd.Parameters.Add("@DOCTORNAME", SqlDbType.VarChar).Value = dropdoctor.SelectedValue;
                cmd.Parameters.Add("@TREATEMENT", SqlDbType.VarChar).Value = txttreatment.Text;
                cmd.Parameters.Add("@ROOMTYPE", SqlDbType.VarChar).Value = droproomtype.Text;
                cmd.Parameters.Add("@OPERATIONDATE", SqlDbType.Date).Value = txtoperationdate.Text;
                cmd.Parameters.Add("@BOOKINGFORDATE", SqlDbType.Date).Value = txtbookingdate.Text;
                cmd.Parameters.Add("@DAYS", SqlDbType.VarChar).Value = txtdays.Text;
                cmd.ExecuteNonQuery();
            }
            binddata();
            con.Close();
            clearcontrol();
            string message1 = "alert('Successfully Updated.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

        Response.Redirect("~/RECEPTION/NICU.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/NICU.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
            using (SqlCommand cmd = new SqlCommand("RECP_NICU", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = slno;

                //SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlCommand cm = new SqlCommand("delete from NICU_TABLE where ID='" + slno + "'", con);
                cmd.ExecuteNonQuery();
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_NICU", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ID";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;

                //SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("SELECT * from NICU_TABLE where id='" + slno + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btnSubmit.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    txtregn.Text = dr["REGN_NO"].ToString();
                    txtdate.Text = dr["DATE"].ToString();
                    txtipno.Text = dr["IPNO"].ToString();
                    txtname.Text = dr["NAME"].ToString();
                    dropmotherexit.Text = dr["MOTHER_EXIT"].ToString();
                    dropcombinebill.Text = dr["COMBINE_BILL"].ToString();
                    txtminorpatient.Text = dr["MINOR_PATIENT"].ToString();
                    txtemergency.Text = dr["EMERGENCY"].ToString();
                    txtpatientcond.Text = dr["PATIENT_CONDITION"].ToString();
                    txtpatientid.Text = dr["PATIENT_ID"].ToString();
                    dropgender.Text = dr["GENDER"].ToString();
                    dropbloodgroup.Text = dr["BLOODGROUP"].ToString();
                    txtdateofbirth.Text = dr["DATEOFBIRTH"].ToString();
                    txtaddress.Text = dr["ADDRESS"].ToString();
                    txtpin.Text = dr["PIN"].ToString();
                    txtdistrict.Text = dr["DISTRICT"].ToString();
                    txtstate.Text = dr["STATE"].ToString();
                    txtmobileno.Text = dr["MOBILE"].ToString();
                    txtemailid.Text = dr["EMAIL"].ToString();
                    txtfrom.Text = dr["FROM"].ToString();
                    txtrefname.Text = dr["REFERALNAME"].ToString();
                    txtcomplaint.Text = dr["COMPLAINT"].ToString();
                    dropdept.SelectedValue = dr["CONSULTINGDEPT"].ToString();
                    dropdoctor.SelectedValue = dr["DOCTORNAME"].ToString();
                    txttreatment.Text = dr["TREATEMENT"].ToString();
                    droproomtype.Text = dr["ROOMTYPE"].ToString();
                    txtoperationdate.Text = dr["OPERATIONDATE"].ToString();
                    txtbookingdate.Text = dr["BOOKINGFORDATE"].ToString();
                    txtdays.Text = dr["DAYS"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_NICU", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_NICU_PAGE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT * from NICU_TABLE", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void dropdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_NICU", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = dropdept.SelectedValue;

                SqlDataAdapter Adp1 = new SqlDataAdapter(cmd);
                //SqlDataAdapter Adp1 = new SqlDataAdapter("select id,Sname from tblStaff where Deptid='" + dropdept.SelectedValue + "'", con);
                DataTable Dt1 = new DataTable();
                Adp1.Fill(Dt1);
                dropdoctor.DataSource = Dt1;
                dropdoctor.DataTextField = "Sname";
                dropdoctor.DataValueField = "id";
                dropdoctor.DataBind();
                dropdoctor.Items.Insert(0, "---select---");
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}