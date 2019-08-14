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

public partial class OT_ot_indent_to_pharmacy : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("RADIO_INDENT_PHARMA", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SEARCH_TEXT";
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = prefix;

                //cmd.Parameters.AddWithValue("@SearchText", prefix);
                //cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["NAME"]));
                    }
                }

            }
            con.Close();
        }
        return customers.ToArray();
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from INDENT_TO_PHARMACY";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        txtindentno.Text = "IND-" + num1 + "-" + lblfyear.Text;
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

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DEPT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RADIO_INDENT_PHARMA", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                dropdept.DataSource = Ds2;
                dropdept.DataTextField = "DeptName";
                dropdept.DataValueField = "id";
                dropdept.DataBind();
                dropdept.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_INDENT_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RADIO_INDENT_PHARMA", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds1;
                GridView1.DataKeyNames = new string[] { "INDENT_NO" };
                GridView1.DataBind();
            }
            //using (SqlCommand cmd = new SqlCommand("RADIO_INDENT_PHARMA", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DEPT";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter Adp2 = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter Adp2 = new SqlDataAdapter("select * from tblDepartment", con);
            //    DataTable Dt2 = new DataTable();
            //    Adp2.Fill(Dt2);
            //    dropdept.DataSource = Dt2;
            //    dropdept.DataTextField = "DeptName";
            //    dropdept.DataValueField = "id";
            //    dropdept.DataBind();
            //    dropdept.Items.Insert(0, "Please Select");
            //}

            //using (SqlCommand cmd1 = new SqlCommand("RADIO_INDENT_PHARMA", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_INDENT_PAGE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //    //SqlDataAdapter Adp = new SqlDataAdapter("select * from INDENT_TO_PHARMACY order by ID desc", con);
            //    DataTable Dt = new DataTable();
            //    Adp.Fill(Dt);
            //    GridView1.DataSource = Dt;
            //    GridView1.DataBind();
            //}
            //con.Close();
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
        if (!IsPostBack)
        {
            Session["ITEM"] = null;
            binddata();
            bindTGrid();
            auto();
        }
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (dropdept.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtipno.Text == "")
            {
                string message = "alert('* Please Select IP Number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtenterby.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (grdMaterial.Rows.Count <= 0)
            {

                string message = "alert('*Add item.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtipno.Text != "")
            {
                DataSet Ds = OBJ_METHOD.Get_DataSet("select * from BED_TABLE where VN='" + txtipno.Text + "' and Branch_ID = " + Session["Branch"] + "", false, false);

                if (Ds.Tables[0].Rows.Count < 0)
                {
                    string message = "alert('No Record Found.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            int chkedcounter = 0;
            int correctinput = 0;

            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@INDENT_NO", SqlDbType.VarChar, 500, txtindentno.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DEPARTMENT", SqlDbType.VarChar, 500, dropdept.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ENTER_BY", SqlDbType.VarChar, 500, txtenterby.Text);

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@AUTHORISED_BY", SqlDbType.VarChar, 500, txtauthorised.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 500, txtipno.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("USP_INDENT_PHARMACY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                foreach (GridViewRow gv1 in grdMaterial.Rows)
                {
                    //var lbname = (gv1.FindControl("chkRow") as CheckBox);
                    var nameGr = (gv1.FindControl("lbl_Name") as Label);
                    var unitGr = (gv1.FindControl("lbl_Unit") as Label);
                    var quantyGr = (gv1.FindControl("lbl_Qty") as Label);
                    {
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[12];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@INDENT_NO", SqlDbType.VarChar, 500, txtindentno.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@DEPARTMENT", SqlDbType.VarChar, 500, dropdept.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@ENTER_BY", SqlDbType.VarChar, 500, txtenterby.Text);

                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@AUTHORISED_BY", SqlDbType.VarChar, 500, txtauthorised.Text);
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 500, txtipno.Text);
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, nameGr.Text);
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, quantyGr.Text);
                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unitGr.Text);

                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                        OBJ_METHOD.ExecuteProceedure("USP_INDENT_PHARMACY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

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
            //using (SqlCommand cmd = new SqlCommand("USP_INDENT_PHARMACY", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    // cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = txtindentno.Text;
            //    cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    cmd.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = dropdept.SelectedItem.Text;
            //    cmd.Parameters.Add("@ENTER_BY", SqlDbType.VarChar).Value = txtenterby.Text;
            //    cmd.Parameters.Add("@AUTHORISED_BY", SqlDbType.VarChar).Value = txtauthorised.Text;
            //    cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtipno.Text;
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            //    cmd.ExecuteNonQuery();
            //}
            //ItemCM();
            //con.Close();
            //string message1 = "alert('Successfully Created.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
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
    //public void ItemCM()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    foreach (GridViewRow gv1 in grdMaterial.Rows)
    //    {
    //        //var lbname = (gv1.FindControl("chkRow") as CheckBox);
    //        var nameGr = (gv1.FindControl("lbl_Name") as Label);
    //        var unitGr = (gv1.FindControl("lbl_Unit") as Label);
    //        var quantyGr = (gv1.FindControl("lbl_Qty") as Label);

    //        using (SqlCommand cmd1 = new SqlCommand("USP_INDENT_PHARMACY", con))
    //        {
    //            cmd1.CommandType = CommandType.StoredProcedure;
    //            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
    //            // cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
    //            cmd1.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = txtindentno.Text;
    //            cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
    //            cmd1.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = dropdept.SelectedItem.Text;

    //            cmd1.Parameters.Add("@ENTER_BY", SqlDbType.VarChar).Value = txtenterby.Text;
    //            cmd1.Parameters.Add("@AUTHORISED_BY", SqlDbType.VarChar).Value = txtauthorised.Text;
    //            cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtipno.Text;
    //            cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameGr.Text;
    //            cmd1.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyGr.Text;
    //            cmd1.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitGr.Text;

    //            cmd1.ExecuteNonQuery();
    //        }
    //    }
    //    con.Close();
    //}
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            // Validation
            if (dropdept.SelectedItem.Text == "" && dropdept.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtauthorised.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtenterby.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (grdMaterial.Rows.Count <= 0)
            {

                string message = "alert('*Add item.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtipno.Text == "")
            {
                string message = "alert('Please Enter IPD Number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtipno.Text != "")
            {
                DataSet Ds = OBJ_METHOD.Get_DataSet("select * from BED_TABLE where VN='" + txtipno.Text + "' and Branch_ID = " + Session["Branch"] + "", false, false);

                if (Ds.Tables[0].Rows.Count < 0)
                {
                    string message = "alert('No Record Found.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                //SqlCommand cmd2 = new SqlCommand("select * from BED_TABLE where VN='" + txtipno.Text + "'", con);
                //SqlDataReader dr1 = cmd2.ExecuteReader();
                //if (dr1.Read() == false)
                //{
                //    string message = "alert('Invalid IPD Number.')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //    return;
                //}
                //dr1.Close();
            }
            int chkedcounter = 0;
            int correctinput = 0;
            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@INDENT_NO", SqlDbType.VarChar, 500, txtindentno.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DEPARTMENT", SqlDbType.VarChar, 500, dropdept.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ENTER_BY", SqlDbType.VarChar, 500, txtenterby.Text);

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@AUTHORISED_BY", SqlDbType.VarChar, 500, txtauthorised.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 500, txtipno.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("USP_INDENT_PHARMACY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                
                foreach (GridViewRow gv1 in grdMaterial.Rows)
                {
                    //var lbname = (gv1.FindControl("chkRow") as CheckBox);
                    var nameGr = (gv1.FindControl("lbl_Name") as Label);
                    var unitGr = (gv1.FindControl("lbl_Unit") as Label);
                    var quantyGr = (gv1.FindControl("lbl_Qty") as Label);
                    {
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[12];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDUPDATE");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@INDENT_NO", SqlDbType.VarChar, 500, txtindentno.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@DEPARTMENT", SqlDbType.VarChar, 500, dropdept.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@ENTER_BY", SqlDbType.VarChar, 500, txtenterby.Text);

                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@AUTHORISED_BY", SqlDbType.VarChar, 500, txtauthorised.Text);
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 500, txtipno.Text);
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, nameGr.Text);
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, quantyGr.Text);
                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unitGr.Text);

                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                        OBJ_METHOD.ExecuteProceedure("USP_INDENT_PHARMACY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

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
        binddata();
        clearfield();
    }
    public void clearfield()
    {
        txtauthorised.Text = txtenterby.Text = txtindentno.Text = txtipno.Text = "";
        txtname.Text = "";
        txtqty.Text = "";
        dropdept.SelectedIndex = 0;
        grdMaterial.DataSource = null;
        grdMaterial.DataBind();
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('*Add item.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtqty.Text == "")
            {
                string message = "alert('*Add Qty.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtname.Text.Trim(), txtUnit.Text.Trim(), txtqty.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();

            con.Close();
            txtname.Text = "";
            txtqty.Text = "";
            txtUnit.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //btncreate.Visible = true;
        //btncancel.Visible = true;
    }
    protected void grdMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = (DataTable)Session["ITEM"];
        GridViewRow row = (GridViewRow)grdMaterial.Rows[e.RowIndex];

        Label name = (Label)row.FindControl("lbl_Name");
        Label unit = (Label)row.FindControl("lbl_Unit");
        Label quantity = (Label)row.FindControl("lbl_Qty");

        string name1 = name.Text.ToString();
        string unit1 = unit.Text.ToString();
        string quantity1 = quantity.Text.ToString();

        dt.Rows[index].Delete();
        Session["ITEM"] = dt;
        //  lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();

        this.BindGrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["INDENT_NO"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@INDENT_NO", SqlDbType.VarChar, 500, slno);

            OBJ_METHOD.ExecuteProceedure("USP_INDENT_PHARMACY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";


            //using (SqlCommand cmd1 = new SqlCommand("RADIO_INDENT_PHARMA", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //    //SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //    //SqlCommand cm = new SqlCommand("delete from INDENT_TO_PHARMACY where INDENT_NO='" + slno + "'", con);
            //    cmd1.ExecuteNonQuery();
            //    binddata();
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
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["INDENT_NO"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_1EVENT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet DS = OBJ_METHOD.Get_DataSet("RADIO_INDENT_PHARMA", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                lblEditgrd.Text = slno.ToString();
                txtindentno.Text = DS.Tables[0].Rows[0]["INDENT_NO"].ToString(); ;
                txtid.Text = DS.Tables[0].Rows[0]["INDENT_NO"].ToString();
                txtdate.Text = DS.Tables[0].Rows[0]["DATE"].ToString();
                dropdept.Text = DS.Tables[0].Rows[0]["DEPARTMENT"].ToString();
                txtipno.Text = DS.Tables[0].Rows[0]["IPNO"].ToString();
                txtenterby.Text = DS.Tables[0].Rows[0]["ENTER_BY"].ToString();
                txtauthorised.Text = DS.Tables[0].Rows[0]["AUTHORISED_BY"].ToString();
            }

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_2EVENT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RADIO_INDENT_PHARMA", false, true, SQL_PARAMS);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                grdMaterial.DataSource = DS1.Tables["Table"];
                grdMaterial.DataBind();

                DataTable dt = DS1.Tables["Table"];
                ViewState["ITEM"] = dt;
            }
            //using (SqlCommand cmd1 = new SqlCommand("RADIO_INDENT_PHARMA", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_1EVENT";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //    //SqlCommand com = new SqlCommand("SELECT * from INDENT_TO_PHARMACY where INDENT_NO='" + slno + "'", con);
            //    dr = cmd1.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        lblEditgrd.Text = slno.ToString();
            //        txtid.Text = dr["INDENT_NO"].ToString();
            //        txtdate.Text = dr["DATE"].ToString();
            //        dropdept.SelectedItem.Text = dr["DEPARTMENT"].ToString();
            //        txtipno.Text = dr["IPNO"].ToString();
            //        txtenterby.Text = dr["ENTER_BY"].ToString();
            //        txtauthorised.Text = dr["AUTHORISED_BY"].ToString();
            //    }
            //    dr.Close();
            //}
            //using (SqlCommand cmd1 = new SqlCommand("RADIO_INDENT_PHARMA", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2EVENT";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //    da = new SqlDataAdapter(cmd1);
            //    //da = new SqlDataAdapter("select NAME,UNIT,QTY FROM INDENT_TO_PHARMACY_ITEM where INDENT_NO='" + slno + "'", con);
            //    DataSet ds2 = new DataSet();
            //    da.Fill(ds2);
            //    grdMaterial.DataSource = ds2.Tables["Table"];
            //    grdMaterial.DataBind();

            //    DataTable dt = ds2.Tables["Table"];
            //    ViewState["ITEM"] = dt;
            //}
            //con.Close();
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_INDENT_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RADIO_INDENT_PHARMA", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds1;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "INDENT_NO" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
}