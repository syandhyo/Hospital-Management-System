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

public partial class RECEPTION_reception_lab_requisition : System.Web.UI.Page
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
    string succ;

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
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lblid.Text = Session["NAME"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();

            if (!IsPostBack)
            {
                testype();
                binddata();
                //  auto();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        //string qry1 = "select max(ID) as ID from TBLRECLAB where ID like '%LB%'";

        //com = new SqlCommand(qry1, con);
        //dr = null;
        //dr = com.ExecuteReader();
        //string str1 = "1";
        //if (dr.Read() && dr["ID"].ToString() != "")
        //{
        //    // num1 = dr["ID"].ToString();
        //    //------------------
        //    num1 = dr["ID"].ToString();
        //    // string str="0";
        //    string str = num1.Substring(0, num1.Length - 0);//delete last 10 record
        //    string d = str.Substring(2);//delete first 3 record
        //    str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
        //}
        ////-----------------------------------------------------
        ////num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
        //txtid.Text = "LB" + str1;
        string qry1 = "select Max(ID) as ID from TBLRECLAB";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("LB{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
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
        //using (SqlCommand cmd1 = new SqlCommand("RECP_LAB_REC", con))
        //{
        //    cmd1.CommandType = CommandType.StoredProcedure;
        //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEST";
        //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
        //    da = new SqlDataAdapter(cmd1);
        //    DataTable ds = new DataTable();
        //    da.Fill(ds);
        //    dropTestype.DataSource = ds;
        //    dropTestype.DataTextField = "NAME";
        //    dropTestype.DataValueField = "ID";
        //    dropTestype.DataBind();
        //    dropTestype.Items.Insert(0, "Please Select");
        //}

        
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
            dropCorprt.Items.Insert(0, new ListItem("Please Select","0"));
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
            dropPackage.Items.Insert(0, new ListItem("Please Select","0"));
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
        SQL_PARAMS2 = new SqlParameter[2];

        SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_RECLAB");
        SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

        DataSet DS3 = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS2);
        if (DS3.Tables[0].Rows.Count > 0)
        {
            grdlabill.SelectedIndex = 0;
            grdlabill.DataSource = DS3;
            grdlabill.DataKeyNames = new string[] { "ID" };
            grdlabill.DataBind();

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
        try
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void chkTestType_CheckedChanged(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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
    ////        div1.Visible = true;
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
    protected void txtopdno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            
            //using (SqlCommand cmd2 = new SqlCommand("RECP_LAB_REC", con))
            //{
                //cmd2.CommandType = CommandType.StoredProcedure;
                //cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_REGISTRATION";
                //cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtopdno.Text;
                //cmd2.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                //cmd2.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                //cmd2.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
                ////SqlDataAdapter da = new SqlDataAdapter(cmd2);
                ////SqlCommand com = new SqlCommand("select ID,PNAME,CORPORATE from  REGISTRATION_TBL where ID='" + txtopdno.Text + "'", con);
                //dr = cmd2.ExecuteReader();
                //if (dr.Read())
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_REGISTRATION");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtopdno.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                txtname.Text = DS.Tables[0].Rows[0]["PNAME"].ToString();
                if (DS.Tables[0].Rows[0]["CORPORATE"].ToString() == "0")
                {
                    testype();
                }
                else
                {
                    Session["mode"] = "true";
                    SQL_PARAMS = new SqlParameter[3];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TEXTCHANGE_MULTIPLE");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtopdno.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

                    DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS);
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        dropTestype.DataSource = DS1;
                        dropTestype.DataTextField = "NAME";
                        dropTestype.DataValueField = "ID";
                        dropTestype.DataBind();
                        dropTestype.Items.Insert(0, new ListItem("Please Select","0"));
                    }
                    //using (SqlCommand cmd = new SqlCommand("RECP_LAB_REC", con))
                    //{
                    //    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEXTCHANGE_MULTIPLE";
                    //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtopdno.Text;
                    //    cmd.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                    //    cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                    //    cmd.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
                    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //    //da = new SqlDataAdapter("select DISTINCT B.ID AS ID,B.NAME AS NAME from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C,REGISTRATION_TBL D  WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND D.CORPORATE=A.C_ID AND  A.ORGID='" + lblorgid.Text + "' AND D.ID='" + txtopdno.Text + "'", con);
                    //    DataTable ds = new DataTable();
                    //    da.Fill(ds);
                    //    dropTestype.DataSource = ds;
                    //    dropTestype.DataTextField = "NAME";
                    //    dropTestype.DataValueField = "ID";
                    //    dropTestype.DataBind();
                    //    dropTestype.Items.Insert(0, "Please Select");
                    //}
                }
                //droptesttype.Text = dr["SELECTYPE"].ToString();
            }
            //}
            //dr.Close();
            //con.Close();
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
        try
        {
            foreach (GridViewRow row in grdtestype.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[3].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        var AMT = row.FindControl("lblprice") as TextBox;                
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        
        string message1 = string.Empty;
        try
        {
            if (txtopdno.Text == "")
            {
                string message = "alert('* Please Enter OPDNO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            
            

            
            auto();
            labtory_item();
            OBJ_METHOD = new DataMathods();
            if (SS == "FALSE")
            {
                string message = "alert('*Please select any item to save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (succ == "success")
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[15];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@OPDNO", SqlDbType.VarChar, 500, txtopdno.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropCorprt.SelectedItem.Text);
                SQL_PARAMS[7] = OBJ_METHOD.createParams("@EMPID", SqlDbType.VarChar, 500, txtEmpid.Text);
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@PACKAGE", SqlDbType.VarChar, 500, dropPackage.SelectedItem.Text);
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@TESTYPEPK", SqlDbType.VarChar, 500, dropTestype.SelectedItem.Text);
                SQL_PARAMS[10] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                SQL_PARAMS[11] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm"));
                SQL_PARAMS[12] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.Decimal, 0, lbltotalprice.Text);
                SQL_PARAMS[13] = OBJ_METHOD.createParams("@TOTALAMOUNT", SqlDbType.Decimal, 0, lbltotalamt.Text);
                SQL_PARAMS[14] = OBJ_METHOD.createParams("@DISCOUNT", SqlDbType.Decimal, 0, txtdisc.Text);


                OBJ_METHOD.ExecuteProceedure("USP_RECLABTOR", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SQL_PARAMS = new SqlParameter[18];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 0, txtopdno.Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 500, txtname.Text);
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@UNAME", SqlDbType.VarChar, 500, lblid.Text);
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@UID", SqlDbType.VarChar, 500, "");
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@TYPE", SqlDbType.VarChar, 500, "");
                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, lbltotalprice.Text);
                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm"));
                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@TESTTYPE", SqlDbType.VarChar, 0, dropTestype.SelectedItem.Text.ToString());
                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@TESTINDEX", SqlDbType.VarChar, 500, "");
                    SQL_PARAMS[14] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "OUTPATIENT");
                    SQL_PARAMS[15] = OBJ_METHOD.createParams("@DISCAMT", SqlDbType.Decimal, 0, txtdisc.Text);
                    SQL_PARAMS[16] = OBJ_METHOD.createParams("@PAIDAMT", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[17] = OBJ_METHOD.createParams("@DUEAMT", SqlDbType.Decimal, 0, "0.00");

                    OBJ_METHOD.ExecuteProceedure("RECP_LAB_REC_INSRTUP", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        
                    }
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
            }
            #region old coding
            //using (SqlCommand stock_cmd = new SqlCommand("USP_RECLABTOR", con))
            //{
            //    stock_cmd.CommandType = CommandType.StoredProcedure;
            //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

            //    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text;
            //    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
            //    stock_cmd.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropCorprt.SelectedItem.Text;
            //    stock_cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = txtEmpid.Text;
            //    stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
            //    stock_cmd.Parameters.Add("@TESTYPEPK", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
            //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = lbltotalprice.Text;
            //    stock_cmd.Parameters.Add("@TOTALAMOUNT", SqlDbType.Decimal).Value = lbltotalamt.Text;
            //    stock_cmd.Parameters.Add("@DISCOUNT", SqlDbType.Decimal).Value = txtdisc.Text;
            //    // stock_cmd.Parameters.Add("@TESTNAME", SqlDbType.VarChar).Value = "0.00";   
            //    stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
            //    stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "0.00";

            //    stock_cmd.ExecuteNonQuery();
            //}
            //-------------------------------------
            //using (SqlCommand cmd = new SqlCommand("RECP_LAB_REC_INSRTUP", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
               
            //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopdno.Text;
            //    cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
            //    cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
            //    cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
            //    cmd.Parameters.Add("@TESTTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text.ToString();
            //    cmd.Parameters.Add("@TESTINDEX", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
            //    cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = txtdisc.Text;
            //    cmd.Parameters.Add("@PAIDAMT", SqlDbType.Decimal).Value = "0.00";
            //    cmd.Parameters.Add("@DUEAMT", SqlDbType.Decimal).Value = "0.00";
            //    cmd.Parameters.Add("@REFBY", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";

            //    cmd.ExecuteNonQuery();
            //}
            ////---------------------------
            //binddata();
            //con.Close();
            #endregion 
            Session["labbill"] = txtid.Text;
            clearcontrol();
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
        Response.Redirect("~/RECEPTION/reception_lab_receipt.aspx");
    }
    public void clearcontrol()
    {
        txtopdno.Text = "";
        txtname.Text = "";
        chkPackage.Checked = false;
        chkTestType.Checked = false;
        dropPackage.SelectedIndex = dropTestype.SelectedIndex = 0;
        grdtestype.DataSource = null;
        grdtestype.DataBind();
        
        grdtestype.DataSource = null;
        grdtestype.DataBind();
        divinner2.Visible = false;
    }
    public void labtory_item()
    {
        try
        {
            int chkedcounter = 0;
            int correctinput = 0;
            
            OBJ_METHOD = new DataMathods();
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
                        chkedcounter++;
                        var INV = Convert.ToInt32(grdtestype.DataKeys[row.RowIndex].Values[0]);
                        SS = "TRUE";
                        SqlParameter[] SQL_PARAMS = new SqlParameter[8];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@INVID", SqlDbType.VarChar, 500, INV.ToString());
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, invIt.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, priceIt.Text);
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@CHKSEL", SqlDbType.VarChar, 500, chkRow.Checked);


                        OBJ_METHOD.ExecuteProceedure("USP_RECLABTOR", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            SQL_PARAMS = new SqlParameter[6];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT1");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, INV.ToString());
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, priceIt.Text);
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                            OBJ_METHOD.ExecuteProceedure("RECP_LAB_REC_INSRTUP", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                            if (OBJ_METHOD._RESULT > 0)
                            {
                                correctinput++;

                            }

                        }
                        #region old code
                        //using (SqlCommand stock_cmd = new SqlCommand("USP_RECLABTOR", con))
                        //{
                        //    stock_cmd.CommandType = CommandType.StoredProcedure;
                        //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";

                        //    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                        //    stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text;
                        //    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                        //    stock_cmd.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropCorprt.SelectedItem.Text;
                        //    stock_cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = txtEmpid.Text;
                        //    stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                        //    stock_cmd.Parameters.Add("@TESTYPEPK", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                        //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                        //    stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                        //    stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = "0.00";
                        //    stock_cmd.Parameters.Add("@TOTALAMOUNT", SqlDbType.Decimal).Value = "0.00";
                        //    stock_cmd.Parameters.Add("@DISCOUNT", SqlDbType.Decimal).Value = "0.00";

                        //    stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = INV.ToString();
                        //    stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = invIt.Text;
                        //    stock_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
                        //    stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

                        //    stock_cmd.ExecuteNonQuery();
                        //}
                        //using (SqlCommand labres_cmd = new SqlCommand("RECP_LAB_REC_INSRTUP", con))
                        //{
                        //    labres_cmd.CommandType = CommandType.StoredProcedure;
                        //    labres_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT1";
                        //    labres_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                        //    labres_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                        //    labres_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopdno.Text;
                        //    labres_cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
                        //    labres_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                        //    labres_cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
                        //    labres_cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = "";
                        //    labres_cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = "";
                        //    labres_cmd.Parameters.Add("@TESTTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text.ToString();
                        //    labres_cmd.Parameters.Add("@TESTINDEX", SqlDbType.VarChar).Value = "";
                        //    labres_cmd.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                        //    labres_cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = txtdisc.Text;
                        //    labres_cmd.Parameters.Add("@PAIDAMT", SqlDbType.Decimal).Value = "0.00";
                        //    labres_cmd.Parameters.Add("@DUEAMT", SqlDbType.Decimal).Value = "0.00";
                        //    labres_cmd.Parameters.Add("@REFBY", SqlDbType.VarChar).Value = "";
                        //    labres_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
                        //    labres_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = priceIt.Text;
                        //    labres_cmd.ExecuteNonQuery();
                        //}
                        #endregion
                    }
                }
            }
            if (chkedcounter == correctinput && chkedcounter > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                succ = "success";
                
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        
        
    }
    protected void dropCorprt_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/reception_lab_requisition.aspx");
    }
    protected void grdlabill_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[]  SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_RECLAB");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS3 = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS2);
            if (DS3.Tables[0].Rows.Count > 0)
            {
                grdlabill.SelectedIndex = 0;
                grdlabill.DataSource = DS3;
                grdlabill.DataKeyNames = new string[] { "ID" };
                grdlabill.DataBind();
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
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd3);
            //    //SqlDataAdapter adp = new SqlDataAdapter("select ID,OPDNO,NAME,CONVERT(varchar,DATE , 105) as  DATE FROM TBLRECLAB ORDER BY ID ASC", con);
            //    DataTable dt1 = new DataTable();
            //    adp.Fill(dt1);
            //    grdlabill.DataSource = dt1;
            //    grdlabill.PageIndex = e.NewPageIndex;
            //    grdlabill.DataKeyNames = new string[] { "ID" };
            //    grdlabill.DataBind();
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
}