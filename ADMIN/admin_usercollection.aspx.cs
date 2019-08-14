using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
public partial class ADMIN_admin_usercollection : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
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

        SqlParameter[] SQL_PARAMS = new SqlParameter[1];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

        DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
        if (DS.Tables[0].Rows.Count > 0)
        {
            lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
        }

        if (!IsPostBack)
        {
            DataSet dt1 = OBJ_METHOD.Get_DataSet("SELECT DISTINCT USERID FROM USERCOLLECTION", false,false);
            if (dt1.Tables[0].Rows.Count > 0)
            {
                dropuser.DataSource = dt1;
                dropuser.DataTextField = "USERID";
                dropuser.DataBind();
                dropuser.Items.Insert(0, new ListItem("Please Select","0"));
            }
        }

    
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "UserCollectionReport.xls"));
                Response.ContentType = "application/ms-excel";

                //Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GridView1.AllowPaging = false;
               
                SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, dropuser.SelectedItem.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 500, txtfromdate.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 500, txttodate.Text);

                DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_USER_COLLECTION", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = DS;
                    GridView1.DataBind();
                    if (DS.Tables[0].Rows.Count == 0)
                    {
                        Button2.Visible = false;
                    }
                    else
                    {
                        Button2.Visible = true;
                    }
                }

                GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
                //Applying stlye to gridview header cells
                for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
                {
                    GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                }
                GridView1.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }

        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Showing.')";
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtfromdate.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfromdate.Focus();
                return;
            }
            else if (txttodate.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txttodate.Focus();
                return;
            }
            else if (dropuser.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropuser.Focus();
                return;
            }
             
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500,"DISPLAY");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, dropuser.SelectedItem.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 500, txtfromdate.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 500, txttodate.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_USER_COLLECTION", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS;
                GridView1.DataBind();
                if (DS.Tables[0].Rows.Count == 0)
                {
                    Button2.Visible = false;
                }
                else
                {
                    Button2.Visible = true;
                }
            }
            //Calculate Sum and display in Footer Row
            decimal total = DS.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CAMOUNT"));
            GridView1.FooterRow.Cells[3].Text = "Total";
            GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            GridView1.FooterRow.Cells[4].Text = total.ToString("N2");


        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Showing.')";
        }
    }
}