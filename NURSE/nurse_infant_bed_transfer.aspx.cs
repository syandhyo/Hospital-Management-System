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

public partial class NURSE_nurse_infant_bed_transfer : System.Web.UI.Page
{
    SqlDataReader dr;
    SqlConnection con;
    DataMathods OBJ_METHOD = new DataMathods();

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
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }

            DataSet Ds = OBJ_METHOD.Get_DataSet("select VN FROM BED_TABLE where VN like '%IP%' and Branch_FY='" + Session["Branch"] + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataSource = Ds;
                DropDownList1.DataTextField = "VN";
                DropDownList1.DataValueField = "VN";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0,new ListItem("Please Select","0"));
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT DISTINCT NAME FROM WARD_TABLE WHERE ORGID='" + lblorgid.Text + "' and Branch_FY='" + Session["Branch"] + "'", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropward.DataSource = Ds1;
                dropward.DataTextField = "NAME";
                dropward.DataBind();
                dropward.Items.Insert(0,new ListItem("Please Select","0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }

        // SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM BED_TABLE ", con);
        //SqlDataAdapter da1 = new SqlDataAdapter("select VN FROM BED_TABLE where VN like '%IP%' ", con);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);
        ////dropbedno.SelectedIndex = 0;
        //DropDownList1.DataSource = dt1;
        //DropDownList1.DataTextField = "VN";
        //DropDownList1.DataValueField = "VN";
        //DropDownList1.DataBind();
        //DropDownList1.Items.Insert(0, "Please Select");

        //SqlDataAdapter da2 = new SqlDataAdapter("SELECT DISTINCT NAME FROM BED_MATRIX_TABLE WHERE ORGID='" + lblorgid.Text + "'", con);
        //DataTable dt2 = new DataTable();
        //da2.Fill(dt2);
        ////dropbedno.SelectedIndex = 0;
        //dropward.DataSource = dt2;
        //dropward.DataTextField = "NAME";
        //dropward.DataBind();
        //dropward.Items.Insert(0, "Please Select");
        //con.Close();
    }
    
    protected void btnshowbed_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT * FROM NEWBORN_TABLE WHERE Mother_id= '" + DropDownList1.Text + "' and ORGID='" + lblorgid.Text + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                lblpid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                lblname.Text = Ds.Tables[0].Rows[0]["Mother_id"].ToString();
                
            }
            else
            {
                lblpid.Text = "";
                lblname.Text = "";
                lblbed.Text = "";
                lblward.Text = "";
                string message = "alert('Infant Not Found. Try New PID.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT * FROM BED_TABLE Where VN= '" + lblpid.Text + "' and ORGID='" + lblorgid.Text + "'", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                lblbed.Text = Ds1.Tables[0].Rows[0]["BEDNO"].ToString();
                lblward.Text = Ds1.Tables[0].Rows[0]["WARD"].ToString();
            }
            else
            {
                lblbed.Text = "";
                lblward.Text = "";
                string message = "alert('No Bed Assigned.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            #region oldcode
            //SqlCommand cm = new SqlCommand("SELECT * FROM NEWBORN_TABLE WHERE Mother_id= '" + DropDownList1.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            //dr = cm.ExecuteReader();
            //if (dr.Read())
            //{
            //    lblpid.Text = dr["ID"].ToString();
            //    lblname.Text = dr["Mother_id"].ToString();
            //    //lblbed.Text = dr["BEDNO"].ToString();
            //    // lblward.Text = dr["WARD"].ToString();
            //    //lblinsurance.Text = dr["INSURANCE"].ToString();
            //}
            //else
            //{
            //    dr.Close();
            //    lblpid.Text = "";
            //    lblname.Text = "";
            //    lblbed.Text = "";
            //    lblward.Text = "";
            //    string message = "alert('Infant No.Not Found. Try New PID.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;

            //}
            //dr.Close();
            //// SqlCommand cm1 = new SqlCommand("SELECT * FROM BED_TABLE WHERE PNAME= '" + DropDownList1.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            //SqlCommand cm1 = new SqlCommand("SELECT * FROM BED_TABLE WHERE VN= '" + DropDownList1.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            //dr = cm1.ExecuteReader();
            //if (dr.Read())
            //{
            //    //lblpid.Text = dr["ID"].ToString();
            //    //lblname.Text = dr["Mother_id"].ToString();
            //    lblbed.Text = dr["BEDNO"].ToString();
            //    lblward.Text = dr["WARD"].ToString();
            //    //lblinsurance.Text = dr["INSURANCE"].ToString();
            //}
            //else
            //{
            //    dr.Close();
            //    lblpid.Text = "";
            //    lblname.Text = "";
            //    lblbed.Text = "";
            //    lblward.Text = "";
            //    string message = "alert('No Bed Assigned.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;

            //}
            //dr.Close();
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (lblpid.Text == "")
            {
                string message = "alert('Select Infant First.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                con.Close();
                return;
            }
            if (dropward.SelectedIndex == 0)
            {
                string message = "alert('*Select Ward First.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                con.Close();
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[12];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblpid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, lblname.Text);

            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@WARDNAME", SqlDbType.VarChar, 500, lblward.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@NWARDNAME", SqlDbType.VarChar, 500, dropward.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@CBEDNO", SqlDbType.VarChar, 500, lblbed.Text);

            SQL_PARAMS[9] = OBJ_METHOD.createParams("@NBEDNO", SqlDbType.VarChar, 500, dropbed.Text);
            if (txtdate.Text == "")
            {
                txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, txtdate.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@INSURANCE", SqlDbType.VarChar, 500, lblinsurance.Text);

            OBJ_METHOD.ExecuteProceedure("usp_BEDTRANSFER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
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
    public void clearcontrol()
    {
        lblpid.Text = "";
        lblname.Text = "";
        lblward.Text = "";
        lblpid.Text = "";
        lblbed.Text = "";
        txtdate.Text = "";
        dropbed.SelectedItem.Text = "";
        dropbed.DataSource = null;
        dropbed.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        clearcontrol();
    }
    protected void dropward_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_STATUS_NAME");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID      ", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, dropward.SelectedItem.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_BED_TRANSFER", false, true, SQL_PARAMS1);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropbed.DataSource = Ds;
                dropbed.DataTextField = "BEDNO";
                dropbed.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void Btnrelease_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (lblbed.Text == "")
            {
                string message = "alert('No Bed Selected. Try New ID.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "RELESE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@CBEDNO", SqlDbType.VarChar, 500, lblbed.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblpid.Text);

                SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                OBJ_METHOD.ExecuteProceedure("usp_BEDTRANSFER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearcontrol();
                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                //con.Open();
                //SqlCommand cmd1 = new SqlCommand(" UPDATE BED_MATRIX_TABLE SET STATUS='AVAILABLE' WHERE BEDNO='" + lblbed.Text + "'", con);
                //cmd1.ExecuteNonQuery();
                //SqlCommand cmd2 = new SqlCommand("DELETE FROM BED_TABLE WHERE VN='" + lblpid.Text + "'", con);
                //cmd2.ExecuteNonQuery();
                ////SqlCommand cm = new SqlCommand("delete from NEWBORN_TABLE where ID='" + lblpid.Text + "'", con);
                ////cm.ExecuteNonQuery();
                //con.Close();
            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Bed not released.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }

    }
}