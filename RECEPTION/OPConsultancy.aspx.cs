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

public partial class RECEPTION_OPConsultancy : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    string AMOUNT;
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select id from tblOPConsultancy";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["id"].ToString();
        }
        num1 = string.Format("CN{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        LBLSLNO.Text = num1;

        dr.Close();
        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
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
                div1.Visible = true;
                div2.Visible = false;

                using (SqlCommand cmd = new SqlCommand("RECP_OPCONSULT_SELDEL", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DESGID";
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = "";
                    SqlDataAdapter Adp1 = new SqlDataAdapter(cmd);
                    //SqlDataAdapter Adp1 = new SqlDataAdapter("select * from tblStaff where DesgId = (select id from tblDesignation where DesgName='Doctor')", con);
                    DataTable Dt1 = new DataTable();
                    Adp1.Fill(Dt1);
                    dropdoctor.DataSource = Dt1;
                    dropdoctor.DataTextField = "Sname";
                    dropdoctor.DataValueField = "id";
                    dropdoctor.DataBind();
                    dropdoctor.Items.Insert(0, "Please Select");
                }
            }
            con.Close();
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            txtdate.Enabled = false;
            binddata();
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
        using (SqlCommand cmd1 = new SqlCommand("RECP_OPCONSULT_SELDEL", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOW_GRID_PAGE";
            cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
            cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = "";
            cmd1.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = "";
            SqlDataAdapter Adp1 = new SqlDataAdapter(cmd1);
            //SqlDataAdapter Adp = new SqlDataAdapter("select A.id,A.OPNo,A.CDate,A.fee,B.NAME,C.Sname from tblOPConsultancy A, PATIENT_REG_TABLE B,tblStaff C WHERE A.OPNo= B.ID and C.id=A.Staffid Order by id DESC", con);
            DataTable Dt = new DataTable();
            Adp1.Fill(Dt);
            GridView1.DataSource = Dt;
            GridView1.DataBind();
        }
        con.Close();
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand com = new SqlCommand("RECP_OPCONSULT_SELDEL", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SLECTED_EVENT";
                com.Parameters.Add("@id", SqlDbType.VarChar).Value = slno;
                com.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = "";
                com.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = "";
                //SqlDataAdapter Adp1 = new SqlDataAdapter(cmd1);
                //SqlCommand com = new SqlCommand("select A.id,A.OPNo, CDate ,A.Staffid,A.fee,B.NAME from tblOPConsultancy A, PATIENT_REG_TABLE B WHERE A.OPNo= B.ID AND A.id='" + slno + "'", con);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    btnSubmit.Visible = false;
                    btndelete.Visible = true;
                    btnupdate.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    txtopno.Text = dr["OPNo"].ToString();
                    dropdoctor.Text = dr["Staffid"].ToString();
                    txtfee.Text = dr["fee"].ToString();
                    txtdate.Text = dr["CDate"].ToString();
                    lblname.Text = dr["NAME"].ToString();
                    LBPAIDAMT.Text = dr["fee"].ToString();
                    div1.Visible = false;
                    div2.Visible = true;
                }
                dr.Close();
            }
            con.Close();
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
            using (SqlCommand com = new SqlCommand("RECP_OPCONSULT_SELDEL", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOW_GRID_PAGE";
                com.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
                com.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = "";
                com.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = "";
                SqlDataAdapter da = new SqlDataAdapter(com);
                //SqlDataAdapter da = new SqlDataAdapter("select A.id,A.OPNo,A.CDate,A.fee,B.NAME,C.Sname from tblOPConsultancy A, PATIENT_REG_TABLE B,tblStaff C WHERE A.OPNo= B.ID and C.id=A.Staffid Order by id DESC", con);
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
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtspid.Text == "")
            {
                string message = "alert('* Please!! Enter The OPD Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            using (SqlCommand com = new SqlCommand("RECP_OPCONSULT_SELDEL", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECTED_PATIENT_ID";
                com.Parameters.Add("@id", SqlDbType.VarChar).Value = txtspid.Text;
                com.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = "";
                com.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = "";
                //SqlDataAdapter da = new SqlDataAdapter(com);
                //SqlCommand com = new SqlCommand("select * from PATIENT_REG_TABLE where ID='" + txtspid.Text + "'", con);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    div2.Visible = true;
                    div1.Visible = false;
                    txtopno.Text = dr["ID"].ToString();
                    lblname.Text = dr["NAME"].ToString();

                }
                else
                {
                    string message = "alert('* Invalid OPD No..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    return;
                }
                dr.Close();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if(txtsmobile.Text=="")
            {
                string message = "alert('* Please!! Enter The Phone Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            using (SqlCommand com = new SqlCommand("RECP_OPCONSULT_SELDEL", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECTED_PATIENT_PHONE";
                com.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
                com.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = txtsmobile.Text;
                com.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = "";
                //SqlCommand com = new SqlCommand("select * from PATIENT_REG_TABLE where PHONE='" + txtsmobile.Text + "' ORDER BY ID DESC", con);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    div2.Visible = true;
                    div1.Visible = false;
                    txtopno.Text = dr["ID"].ToString();
                    lblname.Text = dr["NAME"].ToString();

                }
                else
                {
                    string message = "alert('*Data Not Found..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    return;
                }
                dr.Close();
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
            // Validation

            if (dropdoctor.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Doctor Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtfee.Text == "")
            {
                //string message = "alert('* Fields are mandatory.')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfee.Text = "0";
                // return;
            }

            else if (Convert.ToDecimal(txtfee.Text) <= 0)
            {
                //string message = "alert('* Fields are mandatory.')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                // return;
            }


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cmd = new SqlCommand("RECP_OPCONSULT_INSUPD", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "insert";
                //SqlCommand cmd = new SqlCommand("insert into tblOPConsultancy(id,ORGID,OPNo,Staffid,fee,CDate,UserId,Followup)values(@id,@ORGID,@OPNo,@Staffid,@fee,@CDate,@UserId,@Followup)", con);
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = LBLSLNO.Text;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = txtopno.Text;
                cmd.Parameters.Add("@Staffid", SqlDbType.Int).Value = Convert.ToInt32(dropdoctor.Text);
                cmd.Parameters.Add("@fee", SqlDbType.Decimal).Value = txtfee.Text;
                cmd.Parameters.Add("@CDate", SqlDbType.DateTime).Value = txtdate.Text;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
                cmd.Parameters.Add("@Followup", SqlDbType.Bit).Value = CheckBox1.Checked;
                cmd.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = LBLSLNO.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "CONSULTING PAYMENT" + "-" + dropdoctor.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtfee.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + txtfee.Text;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "DOCTOR CONSULTANCY";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = LBLSLNO.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "CONSULTING FEE" + "-" + dropdoctor.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtfee.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = txtfee.Text;
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "DOCTOR CONSULTANCY";
                cm.ExecuteNonQuery();
            }
           
            binddata();
            con.Close();
            Session["CON"] = LBLSLNO.Text;
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/Reception/opconreciept.aspx");
    }
    public void clearcontrol()
    {
        txtopno.Text = "";
        txtfee.Text = "";        
        txtdate.Text = "";        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            // Validation
            if (dropdoctor.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Doctor Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtfee.Text == "")
            {
                txtfee.Text = "0";
            }
            else if (Convert.ToDecimal(txtfee.Text) <= 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_OPCONSULT_INSUPD", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "update";
                //SqlCommand cmd1 = new SqlCommand("UPDATE tblOPConsultancy SET OPNo=@OPNo,Staffid=@Staffid,fee=@fee,CDate=@CDate WHERE id=@id", con);
                cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = txtopno.Text;
                cmd1.Parameters.Add("@Staffid", SqlDbType.Int).Value = Convert.ToInt32(dropdoctor.SelectedValue);
                cmd1.Parameters.Add("@fee", SqlDbType.Decimal).Value = txtfee.Text;
                cmd1.Parameters.Add("@CDate", SqlDbType.DateTime).Value = txtdate.Text;
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
                cmd1.Parameters.Add("@Followup", SqlDbType.Bit).Value = CheckBox1.Checked;
                cmd1.ExecuteNonQuery();
            }

            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "CONSULTING PAYMENT" + "-" + dropdoctor.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + LBPAIDAMT.Text;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "DOCTOR CONSULTANCY";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "CONSULTING PAYMENT" + "-" + dropdoctor.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtfee.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + txtfee.Text;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "DOCTOR CONSULTANCY";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "CONSULTING FEE" + "-" + dropdoctor.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "DOCTOR CONSULTANCY";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "CONSULTING FEE" + "-" + dropdoctor.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtfee.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = txtfee.Text;
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "DOCTOR CONSULTANCY";
                cm.ExecuteNonQuery();
            }
            binddata();
            con.Close();
            Session["CON"] = txtid.Text;
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/Reception/opconreciept.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        Response.Redirect("~/Reception/OPConsultancy.aspx");
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_OPCONSULT_SELDEL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = "";
                //SqlDataAdapter da = new SqlDataAdapter(com);
            //SqlCommand cmd = new SqlCommand("delete from tblOPConsultancy where id='" + txtid.Text + "'", con);
            cmd.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "CONSULTING PAYMENT" + "-" + dropdoctor.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + LBPAIDAMT.Text;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "DOCTOR CONSULTANCY";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "CONSULTING FEE" + "-" + dropdoctor.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtopno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "DOCTOR CONSULTANCY";
                cm.ExecuteNonQuery();
            }
            binddata();
         
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/Reception/OPConsultancy.aspx");
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_OPCONSULT_SELDEL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "CHECKED_EVENT";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = dropdoctor.SelectedValue;
                cmd.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = "";
                //SqlDataAdapter da = new SqlDataAdapter(com);
                //SqlCommand cmd1 = new SqlCommand("select FOLLOWUP from tblStaff WHERE id=@id", con);
                //cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = dropdoctor.SelectedValue;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    LBLFOLLOWUP.Text = dr["FOLLOWUP"].ToString();
                }
                dr.Close();
            }
            using (SqlCommand cmd1 = new SqlCommand("RECP_OPCONSULT_SELDEL", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "CHECKED_EVENT1";
                cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = txtopno.Text;
                //SqlCommand cmd2 = new SqlCommand("select CDate from tblOPConsultancy WHERE OPNo=@OPNo ORDER BY id DESC", con);
                //cmd2.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = txtopno.Text;
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    LBLLASTDATE.Text = dr["CDate"].ToString();
                }
                dr.Close();
            }

            string TODAY = DateTime.Now.ToString("yyyy-MM-dd");
            string LASTDAY = LBLLASTDATE.Text;

            TimeSpan TIME = Convert.ToDateTime(TODAY) - Convert.ToDateTime(LBLLASTDATE.Text);
            double NOFDAYS = TIME.TotalDays;

            if (Convert.ToDouble(LBLFOLLOWUP.Text) < Convert.ToDouble(NOFDAYS))
            {
                CheckBox1.Checked = false;
                string message = "alert('* Follow up time has expired.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                txtfee.Text = "0.00";
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void dropdoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_OPCONSULT_SELDEL", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DROP_EVENT";
                //cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@OPNo", SqlDbType.VarChar).Value = "";

                //SqlCommand cmd1 = new SqlCommand("select fee from tblStaff WHERE id=@id", con);
                cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = dropdoctor.SelectedValue;
                dr = cmd1.ExecuteReader();
                if (dr.Read() && CheckBox1.Checked == false)
                {
                    txtfee.Text = dr["FEE"].ToString();
                }
                else
                {
                    txtfee.Text = "0.00";
                }
                dr.Close();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
}