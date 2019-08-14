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
using System.IO;

public partial class RECEPTION_opconreciept : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    SqlDataReader dr, dr1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    RECEPTION_opconreciept cr;
    string fr, to;
    public class ReportFactory
    {
        protected static Queue reportQueue = new Queue();

        protected static ReportClass CreateReport(Type reportClass)
        {
            object report = Activator.CreateInstance(reportClass);
            reportQueue.Enqueue(report);
            return (ReportClass)report;
        }

        public static ReportClass GetReport(Type reportClass)
        {

            //75 is my print job limit.
            if (reportQueue.Count > 75) ((ReportClass)reportQueue.Dequeue()).Dispose();
            return CreateReport(reportClass);
        }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
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
        string S = Session["CON"].ToString();

        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        dr1 = COM.ExecuteReader();
        if (dr1.Read())
        {
            lblfyear.Text = dr1["FYEAR"].ToString();
        }
        dr1.Close();


        da = new SqlDataAdapter(" select  b.CDate as DATE,B.OPNo AS OPNO,C.NAME AS OPNAME,A.Sname AS SNAME,B.fee AS FEE,B.id AS ID,D.NAME AS ONAME,D.ADDRESS AS ADDRESS,D.PHONE+','+D.PHONE2 AS PHONE from ORG_TABLE D, tblStaff A, tblOPConsultancy b,PATIENT_REG_TABLE C WHERE B.OPNo=C.ID AND B.Staffid=A.id AND B.id='" + S.ToString() + "'", con);
        ds = new DataSet();
        DataTable dt1 = new DataTable();
        da.Fill(ds, "STOCK");
        dt1 = ds.Tables["STOCK"];
        int i = 0, k = 0;
        ds2.PR.Rows.Clear();
        while (i < dt1.Rows.Count)
        {
            dtr = dt1.Rows[i];
            ds2.PR.Rows.Add(new string[] { dtr["DATE"].ToString(), dtr["OPNO"].ToString(), dtr["OPNAME"].ToString(), dtr["SNAME"].ToString(), dtr["FEE"].ToString(), dtr["ID"].ToString(), dtr["ONAME"].ToString(), dtr["ADDRESS"].ToString(), dtr["PHONE"].ToString() });
            k++;
            i += 1;
        }
       
        rodc.Load(Server.MapPath("~/REPORTS/con_reciept.rpt"));
        rodc.SetDataSource(dt1);
        CrystalReportViewer1.ReportSource = rodc;
        CrystalReportViewer1.DataBind();
        con.Close();
     
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        
        CloseReport();
        //if (rodc != null)
        //{
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
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
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            cr.rodc.Close();
            cr.rodc.Clone();
            cr.rodc.Dispose();
            rodc = null;
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //return;
            // Response.Write("<script LANGUAGE='JavaScript' >alert('connect printer settings')</script>");
        }
    }
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
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        CloseReport();

        cr = new RECEPTION_opconreciept();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();

    }
}