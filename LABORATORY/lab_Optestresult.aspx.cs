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
using System.IO;

public partial class LABORATORY_lab_Optestresult : System.Web.UI.Page
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


    public void auto()
    {
        try
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

            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,PID,PNAME AS NAME,DATE from LABRES_TABLE WHERE Branch_ID=" + Session["Branch"] + "  ORDER BY ID DESC", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select a.ID,a.OPDNO,a.NAME,Convert(varchar,a.DATE,105) DATE from TBLRECLAB a,TBLRECLABITEM b where a.ID=b.ID and A.ID='" + lblsession.Text + "' and a.Branch_ID=" + Session["Branch"] + "", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                TXTID.Text = Ds1.Tables[0].Rows[0]["ID"].ToString();
                txtname.Text = Ds1.Tables[0].Rows[0]["NAME"].ToString();
                txtopdno.Text = Ds1.Tables[0].Rows[0]["OPDNO"].ToString();
                txtinvdate.Text = Ds1.Tables[0].Rows[0]["DATE"].ToString();
                txtreqno.Text = Ds1.Tables[0].Rows[0]["ID"].ToString();
            }
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select B.INVID AS ID,C.NAME,B.INV AS INV,C.REF AS RANGE,C.UNIT from TBLRECLAB A,TBLRECLABITEM B,TEST_COMPONENT_TABLE C,TEST_NAME_TABLE D where B.CHKSEL='1' AND A.ID=B.ID AND C.ID=D.ID  AND B.INVID=C.slno AND A.ID='" + lblsession.Text + "' and a.Branch_ID=" + Session["Branch"] + "", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds2;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            #region old code
            // SqlDataAdapter da1 = new SqlDataAdapter("SELECT A.ID as ID,A.DATE AS DATE,A.PNAME AS PID FROM LABRES_TABLE A  WHERE  A.ORGID='" + lblorgid.Text + "' AND A.PTYPE!='INPATIENT' ORDER BY A.ID DESC", con);
            //SqlDataAdapter da1 = new SqlDataAdapter("select ID,PID,PNAME AS NAME,DATE from LABRES_TABLE  ORDER BY ID DESC", con);
            //DataTable dt1 = new DataTable();
            //da1.Fill(dt1);

            //GridView2.DataSource = dt1;
            //GridView2.DataKeyNames = new string[] { "ID" };
            //GridView2.DataBind();



            //SqlCommand C = new SqlCommand("select a.ID,a.OPDNO,a.NAME,Convert(varchar,a.DATE,105) DATE from TBLRECLAB a,TBLRECLABITEM b where a.ID=b.ID and A.ID='" + lblsession.Text + "'", con);
            //dr = C.ExecuteReader();
            //if (dr.Read())
            //{
            //    TXTID.Text = dr["ID"].ToString();
            //    txtname.Text = dr["NAME"].ToString();
            //    txtopdno.Text = dr["OPDNO"].ToString();
            //    txtinvdate.Text = dr["DATE"].ToString();
            //    txtreqno.Text = dr["ID"].ToString();
            //}
            //dr.Close();

            //SqlDataAdapter da2 = new SqlDataAdapter("select B.INVID AS ID,C.NAME,B.INV AS INV,C.REF AS RANGE,C.UNIT from TBLRECLAB A,TBLRECLABITEM B,TEST_COMPONENT_TABLE C,TEST_NAME_TABLE D where B.CHKSEL='1' AND A.ID=B.ID AND C.ID=D.ID  AND B.INVID=C.slno AND A.ID='" + lblsession.Text + "'", con);
            //DataTable dt2 = new DataTable();
            //da2.Fill(dt2);

            //GridView1.DataSource = dt2;
            //GridView1.DataKeyNames = new string[] { "ID" };
            //GridView1.DataBind();
            #endregion
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
            lbluid.Text = Session["UID"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();
            txtinvdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            lblsession.Text = Session["labid"].ToString();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ID"), new DataColumn("INV"), new DataColumn("PRICE") });
                ViewState["ITEM"] = dt;
                this.BindGrid();
                binddata();
                //selectItem(); 
                txtinvdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            }
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
            GridView1.DataSource = (DataTable)ViewState["ITEM"];
            GridView1.DataBind();

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
            int chkedcounter = 0;
            int correctinput = 0;
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[10];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, txtopdno.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd HH:mm"));

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@LINDID", SqlDbType.VarChar, 500, txtreqno.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@UNAME", SqlDbType.VarChar, 500, lblid.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@UID", SqlDbType.VarChar, 500, lbluid.Text);

            OBJ_METHOD.ExecuteProceedure("USP_TESTRESULT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
               foreach (GridViewRow row in GridView1.Rows)
               {
                   var inv = row.FindControl("lblinv") as Label;
                   var refe = row.FindControl("txtref") as TextBox;
                   var unit = row.FindControl("txtunit") as TextBox;
                   var value = row.FindControl("txt_value") as TextBox;
                   var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
                    {
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[8];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, INV.ToString());
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@REF", SqlDbType.VarChar, 500, refe.Text.ToString());
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@VALUE", SqlDbType.VarChar, 500, value.Text.ToString());
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unit.Text.ToString());

                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                        OBJ_METHOD.ExecuteProceedure("USP_TESTRESULT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

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
                    clearcontrol();
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                }
            }
            #region old code
            //using (SqlCommand cm = new SqlCommand("USP_TESTRESULT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopdno.Text;
            //    cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
            //    cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@LINDID", SqlDbType.VarChar).Value = txtreqno.Text;
            //    cm.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
            //    cm.Parameters.Add("@UID", SqlDbType.VarChar).Value = lbluid.Text;

            //    cm.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@REF", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@VALUE", SqlDbType.VarChar).Value = "";
            //    cm.ExecuteNonQuery();
            //}
            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    var inv = row.FindControl("lblinv") as Label;
            //    var refe = row.FindControl("txtref") as TextBox;
            //    var unit = row.FindControl("txtunit") as TextBox;
            //    var value = row.FindControl("txt_value") as TextBox;
            //    var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
            //    using (SqlCommand cm = new SqlCommand("USP_TESTRESULT", con))
            //    {
            //        cm.CommandType = CommandType.StoredProcedure;
            //        cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
            //        cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //        cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopdno.Text;
            //        cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
            //        cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd HH:mm");
            //        cm.Parameters.Add("@LINDID", SqlDbType.VarChar).Value = txtreqno.Text;
            //        cm.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
            //        cm.Parameters.Add("@UID", SqlDbType.VarChar).Value = lbluid.Text;
            //        cm.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
            //        cm.Parameters.Add("@REF", SqlDbType.VarChar).Value = refe.Text.ToString();
            //        cm.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text.ToString();
            //        cm.Parameters.Add("@VALUE", SqlDbType.VarChar).Value = value.Text.ToString();
            //        cm.ExecuteNonQuery();
            //    }
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
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }

    //public void datatable_Resul_item()
    //{
    //    //try
    //    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();

    //    foreach (GridViewRow row in GridView1.Rows)
    //    {

    //        var slno = row.FindControl("lblSlno") as Label;
    //        var value = row.FindControl("txt_value") as TextBox;
    //        var reff = row.FindControl("txtref") as TextBox;
    //        var unit = row.FindControl("txtunit") as TextBox;


    //        //var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
    //        SqlCommand test_cmd = new SqlCommand("UPDATE LABRESULT_TABLE SET REF=@REF,UNIT=@UNIT,VALUE=@VALUE WHERE slno=@slno", con);
    //        test_cmd.Parameters.Add("@slno", SqlDbType.Int).Value = slno.Text.ToString();
    //        // stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
    //        test_cmd.Parameters.Add("@VALUE", SqlDbType.VarChar).Value = value.Text.ToString();
    //        //  stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text.ToString();
    //        test_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = reff.Text.ToString();
    //        test_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text.ToString();
    //        test_cmd.ExecuteNonQuery();
    //        // string message = "alert('Data Create Sucessfully.')";
    //        // ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //    }

    //    con.Close();
    //}
   
    public void datatable_Resul_itemUDATE()
    {
        foreach (GridViewRow row in GridView3.Rows)
        {

            var slno = row.FindControl("lblSlno") as Label;
            var value = row.FindControl("txt_value") as TextBox;
            var reff = row.FindControl("txtref") as TextBox;
            var unit = row.FindControl("txtunit") as TextBox;


            //var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
            SqlCommand test_cmd = new SqlCommand("UPDATE LABRESULT_TABLE SET REF=@REF,UNIT=@UNIT,VALUE=@VALUE WHERE slno=@slno ", con);
            test_cmd.Parameters.Add("@slno", SqlDbType.Int).Value = slno.Text.ToString();

            test_cmd.Parameters.Add("@VALUE", SqlDbType.VarChar).Value = value.Text.ToString();
            test_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = reff.Text.ToString();
            test_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text.ToString();
            test_cmd.ExecuteNonQuery();
            // string message = "alert('Data Create Sucessfully.')";
            // ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    //public void datatable_Amt_Updt()
    //{
    //    //try
    //    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    // var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

    //    SqlCommand Amt_cmd = new SqlCommand("UPDATE LABRES_TABLE SET PRICE=@PRICE,DISCAMT=@DISCAMT,PAIDAMT=@PAIDAMT,DUEAMT=@DUEAMT WHERE ID=@ID", con);

    //    Amt_cmd.ExecuteNonQuery();
    //    con.Close();
    //}
    protected void txtpaidLastamt_TextChanged(object sender, EventArgs e)
    {
        //try
        //{

        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
    //protected void LinkButton2_Click(object sender, EventArgs e)
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
    //    Session["LABID"] = gr.Cells[0].Text;
    //    Response.Redirect("~/LABORATORY/lab_Bill.aspx");
    //    con.Close();
    //}
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,PNAME  AS NAME,PID,TESTTYPE,PTYPE,TESTINDEX,DATE,LINDID from LABRES_TABLE  where ID='" + slno + "'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                TXTID.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                txtname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                txtopdno.Text = Ds.Tables[0].Rows[0]["PID"].ToString();
                txtinvdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy HH:mm");
                txtreqno.Text = Ds.Tables[0].Rows[0]["LINDID"].ToString();
            }

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select B.INVID AS ID,C.NAME,B.INV AS INV,C.REF AS RANGE,C.UNIT from TBLRECLAB A,TBLRECLABITEM B,TEST_COMPONENT_TABLE C,TEST_NAME_TABLE D where B.CHKSEL='1' AND A.ID=B.ID AND C.ID=D.ID  AND B.INVID=C.slno AND A.ID='" + lblsession.Text + "'", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView3.DataSource = Ds1;
                GridView3.DataKeyNames = new string[] { "ID" };
                GridView3.DataBind();
            }
            #region oldcode
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //SqlCommand C = new SqlCommand("select ID,PNAME  AS NAME,PID,TESTTYPE,PTYPE,TESTINDEX,DATE,LINDID from LABRES_TABLE  where ID='" + slno + "'", con);
            //dr = C.ExecuteReader();
            //if (dr.Read())
            //{
            //    btncreate.Visible = false;
            //    btnupdate.Visible = true;
            //    TXTID.Text = dr["ID"].ToString();
            //    txtname.Text = dr["NAME"].ToString();
            //    txtopdno.Text = dr["PID"].ToString();
            //    txtinvdate.Text = Convert.ToDateTime(dr["DATE"]).ToString("dd-MM-yyyy HH:mm");
            //    txtreqno.Text = dr["LINDID"].ToString();
            //}
            //dr.Close();

            //SqlDataAdapter da2 = new SqlDataAdapter("select B.INVID AS ID,C.NAME,B.INV AS INV,C.REF AS RANGE,C.UNIT from TBLRECLAB A,TBLRECLABITEM B,TEST_COMPONENT_TABLE C,TEST_NAME_TABLE D where B.CHKSEL='1' AND A.ID=B.ID AND C.ID=D.ID  AND B.INVID=C.slno AND A.ID='" + lblsession.Text + "'", con);
            //DataTable dt2 = new DataTable();
            //da2.Fill(dt2);
            //GridView1.DataSource = null;
            //GridView1.DataBind();
            //GridView3.DataSource = dt2;
            //GridView3.DataKeyNames = new string[] { "ID" };
            //GridView3.DataBind();

            //btndelete.Visible = true;
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,PID,PNAME AS NAME,DATE from LABRES_TABLE WHERE Branch_ID=" + Session["Branch"] + "  ORDER BY ID DESC", false, false);
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
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            foreach (GridViewRow row in GridView3.Rows)
            {

                var slno = row.FindControl("lblSlno") as Label;
                var value = row.FindControl("txt_value") as TextBox;
                var refe = row.FindControl("txtref") as TextBox;
                var unit = row.FindControl("txtunit") as TextBox;


                SqlParameter[] SQL_PARAMS = new SqlParameter[5];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@slno", SqlDbType.Int, 0, TXTID.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@REF", SqlDbType.VarChar, 500, refe.Text.ToString());
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@VALUE", SqlDbType.VarChar, 500, value.Text.ToString());
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unit.Text.ToString());

                OBJ_METHOD.ExecuteProceedure("USP_TESTRESULT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                    clearcontrol();
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //datatable_Resul_itemUDATE();
            ////datatable_Amt_Updt();
            ////Session["LABID"] = droptesttype.Text;
            //con.Close();
            //string message = "alert('Data Updated Sucessfully.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
    }
    public void clearcontrol()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ID"), new DataColumn("INV"), new DataColumn("PRICE") });
            ViewState["ITEM"] = dt;
            this.BindGrid();
            binddata();
            txtinvdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            btncreate.Visible = true;
            btndelete.Visible = false;
            btnupdate.Visible = false;
        }
        catch (Exception ex)
        {
            Response.Redirect("~/LABORATORY/lab_view_recuisition.aspx");
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, TXTID.Text);

            OBJ_METHOD.ExecuteProceedure("USP_TESTRESULT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearcontrol();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //SqlDataAdapter da = new SqlDataAdapter("delete from LABRESULT_TABLE where ID='" + TXTID.Text + "'", con);
            //DataSet d = new DataSet();
            //da.Fill(d);
            //SqlDataAdapter da1 = new SqlDataAdapter("delete from LABRES_TABLE where ID='" + TXTID.Text + "'", con);
            //DataSet d1 = new DataSet();
            //da1.Fill(d1);
            //SqlDataAdapter da2 = new SqlDataAdapter("delete from LAB_WIDALRESULT_TABLE where ID='" + TXTID.Text + "'", con);
            //DataSet ds2 = new DataSet();
            //da2.Fill(ds2);
            //con.Close();
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