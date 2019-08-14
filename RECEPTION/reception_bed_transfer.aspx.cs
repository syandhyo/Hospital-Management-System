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

public partial class RECEPTION_reception_bed_transfer : System.Web.UI.Page
{
    SqlDataReader dr;
    SqlConnection con;
    DataMathods OBJ_METHOD = new DataMathods();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
            lbluid.Text = Session["UID"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();

            if (!IsPostBack)
            {
                binddata();
                txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
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

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BED");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);


            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_BED_TRANSFER", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataSource = DS1;
                DropDownList1.DataTextField = "BEDNO";
                DropDownList1.DataValueField = "BEDNO";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            //using (SqlCommand cmd = new SqlCommand("RECP_BED_TRANSFER", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";

            //    //DataTable dt = new DataTable();
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT BEDNO FROM BED_TABLE", con);
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    //dropbedno.SelectedIndex = 0;
            //    DropDownList1.DataSource = dt1;
            //    DropDownList1.DataTextField = "BEDNO";
            //    DropDownList1.DataValueField = "BEDNO";
            //    DropDownList1.DataBind();
            //    DropDownList1.Items.Insert(0, "Please Select");
            //}
            SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BEDMATRIX");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_BED_TRANSFER", false, true, SQL_PARAMS1);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropward.DataSource = Ds;
                dropward.DataTextField = "NAME";
                dropward.DataBind();
                dropward.Items.Insert(0, "Please Select");
            }
        }
        
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        //using (SqlCommand cmd1 = new SqlCommand("RECP_BED_TRANSFER", con))
        //{
        //    cmd1.CommandType = CommandType.StoredProcedure;
        //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BEDMATRIX";
        //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
        //    SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
        //    //SqlDataAdapter da2 = new SqlDataAdapter("SELECT DISTINCT NAME FROM BED_MATRIX_TABLE WHERE ORGID='" + lblorgid.Text + "'", con);
        //    DataTable dt2 = new DataTable();
        //    da2.Fill(dt2);
        //    //dropbedno.SelectedIndex = 0;
        //    dropward.DataSource = dt2;
        //    dropward.DataTextField = "NAME";
        //    dropward.DataBind();
        //    dropward.Items.Insert(0, "Please Select");
        //}
    }


    protected void btnshowip_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (txtip.Text == "")
            {
                string message = "alert('Please!!Enter The IP Number.. ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_VN");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtip.Text.Trim());

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_BED_TRANSFER", false, true, SQL_PARAMS1);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                lblpid.Text = Ds.Tables[0].Rows[0]["VN"].ToString();
                lblname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
                lblbed.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                lblward.Text = Ds.Tables[0].Rows[0]["WARD"].ToString();
                lblinsurance.Text = Ds.Tables[0].Rows[0]["INSURANCE"].ToString();
            }
            else
            {
                string message = "alert('* No Data Found.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            //using (SqlCommand cmd1 = new SqlCommand("RECP_BED_TRANSFER", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VN";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtip.Text;
            //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    //SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
            //    //SqlCommand cm = new SqlCommand("SELECT VN,PNAME,WARD,BEDNO,INSURANCE FROM BED_TABLE WHERE VN= '" + txtip.Text + "' and ORGID='" + lblorgid.Text + "' ", con);
            //    dr = cmd1.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        lblpid.Text = dr["VN"].ToString();
            //        lblname.Text = dr["PNAME"].ToString();
            //        lblbed.Text = dr["BEDNO"].ToString();
            //        lblward.Text = dr["WARD"].ToString();
            //        lblinsurance.Text = dr["INSURANCE"].ToString();
            //    }
            //    else
            //    {
            //        string message = "alert('* No Data Found.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;
            //    }
            //}
            //dr.Close();
            //con.Close();
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
            
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The Bed..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BEDNO");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID      ", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, DropDownList1.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_BED_TRANSFER", false, true, SQL_PARAMS1);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                lblpid.Text = Ds.Tables[0].Rows[0]["VN"].ToString();
                lblname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
                lblbed.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                lblward.Text = Ds.Tables[0].Rows[0]["WARD"].ToString();
                lblinsurance.Text = Ds.Tables[0].Rows[0]["INSURANCE"].ToString();
            }
            else
            {
                string message = "alert('* No Data Found.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            //using (SqlCommand cmd1 = new SqlCommand("RECP_BED_TRANSFER", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BEDNO";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = DropDownList1.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    //SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
            //    //SqlCommand cm = new SqlCommand("SELECT VN,PNAME,WARD,BEDNO,INSURANCE FROM BED_TABLE WHERE BEDNO= '" + DropDownList1.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            //    dr = cmd1.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        lblpid.Text = dr["VN"].ToString();
            //        lblname.Text = dr["PNAME"].ToString();
            //        lblbed.Text = dr["BEDNO"].ToString();
            //        lblward.Text = dr["WARD"].ToString();
            //        lblinsurance.Text = dr["INSURANCE"].ToString();
            //    }
            //    else
            //    {
            //        string message = "alert('* No Data Found.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;
            //    }
            //}
            //dr.Close();
            //con.Close();
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
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cmd = new SqlCommand("RECP_BED_TRANSFER", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STATUS";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbed.Text;
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    //SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
            //    //SqlCommand com1 = new SqlCommand("select * from BED_MATRIX_TABLE where BEDNO='" + dropbed.Text + "' AND STATUS='AVAILABLE' and ORGID='" + lblorgid.Text + "'", con);
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        dr.Close();

            //        using (SqlCommand cmd1 = new SqlCommand("usp_BEDTRANSFER", con))
            //        {
            //            cmd1.CommandType = CommandType.StoredProcedure;
            //            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //            cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
            //            cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblname.Text;

            //            cmd1.Parameters.Add("@WARDNAME", SqlDbType.VarChar).Value = lblward.Text;
            //            cmd1.Parameters.Add("@NWARDNAME", SqlDbType.VarChar).Value = dropward.Text;
            //            cmd1.Parameters.Add("@CBEDNO", SqlDbType.VarChar).Value = lblbed.Text;
            //            cmd1.Parameters.Add("@NBEDNO", SqlDbType.VarChar).Value = dropbed.Text;

            //            cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
            //            cmd1.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = lblinsurance.Text;
            //            cmd1.ExecuteNonQuery();
            //        }
            //    }
            //    else
            //    {
            //        dr.Close();
            //        string message = "alert('The selected bed is not available. Try new bed.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;

            //    }
            //}

            //binddata();
            //clearcontrol();
            //string message1 = "alert('Successfully Saved.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            //con.Close();

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
            //using (SqlCommand cmd = new SqlCommand("RECP_BED_TRANSFER", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STATUS_NAME";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbed.Text;
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropward.SelectedItem.Text;
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da1 = new SqlDataAdapter("SELECT BEDNO FROM BED_MATRIX_TABLE WHERE STATUS='AVAILABLE' AND NAME='" + dropward.SelectedItem.Text + "' AND ORGID='" + lblorgid.Text + "' ORDER BY ID ASC", con);
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    //dropbedno.SelectedIndex = 0;
            //    dropbed.DataSource = dt1;
            //    dropbed.DataTextField = "BEDNO";
            //    dropbed.DataBind();
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}