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

public partial class RECEPTION_Reception_advancebill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    SqlDataReader dr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    RECEPTION_Reception_advancebill cr;
    string fr, to;
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
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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
            string S = Session["ADID"].ToString();
            binddata();

          SqlParameter[] SQL_PARAMS = new SqlParameter[1];

          SQL_PARAMS[0] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, S.ToString());


        DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_ADVANCE_BILL_REPORT", false, true, SQL_PARAMS);
        if (DS2.Tables[0].Rows.Count > 0)
        {
            DataTable dt1 = new DataTable();
            DS2.Tables[0].TableName = "STOCK";
            dt1 = DS2.Tables["STOCK"];
            int i = 0, k = 0;
            ds2.PAYMENT.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.PAYMENT.Rows.Add(new string[] { dtr["ID"].ToString(), dtr["DATE"].ToString(), dtr["PID"].ToString(), dtr["BEDNO"].ToString(), dtr["AMOUNT"].ToString(), dtr["HNAME"].ToString(), dtr["PHONE"].ToString(), dtr["ADDRESS"].ToString(), dtr["GST"].ToString() });
                k++;
                i += 1;
            }
            cr = new RECEPTION_Reception_advancebill();
            rodc.Load(Server.MapPath("~/REPORTS/paymentreciept.rpt"));
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();
        }
            //using (SqlCommand cmd = new SqlCommand("RECP_ADVANCE_BILL_REPORT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = S.ToString();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //da = new SqlDataAdapter("SELECT A.id AS ID,A.Adate AS DATE,A.PID+','+B.NAME AS PID,A.Bedno AS BEDNO,A.Amount AS AMOUNT,C.NAME AS HNAME,C.PHONE+','+C.PHONE2 AS PHONE,C.ADDRESS,C.GSTNO AS GST FROM tblAdvancePayment A,ADMISSION_TABLE B,ORG_TABLE C WHERE A.PID=B.VN AND A.id='" + S.ToString() + "'", con);
            //    ds = new DataSet();
            //    DataTable dt1 = new DataTable();
            //    da.Fill(ds, "STOCK");
            //    dt1 = ds.Tables["STOCK"];
            //    int i = 0, k = 0;
            //    ds2.PAYMENT.Rows.Clear();
            //    while (i < dt1.Rows.Count)
            //    {
            //        dtr = dt1.Rows[i];
            //        ds2.PAYMENT.Rows.Add(new string[] { dtr["ID"].ToString(), dtr["DATE"].ToString(), dtr["PID"].ToString(), dtr["BEDNO"].ToString(), dtr["AMOUNT"].ToString(), dtr["HNAME"].ToString(), dtr["PHONE"].ToString(), dtr["ADDRESS"].ToString(), dtr["GST"].ToString() });
            //        k++;
            //        i += 1;
            //    }
            //    cr = new RECEPTION_Reception_advancebill();
            //    rodc.Load(Server.MapPath("~/REPORTS/paymentreciept.rpt"));
            //    rodc.SetDataSource(dt1);
            //    CrystalReportViewer1.ReportSource = rodc;
            //    CrystalReportViewer1.DataBind();
            //}
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
        cr = new RECEPTION_Reception_advancebill();
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