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

public partial class ACCOUNTS_PayrollEntry : System.Web.UI.Page
{
    SqlDataReader dr;
    SqlConnection con;
    Double Totalamt,Deduction,Bal;
     

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

            SqlDataAdapter Adp = new SqlDataAdapter("select EMPID,Sname from tblStaff", con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            dropname.DataSource = Dt;
            dropname.DataTextField = "EMPID";
            dropname.DataValueField = "EMPID";
            dropname.DataBind();
            dropname.Items.Insert(0, "Please Select");
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


        using (SqlCommand cmd = new SqlCommand("Pay_roll_Select", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ALL";
           // cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        con.Close();

        //SqlDataAdapter da = new SqlDataAdapter("SELECT A.id,A.Name,B.Basic,B.HRA,B.Con,B.Med,A.PF,A.PT,A.TDS,A.TotalSal,A.Month,A.Year,A.DateStamp FROM tblPayroll A,tblstaff B WHERE A.Empid=B.EMPID order by A.id desc", con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
            SqlCommand cm = new SqlCommand("delete from tblPayroll where id='" + slno + "'", con);
            cm.ExecuteNonQuery();

            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[12].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this Staff info ?');";
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM tblPayroll A,tblstaff B where A.id ='" + slno + "' and A.Empid=B.EMPID", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = dr["ID"].ToString();
                dropname.SelectedValue = dr["Empid"].ToString();
                txtbasic.Text = dr["Basic"].ToString();
                txthra.Text = dr["HRA"].ToString();
                txtcon.Text = dr["Con"].ToString();
                txtmed.Text = dr["Med"].ToString();
                dropmonth.Text = dr["Month"].ToString();
                dropyear.Text = dr["Year"].ToString();
                txtpf.Text = dr["PF"].ToString();
                txtpt.Text = dr["PT"].ToString();
                txttds.Text = dr["TDS"].ToString();
                txtdate.Text = dr["DateStamp"].ToString();
                txtodeduc.Text = dr["OtherDeductions"].ToString();
                txtDays.Text = dr["WorkingDays"].ToString();
                dropdm.Text = dr["Dm"].ToString();
                txtad.Text = dr["Ad"].ToString();
                lbltamt.Text = dr["TotalSal"].ToString();
                lbldeduction.Text = dr["totald"].ToString();
                lblcal.Text = dr["netpay"].ToString();
                lblname.Text = dr["Name"].ToString();
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd = new SqlCommand("Pay_roll_Select", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }

        con.Close();
        //SqlDataAdapter da = new SqlDataAdapter("SELECT A.id,A.Name,B.Basic,B.HRA,B.Con,B.Med,A.PF,A.PT,A.TDS,A.TotalSal,A.Month,A.Year FROM tblPayroll A,tblstaff B WHERE A.Empid=B.EMPID ", con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //GridView1.DataSource = dt;
        //GridView1.PageIndex = e.NewPageIndex;
        //GridView1.DataKeyNames = new string[] { "id" };
        //GridView1.DataBind();
        //con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            // Validation
            if (dropname.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Emplyee Id.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropmonth.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Month.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropyear.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Year.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtpf.Text == "" || Convert.ToDouble(txtpf.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtodeduc.Text == "" || Convert.ToDouble(txtodeduc.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (lbldeduction.Text == "")
            {
                string message = "alert('* Please Press Calculate Button.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtpt.Text == "" || Convert.ToDouble(txtpt.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txttds.Text == "" || Convert.ToDouble(txttds.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            else if (Convert.ToDouble(txtDays.Text) <= 0 || Convert.ToDouble(txtDays.Text) > 31 || txtDays.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (Convert.ToDouble(txtad.Text) < 0 || Convert.ToDouble(txtad.Text) > 31 || txtad.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand cd = new SqlCommand("select * from tblpayroll where Empid='" + dropname.Text + "'and Month='" + dropmonth.Text + "' and Year='" + dropyear.Text + "'", con);
            dr = cd.ExecuteReader();
            if (dr.Read())
            {
                string message = "alert('Payslip for this employee already genereted.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            dr.Close();

           // SqlCommand cmd = new SqlCommand("insert into tblPayroll(ORGID,Empid,Name,PF,PT,TDS,Month,Year,DateStamp,UserId,TotalSal,WorkingDays,OtherDeductions,Dm,Ad,totald,netpay)values(@ORGID,@Empid,@Name,@PF,@PT,@TDS,@Month,@Year,@DateStamp,@UserId,@TotalSal,@WorkingDays,@OtherDeductions,@Dm,@Ad,@totald,@netpay)", con);



            using (SqlCommand cmd = new SqlCommand("Sp_New_PayRoll", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@Empid", SqlDbType.VarChar).Value = dropname.Text;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = lblname.Text;
                cmd.Parameters.Add("@PF", SqlDbType.VarChar).Value = txtpf.Text;
                cmd.Parameters.Add("@PT", SqlDbType.VarChar).Value = txtpt.Text;
                cmd.Parameters.Add("@TDS", SqlDbType.VarChar).Value = txttds.Text;
                cmd.Parameters.Add("@Month", SqlDbType.VarChar).Value = dropmonth.Text;
                cmd.Parameters.Add("@Year", SqlDbType.VarChar).Value = dropyear.Text;
                cmd.Parameters.Add("@DateStamp", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
                cmd.Parameters.Add("@TotalSal", SqlDbType.VarChar).Value = lbltamt.Text;
                cmd.Parameters.Add("@WorkingDays", SqlDbType.VarChar).Value = txtDays.Text;
                cmd.Parameters.Add("@OtherDeductions", SqlDbType.VarChar).Value = txtodeduc.Text;
                cmd.Parameters.Add("@Dm", SqlDbType.VarChar).Value = dropdm.Text;
                cmd.Parameters.Add("@Ad", SqlDbType.VarChar).Value = txtad.Text;
                cmd.Parameters.Add("@totald", SqlDbType.VarChar).Value = lbldeduction.Text;
                cmd.Parameters.Add("@netpay", SqlDbType.VarChar).Value = lblcal.Text;
                cmd.ExecuteNonQuery();
            }

            clearcontrol();
            string message1 = "alert('Successfully Saved')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/Accounts/PayrollEntry.aspx");
    }
    public void clearcontrol()
    {

        txtpf.Text = "";
        txtpt.Text = "";
        txttds.Text = "";        
        txtdate.Text = "";

        txtbasic.Text = "";
        txthra.Text = "";
        txtcon.Text = "";
        txtmed.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtpf.Text == "" || Convert.ToDouble(txtpf.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (lbldeduction.Text == "")
            {
                string message = "alert('* Please Press Calculate Button.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtodeduc.Text == "" || Convert.ToDouble(txtodeduc.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtpt.Text == "" || Convert.ToDouble(txtpt.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txttds.Text == "" || Convert.ToDouble(txttds.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            else if (Convert.ToDouble(txtDays.Text) <= 0 || Convert.ToDouble(txtDays.Text) > 31 || txtDays.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (Convert.ToDouble(txtad.Text) < 0 || Convert.ToDouble(txtad.Text) > 31 || txtad.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand cmd1 = new SqlCommand("UPDATE tblPayroll SET PF=@PF,PT=@PT,TDS=@TDS,Month=@Month,Year=@Year,TotalSal=@TotalSal WHERE id=@id", con);
            cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            cmd1.Parameters.Add("@PF", SqlDbType.Decimal).Value = txtpf.Text;
            cmd1.Parameters.Add("@PT", SqlDbType.Decimal).Value = txtpt.Text;
            cmd1.Parameters.Add("@TDS", SqlDbType.Decimal).Value = txttds.Text;
            cmd1.Parameters.Add("@Month", SqlDbType.VarChar).Value = dropmonth.Text;
            cmd1.Parameters.Add("@Year", SqlDbType.VarChar).Value = dropyear.Text;
            cmd1.Parameters.Add("@TotalSal", SqlDbType.Decimal).Value = lblcal.Text;
            cmd1.ExecuteNonQuery();
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/Accounts/PayrollEntry.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounts/PayrollEntry.aspx");
    }

    protected void dropname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand("select * FROM tblStaff WHERE EMPID='" + dropname.SelectedValue + "'", con);
            dr = cm.ExecuteReader();
            if (dr.Read())
            {
                txtbasic.Text = dr["Basic"].ToString();
                txthra.Text = dr["HRA"].ToString();
                txtcon.Text = dr["Con"].ToString();
                txtmed.Text = dr["Med"].ToString();
                lblname.Text = dr["Sname"].ToString();

            }
            dr.Close();

            con.Close();


            Totalamt = Math.Round((Convert.ToDouble(txtbasic.Text)) + Convert.ToDouble(txthra.Text) + Convert.ToDouble(txtcon.Text) + Convert.ToDouble(txtmed.Text));
            lbltamt.Text = Totalamt.ToString();

            Deduction = Math.Round((Convert.ToDouble(txtpf.Text)) + Convert.ToDouble(txtpt.Text) + Convert.ToDouble(txttds.Text) + Convert.ToDouble(txtodeduc.Text));
            lbldeduction.Text = Deduction.ToString();
            Bal = Totalamt - Deduction;
            lblcal.Text = Bal.ToString();
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncal_Click(object sender, EventArgs e)
    {
        try
        {

            Totalamt = Math.Round((Convert.ToDouble(txtbasic.Text)) + Convert.ToDouble(txthra.Text) + Convert.ToDouble(txtcon.Text) + Convert.ToDouble(txtmed.Text) );
            lbltamt.Text = Totalamt.ToString();

            Deduction = Math.Round((Convert.ToDouble(txtpf.Text)) + Convert.ToDouble(txtpt.Text) + Convert.ToDouble(txttds.Text) + Convert.ToDouble(txtodeduc.Text));
            lbldeduction.Text = Deduction.ToString();
            Bal = Totalamt - Deduction;
            lblcal.Text = Bal.ToString();
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}