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

public partial class RECEPTION_reception_lama_form : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
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
        string qry1 = "select ID from LAMA_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("LM{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }

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
            }
            
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_LAMA_PAGE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_LAMA_SELECT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
            }

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BED");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_LAMA_SELECT", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                dropbedno.DataSource = DS2;
                dropbedno.DataTextField = "NAME";
               // dropbedno.DataValueField = "ID";
                dropbedno.DataBind();
                dropbedno.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        #region oldcode
        //using (SqlCommand cmd = new SqlCommand("RECP_LAMA_SELECT", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_LAMA_PAGE";
        //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = "NULL";
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,DATE,IPNO,PNAME,BEDNO FROM LAMA_TABLE ORDER BY ID DESC", con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    GridView1.SelectedIndex = 0;
        //    GridView1.DataSource = dt;
        //    GridView1.DataKeyNames = new string[] { "ID" };
        //    GridView1.DataBind();
        //}
        //using (SqlCommand cmd1 = new SqlCommand("RECP_LAMA_SELECT", con))
        //{
        //    cmd1.CommandType = CommandType.StoredProcedure;
        //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
        //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = "NULL";
        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //    //SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT BEDNO AS NAME FROM BED_TABLE", con);
        //    DataTable dt1 = new DataTable();
        //    da1.Fill(dt1);
        //    //dropbedno.SelectedIndex = 0;
        //    dropbedno.DataSource = dt1;
        //    dropbedno.DataTextField = "NAME";
        //    dropbedno.DataBind();
        //    dropbedno.Items.Insert(0, "Please Select");
        //}

        #endregion
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
            else if (txtgname.Text == "")
            {
                string message = "alert('* Please Enter Guardian Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtgname.Focus();
                return;
            }
           
            auto();

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[9];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, txtdate.Text);
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 200, txtid.Text);
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 200, lblipno.Text);
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 200, lblpname.Text);
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 200, dropbedno.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@GNAME", SqlDbType.VarChar, 200, txtgname.Text);
            SQL_PARAMS2[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS2[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("RECP_LAMA_INSER_UPDATE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

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
            Session["LAID"] = txtid.Text;
            #region oldcode
            //using (SqlCommand cmd1 = new SqlCommand("RECP_LAMA_SELECT", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_IPNO";
            //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = lblipno.Text;
            //    //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //    //SqlCommand com = new SqlCommand("select * from LAMA_TABLE where IPNO='" + lblipno.Text + "'", con);
            //    dr = cmd1.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        string message = "alert('*Lama details for this patient already exist.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;
            //    }
            //    dr.Close();
            //}

            //using (SqlCommand cm = new SqlCommand("RECP_LAMA_INSER_UPDATE", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    //SqlCommand cm = new SqlCommand("INSERT INTO LAMA_TABLE(ID,IPNO,PNAME,BEDNO,DATE,GNAME) VALUES(@ID,@IPNO,@PNAME,@BEDNO,@DATE,@GNAME)", con);
            //    cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = lblipno.Text;
            //    cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = lblpname.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cm.Parameters.Add("@GNAME", SqlDbType.VarChar).Value = txtgname.Text;
            //    cm.ExecuteNonQuery();
            //}


            //binddata();
            #endregion
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
              
           
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
        Response.Redirect("reception_lama_reciept.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
           
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_LAMA_ID");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_LAMA_SELECT", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                    lblipno.Text = Ds.Tables[0].Rows[0]["IPNO"].ToString();
                    lblpname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
                    dropbedno.SelectedItem.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                    txtgname.Text = Ds.Tables[0].Rows[0]["GNAME"].ToString();
       
            }
         
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void dropbedno_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlParameter[] SQL_PARAMS = new SqlParameter[2];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DROP");
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);

        DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_LAMA_SELECT", false, true, SQL_PARAMS);
        if (Ds.Tables[0].Rows.Count > 0)
        {
            lblipno.Text = Ds.Tables[0].Rows[0]["VN"].ToString();
            lblpname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
            txtgname.Text = Ds.Tables[0].Rows[0]["FAMILY"].ToString();
        }
        else
        {
            string message = "alert('No Record Found..')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
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
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 100, slno);
           
            OBJ_METHOD.ExecuteProceedure("RECP_LAMA_INSER_UPDATE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
              
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_LAMA_PAGE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_LAMA_SELECT", false, true, SQL_PARAMS1);
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
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropbedno.Text == "")
            {
                string message = "alert('* Please Select Bed No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropbedno.Focus();
                return;
            }
            else if (txtgname.Text == "")
            {
                string message = "alert('* Please Enter Guardian Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtgname.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[7];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, txtdate.Text);
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 200, txtid.Text);
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 200, lblipno.Text);
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 200, lblpname.Text);
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 200, dropbedno.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@GNAME", SqlDbType.VarChar, 200, txtgname.Text);

            OBJ_METHOD.ExecuteProceedure("RECP_LAMA_INSER_UPDATE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                btncreate.Visible = true;
                btnupdate.Visible = false;
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due to some issues, Data not saved.')";
            }
           
            Session["LAID"] = txtid.Text;
           
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
        Response.Redirect("reception_lama_reciept.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
  
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["LAID"] = gr.Cells[0].Text;
        Response.Redirect("~/RECEPTION/reception_lama_reciept.aspx");
    }
    public void clearcontrol()
    {
        dropbedno.SelectedIndex = 0;
        txtgname.Text = "";
        lblipno.Text = "";
        lblpname.Text = "";
    }
}
