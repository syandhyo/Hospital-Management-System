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

public partial class RECEPTION_CollectionReport : System.Web.UI.Page
{   
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
            try
            {
                using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
                {
                    COM.CommandType = CommandType.StoredProcedure;
                    COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    dr = COM.ExecuteReader();
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
        //SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='OR0001'", con);
        //dr = COM.ExecuteReader();
        //if (dr.Read())
        //{
        //    lblfyear.Text = dr["FYEAR"].ToString();
        //}
        //dr.Close();
        //SqlDataAdapter da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(10),DATE,105) AS DATE,USERID,CAMOUNT FROM USERCOLLECTION WHERE USERID='" + lblid.Text + "' and DATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "'", con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    Button2.Visible = true;
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //}
        //else
        //{
        //    Button2.Visible = false;
        //}
        con.Close();
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "TodaysCollectionReport.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_REPORT_COLLECTION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_COLLECT";
                cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;
                cmd.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(10),DATE,105) AS DATE,USERID,CAMOUNT FROM USERCOLLECTION WHERE USERID='" + lblid.Text + "' and DATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "'", con);
                DataTable dt = new DataTable();
                da.Fill(ds);
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }

            con.Close();

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
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void Show_Click(object sender, EventArgs e)
    {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtfrom.Text == "00.00")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtto.Text == "00.00")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            try
            {
           // String FromDate = DateTime.Now.ToString("yyyy-MM-dd ") + "" + txtfrom.Text + "";
            //String ToDate = DateTime.Now.ToString("yyyy-MM-dd ") + "" + txtto.Text + "";
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                String FromDate = DateTime.Parse(date + " " + txtfrom.Text).ToString();
                String ToDate = DateTime.Parse(date + " " + txtto.Text).ToString();

           //// SqlDataAdapter da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(10),DATE,105) AS DATE,USERID,CAMOUNT FROM USERCOLLECTION WHERE USERID='" + lblid.Text + "' and DATE BETWEEN'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "' AND '" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'", con);
                using (SqlCommand cmd = new SqlCommand("RECP_REPORT_COLLECTION", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_COLLECT_DATE";
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;
                    cmd.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = FromDate;
                    cmd.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = ToDate;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //SqlDataAdapter da = new SqlDataAdapter("SELECT DATE,USERID,CAMOUNT FROM USERCOLLECTION WHERE USERID='" + lblid.Text + "' and DATE BETWEEN'" + FromDate + "'  AND '" + ToDate + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {

                        string message = "alert(' No Record Found')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                    else
                    {
                        GridView1.SelectedIndex = 0;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    //da.Fill(ds);
                    //GridView1.DataSource = ds.Tables[0];
                    //GridView1.DataBind();

                    //Calculate Sum and display in Footer Row
                    decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("CAMOUNT"));
                    GridView1.FooterRow.Cells[1].Text = "Total";
                    GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[2].Text = total.ToString("N2");

                    if (dt.Rows.Count == 0)
                    {
                        Button2.Visible = false;
                    }
                    else
                    {
                        Button2.Visible = true;
                    }
                }
           }
            catch (Exception ex)
            {
               // Response.Write(ex.Message);
               Console.WriteLine("An error occurred: '{0}'", ex);
            }
           
            con.Close();
                    
        
       
    }
}