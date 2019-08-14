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

public partial class RECEPTION_reception_lab_receipt : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    SqlDataReader dr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    ReportDocument rodc1 = new ReportDocument();
    string fr, to;
    RECEPTION_reception_lab_receipt cr;


    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
            string S = Session["labbill"].ToString();
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
            using (SqlCommand cmd1 = new SqlCommand("RECP_LAB_SECONDOBILL", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = S.ToString();
                da = new SqlDataAdapter(cmd1);

                //da = new SqlDataAdapter("SELECT A.ID AS ID,A.LINDID AS LIND,A.PID+' '+A.PNAME AS PID,A.PRICE AS PRICE,A.DATE AS DATE,B.NAME AS ORGNAME,B.PHONE AS ORGPHONE,B.GSTNO AS GSTNO,B.ADDRESS AS ADDRESS,D.INV AS INV,C.PRICE AS PPRICE,A.PAIDAMT,A.DISCAMT,A.DUEAMT,A.UNAME AS PREPAREDBY FROM LABRES_TABLE A,LABRESULT_TABLE C,TEST_COMPONENT_TABLE D, ORG_TABLE B WHERE A.ID='" + S.ToString() + "' AND A.ORGID=B.ID AND C.ID=A.ID AND C.INV=D.slno", con);
                ds = new DataSet();
                DataTable dt1 = new DataTable();
                da.Fill(ds, "STOCK");
                dt1 = ds.Tables["STOCK"];
                int i = 0, k = 0;
                ds2.LBILL1.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.LBILL1.Rows.Add(new string[] { dtr["ID"].ToString(), dtr["LIND"].ToString(), dtr["PID"].ToString(), dtr["PRICE"].ToString(), dtr["DATE"].ToString(), dtr["ORGNAME"].ToString(), dtr["ORGPHONE"].ToString(), dtr["GSTNO"].ToString(), dtr["ADDRESS"].ToString(), dtr["INV"].ToString(), dtr["PPRICE"].ToString(), dtr["PAIDAMT"].ToString(), dtr["DISCAMT"].ToString(), dtr["DUEAMT"].ToString(), dtr["PREPAREDBY"].ToString() });
                    k++;
                    i += 1;
                }
                cr = new RECEPTION_reception_lab_receipt();
                rodc.Load(Server.MapPath("~/REPORTS/labbill2.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                cr.rodc.Close();
            }
            cr.rodc.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
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
        cr = new RECEPTION_reception_lab_receipt();
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
            rodc1.PrintOptions.PrinterName = GetDefaultPrinter();
            rodc1.PrintToPrinter(1, false, 0, 0);
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