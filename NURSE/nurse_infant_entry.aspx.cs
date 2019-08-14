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
using System.Web.Services;

public partial class NURSE_nurse_infant_entry : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1;
    SqlCommand com;
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();
    [WebMethod]
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from NEWBORN_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("NB{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

        dr.Close();
        con.Close();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }

            DataSet Ds = OBJ_METHOD.Get_DataSet("Select ID,Mother_id,MName,DOB,Gender,Weight,FatherName from NEWBORN_TABLE Where Branch_FY='" + Session["Branch"] + "' ORDER BY ID DESC", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = Ds;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select NAME,ID from  WARD_TABLE Where Branch_FY='" + Session["Branch"] + "'", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropward.DataSource = Ds1;
                dropward.DataTextField = "NAME";
                dropward.DataValueField = "ID";
                dropward.DataBind();
                dropward.Items.Insert(0,new ListItem("Please Select","0"));
            }
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select VN FROM BED_TABLE where VN like '%IP%' and ORGID='" + lblorgid.Text + "' and Branch_FY='" + Session["Branch"] + "'", false, false);

            if (Ds2.Tables[0].Rows.Count > 0)
            {
                da1 = new SqlDataAdapter("select VN FROM BED_TABLE where VN like '%IP%' and ORGID='" + lblorgid.Text + "'", con);
                DataTable ds1 = new DataTable();
                da1.Fill(ds1);
                dropmotherid.DataSource = Ds2;
                dropmotherid.DataTextField = "VN";
                dropmotherid.DataValueField = "VN";
                dropmotherid.DataBind();
                dropmotherid.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }


        //SqlDataAdapter da = new SqlDataAdapter("Select ID,Mother_id,MName,DOB,Gender,Weight,FatherName from NEWBORN_TABLE Where Branch_FY='" + Session["Branch"] + "' ORDER BY ID DESC", con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //GridView1.SelectedIndex = 0;
        //GridView1.DataSource = dt;
        //GridView1.DataKeyNames = new string[] { "ID" };
        //GridView1.DataBind();


        //SqlDataAdapter da1 = new SqlDataAdapter("select NAME,ID from  WARD_TABLE Where Branch_FY='" + Session["Branch"] + "'", con);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);
        ////dropbedno.SelectedIndex = 0;
        //dropward.DataSource = dt1;
        //dropward.DataTextField = "NAME";
        //dropward.DataValueField = "ID";
        //dropward.DataBind();
        //dropward.Items.Insert(0, "Please Select");
        //con.Close();
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
        lbluid.Text = Session["UID"].ToString();


        if (!IsPostBack)
        {
            binddata();
            div1.Visible = false;
            txtdob.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        }
    }
    protected void dropmname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select * FROM BED_TABLE WHERE VN='" + dropmotherid.SelectedItem.Text + "'", false, false);

            if (Ds2.Tables[0].Rows.Count > 0)
            {
                lblmname.Text = Ds2.Tables[0].Rows[0]["PNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Ds = OBJ_METHOD.Get_DataSet("Select ID,Mother_id,MName,DOB,Gender,Weight,FatherName from NEWBORN_TABLE where ID='" + slno + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                TXTID.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                btncreate.Visible = false;
                btnupdate.Visible = true;
                dropmotherid.SelectedValue = Ds.Tables[0].Rows[0]["Mother_id"].ToString();
                lblmname.Text = Ds.Tables[0].Rows[0]["MName"].ToString();
                txtdob.Text = Ds.Tables[0].Rows[0]["DOB"].ToString();
                dropgen.Text = Ds.Tables[0].Rows[0]["Gender"].ToString();
                txtweight.Text = Ds.Tables[0].Rows[0]["Weight"].ToString();
                txtfname.Text = Ds.Tables[0].Rows[0]["FatherName"].ToString();
                
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select A.BEDNO,B.ID From BED_TABLE A,WARD_TABLE B Where A.PID='" + slno + "' And A.WARD=B.ID", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                CheckBox1.Checked = true;
                Session["bid"] = Ds1.Tables[0].Rows[0]["BEDNO"].ToString();
                dropward.SelectedValue = Ds1.Tables[0].Rows[0]["ID"].ToString();
                //dropbedno.DataValueField = Ds1.Tables[0].Rows[0]["BEDNO"].ToString();
                DataSet Ds2 = OBJ_METHOD.Get_DataSet("SELECT BEDNO FROM BED_MATRIX_TABLE WHERE STATUS='AVAILABLE' AND NAME='" + dropward.SelectedValue + "' AND ORGID='" + lblorgid.Text + "' ORDER BY ID ASC", false, false);

                if (Ds2.Tables[0].Rows.Count > 0)
                {
                    dropbedno.DataSource = Ds2;
                    dropbedno.DataTextField = "BEDNO";
                    dropbedno.DataValueField = "BEDNO";
                    dropbedno.DataBind();
                    dropbedno.Items.Insert(0, new ListItem("Please Select", "0"));
                    //dropbedno.SelectedValue = Session["bid"].ToString();
                    dropbedno.DataValueField = Session["bid"].ToString();
                }
            }
            else
            {
                CheckBox1.Checked = false;
                div1.Visible = false;
            }
            #region oldcode
            //SqlCommand com = new SqlCommand("Select ID,Mother_id,MName,DOB,Gender,Weight,FatherName from NEWBORN_TABLE where ID='" + slno + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    TXTID.Text = dr["ID"].ToString();
            //    btncreate.Visible = false;
            //    btnupdate.Visible = true;
            //    dropmotherid.SelectedItem.Text = dr["Mother_id"].ToString();
            //    lblmname.Text = dr["MName"].ToString();
            //    txtdob.Text = dr["DOB"].ToString();
            //    dropgen.Text = dr["Gender"].ToString();
            //    txtweight.Text = dr["Weight"].ToString();
            //    txtfname.Text = dr["FatherName"].ToString();
            //    if (CheckBox1.Checked == true)
            //    {
            //        div1.Visible = true;
            //    }
            //}
#endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();

            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            OBJ_METHOD.ExecuteProceedure("SP_NEW_BORN", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
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
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[6].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("Select ID,Mother_id,MName,DOB,Gender,Weight,FatherName from NEWBORN_TABLE Where Branch_FY='" + Session["Branch"] + "' ORDER BY ID DESC", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = Ds;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            //SqlDataAdapter da = new SqlDataAdapter("select ID,Mother_id,MName,DOB,Gender,Weight,FatherName from NEWBORN_TABLE ORDER BY ID DESC", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //GridView1.SelectedIndex = 0;
            //GridView1.DataSource = dt;
            //GridView1.PageIndex = e.NewPageIndex;
            //GridView1.DataKeyNames = new string[] { "ID" };
            //GridView1.DataBind();
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            if (dropmotherid.SelectedIndex == 0 || dropmotherid.SelectedItem.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtweight.Text == "" || (Convert.ToDecimal(txtweight.Text) > 5))
            {
                string message = "alert('* Incorrect weight')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropgen.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtfname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            auto();
            if (CheckBox1.Checked == true)
            {
                if (dropward.SelectedIndex == 0)
                {
                    string message = "alert('* Please Select Ward.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }

                DataSet Ds = OBJ_METHOD.Get_DataSet("select * from BED_MATRIX_TABLE where BEDNO='" + dropbedno.Text + "' AND STATUS='AVAILABLE' and ORGID='" + lblorgid.Text + "' and Branch_FY='" + Session["Branch"] + "'", false, false);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    SqlParameter[] SQL_PARAMS = new SqlParameter[9];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, TXTID.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, TXTID.Text);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 500, dropmotherid.Text);
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@WARD", SqlDbType.VarChar, 500, dropward.SelectedItem.Text);
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                    OBJ_METHOD.ExecuteProceedure("SP_BED_ENTRY", "", "", SqlDbType.VarChar, SQL_PARAMS);


                    if (OBJ_METHOD._RESULT > 0)
                    {
                        SQL_PARAMS = new SqlParameter[12];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@Mother_id", SqlDbType.VarChar, 500, dropmotherid.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@DOB", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd"));
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@MName", SqlDbType.VarChar, 500, lblmname.Text);
                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@Gender", SqlDbType.VarChar, 500, dropgen.Text);
                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@Weight", SqlDbType.Decimal, 0, txtweight.Text);
                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@FatherName", SqlDbType.VarChar, 500, txtfname.Text);

                        OBJ_METHOD.ExecuteProceedure("SP_NEW_BORN", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);


                        if (OBJ_METHOD._RESULT > 0)
                        {
                            OBJ_METHOD.commitOrRollbackTran("commit");
                            clearcontrol();
                            message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        }
                    }
                }
                else
                {
                    string message = "alert('The selected bed is not available. Try new bed.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            else
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[11];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT1");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@Mother_id", SqlDbType.VarChar, 500, dropmotherid.Text);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@DOB", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd"));
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[7] = OBJ_METHOD.createParams("@MName", SqlDbType.VarChar, 500, lblmname.Text);
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@Gender", SqlDbType.VarChar, 500, dropgen.Text);
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@Weight", SqlDbType.Decimal, 0, txtweight.Text);
                SQL_PARAMS[10] = OBJ_METHOD.createParams("@FatherName", SqlDbType.VarChar, 500, txtfname.Text);

                OBJ_METHOD.ExecuteProceedure("SP_NEW_BORN", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);


                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearcontrol();
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
        #region oldcode
            //SqlCommand com1 = new SqlCommand("select * from BED_MATRIX_TABLE where BEDNO='" + dropbedno.Text + "' AND STATUS='AVAILABLE' and ORGID='" + lblorgid.Text + "'", con);
                //dr = com1.ExecuteReader();
                //if (dr.Read())
                //    {
                //        dr.Close();

                //        auto();
                //        //  SqlCommand cmd = new SqlCommand("insert into NEWBORN_TABLE (ID,ORGID,Mother_id,MName,DOB,Gender,Weight,FatherName)values(@ID,@ORGID,@Mother_id,@MName,@DOB,@Gender,@Weight,@FatherName)", con);
                //        using (SqlCommand cmd = new SqlCommand("SP_NEW_BORN", con))
                //        {
                //            cmd.CommandType = CommandType.StoredProcedure;
                //            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                //            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                //            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //            cmd.Parameters.Add("@Mother_id", SqlDbType.VarChar).Value = dropmotherid.Text;
                //            cmd.Parameters.Add("@MName", SqlDbType.VarChar).Value = lblmname.Text;
                //            cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd");
                //            cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = dropgen.Text;
                //            cmd.Parameters.Add("@Weight", SqlDbType.Decimal).Value = txtweight.Text; ;
                //            cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = txtfname.Text;
                //            cmd.ExecuteNonQuery();
                //        }

                //        SqlCommand cmd1 = new SqlCommand("UPDATE BED_MATRIX_TABLE SET STATUS='OCCUPIED' WHERE BEDNO=@BEDNO AND ORGID=@ORGID", con);
                //        cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
                //        cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //        cmd1.ExecuteNonQuery();

                //        // SqlCommand cmd2 = new SqlCommand("insert into BED_TABLE (WARD,BEDNO,PID,PNAME,ORGID,VN)values(@WARD,@BEDNO,@PID,@PNAME,@ORGID,@VN)", con);
                //        using (SqlCommand cmd2 = new SqlCommand("SP_BED_ENTRY", con))
                //        {
                //            cmd2.CommandType = CommandType.StoredProcedure;
                //            cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                //            cmd2.Parameters.Add("@VN", SqlDbType.VarChar).Value = TXTID.Text;
                //            cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //            cmd2.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
                //            cmd2.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
                //            cmd2.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = dropmotherid.Text;
                //            cmd2.Parameters.Add("@WARD", SqlDbType.VarChar).Value = dropward.Text;
                //            cmd2.ExecuteNonQuery();
                //        }
                //    }
                //    else
                //    {
                //        dr.Close();
                //        string message = "alert('The selected bed is not available. Try new bed.')";
                //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //        return;
                //    }
                //}
                //else
                //{
                //    auto();
                //    // SqlCommand cmd = new SqlCommand("insert into NEWBORN_TABLE (ID,ORGID,Mother_id,MName,DOB,Gender,Weight,FatherName)values(@ID,@ORGID,@Mother_id,@MName,@DOB,@Gender,@Weight,@FatherName)", con);
                //    using (SqlCommand cmd = new SqlCommand("SP_NEW_BORN", con))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                //        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                //        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //        cmd.Parameters.Add("@Mother_id", SqlDbType.VarChar).Value = dropmotherid.Text;
                //        cmd.Parameters.Add("@MName", SqlDbType.VarChar).Value = lblmname.Text;
                //        cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd");
                //        cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = dropgen.Text;
                //        cmd.Parameters.Add("@Weight", SqlDbType.VarChar).Value = txtweight.Text; ;
                //        cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = txtfname.Text;
                //        cmd.ExecuteNonQuery();
                //    }
                //}
                //binddata();
                //string message1 = "alert('Sucessfully Save.')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                //con.Close();
            //clearcontrol();
            #endregion
        
    }
    public void clearcontrol()
    {
        //dropbedno.SelectedItem.Text = "";
        dropgen.SelectedIndex = 0;
        dropmotherid.SelectedIndex = 0;
        dropward.SelectedIndex = 0;
        txtdob.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        div1.Visible = false;
        txtfname.Text = "";
        txtweight.Text = "";
        lblmname.Text = "";
        btncreate.Visible = true;
        btnupdate.Visible = false;
        CheckBox1.Checked = false;
        binddata();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            if (dropmotherid.SelectedItem.Text == "" || dropmotherid.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtweight.Text == "" || (Convert.ToDecimal(txtweight.Text) > 5))
            {
                string message = "alert('* Incorrect Weight.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropgen.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtfname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[11];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Mother_id", SqlDbType.VarChar, 500, dropmotherid.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DOB", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@MName", SqlDbType.VarChar, 500, lblmname.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Gender", SqlDbType.VarChar, 500, dropgen.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@Weight", SqlDbType.Decimal, 0, txtweight.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@FatherName", SqlDbType.VarChar, 500, txtfname.Text);

            OBJ_METHOD.ExecuteProceedure("SP_NEW_BORN", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
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
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //// SqlCommand cmd1 = new SqlCommand("UPDATE NEWBORN_TABLE SET Mother_id=@Mother_id,DOB=@DOB,Gender=@Gender,Weight=@Weight,FatherName=@FatherName WHERE ID=@ID", con);
            //using (SqlCommand cmd1 = new SqlCommand("SP_NEW_BORN", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@Mother_id", SqlDbType.VarChar).Value = dropmotherid.SelectedValue.ToString();
            //    cmd1.Parameters.Add("@MName ", SqlDbType.VarChar).Value = lblmname.Text;
            //    cmd1.Parameters.Add("@DOB", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd");
            //    cmd1.Parameters.Add("@Gender", SqlDbType.VarChar).Value = dropgen.Text;
            //    cmd1.Parameters.Add("@Weight", SqlDbType.Decimal).Value = txtweight.Text;
            //    cmd1.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = txtfname.Text;
            //    cmd1.ExecuteNonQuery();
            //}

            //binddata();
        //con.Close();
        #endregion

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
        binddata();
    }
    protected void dropward_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT BEDNO FROM BED_MATRIX_TABLE WHERE STATUS='AVAILABLE' AND NAME='" + dropward.SelectedValue + "' AND ORGID='" + lblorgid.Text + "' ORDER BY ID ASC", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropbedno.DataSource = Ds;
                dropbedno.DataTextField = "BEDNO";
                dropbedno.DataValueField = "BEDNO";
                dropbedno.DataBind();
                dropbedno.Items.Insert(0,new ListItem("Please Select","0"));
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT BEDNO FROM BED_MATRIX_TABLE WHERE STATUS='AVAILABLE' AND NAME='" + dropward.SelectedValue+ "' AND ORGID='" + lblorgid.Text + "' ORDER BY ID ASC", con);
            //DataTable dt1 = new DataTable();
            //da1.Fill(dt1);
            ////dropbedno.SelectedIndex = 0;
            //dropbedno.DataSource = dt1;
            //dropbedno.DataTextField = "BEDNO";
            //dropbedno.DataBind();
            //dropbedno.Items.Insert(0, "Please Select");
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            div1.Visible = true;
        }
        else
        {
            div1.Visible = false;
        }
    }
}