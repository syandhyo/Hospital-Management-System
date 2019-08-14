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

public partial class PHARMACYSTORE_pharmacy_expiry_report : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    PHARMACYSTORE_pharmacy_expiry_report cr;
    DataMathods OBJ_METHOD = new DataMathods();
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

        if (TextBox1.Text == "")
        {
            string message = "alert('* Fields are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        else if (TextBox2.Text == "")
        {
            string message = "alert('* Fields are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }

        if (IsPostBack)
        {
            fr = Convert.ToDateTime(TextBox1.Text).ToString("dd-MM-yyyy");
            to = Convert.ToDateTime(TextBox2.Text).ToString("dd-MM-yyyy");
            SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@FDATE", SqlDbType.VarChar, 500, fr);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@TDATE", SqlDbType.VarChar, 500, to);

            DataSet Ds = OBJ_METHOD.Get_DataSet("phrmc_ProdExpRpt", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                Ds.Tables[0].TableName = "123";
                dt1 = Ds.Tables["123"];
                int i = 0;
                ds2.STOCK_RE.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.STOCK_RE.Rows.Add(new string[] { dtr["COMPANY"].ToString(), dtr["NAME"].ToString(), dtr["EXPDATE"].ToString(), dtr["QTY"].ToString() });
                    i += 1;
                }
                cr = new PHARMACYSTORE_pharmacy_expiry_report();
                rodc.Load(Server.MapPath("~/REPORTS/Expreport.rpt"));
                TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();
            }
        
            //using (SqlCommand cmd = new SqlCommand("phrmc_ProdExpRpt", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@FDATE", SqlDbType.VarChar).Value = fr;
            //    cmd.Parameters.Add("@TDATE", SqlDbType.VarChar).Value = to;
            //    da1 = new SqlDataAdapter(cmd);
            //    ds = new DataSet();
            //    da1.Fill(ds, "123");
            //    dt1 = ds.Tables["123"];
            //    int i = 0;
            //    ds2.STOCK_RE.Rows.Clear();
            //    while (i < dt1.Rows.Count)
            //    {
            //        dtr = dt1.Rows[i];
            //        ds2.STOCK_RE.Rows.Add(new string[] { dtr["COMPANY"].ToString(), dtr["NAME"].ToString(), dtr["EXPDATE"].ToString(), dtr["QTY"].ToString() });
            //        i += 1;
            //    }
            //    cr = new PHARMACYSTORE_pharmacy_expiry_report();
            //    rodc.Load(Server.MapPath("~/REPORTS/Expreport.rpt"));
            //    TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
            //    fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
            //    TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
            //    todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
            //    rodc.SetDataSource(dt1);
            //    CrystalReportViewer1.ReportSource = rodc;
            //    CrystalReportViewer1.DataBind();
            //}

        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new PHARMACYSTORE_pharmacy_expiry_report();
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
        if (TextBox1.Text == "")
        {
            string message = "alert('* Fields are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        else if (TextBox2.Text == "")
        {
            string message = "alert('* Fields are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        fr = Convert.ToDateTime(TextBox1.Text).ToString("dd-MM-yyyy");
        to = Convert.ToDateTime(TextBox2.Text).ToString("dd-MM-yyyy");
        SqlParameter[] SQL_PARAMS = new SqlParameter[5];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
        SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
        SQL_PARAMS[3] = OBJ_METHOD.createParams("@FDATE", SqlDbType.VarChar, 500, fr);
        SQL_PARAMS[4] = OBJ_METHOD.createParams("@TDATE", SqlDbType.VarChar, 500, to);

        DataSet Ds = OBJ_METHOD.Get_DataSet("phrmc_ProdExpRpt", false, true, SQL_PARAMS);
        if (Ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt1 = new DataTable();
            Ds.Tables[0].TableName = "123";
            dt1 = Ds.Tables["123"];
            int i = 0;
            ds2.STOCK_RE.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.STOCK_RE.Rows.Add(new string[] { dtr["COMPANY"].ToString(), dtr["NAME"].ToString(), dtr["EXPDATE"].ToString(), dtr["QTY"].ToString() });
                i += 1;
            }
            cr = new PHARMACYSTORE_pharmacy_expiry_report();
            rodc.Load(Server.MapPath("~/REPORTS/Expreport.rpt"));
            TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
            fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
            TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
            todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");

            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();
        }
        con.Close();
        btnPrint.Visible = true;
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