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

public partial class RADIOLOGY_radiology_test_name : System.Web.UI.Page
{
    //string num1 = "SJ000";
    //SqlConnection con;
    //SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    //DataSet ds = new DataSet();
    //SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd13, cmd14, cmd15, cmd16;
    //SqlDataReader dr, dr1, dr2;
    //DataTable dt;
    //DataRow dtr;
    //int i, no, no1, sl, F;
    //int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    //string id, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    //public string id_hist;
    //public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    //decimal a, b, c, d, e, f, g, h, j;
    //DateTime DT;
    //double totalamt1, totamt, totalgstamt, dis, dism;
    //GridViewRow gr;
    DataMathods OBJ_METHOD = new DataMathods();
   
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

            SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_GRIDPAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_TESTNAME", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }

            SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CATA");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RADIO_TESTNAME", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                Dropcategory.DataSource = Ds1;
                Dropcategory.DataTextField = "NAME";
                Dropcategory.DataValueField = "ID";
                Dropcategory.DataBind();
                Dropcategory.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            #region old code
            //using (SqlCommand cmd = new SqlCommand("RADIO_TESTNAME", con))
                //{
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRIDPAGE";
                //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                //    //SqlDataAdapter da = new SqlDataAdapter("SELECT A.slno as ID,A.INV AS CATEGORY FROM RADIOLOGY_COMPONENT_TABLE A,RADIOLOGY_CATEGORY_TABLE B WHERE A.ID=B.ID AND A.ORGID='" + lblorgid.Text + "'ORDER BY A.ID DESC", con);
                //    DataTable dt = new DataTable();
                //    da.Fill(dt);
                //    GridView2.SelectedIndex = 0;
                //    GridView2.DataSource = dt;
                //    GridView2.DataKeyNames = new string[] { "ID" };
                //    GridView2.DataBind();
                //}
                //using (SqlCommand cmd1 = new SqlCommand("RADIO_TESTNAME", con))
                //{
                //    cmd1.CommandType = CommandType.StoredProcedure;
                //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CATA";
                //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //    cmd1.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
                //    cmd1.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "";
                //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                //    //da = new SqlDataAdapter("select ID,NAME FROM RADIOLOGY_CATEGORY_TABLE where ORGID='" + lblorgid.Text + "'", con);
                //    DataTable ds = new DataTable();
                //    da.Fill(ds);
                //    Dropcategory.DataSource = ds;
                //    Dropcategory.DataTextField = "NAME";
                //    Dropcategory.DataValueField = "ID";
                //    Dropcategory.DataBind();
                //    Dropcategory.Items.Insert(0, new ListItem("Please Select", "0"));
            //}
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
            grvStudentDetails.DataSource = (DataTable)ViewState["ITEM"];
            grvStudentDetails.DataBind();

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[1].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    public void clearcontrol()
    {
        txtinv.Text = "";
        txtprice.Text = "0";
    }
    public void clearfield()
    {
        txtinv.Text = "";
        txtprice.Text = "0";
        Dropcategory.SelectedIndex = 0;
        btncreate.Visible = true;
        btnupdate.Visible = false;
        grvStudentDetails.DataSource = null;
        grvStudentDetails.DataBind();
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("INV"), new DataColumn("PRICE") });
            ViewState["ITEM"] = dt;
            this.BindGrid();
            binddata();

        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtinv.Text == "")
            {
                string message = "alert('*Please!! Enter The Investigation Desired..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtprice.Text == "" || txtprice.Text == "0")
            {
                string message = "alert('*Please!! Enter The Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDouble(txtprice.Text) < 0)
            {
                string message = "alert('*Please!! Enter The Price As Positive Value..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtinv.Text.ToUpper(), txtprice.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();
            clearcontrol();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = (DataTable)ViewState["ITEM"];
        GridViewRow row = (GridViewRow)grvStudentDetails.Rows[e.RowIndex];
        dt.Rows[index].Delete();
        ViewState["ITEM"] = dt;
        this.BindGrid();

    }
    protected void GVOnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lbl_slno") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_GRIDPAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_TESTNAME", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        #region oldcode
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //using (SqlCommand COM = new SqlCommand("RADIO_TESTNAME", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRIDPAGE";
        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //    cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
        //    cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "";
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    //SqlDataAdapter da = new SqlDataAdapter("SELECT A.ID as ID,A.INV AS CATEGORY FROM RADIOLOGY_COMPONENT_TABLE A,RADIOLOGY_CATEGORY_TABLE B WHERE A.ID=B.ID AND A.ORGID='" + lblorgid.Text + "'ORDER BY A.ID DESC", con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    GridView2.SelectedIndex = 0;
        //    GridView2.DataSource = dt;
        //    GridView2.PageIndex = e.NewPageIndex;
        //    GridView2.DataKeyNames = new string[] { "ID" };
        //    GridView2.DataBind();
        //}
        //con.Close();
        #endregion
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            string slno = GridView2.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            OBJ_METHOD.ExecuteProceedure("RADIO_TESTNAME_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
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
        #region old code
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //try
        //{
        //    string slno = GridView2.DataKeys[e.RowIndex].Values["ID"].ToString();
        //    using (SqlCommand cmd1 = new SqlCommand("RADIO_TESTNAME", con))
        //    {
        //        cmd1.CommandType = CommandType.StoredProcedure;
        //        cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
        //        cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
        //        cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //        cmd1.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
        //        cmd1.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "";
        //        //SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //        //SqlCommand cm1 = new SqlCommand("delete from RADIOLOGY_COMPONENT_TABLE where ID='" + slno + "'", con);
        //        cmd1.ExecuteNonQuery();
        //    }
        //    binddata();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
        //con.Close();
        //Response.Redirect("~/RADIOLOGY/radiology_test_name.aspx");
        #endregion
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT1");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_TESTNAME", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                TXTID.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                Dropcategory.SelectedValue = Ds.Tables[0].Rows[0]["ID"].ToString();
            }
            SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT2");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RADIO_TESTNAME", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grvStudentDetails.DataSource = Ds1.Tables["Table"];
                grvStudentDetails.DataBind();

                DataTable dt = Ds1.Tables["Table"];
                ViewState["ITEM"] = dt;

                GridView1.DataSource = Ds1.Tables["Table"];
                GridView1.DataBind();
            }
            #region oldcode
            //using (SqlCommand cmd1 = new SqlCommand("RADIO_TESTNAME", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT1";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "";
            //    //SqlCommand com = new SqlCommand("select ID,INV,PRICE from RADIOLOGY_COMPONENT_TABLE where ID='" + slno + "'", con);
            //    dr = cmd1.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        TXTID.Text = dr["ID"].ToString();
            //        Dropcategory.SelectedValue = dr["ID"].ToString();
            //    }
            //    dr.Close();
            //}
            //using (SqlCommand cmd = new SqlCommand("RADIO_TESTNAME", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT2";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "";
            //    da = new SqlDataAdapter(cmd);
            //    //da = new SqlDataAdapter("select INV,PRICE FROM RADIOLOGY_COMPONENT_TABLE where ID='" + TXTID.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            //    DataSet ds2 = new DataSet();
            //    da.Fill(ds2);
            //    grvStudentDetails.DataSource = ds2.Tables["Table"];
            //    grvStudentDetails.DataBind();

            //    DataTable dt = ds2.Tables["Table"];
            //    ViewState["ITEM"] = dt;
            //}
            //using (SqlCommand cmd2 = new SqlCommand("RADIO_TESTNAME", con))
            //{
            //    cmd2.CommandType = CommandType.StoredProcedure;
            //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT2";
            //    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd2.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
            //    SqlDataAdapter da1 = new SqlDataAdapter("select INV,PRICE FROM RADIOLOGY_COMPONENT_TABLE where ID='" + TXTID.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            //    DataSet ds3 = new DataSet();
            //    da1.Fill(ds3);
            //    GridView1.DataSource = ds3.Tables["Table"];
            //    GridView1.DataBind();
            //}
            #endregion
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
            if (Dropcategory.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Catagory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (grvStudentDetails.Rows.Count <= 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            int chkedcounter = 0;
            int correctinput = 0;
            DataTable dt1 = ViewState["ITEM"] as DataTable;
            GridView1.DataSource = dt1;
            GridView1.DataBind();
            foreach (GridViewRow gv1 in GridView1.Rows)
            {
                chkedcounter++;
                SqlParameter[] SQL_PARAMS = new SqlParameter[7];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, Dropcategory.SelectedValue);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, gv1.Cells[1].Text);

                OBJ_METHOD.ExecuteProceedure("RADIO_TESTNAME_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                if (OBJ_METHOD._RESULT > 0)
                {
                    correctinput++;
                }
            }
            if (chkedcounter == correctinput && chkedcounter > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Error occurred while processing data... Rolling back...')";
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
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

    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (grvStudentDetails.Rows.Count <= 0)
            {
                string message = "alert('* Add item to save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            int chkedcounter = 0;
            int correctinput = 0;

            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("RADIO_TESTNAME", "", "", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                DataTable dt1 = ViewState["ITEM"] as DataTable;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                foreach (GridViewRow gv1 in GridView1.Rows)
                {
                    chkedcounter++;
                    SQL_PARAMS = new SqlParameter[7];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, Dropcategory.SelectedValue);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, gv1.Cells[1].Text);

                    OBJ_METHOD.ExecuteProceedure("RADIO_TESTNAME_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        correctinput++;
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
            #region oldcode
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cmd1 = new SqlCommand("RADIO_TESTNAME", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //    //SqlDataAdapter da = new SqlDataAdapter("delete from RADIOLOGY_COMPONENT_TABLE where ID='" + TXTID.Text + "'", con);
            //    DataSet d = new DataSet();
            //    da.Fill(d);
            //}

            //DataTable dt1 = ViewState["ITEM"] as DataTable;
            //GridView1.DataSource = dt1;
            //GridView1.DataBind();
            //foreach (GridViewRow gv1 in GridView1.Rows)
            //{
            //    using (SqlCommand cmd2 = new SqlCommand("RADIO_TESTNAME", con))
            //    {
            //        cmd2.CommandType = CommandType.StoredProcedure;
            //        cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //        //SqlCommand cmd2 = new SqlCommand("insert into RADIOLOGY_COMPONENT_TABLE (ORGID,ID,INV,PRICE) values (@ORGID,@ID,@INV,@PRICE)", con);
            //        cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = Dropcategory.SelectedValue; ;
            //        cmd2.Parameters.Add("@INV", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            //        cmd2.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            //        cmd2.ExecuteNonQuery();
            //    }
            //}
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        clearfield();
       
    }
}