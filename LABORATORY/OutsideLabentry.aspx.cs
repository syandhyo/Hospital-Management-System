using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web.Services;

public partial class LABORATORY_OutsideLabentry : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    GridViewRow gr;
    string PAIDMAT;
    decimal amount = 0;

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from OUTSIDE_LAB";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("OL{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;
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
        //   txtinvdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        if (!IsPostBack)
        {
            binddata();
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
        dr.Close();
        using (SqlCommand cmd4 = new SqlCommand("LAB_OUTSIDE_ENTRY", con))
        {
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_OUTLAB_PAGE";
            cmd4.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            SqlDataAdapter da1 = new SqlDataAdapter(cmd4);
            //SqlDataAdapter da1 = new SqlDataAdapter("select ID,CONVERT(varchar, DATE, 105) AS DATE,PATIENTYPE,OPDNO,FIRSTNAME from OUTSIDE_LAB ORDER BY ID asc", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            grvlabst.SelectedIndex = 0;
            grvlabst.DataSource = dt1;
            grvlabst.DataKeyNames = new string[] { "ID" };
            grvlabst.DataBind();
        }
        con.Close();
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text == "")
            {
                string message = "alert('*Date Field Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            if (txtsentolab.Text == "")
            {
                string message = "alert('*Sent To Lab Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            if (txtsamplfm.Text == "")
            {
                string message = "alert('* Sample From Is Mandatory')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            //else if (droptesttype.SelectedIndex == 0)
            //{
            //    Response.Write("<script LANGUAGE='JavaScript' >alert('*Select Test Type.')</script>");

            // return;
            //}  
            DateTime datetime = Convert.ToDateTime(txtdate.Text);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cm = new SqlCommand("USP_OUTSIDELAB", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = datetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                cm.Parameters.Add("@SENTNOTENO", SqlDbType.VarChar).Value = txtsenoteno.Text;
                cm.Parameters.Add("@SENTOLAB", SqlDbType.VarChar).Value = txtsentolab.Text;
                cm.Parameters.Add("@SAMPLEFORM", SqlDbType.VarChar).Value = txtsamplfm.Text;
                cm.Parameters.Add("@LABREQNO", SqlDbType.VarChar).Value = txtlabreqno.Text;
                cm.Parameters.Add("@PATIENTYPE", SqlDbType.VarChar).Value = droppatype.SelectedItem.Text;
                cm.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text.ToUpper();
                cm.Parameters.Add("@REQSITNAT", SqlDbType.VarChar).Value = txtreqat.Text;
                cm.Parameters.Add("@FIRSTNAME", SqlDbType.VarChar).Value = txtFname.Text;
                cm.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
                cm.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.SelectedItem.Text;
                cm.Parameters.Add("@CONTACTNO", SqlDbType.VarChar).Value = txtcontactno.Text;
                cm.Parameters.Add("@ADRESSP ", SqlDbType.VarChar).Value = txtadress.Text;
                cm.Parameters.Add("@ADRESSPRT ", SqlDbType.VarChar).Value = txtaddresPermnt.Text;
                cm.Parameters.Add("@TREATINGDR", SqlDbType.VarChar).Value = txttreatingdr.Text;
                cm.Parameters.Add("@ADVPAY", SqlDbType.Decimal).Value = txtAdvpaid.Text;
                cm.Parameters.Add("@SENTBY", SqlDbType.VarChar).Value = txtsentby.Text;
                cm.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = txttotamt.Text;

                cm.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                // cm.Parameters.Add("@DOB", SqlDbType.VarChar).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd HH:mm");

                cm.ExecuteNonQuery();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

        Response.Redirect("~/LABORATORY/OutsideLabentry.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LABORATORY/OutsideLabentry.aspx");
    }
    protected void grvlabst_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grvlabst.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand com = new SqlCommand("LAB_OUTSIDE_ENTRY", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_OUTLAB_ID";
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd4);
                //SqlCommand com = new SqlCommand("select * from OUTSIDE_LAB  where ID='" + slno + "'", con);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    btncancel.Visible = true;
                    txtselid.Text = slno.ToString();
                    txtdate.Text = Convert.ToDateTime(dr["DATE"]).ToString("dd-MM-yyyy");
                    txtsenoteno.Text = dr["SENTNOTENO"].ToString();
                    txtsentolab.Text = dr["SENTOLAB"].ToString();
                    txtsamplfm.Text = dr["SAMPLEFORM"].ToString();

                    txtlabreqno.Text = dr["LABREQNO"].ToString();
                    droppatype.Text = dr["PATIENTYPE"].ToString();
                    txtopdno.Text = dr["OPDNO"].ToString();
                    txtreqat.Text = dr["REQSITNAT"].ToString();
                    txtFname.Text = dr["FIRSTNAME"].ToString();
                    txtage.Text = dr["AGE"].ToString();
                    dropgender.Text = dr["GENDER"].ToString();
                    txtcontactno.Text = dr["CONTACTNO"].ToString();
                    txtadress.Text = dr["ADRESSP"].ToString();
                    txtaddresPermnt.Text = dr["ADRESSPRT"].ToString();
                    txttreatingdr.Text = dr["TREATINGDR"].ToString();
                    txtAdvpaid.Text = dr["ADVPAY"].ToString();
                    txtsentby.Text = dr["SENTBY"].ToString();
                    txttotamt.Text = dr["TOTAMT"].ToString();

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
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text == "")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('* Date Fields are mandatory.')</script>");

                return;
            }

            if (txtsentolab.Text == "")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('*  Fields are mandatory.')</script>");

                return;
            }

            if (txtsamplfm.Text == "")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('* Batch No are mandatory.')</script>");

                return;
            }
            //else if (droptesttype.SelectedIndex == 0)
            //{
            //    Response.Write("<script LANGUAGE='JavaScript' >alert('*Select Test Type.')</script>");

            // return;
            //}  

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cm = new SqlCommand("USP_OUTSIDELAB", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
                cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                cm.Parameters.Add("@SENTNOTENO", SqlDbType.VarChar).Value = txtsenoteno.Text;
                cm.Parameters.Add("@SENTOLAB", SqlDbType.VarChar).Value = txtsentolab.Text;
                cm.Parameters.Add("@SAMPLEFORM", SqlDbType.VarChar).Value = txtsamplfm.Text;
                // cm.Parameters.Add("@EXPIRY", SqlDbType.DateTime).Value = Convert.ToDateTime(txtexpiry.Text).ToString("yyyy-MM-dd HH:mm");
                //cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
                cm.Parameters.Add("@LABREQNO", SqlDbType.VarChar).Value = txtlabreqno.Text;
                cm.Parameters.Add("@PATIENTYPE", SqlDbType.VarChar).Value = droppatype.SelectedItem.Text;
                cm.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text.ToUpper();
                cm.Parameters.Add("@REQSITNAT", SqlDbType.VarChar).Value = txtreqat.Text;
                cm.Parameters.Add("@FIRSTNAME", SqlDbType.VarChar).Value = txtFname.Text;
                cm.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
                cm.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.SelectedItem.Text;
                cm.Parameters.Add("@CONTACTNO", SqlDbType.VarChar).Value = txtcontactno.Text;
                cm.Parameters.Add("@ADRESSP ", SqlDbType.VarChar).Value = txtadress.Text;
                cm.Parameters.Add("@ADRESSPRT ", SqlDbType.VarChar).Value = txtaddresPermnt.Text;
                cm.Parameters.Add("@TREATINGDR", SqlDbType.VarChar).Value = txttreatingdr.Text;
                cm.Parameters.Add("@ADVPAY", SqlDbType.Decimal).Value = txtAdvpaid.Text;
                cm.Parameters.Add("@SENTBY", SqlDbType.VarChar).Value = txtsentby.Text;
                cm.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = txttotamt.Text;

                cm.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                // cm.Parameters.Add("@DOB", SqlDbType.VarChar).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd HH:mm");

                cm.ExecuteNonQuery();
            }
            string message = "alert('Updated Successfuly !')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //Session["LABID"] = TXTID.Text;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/LABORATORY/OutsideLabentry.aspx");
    }
    protected void grvlabst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void grvlabst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            string slno = grvlabst.DataKeys[e.RowIndex].Values["ID"].ToString();
            using (SqlCommand com = new SqlCommand("LAB_OUTSIDE_ENTRY", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                //SqlCommand cm = new SqlCommand("delete from OUTSIDE_LAB where ID='" + slno + "'", con);
                com.ExecuteNonQuery();
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grvlabst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd4 = new SqlCommand("LAB_OUTSIDE_ENTRY", con))
            {
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_OUTLAB_PAGE";
                cmd4.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                SqlDataAdapter Adp = new SqlDataAdapter(cmd4);
                //SqlDataAdapter Adp = new SqlDataAdapter("select ID,CONVERT(varchar, DATE, 105) AS DATE,PATIENTYPE,OPDNO,FIRSTNAME from OUTSIDE_LAB ORDER BY ID asc", con);
                DataTable dt = new DataTable();
                Adp.Fill(dt);
                grvlabst.DataSource = dt;
                grvlabst.PageIndex = e.NewPageIndex;
                grvlabst.DataKeyNames = new string[] { "ID" };
                grvlabst.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txtlabreqno_TextChanged(object sender, EventArgs e)
    {

    }
}