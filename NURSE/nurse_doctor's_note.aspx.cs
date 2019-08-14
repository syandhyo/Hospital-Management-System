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

public partial class NURSE_nurse_doctor_s_note : System.Web.UI.Page
{
    SqlDataReader dr;
    SqlConnection con;
    DataMathods OBJ_METHOD = new DataMathods();

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

            DataSet Ds = OBJ_METHOD.Get_DataSet("select BEDNO AS ID from BED_TABLE WHERE Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropPID.DataSource = Ds;
                dropPID.DataTextField = "ID";
                dropPID.DataValueField = "ID";
                dropPID.DataBind();
                dropPID.Items.Insert(0,new ListItem("Please Select","0"));
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select A.id,A.Sname from tblStaff A,tblDesignation B Where B.DesgName='Doctor' And B.id=A.DesgId and A.Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropdr.DataSource = Ds1;
                dropdr.DataTextField = "Sname";
                dropdr.DataValueField = "id";
                dropdr.DataBind();
                dropdr.Items.Insert(0,new ListItem("Please Select","0"));
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ALL");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_DOCTOR_NOTE_SEL", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds2;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
            #region oldcode
            //SqlDataAdapter Adp = new SqlDataAdapter("select BEDNO AS ID from BED_TABLE", con);
            //DataTable Dt = new DataTable();
            //Adp.Fill(Dt);
            //dropPID.DataSource = Dt;
            //dropPID.DataTextField = "ID";
            //dropPID.DataValueField = "ID";
            //dropPID.DataBind();
            //dropPID.Items.Insert(0, "Please Select");

            //SqlDataAdapter Adp1 = new SqlDataAdapter("select id,Sname from tblStaff", con);
            //DataTable Dt1 = new DataTable();
            //Adp1.Fill(Dt1);
            //dropdr.DataSource = Dt1;
            //dropdr.DataTextField = "Sname";
            //dropdr.DataValueField = "id";
            //dropdr.DataBind();
            //dropdr.Items.Insert(0, "Please Select");

            //using (SqlCommand cmd = new SqlCommand("SP_DOCTOR_NOTE_SEL", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ALL";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable Dt2 = new DataTable();
            //    da.Fill(Dt2);
            //    GridView1.DataSource = Dt2;
            //    GridView1.DataBind();
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
            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

            OBJ_METHOD.ExecuteProceedure("SP_DOCTOR_NOTE_DELETE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearcontrol();
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
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT id,PID,Name,BedNo,Sid,Note,Ndate from tblDoctorNote where id='" + slno + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                lblbed.Text = Ds.Tables[0].Rows[0]["PID"].ToString();
                lblname.Text = Ds.Tables[0].Rows[0]["Name"].ToString();
                dropPID.SelectedValue = Ds.Tables[0].Rows[0]["BedNo"].ToString();
                dropdr.SelectedValue = Ds.Tables[0].Rows[0]["Sid"].ToString();
                txtnote.Text = Ds.Tables[0].Rows[0]["Note"].ToString();
                txtdate.Text = Ds.Tables[0].Rows[0]["Ndate"].ToString();
            }
            //SqlCommand com = new SqlCommand("SELECT id,PID,Name,BedNo,Sid,Note,Ndate from tblDoctorNote where id='" + slno + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    btnSubmit.Visible = false;
            //    btnupdate.Visible = true;
            //    txtid.Text = dr["ID"].ToString();
            //    lblbed.Text = dr["PID"].ToString();
            //    lblname.Text = dr["Name"].ToString();
            //    dropPID.Text = dr["BedNo"].ToString();
            //    dropdr.Text = dr["Sid"].ToString();
            //    txtnote.Text = dr["Note"].ToString();
            //    txtdate.Text = dr["Ndate"].ToString();
            //}
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

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_DOCTOR_NOTE_SEL", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds2;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
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
            if (txtnote.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropPID.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Bed No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropdr.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Doctor Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[13];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, dropPID.Text);
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@Name", SqlDbType.VarChar, 200, lblname.Text);
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 200, lblbed.Text);
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@Sid", SqlDbType.VarChar, 500, dropdr.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS2[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS2[8] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS2[9] = OBJ_METHOD.createParams("@Ndate", SqlDbType.DateTime, 200, txtdate.Text);
            SQL_PARAMS2[10] = OBJ_METHOD.createParams("@UNAME", SqlDbType.VarChar, 500, lblid.Text);
            SQL_PARAMS2[11] = OBJ_METHOD.createParams("@UID", SqlDbType.VarChar, 500, lbluid.Text);
            SQL_PARAMS2[12] = OBJ_METHOD.createParams("@Note", SqlDbType.VarChar,500, txtnote.Text);

            OBJ_METHOD.ExecuteProceedure("SP_DOCTOR_NOTE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearcontrol();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            #region oldcode
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //SqlCommand cmd = new SqlCommand("insert into tblDoctorNote(FYEAR,ORGID,PID,Name,Bedno,Sid,Note,Ndate,UNAME,UID)values(@FYEAR,@ORGID,@PID,@Name,@Bedno,@Sid,@Note,@Ndate,@UNAME,@UID)", con);
            //using (SqlCommand cmd = new SqlCommand("SP_DOCTOR_NOTE", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = dropPID.Text;
            //    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = lblname.Text;
            //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblbed.Text;
            //    cmd.Parameters.Add("@Sid", SqlDbType.VarChar).Value = dropdr.Text;
            //    cmd.Parameters.Add("@Note", SqlDbType.VarChar).Value = txtnote.Text;
            //    cmd.Parameters.Add("@Ndate", SqlDbType.DateTime).Value = txtdate.Text;
            //    cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
            //    cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = lbluid.Text;
            //    cmd.Parameters.Add("@id", SqlDbType.Int).Value = 1;
            //    cmd.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();


            //string message1 = "alert('Successfully Saved.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            //clearcontrol();
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

    public void clearcontrol()
    {
        lblname.Text = "";
        lblbed.Text = "";
        txtnote.Text = "";
        txtdate.Text = "";
        dropdr.SelectedIndex = 0;
        dropPID.SelectedIndex = 0;
        btnSubmit.Visible = true;
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        btnupdate.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtnote.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropPID.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Bed No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropdr.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Doctor Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[9];

            
            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, dropPID.Text);
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Name", SqlDbType.VarChar, 200, lblname.Text);
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 200, lblbed.Text);
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@Sid", SqlDbType.VarChar, 500, dropdr.Text);
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@Ndate", SqlDbType.DateTime, 200, txtdate.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@UNAME", SqlDbType.VarChar, 500, lblid.Text);
            SQL_PARAMS2[7] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS2[8] = OBJ_METHOD.createParams("@Note", SqlDbType.VarChar, 500, txtnote.Text);

            OBJ_METHOD.ExecuteProceedure("SP_DOCTOR_NOTE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearcontrol();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            ////SqlCommand cmd1 = new SqlCommand("UPDATE tblDoctorNote SET Bedno=@Bedno,Name=@Name,PID=@PID,Sid=@Sid,Note=@Note WHERE id=@id", con);
            //using (SqlCommand cmd1 = new SqlCommand("SP_DOCTOR_NOTE", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";

            //    cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = dropPID.Text;
            //    cmd1.Parameters.Add("@Name", SqlDbType.VarChar).Value = lblname.Text;
            //    cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblbed.Text;
            //    cmd1.Parameters.Add("@Sid", SqlDbType.VarChar).Value = dropdr.Text;
            //    cmd1.Parameters.Add("@Note", SqlDbType.VarChar).Value = txtnote.Text;
            //    cmd1.Parameters.Add("@Ndate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //    cmd1.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@UID", SqlDbType.VarChar).Value = "";
            //    cmd1.ExecuteNonQuery();
            //}

            //string message1 = "alert('Successfully Updated.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            //binddata();
            //con.Close();
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
        clearcontrol();
    }

    protected void dropPID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select PNAME,PID from BED_TABLE where BEDNO='" + dropPID.Text.ToString() + "'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                lblname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
                lblbed.Text = Ds.Tables[0].Rows[0]["PID"].ToString();
            }
            //SqlCommand COM = new SqlCommand("select PNAME,PID from BED_TABLE where BEDNO='" + dropPID.Text.ToString() + "'", con);
            //dr = COM.ExecuteReader();
            //if (dr.Read())
            //{
            //    lblname.Text = dr["PNAME"].ToString();
            //    lblbed.Text = dr["PID"].ToString();
            //}
            //dr.Close();
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
}