using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

public partial class ADMIN_admin_duityrates : System.Web.UI.Page
{
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();


    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
            if (!IsPostBack)
            {
                binddata();

                //SqlDataAdapter Adp1 = new SqlDataAdapter("select * from tblStaff where DesgId=(select id from tblDesignation where DesgName='Doctor')", con);
                DataSet Ds = OBJ_METHOD.Get_DataSet("select * from tblStaff where DesgId=(select id from tblDesignation where DesgName='Doctor')", false, false);
                dropdoctor.DataSource = Ds;
                dropdoctor.DataTextField = "Sname";
                dropdoctor.DataValueField = "id";
                dropdoctor.DataBind();
                dropdoctor.Items.Insert(0, new ListItem("Please Select", "0"));
            }

           
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
           
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

            //SqlDataAdapter Adp = new SqlDataAdapter("select * from DUTY_RATES_TABLE", );
            DataSet Ds = OBJ_METHOD.Get_DataSet("select d.id,d.FEES,d.DATE,s.Sname from DUTY_RATES_TABLE d join tblStaff s on s.id=d.NAME where d.Branch_ID='" + Session["Branch"].ToString() + "' Order By d.ID desc", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
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
            // Validation
            if (dropdoctor.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Doctor Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdoctor.Focus();
                return;
            }
            else if (txtfees.Text == "" || txtfees.Text == "0")
            {
                string message = "alert('* Please Enter Doctor Fees.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfees.Focus();
                return;
            }


            SqlParameter[] SQL_PARAMS = new SqlParameter[6];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, dropdoctor.SelectedValue);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@FEES", SqlDbType.Decimal, 0, txtfees.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("USP_DUTY_RATES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
            }

            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            //using (SqlCommand cmd = new SqlCommand("USP_DUTY_RATES", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropdoctor.SelectedItem.Text;
            //    cmd.Parameters.Add("@FEES", SqlDbType.Decimal).Value = FEES.Text;
            //    cmd.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();
            //clearcontrol();
            //string message1 = "alert('Successfully Saved.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        catch (Exception ex)
        {

            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    public void clearcontrol()
    {
        dropdoctor.SelectedIndex = 0;
        txtfees.Text = "";
        btnSubmit.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            // Validation        
            if (dropdoctor.SelectedItem.Text == "Please Select")
            {
                string message = "alert('* Please Select Doctor Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdoctor.Focus();
                return;
            }
            else if (txtfees.Text == "")
            {
                string message = "alert('* Please Enter Doctor Fees.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfees.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[4];
            
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@FEES", SqlDbType.Decimal, 0, txtfees.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, dropdoctor.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("USP_DUTY_RATES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();

                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                btnupdate.Visible = false;
                btnSubmit.Visible = true;

            }
            //using (SqlCommand cmd = new SqlCommand("USP_DUTY_RATES", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropdoctor.SelectedItem.Text;
            //    cmd.Parameters.Add("@FEES", SqlDbType.Decimal).Value = txtfees.Text;
            //    cmd.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();
            //clearcontrol();
            //string message1 = "alert('Successfully Updated.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        catch (Exception ex)
        {

            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Updated.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
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
        string message = string.Empty;
        try
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();

            //string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
            //SqlCommand cm = new SqlCommand("delete from DUTY_RATES_TABLE where ID='" + slno + "'", con);
            //cm.ExecuteNonQuery();

            //binddata();
            //con.Close();
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                string message1 = "alert('* You Cant Delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                OBJ_METHOD.ExecuteProceedure("USP_DUTY_RATES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                }
                message = "alert('" + OBJ_METHOD._objOut + "')";
            }
        }
        catch (Exception ex)
        {
            string message1 = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
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
                var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
                DataSet Dt = OBJ_METHOD.Get_DataSet("SELECT * from DUTY_RATES_TABLE where id='" + slno + "'", false, false);
                if (Dt.Tables[0].Rows.Count > 0)
                {
                    btnSubmit.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = Dt.Tables[0].Rows[0]["ID"].ToString();
                    txtdate.Text = Dt.Tables[0].Rows[0]["DATE"].ToString();
                    dropdoctor.SelectedValue = Dt.Tables[0].Rows[0]["NAME"].ToString();
                    txtfees.Text = Dt.Tables[0].Rows[0]["FEES"].ToString();
                }
            }
            //SqlCommand com = new SqlCommand("SELECT * from DUTY_RATES_TABLE where id='" + slno + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    btnSubmit.Visible = false;
            //    btnupdate.Visible = true;
            //    txtid.Text = dr["ID"].ToString();
            //    txtdate.Text = dr["DATE"].ToString();
            //    dropdoctor.SelectedItem.Text = dr["NAME"].ToString();
            //    txtfees.Text = dr["FEES"].ToString();
            //}
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
           
            //SqlDataAdapter da = new SqlDataAdapter("SELECT * from DUTY_RATES_TABLE", con);
            DataSet Dt = OBJ_METHOD.Get_DataSet("select d.id, d.FEES,d.DATE,s.Sname from DUTY_RATES_TABLE d join tblStaff s on s.id=d.NAME where d.Branch_ID='" + Session["Branch"].ToString() + "' order by d.ID desc", false, false);
            GridView1.DataSource = Dt;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();

        DataSet Ds = OBJ_METHOD.Get_DataSet("select d.id,d.FEES,d.DATE,s.Sname from DUTY_RATES_TABLE d join tblStaff s on s.id=d.NAME  where Branch_ID='" + Session["Branch"].ToString() + "' order by d.ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;

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
}