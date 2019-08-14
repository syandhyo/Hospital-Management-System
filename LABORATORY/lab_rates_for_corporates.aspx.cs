using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class LABORATORY_lab_rates_for_corporates : System.Web.UI.Page
{
    string num1 = "PR000";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    SqlCommand com, cmd, cmd1, cmd2;
    SqlDataReader dr;
    SqlDataAdapter da, da1;
    DataTable dt;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = " select ID from dbo.PATHO_CORPO_CATA";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("PC{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;


        dr.Close();
        con.Close();
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

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BIND_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("LAB_CORPO_RATES_SELDEL", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.DataBind();
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CORPO");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("LAB_CORPO_RATES_SELDEL", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                Ddlincrnce.DataSource = Ds1;
                Ddlincrnce.DataTextField = "CNAME";
                Ddlincrnce.DataValueField = "ID";
                Ddlincrnce.DataBind();
                Ddlincrnce.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TEST");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("LAB_CORPO_RATES_SELDEL", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                ddltesttype.DataSource = Ds2;
                ddltesttype.DataTextField = "NAME";
                ddltesttype.DataValueField = "ID";
                ddltesttype.DataBind();
                ddltesttype.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            #region old code
            //using (SqlCommand cmd1 = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND_PAGE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd"); ;
            //    cmd1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //    //SqlDataAdapter Adp = new SqlDataAdapter("select * from dbo.PATHO_CORPO_CATA order by SLNO desc", con);
            //    DataTable Dt = new DataTable();
            //    Adp.Fill(Dt);
            //    GridView2.DataSource = Dt;
            //    GridView2.DataBind();
            //}
            //using (SqlCommand cmd = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CORPO";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd"); ;
            //    cmd.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da = new SqlDataAdapter("select * from [dbo].[Corporate_Table] where ISACTIVE='true'", con);
            //    DataTable ds = new DataTable();
            //    da.Fill(ds);
            //    Ddlincrnce.DataSource = ds;
            //    Ddlincrnce.DataTextField = "CNAME";
            //    Ddlincrnce.DataValueField = "ID";
            //    Ddlincrnce.DataBind();
            //    Ddlincrnce.Items.Insert(0, new ListItem("Please Select", "0"));
            //}
            //using (SqlCommand cmd1 = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEST";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd"); ;
            //    cmd1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //    //SqlDataAdapter da1 = new SqlDataAdapter("select * from [dbo].[TEST_CATEGORY_TABLE]", con);
            //    DataTable ds1 = new DataTable();
            //    da1.Fill(ds1);
            //    ddltesttype.DataSource = ds1;
            //    ddltesttype.DataTextField = "NAME";
            //    ddltesttype.DataValueField = "ID";
            //    ddltesttype.DataBind();
            //    ddltesttype.Items.Insert(0, new ListItem("Please Select", "0"));
            //}
            #endregion oldcode
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
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
        lblorgid.Text = Session["ORGID"].ToString();
        if (IsPostBack != true)
        {
            binddata();
        }
       
    }
    protected void ddltesttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddltesttype.SelectedIndex != 0)
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DROP");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, ddltesttype.SelectedValue.ToString());

                DataSet Ds = OBJ_METHOD.Get_DataSet("LAB_CORPO_RATES_SELDEL", false, true, SQL_PARAMS);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    GridView1.DataSource = Ds;
                    GridView1.DataKeyNames = new string[] { "ID" };
                    GridView1.DataBind();
                    Txtdate.Enabled = true;
                }
                #region old code
                //using (SqlCommand cmd1 = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
                //{
                //    cmd1.CommandType = CommandType.StoredProcedure;
                //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROP";
                //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = ddltesttype.SelectedValue.ToString();
                //    cmd1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd"); ;
                //    cmd1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
                //    cmd1.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = "";
                //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                //    DataTable dt = new DataTable();
                //    //SqlDataAdapter da = new SqlDataAdapter("select A.slno AS ID,A.ID AS slno ,A.NAME AS NAME,A.INV AS INV,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND B.ID='" + ddltesttype.SelectedValue.ToString() + "'", con);
                //    da.Fill(dt);
                //    GridView1.DataSource = null;
                //    GridView1.DataBind();
                //    GridView1.DataSource = dt;
                //    //GridView1.SelectedIndex = 0;
                //    GridView1.DataKeyNames = new string[] { "ID" };
                //    GridView1.DataBind();
                //}
                #endregion
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
            if (Ddlincrnce.SelectedIndex == 0)
            {
                string message = "alert('Please Select The Corporate Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (ddltesttype.SelectedIndex == 0)
            {
                string message = "alert('Please Select Test Type.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Txtdate.Text == "")
            {
                string message = "alert('Please Select The Apply Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            int chkedcounter = 0;
            int correctinput = 0;
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[8];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, Ddlincrnce.SelectedItem.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@CATAGORY", SqlDbType.VarChar, 500, ddltesttype.SelectedItem.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, DateTime.Now.ToString("yyyy-MM-dd"));

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@APPLY_DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(Txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("LAB_CORPO_INSRTUPD", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
               foreach (GridViewRow row in GridView1.Rows)
               {
                   var id = row.FindControl("lblid") as Label;
                   var name = row.FindControl("lblname") as Label;
                   var inv = row.FindControl("lblinv") as Label;
                   var rnge = row.FindControl("lblrange") as Label;
                   var unt = row.FindControl("Label1") as Label;
                   var AMT = row.FindControl("txtprice") as TextBox;
                   {
                       if (AMT.Text == "")
                       {
                           AMT.Text = "0";
                       }
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[12];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERTGRID");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, id.Text.ToString());
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, inv.Text.ToString());
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@REF", SqlDbType.VarChar, 500, rnge.Text.ToString());
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unt.Text.ToString());

                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, name.Text.ToString());
                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, Convert.ToDecimal(AMT.Text).ToString());
                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@C_ID", SqlDbType.VarChar, 500, Ddlincrnce.SelectedValue);
                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@CID", SqlDbType.VarChar, 500, TXTID.Text);

                        OBJ_METHOD.ExecuteProceedure("LAB_CORPO_INSRTUPD", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

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
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                }
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region old code
            //    foreach (GridViewRow row in GridView1.Rows)
            //    {
            //        var id = row.FindControl("lblid") as Label;
            //        var name = row.FindControl("lblname") as Label;
            //        var inv = row.FindControl("lblinv") as Label;
            //        var rnge = row.FindControl("lblrange") as Label;
            //        var unt = row.FindControl("Label1") as Label;
            //        var AMT = row.FindControl("txtprice") as TextBox;
            //        if (AMT.Text == "")
            //        {
            //            AMT.Text = "0";
            //        }
            //        {
            //            //var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
            //            using (SqlCommand cmd = new SqlCommand("LAB_CORPO_INSRTUPD", con))
            //            {
            //                cmd.CommandType = CommandType.StoredProcedure;
            //                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //                //SqlCommand cmd = new SqlCommand("insert into TEST_COMPONENT_TABLE (ID,ORGID,INV,REF,UNIT,NAME,PRICE,C_ID,CID) values (@ID,@ORGID,@INV,@REF,@UNIT,@NAME,@PRICE,@C_ID,@CID)", con);
            //                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id.Text.ToString();
            //                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //                cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = inv.Text.ToString();
            //                cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = rnge.Text.ToString();

            //                cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unt.Text.ToString();
            //                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = name.Text.ToString();
            //                cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = Convert.ToDecimal(AMT.Text).ToString();
            //                cmd.Parameters.Add("@C_ID", SqlDbType.VarChar).Value = Ddlincrnce.SelectedValue;
            //                cmd.Parameters.Add("@CID", SqlDbType.VarChar).Value = TXTID.Text;

            //                cmd.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = Ddlincrnce.SelectedItem.Text;
            //                cmd.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = ddltesttype.SelectedItem.Text;
            //                cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //                cmd.Parameters.Add("@APPLY_DATE", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //                cmd.ExecuteNonQuery();
            //            }

            //        }
            //    }
            //    using (SqlCommand cmd1 = new SqlCommand("LAB_CORPO_INSRTUPD", con))
            //    {
            //        cmd1.CommandType = CommandType.StoredProcedure;
            //        cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT_UPDATE";
            //        cmd1.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //        cmd1.Parameters.Add("@REF", SqlDbType.VarChar).Value = "";
            //        cmd1.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            //        cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //        cmd1.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = Convert.ToDecimal(0).ToString();
            //        cmd1.Parameters.Add("@C_ID", SqlDbType.VarChar).Value = "";
            //        cmd1.Parameters.Add("@CID", SqlDbType.VarChar).Value = "";
            //        cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //        cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        cmd1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = Ddlincrnce.SelectedItem.Text;
            //        cmd1.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = ddltesttype.SelectedItem.Text;
            //        cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //        cmd1.Parameters.Add("@APPLY_DATE", SqlDbType.Date).Value = Convert.ToDateTime(Txtdate.Text).ToString("dd-MM-yy");
            //        cmd1.ExecuteNonQuery();
            //    }
            //    dr.Close();
            //    string message1 = "alert('Successfully Saved.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            //    con.Close();
            //}
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not created.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    public void clearcontrol()
    {
        Ddlincrnce.SelectedIndex = 0;
        ddltesttype.SelectedIndex = 0;
        Txtdate.Text = "";
        Txtdate.Enabled = false;
        btncreate.Visible = true;
        btndelete.Visible = false;
        btnupdate.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView3.DataSource = null;
        GridView3.DataBind();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (Txtdate.Text == "")
            {
                string message = "alert('Please Select The Apply Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            int chkedcounter = 0;
            int correctinput = 0;
            
            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, Ddlincrnce.SelectedItem.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@CATAGORY", SqlDbType.VarChar, 500, ddltesttype.SelectedItem.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);

            SQL_PARAMS[4] = OBJ_METHOD.createParams("@APPLY_DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(Txtdate.Text).ToString("yyyy-MM-dd"));

            OBJ_METHOD.ExecuteProceedure("LAB_CORPO_INSRTUPD", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE_UPDATE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                OBJ_METHOD.ExecuteProceedure("LAB_CORPO_RATES_SELDEL", "", "", SqlDbType.VarChar, SQL_PARAMS);

                if (OBJ_METHOD._RESULT > 0)
                {
                    foreach (GridViewRow row in GridView3.Rows)
                    {
                        var id = row.FindControl("lblid1") as Label;
                        var name = row.FindControl("lblname1") as Label;
                        var inv = row.FindControl("lblinv1") as Label;
                        var rnge = row.FindControl("lblrange1") as Label;
                        var unt = row.FindControl("Label2") as Label;
                        var AMT = row.FindControl("txtprice1") as TextBox;
                        {
                            if (AMT.Text == "")
                            {
                                AMT.Text = "0";
                            }
                            chkedcounter++;
                            SQL_PARAMS = new SqlParameter[12];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATEGRID");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, id.Text.ToString());
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, inv.Text.ToString());
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@REF", SqlDbType.VarChar, 500, rnge.Text.ToString());
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unt.Text.ToString());

                            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                            SQL_PARAMS[8] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, name.Text.ToString());
                            SQL_PARAMS[9] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, Convert.ToDecimal(AMT.Text).ToString());
                            SQL_PARAMS[10] = OBJ_METHOD.createParams("@C_ID", SqlDbType.VarChar, 500, Ddlincrnce.SelectedValue);
                            SQL_PARAMS[11] = OBJ_METHOD.createParams("@CID", SqlDbType.VarChar, 500, TXTID.Text);

                            OBJ_METHOD.ExecuteProceedure("LAB_CORPO_INSRTUPD", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

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
                    binddata();
                    clearcontrol();
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                }
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region old code
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cm1 = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
            //{
            //    cm1.CommandType = CommandType.StoredProcedure;
            //    cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE_UPDATE";
            //    cm1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cm1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = Txtdate.Text;
            //    cm1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = Ddlincrnce.SelectedItem.Text;
            //    cm1.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = ddltesttype.SelectedItem.Text;
            //    cm1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    //SqlCommand cm1 = new SqlCommand("delete from dbo.TEST_COMPONENT_TABLE where CID='" + TXTID.Text + "'", con);
            //    cm1.ExecuteNonQuery();
            //}
            //foreach (GridViewRow row in GridView3.Rows)
            //{
            //    var id = row.FindControl("lblid1") as Label;
            //    var name = row.FindControl("lblname1") as Label;
            //    var inv = row.FindControl("lblinv1") as Label;
            //    var rnge = row.FindControl("lblrange1") as Label;
            //    var unt = row.FindControl("Label2") as Label;
            //    var AMT = row.FindControl("txtprice1") as TextBox;
            //    if (AMT.Text == "")
            //    {
            //        AMT.Text = "0";
            //    }
            //    //var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);

            //    using (SqlCommand cmd = new SqlCommand("LAB_CORPO_INSRTUPD", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //        //SqlCommand cmd = new SqlCommand("insert into TEST_COMPONENT_TABLE (ID,ORGID,INV,REF,UNIT,NAME,PRICE,C_ID,CID) values (@ID,@ORGID,@INV,@REF,@UNIT,@NAME,@PRICE,@C_ID,@CID)", con);
            //        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id.Text.ToString();
            //        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = inv.Text.ToString();
            //        cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = rnge.Text.ToString();
            //        cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unt.Text.ToString();
            //        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = name.Text.ToString();
            //        cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = Convert.ToDecimal(AMT.Text).ToString();
            //        cmd.Parameters.Add("@C_ID", SqlDbType.VarChar).Value = Ddlincrnce.SelectedValue;
            //        cmd.Parameters.Add("@CID", SqlDbType.VarChar).Value = TXTID.Text;
            //        cmd.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = Ddlincrnce.SelectedItem.Text;
            //        cmd.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = ddltesttype.SelectedItem.Text;
            //        cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //        cmd.Parameters.Add("@APPLY_DATE", SqlDbType.Date).Value = Convert.ToDateTime(Txtdate.Text).ToString("dd-MM-yy");
            //        cmd.ExecuteNonQuery();
            //    }

            //}
            //using (SqlCommand cmd2 = new SqlCommand("LAB_CORPO_INSRTUPD", con))
            //{
            //    cmd2.CommandType = CommandType.StoredProcedure;
            //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    //SqlCommand cmd = new SqlCommand("insert into TEST_COMPONENT_TABLE (ID,ORGID,INV,REF,UNIT,NAME,PRICE,C_ID,CID) values (@ID,@ORGID,@INV,@REF,@UNIT,@NAME,@PRICE,@C_ID,@CID)", con);
            //    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@REF", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = Convert.ToDecimal(0).ToString();
            //    cmd2.Parameters.Add("@C_ID", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@CID", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = Ddlincrnce.SelectedItem.Text;
            //    cmd2.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = ddltesttype.SelectedItem.Text;
            //    cmd2.Parameters.Add("@DATE", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //    cmd2.Parameters.Add("@APPLY_DATE", SqlDbType.Date).Value = Convert.ToDateTime(Txtdate.Text).ToString("dd-MM-yy");
            //    //SqlCommand cmd2 = new SqlCommand("update PATHO_CORPO_CATA set APPLY_DATE=@APPLY_DATE where ID='" + TXTID.Text + "'", con);
            //    //cmd2.Parameters.Add("@APPLY_DATE", SqlDbType.Date).Value = Txtdate.Text;
            //    cmd2.ExecuteNonQuery();
            //}
            //string message1 = "alert('Successfully Updated.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
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
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("LAB_CORPO_INSRTUPD", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearcontrol();

            }
            //using (SqlCommand cm1 = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
            //{
            //    cm1.CommandType = CommandType.StoredProcedure;
            //    cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cm1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cm1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //    cm1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
            //    cm1.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = "";
            //    cm1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    //SqlCommand cm = new SqlCommand("delete from PATHO_CORPO_CATA where ID='" + TXTID.Text + "'", con);
            //    cm1.ExecuteNonQuery();
            //}
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        binddata();
        clearcontrol();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BIND_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("LAB_CORPO_RATES_SELDEL", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        #region oldcode
        //using (SqlCommand cm1 = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
        //{
        //    cm1.CommandType = CommandType.StoredProcedure;
        //    cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND_PAGE";
        //    cm1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
        //    cm1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd");
        //    cm1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
        //    cm1.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = "";
        //    cm1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
        //    SqlDataAdapter Adp = new SqlDataAdapter(cm1);
        //    //SqlDataAdapter Adp = new SqlDataAdapter("select * from dbo.PATHO_CORPO_CATA order by SLNO desc", con);
        //    DataTable Dt = new DataTable();
        //    Adp.Fill(Dt);
        //    GridView2.DataSource = Dt;
        //    GridView2.PageIndex = e.NewPageIndex;
        //    GridView2.DataKeyNames = new string[] { "ID" };
        //    GridView2.DataBind();
        //}
        //con.Close();
        #endregion
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("LAB_CORPO_RATES_SELDEL", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btndelete.Visible = true;
                btnupdate.Visible = true;
                TXTID.Text = slno;
                GridView3.DataSource = Ds;
                GridView3.DataBind();
            }

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT1");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("LAB_CORPO_RATES_SELDEL", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                Ddlincrnce.SelectedItem.Text = Ds1.Tables[0].Rows[0]["CORPORATE"].ToString();
                ddltesttype.SelectedItem.Text = Ds1.Tables[0].Rows[0]["CATAGORY"].ToString();
                Txtdate.Text = Convert.ToDateTime(Ds1.Tables[0].Rows[0]["APPLY_DATE"]).ToString("dd-MM-yyyy");
                Txtdate.Enabled = true;
            }
            SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT2");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, ddltesttype.SelectedItem.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("LAB_CORPO_RATES_SELDEL", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                ddltesttype.DataSource = Ds2;
                ddltesttype.DataTextField = "NAME";
                ddltesttype.DataValueField = "ID";
                ddltesttype.DataBind();
            }
            SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT3");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, Ddlincrnce.SelectedItem.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds3 = OBJ_METHOD.Get_DataSet("LAB_CORPO_RATES_SELDEL", false, true, SQL_PARAMS);
            if (Ds3.Tables[0].Rows.Count > 0)
            {
                Ddlincrnce.DataSource = Ds3;
                Ddlincrnce.DataTextField = "CNAME";
                Ddlincrnce.DataValueField = "ID";
                Ddlincrnce.DataBind();
            }
            #region oldcode
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cm1 = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
            //{
            //    cm1.CommandType = CommandType.StoredProcedure;
            //    cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT";
            //    cm1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cm1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //    cm1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
            //    cm1.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = "";
            //    cm1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da = new SqlDataAdapter(cm1);
            //    //SqlDataAdapter da = new SqlDataAdapter("  select * from TEST_COMPONENT_TABLE where CID='" + slno + "'", con);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    btncreate.Visible = false;
            //    btndelete.Visible = true;
            //    btnupdate.Visible = true;
            //    TXTID.Text = ds.Tables[0].Rows[0]["CID"].ToString();
            //    GridView3.DataSource = ds;
            //    GridView3.DataBind();
            //}
            //GridView1.Visible = false;
            //using (SqlCommand cm = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT1";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cm.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    //SqlDataAdapter Adp = new SqlDataAdapter(cm);
            //    //SqlCommand Cm = new SqlCommand("select * from PATHO_CORPO_CATA where ID='" + slno + "'", con);
            //    dr = cm.ExecuteReader();
            //    if (dr.Read() == true)
            //    {
            //        Ddlincrnce.SelectedItem.Text = dr["CORPORATE"].ToString();
            //        ddltesttype.SelectedItem.Text = dr["CATAGORY"].ToString();
            //        Txtdate.Text = dr["APPLY_DATE"].ToString();
            //    }
            //}
            //dr.Close();

            //Txtdate.Enabled = true;
            //using (SqlCommand Cm = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
            //{
            //    Cm.CommandType = CommandType.StoredProcedure;
            //    Cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT2";
            //    Cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    Cm.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //    Cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
            //    Cm.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = "";
            //    Cm.Parameters.Add("@NAME", SqlDbType.VarChar).Value = ddltesttype.SelectedItem.Text;
            //    SqlDataAdapter da1 = new SqlDataAdapter(Cm);
            //    //SqlDataAdapter da1 = new SqlDataAdapter("select * from [dbo].[TEST_CATEGORY_TABLE] where NAME='" + ddltesttype.SelectedItem.Text + "'", con);
            //    DataTable ds1 = new DataTable();
            //    da1.Fill(ds1);
            //    ddltesttype.DataSource = ds1;
            //    ddltesttype.DataTextField = "NAME";
            //    ddltesttype.DataValueField = "ID";
            //    ddltesttype.DataBind();
            //}
            //using (SqlCommand Cm1 = new SqlCommand("LAB_CORPO_RATES_SELDEL", con))
            //{
            //    Cm1.CommandType = CommandType.StoredProcedure;
            //    Cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT3";
            //    Cm1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    Cm1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //    Cm1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
            //    Cm1.Parameters.Add("@CATAGORY", SqlDbType.VarChar).Value = "";
            //    Cm1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = Ddlincrnce.SelectedItem.Text;
            //    SqlDataAdapter da2 = new SqlDataAdapter(Cm1);
            //    //SqlDataAdapter da2 = new SqlDataAdapter("select * from [dbo].[Corporate_Table] where CNAME='" + Ddlincrnce.SelectedItem.Text + "'", con);
            //    DataTable ds2 = new DataTable();
            //    da2.Fill(ds2);
            //    Ddlincrnce.DataSource = ds2;
            //    Ddlincrnce.DataTextField = "CNAME";
            //    Ddlincrnce.DataValueField = "ID";
            //    Ddlincrnce.DataBind();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}