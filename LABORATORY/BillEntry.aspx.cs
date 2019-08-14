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

public partial class LABORATORY_BillEntry : System.Web.UI.Page
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
    string PAIDMAT,SS;
    decimal amount = 0;
    [WebMethod]
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from LABRES_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("LT{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;
        dr.Close();
        con.Close();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
            Session["labbill"] = gr.Cells[0].Text;

            SqlCommand cmd = new SqlCommand("select PTYPE FROM LABRES_TABLE WHERE ID='" + gr.Cells[0].Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr["PTYPE"].ToString() == "INPATIENT")
                {
                    con.Close();
                    dr.Close();
                    Response.Redirect("~/LABORATORY/Bill.aspx");

                }
                else if (dr["PTYPE"].ToString() == "OUTPATIENT" || dr["PTYPE"].ToString() == "ONCOUNTER")
                {
                    con.Close();
                    dr.Close();
                    Response.Redirect("~/LABORATORY/SecondOBill.aspx");
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
        SqlDataAdapter da1 = new SqlDataAdapter("SELECT A.ID as ID,A.DATE AS DATE,A.PNAME AS PID FROM LABRES_TABLE A  WHERE  A.ORGID='" + lblorgid.Text + "'ORDER BY A.ID DESC", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        GridView2.SelectedIndex = 0;
        GridView2.DataSource = dt1;
        GridView2.DataKeyNames = new string[] { "ID" };
        GridView2.DataBind();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
            txtinvdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ID"), new DataColumn("INV"), new DataColumn("PRICE") });
                ViewState["ITEM"] = dt;
                this.BindGrid();
                binddata();
                da1 = new SqlDataAdapter("select ID,NAME FROM TEST_CATEGORY_TABLE where ORGID='" + lblorgid.Text + "'", con);
                DataTable ds1 = new DataTable();
                da1.Fill(ds1);
                droptesttype.DataSource = ds1;
                droptesttype.DataTextField = "NAME";
                droptesttype.DataValueField = "ID";
                droptesttype.DataBind();
                droptesttype.Items.Insert(0, "-----Select-----");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
    }
    protected void BindGrid()
    {
        try
        {
            GridView1.DataSource = (DataTable)ViewState["ITEM"];
            GridView1.DataBind();

        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void droptesttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (droptesttype.SelectedItem.Text == "SEROLOGY")
            {
                DataTable dt = (DataTable)ViewState["ITEM"];
                SqlDataAdapter da = new SqlDataAdapter("select A.slno AS ID ,A.NAME AS NAME,A.INV AS INV,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND B.ID='" + droptesttype.SelectedValue.ToString() + "' AND A.ORGID='" + lblorgid.Text + "'", con);
                da.Fill(dt);
                ViewState["ITEM"] = dt;
                this.BindGrid();
                SqlDataAdapter da1 = new SqlDataAdapter("select * from TEST_WIDAL_TABLE", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                Gvwidaltest.DataSource = null;
                Gvwidaltest.DataSource = dt1;
                Gvwidaltest.DataKeyNames = new string[] { "ID" };
                Gvwidaltest.DataBind();
                Gvwidaltest.Visible = false;

            }
            else
            {
                DataTable dt = (DataTable)ViewState["ITEM"];
                //dt.Rows.Add(txtcomp.Text.Trim(), txtname.Text.Trim(), txthsncode.Text.Trim(), txtbatchno.Text.Trim(), dropcate.Text.Trim(), txtexpirydate.Text.Trim(), droppurchaseunit.Text.Trim(), txtpprice.Text.Trim(), txtopening.Text.Trim(), txtfeeqty.Text.Trim(), txtdisc.Text.Trim(), txtdiscamt.Text.Trim(), txtamount.Text.Trim(), txtcgst.Text.Trim(), txtSgst.Text.Trim(), txtIGST.Text.Trim(), txtgstamount.Text.Trim(), txttotalamount.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter("select A.slno AS ID ,A.NAME AS NAME,A.INV AS INV,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND B.ID='" + droptesttype.SelectedValue.ToString() + "' AND A.ORGID='" + lblorgid.Text + "'", con);
                //DataTable dt = new DataTable();
                da.Fill(dt);
                ViewState["ITEM"] = dt;
                this.BindGrid();
            }
            div1.Visible = true;
            //GridView1.DataSource = dt;
            //GridView1.DataKeyNames = new string[] { "ID" };
            //GridView1.DataBind();
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
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Patient Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (droptesttype.SelectedIndex == 0)
            {
                string message = "alert('* Select Test Type.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (droprtype.SelectedIndex == 0 && txtIPNO.Text == "")
            {
                string message = "alert('* Please Enter OPD NO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (GridView1.Rows.Count <= 0)
            {
                string message = "alert('*Add item to Save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            datatable_po_item();
            if (SS == "FALSE")
            {
                string message = "alert('*Please select any test to save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
            }
            // datatable_po_item_WIDALTEST();
            datatable_po_item1();
            con.Close();
            Session["labbill"] = TXTID.Text;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //string message1 = "alert('*Insert Data Successfully !')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        
            Response.Redirect("~/LABORATORY/SecondOBill.aspx");
        //}
    
           // Response.Redirect("~/LABORATORY/BillEntry.aspx");
      //  }
    }
    public void datatable_po_item()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SS = "FALSE";
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                 var Inv = row.FindControl("lblinv") as Label;

          //  var value = row.FindControl("txt_value") as TextBox;

                if (chkRow.Checked)
                {
                    SS = "TRUE";
                    var value = row.FindControl("lblname") as Label;
                    var inv = row.FindControl("lblinv") as Label;
                    var price = row.FindControl("lblprice") as TextBox;
                    if (Inv.Text == "WIDAL AGGULUTINATION TEST")
                  {
                      datatable_po_item_WIDALTEST();
                      var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
                      SqlCommand stock_cmd = new SqlCommand("INSERT INTO LABRESULT_TABLE (ID,INV,PRICE) VALUES (@ID,@INV,@PRICE)", con);
                      stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                      stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
                      stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text.ToString();
                      stock_cmd.ExecuteNonQuery();
                  }
                    else
                  {
                         var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
                    SqlCommand stock_cmd = new SqlCommand("INSERT INTO LABRESULT_TABLE (ID,INV,PRICE) VALUES (@ID,@INV,@PRICE)", con);
                    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                    stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
                    stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text.ToString();
                    stock_cmd.ExecuteNonQuery();
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
    public void datatable_po_item_WIDALTEST()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        foreach (GridViewRow row in Gvwidaltest.Rows)
        {
            var inves = row.FindControl("lblinvw") as Label;
            var A140 = row.FindControl("txt140") as TextBox;
            var A160 = row.FindControl("txt160") as TextBox;
            var A180 = row.FindControl("txt180") as TextBox;
            var A320 = row.FindControl("txt320") as TextBox;
            var INV = Convert.ToInt32(Gvwidaltest.DataKeys[row.RowIndex].Values[0]);
            SqlCommand stock_cmd = new SqlCommand("INSERT INTO LAB_WIDALRESULT_TABLE (ID,INV,INVES,A140,A160,A180,A320) VALUES (@ID,@INV,@INVES,@A140,@A160,@A180,@A320)", con);
            stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
            stock_cmd.Parameters.Add("@INVES", SqlDbType.VarChar).Value = inves.Text.ToString();
            stock_cmd.Parameters.Add("@A140", SqlDbType.VarChar).Value = A140.Text.ToString();
            stock_cmd.Parameters.Add("@A160", SqlDbType.VarChar).Value = A160.Text.ToString();
            stock_cmd.Parameters.Add("@A180", SqlDbType.VarChar).Value = A180.Text.ToString();
            stock_cmd.Parameters.Add("@A320", SqlDbType.VarChar).Value = A320.Text.ToString();
            stock_cmd.ExecuteNonQuery();

        }
        //}
        //catch (Exception ex)
        //{

        //}
        con.Close();
    }
    public void datatable_po_item1()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into LABRES_TABLE (ID,PID,ORGID,DATE,UNAME,UID,TYPE,PRICE,PNAME,TESTTYPE,PTYPE,TESTINDEX,REFBY,DISCAMT,PAIDAMT,DUEAMT)VALUES(@ID,@PID,@ORGID,@DATE,@UNAME,@UID,@TYPE,@PRICE,@PNAME,@TESTTYPE,@PTYPE,@TESTINDEX,@REFBY,@DISCAMT,@PAIDAMT,@DUEAMT)", con);
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                    cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
                    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtinvdate.Text;
                    cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
                    cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = lbluid.Text;
                    cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = lblindetype.Text;
                    cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
                    cmd.Parameters.Add("@TESTTYPE", SqlDbType.VarChar).Value = droptesttype.SelectedItem.Text.ToString();
                    cmd.Parameters.Add("@TESTINDEX", SqlDbType.VarChar).Value = droptesttype.SelectedValue.ToString();
                    cmd.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.SelectedItem.Text.ToString();
                    cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = txtdiscamt.Text;
                    cmd.Parameters.Add("@PAIDAMT", SqlDbType.Decimal).Value = txtpaidamt.Text;
                    cmd.Parameters.Add("@DUEAMT", SqlDbType.Decimal).Value = lblbalanceamt.Text;

                    cmd.Parameters.Add("@REFBY", SqlDbType.VarChar).Value = txtref.Text;

                    cmd.ExecuteNonQuery();

                    using (SqlCommand cmd1 = new SqlCommand("usp_UserCollection", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                        cmd1.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["UID"].ToString();
                        cmd1.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
                        cmd1.Parameters.Add("@Collection", SqlDbType.Decimal).Value = txtpaidamt.Text;
                        cmd1.Parameters.Add("@Paid", SqlDbType.Decimal).Value = 0.00;
                        cmd1.ExecuteNonQuery();
                    }

                    if (droprtype.SelectedIndex == 0 || droprtype.SelectedIndex == 2)
                    {
                        using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LABTEST PAYMENT " + droptesttype.SelectedItem.Text;
                            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = '-' + amount.ToString();
                            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + amount.ToString();
                            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                            cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                            cm.ExecuteNonQuery();
                        }
                        using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
                            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = amount.ToString();
                            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = amount.ToString();
                            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                            cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                            cm.ExecuteNonQuery();
                        }

                    }
                    else
                    {
                        using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
                            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = amount.ToString();
                            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = amount.ToString();
                            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                            cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                            cm.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                    //}
                    //catch { }
                }
           
    public void datatable_po_item_UPDATE()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        foreach (GridViewRow row in GridView3.Rows)
        {
            var inv = row.FindControl("lblinv0") as Label;
            var value = row.FindControl("txt_value0") as TextBox;
            var price = row.FindControl("lblprice0") as TextBox;
            var AMT = row.FindControl("lblprice0") as TextBox;
            var re = row.FindControl("txtref") as TextBox;
            var unit = row.FindControl("txtunit") as TextBox;
            if (value.Text == "0" || value.Text == "")
            {
                AMT.Text = "0";
            }
            amount = amount + Convert.ToDecimal(AMT.Text);
            lbltotalprice.Text = amount.ToString();
            //var INV = Convert.ToInt32(GridView3.DataKeys[row.RowIndex].Values[0]);
            //SqlCommand stock_cmd = new SqlCommand("UPDATE LABRESULT_TABLE SET REF=@REF,UNIT=@UNIT,PRICE=@PRICE,VALUE=@VALUE WHERE ID=@ID AND INV=@INV", con);
            //stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
            //stock_cmd.Parameters.Add("@VALUE", SqlDbType.VarChar).Value = value.Text.ToString();
            //stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text.ToString();
            //stock_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = re.Text.ToString();
            //stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text.ToString();
            //stock_cmd.ExecuteNonQuery();
            //SqlCommand cmd = new SqlCommand("update LABIND_TABLE set STATUS=@STATUS where ID=@ID AND ORGID=@ORGID", con);
            //cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = Session["LINDID"].ToString();
            //cmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = "Finished";
            //cmd.ExecuteNonQuery();
        }
        //}
        //catch (Exception ex)
        //{

        //}
        con.Close();
    }
    public void datatable_po_item1_UPDATE()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE LABRES_TABLE SET PNAME=@PNAME,PRICE=@PRICE,UNAME=@UNAME,UID=@UID WHERE ID=@ID", con);
        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
        cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtname.Text;
        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
        cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtinvdate.Text;
        cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
        cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = lbluid.Text;
        cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = amount.ToString();
        cmd.ExecuteNonQuery();

        if (droprtype.SelectedIndex == 0 || droprtype.SelectedIndex == 2)
        {
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LABTEST PAYMENT " + droptesttype.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + LBPAIDAMT.Text.ToString();
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LABTEST PAYMENT " + droptesttype.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = amount.ToString();
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = amount.ToString();
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LABTEST PAYMENT " + droptesttype.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = amount.ToString();
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = amount.ToString();
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB DETAILS";
                cm.ExecuteNonQuery();
            }

        }
        else
        {
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = amount.ToString();
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = amount.ToString();
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                cm.ExecuteNonQuery();
            }
        }

        con.Close();
        //}
        //catch { }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Patient Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            //else if (droptesttype.SelectedIndex == 0)
            //{
            //    Response.Write("<script LANGUAGE='JavaScript' >alert('*Select Test Type.')</script>");

            //    return;
            //}
            else if (GridView3.Rows.Count <= 0)
            {
                string message = "alert('* *Add item to Save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            datatable_po_item_UPDATE();
            datatable_po_item1_UPDATE();
            Session["LABID"] = TXTID.Text;
            con.Close();
            if (droprtype.SelectedIndex == 0 || droprtype.SelectedIndex == 2)
            {
                Response.Redirect("~/LABORATORY/OBill.aspx");
            }
            else
            {
                Response.Redirect("~/LABORATORY/Bill.aspx");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("delete from LABRESULT_TABLE where ID='" + TXTID.Text + "'", con);
            DataSet d = new DataSet();
            da.Fill(d);
            SqlDataAdapter da1 = new SqlDataAdapter("delete from LABRES_TABLE where ID='" + TXTID.Text + "'", con);
            DataSet d1 = new DataSet();
            da1.Fill(d1);
            SqlDataAdapter da2 = new SqlDataAdapter("delete from LAB_WIDALRESULT_TABLE where ID='" + TXTID.Text + "'", con);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (droprtype.SelectedIndex == 0 || droprtype.SelectedIndex == 2)
            {
                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LABTEST PAYMENT " + droptesttype.SelectedItem.Text.ToString();
                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                    cm.ExecuteNonQuery();
                }
                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text.ToString();
                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                    cm.ExecuteNonQuery();
                }
            }
            else
            {
                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text.ToString();
                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
                    cm.ExecuteNonQuery();
                }
            }



            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/LABORATORY/BillEntry.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LABORATORY/BillEntry.aspx");
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT A.ID as ID,A.DATE AS DATE,A.PNAME AS PID FROM LABRES_TABLE A  WHERE  A.ORGID='" + lblorgid.Text + "'ORDER BY A.ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView2.SelectedIndex = 0;
        GridView2.DataSource = dt;
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataKeyNames = new string[] { "ID" };
        GridView2.DataBind();
        con.Close();
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select PNAME  AS NAME,PRICE,PID,TESTTYPE,PTYPE,TESTINDEX,REFBY from LABRES_TABLE  where ID='" + slno + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            btncreate.Visible = false;
            btnupdate.Visible = true;
            TXTID.Text = slno.ToString();
            txtname.Text = dr["NAME"].ToString();
            LBPAIDAMT.Text = dr["PRICE"].ToString();
            txtIPNO.Text = dr["PID"].ToString();
            droprtype.SelectedItem.Text = dr["PTYPE"].ToString();
            txtref.Text = dr["REFBY"].ToString();
            //droptesttype.SelectedItem.Text = dr["TESTTYPE"].ToString();
            droptesttype.SelectedValue = dr["TESTINDEX"].ToString();
            //droptesttype.SelectedItem.Text=
        }
        dr.Close();

        SqlDataAdapter da = new SqlDataAdapter("SELECT A.INV AS ID,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT,B.INV AS INV,A.VALUE AS VALUE FROM LABRESULT_TABLE A,TEST_COMPONENT_TABLE B WHERE A.INV=B.slno AND A.ID='" + slno.ToString() + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView3.SelectedIndex = 0;
        GridView3.DataSource = null;
        GridView3.DataSource = dt;
        GridView3.DataBind();
        btndelete.Visible = true;
        con.Close();
    }
    protected void txtIPNO_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            if (droprtype.SelectedIndex == 0)
            {
                SqlCommand com = new SqlCommand("select * from PATIENT_REG_TABLE where ID='" + txtIPNO.Text + "'", con);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    // lblinsurance.Text = "NA";
                    txtname.Text = dr["NAME"].ToString();
                    dr.Close();
                }
                else
                {
                    string message = "alert('*Invalid OPNO.')";
                    txtname.Text = "";
                    //  lblinsurance.Text = "";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    dr.Close();
                }
                dr.Close();
            }

            //else if (droprtype.SelectedIndex == 1)
            //{
            //    SqlCommand com = new SqlCommand("select * from ADMISSION_TABLE where VN='" + txtIPNO.Text + "'", con);
            //    dr = com.ExecuteReader();
            //    if (dr.Read())
            //    {
            //       // lblinsurance.Text = dr["INSURANCE"].ToString();
            //        txtname.Text = dr["NAME"].ToString();
            //        dr.Close();
            //    }
            //    else
            //    {
            //        string message = "alert('*Invalid IPNO.')";
            //        txtname.Text = "";
            //      //  lblinsurance.Text = "";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        dr.Close();
            //    }
            //    dr.Close();
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
    }
    protected void btnCal_Click(object sender, EventArgs e)
    {
       foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                if (chkRow.Checked)
                {
                    var value = row.FindControl("lblname") as Label;
                    var inv = row.FindControl("lblinv") as Label;
                    var AMT = row.FindControl("lblprice") as TextBox;
                    //var re = row.FindControl("txtref") as TextBox;
                    //var unit = row.FindControl("txtunit") as TextBox;
                    //var AMT = row.FindControl("lblprice") as TextBox;
                    if (value.Text == "0" || value.Text == "")
                    {
                        AMT.Text = "0";
                    }

                    amount = amount + Convert.ToDecimal(AMT.Text);

                }
            }
        }
        lbltotalprice.Text = amount.ToString();
        lblbalanceamt.Text = amount.ToString();
        lblbalanceamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(txtpaidamt.Text) - Convert.ToDouble(txtdiscamt.Text)).ToString();
        
       
    }
    protected void txtdiscamt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblbalanceamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(txtpaidamt.Text) - Convert.ToDouble(txtdiscamt.Text)).ToString();

        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txtpaidamt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblbalanceamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(txtpaidamt.Text) - Convert.ToDouble(txtdiscamt.Text)).ToString();
          //lblbalanceamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(txtpaidamt.Text)).ToString();

        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void droprtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (droprtype.SelectedIndex == 1)
        {
            txtIPNO.Text = "";
            txtIPNO.ReadOnly = true;

        }
        else
        {
            txtIPNO.Text = "";
            txtIPNO.ReadOnly = false;
        }
    }
    protected void ChckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        var value = row.FindControl("lblname") as Label;
                        var inv = row.FindControl("lblinv") as Label;
                        var AMT = row.FindControl("lblprice") as TextBox;
                        //var re = row.FindControl("txtref") as TextBox;
                        //var unit = row.FindControl("txtunit") as TextBox;
                        //var AMT = row.FindControl("lblprice") as TextBox;
                        if (value.Text == "0" || value.Text == "")
                        {
                            AMT.Text = "0";
                        }

                        amount = amount + Convert.ToDecimal(AMT.Text);

                    }
                }
            }
            lbltotalprice.Text = amount.ToString();
            lblbalanceamt.Text = amount.ToString();
            lblbalanceamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(txtpaidamt.Text) - Convert.ToDouble(txtdiscamt.Text)).ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}