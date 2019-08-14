using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

public partial class PHARMACYSTORE_USER_DebitCreditRport : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds = new DataSet();
    SqlCommand com, cmd;
    SqlDataReader dr;
    DataTable dt;
    DataRow dtr;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
            lbluid.Text = Session["UID"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();
            if (!IsPostBack)
            {
                binddata();
            }
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataReader dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnDebitShow_Click(object sender, EventArgs e)
    {
        try
        {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SA_TABLE  where INVDATE between '" + Convert.ToDateTime(txt_Fdate.Text).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(txt_Todate.Text).ToString("yyyy-MM-dd") + "' and BAMT=0", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count == 0)
        {
            btn_Download.Visible = false;
            string message = "alert(' No Record Found')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        else
        {
            Session["mode"] = "true";

            btn_Download.Visible = true;
           // lblDebit.Visible = true;
           // lblDebit.Text = "Debit Bill";
            grdd_show.DataSource = dt;
            grdd_show.DataBind();
        }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void btnCredit_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM SA_TABLE  where INVDATE between '" + Convert.ToDateTime(txt_Fdate.Text).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(txt_Todate.Text).ToString("yyyy-MM-dd") + "' and BAMT!=0", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count == 0)
            {
                btn_Download.Visible = false;
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                Session["mode"] = "false";
                btn_Download.Visible = true;
                //  lblCredit.Visible = true;
                //  lblCredit.Text = "Credit Bill";
                grdd_show.DataSource = dt1;
                grdd_show.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void grd_credit_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    GridView HeaderGrid = (GridView)sender;
        //    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell HeaderCell = new TableCell();
        //    HeaderCell.Text = "CREDIT BILL";
        //    HeaderCell.ColumnSpan = 12;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    //HeaderCell = new TableCell();
        //    //HeaderCell.Text = "Employee";
        //    //HeaderCell.ColumnSpan = 2;
        //    //HeaderGridRow.Cells.Add(HeaderCell);

        //    grd_credit.Controls[0].Controls.AddAt(0, HeaderGridRow);
        //}

    }
   
    protected void btn_Download_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdd_show.Rows.Count > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "opconsutancy.xls"));
                Response.ContentType = "application/ms-excel";

                //Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grdd_show.AllowPaging = false;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                con.Open();
                if (Session["mode"] == "true")
                {
                    SqlDataAdapter Adp = new SqlDataAdapter("SELECT * FROM SA_TABLE  where INVDATE between '" + Convert.ToDateTime(txt_Fdate.Text).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(txt_Todate.Text).ToString("yyyy-MM-dd") + "' and BAMT=0", con);

                    DataTable Dt = new DataTable();
                    Adp.Fill(ds);
                    grdd_show.DataSource = ds.Tables[0];
                    grdd_show.DataBind();
                }
                else
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM SA_TABLE  where INVDATE between '" + Convert.ToDateTime(txt_Fdate.Text).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(txt_Todate.Text).ToString("yyyy-MM-dd") + "' and BAMT!=0", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    grdd_show.DataSource = dt1;
                    grdd_show.DataBind();
                }

                con.Close();

                grdd_show.HeaderRow.Style.Add("background-color", "#FFFFFF");
                //Applying stlye to gridview header cells
                for (int i = 0; i < grdd_show.HeaderRow.Cells.Count; i++)
                {
                    grdd_show.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                }
                grdd_show.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
     
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
}