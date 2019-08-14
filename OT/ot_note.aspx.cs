using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;

public partial class OT_ot_note : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1;
    SqlCommand com;
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from OTNOTE_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("NT{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

        dr.Close();
        con.Close();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["OTID"] = gr.Cells[0].Text;
        Response.Redirect("~/OT/ot_note_reciept.aspx");
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

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_SELECT_OT", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds2;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }

            DataSet Ds = OBJ_METHOD.Get_DataSet("select BEDNO FROM BED_TABLE where ORGID='" + lblorgid.Text + "' and Branch_ID='" + Session["Branch"] + "'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropbedno.DataSource = Ds;
                dropbedno.DataTextField = "BEDNO";
                dropbedno.DataValueField = "BEDNO";
                dropbedno.DataBind();
                dropbedno.Items.Insert(0,new ListItem("Please Select","0"));
            }
            //da1 = new SqlDataAdapter("select BEDNO FROM BED_TABLE where ORGID='" + lblorgid.Text + "'", con);
            //DataTable ds1 = new DataTable();
            //da1.Fill(ds1);
            //dropbedno.DataSource = ds1;
            //dropbedno.DataTextField = "BEDNO";
            //dropbedno.DataValueField = "BEDNO";
            //dropbedno.DataBind();
            //dropbedno.Items.Insert(0, "Please Select");
            // SqlDataAdapter da = new SqlDataAdapter("SELECT OTNOTE_TABLE.ID,OTNOTE_TABLE.IPNO,BED_TABLE.PNAME,OTNOTE_TABLE.BEDNO,OTNOTE_TABLE.DATE FROM BED_TABLE INNER JOIN OTNOTE_TABLE ON BED_TABLE.BEDNO = OTNOTE_TABLE.BEDNO ORDER BY ID DESC", con);
            //using (SqlCommand cmd = new SqlCommand("SP_SELECT_OT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    GridView1.SelectedIndex = 0;
            //    GridView1.DataSource = dt;
            //    GridView1.DataKeyNames = new string[] { "ID" };
            //    GridView1.DataBind();
            //}
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
        lbluid.Text = Session["UID"].ToString();
        // 
        if (!IsPostBack)
        {
            auto();
            binddata();
            lblotno.Text = TXTID.Text;

            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        }
    }
    protected void dropbedno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * FROM BED_TABLE WHERE BEDNO='" + dropbedno.SelectedItem.Text + "' and Branch_ID='" + Session["Branch"] + "'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                lblpid.Text = Ds.Tables[0].Rows[0]["VN"].ToString();
                lblname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
            }
            //SqlCommand cm = new SqlCommand("select * FROM BED_TABLE WHERE BEDNO='" + dropbedno.SelectedItem.Text + "'", con);
            //dr = cm.ExecuteReader();
            //if (dr.Read())
            //{
            //    lblpid.Text = dr["VN"].ToString();
            //    lblname.Text = dr["PNAME"].ToString();
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BYID");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);
            //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_SELECT_OT", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                TXTID.Text = Ds2.Tables[0].Rows[0]["ID"].ToString();
                //lblpid.Text = dr["PID"].ToString();
                lblotno.Text = Ds2.Tables[0].Rows[0]["ID"].ToString();
                lblpid.Text = Ds2.Tables[0].Rows[0]["IPNO"].ToString();
                lblname.Text = Ds2.Tables[0].Rows[0]["PNAME"].ToString();
                dropbedno.Text = Ds2.Tables[0].Rows[0]["BEDNO"].ToString();
                txtnote.Text = Ds2.Tables[0].Rows[0]["NOTE"].ToString();
                txtdate.Text = Ds2.Tables[0].Rows[0]["DATE"].ToString();
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            OBJ_METHOD.ExecuteProceedure("SP_SELECT_OT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
                message1 = "alert('Deleted Successfully.')";
            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_SELECT_OT", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds2;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            //SqlDataAdapter da = new SqlDataAdapter("select ID,PID,IPNO,BEDNO,NOTE from OTNOTE_TABLE", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //GridView1.SelectedIndex = 0;
            //GridView1.DataSource = dt;
            //GridView1.PageIndex = e.NewPageIndex;
            //GridView1.DataKeyNames = new string[] { "ID" };
            //GridView1.DataBind();
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
            if (dropbedno.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Bed No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropbedno.Focus();
                return;
            }
            else if (txtnote.Text == "")
            {
                string message = "alert('* Note is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtnote.Focus();
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[10];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, dropbedno.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@OTNO", SqlDbType.VarChar, 500, lblotno.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 500, lblpid.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Note", SqlDbType.VarChar, 500, txtnote.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Date", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("SP_OT_DETAILS", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                Session["OTID"] = TXTID.Text;
                clearcontrol();
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //auto();
            ////SqlCommand cmd = new SqlCommand("insert into OTNOTE_TABLE (ID,ORGID,BEDNO,OTNO,IPNO,NOTE,DATE)values(@ID,@ORGID,@BEDNO,@OTNO,@IPNO,@NOTE,@DATE)", con);
            //using (SqlCommand cmd = new SqlCommand("SP_OT_DETAILS", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cmd.Parameters.Add("@OTNO", SqlDbType.VarChar).Value = lblotno.Text;
            //    cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = lblpid.Text;
            //    cmd.Parameters.Add("@Note", SqlDbType.VarChar).Value = txtnote.Text; ;
            //    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    cmd.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();

            
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
        Response.Redirect("~/OT/ot_note_reciept.aspx");


    }
    public void clearcontrol()
    {
        txtnote.Text = "";
        lblname.Text = lblotno.Text = lblpid.Text = "";
        auto();
        binddata();
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropbedno.SelectedItem.Text == "")
            {
                string message = "alert('* Please Select Bed No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtnote.Text == "")
            {
                string message = "alert('* Note is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, dropbedno.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@OTNO", SqlDbType.VarChar, 500, lblotno.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 500, lblpid.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Note", SqlDbType.VarChar, 500, txtnote.Text);

            OBJ_METHOD.ExecuteProceedure("SP_OT_DETAILS", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                Session["OTID"] = TXTID.Text;
                clearcontrol();
            }
            // SqlCommand cmd1 = new SqlCommand("UPDATE OTNOTE_TABLE SET BEDNO=@BEDNO,NOTE=@NOTE,DATE=@DATE WHERE ID=@ID", con);

            //using (SqlCommand cmd1 = new SqlCommand("SP_OT_DETAILS", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cmd1.Parameters.Add("@OTNO", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = lblname.Text;
            //    cmd1.Parameters.Add("@Note", SqlDbType.VarChar).Value = txtnote.Text; ;
            //    cmd1.Parameters.Add("@Date", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    cmd1.ExecuteNonQuery();

            //    //cmd1.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();

            //Session["OTID"] = TXTID.Text;
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
        Response.Redirect("~/OT/ot_note_reciept.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
    }
}