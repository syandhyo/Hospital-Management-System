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

public partial class RECEPTION_reception_police_info_form : System.Web.UI.Page
{
     string num1 = "SJ000";
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from POLICE_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("PO{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

        dr.Close();
        con.Close();
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

        if (!IsPostBack)
        {
            binddata();
    
        }
       
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");

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

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_POLICE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_POLICE_INFO", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
            }

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[1];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BED");

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_POLICE_INFO", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                dropbedno.DataSource = DS2;
                dropbedno.DataTextField = "BEDNO";
                dropbedno.DataBind();
                dropbedno.Items.Insert(0, new ListItem("Please Select","0"));
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
            if (dropbedno.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Bed No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropbedno.Focus();
                return;
            }
            else if (txtnationality.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtnationality.Focus();
                return;
            }
            else if (txtperson.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtperson.Focus();
                return;
            }
            else if (lblipno.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
           

            //SqlCommand com = new SqlCommand("select * from POLICE_TABLE where PID='" + lblipno.Text + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    string message = "alert('*Form alredy genereted for this patient.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            //dr.Close();

            auto();

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[24];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 100, TXTID.Text);
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 200, lblipno.Text);
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 200, dropbedno.Text);
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, txtdate.Text);
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 200, lblpname.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@AGE", SqlDbType.VarChar, 200, lblage.Text);
            SQL_PARAMS2[7] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 200, lblgender.Text);
            SQL_PARAMS2[8] = OBJ_METHOD.createParams("@RELIGION", SqlDbType.VarChar, 200, txtreligion.Text);
            SQL_PARAMS2[9] = OBJ_METHOD.createParams("@NATIONALITY", SqlDbType.VarChar, 200, txtnationality.Text);
            SQL_PARAMS2[10] = OBJ_METHOD.createParams("@PADDRESS", SqlDbType.VarChar, 500, txtpaddress.Text);
            SQL_PARAMS2[11] = OBJ_METHOD.createParams("@CAUSE", SqlDbType.VarChar, 200, dropcause.Text);
            SQL_PARAMS2[12] = OBJ_METHOD.createParams("@CADDRESS", SqlDbType.VarChar, 200, txtcaddress.Text);
            SQL_PARAMS2[13] = OBJ_METHOD.createParams("@IDMARK", SqlDbType.VarChar, 200, txtidmark.Text);
            SQL_PARAMS2[14] = OBJ_METHOD.createParams("@ADDATE", SqlDbType.VarChar, 200, lbladdate.Text);
            SQL_PARAMS2[15] = OBJ_METHOD.createParams("@DEATHDATE", SqlDbType.VarChar, 200, Convert.ToDateTime(txtdeathdate.Text).ToString("dd-MM-yyyy"));
            SQL_PARAMS2[16] = OBJ_METHOD.createParams("@PERSONE", SqlDbType.VarChar, 200, txtperson.Text);
            SQL_PARAMS2[17] = OBJ_METHOD.createParams("@RELATION", SqlDbType.VarChar, 200, txtrelationpatient.Text);
            SQL_PARAMS2[18] = OBJ_METHOD.createParams("@CASEHISTORY", SqlDbType.VarChar, 200, txtcasehis.Text);
            SQL_PARAMS2[19] = OBJ_METHOD.createParams("@CAUSEOFDEATH", SqlDbType.VarChar, 200, txtcauseofdeath.Text);
            SQL_PARAMS2[20] = OBJ_METHOD.createParams("@USERNAME", SqlDbType.VarChar, 200, lblid.Text);
            SQL_PARAMS2[21] = OBJ_METHOD.createParams("@UID", SqlDbType.VarChar, 200, lbluid.Text);
            SQL_PARAMS2[22] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"]);
            SQL_PARAMS2[23] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            OBJ_METHOD.ExecuteProceedure("RECP_POLICE_INFO_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
              
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due to some issues, Data not saved.')";
            }
           

            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            
            Session["POID"] = TXTID.Text;
            
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            //reset();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        Response.Redirect("~/RECEPTION/reception_police_reciept.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ID");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_POLICE_INFO", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                TXTID.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                lblipno.Text = Ds.Tables[0].Rows[0]["PID"].ToString();
                dropbedno.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                txtdate.Text = Ds.Tables[0].Rows[0]["DATE"].ToString();
                lblpname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
                lblage.Text = Ds.Tables[0].Rows[0]["AGE"].ToString();
                lblgender.Text = Ds.Tables[0].Rows[0]["GENDER"].ToString();
                txtreligion.Text = Ds.Tables[0].Rows[0]["RELIGION"].ToString();
                txtnationality.Text = Ds.Tables[0].Rows[0]["NATIONALITY"].ToString();
                txtpaddress.Text = Ds.Tables[0].Rows[0]["PADDRESS"].ToString();
                dropcause.Text = Ds.Tables[0].Rows[0]["CAUSE"].ToString();
                txtcaddress.Text = Ds.Tables[0].Rows[0]["CADDRESS"].ToString();
                txtidmark.Text = Ds.Tables[0].Rows[0]["IDMARK"].ToString();
                lbladdate.Text = Ds.Tables[0].Rows[0]["ADDATE"].ToString();

                txtdeathdate.Text = (Convert.ToDateTime(Ds.Tables[0].Rows[0]["DEATHDATE"]).ToString("dd/MM/yyyy"));

                txtperson.Text = Ds.Tables[0].Rows[0]["PERSONE"].ToString();
                txtrelationpatient.Text = Ds.Tables[0].Rows[0]["RELATION"].ToString();
                txtcasehis.Text = Ds.Tables[0].Rows[0]["CASEHISTORY"].ToString();
                txtcauseofdeath.Text = Ds.Tables[0].Rows[0]["CAUSEOFDEATH"].ToString();

            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 100, slno);

                OBJ_METHOD.ExecuteProceedure("RECP_POLICE_INFO_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    // clearcontrol();
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
          
            //using (SqlCommand cmd1 = new SqlCommand("RECP_POLICE_INFO", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno.ToString();
            //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.ExecuteReader();
            //    //SqlDataAdapter da = new SqlDataAdapter("delete from POLICE_TABLE where ID='" + slno.ToString() + "'", con);
            //    //ds = new DataSet();
            //    //da.Fill(ds);
            //}
            //binddata();
          
            //Response.Redirect("~/RECEPTION/reception_police_info_form.aspx");
       
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[1];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_POLICE");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_POLICE_INFO", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
          
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtreligion.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtreligion.Focus();
                return;
            }
            else if (txtnationality.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtperson.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtperson.Focus();
                return;
            }
            else if (lblipno.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[22];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 100, TXTID.Text);
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 200, lblipno.Text);
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 200, dropbedno.Text);
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, txtdate.Text);
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 200, lblpname.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@AGE", SqlDbType.VarChar, 200, lblage.Text);
            SQL_PARAMS2[7] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 200, lblgender.Text);
            SQL_PARAMS2[8] = OBJ_METHOD.createParams("@RELIGION", SqlDbType.VarChar, 200, txtreligion.Text);
            SQL_PARAMS2[9] = OBJ_METHOD.createParams("@NATIONALITY", SqlDbType.VarChar, 200, txtnationality.Text);
            SQL_PARAMS2[10] = OBJ_METHOD.createParams("@PADDRESS", SqlDbType.VarChar, 500, txtpaddress.Text);
            SQL_PARAMS2[11] = OBJ_METHOD.createParams("@CAUSE", SqlDbType.VarChar, 200, dropcause.Text);
            SQL_PARAMS2[12] = OBJ_METHOD.createParams("@CADDRESS", SqlDbType.VarChar, 200, txtcaddress.Text);
            SQL_PARAMS2[13] = OBJ_METHOD.createParams("@IDMARK", SqlDbType.VarChar, 200, txtidmark.Text);
            SQL_PARAMS2[14] = OBJ_METHOD.createParams("@ADDATE", SqlDbType.VarChar, 200, lbladdate.Text);
            SQL_PARAMS2[15] = OBJ_METHOD.createParams("@DEATHDATE", SqlDbType.VarChar, 200, Convert.ToDateTime(txtdeathdate.Text).ToString("dd-MM-yyyy"));
            SQL_PARAMS2[16] = OBJ_METHOD.createParams("@PERSONE", SqlDbType.VarChar, 200, txtperson.Text);
            SQL_PARAMS2[17] = OBJ_METHOD.createParams("@RELATION", SqlDbType.VarChar, 200, txtrelationpatient.Text);
            SQL_PARAMS2[18] = OBJ_METHOD.createParams("@CASEHISTORY", SqlDbType.VarChar, 200, txtcasehis.Text);
            SQL_PARAMS2[19] = OBJ_METHOD.createParams("@CAUSEOFDEATH", SqlDbType.VarChar, 200, txtcauseofdeath.Text);
            SQL_PARAMS2[20] = OBJ_METHOD.createParams("@USERNAME", SqlDbType.VarChar, 200, lblid.Text);
            SQL_PARAMS2[21] = OBJ_METHOD.createParams("@UID", SqlDbType.VarChar, 200, lbluid.Text);

            OBJ_METHOD.ExecuteProceedure("RECP_POLICE_INFO_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                btncreate.Visible = true;
                btnupdate.Visible = false;
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }

            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due to some issues, Data not saved.')";
            }
                     
            Session["POID"] = TXTID.Text;
           
           
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            //reset();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        Response.Redirect("~/RECEPTION/reception_police_reciept.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clear();
    }
   
    protected void dropbedno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DROP");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_POLICE_INFO", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                lblipno.Text = Ds.Tables[0].Rows[0]["VN"].ToString();
                lblpname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
                lblage.Text = Ds.Tables[0].Rows[0]["AGE"].ToString();
                lblgender.Text = Ds.Tables[0].Rows[0]["GENDER"].ToString();
                txtpaddress.Text = Ds.Tables[0].Rows[0]["PADDRESS"].ToString();
                txtperson.Text = Ds.Tables[0].Rows[0]["FAMILY"].ToString();
                lbladdate.Text = Ds.Tables[0].Rows[0]["ADDATE"].ToString();
            }
            else
            {
                string message = "alert('No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
       
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["POID"] = gr.Cells[0].Text;
        Response.Redirect("~/RECEPTION/reception_police_reciept.aspx");
    }
    public void clear()
    {
        dropbedno.SelectedIndex = 0;
        txtdeathdate.Text = "";
        txtrelationpatient.Text = "";
        txtreligion.Text = "";
        txtnationality.Text = "";
        txtpaddress.Text = "";
        txtperson.Text = "";
        txtcaddress.Text = "";
        txtcasehis.Text = "";
        txtidmark.Text = "";
     

    }
}