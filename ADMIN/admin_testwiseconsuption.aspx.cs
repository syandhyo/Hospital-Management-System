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

public partial class ADMIN_admin_testwiseconsuption : System.Web.UI.Page
{
    string num1 = "SJ000";
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
                cmd.CommandText = "select DISTINCT ITEMNAME from TBL_DEPT_MAT_STOCK where ITEMNAME like '%'+@SearchText+'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
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

        if (!IsPostBack)
        {
            binddata();
            bindTGrid();
        }
        con.Close();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
    }
    public void auto()
    {
       // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
       // con.Open();
       // //string qry1 = "select ID from TEST_WISE_CONSUMPTION";
       // string qry1 = "select max(SERIAL_NO) from TEST_WISE_CONSUMPTION";
       // com = new SqlCommand(qry1, con);
       //// dr = null;
       // dr = com.ExecuteReader();

       // while (dr.Read())
       // {
       //     num1 = dr["SERIAL_NO"].ToString();
       // }
       // num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
       // txtserialno.Text = num1;

       // dr.Close();
       // con.Close();


        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //string qry1 = "select max(SERIAL_NO) as ID from TEST_WISE_CONSUMPTION";

        //com = new SqlCommand(qry1, con);
        //dr = null;
        //dr = com.ExecuteReader();
        //string str1 = "1";
        //if (dr.Read() && dr["ID"].ToString() != "")
        //{
        //    // num1 = dr["ID"].ToString();
        //    //------------------
        //    num1 = dr["ID"].ToString();
        //    // string str="0";
        //    string str = num1.Substring(0, num1.Length - 10);//delete last 10 record
        //    string d = str.Substring(4);//delete first 3 record
        //    str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
        //}
        ////-----------------------------------------------------
        ////num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
        //txtserialno.Text = "PU{0}" + str1;

        //dr.Close();
        //con.Close();
    }

    public void binddata()
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,INV FROM TEST_COMPONENT_TABLE where CID IS NULL and Branch_ID = " + Session["Branch"] + " and INV not in (select PROCE from  TEST_WISE_CONSUMPTION PROCE where Branch_ID = " + Session["Branch"] + ") and STATUS='ACTIVE'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
           {
                dropprocedure.DataSource = Ds;
                dropprocedure.DataTextField = "INV";
                dropprocedure.DataValueField = "INV";
                dropprocedure.DataBind();
                dropprocedure.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from TEST_WISE_CONSUMPTION where Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);

            //SqlDataAdapter Adp = new SqlDataAdapter("select * from TEST_WISE_CONSUMPTION", con);
            //DataTable Dt = new DataTable();
            //Adp.Fill(Dt);
            GridView1.DataSource = Ds1;
            GridView1.DataBind();
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select UOM_ID,UOM_Name FROM UOM_MST where Branch_ID= " + Session["Branch"] + "", false, false);

            if (Ds2.Tables[0].Rows.Count > 0)
            {
                dropunit.DataSource = Ds2;
                dropunit.DataTextField = "UOM_Name";
                dropunit.DataValueField = "UOM_ID";
                dropunit.DataBind();
                dropunit.Items.Insert(0, new ListItem("Please Select", "0"));
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
        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("NAME"), new DataColumn("QTY"), new DataColumn("UOM_NAME"), new DataColumn("UOM_ID") });
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
        OBJ_METHOD = new DataMathods();
        try
        {
            // Validation

            if (dropprocedure.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Procedure.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (grdMaterial.Rows.Count <= 0)
            {
                string message = "alert('*Add item.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
           // auto();

            //date in split format 
                       
            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, DateTime.Now.Date);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PROCE", SqlDbType.VarChar, 500, dropprocedure.SelectedItem.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


            OBJ_METHOD.ExecuteProceedure("USP_TEST_WISE_CONSUMPTION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            var id = OBJ_METHOD._objOut;
            string status = id.ToString().Split('-')[0];
            string master_id = id.ToString().Split('-')[1];
            if (status == "2")
            {
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            else
            {
                int chkedcounter = 0;
                int correctinput = 0;
                if (OBJ_METHOD._RESULT > 0)
                {
                    foreach (GridViewRow gv1 in grdMaterial.Rows)
                    {
                        var unit = (gv1.FindControl("lbl_Unit") as Label);

                        var nameGr = (gv1.FindControl("lbl_Name") as Label);
                        var quantyGr = (gv1.FindControl("lbl_Qty") as Label);
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[9];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@SERIAL_NO", SqlDbType.VarChar, 500, master_id);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, DateTime.Now.Date);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@PROCE", SqlDbType.VarChar, 500, dropprocedure.SelectedItem.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@UOM_ID", SqlDbType.Int, 0, unit.Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, nameGr.Text);
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, quantyGr.Text);
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


                        OBJ_METHOD.ExecuteProceedure("USP_TEST_WISE_CONSUMPTION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            correctinput++;
                            //OBJ_METHOD.commitOrRollbackTran("commit");
                            //binddata();

                        }
                    }
                    if (chkedcounter == correctinput && chkedcounter > 0)
                    {

                        OBJ_METHOD.commitOrRollbackTran("commit");
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        clearcontrol();
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Error occurred while processing data... Rolling back...')";
                    }

                    //  message1 = "alert('" + OBJ_METHOD._objOut + "')";

                    //using (SqlCommand cmd1 = new SqlCommand("USP_TEST_WISE_CONSUMPTION", con))
                    //{
                    //    cmd1.CommandType = CommandType.StoredProcedure;
                    //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                    //    cmd1.Parameters.Add("@SERIAL_NO", SqlDbType.VarChar).Value = txtserialno.Text;
                    //    cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                    //    cmd1.Parameters.Add("@PROCE", SqlDbType.VarChar).Value = dropprocedure.SelectedValue;
                    //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameGr.Text;
                    //    cmd1.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyGr.Text;

                    //    cmd1.ExecuteNonQuery();
                    //}

                }
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                //con.Open();


                //auto();
                //using (SqlCommand cmd = new SqlCommand("USP_TEST_WISE_CONSUMPTION", con))
                //{
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                //    cmd.Parameters.Add("@SERIAL_NO", SqlDbType.VarChar).Value = txtserialno.Text;
                //    cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                //    cmd.Parameters.Add("@PROCE", SqlDbType.VarChar).Value = dropprocedure.SelectedValue;
                //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                //    cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "";
                //    cmd.ExecuteNonQuery();
                //}
                //ItemCM();
            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
        finally
        {
            binddata();
          ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    
    }
    public void clearcontrol()
    {
        txtserialno.Text = "";
        txtname.Text = txtqty.Text = "";
        //ViewState["ITEM"] = null;
       // this.BindGrid();
        grdMaterial.DataSource = null;
        grdMaterial.DataBind();

        btncreate.Visible = true;
        btnupdate.Visible = false;
        dropprocedure.SelectedIndex = 0;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        int chkedcounter = 0;
        int correctinput = 0;
        try
        {
            // Validation        
            if (dropprocedure.SelectedItem.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropprocedure.Focus();
                return;
            }
            else if (grdMaterial.Rows.Count <= 0)
            {

                string message = "alert('*Add item.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@SERIAL_NO", SqlDbType.VarChar, 500, txtserialno.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PROCE", SqlDbType.VarChar, 500, dropprocedure.SelectedItem.Text);
            OBJ_METHOD.ExecuteProceedure("USP_TEST_WISE_CONSUMPTION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                DataTable dt1 = ViewState["ITEM"] as DataTable;
                grdMaterial.DataSource = dt1;
                grdMaterial.DataBind();
                foreach (GridViewRow gv1 in grdMaterial.Rows)
                {

                    var unit = (gv1.FindControl("lbl_Unit") as Label);
                    var nameGr = (gv1.FindControl("lbl_Name") as Label);
                    var quantyGr = (gv1.FindControl("lbl_Qty") as Label);

                    chkedcounter++;
                    SQL_PARAMS = new SqlParameter[9];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@SERIAL_NO", SqlDbType.VarChar, 500, txtserialno.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@PROCE", SqlDbType.VarChar, 500, dropprocedure.SelectedItem.Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@UOM_ID", SqlDbType.Int, 0, unit.Text);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, nameGr.Text);
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, quantyGr.Text);
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


                    OBJ_METHOD.ExecuteProceedure("USP_TEST_WISE_CONSUMPTION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        correctinput++;
                    }
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
                if (chkedcounter == correctinput && chkedcounter > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearcontrol();
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                } 
                
                
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();

            //using (SqlCommand cmd = new SqlCommand("USP_TEST_WISE_CONSUMPTION", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd.Parameters.Add("@SERIAL_NO", SqlDbType.VarChar).Value = lblslno.Text;
            //    cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    cmd.Parameters.Add("@PROCE", SqlDbType.VarChar).Value = dropprocedure.SelectedValue;
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "0.00";
            //    cmd.ExecuteNonQuery();
            //}

            //binddata();
            //ITEM_UPDATE();
            //con.Close();

            ////clearcontrol();
            //string message1 = "alert('Successfully Updated.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
        binddata();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
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
        else if (dropunit.SelectedIndex == 0)
        {
            string message = "alert('*Add Unit.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        //DataMathods OBJ_METHOD = new DataMathods();
        //txtserialno.Text = "";
        DataTable dt = (DataTable)ViewState["ITEM"];
        dt.Rows.Add(txtname.Text.Trim(), txtqty.Text.Trim(),dropunit.SelectedItem, dropunit.SelectedValue);
        ViewState["ITEM"] = dt;
        this.BindGrid();

       
        txtname.Text = "";
        txtqty.Text = "";
        dropunit.SelectedIndex = 0;

    }
    protected void grdMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM"];
            GridViewRow row = (GridViewRow)grdMaterial.Rows[e.RowIndex];

            Label name = (Label)row.FindControl("lbl_Name");
            Label quantity = (Label)row.FindControl("lbl_Qty");
            Label unit = (Label)row.FindControl("lbl_Unit");
            Label unitname = (Label)row.FindControl("lbl_Unitname");

            string name1 = name.Text.ToString();
            string quantity1 = quantity.Text.ToString();
            string unit1 = unit.Text.ToString();
            string unitnm = unitname.Text.ToString();
            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;

            this.BindGrid();
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    // reference the Delete LinkButton
        //    LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

        //    db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        //}
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["SERIAL_NO"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@SERIAL_NO", SqlDbType.VarChar, 500, slno);


            OBJ_METHOD.ExecuteProceedure("USP_TEST_WISE_CONSUMPTION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
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
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        //try
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //    con.Open();

        //    string slno = GridView1.DataKeys[e.RowIndex].Values["SERIAL_NO"].ToString();
        //    SqlCommand cm = new SqlCommand("delete from TEST_WISE_CONSUMPTION where SERIAL_NO='" + slno + "'", con);
        //    cm.ExecuteNonQuery();
        //    binddata();
        //    lblitem.Text = slno;
        //    ITEM_DEL();
        //    con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
   
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
             DataSet Ds3 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", false, false);
             if (Ds3.Tables[0].Rows.Count > 0)
             {
                 string message = "alert('* You Cant Edit.')";
                 ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                 return;
             }
             else
             {
                 var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["SERIAL_NO"].ToString();
                 DataSet Ds2 = OBJ_METHOD.Get_DataSet("SELECT * FROM TEST_WISE_CONSUMPTION  WHERE  SERIAL_NO='" + slno + "'", false, false);
                 if (Ds2.Tables[0].Rows.Count > 0)
                 //SqlCommand com = new SqlCommand("SELECT * FROM TEST_WISE_CONSUMPTION  WHERE  SERIAL_NO='" + slno + "'", con);

                 //dr = com.ExecuteReader();
                 //if (dr.Read())
                 {
                     btncreate.Visible = false;
                     btnupdate.Visible = true;
                     lblslno.Text = slno.ToString();
                     txtserialno.Text = slno.ToString();
                     txtdate.Text = Ds2.Tables[0].Rows[0]["DATE"].ToString();
                     dropprocedure.SelectedValue = Ds2.Tables[0].Rows[0]["PROCE"].ToString();
                     //dropunit.SelectedItem.Text = Ds2.Tables[0].Rows[0]["UOM_ID"].ToString();
                 }

                 DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT B.NAME,B.QTY,C.UOM_Name AS UOM_NAME,B.UOM_ID FROM TEST_WISE_CONSUMPTION A, TEST_WISE_CONSUMPTION_ITEM B,UOM_MST C WHERE A.SERIAL_NO=B.SERIAL_NO and C.UOM_ID=B.UOM_ID AND A.SERIAL_NO='" + slno + "'", false, false);
                 //da = new SqlDataAdapter("SELECT B.NAME,B.QTY FROM TEST_WISE_CONSUMPTION A, TEST_WISE_CONSUMPTION_ITEM B WHERE A.SERIAL_NO=B.SERIAL_NO AND A.SERIAL_NO='" + slno + "'", con);
                 //DataSet ds2 = new DataSet();
                 //da.Fill(ds2);
                 grdMaterial.DataSource = Ds.Tables["Table"];
                 grdMaterial.DataBind();

                 DataTable dt = Ds.Tables["Table"];
                 ViewState["ITEM"] = dt;
             }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from TEST_WISE_CONSUMPTION Where Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);
            GridView1.DataSource = Ds1;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "SERIAL_NO" };
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";

        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";

        }
        DataView sortedView = new DataView(getdata());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        GridView1.DataSource = sortedView;
        GridView1.DataBind();
    }
    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();

        DataSet Ds = OBJ_METHOD.Get_DataSet("select * from TEST_WISE_CONSUMPTION where Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;
    }

    protected void btnasset_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/Admin_Asset_Entry.aspx");
    }
    protected void btnrate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/admin_testnsme_prices.aspx");
    }
}