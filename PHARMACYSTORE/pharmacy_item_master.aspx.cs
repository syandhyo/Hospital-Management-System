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
public partial class PHARMACYSTORE_pharmacy_item_master : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;
    DataMathods OBJ_METHOD = new DataMathods();

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
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
            binddata();
        }
    }

    public void binddata()
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("phrmc_ItemMstSelDel", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }

            DataSet ds1 = OBJ_METHOD.Get_DataSet("select distinct NAME FROM COMPANY_TABLE", false, false);
            dropcompany.DataSource = ds1;
            dropcompany.DataTextField = "NAME";
            dropcompany.DataValueField = "NAME";
            dropcompany.DataBind();
            dropcompany.Items.Insert(0, new ListItem("Please Select", "0"));

            DataSet ds2 = OBJ_METHOD.Get_DataSet("select distinct NAME FROM CATEGORY_TABLE", false, false);
            dropcate.DataSource = ds2;
            dropcate.DataTextField = "NAME";
            dropcate.DataValueField = "NAME";
            dropcate.DataBind();
            dropcate.Items.Insert(0, new ListItem("Please Select", "0"));


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
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    public void clear_control()
    {
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtid.Text = "";
        txtname.Text = "";
        txtpprice.Text = "0";
        txtsprice.Text = "0";
        txtopening.Text = "0";
        txthsncode.Text = "0";
        TXTGST.Text = "0";
        txtbatchno.Text = "0";
        btncreate.Visible = true;
        btnupdate.Visible = false;
        txtmfgdate0.Text = "";
        txtexpirydate.Text = "";
        txtrack.Text = "";
        txtself.Text = "";
        txtreorder.Text = "";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!! Enter The Item Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txthsncode.Text == "" || txthsncode.Text == "0")
            {
                string message = "alert('Please!! Enter HSN Code..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txthsncode.Focus();
                return;
            }
            else if (txtbatchno.Text == "" || txtbatchno.Text == "0")
            {
                string message = "alert('Please!! Enter Batch Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbatchno.Focus();
                return;
            }
            else if (dropcate.SelectedValue == "TABLET" && txttabletperstrip.Text == "")
            {
                string message = "alert('Please!! Enter Tablet Per Strip..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txttabletperstrip.Focus();
                return;
            }
            else if (dropcate.SelectedValue == "TABLET" && txttabletperstrip.Text == "0")
            {
                string message = "alert('Please!! Enter Tablet Per Strip..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txttabletperstrip.Focus();
                return;
            }
            else if (droppurchaseunit.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select Purchase Unit..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                droppurchaseunit.Focus();
                return;
            }
            else if (txtself.Text == "")
            {
                string message = "alert('Please!! plese Enter The Shelf..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtself.Focus();
                return;
            }
            else if (txtrack.Text == "")
            {
                string message = "alert('Please!! plese Enter The Rack..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtself.Focus();
                return;
            }
            else if (txtreorder.Text == "")
            {
                string message = "alert('Please!! plese Enter The Reorder point..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtreorder.Focus();
                return;
            }
            else if (txtpprice.Text == "")
            {
                string message = "alert('Please!! Enter Purchase Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpprice.Focus();
                return;
            }
            else if (Convert.ToDecimal(txtpprice.Text) <= 0)
            {
                string message = "alert('Please!! Enter Purchase Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpprice.Focus();
                return;
            }
            else if (txtsprice.Text == "")
            {
                string message = "alert('Please!! Enter Sale Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtsprice.Focus();
                return;
            }
            else if (Convert.ToDecimal(txtsprice.Text) < Convert.ToDecimal(txtpprice.Text))
            {
                string message = "alert('Please!! MRP Should More Than Purchase Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtsprice.Focus();
                return;
            }
            else if (TXTGST.Text == "")
            {
                string message = "alert('Please!! Enter GST In %..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                TXTGST.Focus();
                return;
            }
            else if (txttabletperstrip.Text == "")
            {
                txttabletperstrip.Text = "0";

            }
            else if (Convert.ToDecimal(txttabletperstrip.Text) < 0)
            {
                txttabletperstrip.Text = "0";

            }
            else if (Convert.ToDecimal(txtsprice.Text) <= 0)
            {
                string message = "alert('Please!! Enter Sale Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtsprice.Focus();
                return;
            }
            else if (txtexpirydate.Text == "")
            {
                string message = "alert('Please!! Enter Expiry Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtexpirydate.Focus();
                return;
            }
            else if (txtreorder.Text == "")
            {
                txtreorder.Text = "0";
            }
            else if (txtmfgdate0.Text == "")
            {
                string message = "alert('Please!! Enter Manufacturing Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtmfgdate0.Focus();
                return;
            }

            else if (txtopening.Text == "")
            {
                string message = "alert('Please!! Enter Opening Balance..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtopening.Focus();
                return;
            }

            #region extra code
            // date split format
            //string[] strmfd = txtmfgdate0.Text.Split('-');

            //DateTime mfd = new DateTime(Convert.ToInt32(strmfd[2]), Convert.ToInt32(strmfd[1]), Convert.ToInt32(strmfd[0]));

            //string[] strexp = txtexpirydate.Text.Split('-');

            //DateTime exp = new DateTime(Convert.ToInt32(strexp[2]), Convert.ToInt32(strexp[1]), Convert.ToInt32(strexp[0]));

            SqlParameter[] SQL_PARAMS = new SqlParameter[20];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, dropcompany.Text.ToUpper());
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, dropcate.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@SUNIT", SqlDbType.VarChar, 500, dropsaleunit.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@PUNIT", SqlDbType.VarChar, 500, droppurchaseunit.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@GST", SqlDbType.Decimal, 0, TXTGST.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@PPRICE", SqlDbType.Decimal, 0, txtpprice.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@SPRICE", SqlDbType.Decimal, 0, txtsprice.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, txthsncode.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 50, txtbatchno.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, txtopening.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@TPS", SqlDbType.Decimal, 0, txttabletperstrip.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@SELF", SqlDbType.VarChar, 50, txtself.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@RACK", SqlDbType.VarChar, 50, txtrack.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@ROP", SqlDbType.Decimal, 0, txtreorder.Text);
            SQL_PARAMS[15] = OBJ_METHOD.createParams("@MFG", SqlDbType.VarChar, 50, txtmfgdate0.Text);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@EXPDATE", SqlDbType.VarChar, 50, txtexpirydate.Text);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[18] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[19] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("phrmc_ItemMstInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            var id = OBJ_METHOD._objOut;
            string status = id.ToString().Split('-')[0];
            string master_id = id.ToString().Split('-')[1];
            if (status == "2")
            {
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            else
            {
                if (OBJ_METHOD._RESULT > 0)
                {
                    SqlParameter[] SQL_PARAMS1 = new SqlParameter[19];

                    SQL_PARAMS1[0] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                    SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, master_id);
                    SQL_PARAMS1[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, lbldate.Text);
                    SQL_PARAMS1[4] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, dropcompany.Text.ToUpper());
                    SQL_PARAMS1[5] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 200, txtname.Text.ToUpper());
                    SQL_PARAMS1[6] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 200, dropcate.Text);
                    SQL_PARAMS1[7] = OBJ_METHOD.createParams("@PUNIT", SqlDbType.VarChar, 500, droppurchaseunit.Text);
                    SQL_PARAMS1[8] = OBJ_METHOD.createParams("@SUNIT", SqlDbType.VarChar, 50, dropsaleunit.Text);
                    SQL_PARAMS1[9] = OBJ_METHOD.createParams("@SPRICE", SqlDbType.Decimal, 0, txtsprice.Text);
                    SQL_PARAMS1[10] = OBJ_METHOD.createParams("@PPRICE", SqlDbType.Decimal, 0, txtpprice.Text);
                    SQL_PARAMS1[11] = OBJ_METHOD.createParams("@OPENING", SqlDbType.Decimal, 0, txtopening.Text);
                    SQL_PARAMS1[12] = OBJ_METHOD.createParams("@PRETURN", SqlDbType.Decimal, 0, "0");
                    SQL_PARAMS1[13] = OBJ_METHOD.createParams("@PURCHASE", SqlDbType.Decimal, 0, "0");
                    SQL_PARAMS1[14] = OBJ_METHOD.createParams("@CLOSEING", SqlDbType.Decimal, 0, txtopening.Text);
                    SQL_PARAMS1[15] = OBJ_METHOD.createParams("@EXP", SqlDbType.VarChar, 200, txtexpirydate.Text);
                    SQL_PARAMS1[16] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
                    SQL_PARAMS1[17] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                    SQL_PARAMS1[18] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

                    OBJ_METHOD.ExecuteProceedure("phrmc_ItemStkTrInsUp", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        SqlParameter[] SQL_PARAMS2 = new SqlParameter[10];

                        SQL_PARAMS2[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                        SQL_PARAMS2[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, master_id);
                        SQL_PARAMS2[2] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, dropcompany.Text.ToUpper());
                        SQL_PARAMS2[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 200, txtname.Text.ToUpper());
                        SQL_PARAMS2[4] = OBJ_METHOD.createParams("@EXPDATE", SqlDbType.VarChar, 200, txtexpirydate.Text);
                        SQL_PARAMS2[5] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, txtopening.Text);
                        SQL_PARAMS2[6] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 200, txtbatchno.Text);
                        SQL_PARAMS2[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
                        SQL_PARAMS2[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                        SQL_PARAMS2[9] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

                        OBJ_METHOD.ExecuteProceedure("phrmc_ItemStkTblIns", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            OBJ_METHOD.commitOrRollbackTran("commit");
                            binddata();
                            message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        }
                        else
                        {
                            OBJ_METHOD.commitOrRollbackTran("rollback");
                            message1 = "alert('Due to some issues, Data not saved.')";
                        }
                        //clear();
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Due to some issues, Data not saved.')";
                    }
                    //clear();
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Due to some issues, Data not saved.')";
                }
            }
            #endregion
            // satayjit sir coding

            //======================================================================================================================

            #region NewCode
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();

            //SqlCommand com = new SqlCommand("select * from ITEM_TABLE where NAME='" + txtname.Text.ToUpper() + "' AND BATCHNO='" + txtbatchno.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    string message = "alert('* Alredy Exist.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            //dr.Close();

            // auto();
            //if (txtmfgdate0.Text == "")
            //{
            //    txtmfgdate0.Text = "NULL";
            //}
            //SqlCommand cmd1 = new SqlCommand("insert into ITEM_TABLE (ID,COMPANY,NAME,CATEGORY,PUNIT,SUNIT,PPRICE,SPRICE,GST,HSNCODE,ORGID,BATCHNO,QTY,TPS,SELF,RACK,ROP,MFG,EXPDATE) values (@ID,@COMPANY,@NAME,@CATEGORY,@PUNIT,@SUNIT,@PPRICE,@SPRICE,@GST,@HSNCODE,@ORGID,@BATCHNO,@QTY,@TPS,@SELF,@RACK,@ROP,@MFG,@EXPDATE)", con);

            //using (SqlCommand cmd1 = new SqlCommand("phrmc_ItemMstInsUp", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text.ToUpper();
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //    cmd1.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = dropcate.Text;
            //    cmd1.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = droppurchaseunit.Text;
            //    cmd1.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = dropsaleunit.Text;
            //    cmd1.Parameters.Add("@GST", SqlDbType.Decimal).Value = TXTGST.Text;
            //    cmd1.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = txtpprice.Text;
            //    cmd1.Parameters.Add("@SPRICE", SqlDbType.Decimal).Value = txtsprice.Text;
            //    cmd1.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = txthsncode.Text;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
            //    cmd1.Parameters.Add("@QTY", SqlDbType.Decimal).Value = txtopening.Text;
            //    cmd1.Parameters.Add("@TPS", SqlDbType.Decimal).Value = txttabletperstrip.Text;
            //    cmd1.Parameters.Add("@SELF", SqlDbType.VarChar).Value = txtself.Text;
            //    cmd1.Parameters.Add("@RACK", SqlDbType.VarChar).Value = txtrack.Text;
            //    cmd1.Parameters.Add("@ROP", SqlDbType.Decimal).Value = txtreorder.Text;
            //    cmd1.Parameters.Add("@MFG", SqlDbType.VarChar).Value = txtmfgdate0.Text;
            //    cmd1.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = txtexpirydate.Text;
            //    cmd1.ExecuteNonQuery();
            //}

            //SqlCommand cmd3 = new SqlCommand("insert into STOCK_TRAN (FYEAR,ORGID,DATE,ID,COMPANY,ITEMNAME,CATEGORY,PUNIT,SUNIT,PPRICE,SPRICE,OPENING,PURCHASE,PRETURN,SALE,SRETURN,CLOSEING,EXP) values (@FYEAR,@ORGID,@DATE,@ID,@COMPANY,@ITEMNAME,@CATEGORY,@PUNIT,@SUNIT,@PPRICE,@SPRICE,@OPENING,@PURCHASE,@PRETURN,@SALE,@SRETURN,@CLOSEING,@EXP)", con);



            //using (SqlCommand cmd3 = new SqlCommand("phrmc_ItemStkTrInsUp", con))
            //{
            //    cmd3.CommandType = CommandType.StoredProcedure;
            //    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd3.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd3.Parameters.Add("@DATE", SqlDbType.Date).Value = lbldate.Text;
            //    cmd3.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text.ToUpper();
            //    cmd3.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //    cmd3.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = dropcate.Text;
            //    cmd3.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = droppurchaseunit.Text;
            //    cmd3.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = dropsaleunit.Text;
            //    cmd3.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = txtpprice.Text;
            //    cmd3.Parameters.Add("@SPRICE", SqlDbType.Decimal).Value = txtsprice.Text;
            //    cmd3.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtopening.Text;
            //    cmd3.Parameters.Add("@PURCHASE", SqlDbType.Decimal).Value = "0";
            //    cmd3.Parameters.Add("@PRETURN", SqlDbType.Decimal).Value = "0";
            //    cmd3.Parameters.Add("@SALE", SqlDbType.Decimal).Value = "0";
            //    cmd3.Parameters.Add("@SRETURN", SqlDbType.Decimal).Value = "0";
            //    cmd3.Parameters.Add("@CLOSEING", SqlDbType.Decimal).Value = txtopening.Text;
            //    cmd3.Parameters.Add("@EXP", SqlDbType.VarChar).Value = txtexpirydate.Text;
            //    cmd3.ExecuteNonQuery();
            //}
            // SqlCommand cmd2 = new SqlCommand("insert into STOCK_TABLE (ID,COMPANY,NAME,EXPDATE,QTY,ORGID,BATCHNO) values (@ID,@COMPANY,@NAME,@EXPDATE,@QTY,@ORGID,@BATCHNO)", con);



            //using (SqlCommand cmd2 = new SqlCommand("phrmc_ItemStkTblIns", con))
            // {
            //     cmd2.CommandType = CommandType.StoredProcedure;
            //     cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //     cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //     cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //     cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text;
            //     cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //     cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = txtexpirydate.Text;
            //     cmd2.Parameters.Add("@QTY", SqlDbType.Decimal).Value = txtopening.Text;
            //     cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
            //     cmd2.ExecuteNonQuery();
            // }
            //---------------------------
            //binddata();

            #endregion

            #region latest code
            //SqlParameter[] SQL_PARAMS = new SqlParameter[20];

            //SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            //SQL_PARAMS[1] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, dropcompany.Text.ToUpper());
            //SQL_PARAMS[2] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, dropcate.Text);
            //SQL_PARAMS[3] = OBJ_METHOD.createParams("@SUNIT", SqlDbType.VarChar, 500, dropsaleunit.Text);
            //SQL_PARAMS[4] = OBJ_METHOD.createParams("@PUNIT", SqlDbType.VarChar, 500, droppurchaseunit.Text);
            //SQL_PARAMS[5] = OBJ_METHOD.createParams("@GST", SqlDbType.Decimal, 0, TXTGST.Text);
            //SQL_PARAMS[6] = OBJ_METHOD.createParams("@PPRICE", SqlDbType.Decimal, 0, txtpprice.Text);
            //SQL_PARAMS[7] = OBJ_METHOD.createParams("@SPRICE", SqlDbType.Decimal, 0, txtsprice.Text);
            //SQL_PARAMS[8] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, txthsncode.Text);
            //SQL_PARAMS[9] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 50, txtbatchno.Text);
            //SQL_PARAMS[10] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, txtopening.Text);
            //SQL_PARAMS[11] = OBJ_METHOD.createParams("@TPS", SqlDbType.Decimal, 0, txttabletperstrip.Text);
            //SQL_PARAMS[12] = OBJ_METHOD.createParams("@SELF", SqlDbType.VarChar, 50, txtself.Text);
            //SQL_PARAMS[13] = OBJ_METHOD.createParams("@RACK", SqlDbType.VarChar, 50, txtrack.Text);
            //SQL_PARAMS[14] = OBJ_METHOD.createParams("@ROP", SqlDbType.Decimal, 0, txtreorder.Text);
            //SQL_PARAMS[15] = OBJ_METHOD.createParams("@MFG", SqlDbType.VarChar, 50, txtmfgdate0.Text);
            //SQL_PARAMS[16] = OBJ_METHOD.createParams("@EXPDATE", SqlDbType.VarChar, 50, txtexpirydate.Text);
            //SQL_PARAMS[17] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            //SQL_PARAMS[18] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            //SQL_PARAMS[19] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            //OBJ_METHOD.ExecuteProceedure("phrmc_ItemMstInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            //if (OBJ_METHOD._RESULT > 0)
            //{
            //    OBJ_METHOD.commitOrRollbackTran("commit");
            //    binddata();
            //}
            #endregion

        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clear_control();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 200, slno);
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("phrmc_ItemMstSelDel", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = DS1.Tables[0].Rows[0]["ID"].ToString();
                dropcompany.Text = DS1.Tables[0].Rows[0]["COMPANY"].ToString();
                txtname.Text = DS1.Tables[0].Rows[0]["NAME"].ToString();
                txthsncode.Text = DS1.Tables[0].Rows[0]["HSNCODE"].ToString();
                txtbatchno.Text = DS1.Tables[0].Rows[0]["BATCHNO"].ToString();
                dropcate.Text = DS1.Tables[0].Rows[0]["CATEGORY"].ToString();
                droppurchaseunit.Text = DS1.Tables[0].Rows[0]["PUNIT"].ToString();
                dropsaleunit.Text = DS1.Tables[0].Rows[0]["SUNIT"].ToString();
                txtpprice.Text = DS1.Tables[0].Rows[0]["PPRICE"].ToString();
                txtsprice.Text = DS1.Tables[0].Rows[0]["SPRICE"].ToString();
                TXTGST.Text = DS1.Tables[0].Rows[0]["GST"].ToString();
                txtopening.Text = DS1.Tables[0].Rows[0]["QTY"].ToString();
                txtexpirydate.Text = DS1.Tables[0].Rows[0]["EXPDATE"].ToString();
                txtmfgdate0.Text = DS1.Tables[0].Rows[0]["MFG"].ToString();
                txttabletperstrip.Text = DS1.Tables[0].Rows[0]["TPS"].ToString();
                txtself.Text = DS1.Tables[0].Rows[0]["SELF"].ToString();
                txtrack.Text = DS1.Tables[0].Rows[0]["RACK"].ToString();
                txtreorder.Text = DS1.Tables[0].Rows[0]["ROP"].ToString();
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txthsncode.Text == "")
            {
                string message = "alert('Please!! Enter HSN Code..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txthsncode.Focus();
                return;
            }
            else if (txtbatchno.Text == "")
            {
                string message = "alert('Please!! Enter Batch Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbatchno.Focus();
                return;
            }
            else if (dropcate.SelectedIndex == 5 && txttabletperstrip.Text == "" || txttabletperstrip.Text == "0")
            {
                string message = "alert('Please!! Enter Tablet Per Strip..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txttabletperstrip.Focus();
                return;
            }
            else if (droppurchaseunit.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select Purchase Unit..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                droppurchaseunit.Focus();
                return;
            }
            else if (txtself.Text == "")
            {
                string message = "alert('Please!! plese Enter The Shelf..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtself.Focus();
                return;
            }
            else if (txtrack.Text == "")
            {
                string message = "alert('Please!! plese Enter The Rack..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtself.Focus();
                return;
            }
            else if (txtreorder.Text == "")
            {
                string message = "alert('Please!! plese Enter The Reorder point..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtreorder.Focus();
                return;
            }
            else if (txtpprice.Text == "")
            {
                string message = "alert('Please!! Enter Purchase Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpprice.Focus();
                return;
            }
            else if (Convert.ToDecimal(txtpprice.Text) <= 0)
            {
                string message = "alert('Please!! Enter Purchase Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpprice.Focus();
                return;
            }
            else if (txtsprice.Text == "")
            {
                string message = "alert('Please!! Enter Sale Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtsprice.Focus();
                return;
            }
            else if (Convert.ToDecimal(txtsprice.Text) < Convert.ToDecimal(txtpprice.Text))
            {
                string message = "alert('Please!! MRP Should More Than Purchase Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtsprice.Focus();
                return;
            }
            else if (TXTGST.Text == "")
            {
                string message = "alert('Please!! Enter GST In %..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                TXTGST.Focus();
                return;
            }

            else if (txttabletperstrip.Text == "")
            {
                txttabletperstrip.Text = "0";

            }
            else if (Convert.ToDecimal(txttabletperstrip.Text) < 0)
            {
                txttabletperstrip.Text = "0";

            }
            else if (Convert.ToDecimal(txtsprice.Text) <= 0)
            {
                string message = "alert('Please!! Enter Sale Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtsprice.Focus();
                return;
            }
            else if (txtexpirydate.Text == "")
            {
                string message = "alert('Please!! Enter Expiry Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtexpirydate.Focus();
                return;
            }
            else if (txtreorder.Text == "")
            {
                txtreorder.Text = "0";
            }
            else if (txtmfgdate0.Text == "")
            {
                string message = "alert('Please!! Enter Manufacturing Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtmfgdate0.Focus();
                return;
            }
            else if (txtopening.Text == "")
            {
                string message = "alert('Please!! Enter Opening Balance..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtopening.Focus();
                return;
            }

            if (txtexpirydate.Text == "")
            {
                txtexpirydate.Text = "NULL";
            }

            #region OLD CODE
            //SqlCommand cmd1 = new SqlCommand("UPDATE ITEM_TABLE SET COMPANY=@COMPANY, NAME=@NAME,CATEGORY=@CATEGORY,PUNIT=@PUNIT,SUNIT=@SUNIT,PPRICE=@PPRICE,SPRICE=@SPRICE,GST=@GST,HSNCODE=@HSNCODE,BATCHNO=@BATCHNO,QTY=@QTY,TPS=@TPS,SELF=@SELF,RACK=@RACK,ROP=@ROP,MFG=@MFG,EXPDATE=@EXPDATE WHERE ID=@ID AND ORGID=@ORGID ", con);
            //using (SqlCommand cmd1 = new SqlCommand("phrmc_ItemMstInsUp", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text.ToUpper();
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //    cmd1.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = dropcate.Text;
            //    cmd1.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = droppurchaseunit.Text;
            //    cmd1.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = dropsaleunit.Text;
            //    cmd1.Parameters.Add("@GST", SqlDbType.Decimal).Value = TXTGST.Text;
            //    cmd1.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = txtpprice.Text;
            //    cmd1.Parameters.Add("@SPRICE", SqlDbType.Decimal).Value = txtsprice.Text;
            //    cmd1.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = txthsncode.Text;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
            //    cmd1.Parameters.Add("@QTY", SqlDbType.Decimal).Value = txtopening.Text;
            //    cmd1.Parameters.Add("@TPS", SqlDbType.Decimal).Value = txttabletperstrip.Text;
            //    cmd1.Parameters.Add("@SELF", SqlDbType.VarChar).Value = txtself.Text;
            //    cmd1.Parameters.Add("@RACK", SqlDbType.VarChar).Value = txtrack.Text;
            //    cmd1.Parameters.Add("@ROP", SqlDbType.Decimal).Value = txtreorder.Text;
            //    cmd1.Parameters.Add("@MFG", SqlDbType.VarChar).Value = txtmfgdate0.Text;
            //    cmd1.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = txtexpirydate.Text;
            //    cmd1.ExecuteNonQuery();
            //}


            //-----------------------------
            //SqlDataAdapter da = new SqlDataAdapter("delete from STOCK_TRAN WHERE ID='" + txtid.Text + "'", con);
            //DataSet ds = new DataSet();
            // da.Fill(ds, "STOCK_TRAN");


            //SqlCommand cmd3 = new SqlCommand("insert into STOCK_TRAN (FYEAR,ORGID,DATE,ID,COMPANY,ITEMNAME,CATEGORY,PUNIT,SUNIT,PPRICE,SPRICE,OPENING,PURCHASE,PRETURN,SALE,SRETURN,CLOSEING,EXP) values (@FYEAR,@ORGID,@DATE,@ID,@COMPANY,@ITEMNAME,@CATEGORY,@PUNIT,@SUNIT,@PPRICE,@SPRICE,@OPENING,@PURCHASE,@PRETURN,@SALE,@SRETURN,@CLOSEING,@EXP)", con);
            //using (SqlCommand cmd3 = new SqlCommand("phrmc_ItemStkTrInsUp", con))
            //{
            //    cmd3.CommandType = CommandType.StoredProcedure;
            //    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd3.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd3.Parameters.Add("@DATE", SqlDbType.Date).Value = lbldate.Text;
            //    cmd3.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text.ToUpper();
            //    cmd3.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //    cmd3.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = dropcate.Text;
            //    cmd3.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = droppurchaseunit.Text;
            //    cmd3.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = dropsaleunit.Text;
            //    cmd3.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = txtpprice.Text;
            //    cmd3.Parameters.Add("@SPRICE", SqlDbType.Decimal).Value = txtsprice.Text;
            //    cmd3.Parameters.Add("@GST", SqlDbType.Decimal).Value = TXTGST.Text;
            //    cmd3.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtopening.Text;
            //    cmd3.Parameters.Add("@PURCHASE", SqlDbType.Decimal).Value = "0";
            //    cmd3.Parameters.Add("@PRETURN", SqlDbType.Decimal).Value = "0";
            //    cmd3.Parameters.Add("@SALE", SqlDbType.Decimal).Value = "0";
            //    cmd3.Parameters.Add("@SRETURN", SqlDbType.Decimal).Value = "0";
            //    cmd3.Parameters.Add("@CLOSEING", SqlDbType.Decimal).Value = txtopening.Text;
            //    cmd3.Parameters.Add("@EXP", SqlDbType.VarChar).Value = txtexpirydate.Text;
            //    cmd3.ExecuteNonQuery();
            //    cmd3.ExecuteNonQuery();
            //}

            //--------------------------------------------

            //SqlDataAdapter da1 = new SqlDataAdapter("delete from STOCK_TABLE WHERE ID='" + txtid.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
            //DataSet ds1 = new DataSet();
            //da1.Fill(ds1, "STOCK_TABLE");

            // SqlCommand cmd2 = new SqlCommand("insert into STOCK_TABLE (ID,COMPANY,NAME,EXPDATE,QTY,ORGID,BATCHNO) values (@ID,@COMPANY,@NAME,@EXPDATE,@QTY,@ORGID,@BATCHNO)", con);

            //using (SqlCommand cmd2 = new SqlCommand("phrmc_ItemStkTblIns", con))
            //{
            //    cmd2.CommandType = CommandType.StoredProcedure;
            //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text;
            //    cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //    cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = txtexpirydate.Text;
            //    cmd2.Parameters.Add("@QTY", SqlDbType.Decimal).Value = txtopening.Text;
            //    cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
            //    cmd2.ExecuteNonQuery();
            //}

            #endregion


            #region New CODE
            // date split
            //string[] strmfd = txtmfgdate0.Text.Split('-');

            //DateTime mfd = new DateTime(Convert.ToInt32(strmfd[2]), Convert.ToInt32(strmfd[1]), Convert.ToInt32(strmfd[0]));

            //string[] strexp = txtexpirydate.Text.Split('-');

            //DateTime exp = new DateTime(Convert.ToInt32(strexp[2]), Convert.ToInt32(strexp[1]), Convert.ToInt32(strexp[0]));

            SqlParameter[] SQL_PARAMS = new SqlParameter[19];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, dropcompany.Text.ToUpper());
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, dropcate.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@SUNIT", SqlDbType.VarChar, 500, dropsaleunit.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@PUNIT", SqlDbType.VarChar, 500, droppurchaseunit.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@GST", SqlDbType.Decimal, 0, TXTGST.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@PPRICE", SqlDbType.Decimal, 0, txtpprice.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@SPRICE", SqlDbType.Decimal, 0, txtsprice.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, txthsncode.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 50, txtbatchno.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, txtopening.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@TPS", SqlDbType.Decimal, 0, txttabletperstrip.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@SELF", SqlDbType.VarChar, 50, txtself.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@RACK", SqlDbType.VarChar, 50, txtrack.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@ROP", SqlDbType.Decimal, 0, txtreorder.Text);
            SQL_PARAMS[15] = OBJ_METHOD.createParams("@MFG", SqlDbType.VarChar, 50, txtmfgdate0.Text);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@EXPDATE", SqlDbType.VarChar, 50, txtexpirydate.Text);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[18] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 100, txtid.Text);

            OBJ_METHOD.ExecuteProceedure("phrmc_ItemMstInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            //var id = OBJ_METHOD._objOut;
            //string status = id.ToString().Split('-')[0];
            //string master_id = id.ToString().Split('-')[1];
            //if (status == "2")
            //{
            //    message1 = "alert('" + OBJ_METHOD._objOut + "')";
            //}
            //else
            //{
            if (OBJ_METHOD._RESULT > 0)
            {

                SqlParameter[] SQL_PARAMS1 = new SqlParameter[19];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, lbldate.Text);
                SQL_PARAMS1[4] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, dropcompany.Text.ToUpper());
                SQL_PARAMS1[5] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 200, txtname.Text.ToUpper());
                SQL_PARAMS1[6] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 200, dropcate.Text);
                SQL_PARAMS1[7] = OBJ_METHOD.createParams("@PUNIT", SqlDbType.VarChar, 500, droppurchaseunit.Text);
                SQL_PARAMS1[8] = OBJ_METHOD.createParams("@SUNIT", SqlDbType.VarChar, 50, dropsaleunit.Text);
                SQL_PARAMS1[9] = OBJ_METHOD.createParams("@SPRICE", SqlDbType.Decimal, 0, txtsprice.Text);
                SQL_PARAMS1[10] = OBJ_METHOD.createParams("@PPRICE", SqlDbType.Decimal, 0, txtpprice.Text);
                SQL_PARAMS1[11] = OBJ_METHOD.createParams("@OPENING", SqlDbType.Decimal, 0, txtopening.Text);
                SQL_PARAMS1[12] = OBJ_METHOD.createParams("@PRETURN", SqlDbType.Decimal, 0, "0");
                SQL_PARAMS1[13] = OBJ_METHOD.createParams("@PURCHASE", SqlDbType.Decimal, 0, "0");
                SQL_PARAMS1[14] = OBJ_METHOD.createParams("@CLOSEING", SqlDbType.Decimal, 0, txtopening.Text);
                SQL_PARAMS1[15] = OBJ_METHOD.createParams("@EXP", SqlDbType.VarChar, 200, txtexpirydate.Text);
                SQL_PARAMS1[16] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
                SQL_PARAMS1[17] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                SQL_PARAMS1[18] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT_DELETE");

                OBJ_METHOD.ExecuteProceedure("phrmc_ItemStkTrInsUp", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SqlParameter[] SQL_PARAMS2 = new SqlParameter[10];

                    SQL_PARAMS2[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS2[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS2[2] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, dropcompany.Text.ToUpper());
                    SQL_PARAMS2[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 200, txtname.Text.ToUpper());
                    SQL_PARAMS2[4] = OBJ_METHOD.createParams("@EXPDATE", SqlDbType.VarChar, 200, txtexpirydate.Text);
                    SQL_PARAMS2[5] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, txtopening.Text);
                    SQL_PARAMS2[6] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 200, txtbatchno.Text);
                    SQL_PARAMS2[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
                    SQL_PARAMS2[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                    SQL_PARAMS2[9] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT_DELETE");

                    OBJ_METHOD.ExecuteProceedure("phrmc_ItemStkTblIns", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        binddata();
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        btncreate.Visible = true;
                        btnupdate.Visible = false;
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Due to some issues, Data not saved.')";
                    }
                    //clear();
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Due to some issues, Data not saved.')";
                }
                //clear();
                //}
                //else
                //{
                //    OBJ_METHOD.commitOrRollbackTran("rollback");
                //    message1 = "alert('Due to some issues, Data not saved.')";
                //}
            }

            #endregion

        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clear_control();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }

    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            #region OLD CODE
            //SqlCommand cm = new SqlCommand("delete from STOCK_TRAN where ID='" + slno + "'", con);        
            //using (SqlCommand cm = new SqlCommand("phrmc_ItemMstSelDel", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELSTTRAN";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cm.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
            //    cm.ExecuteNonQuery();
            //}
            ////SqlCommand cm1 = new SqlCommand("delete from ITEM_TABLE where ID='" + slno + "'", con);

            //using (SqlCommand cm1 = new SqlCommand("phrmc_ItemMstSelDel", con))
            //{
            //    cm1.CommandType = CommandType.StoredProcedure;
            //    cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELITEMTBL";
            //    cm1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cm1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    cm1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
            //    cm1.ExecuteNonQuery();
            //}
            ////SqlDataAdapter da1 = new SqlDataAdapter("delete from STOCK_TABLE WHERE ID='" + slno + "'", con);
            //using (SqlCommand cm2 = new SqlCommand("phrmc_ItemMstSelDel", con))
            //{
            //    cm2.CommandType = CommandType.StoredProcedure;
            //    cm2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELSTTBL";
            //    cm2.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cm2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    cm2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter adp = new SqlDataAdapter(cm2);
            //    DataSet dt = new DataSet();
            //    adp.Fill(dt, "STOCK_TABLE");
            //}
            //binddata();
            #endregion

            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 100, slno);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DEL_ALL");

            OBJ_METHOD.ExecuteProceedure("phrmc_ItemMstInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
            }
            message = "alert('" + OBJ_METHOD._objOut + "')";
        }
        catch (Exception ex)
        {

            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not Deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        clear_control();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, "");
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INDEXCHG");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("phrmc_ItemMstSelDel", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void dropcate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropcate.Text != "TABLET")
        {
            txttabletperstrip.Enabled = false;
            txttabletperstrip.Text = "0";
        }
        else
        {
            txttabletperstrip.Enabled = true;
        }
    }
    protected void Btnsearch_click(object sender, EventArgs e)
    {
        try
        {
            if (txtsearchname.Text == "")
            {
                string message = "alert('Search Item Name Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtsearchname.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SEARCH");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("phrmc_ItemMstSelDel", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void droppurchaseunit_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropsaleunit.SelectedItem.Text = droppurchaseunit.SelectedItem.Text;
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView(getdata());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        GridView1.DataSource = sortedView;
        GridView1.DataBind();
    }
    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();

        DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT ID as ID,NAME,CATEGORY,QTY FROM ITEM_TABLE   where Branch_ID='" + Session["Branch"].ToString() + "' order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;
    }
}