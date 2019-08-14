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

public partial class PHARMACYSTORE_pharmacy_stock : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    PHARMACYSTORE_pharmacy_stock cr;
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

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
            if (!IsPostBack)
            {
                DataSet Ds = OBJ_METHOD.Get_DataSet("select distinct NAME FROM ITEM_TABLE where ORGID='" + lblorgid.Text + "' AND Branch_ID='" + Session["Branch"] + "'", false, false);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    DropDownList1.DataSource = Ds;
                    DropDownList1.DataTextField = "NAME";
                    DropDownList1.DataValueField = "NAME";
                    DropDownList1.DataBind();
                }
                //da1 = new SqlDataAdapter("select distinct NAME FROM ITEM_TABLE where ORGID='" + lblorgid.Text + "'", con);
                //DataTable ds1 = new DataTable();
                //da1.Fill(ds1);
                //DropDownList1.DataSource = ds1;
                //DropDownList1.DataTextField = "NAME";
                //DropDownList1.DataValueField = "NAME";
                //DropDownList1.DataBind();
            }

            if (IsPostBack)
            {

                if (Session["B1"] == "TRUE")
                {
                    DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.COMPANY AS COMPANY,A.NAME AS NAME,A.EXPDATE AS EXPDATE,A.QTY AS QTY,B.PPRICE AS PRICE FROM STOCK_TABLE A,ITEM_TABLE B WHERE A.COMPANY =B.COMPANY AND A.NAME=B.NAME  AND A.BATCHNO=B.BATCHNO AND A.NAME='" + DropDownList1.Text + "' AND A.ORGID='" + lblorgid.Text + "' And A.Branch_ID=B.Branch_ID AND A.Branch_ID='" + Session["Branch"] + "'", false, false);
                    DataTable dt1 = new DataTable();
                    Ds.Tables[0].TableName = "123";
                    dt1 = Ds.Tables["123"];
                    int i = 0;
                    ds2.STOCK_RE.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.STOCK_RE.Rows.Add(new string[] { dtr["COMPANY"].ToString(), dtr["NAME"].ToString(), dtr["EXPDATE"].ToString(), dtr["QTY"].ToString(), dtr["PRICE"].ToString() });
                        i += 1;
                    }
                    cr = new PHARMACYSTORE_pharmacy_stock();
                    rodc.Load(Server.MapPath("~/REPORTS/stock.rpt"));
                    rodc.SetDataSource(dt1);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();
                    //da1 = new SqlDataAdapter("SELECT A.COMPANY AS COMPANY,A.NAME AS NAME,A.EXPDATE AS EXPDATE,A.QTY AS QTY,B.PPRICE AS PRICE FROM STOCK_TABLE A,ITEM_TABLE B WHERE A.COMPANY =B.COMPANY AND A.NAME=B.NAME  AND A.BATCHNO=B.BATCHNO AND A.NAME='" + DropDownList1.Text + "' AND A.ORGID='" + lblorgid.Text + "'", con);
                }
                else
                {
                    DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.COMPANY AS COMPANY,A.NAME AS NAME,A.EXPDATE AS EXPDATE,A.QTY AS QTY,B.PPRICE AS PRICE FROM STOCK_TABLE A,ITEM_TABLE B WHERE A.COMPANY =B.COMPANY AND A.NAME=B.NAME  AND A.BATCHNO=B.BATCHNO AND A.ORGID='" + lblorgid.Text + "' And A.Branch_ID=B.Branch_ID AND A.Branch_ID='" + Session["Branch"] + "'", false, false);
                    DataTable dt1 = new DataTable();
                    Ds.Tables[0].TableName = "123";
                    dt1 = Ds.Tables["123"];
                    int i = 0;
                    ds2.STOCK_RE.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.STOCK_RE.Rows.Add(new string[] { dtr["COMPANY"].ToString(), dtr["NAME"].ToString(), dtr["EXPDATE"].ToString(), dtr["QTY"].ToString(), dtr["PRICE"].ToString() });
                        i += 1;
                    }
                    cr = new PHARMACYSTORE_pharmacy_stock();
                    rodc.Load(Server.MapPath("~/REPORTS/stock.rpt"));
                    rodc.SetDataSource(dt1);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();
                    //da1 = new SqlDataAdapter("SELECT A.COMPANY AS COMPANY,A.NAME AS NAME,A.EXPDATE AS EXPDATE,A.QTY AS QTY,B.PPRICE AS PRICE FROM STOCK_TABLE A,ITEM_TABLE B WHERE A.COMPANY =B.COMPANY AND A.NAME=B.NAME  AND A.BATCHNO=B.BATCHNO AND A.ORGID='" + lblorgid.Text + "'", con);
                }
                //da1 = new SqlDataAdapter("SELECT A.COMPANY AS COMPANY,A.NAME AS NAME,CONVERT(Date, A.EXPDATE, 121) AS EXPDATE,A.QTY AS QTY,B.PPRICE AS PRICE FROM STOCK_TABLE A,ITEM_TABLE B WHERE A.COMPANY =B.COMPANY AND A.NAME=B.NAME AND A.EXPDATE = B.EXPDATE AND A.ORGID=B.ORGID AND A.ORGID='"+lblorgid.Text+"'", con);
                

            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.COMPANY AS COMPANY,A.NAME AS NAME,A.EXPDATE AS EXPDATE,A.QTY AS QTY,B.PPRICE AS PRICE FROM STOCK_TABLE A,ITEM_TABLE B WHERE A.COMPANY =B.COMPANY AND A.NAME=B.NAME  AND A.BATCHNO=B.BATCHNO AND A.NAME='" + DropDownList1.Text + "' AND A.ORGID='" + lblorgid.Text + "' And A.Branch_ID=B.Branch_ID AND A.Branch_ID='" + Session["Branch"] + "'", false, false);
            DataTable dt1 = new DataTable();
            Ds.Tables[0].TableName = "123";
            dt1 = Ds.Tables["123"];
            //da1 = new SqlDataAdapter("SELECT A.COMPANY AS COMPANY,A.NAME AS NAME,A.EXPDATE AS EXPDATE,A.QTY AS QTY,B.PPRICE AS PRICE FROM STOCK_TABLE A,ITEM_TABLE B WHERE A.COMPANY =B.COMPANY AND A.NAME=B.NAME  AND A.BATCHNO=B.BATCHNO AND A.NAME='" + DropDownList1.Text + "' AND A.ORGID='" + lblorgid.Text + "'", con);
            //ds = new DataSet();
            //da1.Fill(ds, "123");
            //dt1 = ds.Tables["123"];
            int i = 0;
            ds2.STOCK_RE.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.STOCK_RE.Rows.Add(new string[] { dtr["COMPANY"].ToString(), dtr["NAME"].ToString(), dtr["EXPDATE"].ToString(), dtr["QTY"].ToString(), dtr["PRICE"].ToString() });
                i += 1;
            }
            cr = new PHARMACYSTORE_pharmacy_stock();
            rodc.Load(Server.MapPath("~/REPORTS/stock.rpt"));
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();

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
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.COMPANY AS COMPANY,A.NAME AS NAME,A.EXPDATE AS EXPDATE,A.QTY AS QTY,B.PPRICE AS PRICE FROM STOCK_TABLE A,ITEM_TABLE B WHERE A.COMPANY =B.COMPANY AND A.NAME=B.NAME  AND A.ORGID=B.ORGID AND A.ORGID='" + lblorgid.Text + "' And A.Branch_ID=B.Branch_ID AND A.Branch_ID='" + Session["Branch"] + "'", false, false);
            DataTable dt1 = new DataTable();
            Ds.Tables[0].TableName = "123";
            dt1 = Ds.Tables["123"];
            //da1 = new SqlDataAdapter("SELECT A.COMPANY AS COMPANY,A.NAME AS NAME,A.EXPDATE AS EXPDATE,A.QTY AS QTY,B.PPRICE AS PRICE FROM STOCK_TABLE A,ITEM_TABLE B WHERE A.COMPANY =B.COMPANY AND A.NAME=B.NAME  AND A.ORGID=B.ORGID AND A.ORGID='" + lblorgid.Text + "'", con);
            //ds = new DataSet();
            //da1.Fill(ds, "123");
            //dt1 = ds.Tables["123"];
            int i = 0;
            ds2.STOCK_RE.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.STOCK_RE.Rows.Add(new string[] { dtr["COMPANY"].ToString(), dtr["NAME"].ToString(), dtr["EXPDATE"].ToString(), dtr["QTY"].ToString(), dtr["PRICE"].ToString() });
                i += 1;
            }
            cr = new PHARMACYSTORE_pharmacy_stock();
            rodc.Load(Server.MapPath("~/REPORTS/stock.rpt"));
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();

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
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        
        cr = new PHARMACYSTORE_pharmacy_stock();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
       
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
            
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            
        }
    }
}