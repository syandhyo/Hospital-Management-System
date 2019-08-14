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

public partial class STOREKEEPER_store_material_master : System.Web.UI.Page
{
    string num1 = "MA000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr, dr1;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from MATERIAL_MASTER_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("MA{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
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
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        if (!IsPostBack)
        {
            binddata();
            auto();
            txttag.Visible = false;
        }
    }
    public void binddata()
    {

        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_MATERIAL_PAGING", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select max(ID) as matid from MATERIAL_MASTER_TABLE WHERE Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                txtmatid.Text = Ds1.Tables[0].Rows[0]["matid"].ToString();
            }
            #region oldcode
            //using (SqlCommand cmd1 = new SqlCommand("SP_MATERIAL_PAGING", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;

            //    cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_PAGE";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@Branch_ID", SqlDbType.VarChar).Value = Session["Branch"];
            //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //    //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,PRICE,QTY FROM MATERIAL_MASTER_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    // GridView1.SelectedIndex = 0;
            //    GridView1.DataSource = dt;
            //    GridView1.DataKeyNames = new string[] { "ID" };
            //    GridView1.DataBind();
            //}
            //SqlCommand comid = new SqlCommand("select max(ID) as matid from MATERIAL_MASTER_TABLE ", con);
            //dr1 = comid.ExecuteReader();
            //if (dr1.Read())
            //{
            //    txtmatid.Text = dr1["matid"].ToString();
            //}
            //dr1.Close();
            #endregion
            SQL_PARAMS = new SqlParameter[1];

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
    public void clear_control()
    {
        try
        {
            lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtid.Text = "";
            txtname.Text = "";
            txtprice.Text = "0.00";
            txthsncode.Text = "0";
            txtgst.Text = "0";
            txtunit.Text = "PCS";
            txtqty.Text = "0";
            droptype.SelectedIndex = 0;
            btncreate.Visible = true;
            btnupdate.Visible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {

                string message = "alert(Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txthsncode.Text == "" || txthsncode.Text == "0")
            {

                string message = "alert('Please!! Enter Hsncode..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtprice.Text == "" || txtprice.Text == "0.00")
            {
                string message = "alert('Please!! Enter Purchase Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[21];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@TYPE",SqlDbType.VarChar , 500, droptype.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, txtprice.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, txtqty.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@GST", SqlDbType.Decimal, 0, txtgst.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, txthsncode.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, txtunit.Text.ToUpper());
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@TAG", SqlDbType.VarChar, 500, txttag.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@MATERIAL_NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());


            SQL_PARAMS[15] = OBJ_METHOD.createParams("@OPN_BAL", SqlDbType.Decimal, 0, txtqty.Text);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@COL_BAL", SqlDbType.Decimal, 0, txtqty.Text);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@REC", SqlDbType.Decimal, 0, "0.00");
            SQL_PARAMS[18] = OBJ_METHOD.createParams("@PUR_RTN", SqlDbType.Decimal, 0, "0.00");
            SQL_PARAMS[19] = OBJ_METHOD.createParams("@ISSUE", SqlDbType.Decimal, 0, "0.00");
            SQL_PARAMS[20] = OBJ_METHOD.createParams("@ISS_RTN", SqlDbType.Decimal, 0, "0.00");

            OBJ_METHOD.ExecuteProceedure("sp_material", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clear_control();
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region oldcode
            //using (SqlCommand cmd1 = new SqlCommand("sp_material", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;

            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
            //    cmd1.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = droptype.Text;
            //    cmd1.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = txtprice.Text;
            //    cmd1.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txtqty.Text;
            //    cmd1.Parameters.Add("@GST", SqlDbType.Decimal).Value = txtgst.Text;
            //    cmd1.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = txthsncode.Text;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    cmd1.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = txtunit.Text;
            //    cmd1.Parameters.Add("@TAG", SqlDbType.VarChar).Value = txttag.Text;
            //    cmd1.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = txtname.Text;
            //    cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtqty.Text;
            //    cmd1.Parameters.Add("@PURCHES", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@RETN", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@ISSUE", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@REF", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = txtqty.Text;


            //    cmd1.ExecuteNonQuery();
            //    string message = "alert('Successfully Inserted.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //}

            //binddata();
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
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT_EVENT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_MATERIAL_PAGING", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                txtname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                droptype.Text = Ds.Tables[0].Rows[0]["TYPE"].ToString();
                txtprice.Text = Ds.Tables[0].Rows[0]["PRICE"].ToString();
                txtqty.Text = Ds.Tables[0].Rows[0]["QTY"].ToString();
                txtgst.Text = Ds.Tables[0].Rows[0]["GST"].ToString();
                txthsncode.Text = Ds.Tables[0].Rows[0]["HSNCODE"].ToString();
                txtdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");
                txtunit.Text = Ds.Tables[0].Rows[0]["UNIT"].ToString();
                txttag.Text = Ds.Tables[0].Rows[0]["TAG"].ToString();
            }
            #region oldcode
            //using (SqlCommand cmd1 = new SqlCommand("SP_MATERIAL_PAGING", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;

            //    cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_EVENT";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    //SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //    //SqlCommand com = new SqlCommand("select * from MATERIAL_MASTER_TABLE where ID='" + slno + "'", con);
            //    dr = cmd1.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        txtid.Text = dr["ID"].ToString();
            //        txtname.Text = dr["NAME"].ToString();
            //        droptype.Text = dr["TYPE"].ToString();
            //        txtprice.Text = dr["PRICE"].ToString();
            //        txtqty.Text = dr["QTY"].ToString();
            //        txtgst.Text = dr["GST"].ToString();
            //        txthsncode.Text = dr["HSNCODE"].ToString();
            //        txtdate.Text = Convert.ToDateTime(dr["DATE"]).ToString("dd-MM-yyyy");
            //        txtunit.Text = dr["UNIT"].ToString();
            //        txttag.Text = dr["TAG"].ToString();
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txthsncode.Text == "" || txthsncode.Text == "0")
            {

                string message = "alert('Please!! Enter The HSN Code..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtprice.Text == "" || txtprice.Text == "0.00")
            {
                string message = "alert('Please!! Enter Purchase Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[15];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@TYPE", SqlDbType.VarChar, 500, droptype.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, txtprice.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, txtqty.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@GST", SqlDbType.Decimal, 0, txtgst.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, txthsncode.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, txtunit.Text.ToUpper());
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@TAG", SqlDbType.VarChar, 500, txttag.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@MATERIAL_NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());

            SQL_PARAMS[13] = OBJ_METHOD.createParams("@OPN_BAL", SqlDbType.Decimal, 0, txtqty.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@COL_BAL", SqlDbType.Decimal, 0, txtqty.Text);

            OBJ_METHOD.ExecuteProceedure("sp_material", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";

            #region oldcode
            //using (SqlCommand cmd1 = new SqlCommand("sp_material", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;

            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //    cmd1.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = droptype.Text;
            //    cmd1.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = txtprice.Text;
            //    cmd1.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txtqty.Text;
            //    cmd1.Parameters.Add("@GST", SqlDbType.Decimal).Value = txtgst.Text;
            //    cmd1.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = txthsncode.Text.ToUpper();
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    cmd1.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = txtunit.Text;
            //    cmd1.Parameters.Add("@TAG", SqlDbType.VarChar).Value = txttag.Text;
            //    cmd1.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = txtname.Text;
            //    cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtqty.Text;
            //    cmd1.Parameters.Add("@PURCHES", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@RETN", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@ISSUE", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@REF", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = txtqty.Text;
            //    cmd1.ExecuteNonQuery();
            //    string message = "alert('Successfully Updated.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //}
            //binddata();
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Updated.')";
        }
        finally
        {
            binddata();
            clear_control();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to Edit this item ?');";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            OBJ_METHOD.ExecuteProceedure("sp_material", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clear_control();
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region oldcode
            //using (SqlCommand cmd1 = new SqlCommand("SP_MATERIAL_PAGING", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;

            //    cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "DELETE";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    //SqlCommand cm = new SqlCommand("delete from MATERIAL_STORE_TABLE where ID='" + slno + "'", con);
            //    //cm.ExecuteNonQuery();
            //    //SqlCommand cm1 = new SqlCommand("delete from MATERIAL_MASTER_TABLE where ID='" + slno + "'", con);
            //    cmd1.ExecuteNonQuery();
            //    //SqlDataAdapter da1 = new SqlDataAdapter("delete from MATERIAL_STOCK_TABLE WHERE ID='" + slno + "'", con);
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

            //    DataSet ds1 = new DataSet();
            //    da1.Fill(ds1, "MATERIAL_STOCK_TABLE");
            //    binddata();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
        finally
        {
            binddata();
            clear_control();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clear_control();
        binddata();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_MATERIAL_PAGING", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void dropcate_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void Btnsearch_click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SEARCH");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtsearchname.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_MATERIAL_PAGING", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = Ds;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            #region oldcode
            //using (SqlCommand cmd1 = new SqlCommand("SP_MATERIAL_NAME_SEARCH", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtsearchname.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    GridView1.SelectedIndex = 0;
            //    GridView1.DataSource = dt;
            //    GridView1.DataKeyNames = new string[] { "ID" };
            //    GridView1.DataBind();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void droptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (droptype.SelectedIndex == 0)
        {
            tag.Visible = false;
            txttag.Visible = false;
            txttag.Text = "";
        }
        else if (droptype.SelectedIndex == 1)
        {
            tag.Visible = false;
            txttag.Visible = false;
            txttag.Text = "";
        }
        else
        {
            tag.Visible = true;
            txttag.Visible = true;
            txttag.Text = "";
        }
    }
}