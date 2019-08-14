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
using System.Web.Services;

public partial class NURSE_nurse_requisition_lab : System.Web.UI.Page
{
    string num1 = "PK000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da, da1, da2;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;
    string SS;
    decimal amount = 0;
    DataMathods OBJ_METHOD = new DataMathods();

    [WebMethod]
    public static string[] GetCustomers(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct VN FROM BED_TABLE where VN like '%'+@SearchText+'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["VN"]));
                    }
                }
                con.Close();
            }
        }
        return customers.ToArray();
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
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            testype();
            binddata();
            //  auto();
        }
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID as ID from LABRES_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("LT{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }
    public void testype()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("TESTNAME"), new DataColumn("INV"), new DataColumn("PRICE"), new DataColumn("CHKTYPE") });

            ViewState["ITEM"] = dt;
            this.BindGrid();

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TEST");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 0, lblorgid.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                dropTestype.DataSource = DS1;
                dropTestype.DataTextField = "NAME";
                dropTestype.DataValueField = "ID";
                dropTestype.DataBind();
                dropTestype.Items.Insert(0, new ListItem("Please Select", "0"));
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
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }

        SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CORPORATE");
        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

        DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS1);
        if (DS1.Tables[0].Rows.Count > 0)
        {
            dropCorprt.DataSource = DS1;
            dropCorprt.DataTextField = "CNAME";
            dropCorprt.DataValueField = "ID";
            dropCorprt.DataBind();
            dropCorprt.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        //using (SqlCommand cmd1 = new SqlCommand("RECP_LAB_REC", con))
        //{
        //    cmd1.CommandType = CommandType.StoredProcedure;
        //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CORPORATE";
        //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
        //    da = new SqlDataAdapter(cmd1);
        //    //da = new SqlDataAdapter("select ID,CNAME FROM Corporate_Table", con);
        //    DataTable ds = new DataTable();
        //    da.Fill(ds);
        //    dropCorprt.DataSource = ds;
        //    dropCorprt.DataTextField = "CNAME";
        //    dropCorprt.DataValueField = "ID";
        //    dropCorprt.DataBind();
        //    dropCorprt.Items.Insert(0, "Please Select");
        //}
        SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

        SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PACKAGE");
        SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

        DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS2);
        if (DS2.Tables[0].Rows.Count > 0)
        {
            dropPackage.DataSource = DS2;
            dropPackage.DataTextField = "PACKGNAME";
            dropPackage.DataValueField = "ID";
            dropPackage.DataBind();
            dropPackage.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        //using (SqlCommand cmd2 = new SqlCommand("RECP_LAB_REC", con))
        //{
        //    cmd2.CommandType = CommandType.StoredProcedure;
        //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PACKAGE";
        //    cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd2.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
        //    cmd2.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
        //    cmd2.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
        //    da1 = new SqlDataAdapter(cmd2);
        //    //da1 = new SqlDataAdapter("select ID,PACKGNAME FROM TBLPACKAGE", con);
        //    DataTable ds1 = new DataTable();
        //    da1.Fill(ds1);
        //    dropPackage.DataSource = ds1;
        //    dropPackage.DataTextField = "PACKGNAME";
        //    dropPackage.DataValueField = "ID";
        //    dropPackage.DataBind();
        //    dropPackage.Items.Insert(0, "Please Select");
        //}
        DataSet DS3 = OBJ_METHOD.Get_DataSet("SELECT * FROM TBLRECLAB WHERE Branch_ID = " + Session["Branch"] + " ORDER BY SLNO DESC", false, false);

        if (DS3.Tables[0].Rows.Count > 0)
        {
            grdreclab.SelectedIndex = 0;
            grdreclab.DataSource = DS3;
            grdreclab.DataKeyNames = new string[] { "ID" };
            grdreclab.DataBind();

            txtdisc.Text = "0";
            lbltotalamt.Text = "0";
            lbltotalprice.Text = "0";
        }
        //using (SqlCommand cmd3 = new SqlCommand("RECP_LAB_REC", con))
        //{
        //    cmd3.CommandType = CommandType.StoredProcedure;
        //    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_RECLAB";
        //    cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //    cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd3.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
        //    cmd3.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
        //    cmd3.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
        //    da2 = new SqlDataAdapter(cmd3);
        //    //SqlDataAdapter da2 = new SqlDataAdapter("select ID,OPDNO,NAME,CONVERT(varchar,DATE , 105) as  DATE FROM TBLRECLAB ORDER BY ID ASC", con);
        //    DataTable dt1 = new DataTable();
        //    da2.Fill(dt1);
        //    grdlabill.SelectedIndex = 0;
        //    grdlabill.DataSource = dt1;
        //    // grdRegtyp.DataKeyNames = new string[] { "ID" };
        //    grdlabill.DataBind();

        //    txtdisc.Text = "0";
        //    lbltotalamt.Text = "0";
        //    lbltotalprice.Text = "0";
        //}
        //con.Close();
    }
    protected void chkPackage_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPackage.Checked)
        {

            dropPackage.Visible = true;
            dropTestype.Visible = false;
            chkTestType.Checked = false;
            divinner2.Visible = false;
            dropTestype.SelectedIndex = 0;
        }
        else
        {

            dropPackage.Visible = false;
            divinner2.Visible = false;
            // chkTestType.Enabled = true;
        }
    }
    protected void chkTestType_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTestType.Checked)
        {

            dropTestype.Visible = true;
            dropPackage.Visible = false;
            chkPackage.Checked = false;
            divinner2.Visible = false;
            dropPackage.SelectedIndex = 0;
            // divinner1.Visible = false;
        }
        else
        {

            dropTestype.Visible = false;
            divinner2.Visible = false;
            //chkPackage.Enabled = true;
        }
    }
    protected void dropPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PACKAGE_MULTI");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PACKGNAME", SqlDbType.VarChar, 500, dropPackage.SelectedItem.Text);

            DataSet DS3 = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS);
            if (DS3.Tables[0].Rows.Count > 0)
            {
                grdtestype.DataSource = DS3;
                grdtestype.DataBind();
                divinner2.Visible = true;
            }
            //using (SqlCommand cmd3 = new SqlCommand("RECP_LAB_REC", con))
            //{
            //    cmd3.CommandType = CommandType.StoredProcedure;
            //    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PACKAGE_MULTI";
            //    cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd3.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
            //    cmd3.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
            //    cmd3.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
            //    da = new SqlDataAdapter(cmd3);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    grdtestype.DataSource = dt;
            //    grdtestype.DataBind();
            //    divinner2.Visible = true;
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void dropTestype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //  DataTable dt = new DataTable();
            DataTable dt = (DataTable)ViewState["ITEM"];
            if (Session["mode"] == "true")
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TEST_MULTI1");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, dropTestype.SelectedValue.ToString());
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

                DataSet DS = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {

                    DS.Tables[0].TableName = "table1";
                    dt = DS.Tables["table1"];
                }
                //using (SqlCommand cmd3 = new SqlCommand("RECP_LAB_REC", con))
                //{
                //    cmd3.CommandType = CommandType.StoredProcedure;
                //    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEST_MULTI1";
                //    cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //    cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = dropTestype.SelectedValue.ToString();
                //    cmd3.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                //    cmd3.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                //    cmd3.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
                //    SqlDataAdapter da = new SqlDataAdapter(cmd3);
                //    //SqlDataAdapter da = new SqlDataAdapter("select A.slno AS ID ,A.NAME AS NAME,A.INV AS INV,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C,REGISTRATION_TBL D WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND D.CORPORATE=A.C_ID AND  B.ID='" + dropTestype.SelectedValue.ToString() + "' AND A.ORGID='" + lblorgid.Text + "'", con);
                //    da.Fill(dt);
                //}
            }
            else
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TEST_MULTI2");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, dropTestype.SelectedValue.ToString());
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

                DataSet DS = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {

                    DS.Tables[0].TableName = "table2";
                    dt = DS.Tables["table2"];
                }
                //using (SqlCommand cmd2 = new SqlCommand("RECP_LAB_REC", con))
                //{
                //    cmd2.CommandType = CommandType.StoredProcedure;
                //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEST_MULTI2";
                //    cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = dropTestype.SelectedValue.ToString();
                //    cmd2.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                //    cmd2.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                //    cmd2.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
                //    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                //    //SqlDataAdapter da = new SqlDataAdapter("select A.slno AS ID ,A.NAME AS NAME,A.INV AS INV,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND B.ID='" + dropTestype.SelectedValue.ToString() + "' AND A.ORGID='" + lblorgid.Text + "'", con);
                //    da.Fill(dt);
                //}
            }
            //SqlDataAdapter da = new SqlDataAdapter("select A.slno AS ID ,A.NAME AS NAME,A.INV AS INV,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT,D.SELECTYPE AS TESTYPE from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C,TBLPACKAGEITEM D WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND D.TESTNAME=A.NAME AND B.ID='" + dropTestype.SelectedValue.ToString() + "' AND A.ORGID='" + lblorgid.Text + "'", con);


            //grdtestype.DataSource = dt;
            //grdtestype.DataBind();
            ViewState["ITEM"] = dt;
            this.BindGrid();
            divinner2.Visible = true;
            //lbltotalprice.Text =  dt.Rows[0]["PRICE"].ToString();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void BindGrid()
    {
        try
        {
            grdtestype.DataSource = (DataTable)ViewState["ITEM"];
            grdtestype.DataBind();

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    //protected void chkCorporate_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkCorporate.Checked)
    //    {
    //        txtdisc.Text = "0";
    //        lbltotalamt.Text = "0";
    //        lbltotalprice.Text = "0";
    //        div1.Visible = true;
    //        //dropPackage.Visible = false;
    //        //chkPackage.Checked = false;
    //    }
    //    else
    //    {
    //        txtdisc.Text = "0";
    //        lbltotalamt.Text = "0";
    //        lbltotalprice.Text = "0";
    //        div1.Visible = false;
    //        //chkPackage.Enabled = true;
    //    }
    //}
    protected void txtipdno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select VN,PNAME,INSURANCE,UHID from  BED_TABLE where VN='" + txtipdno.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                txtname.Text = dr["PNAME"].ToString();
                LBLUHID.Text = dr["UHID"].ToString();
                if (dr["INSURANCE"].ToString() == "0")
                {
                    testype();
                }
                else
                {
                    Session["mode"] = "true";
                    dr.Close();
                    da = new SqlDataAdapter("select DISTINCT B.ID AS ID,B.NAME AS NAME  from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C ,BED_TABLE D  WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND D.INSURANCE=A.C_ID  AND A.ORGID='" + lblorgid.Text + "' AND D.VN='" + txtipdno.Text + "'", con);
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                    dropTestype.DataSource = ds;
                    dropTestype.DataTextField = "NAME";
                    dropTestype.DataValueField = "ID";
                    dropTestype.DataBind();
                    dropTestype.Items.Insert(0, "Please Select");
                }
                //droptesttype.Text = dr["SELECTYPE"].ToString();
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void txtdisc_TextChanged(object sender, EventArgs e)
    {
        lbltotalamt.Text = Math.Round((Convert.ToDouble(lbltotalprice.Text)) - Convert.ToDouble(txtdisc.Text)).ToString();
    }
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdtestype.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[3].FindControl("chkRow") as CheckBox);

                if (chkRow.Checked)
                {
                    //var value = row.FindControl("lblname") as Label;
                    // var inv = row.FindControl("lblinv") as Label;
                    var AMT = row.FindControl("lblprice") as TextBox;
                    //var re = row.FindControl("txtref") as TextBox;
                    //var unit = row.FindControl("txtunit") as TextBox;
                    //var AMT = row.FindControl("lblprice") as TextBox;                   
                    amount = amount + Convert.ToDecimal(AMT.Text);
                }
            }
        }
        lbltotalprice.Text = amount.ToString();
        // lblbalanceamt.Text = amount.ToString();
        decimal d = (amount / 100) * Convert.ToDecimal(txtdisc.Text);
        //  lbltotalamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(txtpaidamt.Text) - Convert.ToDouble(txtdisc.Text)).ToString();
        lbltotalamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(d)).ToString();
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtipdno.Text == "")
            {
                string message = "alert('* Please Enter IPDNO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            
            auto();
            labtory_item();
            if (SS == "FALSE")
            {
                string message = "alert('*Please select any item to save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            //Invalid IP NUMBER
            if (txtipdno.Text != "")
            {
                SqlCommand cmd2 = new SqlCommand("select * from BED_TABLE where VN='" + txtipdno.Text + "'", con);
                SqlDataReader dr1 = cmd2.ExecuteReader();
                if (dr1.Read() == false)
                {
                    string message = "alert('Invalid IPD Number.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                dr1.Close();
            }
            using (SqlCommand stock_cmd = new SqlCommand("USP_RECLABTOR", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtipdno.Text;
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                stock_cmd.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropCorprt.SelectedItem.Text;
                stock_cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = txtEmpid.Text;
                stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                stock_cmd.Parameters.Add("@TESTYPEPK", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = lbltotalprice.Text;
                stock_cmd.Parameters.Add("@TOTALAMOUNT", SqlDbType.Decimal).Value = lbltotalamt.Text;
                stock_cmd.Parameters.Add("@DISCOUNT", SqlDbType.Decimal).Value = txtdisc.Text;
                //stock_cmd.Parameters.Add("@TESTNAME", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
                stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "0.00";

                stock_cmd.ExecuteNonQuery();
            }

            using (SqlCommand cm = new SqlCommand("per_tran", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "LABUPDATE";
                cm.Parameters.Add("@REF", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = LBLUHID.Text;
                cm.Parameters.Add("@PCHARGE", SqlDbType.Decimal).Value = "0.00";
                cm.Parameters.Add("@PP", SqlDbType.Decimal).Value = "0.00";
                cm.Parameters.Add("@LCHARGE", SqlDbType.Decimal).Value = lbltotalamt.Text;
                cm.Parameters.Add("@LP", SqlDbType.Decimal).Value = "0.00";
                cm.Parameters.Add("@BCHARGE", SqlDbType.Decimal).Value = "0.00";
                cm.Parameters.Add("@BP", SqlDbType.Decimal).Value = "0.00";
                cm.Parameters.Add("@MISCHARGE", SqlDbType.Decimal).Value = "0.00";
                cm.Parameters.Add("@MP", SqlDbType.Decimal).Value = "0.00";
                cm.Parameters.Add("@RCHARGE", SqlDbType.Decimal).Value = "0.00";
                cm.Parameters.Add("@RP", SqlDbType.Decimal).Value = "0.00";
                cm.ExecuteNonQuery();
            }


            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtipdno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL ";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = lbltotalamt.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lbltotalamt.Text;
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtipdno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                cm.ExecuteNonQuery();
            }
            //-------------------------------------
            // SqlCommand cmd = new SqlCommand("insert into LABRES_TABLE (ID,LINDID,PID,ORGID,DATE,UNAME,UID,TYPE,PRICE,PNAME,TESTTYPE,PTYPE,TESTINDEX,REFBY,DISCAMT,PAIDAMT,DUEAMT)VALUES(@ID,@LINDID,@PID,@ORGID,@DATE,@UNAME,@UID,@TYPE,@PRICE,@PNAME,@TESTTYPE,@PTYPE,@TESTINDEX,@REFBY,@DISCAMT,@PAIDAMT,@DUEAMT)", con);
            using (SqlCommand cmd = new SqlCommand("SP_NURSE_LAB_REQ", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@LINDID", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtipdno.Text;
                cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
                cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
                cmd.Parameters.Add("@TESTTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text.ToString();
                cmd.Parameters.Add("@TESTINDEX", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = txtdisc.Text;
                cmd.Parameters.Add("@PAIDAMT", SqlDbType.Decimal).Value = "0.00";
                cmd.Parameters.Add("@DUEAMT", SqlDbType.Decimal).Value = "0.00";
                cmd.Parameters.Add("@REFBY", SqlDbType.VarChar).Value = "";
                cmd.ExecuteNonQuery();
            }
            //---------------------------
            binddata();
            con.Close();

            Session["labbill"] = txtid.Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/NURSE/nurse_LabRecutionbill.aspx");

    }
    public void labtory_item()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SS = "FALSE";
        foreach (GridViewRow row in grdtestype.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                //  Label typeIt = (row.Cells[0].FindControl("lbltype") as Label);
                Label nameIt = (row.Cells[0].FindControl("lblname") as Label);
                Label invIt = (row.Cells[1].FindControl("lblinv") as Label);
                TextBox priceIt = (row.Cells[2].FindControl("lblprice") as TextBox);
                CheckBox chkRow = (row.Cells[3].FindControl("chkRow") as CheckBox);

                if (chkRow.Checked)
                {
                    var INV = Convert.ToInt32(grdtestype.DataKeys[row.RowIndex].Values[0]);
                    SS = "TRUE";
                    using (SqlCommand stock_cmd = new SqlCommand("USP_RECLABTOR", con))
                    {
                        stock_cmd.CommandType = CommandType.StoredProcedure;
                        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                        stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                        stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtipdno.Text;
                        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                        stock_cmd.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropCorprt.SelectedItem.Text;
                        stock_cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = txtEmpid.Text;
                        stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                        stock_cmd.Parameters.Add("@TESTYPEPK", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                        stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                        stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = lbltotalprice.Text;
                        stock_cmd.Parameters.Add("@TOTALAMOUNT", SqlDbType.Decimal).Value = lbltotalamt.Text;
                        stock_cmd.Parameters.Add("@DISCOUNT", SqlDbType.Decimal).Value = txtdisc.Text;
                        // stock_cmd.Parameters.Add("@TESTNAME", SqlDbType.VarChar).Value = nameIt.Text;
                        stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = INV.ToString();
                        stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = invIt.Text;
                        stock_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
                        stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

                        stock_cmd.ExecuteNonQuery();
                    }
                    SqlCommand labres_cmd = new SqlCommand("INSERT INTO LABRESULT_TABLE (ID,INV,PRICE) VALUES (@ID,@INV,@PRICE)", con);
                    labres_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                    labres_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
                    labres_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = priceIt.Text;
                    labres_cmd.ExecuteNonQuery();
                }
            }
        }
    }
    protected void dropCorprt_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select LAB from  Corporate_Table where CNAME='" + dropCorprt.SelectedItem.Text + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            txtdisc.Text = "0";
            txtdisc.Text = dr["LAB"].ToString();
        }
        else
        {
            txtdisc.Text = "0";
        }
        dr.Close();
        con.Close();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/NURSE/nurse_requisition_lab.aspx");
    }
    protected void grdreclab_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
        }
    }
    protected void grdreclab_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        string slno = grdreclab.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlCommand cm = new SqlCommand("delete from TBLRECLAB where ID='" + slno + "'", con);
        cm.ExecuteNonQuery();
        //----------------------
        SqlCommand cmres = new SqlCommand("delete from LABRES_TABLE where ID='" + slno + "'", con);
        cmres.ExecuteNonQuery();
        // labtory_itemDel();
        foreach (GridViewRow row in grdreclab.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                //   SS = "TRUE";
                using (SqlCommand stock_cmd = new SqlCommand("USP_RECLABTOR", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                    stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtipdno.Text;
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                    stock_cmd.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropCorprt.SelectedItem.Text;
                    stock_cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = txtEmpid.Text;
                    stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                    stock_cmd.Parameters.Add("@TESTYPEPK", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                    stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = lbltotalprice.Text;
                    stock_cmd.Parameters.Add("@TOTALAMOUNT", SqlDbType.Decimal).Value = lbltotalamt.Text;
                    stock_cmd.Parameters.Add("@DISCOUNT", SqlDbType.Decimal).Value = txtdisc.Text;
                    // stock_cmd.Parameters.Add("@TESTNAME", SqlDbType.VarChar).Value = nameIt.Text;
                    stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = "";
                    stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
                    stock_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
                    stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";

                    stock_cmd.ExecuteNonQuery();
                }
                SqlCommand cmresult = new SqlCommand("delete from LABRESULT_TABLE where ID='" + slno + "'", con);
                cmresult.ExecuteNonQuery();
                
            }
        }
        
        binddata();
        con.Close();
    }
    

    protected void grdreclab_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_RECLAB");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS3 = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS2);
            if (DS3.Tables[0].Rows.Count > 0)
            {
                grdreclab.SelectedIndex = 0;
                grdreclab.DataSource = DS3;
                grdreclab.DataKeyNames = new string[] { "ID" };
                grdreclab.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}