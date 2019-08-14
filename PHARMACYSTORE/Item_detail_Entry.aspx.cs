using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PHARMACYSTORE_Item_detail_Entry : System.Web.UI.Page
{
    string num1 = "SJ000";

    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    int i, no, no1, sl, j;
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
        // lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
            Session["SortedView"] = null;
            binddata();
            bindgridview();
        }
    }
    public void binddata()
    {
        try
        {
            DataSet ds3 = OBJ_METHOD.Get_DataSet("select SELF_DTL_ID,RACK_NO FROM SELF_DTL where STORE_ID=" + Session["STOREID"] + " and Branch_ID='" + Session["Branch"].ToString() + "'", false, false);
            dropcell.DataSource = ds3;
            dropcell.DataTextField = "RACK_NO";
            dropcell.DataValueField = "SELF_DTL_ID";
            dropcell.DataBind();
            dropcell.Items.Insert(0, new ListItem("Please Select", "0"));

            DataSet ds4 = OBJ_METHOD.Get_DataSet("select slno,NAME FROM ITEM_TABLE", false, false);
            dropitem.DataSource = ds4;
            dropitem.DataTextField = "NAME";
            dropitem.DataValueField = "slno";
            dropitem.DataBind();
            dropitem.Items.Insert(0, new ListItem("Please Select", "0"));

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
    public void bindgridview()
    {
        try
        {
            DataSet DS = OBJ_METHOD.Get_DataSet("select B.LOC_ID,A.RACK_NO,B.QUANTITY,C.NAME from SELF_DTL as A ,ITEM_LOCATION as B,ITEM_TABLE as C where A.SELF_DTL_ID =B.CELL_ID and c.slno = B.ITEM_ID and B.STATUS = 'ACTIVE' and A.STORE_ID=" + Session["STOREID"].ToString() + " and A.Branch_ID='" + Session["Branch"].ToString() + "'", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView2.SelectedIndex = 0;
                GridView2.DataSource = DS;
                GridView2.DataKeyNames = new string[] { "LOC_ID" };
                GridView2.DataBind();
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
            if (dropitem.Text == "")
            {
                string message = "alert('* Select Item Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropitem.Focus();
                return;
            }
            if (dropbatch.Text == "")
            {
                string message = "alert('* Select Batch No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropbatch.Focus();
                return;
            }
            if (dropexp.Text == "")
            {
                string message = "alert('* Select expiry date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropexp.Focus();
                return;
            }


            SqlParameter[] SQL_PARAMS = new SqlParameter[8];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ITEM_ID", SqlDbType.Int, 0, dropexp.SelectedValue);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@QUANTITY", SqlDbType.Decimal, 0, txtqty.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@BRANCH_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@CELL_ID", SqlDbType.Int, 0, dropcell.SelectedValue);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@CELL_NAME", SqlDbType.VarChar, 50, dropcell.SelectedItem.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@STORE_ID", SqlDbType.Int, 0, Session["STOREID"]);
           
            OBJ_METHOD.ExecuteProceedure("ITEM_LOC_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                
            }
            
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
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropitem.Text == "")
            {
                string message = "alert('* Select Item Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropitem.Focus();
                return;
            }
            if (dropbatch.Text == "")
            {
                string message = "alert('* Select Batch No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropbatch.Focus();
                return;
            }
            if (dropexp.Text == "")
            {
                string message = "alert('* Select expiry date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropexp.Focus();
                return;
            }


            SqlParameter[] SQL_PARAMS = new SqlParameter[6];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ITEM_ID", SqlDbType.Int, 0, dropexp.SelectedValue);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@QUANTITY", SqlDbType.Decimal, 0, txtqty.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@LOC_ID", SqlDbType.Int, 0, TXTID.Text);

            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@CELL_ID", SqlDbType.Int, 0, dropcell.SelectedValue);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@CELL_NAME", SqlDbType.VarChar, 50, dropcell.SelectedItem.Text);

            OBJ_METHOD.ExecuteProceedure("ITEM_LOC_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";

            }

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
        Response.Redirect("~/Item_detail_Entry.aspx");
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["LOC_ID"].ToString();
            TXTID.Text = slno;
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@LOC_ID", SqlDbType.Int, 0, slno);


            DataSet Ds = OBJ_METHOD.Get_DataSet("loc_edit", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                dropcell.SelectedValue = Ds.Tables[0].Rows[0]["CELL_ID"].ToString();
                txtqty.Text = Ds.Tables[0].Rows[0]["QUANTITY"].ToString();
                dropitem.SelectedValue = Ds.Tables[0].Rows[0]["ITEM_ID"].ToString();
                DataSet ds4 = OBJ_METHOD.Get_DataSet("select slno,BATCHNO,HSNCODE FROM ITEM_TABLE where NAME='" + dropitem.SelectedItem.Text + "'", false, false);
                dropbatch.DataSource = ds4;
                dropbatch.DataTextField = "BATCHNO";
                dropbatch.DataValueField = "slno";
                dropbatch.DataBind();
                dropbatch.Items.Insert(0, new ListItem("Please Select", "0"));
                dropbatch.SelectedValue = Ds.Tables[0].Rows[0]["ITEM_ID"].ToString();
                txthsncode.Text = Ds.Tables[0].Rows[0]["HSNCODE"].ToString();
                DataSet ds6 = OBJ_METHOD.Get_DataSet("select slno,EXPDATE FROM ITEM_TABLE where BATCHNO='" + dropbatch.SelectedItem.Text + "' ", false, false);
                dropexp.DataSource = ds6;
                dropexp.DataTextField = "EXPDATE";
                dropexp.DataValueField = "slno";
                dropexp.DataBind();
                dropexp.Items.Insert(0, new ListItem("Please Select", "0"));
                dropexp.SelectedValue = Ds.Tables[0].Rows[0]["ITEM_ID"].ToString();
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            string str = "select B.LOC_ID, A.RACK_NO,B.QUANTITY,C.NAME from SELF_DTL as A ,ITEM_LOCATION as B,ITEM_TABLE as C where A.SELF_DTL_ID =B.CELL_ID and B.STATUS = 'ACTIVE' and c.slno = B.ITEM_ID and A.STORE_ID=" + Session["STOREID"].ToString() + " and A.Branch_ID='" + Session["Branch"].ToString() + "'";
            DataSet ds = OBJ_METHOD.Get_DataSet(str);

            DataTable dt = new DataTable();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }

            GridView2.DataSource = dt;
            GridView2.DataKeyNames = new string[] { "LOC_ID" };
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataBind();
            if (Session["SortedView"] != null)
            {

                GridView2.DataSource = Session["SortedView"];
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
     
    protected void dropitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            DataSet ds4 = OBJ_METHOD.Get_DataSet("select slno,BATCHNO,HSNCODE FROM ITEM_TABLE where NAME='" + dropitem.SelectedItem.Text + "' and Branch_ID='" + Session["Branch"] + "'", false, false);
            dropbatch.DataSource = ds4;
            dropbatch.DataTextField = "BATCHNO";
            dropbatch.DataValueField = "slno";
            dropbatch.DataBind();
            dropbatch.Items.Insert(0, new ListItem("Please Select", "0"));
            txthsncode.Text = ds4.Tables[0].Rows[0]["HSNCODE"].ToString();
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            DataSet ds4 = OBJ_METHOD.Get_DataSet("select slno,EXPDATE FROM ITEM_TABLE where BATCHNO='" + dropbatch.SelectedItem.Text + "' and Branch_ID='" + Session["Branch"] + "'", false, false);
            dropexp.DataSource = ds4;
            dropexp.DataTextField = "EXPDATE";
            dropexp.DataValueField = "slno";
            dropexp.DataBind();
            dropexp.Items.Insert(0, new ListItem("Please Select", "0"));


        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            string slno = GridView2.DataKeys[e.RowIndex].Values["LOC_ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@LOC_ID", SqlDbType.Int, 0, slno);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

            OBJ_METHOD.ExecuteProceedure("ITEM_LOC_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
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
}