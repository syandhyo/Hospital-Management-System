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

public partial class PHARMACYSTORE_USER_stock_ledger_report : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    PHARMACYSTORE_USER_stock_ledger_report cr;
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
                da = new SqlDataAdapter("select distinct ITEMNAME AS NAME FROM STOCK_TRAN where ORGID='" + lblorgid.Text + "'", con);
                DataTable ds = new DataTable();
                da.Fill(ds);
                dropitem.DataSource = ds;
                dropitem.DataTextField = "NAME";
                dropitem.DataValueField = "NAME";
                dropitem.DataBind();
                dropitem.Items.Insert(0, "-----Select-----");
            }

            if (IsPostBack)
            {
                fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
                to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
                //da1 = new SqlDataAdapter("select A.FYEAR,CONVERT(VARCHAR(10),A.DATE,103) AS DATE,A.ITEMNAME,[EXP] AS EXP,A.OPENING,A.PURCHASE ,A.PRETURN,A.SALE,A.SRETURN,A.CLOSEING,( SELECT SUM(QTY) AS QTY FROM STOCK_TABLE WHERE NAME='" + dropitem.Text + "' GROUP BY NAME) AS QTY from STOCK_TRAN A,STOCK_TABLE B where A.DATE BETWEEN '" + fr + "' AND '" + to + "' AND A.ITEMNAME='" + dropitem.Text + "' AND A.ITEMNAME=B.NAME AND A.EXP=B.EXPDATE AND A.ORGID='" + lblorgid.Text + "' order by CONVERT(datetime, [DATE] ) ASC ", con);

                //ds = new DataSet();
                //da1.Fill(ds, "123");
                //-------------------------
                using (SqlCommand cmd = new SqlCommand("phrmc_StokLedgerRpt", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    cmd.Parameters.Add("@FDATE", SqlDbType.VarChar).Value = fr;
                    cmd.Parameters.Add("@TDATE", SqlDbType.VarChar).Value = to;
                    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropitem.Text;
                    da1 = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da1.Fill(ds, "123");
                    dt1 = ds.Tables["123"];
                    int i = 0;
                    ds2.STOCKTRAN.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.STOCKTRAN.Rows.Add(new string[] { dtr["FYEAR"].ToString(), dtr["DATE"].ToString(), dtr["ITEMNAME"].ToString(), dtr["EXP"].ToString(), dtr["OPENING"].ToString(), dtr["PURCHASE"].ToString(), dtr["PRETURN"].ToString(), dtr["SALE"].ToString(), dtr["SRETURN"].ToString(), dtr["CLOSEING"].ToString(), dtr["QTY"].ToString() });
                        i += 1;
                    }
                    cr = new PHARMACYSTORE_USER_stock_ledger_report();
                    rodc.Load(Server.MapPath("~/REPORTS/stocktran.rpt"));
                    TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                    fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                    TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                    todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
                    rodc.SetDataSource(dt1);
                    //rodc.SetParameterValue("TextBox1Text1", TextBox1.Text);
                    //rodc.SetParameterValue("TextBox1Text1", TextBox2.Text);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();
                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new PHARMACYSTORE_USER_stock_ledger_report();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (dropitem.SelectedIndex==0)
            {
                string message = "alert('* Please Choose Item.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropitem.Focus();
                return;
            }
            if (TextBox1.Text=="")
            {
                string message = "alert('* Please Enter From Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
               // TextBox1.Focus();
                return;
            }
            if (TextBox2.Text=="")
            {
                string message = "alert('* Please Enter To Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
               // TextBox2.Focus();
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
            to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
            //da1 = new SqlDataAdapter("select A.FYEAR,CONVERT(VARCHAR(10),A.DATE,103) AS DATE,A.ITEMNAME,[EXP] AS EXP,A.OPENING,A.PURCHASE ,A.PRETURN,A.SALE,A.SRETURN,A.CLOSEING,( SELECT SUM(QTY) AS QTY FROM STOCK_TABLE WHERE NAME='" + dropitem.Text + "' GROUP BY NAME) AS QTY from STOCK_TRAN A,STOCK_TABLE B where A.DATE BETWEEN '" + fr + "' AND '" + to + "' AND A.ITEMNAME='" + dropitem.Text + "' AND A.ITEMNAME=B.NAME AND A.EXP=B.EXPDATE AND A.ORGID='" + lblorgid.Text + "' order by CONVERT(datetime, [DATE] ) ASC ", con);

            //ds = new DataSet();
            //da1.Fill(ds, "123");
            //--------------------------------
            using (SqlCommand cmd = new SqlCommand("phrmc_StokLedgerRpt", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@FDATE", SqlDbType.VarChar).Value = fr;
                cmd.Parameters.Add("@TDATE", SqlDbType.VarChar).Value = to;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropitem.Text;
                da1 = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da1.Fill(ds, "123");
                dt1 = ds.Tables["123"];
                int i = 0;
                ds2.STOCKTRAN.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.STOCKTRAN.Rows.Add(new string[] { dtr["FYEAR"].ToString(), dtr["DATE"].ToString(), dtr["ITEMNAME"].ToString(), dtr["EXP"].ToString(), dtr["OPENING"].ToString(), dtr["PURCHASE"].ToString(), dtr["PRETURN"].ToString(), dtr["SALE"].ToString(), dtr["SRETURN"].ToString(), dtr["CLOSEING"].ToString(), dtr["QTY"].ToString() });
                    i += 1;
                }
                cr = new PHARMACYSTORE_USER_stock_ledger_report();
                rodc.Load(Server.MapPath("~/REPORTS/stocktran.rpt"));
                TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
                rodc.SetDataSource(dt1);
                //rodc.SetParameterValue("TextBox1Text1", TextBox1.Text);
                //rodc.SetParameterValue("TextBox1Text1", TextBox2.Text);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();
            }
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
}