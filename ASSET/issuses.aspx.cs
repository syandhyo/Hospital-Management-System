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


public partial class ASSET_issuses : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
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
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select DISTINCT QRCODE from ASSET_MST_DTL where QRCODE like @SearchText+'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["QRCODE"]));
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
            string qry1 = "select max(ISS_NUM) as ID from ASSET_ISS_HDR";

            com = new SqlCommand(qry1, con);
            dr = null;
            dr = com.ExecuteReader();
            string str1 = "1";
            if (dr.Read() && dr["ID"].ToString() != "")
            {
                num1 = dr["ID"].ToString();
                string str = num1.Substring(0, num1.Length - 10);//delete last 10 record
                string d = str.Substring(4);//delete first 3 record
                str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
            }
            //num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
            txtContrtno.Text = "ISS-" + str1 + "-" + lblfyear.Text;
            txtrfqid.Text = "ISS-" + str1 + "-" + lblfyear.Text;
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
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
            DataSet Ds = OBJ_METHOD.Get_DataSet("select distinct DeptName,id from tblDepartment where Branch_ID = " + Session["Branch"] + "", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropvendor.DataSource = Ds;
                dropvendor.DataTextField = "DeptName";
                dropvendor.DataValueField = "id";
                dropvendor.DataBind();
                dropvendor.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            DataSet Ds4 = OBJ_METHOD.Get_DataSet("select distinct DeptName,id from tblDepartment where Branch_ID = " + Session["Branch"] + "", false, false);
            if (Ds4.Tables[0].Rows.Count > 0)
            {
                ddltodept.DataSource = Ds4;
                ddltodept.DataTextField = "DeptName";
                ddltodept.DataValueField = "id";
                ddltodept.DataBind();
                ddltodept.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select max(ISS_NUM) as indhid from ASSET_ISS_HDR where Branch_ID = " + Session["Branch"] + "", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                txtrfqid.Text = Ds1.Tables[0].Rows[0]["indhid"].ToString();
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_ISS_PAGING", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                grdrfq.DataSource = Ds2;
                grdrfq.DataKeyNames = new string[] { "ISS_NUM" };
                grdrfq.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }

    }
    public void clearfield()
    {
        try
        {
            txtdate.Text = DateTime.Now.ToString();
            txtMaterial.Text = "";
            txtUnit.Text = "";
            txtqty.Text = "";
            dropvendor.SelectedIndex = 0;
            auto();
            btncreate.Visible = true;
            btnupdate.Visible = false;
            btndelete.Visible = false;
            grdMaterial.DataSource = null;
            grdMaterial.DataBind();
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
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            dropvendor.Focus();
            if (!IsPostBack)
            {
                binddata();
                bindTGrid();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    public void bindTGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("NAME"), new DataColumn("UNIT"), new DataColumn("QTY") });
        ViewState["ITEM"] = dt;
        this.BindGrid();
    }
    protected void BindGrid()
    {
        try
        {
            grdMaterial.DataSource = (DataTable)ViewState["ITEM"];
            grdMaterial.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtMaterial.Text.Trim(), txtUnit.Text.Trim(), txtqty.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();

            con.Close();
            txtMaterial.Text = "";
            txtqty.Text = "";
            txtUnit.Text = "";
        }
        catch (Exception ex)
        {
            //Console.WriteLine("An error occurred: '{0}'", ex);
            Trace.Write(ex.Message);
        }
    }

    protected void grdMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM"];
            GridViewRow row = (GridViewRow)grdMaterial.Rows[e.RowIndex];

            Label nameit = (Label)row.FindControl("lbl_Name");
            Label unitit = (Label)row.FindControl("lbl_Unit");
            Label quantityit = (Label)row.FindControl("lbl_Qty");
            //Label sgst = (Label)row.FindControl("lbl_sgst");
            //  Label cgst = (Label)row.FindControl("lbl_cgst");
            //  Label hsncode = (Label)row.FindControl("lbl_cgst");

            string NAME = nameit.Text.ToString();
            string UNIT = unitit.Text.ToString();
            string QTY = quantityit.Text.ToString();
            // string SGST = sgst.Text.ToString();
            // string CGST = cgst.Text.ToString();
            // string HSNCODE = hsncode.Text.ToString();

            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;
            //  lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();

            this.BindGrid();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txtMaterial_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select a. ASSET_NAME ,count(a.ASSET_NAME) as QNTY,a.UOM_ID,b.UOM_Name from ASSET_MST a,UOM_MST b where a. ASSET_NAME='" + txtMaterial.Text + "'and a.UOM_ID=b.UOM_ID and a.Branch_ID=" + Session["Branch"] + "  group by a.ASSET_NAME,a.UOM_ID,b.UOM_Name   ORDER BY a.ASSET_NAME DESC ", false, false);
            //  DataSet Ds = OBJ_METHOD.Get_DataSet("select ID, UNIT,NAME,GST,HSNCODE from MATERIAL_MASTER_TABLE where NAME='" + txtMaterial.Text + "' and Branch_ID = " + Session["Branch"] + " order by id desc", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtUnit.Text = Ds.Tables[0].Rows[0]["UOM_Name"].ToString();
               
            }

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
            if (dropvendor.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Vendor..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (grdMaterial.Rows.Count <= 0)
            {

                string message = "alert('*Add item to Save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@INDH_NUM", SqlDbType.VarChar, 500, txtContrtno.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DEPT_From", SqlDbType.VarChar, 500, dropvendor.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DEPT_TO", SqlDbType.VarChar, 500, ddltodept.SelectedValue);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


            OBJ_METHOD.ExecuteProceedure("USP_INDNT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                int chkedcounter = 0;
                int correctinput = 0;
                if (OBJ_METHOD._RESULT > 0)
                {
                    foreach (GridViewRow gv1 in grdMaterial.Rows)
                    {
                        //var lbname = (gv1.FindControl("chkRow") as CheckBox);
                        var nameGr = (gv1.FindControl("lbl_Name") as Label);
                        var unitGr = (gv1.FindControl("lbl_Unit") as Label);
                        var quantyGr = (gv1.FindControl("lbl_Qty") as Label);
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[7];
                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@INDH_NUM", SqlDbType.VarChar, 500, txtContrtno.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@MATRL_NM", SqlDbType.VarChar, 500, nameGr.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unitGr.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, quantyGr.Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        OBJ_METHOD.ExecuteProceedure("USP_INDNT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                        if (OBJ_METHOD._RESULT > 0)
                        {
                            correctinput++;
                        }
                    }
                    if (chkedcounter == correctinput && chkedcounter > 0)
                    {

                        OBJ_METHOD.commitOrRollbackTran("commit");
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        binddata();
                        clearfield();
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Error occurred while processing data... Rolling back...')";
                    }
                }
            }
            #region oldcode

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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            binddata();
            clearfield();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    protected void grdrfq_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grdrfq.DataKeys[e.NewSelectedIndex].Values["RFQID"].ToString();
            DataSet Ds = OBJ_METHOD.Get_DataSet("select RFQID,CONVERT(varchar, DATE, 105) DATE,VENDORID from RFQ_TABLE where  RFQID='" + slno + "' and Branch_ID = " + Session["Branch"] + "", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btncancel.Visible = true;
                btndelete.Visible = true;
                lblEditgrd.Text = slno.ToString();
                txtdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");
                txtrfqid.Text = Ds.Tables[0].Rows[0]["RFQID"].ToString();
                dropvendor.Text = Ds.Tables[0].Rows[0]["VENDORID"].ToString();

                DataSet Ds1 = OBJ_METHOD.Get_DataSet("select ITEMNAME NAME,UNIT,QTY,SGST,CGST,HSNCODE  from RFQ_ITEM_TABLE where RFQID='" + slno + "' and Branch_ID = " + Session["Branch"] + "", false, false);
                if (Ds1.Tables[0].Rows.Count > 0)
                {
                    grdMaterial.DataSource = Ds1;
                    grdMaterial.DataBind();
                    ViewState["ITEM"] = Ds1.Tables[0];
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropvendor.SelectedItem.Text == "")
            {
                string message = "alert('* Please Select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@RFQID", SqlDbType.VarChar, 500, lblEditgrd.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VENDORID", SqlDbType.VarChar, 500, dropvendor.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);

            OBJ_METHOD.ExecuteProceedure("USP_RFQ", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                int chkedcounter = 0;
                int correctinput = 0;
                if (OBJ_METHOD._RESULT > 0)
                {
                    foreach (GridViewRow gv1 in grdMaterial.Rows)
                    {
                        //var lbname = (gv1.FindControl("chkRow") as CheckBox);
                        var nameGr = (gv1.FindControl("lbl_Name") as Label);
                        var unitGr = (gv1.FindControl("lbl_Unit") as Label);
                        var quantyGr = (gv1.FindControl("lbl_Qty") as Label);
                        var sgst = (gv1.FindControl("lbl_sgst") as Label);
                        var cgst = (gv1.FindControl("lbl_cgst") as Label);
                        var hsncode = (gv1.FindControl("lbl_cgst") as Label);
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[10];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDUPDATE");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@RFQID", SqlDbType.VarChar, 500, lblEditgrd.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, nameGr.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unitGr.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, quantyGr.Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@SGST", SqlDbType.VarChar, 500, sgst.Text);
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@CGST", SqlDbType.VarChar, 500, cgst.Text);
                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, hsncode.Text);

                        OBJ_METHOD.ExecuteProceedure("USP_RFQ", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                        if (OBJ_METHOD._RESULT > 0)
                        {
                            correctinput++;

                        }
                    }
                    if (chkedcounter == correctinput && chkedcounter > 0)
                    {

                        OBJ_METHOD.commitOrRollbackTran("commit");
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        binddata();
                        clearfield();
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Error occurred while processing data... Rolling back...')";
                    }
                }
            }


        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not updated.')";
        }
        finally
        {

            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@RFQID", SqlDbType.VarChar, 500, lblEditgrd.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            OBJ_METHOD.ExecuteProceedure("USP_RFQ", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();

            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region deletecode

            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void grdrfq_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_RFQ_PAGING", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                grdrfq.DataSource = Ds2;
                grdrfq.PageIndex = e.NewPageIndex;
                grdrfq.DataKeyNames = new string[] { "RFQID" };
                grdrfq.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}