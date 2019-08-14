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

public partial class RADIOLOGY_radiology_test_wise_consumption : System.Web.UI.Page
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
    string PAIDMAT;
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
            using (SqlCommand cmd = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DISTINCT_DEPT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                //cmd.CommandText = "select DISTINCT ITEMNAME from TBL_DEPT_MAT_STOCK where ITEMNAME like @SearchText+'%' AND ID=(select id from tblDepartment where DeptName like 'Radi%')";
                cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = prefix;
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ITEMNAME"]));
                    }
                }
                con.Close();
            }
        }
        return customers.ToArray();
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from RADIO_CONSUMP_TEST";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("RC{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
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
        lblid.Text = Session["NAME"].ToString();
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        if (!IsPostBack)
        {
            binddata();
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
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("NAME"), new DataColumn("QTY") });
            ViewState["ITEM"] = dt;
            this.BindGrid();

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DISTINCT_CONSUM_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_TESTWISE_CONSUMP", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvtestCons.DataSource = Ds;
                grvtestCons.DataKeyNames = new string[] { "ID" };
                grvtestCons.DataBind();
            }

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_COMPO");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RADIO_TESTWISE_CONSUMP", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                droptesname.DataSource = Ds1;
                droptesname.DataTextField = "INV";
                droptesname.DataValueField = "slno";
                droptesname.DataBind();
                droptesname.Items.Insert(0,new ListItem("Please Select","0"));
            }
            #region oldcode
            //using (SqlCommand cmd = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DISTINCT_CONSUM_PAGE";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da1 = new SqlDataAdapter("select DISTINCT B.INV RADTESTNAME,A.DATE,A.ID AS ID from  RADIO_CONSUMP_TEST A,RADIOLOGY_COMPONENT_TABLE B WHERE A.RADTESTNAME=B.slno ORDER BY A.ID ASC", con);
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    grvtestCons.SelectedIndex = 0;
            //    grvtestCons.DataSource = dt1;
            //    grvtestCons.DataKeyNames = new string[] { "ID" };
            //    grvtestCons.DataBind();
            //}
            //using (SqlCommand cmd1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_COMPO";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //    //SqlDataAdapter Adp = new SqlDataAdapter("select slno,INV from RADIOLOGY_COMPONENT_TABLE", con);
            //    DataTable Dt = new DataTable();
            //    Adp.Fill(Dt);
            //    droptesname.DataSource = Dt;
            //    droptesname.DataTextField = "INV";
            //    droptesname.DataValueField = "slno";
            //    droptesname.DataBind();
            //    droptesname.Items.Insert(0, "Please Select");
            //}
            //con.Close();
            #endregion
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
            GridView1.DataSource = (DataTable)ViewState["ITEM"];
            GridView1.DataBind();
        }
        catch (Exception x)
        {
            string var = x.Message;
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtmaterialnm.Text == "")
            {
                string message = "alert('* Material Name are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtquant.Text == "" || txtquant.Text == "0.00")
            {
                string message = "alert('* Quantity are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtmaterialnm.Text.Trim(), txtquant.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();

            con.Close();
            txtmaterialnm.Text = "";
            txtquant.Text = "";
        }
        catch (Exception x)
        {
            string var = x.Message;
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM"];
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];

            Label name = (Label)row.FindControl("lblname");

            Label quantity = (Label)row.FindControl("lblqty");

            string name1 = name.Text.ToString();
            string quantity1 = quantity.Text.ToString();

            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;

            this.BindGrid();
        }
        catch (Exception x)
        {
            string var = x.Message;
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (droptesname.SelectedIndex == 0)
            {
                string message = "alert('* Test Name are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (GridView1.Rows.Count <= 0)
            {

                string message = "alert('*Add item For Test.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            int chkedcounter = 0;
            int correctinput = 0;

            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@RADTESTNAME", SqlDbType.VarChar, 500, droptesname.SelectedValue);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("USP_RADO_CONSUMP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                foreach (GridViewRow gv1 in GridView1.Rows)
                {
                    var nameGr = (gv1.FindControl("lblname") as Label);
                    var quantyGr = (gv1.FindControl("lblqty") as Label);
                    {
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[6];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@QUANTY", SqlDbType.Decimal, 0, quantyGr.Text);

                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@MATERIAL_NAME", SqlDbType.VarChar, 500, nameGr.Text);

                        OBJ_METHOD.ExecuteProceedure("USP_RADO_CONSUMP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            correctinput++;
                        }
                    }

                }
                if (chkedcounter == correctinput && chkedcounter > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                    clearfield();
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                }
            }
            #region old code
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //auto();
            //using (SqlCommand MR_cmd = new SqlCommand("USP_RADO_CONSUMP", con))
            //{
            //    MR_cmd.CommandType = CommandType.StoredProcedure;
            //    MR_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    MR_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    MR_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    MR_cmd.Parameters.Add("@RADTESTNAME", SqlDbType.VarChar).Value = droptesname.SelectedValue;

            //    MR_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //    MR_cmd.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = "";
            //    MR_cmd.Parameters.Add("@QUANTY", SqlDbType.Decimal).Value = "0.00";

            //    MR_cmd.ExecuteNonQuery();

            //}
            //foreach (GridViewRow gv1 in GridView1.Rows)
            //{
            //    var nameGr = (gv1.FindControl("lblname") as Label);
            //    var quantyGr = (gv1.FindControl("lblqty") as Label);
            //    using (SqlCommand MRitem_cmd = new SqlCommand("USP_RADO_CONSUMP", con))
            //    {
            //        MRitem_cmd.CommandType = CommandType.StoredProcedure;
            //        MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
            //        MRitem_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //        MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //        MRitem_cmd.Parameters.Add("@RADTESTNAME", SqlDbType.VarChar).Value = "";

            //        MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //        MRitem_cmd.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = nameGr.Text;
            //        MRitem_cmd.Parameters.Add("@QUANTY", SqlDbType.Decimal).Value = quantyGr.Text;

            //        MRitem_cmd.ExecuteNonQuery();
            //    }
            //}
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
   
    protected void grvtestCons_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DISTINCT_CONSUM_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_TESTWISE_CONSUMP", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvtestCons.DataSource = Ds;
                grvtestCons.PageIndex = e.NewPageIndex;
                grvtestCons.DataKeyNames = new string[] { "ID" };
                grvtestCons.DataBind();
            }
            #region old code
            //using (SqlCommand cmd1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DISTINCT_CONSUM_PAGE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //    //SqlDataAdapter Adp = new SqlDataAdapter("select DISTINCT B.INV RADTESTNAME,A.DATE,A.ID AS ID from  RADIO_CONSUMP_TEST A,RADIOLOGY_COMPONENT_TABLE B WHERE A.RADTESTNAME=B.slno ORDER BY A.ID ASC", con);
            //    DataTable dt = new DataTable();
            //    Adp.Fill(dt);
            //    grvtestCons.DataSource = dt;
            //    grvtestCons.PageIndex = e.NewPageIndex;
            //    grvtestCons.DataKeyNames = new string[] { "ID" };
            //    grvtestCons.DataBind();
            //}
        #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grvtestCons_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grvtestCons.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT1");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_TESTWISE_CONSUMP", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btncancel.Visible = true;
                btndelete.Visible = false;
                txtselid.Text = slno.ToString();
                txtdate.Text = Ds.Tables[0].Rows[0]["DATE"].ToString();
                droptesname.Text = Ds.Tables[0].Rows[0]["RADTESTNAME"].ToString();
                txtAutoid.Text = Ds.Tables[0].Rows[0]["RID"].ToString();
            }
            SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_2EVENT2");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RADIO_TESTWISE_CONSUMP", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = Ds1;
                GridView1.DataBind();

                DataTable dt = Ds1.Tables["Table"];
                ViewState["ITEM"] = dt;


                da1.Fill(Ds1);
                grvhidden.SelectedIndex = 0;
                grvhidden.DataSource = Ds1;
                grvhidden.DataBind();
            }
            #region old code
            //using (SqlCommand cmd1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT1";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cmd1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    dr = cmd1.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        btncancel.Visible = true;
            //        btndelete.Visible = false;
            //        txtselid.Text = slno.ToString();
            //        txtdate.Text = dr["DATE"].ToString();
            //        droptesname.Text = dr["RADTESTNAME"].ToString();
            //        txtAutoid.Text = dr["RID"].ToString();
            //        dr.Close();
            //    }
            //}
            //using (SqlCommand com1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
            //{
            //    com1.CommandType = CommandType.StoredProcedure;
            //    com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2EVENT2";
            //    com1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    com1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da1 = new SqlDataAdapter(com1);
            //    DataSet ds1 = new DataSet();
            //    da1.Fill(ds1);
            //    GridView1.SelectedIndex = 0;
            //    GridView1.DataSource = ds1;
            //    GridView1.DataBind();

            //    DataTable dt = ds1.Tables["Table"];
            //    ViewState["ITEM"] = dt;

                
            //    da1.Fill(ds1);
            //    grvhidden.SelectedIndex = 0;
            //    grvhidden.DataSource = ds1;
            //    grvhidden.DataBind();
            //}
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void clearfield()
    {
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        txtmaterialnm.Text = "";
        txtquant.Text = "0";
        txtselid.Text = "";
        btncreate.Visible = true;
        btndelete.Visible = false;
        btnupdate.Visible = false;
        grvhidden.DataSource = null;
        grvhidden.DataBind();
        GridView1.DataSource = null;
        GridView1.DataBind();
        auto();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        binddata();
        clearfield();
        auto();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (droptesname.SelectedIndex == 0)
            {
                string message = "alert('* Test Name are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (GridView1.Rows.Count <= 0)
            {

                string message = "alert('*Add item For Test.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            int chkedcounter = 0;
            int correctinput = 0;
            
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtselid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@RADTESTNAME", SqlDbType.VarChar, 500, droptesname.SelectedValue);

            OBJ_METHOD.ExecuteProceedure("USP_RADO_CONSUMP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE3_UPDATE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtselid.Text);

                OBJ_METHOD.ExecuteProceedure("RADIO_TESTWISE_CONSUMP", "", "", SqlDbType.VarChar, SQL_PARAMS);

                if (OBJ_METHOD._RESULT > 0)
                {
                    foreach (GridViewRow gv1 in GridView1.Rows)
                    {
                        var nameGr = (gv1.FindControl("lblname") as Label);
                        var quantyGr = (gv1.FindControl("lblqty") as Label);
                        {
                            chkedcounter++;
                            SQL_PARAMS = new SqlParameter[6];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDUPDATE");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtselid.Text);
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@QUANTY", SqlDbType.Decimal, 0, quantyGr.Text);

                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@MATERIAL_NAME", SqlDbType.VarChar, 500, nameGr.Text);

                            OBJ_METHOD.ExecuteProceedure("USP_RADO_CONSUMP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                            if (OBJ_METHOD._RESULT > 0)
                            {
                                correctinput++;
                            }
                        }
                    }
                    if (chkedcounter == correctinput && chkedcounter > 0)
                    {

                        OBJ_METHOD.commitOrRollbackTran("commit");
                        binddata();
                        clearfield();
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Error occurred while processing data... Rolling back...')";
                    }
                }
                
            }
            #region old code
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            ////  auto();
            //using (SqlCommand MR_cmd = new SqlCommand("USP_RADO_CONSUMP", con))
            //{
            //    MR_cmd.CommandType = CommandType.StoredProcedure;
            //    MR_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    MR_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
            //    MR_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    MR_cmd.Parameters.Add("@RADTESTNAME", SqlDbType.VarChar).Value = droptesname.SelectedValue;

            //    MR_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //    MR_cmd.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = "";
            //    MR_cmd.Parameters.Add("@QUANTY", SqlDbType.Decimal).Value = "0.00";

            //    MR_cmd.ExecuteNonQuery();

            //}
            //using (SqlCommand com1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
            //{
            //    com1.CommandType = CommandType.StoredProcedure;
            //    com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE3_UPDATE";
            //    com1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
            //    com1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da = new SqlDataAdapter(com1);
            //    //SqlDataAdapter da = new SqlDataAdapter("delete from RAD_CONSUM_TESTITEM where ID='" + txtselid.Text + "'", con);
            //    DataSet ds1 = new DataSet();
            //    da.Fill(ds1);

            //    DataTable dt1 = ViewState["ITEM"] as DataTable;
            //    GridView1.DataSource = dt1;
            //    GridView1.DataBind();
            //}

            //foreach (GridViewRow gv1 in GridView1.Rows)
            //{
            //    //var lbname = (gv1.FindControl("chkRow") as CheckBox);
            //    var nameGr = (gv1.FindControl("lblname") as Label);
            //    var quantyGr = (gv1.FindControl("lblqty") as Label);
            //    using (SqlCommand MRitem_cmd = new SqlCommand("USP_RADO_CONSUMP", con))
            //    {
            //        MRitem_cmd.CommandType = CommandType.StoredProcedure;
            //        MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
            //        MRitem_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
            //        MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //        MRitem_cmd.Parameters.Add("@RADTESTNAME", SqlDbType.VarChar).Value = "";

            //        MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //        MRitem_cmd.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = nameGr.Text;
            //        MRitem_cmd.Parameters.Add("@QUANTY", SqlDbType.Decimal).Value = quantyGr.Text;

            //        MRitem_cmd.ExecuteNonQuery();
            //    }
            //}
            //con.Close();
            #endregion
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
    
    protected void grvtestCons_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void grvtestCons_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            var slno = grvtestCons.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            OBJ_METHOD.ExecuteProceedure("USP_RADO_CONSUMP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();
            }
           
            DataTable dt1 = ViewState["ITEM"] as DataTable;
            GridView1.DataSource = dt1;
            GridView1.DataBind();

            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region old code
            //using (SqlCommand com1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
            //{
            //    com1.CommandType = CommandType.StoredProcedure;
            //    com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE3_UPDATE";
            //    com1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    com1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da = new SqlDataAdapter(com1);
            //    //SqlDataAdapter da = new SqlDataAdapter("delete from RADIO_CONSUMP_TEST where ID='" + slno + "'", con);
            //    DataSet d = new DataSet();
            //    da.Fill(d);
            //    hdndel.Value = slno.ToString();

            //    DataTable dt1 = ViewState["ITEM"] as DataTable;
            //    GridView1.DataSource = dt1;
            //    GridView1.DataBind();
            //}
            //using (SqlCommand com = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE3_UPDATE";
            //    com.Parameters.Add("@ID", SqlDbType.VarChar).Value = hdndel.Value;
            //    com.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da1 = new SqlDataAdapter(com);
            //    DataSet d1 = new DataSet();
            //    da1.Fill(d1);
            //}
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
}