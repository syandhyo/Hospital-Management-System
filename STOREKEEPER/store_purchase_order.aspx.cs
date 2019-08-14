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


public partial class STOREKEEPER_store_purchase_order : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9;
    SqlDataReader dr, dr1, dr2;
    DataTable dt, Dt2;
    DataRow dtr;
    int i, no, no1, sl, F;
    int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    public string id_hist;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b, c, d, e, f, g, h, j;
    decimal amount = 0;
    decimal amount1 = 0;
    decimal gstamount = 0;
    DateTime DT;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;
    DataMathods OBJ_METHOD = new DataMathods();

    [WebMethod]
    public static string[] GetCustomers(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            
            using (SqlCommand cmd = new SqlCommand("STORE_PURCHASE_ORDER", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_SEARCH";
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = prefix;
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["QUTIONID"]));
                    }
                }
                con.Close();
            }
        }
        return customers.ToArray();
    }
    public void auto()
    {
       
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select max(PONO) AS ID from PO_TABLE ";

            com = new SqlCommand(qry1, con);
            dr = null;
            dr = com.ExecuteReader();
            string str1 = "1";
            if (dr.Read() && dr["ID"].ToString() != "")
            {
                // num1 = dr["ID"].ToString();
                //------------------
                num1 = dr["ID"].ToString();
                // string str="0";
                string str = num1.Substring(0, num1.Length - 10);//delete last 10 record
                string d = str.Substring(3);//delete first 3 record
                str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
            }
            //-----------------------------------------------------
            //num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
            txtpono.Text = "PO-" + str1 + "-" + lblfyear.Text;

            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_VIEW1");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtrefno.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("STORE_PURCHASE_ORDER", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropVendor.SelectedValue = Ds1.Tables[0].Rows[0]["VENDORNAME"].ToString();
                txtstatecode.Text = Ds1.Tables[0].Rows[0]["STATECODE"].ToString();
                txtrefdate.Text = Ds1.Tables[0].Rows[0]["QUOTIONDATE"].ToString();

                SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_VIEW2");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtrefno.Text);

                DataSet Ds = OBJ_METHOD.Get_DataSet("STORE_PURCHASE_ORDER", false, true, SQL_PARAMS);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["ITEM"] = Ds.Tables[0];
                    this.BindGrid();
                }
                else
                {
                    string message = "alert('* Incorrect QUOTATION_NO.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            else
            {
                string message = "alert('* Incorrect QUOTATION_NO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            #region oldecode
            //using (SqlCommand cmd = new SqlCommand("STORE_PURCHASE_ORDER", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VIEW1";

            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtrefno.Text;
            //    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //    //SqlCommand COM1 = new SqlCommand("SELECT VENDORNAME,STATECODE,CONVERT(VARCHAR(10),QUOTIONDATE,105) AS QUOTIONDATE FROM QUOTATION_TBL where QUTIONNO='" + txtrefno.Text + "'", con);
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        dropVendor.SelectedValue = dr["VENDORNAME"].ToString();
            //        txtstatecode.Text = dr["STATECODE"].ToString();
            //        txtrefdate.Text = dr["QUOTIONDATE"].ToString();
            //        dr.Close();
            //        using (SqlCommand cmd1 = new SqlCommand("STORE_PURCHASE_ORDER", con))
            //        {
            //            cmd1.CommandType = CommandType.StoredProcedure;
            //            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VIEW2";

            //            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtrefno.Text;
            //            cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //            //SqlDataAdapter da = new SqlDataAdapter("Select A.NAME,A.HSNCODE,A.UNIT,A.QUANTITY AS QTY,A.PRICE,A.AMOUNT,A.CGST,A.SGST,A.IGST,A.GSTAMT,A.TOTAMT AS TOTALAMOUNT from QUOTATION_ITEM_TBL A,QUOTATION_TBL B where A.QUTIONID=B.QUTIONID AND B.QUTIONNO='" + txtrefno.Text + "'", con);
            //            DataTable dt = (DataTable)ViewState["ITEM"];
            //            dt.Clear();
            //            da.Fill(dt);
            //            ViewState["ITEM"] = dt;
            //            this.BindGrid();
            //        }
            //    }
            //    else
            //    {
            //        string message = "alert('* Incorrect QUOTATION_NO.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;
            //    }
            //    dr.Close();
            //}

            //con.Close();
            #endregion
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

            DataSet Ds = OBJ_METHOD.Get_DataSet("select max(PONO) as poid from PO_TABLE where Branch_ID = " + Session["Branch"] + "", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtpono.Text = Ds.Tables[0].Rows[0]["poid"].ToString();
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "BIND1");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("STORE_PURCHASE_ORDER", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropVendor.DataSource = Ds1;
                dropVendor.DataTextField = "NAME1";
                dropVendor.DataValueField = "ID";
                dropVendor.DataBind();
                dropVendor.Items.Insert(0,new ListItem("Please Select","0"));
            }
            SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "BIND2");

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("STORE_PURCHASE_ORDER", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                txtorgname.Text = Ds2.Tables[0].Rows[0]["NAME"].ToString();
                txtaddress.Text = Ds2.Tables[0].Rows[0]["ADDRESS"].ToString();
                txtgstin.Text = Ds2.Tables[0].Rows[0]["GSTNO"].ToString();
                txtpin.Text = "752055";
                txtphone.Text = Ds2.Tables[0].Rows[0]["PHONE"].ToString();
                txtcity.Text = "BHUBANESWAR";
                txtdstatecode.Text = Ds2.Tables[0].Rows[0]["STATECODE"].ToString();
                txtstate.Text = "ODISHA";
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "ViewGrid");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            DataSet Ds3 = OBJ_METHOD.Get_DataSet("STORE_PURCHASE_ORDER", false, true, SQL_PARAMS);
            if (Ds3.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds3;
                GridView2.DataBind();
            }
            #region old code
            //SqlCommand comid = new SqlCommand("select max(PONO) as poid from PO_TABLE ", con);
            //dr1 = comid.ExecuteReader();
            //if (dr1.Read())
            //{
            //    txtpono.Text = dr1["poid"].ToString();
            //}
            //dr1.Close();
            //using (SqlCommand cmd1 = new SqlCommand("STORE_PURCHASE_ORDER", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "BIND1";

            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtrefno.Text;
            //    cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //    da1 = new SqlDataAdapter(cmd1);
            //    //da1 = new SqlDataAdapter("select NAME1,ID FROM VENDER_MASTER_TABLE", con);
            //    DataTable ds1 = new DataTable();
            //    da1.Fill(ds1);
            //    dropVendor.DataSource = ds1;
            //    dropVendor.DataTextField = "NAME1";
            //    dropVendor.DataValueField = "ID";
            //    dropVendor.DataBind();
            //    dropVendor.Items.Insert(0, "Please Select");
            //}
            //using (SqlCommand cmd = new SqlCommand("STORE_PURCHASE_ORDER", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "BIND2";

            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtrefno.Text;
            //    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //    //SqlCommand COM1 = new SqlCommand("SELECT * FROM ORG_TABLE", con);
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        txtorgname.Text = dr["NAME"].ToString();
            //        txtaddress.Text = dr["ADDRESS"].ToString();
            //        txtgstin.Text = dr["GSTNO"].ToString();
            //        txtpin.Text = "752055";
            //        txtphone.Text = dr["PHONE"].ToString();
            //        txtcity.Text = "BHUBANESWAR";
            //        txtdstatecode.Text = dr["STATECODE"].ToString();
            //        txtstate.Text = "ODISHA";
            //    }
            //    dr.Close();
            //}
            //using (SqlCommand cmd2 = new SqlCommand("STORE_PURCHASE_ORDER", con))
            //{
            //    cmd2.CommandType = CommandType.StoredProcedure;
            //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "ViewGrid";
            //    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter Adp1 = new SqlDataAdapter(cmd2);
            //    Dt2 = new DataTable();
            //    Adp1.Fill(Dt2);
            //    GridView2.DataSource = Dt2;
            //    GridView2.DataBind();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GVOnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lbl_slno") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    public void clearinercontrol()
    {
        try
        {
            auto();
            binddata();
            txtstatecode.Text = "";
            txtrefdate.Text = "";
            txtrefno.Text = "";
            txtdateofissue.Text = DateTime.Now.ToString("dd-MM-YYYY");
            lblEditgrd.Text = "";
            lblgrandtotal.Text = "0";
            lblgstamt.Text = "0";
            lbltotalprice.Text = "0";
            grvItemtDetails.DataSource = null;
            grvItemtDetails.DataBind();
            btncreate.Visible = true;
            btnupdate.Visible = false;
            btndelete.Visible = false;
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
            txtdateofissue.Text = DateTime.Now.ToString("dd-MM-yyyy");
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("HSN"), new DataColumn("NAME"), new DataColumn("UNIT"), new DataColumn("PRICE"), new DataColumn("QTY"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
                ViewState["ITEM"] = dt;
                this.BindGrid();

                binddata();
            }
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
            grvItemtDetails.DataSource = (DataTable)ViewState["ITEM"];
            grvItemtDetails.DataBind();

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void dropVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DROP_EVENT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar,500 , dropVendor.SelectedValue);
            DataSet Ds3 = OBJ_METHOD.Get_DataSet("STORE_PURCHASE_ORDER", false, true, SQL_PARAMS);
            if (Ds3.Tables[0].Rows.Count > 0)
            {
                txtstatecode.Text = Ds3.Tables[0].Rows[0]["STATECODE"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
   
    protected void Btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropVendor.SelectedIndex == 0)
            {
                string message = "alert('* Vendors are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;

            }
            else if (txtrefno.Text == "")
            {
                string message = "alert('* Ref. No. are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstatecode.Text == "")
            {
                string message = "alert('* State Code  are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtdateofissue.Text == "")
            {
                string message = "alert('*Date Of Issue  are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtrefdate.Text == "")
            {
                string message = "alert('* Ref. Date are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtorgname.Text == "")
            {
                string message = "alert('* Organisation Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtphone.Text == "")
            {
                string message = "alert('* Phone Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('* Address are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtdstatecode.Text == "")
            {
                string message = "alert('* State Code are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtpin.Text == "")
            {
                string message = "alert('* Pin are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtcity.Text == "")
            {
                string message = "alert('* City are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstate.Text == "")
            {
                string message = "alert('* State are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtgstin.Text == "")
            {
                string message = "alert('* GST In are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[36];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VENDOR", SqlDbType.VarChar, 500, dropVendor.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DATEOFISSUE", SqlDbType.Date, 0, Convert.ToDateTime(txtdateofissue.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@PONO", SqlDbType.VarChar, 500, txtpono.Text);

            SQL_PARAMS[7] = OBJ_METHOD.createParams("@REFNO", SqlDbType.VarChar, 500, txtrefno.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@REFDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtrefdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@ORGNAME", SqlDbType.VarChar, 500, txtorgname.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@PIN", SqlDbType.VarChar, 500, txtpin.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@CITY", SqlDbType.VarChar, 500, txtcity.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@GSTIN", SqlDbType.VarChar, 500, txtgstin.Text);

            SQL_PARAMS[14] = OBJ_METHOD.createParams("@OSTATECODE", SqlDbType.VarChar, 500, txtdstatecode.Text);
            SQL_PARAMS[15] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@TOTALPRICE", SqlDbType.VarChar, 500, lbltotalprice.Text);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@GSTAMOUNT", SqlDbType.VarChar, 500, lblgstamt.Text);
            SQL_PARAMS[18] = OBJ_METHOD.createParams("@GRANDTOTAL", SqlDbType.VarChar, 500, lblgrandtotal.Text);
            SQL_PARAMS[19] = OBJ_METHOD.createParams("@TC1", SqlDbType.VarChar, 500, txt_term_condition.Text);
            SQL_PARAMS[20] = OBJ_METHOD.createParams("@TC2", SqlDbType.VarChar, 500, txt_TEST_REPORT0.Text);

            SQL_PARAMS[21] = OBJ_METHOD.createParams("@TC3", SqlDbType.VarChar, 500, txt_DELIVERY_TERM.Text);
            SQL_PARAMS[22] = OBJ_METHOD.createParams("@TC4", SqlDbType.VarChar, 500, txt_INSPECTION0.Text);
            SQL_PARAMS[23] = OBJ_METHOD.createParams("@TC5", SqlDbType.VarChar, 500, txt_DELIVERY_TIME.Text);
            SQL_PARAMS[24] = OBJ_METHOD.createParams("@TC6", SqlDbType.VarChar, 500, txt_WARANTY0.Text);
            SQL_PARAMS[25] = OBJ_METHOD.createParams("@TC7", SqlDbType.VarChar, 500, txt_FREIGHT.Text);
            SQL_PARAMS[26] = OBJ_METHOD.createParams("@TC8", SqlDbType.VarChar, 500, txt_LICENCE_PERMIT0.Text);
            SQL_PARAMS[27] = OBJ_METHOD.createParams("@TC9", SqlDbType.VarChar, 500, txt_make.Text);

            SQL_PARAMS[28] = OBJ_METHOD.createParams("@TC10", SqlDbType.VarChar, 500, txt_PRICE_PAYMENT.Text);
            SQL_PARAMS[29] = OBJ_METHOD.createParams("@TC11", SqlDbType.VarChar, 500, txt_PACKING_FORWARDING.Text);
            SQL_PARAMS[30] = OBJ_METHOD.createParams("@TC12", SqlDbType.VarChar, 500, txt_ACCEPTANCE0.Text);
            SQL_PARAMS[31] = OBJ_METHOD.createParams("@TC13", SqlDbType.VarChar, 500, txt_TRANSIT_INSURANCE.Text);
            SQL_PARAMS[32] = OBJ_METHOD.createParams("@TC14", SqlDbType.VarChar, 500, txt_TERMINATION0.Text);
            SQL_PARAMS[33] = OBJ_METHOD.createParams("@TC15", SqlDbType.VarChar, 500, txt_LOADING_UNLOADING.Text);
            SQL_PARAMS[34] = OBJ_METHOD.createParams("@TC16", SqlDbType.VarChar, 500, txt_INSTALLATION_COMMI1.Text);
            SQL_PARAMS[35] = OBJ_METHOD.createParams("@VSCODE", SqlDbType.VarChar, 500, txtstatecode.Text);
            OBJ_METHOD.ExecuteProceedure("PO_OPERATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                int chkedcounter = 0;
                int correctinput = 0;
                if (OBJ_METHOD._RESULT > 0)
                {
                    foreach (GridViewRow row in grvItemtDetails.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                            
                            var item = row.FindControl("lbl_name") as Label;
                            var hsncode = row.FindControl("lbl_hsn") as Label;
                            var unit = row.FindControl("lbl_unit") as Label;
                            var qty = row.FindControl("txt_qty") as TextBox;
                            var price = row.FindControl("lbl_price") as Label;
                            var amount = row.FindControl("lbl_amount") as Label;
                            var cgst = row.FindControl("lbl_cgst") as Label;
                            var sgst = row.FindControl("lbl_sgst") as Label;
                            var igst = row.FindControl("lbl_igst") as Label;
                            var gstamt = row.FindControl("lbl_gstamt") as Label;
                            var totalamt = row.FindControl("lbl_totalamount") as Label;
                            if (chkRow.Checked)
                            {
                                chkedcounter++;
                                SQL_PARAMS = new SqlParameter[16];

                                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                                SQL_PARAMS[1] = OBJ_METHOD.createParams("@PONO", SqlDbType.VarChar, 500, txtpono.Text);
                                SQL_PARAMS[2] = OBJ_METHOD.createParams("@HSN", SqlDbType.VarChar, 500, hsncode.Text);
                                SQL_PARAMS[3] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, item.Text);
                                SQL_PARAMS[4] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unit.Text);
                                SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                                SQL_PARAMS[7] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, price.Text);
                                SQL_PARAMS[8] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, qty.Text);
                                SQL_PARAMS[9] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.VarChar, 500, amount.Text);
                                SQL_PARAMS[10] = OBJ_METHOD.createParams("@CGST", SqlDbType.VarChar, 500, cgst.Text);
                                SQL_PARAMS[11] = OBJ_METHOD.createParams("@SGST", SqlDbType.VarChar, 500, sgst.Text);

                                SQL_PARAMS[12] = OBJ_METHOD.createParams("@IGST", SqlDbType.VarChar, 500, igst.Text);
                                SQL_PARAMS[13] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.VarChar, 500, gstamt.Text);
                                SQL_PARAMS[14] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.VarChar, 500, totalamt.Text);
                                SQL_PARAMS[15] = OBJ_METHOD.createParams("@ischecked", SqlDbType.Bit, 500, chkRow.Checked);
                                OBJ_METHOD.ExecuteProceedure("PO_OPERATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                                if (OBJ_METHOD._RESULT > 0)
                                {
                                    correctinput++;

                                }
                            }
                        }
                    }
                    if (chkedcounter == correctinput && chkedcounter > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        binddata();
                        clearinercontrol();
                    }
                    else if (chkedcounter == 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('No row is selected.')";
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Error occurred while processing data... Rolling back...')";
                    }
                }
            }
            #region insert code
            //using (SqlCommand stock_cmd = new SqlCommand("PO_OPERATION", con))
            //{
            //    stock_cmd.CommandType = CommandType.StoredProcedure;
            //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

            //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            //    stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //    stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            //    stock_cmd.Parameters.Add("@DATEOFISSUE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateofissue.Text).ToString("yyyy-MM-dd");
            //    stock_cmd.Parameters.Add("@REFNO", SqlDbType.VarChar).Value = txtrefno.Text;
            //    stock_cmd.Parameters.Add("@REFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtrefdate.Text).ToString("yyyy-MM-dd");
            //    stock_cmd.Parameters.Add("@ORGNAME", SqlDbType.VarChar).Value = txtorgname.Text;
            //    stock_cmd.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
            //    stock_cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
            //    stock_cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
            //    stock_cmd.Parameters.Add("@GSTIN", SqlDbType.VarChar).Value = txtgstin.Text;
            //    stock_cmd.Parameters.Add("@OSTATECODE", SqlDbType.VarChar).Value = txtdstatecode.Text;
            //    stock_cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
            //    stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            //    stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            //    stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            //    stock_cmd.Parameters.Add("@TC1", SqlDbType.VarChar).Value = txt_term_condition.Text;
            //    stock_cmd.Parameters.Add("@TC2", SqlDbType.VarChar).Value = txt_TEST_REPORT0.Text;
            //    stock_cmd.Parameters.Add("@TC3", SqlDbType.VarChar).Value = txt_DELIVERY_TERM.Text;
            //    stock_cmd.Parameters.Add("@TC4", SqlDbType.VarChar).Value = txt_INSPECTION0.Text;
            //    stock_cmd.Parameters.Add("@TC5", SqlDbType.VarChar).Value = txt_DELIVERY_TIME.Text;
            //    stock_cmd.Parameters.Add("@TC6", SqlDbType.VarChar).Value = txt_WARANTY0.Text;
            //    stock_cmd.Parameters.Add("@TC7", SqlDbType.VarChar).Value = txt_FREIGHT.Text;
            //    stock_cmd.Parameters.Add("@TC8", SqlDbType.VarChar).Value = txt_LICENCE_PERMIT0.Text;
            //    stock_cmd.Parameters.Add("@TC9", SqlDbType.VarChar).Value = txt_make.Text;
            //    stock_cmd.Parameters.Add("@TC10", SqlDbType.VarChar).Value = txt_PRICE_PAYMENT.Text;
            //    stock_cmd.Parameters.Add("@TC11", SqlDbType.VarChar).Value = txt_PACKING_FORWARDING.Text;
            //    stock_cmd.Parameters.Add("@TC12", SqlDbType.VarChar).Value = txt_ACCEPTANCE0.Text;
            //    stock_cmd.Parameters.Add("@TC13", SqlDbType.VarChar).Value = txt_TRANSIT_INSURANCE.Text;
            //    stock_cmd.Parameters.Add("@TC14", SqlDbType.VarChar).Value = txt_TERMINATION0.Text;
            //    stock_cmd.Parameters.Add("@TC15", SqlDbType.VarChar).Value = txt_LOADING_UNLOADING.Text;
            //    stock_cmd.Parameters.Add("@TC16", SqlDbType.VarChar).Value = txt_INSTALLATION_COMMI1.Text;
            //    stock_cmd.Parameters.Add("@HSN", SqlDbType.VarChar).Value = "";
            //    stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = txt_term_condition.Text;
            //    stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = txt_TEST_REPORT0.Text;
            //    stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txt_DELIVERY_TERM.Text;
            //    stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = txt_INSPECTION0.Text;
            //    stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = txt_DELIVERY_TIME.Text;
            //    stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = txt_WARANTY0.Text;
            //    stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = txt_FREIGHT.Text;
            //    stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = txt_LICENCE_PERMIT0.Text;
            //    stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = txt_make.Text;
            //    stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = txt_PRICE_PAYMENT.Text;
            //    stock_cmd.Parameters.Add("@ischecked", SqlDbType.Bit).Value = "true";
            //    stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = "0.00";
            //    //stock_cmd.ExecuteNonQuery();
            //    int i = stock_cmd.ExecuteNonQuery();

            //    if (i > 0)
            //    {
            //        // string message = "alert('Data has saved successfully.')";
            //        Response.Write("<script>alert('Data successfully saved');</script>");
            //        //return;
            //    }
            //}
            //foreach (GridViewRow row in grvItemtDetails.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

            //        //if (chkRow.Checked)
            //        //{
            //        var item = row.FindControl("lbl_name") as Label;
            //        var hsncode = row.FindControl("lbl_hsn") as Label;
            //        var unit = row.FindControl("lbl_unit") as Label;
            //        var qty = row.FindControl("txt_qty") as TextBox;
            //        var price = row.FindControl("lbl_price") as Label;
            //        var amount = row.FindControl("lbl_amount") as Label;
            //        var cgst = row.FindControl("lbl_cgst") as Label;
            //        var sgst = row.FindControl("lbl_sgst") as Label;
            //        var igst = row.FindControl("lbl_igst") as Label;
            //        var gstamt = row.FindControl("lbl_gstamt") as Label;
            //        var totalamt = row.FindControl("lbl_totalamount") as Label;
            //        using (SqlCommand stock_cmd = new SqlCommand("PO_OPERATION", con))
            //        {
            //            stock_cmd.CommandType = CommandType.StoredProcedure;
            //            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
            //            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //            stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            //            stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //            stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            //            stock_cmd.Parameters.Add("@DATEOFISSUE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateofissue.Text).ToString("yyyy-MM-dd");
            //            stock_cmd.Parameters.Add("@REFNO", SqlDbType.VarChar).Value = txtrefno.Text;
            //            stock_cmd.Parameters.Add("@REFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtrefdate.Text).ToString("yyyy-MM-dd");
            //            stock_cmd.Parameters.Add("@ORGNAME", SqlDbType.VarChar).Value = txtorgname.Text;
            //            stock_cmd.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
            //            stock_cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
            //            stock_cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
            //            stock_cmd.Parameters.Add("@GSTIN", SqlDbType.VarChar).Value = txtgstin.Text;
            //            stock_cmd.Parameters.Add("@OSTATECODE", SqlDbType.VarChar).Value = txtdstatecode.Text;
            //            stock_cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
            //            stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            //            stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            //            stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            //            stock_cmd.Parameters.Add("@TC1", SqlDbType.VarChar).Value = txt_term_condition.Text;
            //            stock_cmd.Parameters.Add("@TC2", SqlDbType.VarChar).Value = txt_TEST_REPORT0.Text;
            //            stock_cmd.Parameters.Add("@TC3", SqlDbType.VarChar).Value = txt_DELIVERY_TERM.Text;
            //            stock_cmd.Parameters.Add("@TC4", SqlDbType.VarChar).Value = txt_INSPECTION0.Text;
            //            stock_cmd.Parameters.Add("@TC5", SqlDbType.VarChar).Value = txt_DELIVERY_TIME.Text;
            //            stock_cmd.Parameters.Add("@TC6", SqlDbType.VarChar).Value = txt_WARANTY0.Text;
            //            stock_cmd.Parameters.Add("@TC7", SqlDbType.VarChar).Value = txt_FREIGHT.Text;
            //            stock_cmd.Parameters.Add("@TC8", SqlDbType.VarChar).Value = txt_LICENCE_PERMIT0.Text;
            //            stock_cmd.Parameters.Add("@TC9", SqlDbType.VarChar).Value = txt_make.Text;
            //            stock_cmd.Parameters.Add("@TC10", SqlDbType.VarChar).Value = txt_PRICE_PAYMENT.Text;
            //            stock_cmd.Parameters.Add("@TC11", SqlDbType.VarChar).Value = txt_PACKING_FORWARDING.Text;
            //            stock_cmd.Parameters.Add("@TC12", SqlDbType.VarChar).Value = txt_ACCEPTANCE0.Text;
            //            stock_cmd.Parameters.Add("@TC13", SqlDbType.VarChar).Value = txt_TRANSIT_INSURANCE.Text;
            //            stock_cmd.Parameters.Add("@TC14", SqlDbType.VarChar).Value = txt_TERMINATION0.Text;
            //            stock_cmd.Parameters.Add("@TC15", SqlDbType.VarChar).Value = txt_LOADING_UNLOADING.Text;
            //            stock_cmd.Parameters.Add("@TC16", SqlDbType.VarChar).Value = txt_INSTALLATION_COMMI1.Text;
            //            stock_cmd.Parameters.Add("@HSN", SqlDbType.VarChar).Value = hsncode.Text;
            //            stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = item.Text;

            //            stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text;
            //            stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text;
            //            stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = qty.Text;
            //            stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = "0.00";
            //            stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = amount.Text;
            //            stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = cgst.Text;
            //            stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = sgst.Text;
            //            stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = igst.Text;
            //            stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gstamt.Text;
            //            stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = totalamt.Text;
            //            stock_cmd.Parameters.Add("@ischecked", SqlDbType.Bit).Value = chkRow.Checked;
            //            stock_cmd.ExecuteNonQuery();
            //        }
            //    }
            //}
            //string message1 = "alert('*Save Successfully')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

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
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }

    protected void Btndelete_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearinercontrol();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grvItemtDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                    if (chkRow.Checked)
                    {
                        var item = row.FindControl("lbl_name") as Label;
                        var qty = row.FindControl("txt_qty") as TextBox;
                        var price = row.FindControl("lbl_price") as Label;
                        var amount = row.FindControl("lbl_amount") as Label;
                        var cgst = row.FindControl("lbl_cgst") as Label;
                        var sgst = row.FindControl("lbl_sgst") as Label;
                        var igst = row.FindControl("lbl_igst") as Label;
                        var gstamt = row.FindControl("lbl_gstamt") as Label;
                        var totalamt = row.FindControl("lbl_totalamount") as Label;
                        string CQTY = "0";
                        string QTY = "0";
                        SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DROP1_TEXT_2EVENT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, item.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@SearchText", SqlDbType.VarChar, 500, dropVendor.SelectedValue);
                        DataSet Ds = OBJ_METHOD.Get_DataSet("STORE_PURCHASE_ORDER", false, true, SQL_PARAMS);
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            CQTY = Ds.Tables[0].Rows[0]["QTY"].ToString();
                        }
                        SQL_PARAMS = new SqlParameter[4];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DROP2_TEXT_EVENT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, item.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@SearchText", SqlDbType.VarChar, 500, dropVendor.SelectedValue);
                        DataSet Ds1 = OBJ_METHOD.Get_DataSet("STORE_PURCHASE_ORDER", false, true, SQL_PARAMS);
                        if (Ds1.Tables[0].Rows.Count > 0)
                        {
                            QTY = Ds1.Tables[0].Rows[0]["QTY"].ToString();
                        }
                        string mqty = (Convert.ToDouble(CQTY) - Convert.ToDouble(QTY)).ToString();
                        //if ((Convert.ToDouble(qty.Text) > Convert.ToDouble(mqty)))
                        //{
                        //    qty.Text = "0.00";
                        //    string message = "alert('*You can not add more than-" + mqty + " no.of quantity.')";
                        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        //    return;
                        //}
                        #region oldcode
                        //using (SqlCommand cmd = new SqlCommand("STORE_PURCHASE_ORDER", con))
                        //{
                        //    cmd.CommandType = CommandType.StoredProcedure;
                        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DROP2_TEXT_EVENT";

                        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = item.Text;
                        //    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                        //    //SqlCommand COM1 = new SqlCommand("SELECT ISNULL(SUM(B.QTY),0) AS QTY FROM PO_TABLE A,PO_ITEM_TABLE B WHERE A.VENDOR='" + dropVendor.SelectedValue + "' AND A.PONO=B.ID AND B.ITEMNAME='" + item.Text + "'", con);
                        //    dr = cmd.ExecuteReader();
                        //    QTY = "0";
                        //    if (dr.Read())
                        //    {
                        //        QTY = dr["QTY"].ToString();
                        //    }
                        //    dr.Close();
                        //}
                        //string mqty = (Convert.ToDouble(CQTY) - Convert.ToDouble(QTY)).ToString();
                        //if ((Convert.ToDouble(qty.Text) > Convert.ToDouble(mqty)))
                        //{
                        //    qty.Text = "0.00";
                        //    string message = "alert('*You can not add more than-" + mqty + " no.of quantity.')";
                        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        //    return;
                        //}
                        #endregion
                        try
                        {
                            amount.Text = Math.Round((Convert.ToDouble(qty.Text)) * Convert.ToDouble(price.Text)).ToString();
                            if (txtstatecode.Text == "21")
                            {
                                gstamt.Text = Math.Round(((Convert.ToDouble(amount.Text) / 100) * (Convert.ToDouble(cgst.Text))) * 2).ToString();
                                sgst.Text = cgst.Text;
                            }
                            else
                            {
                                gstamt.Text = Math.Round((Convert.ToDouble(amount.Text) / 100) * (Convert.ToDouble(igst.Text))).ToString();
                            }

                            // gstamIt.Text = Math.Round((Convert.ToDouble(amtIt.Text) / 100) * (Convert.ToDouble(sgstIt.Text))).ToString();
                            totalamt.Text = Math.Round(Convert.ToDouble(amount.Text) + Convert.ToDouble(gstamt.Text)).ToString();
                            // totamtIt.Text = Math.Round(( ((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) ) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: '{0}'", ex);
                        }


                        amount1 = amount1 + Convert.ToDecimal(totalamt.Text);
                        gstamount = gstamount + Convert.ToDecimal(gstamt.Text);
                    }

                }

                lblgrandtotal.Text = amount1.ToString();
                lblgstamt.Text = gstamount.ToString();
                lbltotalprice.Text = (Convert.ToDouble(lblgrandtotal.Text) - Convert.ToDouble(lblgstamt.Text)).ToString();

            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void txt_price_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grvItemtDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                    if (chkRow.Checked)
                    {
                        var item = row.FindControl("lbl_name") as Label;
                        var qty = row.FindControl("txt_qty") as TextBox;
                        var price = row.FindControl("lbl_price") as Label;
                        var amount = row.FindControl("lbl_amount") as Label;
                        var cgst = row.FindControl("lbl_cgst") as Label;
                        var sgst = row.FindControl("lbl_sgst") as Label;
                        var igst = row.FindControl("lbl_igst") as Label;
                        var gstamt = row.FindControl("lbl_gstamt") as Label;
                        var totalamt = row.FindControl("lbl_totalamount") as Label;
                        string CQTY = "0";
                        string QTY = "0";
                        SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DROP1_TEXT_2EVENT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, item.Text);
                        DataSet Ds = OBJ_METHOD.Get_DataSet("STORE_PURCHASE_ORDER", false, true, SQL_PARAMS);
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            CQTY = Ds.Tables[0].Rows[0]["QTY"].ToString();
                        }
                        SQL_PARAMS = new SqlParameter[4];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DROP2_TEXT_EVENT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, item.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@SearchText", SqlDbType.VarChar, 500, dropVendor.SelectedValue);
                        DataSet Ds1 = OBJ_METHOD.Get_DataSet("STORE_PURCHASE_ORDER", false, true, SQL_PARAMS);
                        if (Ds1.Tables[0].Rows.Count > 0)
                        {
                            QTY = Ds1.Tables[0].Rows[0]["QTY"].ToString();
                        }
                        string mqty = (Convert.ToDouble(CQTY) - Convert.ToDouble(QTY)).ToString();
                        //if ((Convert.ToDouble(qty.Text) > Convert.ToDouble(mqty)))
                        //{
                        //    qty.Text = "0.00";
                        //    string message = "alert('*You can not add more than-" + mqty + " no.of quantity.')";
                        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        //    return;
                        //}
                        #region oldecode
                        //using (SqlCommand cm = new SqlCommand("STORE_PURCHASE_ORDER", con))
                        //{
                        //    cm.CommandType = CommandType.StoredProcedure;
                        //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DROP1_TEXT_2EVENT";

                        //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = item.Text;
                        //    cm.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                        //    //SqlCommand COM = new SqlCommand("SELECT ISNULL(B.QTY,0)as QTY FROM VENDOR_COTRACT_TBL A,VENDOR_COTRACT_ITEM_TBL B WHERE VENDERNAME='" + dropVendor.SelectedValue + "' AND A.CONTRACTID=B.CONTRACTID AND B.ITEM='" + item.Text + "'", con);
                        //    dr = cm.ExecuteReader();
                        //    CQTY = "0";
                        //    if (dr.Read())
                        //    {
                        //        CQTY = dr["QTY"].ToString();
                        //    }
                        //}
                        //dr.Close();
                        //using (SqlCommand cmd = new SqlCommand("STORE_PURCHASE_ORDER", con))
                        //{
                        //    cmd.CommandType = CommandType.StoredProcedure;
                        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DROP2_TEXT_EVENT";

                        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = item.Text;
                        //    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                        //    //SqlCommand COM1 = new SqlCommand("SELECT ISNULL(SUM(B.QTY),0) AS QTY FROM PO_TABLE A,PO_ITEM_TABLE B WHERE A.VENDOR='" + dropVendor.SelectedValue + "' AND A.PONO=B.ID AND B.ITEMNAME='" + item.Text + "'", con);
                        //    dr = cmd.ExecuteReader();
                        //    QTY = "0";
                        //    if (dr.Read())
                        //    {
                        //        QTY = dr["QTY"].ToString();
                        //    }
                        //}
                        //dr.Close();
                        //string mqty = (Convert.ToDouble(CQTY) - Convert.ToDouble(QTY)).ToString();
                        //if ((Convert.ToDouble(qty.Text) > Convert.ToDouble(mqty)))
                        //{
                        //    qty.Text = "0.00";
                        //    string message = "alert('*You can not add more than-" + mqty + " no.of quantity.')";
                        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        //    return;
                        //}
                        #endregion

                        try
                        {
                            amount.Text = Math.Round((Convert.ToDouble(qty.Text)) * Convert.ToDouble(price.Text)).ToString();
                            if (txtstatecode.Text == "21")
                            {
                                gstamt.Text = Math.Round(((Convert.ToDouble(amount.Text) / 100) * (Convert.ToDouble(cgst.Text))) * 2).ToString();
                                sgst.Text = cgst.Text;
                            }
                            else
                            {
                                gstamt.Text = Math.Round((Convert.ToDouble(amount.Text) / 100) * (Convert.ToDouble(igst.Text))).ToString();
                            }

                            // gstamIt.Text = Math.Round((Convert.ToDouble(amtIt.Text) / 100) * (Convert.ToDouble(sgstIt.Text))).ToString();
                            totalamt.Text = Math.Round(Convert.ToDouble(amount.Text) + Convert.ToDouble(gstamt.Text)).ToString();
                            // totamtIt.Text = Math.Round(( ((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) ) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: '{0}'", ex);
                        }


                        amount1 = amount1 + Convert.ToDecimal(totalamt.Text);
                        gstamount = gstamount + Convert.ToDecimal(gstamt.Text);
                    }

                }

                lblgrandtotal.Text = amount1.ToString();
                lblgstamt.Text = gstamount.ToString();
                lbltotalprice.Text = (Convert.ToDouble(lblgrandtotal.Text) - Convert.ToDouble(lblgstamt.Text)).ToString();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["PONO"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select VENDOR,CONVERT(varchar,DATEOFISSUE, 105) as DATEOFISSUE,PONO,CONVERT(varchar,REFDATE, 105) as REFDATE,VSCODE,REFNO from PO_TABLE where  PONO='" + slno + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btncancel.Visible = true;
                btndelete.Visible = true;
                lblEditgrd.Text = slno.ToString();

                dropVendor.Text = dr["VENDOR"].ToString();
                txtstatecode.Text = dr["VSCODE"].ToString();
                txtpono.Text = dr["PONO"].ToString();
                txtdateofissue.Text = dr["DATEOFISSUE"].ToString();
                txtrefno.Text = dr["REFNO"].ToString();
                txtrefdate.Text = dr["REFDATE"].ToString();

                dr.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select * from PO_ITEM_TABLE where  ID='" + slno + "'", con);
                da.Fill(dt);
                grvItemtDetails.DataSource = dt;
                grvItemtDetails.DataBind();
                ViewState["ITEM"] = dt;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {

    }
}