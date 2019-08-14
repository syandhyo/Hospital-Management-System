using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class RADIOLOGY_radiology_stock_reconcilation : System.Web.UI.Page
{
    string num1 = "000";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr;
    SqlDataAdapter da, da1;
    DataTable dt;
    DataMathods OBJ_METHOD = new DataMathods();
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = " select slno as ID from TBL_STOCKRECONLAB";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }

        TXTID.Text = "STC" + num1;

        dr.Close();
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

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_2DEPT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RADIO_STOCK_RECON", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                Ddldept.DataSource = Ds2;
                Ddldept.DataTextField = "DeptName";
                Ddldept.DataValueField = "id";
                Ddldept.DataBind();
            }
            SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_GRID_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, Ddldept.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_STOCK_RECON", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = Ds;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            
            #region old code
            //using (SqlCommand cmd4 = new SqlCommand("RADIO_STOCK_RECON", con))
            //{
            //    cmd4.CommandType = CommandType.StoredProcedure;
            //    cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRID_PAGE";
            //    cmd4.Parameters.Add("@ID", SqlDbType.VarChar).Value = Ddldept.SelectedValue;
            //    cmd4.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd4);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    GridView1.SelectedIndex = 0;
            //    GridView1.DataSource = dt;
            //    GridView1.DataKeyNames = new string[] { "ID" };
            //    GridView1.DataBind();
            //}
            //using (SqlCommand cmd4 = new SqlCommand("RADIO_STOCK_RECON", con))
            //{
            //    cmd4.CommandType = CommandType.StoredProcedure;
            //    cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2DEPT";
            //    cmd4.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd4.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd4);
            //    DataTable ds = new DataTable();
            //    da.Fill(ds);
            //    Ddldept.DataSource = ds;
            //    Ddldept.DataTextField = "DeptName";
            //    Ddldept.DataValueField = "id";
            //    Ddldept.DataBind();
            //}
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
       


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
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        binddata();
        if (!IsPostBack)
        {
            dropdownbind();
        }
        
    }
    public void dropdownbind()
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DEPT_ID");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, Ddldept.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RADIO_STOCK_RECON", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                Ddlitem.DataSource = Ds1;
                Ddlitem.DataTextField = "ITEMNAME";
                Ddlitem.DataValueField = "ITEMNAME";
                Ddlitem.DataBind();
                Ddlitem.Items.Insert(0, new ListItem("Please Select", "0"));

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    public void clearfield()
    {
        dropdownbind();
        txt_des.Text = txtquant.Text = Txt_minus.Text = TXTID.Text = "";
        Ddldept.SelectedIndex = Ddlitem.SelectedIndex = 0;
        lblquant.Text = "";
        btndelete.Visible = false;
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (Ddlitem.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Item..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtquant.Text == "" || txtquant.Text == "0")
            {
                string message = "alert('Please!! Select The Quantity..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txt_des.Text == "")
            {
                string message = "alert('Please!! Select The Reason..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (lblquant.Text == "0.00")
            {
                string message = "alert('There Is No Stock..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(txtquant.Text) >= Convert.ToDecimal(lblquant.Text))
            {
                string message = "alert('Insufficient Stock')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, Txt_minus.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, Ddlitem.SelectedItem.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("LAB_STOCK_RECONCILIATION", "", "", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[11];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(lbldate.Text).ToString("yyyy-MM-dd"));
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@ITEM", SqlDbType.VarChar, 500, Ddlitem.SelectedItem.Text);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@DESCRIPTN", SqlDbType.VarChar, 500, txt_des.Text.ToUpper());

                SQL_PARAMS[5] = OBJ_METHOD.createParams("@QUANTITY", SqlDbType.Decimal, 0, txtquant.Text);
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@USERID", SqlDbType.VarChar, 500, lblid.Text);
                SQL_PARAMS[10] = OBJ_METHOD.createParams("@DEPTID", SqlDbType.Int, 0, Convert.ToInt32(Ddldept.SelectedValue).ToString());

                OBJ_METHOD.ExecuteProceedure("USP_STOCKRECLAB", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");

                    clearfield();
                    binddata();
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
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
        #region old code
        //    auto();
        //    con.Open();
        //    using (SqlCommand cmd = new SqlCommand("USP_STOCKRECLAB", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

        //        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
        //        cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(lbldate.Text).ToString("yyyy-MM-dd");
        //        cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = Ddlitem.SelectedItem.Text;
        //        cmd.Parameters.Add("@DESCRIPTN", SqlDbType.VarChar).Value = txt_des.Text;
        //        cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = txtquant.Text;
        //        cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
        //        cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;
        //        cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = Convert.ToInt32(Ddldept.SelectedValue).ToString();
        //        cmd.ExecuteNonQuery();
        //    }
        //    binddata();
        //    dr.Close();
        //    con.Close();

        //    string message1 = "alert('Successfully Inserted.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        //    con.Open();
        //    using (SqlCommand cmd = new SqlCommand("LAB_STOCK_RECONCILIATION", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
        //        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = Txt_minus.Text;
        //        cmd.Parameters.Add("@DEPTID", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = Ddlitem.SelectedItem.Text;
                
        //        cmd.ExecuteNonQuery();
        //    }
        //    dr.Close();
        //    con.Close();
        //    clearfield();
        #endregion
    }
    protected void txtquant_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txt_minus.Text = (Convert.ToDouble(lblquant.Text) - Convert.ToDouble(txtquant.Text)).ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Ddlitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Ddlitem.SelectedIndex != 0)
            {
                if (Ddlitem.SelectedIndex != 0)
                {
                    SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ITEMNAME");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, Ddlitem.SelectedItem.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

                    DataSet Ds = OBJ_METHOD.Get_DataSet("LAB_STOCK_RECONCILIATION", false, true, SQL_PARAMS);
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        lblquant.Text = Ds.Tables[0].Rows[0]["STOCK"].ToString();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_GRID_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DEPTID", SqlDbType.VarChar, 500, Ddldept.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_STOCK_RECON", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
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
        #region oldcode
        //con.Open();
        //using (SqlCommand cmd4 = new SqlCommand("RADIO_STOCK_RECON", con))
        //{
        //    cmd4.CommandType = CommandType.StoredProcedure;
        //    cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRID_PAGE";
        //    cmd4.Parameters.Add("@ID", SqlDbType.VarChar).Value = Ddldept.SelectedValue;
        //    cmd4.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "";
        //    SqlDataAdapter da = new SqlDataAdapter(cmd4);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    GridView1.DataSource = dt;
        //    GridView1.PageIndex = e.NewPageIndex;
        //    GridView1.DataKeyNames = new string[] { "ID" };
        //    GridView1.DataBind();
        //    con.Close();
        //}
        #endregion
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtquant.Text == "" || txtquant.Text == "0")
            {
                string message = "alert('Please!! Select The Quantity..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txt_des.Text == "")
            {
                string message = "alert('Please!! Select The Reason..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (lblquant.Text == "0.00")
            {
                string message = "alert('There Is No Stock..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(txtquant.Text) >= Convert.ToDecimal(lblquant.Text))
            {
                string message = "alert('Insufficient Stock')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (lblupdate.Text == txtquant.Text)
            {
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[5];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ITEM", SqlDbType.VarChar, 500, Ddlitem.SelectedItem.Text);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@DESCRIPTN", SqlDbType.VarChar, 500, txt_des.Text.ToUpper());

                SQL_PARAMS1[4] = OBJ_METHOD.createParams("@QUANTITY", SqlDbType.Decimal, 0, txtquant.Text);

                OBJ_METHOD.ExecuteProceedure("USP_STOCKRECLAB", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                    clearfield();
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
            }
            else if (Convert.ToDecimal(lblupdate.Text) > Convert.ToDecimal(txtquant.Text))
            {
                lblupdate1.Text = (Convert.ToDouble(lblupdate.Text) - Convert.ToDouble(txtquant.Text)).ToString();
                SqlParameter[] SQL_PARAMS = new SqlParameter[5];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "EDIT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@STOCK", SqlDbType.Decimal, 0, lblupdate1.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, Ddlitem.SelectedItem.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);

                OBJ_METHOD.ExecuteProceedure("LAB_RECON_NEW", "", "", SqlDbType.VarChar, SQL_PARAMS);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SQL_PARAMS = new SqlParameter[4];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE1");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEM", SqlDbType.VarChar, 500, Ddlitem.SelectedItem.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@DESCRIPTN", SqlDbType.VarChar, 500, txt_des.Text.ToUpper());

                    OBJ_METHOD.ExecuteProceedure("USP_STOCKRECLAB", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        binddata();

                        clearfield();
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                    }
                }

            }
            else
            {
                lblupdate1.Text = (Convert.ToDecimal(txtquant.Text) - Convert.ToDecimal(lblupdate.Text)).ToString();
                SqlParameter[] SQL_PARAMS = new SqlParameter[5];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "CANCEL");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@STOCK", SqlDbType.Decimal, 0, lblupdate1.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, Ddlitem.SelectedItem.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);

                OBJ_METHOD.ExecuteProceedure("LAB_RECON_NEW", "", "", SqlDbType.VarChar, SQL_PARAMS);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SQL_PARAMS = new SqlParameter[4];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE1");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEM", SqlDbType.VarChar, 500, Ddlitem.SelectedItem.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@DESCRIPTN", SqlDbType.VarChar, 500, txt_des.Text.ToUpper());

                    OBJ_METHOD.ExecuteProceedure("USP_STOCKRECLAB", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        binddata();
                        clearfield();
                        
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
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
        #region oldcode
        //con.Open();
            //using (SqlCommand cmd = new SqlCommand("Add_new_stock", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@STCK", SqlDbType.Decimal).Value = Convert.ToDecimal(txtquant.Text).ToString();
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd.Parameters.Add("@DESCR", SqlDbType.VarChar).Value = txt_des.Text;
            //    cmd.ExecuteNonQuery();
            //}

    //        string message1 = "alert('Item Updated Successfully.')";
    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

    //        con.Open();
    //        using (SqlCommand cmd = new SqlCommand("new_release_stock", con))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "ADD";
    //            cmd.Parameters.Add("@STCK", SqlDbType.VarChar).Value = txtquant.Text;
    //            cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = Session["i_id"].ToString();
    //            cmd.ExecuteNonQuery();
        //        }
        #endregion
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@STCK", SqlDbType.Decimal, 0, lblupdate.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, Ddlitem.SelectedItem.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("new_release_stock", "", "", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);

                OBJ_METHOD.ExecuteProceedure("USP_STOCKRECLAB", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                    clearfield();
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
            }
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
        //try
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //    con.Open();
        //    using (SqlCommand cm = new SqlCommand("LAB_STOCK_RECONCILIATION", con))
        //    {
        //        cm.CommandType = CommandType.StoredProcedure;
        //        cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
        //        cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
        //        cm.Parameters.Add("@DEPTID", SqlDbType.VarChar).Value = "";
        //        cm.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "";
        //        cm.ExecuteNonQuery();
        //    }

        //    binddata();
        //    con.Close();

        //    clearfield();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
        //Response.Redirect("stock_reconcilation.aspx");
        #endregion
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_1EVENT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("LAB_STOCK_RECONCILIATION", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btnsubmit.Visible = false;
                btnupdate.Visible = true;
                btndelete.Visible = true;
                btncancel1.Visible = true;
                TXTID.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                lbldate.Text = Ds.Tables[0].Rows[0]["DATE"].ToString();
                Ddlitem.SelectedValue = Ds.Tables[0].Rows[0]["ITEM"].ToString();
                txt_des.Text = Ds.Tables[0].Rows[0]["DESCRIPTN"].ToString();
                txtquant.Text = Ds.Tables[0].Rows[0]["QUANTITY"].ToString();
                lblupdate.Text = Ds.Tables[0].Rows[0]["QUANTITY"].ToString();
            }

            SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ITEMNAME");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, Ddlitem.SelectedItem.Text);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("LAB_STOCK_RECONCILIATION", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                lblevent.Text = Ds2.Tables[0].Rows[0]["STOCK"].ToString();
                lblquant.Text = (Convert.ToDecimal(txtquant.Text) + Convert.ToDecimal(lblevent.Text)).ToString();
            }
            DataSet Ds3 = OBJ_METHOD.Get_DataSet("Select SLNO as id,ITEMNAME from TBL_DEPT_MAT_STOCK where ID=" + Ddldept.SelectedValue + " and Branch_ID = " + Session["Branch"] + " and ITEMNAME='" + Ddlitem.SelectedItem.Text + "'", false, false);
            Ddlitem.DataSource = Ds3;
            Ddlitem.DataTextField = "ITEMNAME";
            Ddlitem.DataValueField = "ITEMNAME";
            Ddlitem.DataBind();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        #region old code
        //using (SqlCommand cmd2 = new SqlCommand("LAB_STOCK_RECONCILIATION", con))
        //{
        //    cmd2.CommandType = CommandType.StoredProcedure;
        //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ITEMNAME";
        //    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
        //    cmd2.Parameters.Add("@DEPTID", SqlDbType.VarChar).Value = Ddldept.SelectedValue;
        //    cmd2.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = Ddlitem.SelectedItem.Text;
        //    dr = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
        //    if (dr.Read() == true)
        //    {
        //        lblquant.Text = dr["STOCK"].ToString();
        //        dr.Close();
        //        con.Close();

        //    }
        //}
        //using (SqlCommand cmd3 = new SqlCommand("LAB_STOCK_RECONCILIATION", con))
        //{
        //    cmd3.CommandType = CommandType.StoredProcedure;
        //    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_3EVENT";
        //    cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = Ddldept.SelectedValue;
        //    cmd3.Parameters.Add("@DEPTID", SqlDbType.VarChar).Value = Ddldept.SelectedValue;
        //    cmd3.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = Ddlitem.SelectedItem.Text;
        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd3);
        //    DataTable ds1 = new DataTable();
        //    da1.Fill(ds1);
        //    Ddlitem.DataSource = ds1;
        //    Ddlitem.DataTextField = "ITEMNAME";
        //    Ddlitem.DataValueField = "id";
        //}
        #endregion
    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        binddata();
        clearfield();
    }
}