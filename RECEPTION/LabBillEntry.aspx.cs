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

public partial class RECEPTION_LabBillEntry : System.Web.UI.Page
{
    string num1 = "PK000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da,da1,da2;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;
    string SS;
    decimal amount = 0;

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
        string qry1 = "select ID as ID from TBLRECLAB";

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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("TESTNAME"), new DataColumn("INV"), new DataColumn("PRICE"), new DataColumn("CHKTYPE") });

        ViewState["ITEM"] = dt;
        this.BindGrid();

        //DataTable dtP = new DataTable();
        //dtP.Columns.AddRange(new DataColumn[5] { new DataColumn("NAME"), new DataColumn("TESTNAME"), new DataColumn("INV"), new DataColumn("PRICE"), new DataColumn("CHKTYPE") });
        //ViewState["PACKG"] = dtP;
        //this.BindGrid();
        using (SqlCommand cmd1 = new SqlCommand("RECP_LAB_REC", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEST";
            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
            da = new SqlDataAdapter(cmd1);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropTestype.DataSource = ds;
            dropTestype.DataTextField = "NAME";
            dropTestype.DataValueField = "ID";
            dropTestype.DataBind();
            dropTestype.Items.Insert(0, "Please Select");
        }
    
        con.Close();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        //----------------------
        //DataTable dt = new DataTable();
        //dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ID"), new DataColumn("INV"), new DataColumn("PRICE") });
        //ViewState["ITEM"] = dt;
        //this.BindGrid();

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

        using (SqlCommand cmd1 = new SqlCommand("RECP_LAB_REC", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CORPORATE";
            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
            da = new SqlDataAdapter(cmd1);
            //da = new SqlDataAdapter("select ID,CNAME FROM Corporate_Table", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropCorprt.DataSource = ds;
            dropCorprt.DataTextField = "CNAME";
            dropCorprt.DataValueField = "ID";
            dropCorprt.DataBind();
            dropCorprt.Items.Insert(0, "Please Select");
        }
        using (SqlCommand cmd2 = new SqlCommand("RECP_LAB_REC", con))
        {
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PACKAGE";
            cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd2.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
            cmd2.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
            cmd2.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
            da1 = new SqlDataAdapter(cmd2);
            //da1 = new SqlDataAdapter("select ID,PACKGNAME FROM TBLPACKAGE", con);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            dropPackage.DataSource = ds1;
            dropPackage.DataTextField = "PACKGNAME";
            dropPackage.DataValueField = "ID";
            dropPackage.DataBind();
            dropPackage.Items.Insert(0, "Please Select");
        }
        using (SqlCommand cmd3 = new SqlCommand("RECP_LAB_REC", con))
        {
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_RECLAB";
            cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd3.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
            cmd3.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
            cmd3.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
            da2 = new SqlDataAdapter(cmd3);
            //SqlDataAdapter da2 = new SqlDataAdapter("select ID,OPDNO,NAME,CONVERT(varchar,DATE , 105) as  DATE FROM TBLRECLAB ORDER BY ID ASC", con);
            DataTable dt1 = new DataTable();
            da2.Fill(dt1);
            grdlabill.SelectedIndex = 0;
            grdlabill.DataSource = dt1;
            // grdRegtyp.DataKeyNames = new string[] { "ID" };
            grdlabill.DataBind();

            txtdisc.Text = "0";
            lbltotalamt.Text = "0";
            lbltotalprice.Text = "0";
        }
        con.Close();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd3 = new SqlCommand("RECP_LAB_REC", con))
            {
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PACKAGE_MULTI";
                cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd3.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                cmd3.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                cmd3.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
                da = new SqlDataAdapter(cmd3);
                DataTable dt = new DataTable();
                //// DataTable dt = (DataTable)ViewState["ITEM"];
                ////SqlDataAdapter da = new SqlDataAdapter("select c.NAME,b.TESTNAME,b.INV,b.PRICE,b.CHKTYPE as CHKTYPE,b.SELECTYPE as TESTYPE from TBLPACKAGE a,TBLPACKAGEITEM b,TEST_CATEGORY_TABLE c where a.ID=b.ID and b.SELECTYPE=c.ID and a.PACKGNAME='" + dropPackage.SelectedItem.Text + "'", con);
                //SqlDataAdapter da = new SqlDataAdapter("select c.NAME AS NAME,c.slno AS ID,b.INV,b.PRICE,b.CHKTYPE as CHKTYPE from TBLPACKAGE a,TBLPACKGITEM b,TEST_COMPONENT_TABLE c where a.ID=b.ID and c.slno=b.INVID  and  a.PACKGNAME='" + dropPackage.SelectedItem.Text + "'", con);
                da.Fill(dt);
                grdtestype.DataSource = dt;
                grdtestype.DataBind();
                ////  ViewState["ITEM"] = dt;
                //// this.BindGrid();
                ////divinner1.Visible = true;
                divinner2.Visible = true;
                ////  hdntype.Value = dt.Rows[0]["SELECTYPE"].ToString();
                ////lbltotalprice.Text = dt.Rows[0]["PRICE"].ToString();
            }
            con.Close();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //  DataTable dt = new DataTable();
            DataTable dt = (DataTable)ViewState["ITEM"];
            if (Session["mode"] == "true")
            {
                using (SqlCommand cmd3 = new SqlCommand("RECP_LAB_REC", con))
                {
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEST_MULTI1";
                    cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = dropTestype.SelectedValue.ToString();
                    cmd3.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                    cmd3.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                    cmd3.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
                    SqlDataAdapter da = new SqlDataAdapter(cmd3);
                    //SqlDataAdapter da = new SqlDataAdapter("select A.slno AS ID ,A.NAME AS NAME,A.INV AS INV,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C,REGISTRATION_TBL D WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND D.CORPORATE=A.C_ID AND  B.ID='" + dropTestype.SelectedValue.ToString() + "' AND A.ORGID='" + lblorgid.Text + "'", con);
                    da.Fill(dt);
                }
            }
            else
            {
                using (SqlCommand cmd2 = new SqlCommand("RECP_LAB_REC", con))
                {
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEST_MULTI2";
                    cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = dropTestype.SelectedValue.ToString();
                    cmd2.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                    cmd2.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                    cmd2.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    //SqlDataAdapter da = new SqlDataAdapter("select A.slno AS ID ,A.NAME AS NAME,A.INV AS INV,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND B.ID='" + dropTestype.SelectedValue.ToString() + "' AND A.ORGID='" + lblorgid.Text + "'", con);
                    da.Fill(dt);
                }
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
    protected void txtopdno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd2 = new SqlCommand("RECP_LAB_REC", con))
            {
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_REGISTRATION";
                cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtopdno.Text;
                cmd2.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                cmd2.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                cmd2.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da = new SqlDataAdapter(cmd2);
                //SqlCommand com = new SqlCommand("select ID,PNAME,CORPORATE from  REGISTRATION_TBL where ID='" + txtopdno.Text + "'", con);
                dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    txtname.Text = dr["PNAME"].ToString();
                    if (dr["CORPORATE"].ToString() == "0")
                    {
                        testype();
                    }
                    else
                    {
                        Session["mode"] = "true";
                        dr.Close();
                        using (SqlCommand cmd = new SqlCommand("RECP_LAB_REC", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEXTCHANGE_MULTIPLE";
                            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtopdno.Text;
                            cmd.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                            cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                            cmd.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            //da = new SqlDataAdapter("select DISTINCT B.ID AS ID,B.NAME AS NAME from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C,REGISTRATION_TBL D  WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND D.CORPORATE=A.C_ID AND  A.ORGID='" + lblorgid.Text + "' AND D.ID='" + txtopdno.Text + "'", con);
                            DataTable ds = new DataTable();
                            da.Fill(ds);
                            dropTestype.DataSource = ds;
                            dropTestype.DataTextField = "NAME";
                            dropTestype.DataValueField = "ID";
                            dropTestype.DataBind();
                            dropTestype.Items.Insert(0, "Please Select");
                        }
                    }
                    //droptesttype.Text = dr["SELECTYPE"].ToString();
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
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
            //if (chkCorporate.Checked)
            //{
            //    if (dropCorprt.SelectedIndex ==0)
            //{
            //    string message = "alert('* Please Enter Corporate.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            //     if (txtEmpid.Text == "")
            //{
            //    string message = "alert('* Please Enter EmpId.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}



            //}
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            //SqlCommand com = new SqlCommand("select * from TEST_CATEGORY_TABLE where NAME='" + txtname.Text.ToUpper() + "' and ORGID='" + lblorgid.Text + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    string message = "alert('Alredy Exist.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            //dr.Close();
            auto();
            labtory_item();
            if (SS == "FALSE")
            {
                string message = "alert('*Please select any item to save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            using (SqlCommand stock_cmd = new SqlCommand("USP_RECLABTOR", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

                stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text;
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
                // stock_cmd.Parameters.Add("@TESTNAME", SqlDbType.VarChar).Value = "0.00";   
                stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
                stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "0.00";

                stock_cmd.ExecuteNonQuery();
            }
            //-------------------------------------
            using (SqlCommand cmd = new SqlCommand("RECP_LAB_REC_INSRTUP", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                //cmd.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                //cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                //SqlCommand cmd = new SqlCommand("insert into LABRES_TABLE (ID,PID,ORGID,DATE,UNAME,UID,TYPE,PRICE,PNAME,TESTTYPE,PTYPE,TESTINDEX,REFBY,DISCAMT,PAIDAMT,DUEAMT)VALUES(@ID,@PID,@ORGID,@DATE,@UNAME,@UID,@TYPE,@PRICE,@PNAME,@TESTTYPE,@PTYPE,@TESTINDEX,@REFBY,@DISCAMT,@PAIDAMT,@DUEAMT)", con);
                //cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopdno.Text;
                cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
                //cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
                cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
                cmd.Parameters.Add("@TESTTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text.ToString();
                cmd.Parameters.Add("@TESTINDEX", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = txtdisc.Text;
                cmd.Parameters.Add("@PAIDAMT", SqlDbType.Decimal).Value = "0.00";
                cmd.Parameters.Add("@DUEAMT", SqlDbType.Decimal).Value = "0.00";
                cmd.Parameters.Add("@REFBY", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";

                cmd.ExecuteNonQuery();
            }
            //---------------------------
            binddata();
            con.Close();
            Session["labbill"] = txtid.Text;
            // Response.Redirect("~/RECEPTION/LabBillEntry.aspx");
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("../RECEPTION/SecondOBill.aspx");
    }
    public void labtory_item()
    {
        //try
        //{
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
                        stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text;
                        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                        stock_cmd.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropCorprt.SelectedItem.Text;
                        stock_cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = txtEmpid.Text;
                        stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                        stock_cmd.Parameters.Add("@TESTYPEPK", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                        stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                        stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = "0.00";
                        stock_cmd.Parameters.Add("@TOTALAMOUNT", SqlDbType.Decimal).Value = "0.00";
                        stock_cmd.Parameters.Add("@DISCOUNT", SqlDbType.Decimal).Value = "0.00";

                        // stock_cmd.Parameters.Add("@TESTNAME", SqlDbType.VarChar).Value = nameIt.Text;  
                        stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = INV.ToString();
                        stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = invIt.Text;
                        stock_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
                        stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

                        stock_cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand labres_cmd = new SqlCommand("RECP_LAB_REC_INSRTUP", con))
                    {
                        labres_cmd.CommandType = CommandType.StoredProcedure;
                        labres_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT1";
                        labres_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                        labres_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                        //cmd.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                        //cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                        //SqlCommand cmd = new SqlCommand("insert into LABRES_TABLE (ID,PID,ORGID,DATE,UNAME,UID,TYPE,PRICE,PNAME,TESTTYPE,PTYPE,TESTINDEX,REFBY,DISCAMT,PAIDAMT,DUEAMT)VALUES(@ID,@PID,@ORGID,@DATE,@UNAME,@UID,@TYPE,@PRICE,@PNAME,@TESTTYPE,@PTYPE,@TESTINDEX,@REFBY,@DISCAMT,@PAIDAMT,@DUEAMT)", con);
                        //cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                        labres_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopdno.Text;
                        labres_cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
                        //cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                        labres_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                        labres_cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
                        labres_cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = "";
                        labres_cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = "";
                        //labres_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
                        labres_cmd.Parameters.Add("@TESTTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text.ToString();
                        labres_cmd.Parameters.Add("@TESTINDEX", SqlDbType.VarChar).Value = "";
                        labres_cmd.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                        labres_cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = txtdisc.Text;
                        labres_cmd.Parameters.Add("@PAIDAMT", SqlDbType.Decimal).Value = "0.00";
                        labres_cmd.Parameters.Add("@DUEAMT", SqlDbType.Decimal).Value = "0.00";
                        labres_cmd.Parameters.Add("@REFBY", SqlDbType.VarChar).Value = "";
                        //labres_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
                        //SqlCommand labres_cmd = new SqlCommand("INSERT INTO LABRESULT_TABLE (ID,INV,PRICE) VALUES (@ID,@INV,@PRICE)", con);
                        //labres_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                        labres_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
                        labres_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = priceIt.Text;
                        labres_cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        //}
        //catch (Exception ex)
        //{

        //}
        con.Close();
    }
    protected void dropCorprt_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //    con.Open();
        //    SqlCommand com = new SqlCommand("select LAB from  Corporate_Table where CNAME='" + dropCorprt.SelectedItem.Text + "'", con);
        //    dr = com.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        txtdisc.Text = "0";
        //        txtdisc.Text = dr["LAB"].ToString();
        //        //droptesttype.Text = dr["SELECTYPE"].ToString();
        //    }
        //    else
        //    {
        //        txtdisc.Text = "0";
        //    }
        //    dr.Close();
        //    con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/LabBillEntry.aspx");
    }
    protected void grdlabill_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd3 = new SqlCommand("RECP_LAB_REC", con))
            {
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_RECLAB";
                cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd3.Parameters.Add("@PACKGNAME", SqlDbType.VarChar).Value = "NULL";
                cmd3.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                cmd3.Parameters.Add("@CNAME", SqlDbType.VarChar).Value = "NULL";
                SqlDataAdapter adp = new SqlDataAdapter(cmd3);
                //SqlDataAdapter adp = new SqlDataAdapter("select ID,OPDNO,NAME,CONVERT(varchar,DATE , 105) as  DATE FROM TBLRECLAB ORDER BY ID ASC", con);
                DataTable dt1 = new DataTable();
                adp.Fill(dt1);
                grdlabill.DataSource = dt1;
                grdlabill.PageIndex = e.NewPageIndex;
                grdlabill.DataKeyNames = new string[] { "ID" };
                grdlabill.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    
    }
}