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

public partial class ADMIN_Admin_Corpo_Services : System.Web.UI.Page
{
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
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lblorgid.Text = Session["ORGID"].ToString();

            if (!IsPostBack)
            {
                Session["SortedView"] = null;
                binddata();
                bindTGrid();
                show.Visible = false;
            }
            //binddata();

        }

        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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

            DataSet Ds = OBJ_METHOD.Get_DataSet("select A.SER_ID AS ID,A.SER_NAME AS NAME,A.SER_DATE AS DATE,A.PRICE,B.CNAME from CORPO_SERVICE_MASTER AS A,Corporate_Table AS B where A.STATUS='ACTIVE' and A.Branch_ID=B.Branch_ID And B.ID=A.CORPO_ID AND A.Branch_ID='" + Session["Branch"].ToString() + "' ORDER BY A.SER_ID DESC", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }

            DataSet Dt = OBJ_METHOD.Get_DataSet("select ID,CNAME from Corporate_Table where ISACTIVE='true'", false, false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                dropcorpo.DataSource = Dt;
                dropcorpo.DataTextField = "CNAME";
                dropcorpo.DataValueField = "ID";
                dropcorpo.DataBind();
                //dropcorp.Items.Insert(0, "Please Select");
                dropcorpo.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
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
        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("NAME"), new DataColumn("Type"), new DataColumn("ID"), new DataColumn("PRICE") });
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
    protected void droptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (droptype.SelectedValue == "Select")
        {
            dropproc.Items.Clear();
            txtprice.Text = "";
            //dropproc.DataSource = null;
            //dropproc.DataTextField = null;
            //dropproc.DataValueField = null;
            //dropproc.DataBind();
        }
        else if (droptype.SelectedValue == "Ward")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,NAME FROM WARD_TABLE where Branch_ID = " + Session["Branch"] + " AND Type='Ward'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropproc.DataSource = Ds;
                dropproc.DataTextField = "NAME";
                dropproc.DataValueField = "ID";
                dropproc.DataBind();
                dropproc.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            txtprice.Text = "";
        }
        else if (droptype.SelectedValue == "Cabin")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,NAME FROM WARD_TABLE where Branch_ID = " + Session["Branch"] + " AND Type='Cabin'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropproc.DataSource = Ds;
                dropproc.DataTextField = "NAME";
                dropproc.DataValueField = "ID";
                dropproc.DataBind();
                dropproc.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            txtprice.Text = "";
        }
        else if (droptype.SelectedValue == "Labrotory")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select slno,INV FROM TEST_COMPONENT_TABLE where Branch_ID = " + Session["Branch"] + " AND STATUS='ACTIVE'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropproc.DataSource = Ds;
                dropproc.DataTextField = "INV";
                dropproc.DataValueField = "slno";
                dropproc.DataBind();
                dropproc.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            txtprice.Text = "";
        }
        else if (droptype.SelectedValue == "Radiology")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select slno,INV FROM RADIOLOGY_COMPONENT_TABLE where Branch_ID = " + Session["Branch"] + " AND STATUS='ACTIVE'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropproc.DataSource = Ds;
                dropproc.DataTextField = "INV";
                dropproc.DataValueField = "slno";
                dropproc.DataBind();
                dropproc.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            txtprice.Text = "";
        }
        else if (droptype.SelectedValue == "Ambulance")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from tblAmbulanceEntry where  Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropproc.DataSource = Ds;
                dropproc.DataTextField = "AmbulanceNo";
                dropproc.DataValueField = "id";
                dropproc.DataBind();
                dropproc.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            txtprice.Text = "";
        }
        else if (droptype.SelectedValue == "OT")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,Name FROM OT_SURGERY_MST where Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropproc.DataSource = Ds;
                dropproc.DataTextField = "Name";
                dropproc.DataValueField = "ID";
                dropproc.DataBind();
                dropproc.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            txtprice.Text = "";
        }

    }
    protected void grdMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM"];
            GridViewRow row = (GridViewRow)grdMaterial.Rows[e.RowIndex];
            Label name = (Label)row.FindControl("lbl_Name");
            Label type = (Label)row.FindControl("lbl_type");
            Label nameid = (Label)row.FindControl("lbl_ID");
            Label price = (Label)row.FindControl("lbl_Price");
            string nm = name.Text.ToString();
            string PRICE = price.Text.ToString();
            string typ = type.Text.ToString();
            string nmid = nameid.Text.ToString();

            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;
            lbltotalprice.Text = (Convert.ToDecimal(lbltotalprice.Text) - Convert.ToDecimal(PRICE)).ToString();
            lbltotalamt.Text = (Convert.ToDecimal(lbltotalamt.Text) - Convert.ToDecimal(PRICE)).ToString();

            this.BindGrid();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (dropproc.SelectedIndex == 0)
        {
            string message = "alert('*Select Procedure.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            dropproc.Focus();
            return;
        }
        else if (txtdiscount.Text == "")
        {
            string message = "alert('*Add Qty.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            txtdiscount.Focus();
            return;
        }
        else if (droptype.SelectedIndex == 0)
        {
            string message = "alert('*Add Unit.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            droptype.Focus();
            return;
        }
        DataTable dt = (DataTable)ViewState["ITEM"];
        dt.Rows.Add(dropproc.SelectedItem.Text, droptype.SelectedValue, dropproc.SelectedValue, txtdiscount.Text);
        ViewState["ITEM"] = dt;
        this.BindGrid();
        lbltotalprice.Text = (Convert.ToDecimal(txtdiscount.Text) + Convert.ToDecimal(lbltotalprice.Text)).ToString();
        lbltotalamt.Text = (Convert.ToDecimal(lbltotalamt.Text) + Convert.ToDecimal(txtdiscount.Text)).ToString();

        txtdiscount.Text = "";
        txtprice.Text = "";
        droptype.SelectedIndex = 0;
        dropproc.Items.Clear();
        show.Visible = true;
    }
    public void clearcontrol()
    {
        txtsername.Text = "";
        dropproc.Items.Clear();
        droptype.SelectedIndex = 0;
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtdiscount.Text = "";
        DataTable dt = (DataTable)ViewState["ITEM"];
        dt.Clear();
        grdMaterial.DataSource = dt;
        grdMaterial.DataBind();
        txtprice.Text = "";
        lblgstamt.Text = lbltotalamt.Text = lbltotalprice.Text = "0";
        show.Visible = false;
        btnSubmit.Visible = true;
        btnupdate.Visible = false;

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {

            if (txtsername.Text.Trim() == "")
            {
                string message = "alert('* Service Name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtsername.Focus();
                return;
            }
            else if (txtdate.Text == "")
            {
                string message = "alert('* Date Is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdate.Focus();
                return;
            }

            else if (grdMaterial.Rows.Count <= 0)
            {
                string message = "alert('*Add item.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropproc.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@SER_NAME", SqlDbType.VarChar, 500, txtsername.Text.ToUpper());
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@SER_DATE", SqlDbType.Date, 500, Convert.ToDateTime(txtdate.Text).ToString("dd-MM-yyyy"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, Convert.ToDecimal(lbltotalprice.Text));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@GST", SqlDbType.Decimal, 0, Convert.ToDecimal(lblgstamt.Text));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@TOTAL_PRICE", SqlDbType.Decimal, 0, Convert.ToDecimal(lbltotalamt.Text));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@CORPO_ID",SqlDbType.Int,0,dropcorpo.SelectedValue);

            OBJ_METHOD.ExecuteProceedure("ADMIN_SERVICE_CORPO_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            //var id = OBJ_METHOD._objOut;


            int chkedcounter = 0;
            int correctinput = 0;
            if (OBJ_METHOD._RESULT > 0)
            {
                string status = OBJ_METHOD._objOut.ToString().Split('@')[0];
                string master_id = OBJ_METHOD._objOut.ToString().Split('@')[1];
                message1 = "alert('" + master_id + "')";
                foreach (GridViewRow gv1 in grdMaterial.Rows)
                {
                    var name = (gv1.FindControl("lbl_Name") as Label);

                    var type = (gv1.FindControl("lbl_type") as Label);
                    var invid = (gv1.FindControl("lbl_ID") as Label);
                    var invprice = (gv1.FindControl("lbl_Price") as Label);
                    chkedcounter++;
                    SQL_PARAMS = new SqlParameter[8];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@SER_ID", SqlDbType.Int, 0, status);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@INV_NAME", SqlDbType.VarChar, 500, name.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@INV_ID", SqlDbType.Int, 0, invid.Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@SER_TYPE", SqlDbType.VarChar, 500, type.Text);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@IPRICE", SqlDbType.Decimal, 0, invprice.Text);
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


                    OBJ_METHOD.ExecuteProceedure("ADMIN_SERVICE_CORPO_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        correctinput++;

                    }
                }
                if (chkedcounter == correctinput && chkedcounter > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearcontrol();
                    binddata();
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                }

            }
            else
            {
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

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
        binddata();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void dropproc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (droptype.SelectedValue == "Select")
        {
            dropproc.Items.Clear();
            txtprice.Text = "";
        }
        else if (droptype.SelectedValue == "Ward")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select Price from Ward_Price where DATE=(select  max(DATE) from Ward_Price WHERE Branch_ID='" + Session["Branch"] + "' AND WARDID='" + dropproc.SelectedValue + "') AND Branch_ID='" + Session["Branch"] + "' AND WARDID='" + dropproc.SelectedValue + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtprice.Text = Ds.Tables[0].Rows[0]["Price"].ToString();
            }
        }
        else if (droptype.SelectedValue == "Cabin")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select Price from Ward_Price where DATE=(select  max(DATE) from Ward_Price WHERE Branch_ID='" + Session["Branch"] + "' AND WARDID='" + dropproc.SelectedValue + "') AND Branch_ID='" + Session["Branch"] + "' AND WARDID='" + dropproc.SelectedValue + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtprice.Text = Ds.Tables[0].Rows[0]["Price"].ToString();
            }
        }
        else if (droptype.SelectedValue == "Labrotory")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select Price from TEST_COMPONENT_PRICE where date=(select  max(date) from TEST_COMPONENT_PRICE WHERE Branch_ID='" + Session["Branch"] + "' AND INV_ID='" + dropproc.SelectedValue + "') AND Branch_ID='" + Session["Branch"] + "' AND INV_ID='" + dropproc.SelectedValue + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtprice.Text = Ds.Tables[0].Rows[0]["Price"].ToString();
            }
        }
        else if (droptype.SelectedValue == "Radiology")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select Price from RADIOLOGY_PRICE_TABLE where date=(select  max(date) from RADIOLOGY_PRICE_TABLE WHERE Branch_ID='" + Session["Branch"] + "' AND ID='" + dropproc.SelectedValue + "') AND Branch_ID='" + Session["Branch"] + "' AND ID='" + dropproc.SelectedValue + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtprice.Text = Ds.Tables[0].Rows[0]["Price"].ToString();
            }
        }
        else if (droptype.SelectedValue == "Ambulance")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select Price from RADIOLOGY_PRICE_TABLE where date=(select  max(date) from RADIOLOGY_PRICE_TABLE WHERE Branch_ID='" + Session["Branch"] + "' AND ID='" + dropproc.SelectedValue + "') AND Branch_ID='" + Session["Branch"] + "' AND ID='" + dropproc.SelectedValue + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtprice.Text = Ds.Tables[0].Rows[0]["Price"].ToString();
            }
        }
        else if (droptype.SelectedValue == "OT")
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select Price from OT_SURGERY_RATE where date=(select  max(date) from OT_SURGERY_RATE WHERE Branch_ID='" + Session["Branch"] + "' AND SUG_ID='" + dropproc.SelectedValue + "') AND Branch_ID='" + Session["Branch"] + "' AND SUG_ID='" + dropproc.SelectedValue + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtprice.Text = Convert.ToDecimal(Ds.Tables[0].Rows[0]["Price"]).ToString() + ".00"; ;
            }
        }
    }
    protected void grdMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}