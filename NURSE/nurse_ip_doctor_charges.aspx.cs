using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class NURSE_nurse_ip_doctor_charges : System.Web.UI.Page
{
    string num1 = "PR000";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    SqlCommand com, cmd, cmd1, cmd2;
    SqlDataReader dr, dr1, dr2;
    SqlDataAdapter da, da1;
    DataSet ds = new DataSet();
    DataTable dt;
    DataMathods OBJ_METHOD = new DataMathods();
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = " select ID from dbo.ip_doctor_charge";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("DC{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;


        dr.Close();
        con.Close();
    }
    public void binddata()
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }

            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from ip_doctor_charge WHERE Branch_ID = " + Session["Branch"] + " order by id desc", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.SelectedIndex = 0;
                GridView2.DataSource = Ds;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            //SqlDataAdapter da = new SqlDataAdapter("select * from ip_doctor_charge order by ID desc", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //GridView2.SelectedIndex = 0;
            //GridView2.DataSource = dt;
            //GridView2.DataKeyNames = new string[] { "ID" };
            //GridView2.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
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
            DataSet Ds = OBJ_METHOD.Get_DataSet("select DISTINCT DOCTOR from Doctor_charges WHERE Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddldoctor.DataSource = Ds;
                ddldoctor.DataTextField = "DOCTOR";
                ddldoctor.DataValueField = "DOCTOR";
                ddldoctor.DataBind();
                ddldoctor.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT DISTINCT BEDNO FROM BED_TABLE WHERE Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                ddlbed.DataSource = Ds1;
                ddlbed.DataTextField = "BEDNO";
                ddlbed.DataValueField = "BEDNO";
                ddlbed.DataBind();
                ddlbed.Items.Insert(0, new ListItem("Please Select", "0"));
                binddata();
            }
            #region Old code
            //SqlDataAdapter da = new SqlDataAdapter("select DISTINCT DOCTOR from Doctor_charges", con);
            //DataTable ds = new DataTable();
            //da.Fill(ds);
            //ddldoctor.DataSource = ds;
            //ddldoctor.DataTextField = "DOCTOR";
            //ddldoctor.DataValueField = "DOCTOR";
            //ddldoctor.DataBind();
            //ddldoctor.Items.Insert(0,new ListItem("Please Select","0"));

            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT BEDNO FROM BED_TABLE", con);
            //DataTable dt1 = new DataTable();
            //da1.Fill(dt1);
            ////dropbedno.SelectedIndex = 0;
            //ddlbed.DataSource = dt1;
            //ddlbed.DataTextField = "BEDNO";
            //ddlbed.DataValueField = "BEDNO";
            //ddlbed.DataBind();
            //ddlbed.Items.Insert(0,new ListItem("Please Select","0"));
            //con.Close();
            #endregion
            Txtdate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            div1.Visible = false;
            
        }
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from ip_doctor_charge where ID='" + slno + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                div1.Visible = true;
                btnSubmit.Visible = false;
                btnupdate.Visible = true;
                btndelete.Visible = true;
                txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                txtip.Text = Ds.Tables[0].Rows[0]["IPNO"].ToString();
                lblip.Text = Ds.Tables[0].Rows[0]["IPNO"].ToString();
                lblbed.Text = Ds.Tables[0].Rows[0]["bed"].ToString();
                //ddlbed.Items.FindByValue(dr["bed"].ToString()).Selected = true;
                ddlbed.SelectedValue = Ds.Tables[0].Rows[0]["bed"].ToString();
                //ddlbed.DataTextField = Ds.Tables[0].Rows[0]["bed"].ToString();
                ddldoctor.SelectedValue = Ds.Tables[0].Rows[0]["Doctor"].ToString();
                lblprice.Text = Ds.Tables[0].Rows[0]["price"].ToString();
                Txtdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["date"]).ToString("dd-MM-yyyy");
            }
            
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select NAME from dbo.ADMISSION_TABLE where VN='" + lblip.Text + "'", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                lblname.Text = Ds1.Tables[0].Rows[0]["NAME"].ToString();
            }
           
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE,B.CORPORATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.VN= '" + lblip.Text + "' and A.ORGID='" + lblorgid.Text + "'", false, false);

            if (Ds2.Tables[0].Rows.Count > 0)
            {
                lblcorpo.Text = Ds2.Tables[0].Rows[0]["CORPORATE"].ToString();
            }
            #region old code
            //SqlCommand com = new SqlCommand("select * from ip_doctor_charge where ID='" + slno + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    div1.Visible = true;
            //    btnSubmit.Visible = false;
            //    btnupdate.Visible = true;
            //    btndelete.Visible = true;
            //    txtid.Text = dr["ID"].ToString();
            //    txtip.Text = dr["IPNO"].ToString();
            //    lblip.Text = dr["IPNO"].ToString();
            //    lblbed.Text = dr["bed"].ToString();
            //    ddlbed.SelectedItem.Text = dr["bed"].ToString();
            //    ddldoctor.SelectedItem.Text = dr["Doctor"].ToString();
            //    lblprice.Text = dr["price"].ToString();
            //    Txtdate.Text = Convert.ToDateTime(dr["date"].ToString()).ToString("dd-MM-yyyy");
            //}
            //dr.Close();

            //SqlCommand com1 = new SqlCommand("select NAME from dbo.ADMISSION_TABLE where VN='" + lblip.Text + "'", con);
            //dr1 = com1.ExecuteReader();
            //if (dr1.Read())
            //{
            //    lblname.Text = dr1["NAME"].ToString();
            //}
            //dr1.Close();

            //SqlCommand cmd1 = new SqlCommand("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE,B.CORPORATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.VN= '" + lblip.Text + "' and A.ORGID='" + lblorgid.Text + "' ", con);
            //dr2 = cmd1.ExecuteReader();
            //if (dr2.Read())
            //{
            //    lblcorpo.Text = dr2["CORPORATE"].ToString();
            //}
            //dr2.Close();

            //con.Close();
            #endregion
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
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from ip_doctor_charge WHERE Branch_ID = " + Session["Branch"] + " order by id desc", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.SelectedIndex = 0;
                GridView2.DataSource = Ds;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            #region oldcode
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //SqlDataAdapter Adp = new SqlDataAdapter("select * from ip_doctor_charge order by ID desc", con);
            //DataTable Dt = new DataTable();
            //Adp.Fill(Dt);
            //GridView2.DataSource = Dt;
            //GridView2.PageIndex = e.NewPageIndex;
            //GridView2.DataKeyNames = new string[] { "ID" };
            //GridView2.DataBind();
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {

            if (lblip.Text == "" && lblbed.Text == "")
            {

                string message = "alert('Please!! Click On Show Button To Save The Page..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Txtdate.Text == "")
            {
                string message = "alert('Please!! Enter The Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (ddldoctor.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Doctor To Save The Page..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //insert into ip_doctor_charge (ID,IPNO,bed,Doctor,price,date) values (@ID,@IPNO,@bed,@Doctor,@price,@date)
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[13];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, Txtdate.Text);
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblip.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, lblbed.Text);
            SQL_PARAMS1[4] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "DOCTOR CHARGES");
            SQL_PARAMS1[5] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, lblprice.Text);
            SQL_PARAMS1[6] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
            SQL_PARAMS1[7] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, lblprice.Text);
            SQL_PARAMS1[8] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);
            SQL_PARAMS1[9] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "OTHER CHARES");
            SQL_PARAMS1[10] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS1[11] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[12] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                SqlParameter[] SQL_PARAMS2 = new SqlParameter[9];

                SQL_PARAMS2[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS2[1] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 500, lblip.Text);
                SQL_PARAMS2[2] = OBJ_METHOD.createParams("@bed", SqlDbType.VarChar, 500, lblbed.Text);
                SQL_PARAMS2[3] = OBJ_METHOD.createParams("@Doctor", SqlDbType.VarChar, 200, ddldoctor.SelectedItem.Text);
                SQL_PARAMS2[4] = OBJ_METHOD.createParams("@price", SqlDbType.VarChar, 200, lblprice.Text);
                SQL_PARAMS2[5] = OBJ_METHOD.createParams("@date", SqlDbType.DateTime, 0, Txtdate.Text);
                SQL_PARAMS2[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
                SQL_PARAMS2[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                SQL_PARAMS2[8] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

                OBJ_METHOD.ExecuteProceedure("SP_DOCTOR_CHARGES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearfield();
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
            }
            #region oldcode
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //auto();
            ////SqlCommand cmd1 = new SqlCommand("insert into ip_doctor_charge (ID,IPNO,bed,Doctor,price,date) values (@ID,@IPNO,@bed,@Doctor,@price,@date)", con);
            //using (SqlCommand cmd1 = new SqlCommand("SP_DOCTOR_CHARGES", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = lblip.Text;
            //    cmd1.Parameters.Add("@bed", SqlDbType.VarChar).Value = lblbed.Text;
            //    cmd1.Parameters.Add("@Doctor", SqlDbType.VarChar).Value = ddldoctor.SelectedItem.Text;
            //    cmd1.Parameters.Add("@price", SqlDbType.VarChar).Value = lblprice.Text;
            //    cmd1.Parameters.Add("@date", SqlDbType.DateTime).Value = Txtdate.Text;
            //    cmd1.ExecuteNonQuery();
            //}
            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Txtdate.Text;
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DOCTOR CHARGES";
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = lblprice.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lblprice.Text;
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
            //    cm.ExecuteNonQuery();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    public void clearfield()
    {
        div1.Visible = false;
        txtip.Text = "";
        Txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        ddlbed.SelectedIndex = 0;
        ddldoctor.SelectedIndex = 0;
        lblip.Text = "";
        lblname.Text = "";
        lblbed.Text = "";
        btnSubmit.Visible = true;
        btnupdate.Visible = false;
        btndelete.Visible = false;
        binddata();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (Txtdate.Text == "")
            {
                string message = "alert('Please!! Enter The Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, lblprice.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE1");

            OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[7];

            
                SQL_PARAMS[0] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, lblprice.Text);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, lblbed.Text);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "DOCTOR CHARGES");
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, lblprice.Text);
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SqlParameter[] SQL_PARAMS2 = new SqlParameter[7];

                    SQL_PARAMS2[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS2[1] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 500, lblip.Text);
                    SQL_PARAMS2[2] = OBJ_METHOD.createParams("@bed", SqlDbType.VarChar, 500, lblbed.Text);
                    SQL_PARAMS2[3] = OBJ_METHOD.createParams("@Doctor", SqlDbType.VarChar, 200, ddldoctor.SelectedItem.Text);
                    SQL_PARAMS2[4] = OBJ_METHOD.createParams("@price", SqlDbType.VarChar, 200, lblprice.Text);
                    SQL_PARAMS2[5] = OBJ_METHOD.createParams("@date", SqlDbType.DateTime, 0, Txtdate.Text);
                    SQL_PARAMS2[6] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

                    OBJ_METHOD.ExecuteProceedure("SP_DOCTOR_CHARGES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        clearfield();
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                    }
                }
            }
        #region oldcode

            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cm1 = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //{
            //    cm1.CommandType = CommandType.StoredProcedure;
            //    cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
            //    cm1.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Txtdate.Text;
            //    cm1.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            //    cm1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
            //    cm1.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DOCTOR CHARGES";
            //    cm1.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = lblprice.Text;
            //    cm1.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm1.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //    cm1.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lblprice.Text;
            //    cm1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //    cm1.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
            //    cm1.ExecuteNonQuery();
            //}
            ////SqlCommand cmd1 = new SqlCommand("Update ip_doctor_charge set IPNO=@IPNO,bed=@bed,Doctor=@Doctor,price=@price,date=@date where ID=@ID", con);
            //using (SqlCommand cmd1 = new SqlCommand("SP_DOCTOR_CHARGES", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = lblip.Text;
            //    cmd1.Parameters.Add("@bed", SqlDbType.VarChar).Value = lblbed.Text;
            //    cmd1.Parameters.Add("@Doctor", SqlDbType.VarChar).Value = ddldoctor.SelectedItem.Text;
            //    cmd1.Parameters.Add("@price", SqlDbType.VarChar).Value = lblprice.Text;
            //    cmd1.Parameters.Add("@date", SqlDbType.DateTime).Value = Txtdate.Text;
            //    cmd1.ExecuteNonQuery();
            //}


            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Txtdate.Text;
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DOCTOR CHARGES";
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = lblprice.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lblprice.Text;
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
            //    cm.ExecuteNonQuery();
            //}
            //con.Close();
#endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not updated.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[4];
                
                SQL_PARAMS[0] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "DOCTOR CHARGES");
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SQL_PARAMS = new SqlParameter[2];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                    OBJ_METHOD.ExecuteProceedure("SP_DOCTOR_CHARGES_DELETE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        clearfield();
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                    }
                }
            }
            #region oldcode
            //SqlCommand com = new SqlCommand("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    string message = "alert('* You Cant delete.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            //else
            //{
            //    dr.Close();

            //    SqlCommand com1 = new SqlCommand("delete from ip_doctor_charge where ID='" + txtid.Text + "'", con);
            //    com1.ExecuteNonQuery();

            //    using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //    {
            //        cm.CommandType = CommandType.StoredProcedure;
            //        cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //        cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Txtdate.Text;
            //        cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //        cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            //        cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
            //        cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DOCTOR CHARGES";
            //        cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = lblprice.Text;
            //        cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //        cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //        cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lblprice.Text;
            //        cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //        cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
            //        cm.ExecuteNonQuery();
            //    }
            //    binddata();
            //}

            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearfield();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtip.Text == "" && ddlbed.SelectedIndex == 0)
            {
                string message = "alert('Please!! Either Enter IPNO OR select BED..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (ddlbed.SelectedIndex == 0)
            {
                DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE,B.CORPORATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.VN= '" + txtip.Text.Trim() + "' and A.ORGID='" + lblorgid.Text + "' and A.Branch_ID='" + Session["Branch"] + "'", false, false);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblip.Text = Ds.Tables[0].Rows[0]["VN"].ToString();
                    lblname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
                    lblbed.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                    lblcorpo.Text = Ds.Tables[0].Rows[0]["CORPORATE"].ToString();
                    div1.Visible = true;
                }
                else
                {
                    string message = "alert('No record found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                //SqlCommand cmd1 = new SqlCommand("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE,B.CORPORATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.VN= '" + txtip.Text.Trim() + "' and A.ORGID='" + lblorgid.Text + "' ", con);
                //dr = cmd1.ExecuteReader();
                //if (dr.Read())
                //{
                //    lblip.Text = dr["VN"].ToString();
                //    lblname.Text = dr["PNAME"].ToString();
                //    lblbed.Text = dr["BEDNO"].ToString();
                //    lblcorpo.Text = dr["CORPORATE"].ToString();
                //    div1.Visible = true;
                //}
                //else 
                //{
                //    string message = "alert('No record found')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //    return;
                //}
                //dr.Close();
                //con.Close();
            }
            else
            {
                DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE,B.CORPORATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.BEDNO= '" + ddlbed.Text + "' and A.ORGID='" + lblorgid.Text + "' and A.Branch_ID='" + Session["Branch"] + "'", false, false);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblip.Text = Ds.Tables[0].Rows[0]["VN"].ToString();
                    lblname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
                    lblbed.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                    lblcorpo.Text = Ds.Tables[0].Rows[0]["CORPORATE"].ToString();
                    div1.Visible = true;
                }
                else
                {
                    string message = "alert('No record found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                //con.Open();

                //SqlCommand cmd1 = new SqlCommand("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE,B.CORPORATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.BEDNO= '" + ddlbed.Text + "' and A.ORGID='" + lblorgid.Text + "'", con);
                //dr = cmd1.ExecuteReader();
                //if (dr.Read())
                //{
                //    lblip.Text = dr["VN"].ToString();
                //    lblname.Text = dr["PNAME"].ToString();
                //    lblbed.Text = dr["BEDNO"].ToString();
                //    lblcorpo.Text = dr["CORPORATE"].ToString();
                //    div1.Visible = true;
                //}
                //else
                //{
                //    string message = "alert('No record found')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //    return;
                //}
                //dr.Close();
                //con.Close();

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void ddldoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Txtdate.Text == "")
        {
            string message = "alert('Please!! Enter The Date..')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        else
        {
            try
            {
                
                if (lblcorpo.Text != "0")
                {
                    // string date = System.DateTime.Today.ToString("dd/MM/yy");
                    string Date = Convert.ToDateTime(Txtdate.Text).ToString("MM-dd-yyyy");
                    DataSet Ds = OBJ_METHOD.Get_DataSet("select * from Doctor_charges where APPLY_DATE=(select MAX(APPLY_DATE)As LDATE from Doctor_charges where APPLY_DATE<='" + Date + "') and CORPORATE_ID='" + lblcorpo.Text + "' and DOCTOR='" + ddldoctor.SelectedItem.Text + "'", false, false);

                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        lblprice.Text = Ds.Tables[0].Rows[0]["CHARGES"].ToString();
                    }
                    //SqlCommand cmd1 = new SqlCommand("select * from Doctor_charges where APPLY_DATE=(select MAX(APPLY_DATE)As LDATE from Doctor_charges where APPLY_DATE<='" + Date + "') and CORPORATE_ID='" + lblcorpo.Text + "' and DOCTOR='" + ddldoctor.SelectedItem.Text + "'", con);
                    //dr1 = cmd1.ExecuteReader();
                    //if (dr1.Read())
                    //{
                    //    lblprice.Text = dr1["CHARGES"].ToString();
                    //}
                    //dr1.Close();

                }
                else
                {
                    DataSet Ds = OBJ_METHOD.Get_DataSet("select * from Doctor_charges where CORPORATE_ID is null and DOCTOR='" + ddldoctor.SelectedItem.Text + "'", false, false);

                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        lblprice.Text = Ds.Tables[0].Rows[0]["CHARGES"].ToString();
                    }
                    //SqlCommand cmd1 = new SqlCommand("select * from Doctor_charges where CORPORATE_ID is null and DOCTOR='" + ddldoctor.SelectedItem.Text + "'", con);
                    //dr1 = cmd1.ExecuteReader();
                    //if (dr1.Read())
                    //{
                    //    lblprice.Text = dr1["CHARGES"].ToString();
                    //}
                    //dr1.Close();

                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
        }
    }
}