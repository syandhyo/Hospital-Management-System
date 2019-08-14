using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
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


public partial class PHARMACYSTORE_pharmacy_purchase_ret_report : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    string fr, to;
    PHARMACYSTORE_pharmacy_purchase_ret_report cr;
    DataMathods OBJ_METHOD = new DataMathods();
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

            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS1.Tables[0].Rows[0]["FYEAR"].ToString();
            }

            if (IsPostBack)
            {
                fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
                to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[5];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@FDATE", SqlDbType.VarChar, 500, fr);
                SQL_PARAMS1[4] = OBJ_METHOD.createParams("@TDATE", SqlDbType.VarChar, 500, to);

                DataSet DS = OBJ_METHOD.Get_DataSet("phrmc_PuReturnRpt", false, true, SQL_PARAMS1);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    DS.Tables[0].TableName = "STOCK";
                    dt1 = DS.Tables["STOCK"];
                    int i = 0, k = 0;
                    ds2.PURCHASEPO.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.PURCHASEPO.Rows.Add(new string[] { dtr["DATE"].ToString(), dtr["INVOICE"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["ITEMNAME"].ToString(), dtr["QTY"].ToString(), dtr["PRICE"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["SGST"].ToString(), dtr["IGST"].ToString(), dtr["GSTAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["RTYPE"].ToString() });
                        k++;
                        i += 1;
                    }
                    cr = new PHARMACYSTORE_pharmacy_purchase_ret_report();
                    rodc.Load(Server.MapPath("~/REPORTS/purchaseret.rpt"));
                    TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                    fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                    TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                    todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
                    TextObject heading = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txt1"];
                    heading.Text = "PURCHASE RETURN REPORT";
                    rodc.SetDataSource(dt1);
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
        cr = new PHARMACYSTORE_pharmacy_purchase_ret_report();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
            to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[5];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@FDATE", SqlDbType.VarChar, 500, fr);
            SQL_PARAMS1[4] = OBJ_METHOD.createParams("@TDATE", SqlDbType.VarChar, 500, to);

            DataSet DS = OBJ_METHOD.Get_DataSet("phrmc_PuReturnRpt", false, true, SQL_PARAMS1);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                DS.Tables[0].TableName = "STOCK";
                dt1 = DS.Tables["STOCK"];
                int i = 0, k = 0;
                ds2.PURCHASEPO.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.PURCHASEPO.Rows.Add(new string[] { dtr["DATE"].ToString(), dtr["INVOICE"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["ITEMNAME"].ToString(), dtr["QTY"].ToString(), dtr["PRICE"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["SGST"].ToString(), dtr["IGST"].ToString(), dtr["GSTAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["RTYPE"].ToString() });
                    k++;
                    i += 1;
                }
                cr = new PHARMACYSTORE_pharmacy_purchase_ret_report();
                rodc.Load(Server.MapPath("~/REPORTS/purchaseret.rpt"));
                TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
                TextObject heading = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txt1"];
                heading.Text = "PURCHASE RETURN REPORT";
                rodc.SetDataSource(dt1);
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
            
        }
    }
}