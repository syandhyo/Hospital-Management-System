using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System.Drawing.Printing;

public partial class GENERALSTOCK_DeptWiseStockRpt : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet2 ds2 = new DataSet2();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    GENERALSTOCK_DeptWiseStockRpt cr;
    string fr, to;

    protected void CloseReport()
    {
        try
        {
            if (rodc != null)
            {
                Sections objSections = rodc.ReportDefinition.Sections;
                foreach (Section objSection in objSections)
                {
                    ReportObjects objReports = objSection.ReportObjects;
                    foreach (ReportObject rptObj in objReports)
                    {
                        if (rptObj.Kind.Equals(CrystalDecisions.Shared.ReportObjectKind.SubreportObject))
                        {
                            SubreportObject subreportObject = (SubreportObject)rptObj;
                            ReportDocument subReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName);
                            subReportDocument.Close();
                        }
                    }
                }
                rodc.Close();
                rodc.Dispose();
                cr.rodc.Close();
                cr.rodc.Clone();
                cr.rodc.Dispose();
                rodc = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            if (CrystalReportViewer1 != null)
            {
                CrystalReportViewer1.ReportSource = null;
                CrystalReportViewer1.Dispose();
                cr.rodc.Close();
                cr.rodc.Clone();
                cr.rodc.Dispose();
                rodc = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
        }
        catch
        {

        }

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
        lblorgid.Text = Session["ORGID"].ToString();

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

        if (!IsPostBack)
        {
            da = new SqlDataAdapter("select distinct id,DeptName AS NAME FROM tblDepartment ", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropDept.DataSource = ds;
            dropDept.DataTextField = "NAME";
            dropDept.DataValueField = "id";
            dropDept.DataBind();
            dropDept.Items.Insert(0, "-----Select-----");
            //----------------------------------------------
            da = new SqlDataAdapter("select distinct SLNO,ITEMNAME AS NAME FROM TBL_DEPT_MAT_STOCK ", con);
            DataTable ds1 = new DataTable();
            da.Fill(ds1);
            dropitem.DataSource = ds1;
            dropitem.DataTextField = "NAME";
            dropitem.DataValueField = "SLNO";
            dropitem.DataBind();
            dropitem.Items.Insert(0, "-----Select-----");
        }

        if (IsPostBack)
        {
            if (dropDept.SelectedIndex == 0)
            {
                string message = "alert('*Please select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
           // fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
            //to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
            if (Session["B1"] == "TRUE")
            {
                da1 = new SqlDataAdapter("SELECT a.ITEMNAME,a.STOCK,b.DeptName,c.UNIT FROM TBL_DEPT_MAT_STOCK a, tblDepartment b,TBL_DEPT_MAT_MST C where a.ID=b.id and a.ID=c.DEPTNAME and b.id='" + dropDept.SelectedValue + "' and a.ITEMNAME='" + dropitem.SelectedItem.Text + " ' AND A.ITEMNAME=C.ITEMNAME", con);
            }
            else
            {
                da1 = new SqlDataAdapter("SELECT a.ITEMNAME,a.STOCK,b.DeptName,c.UNIT FROM TBL_DEPT_MAT_STOCK a, tblDepartment b,TBL_DEPT_MAT_MST C where a.ID=b.id and a.ID=c.DEPTNAME and b.id='" + dropDept.SelectedValue + "' AND A.ITEMNAME=C.ITEMNAME", con);
            }           

            ds = new DataSet();
            da1.Fill(ds, "123");
            dt1 = ds.Tables["123"];
            int i = 0;
            ds2.DEPTWISE_STOCK_1.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.DEPTWISE_STOCK_1.Rows.Add(new string[] { dtr["ITEMNAME"].ToString(), dtr["STOCK"].ToString(), dtr["DeptName"].ToString(), dtr["UNIT"].ToString() });
                i += 1;
            }
            cr = new GENERALSTOCK_DeptWiseStockRpt();
            rodc.Load(Server.MapPath("~/MATREPORT/DeptWiseStock.rpt"));
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();
        }
        con.Close();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        CloseReport();
        //if (rodc != null)
        //{
        cr = new GENERALSTOCK_DeptWiseStockRpt();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropDept.SelectedIndex == 0)
            {
                string message = "alert('*Please select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropitem.SelectedIndex == 0)
            {
                string message = "alert('*Please select Item..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();


            //  fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
            // to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
           // da1 = new SqlDataAdapter("SELECT a.ITEMNAME,a.STOCK,b.DeptName,c.UNIT FROM TBL_DEPT_MAT_STOCK a, tblDepartment b,TBL_DEPT_MAT_MST C where a.ID=b.id and a.ID=c.DEPTNAME and b.id='" + dropDept.SelectedValue + "' and a.ITEMNAME='" + dropitem.SelectedItem.Text + "' AND A.ITEMNAME=C.ITEMNAME", con);

            //ds = new DataSet();
            //da1.Fill(ds, "123");
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_StockRpt", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPSHOW";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = dropDept.SelectedValue;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropitem.SelectedItem.Text;              
                da1 = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da1.Fill(ds, "123");
                dt1 = ds.Tables["123"];
                int i = 0;
                ds2.DEPTWISE_STOCK_1.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.DEPTWISE_STOCK_1.Rows.Add(new string[] { dtr["ITEMNAME"].ToString(), dtr["STOCK"].ToString(), dtr["DeptName"].ToString(), dtr["UNIT"].ToString() });
                    i += 1;
                }
                cr = new GENERALSTOCK_DeptWiseStockRpt();
                rodc.Load(Server.MapPath("~/MATREPORT/DeptWiseStock.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();
            }
            Session["B1"] = "TRUE";
            Session["B2"] = "FALSE";
            con.Close();
            btnPrint.Visible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    string GetDefaultPrinter()
    {
        PrinterSettings settings = new PrinterSettings();
        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            settings.PrinterName = printer;
            if (settings.IsDefaultPrinter)
                return printer;
        }
        return string.Empty;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {

            rodc.PrintOptions.PrinterName = GetDefaultPrinter();
            rodc.PrintToPrinter(1, false, 0, 0);
            //rodc1.PrintOptions.PrinterName = GetDefaultPrinter();
            // rodc1.PrintToPrinter(1, false, 0, 0);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //return;
            // Response.Write("<script LANGUAGE='JavaScript' >alert('connect printer settings')</script>");
        }
    }
    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropDept.SelectedIndex == 0)
            {
                string message = "alert('*Please select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //  fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
            // to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
            da1 = new SqlDataAdapter("SELECT a.ITEMNAME,a.STOCK,b.DeptName,c.UNIT FROM TBL_DEPT_MAT_STOCK a, tblDepartment b,TBL_DEPT_MAT_MST C where a.ID=b.id and a.ID=c.DEPTNAME and b.id='" + dropDept.SelectedValue + "' AND A.ITEMNAME=C.ITEMNAME", con);
           // ds = new DataSet();
           // da1.Fill(ds, "123");
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_StockRpt", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPSHOWALL";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = dropDept.SelectedValue;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                da1 = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da1.Fill(ds, "123");
                dt1 = ds.Tables["123"];
                int i = 0;
                ds2.DEPTWISE_STOCK_1.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.DEPTWISE_STOCK_1.Rows.Add(new string[] { dtr["ITEMNAME"].ToString(), dtr["STOCK"].ToString(), dtr["DeptName"].ToString(), dtr["UNIT"].ToString() });
                    i += 1;
                }
                cr = new GENERALSTOCK_DeptWiseStockRpt();
                rodc.Load(Server.MapPath("~/MATREPORT/DeptWiseStock.rpt"));
                //TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                // fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                // TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                //  todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
                rodc.SetDataSource(dt1);

                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();
            }
            Session["B1"] = "FALSE";
            Session["B2"] = "TRUE";
            con.Close();
            btnPrint.Visible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}