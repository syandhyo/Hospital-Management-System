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

public partial class RECEPTION_reception_preg_reciept : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    SqlDataReader dr, dr1, dr2;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    RECEPTION_reception_preg_reciept cr;
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
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
            string S = Session["PID"].ToString();
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
            using (SqlCommand cmd = new SqlCommand("RECP_PATIENT_REG_REPORT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = S.ToString();
                da = new SqlDataAdapter(cmd);
                //da = new SqlDataAdapter("select A.ID,'OUTPATIENT' AS PTYPE,A.PNAME AS NAME,A.AGE,A.GENDER,A.MOBNO AS PHONE,A.GURDINMOBLE AS ECONTACT,A.EMAILID AS EMAIL,'' AS PREF,A.DATETIME AS DATE,A.ADDRES AS PADDRESS,'' AS TADDRESS,A.CHARGES AS RGFEE,'' AS DISEASE,B.NAME AS ORGNAME,B.PHONE AS ORGPHONE,B.GSTNO,B.ADDRESS AS ADDRESS,A.UHID AS UHNID FROM REGISTRATION_TBL A, ORG_TABLE B WHERE  A.ID='" + S.ToString() + "'", con);
                ds = new DataSet();
                DataTable dt1 = new DataTable();
                da.Fill(ds, "STOCK");
                dt1 = ds.Tables["STOCK"];
                int i = 0, k = 0;
                ds2.PR.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.PR.Rows.Add(new string[] { dtr["ID"].ToString(), dtr["PTYPE"].ToString(), dtr["NAME"].ToString(), dtr["AGE"].ToString(), dtr["GENDER"].ToString(), dtr["PHONE"].ToString(), dtr["ECONTACT"].ToString(), dtr["EMAIL"].ToString(), dtr["PREF"].ToString(), dtr["DATE"].ToString(), dtr["PADDRESS"].ToString(), dtr["TADDRESS"].ToString(), dtr["RGFEE"].ToString(), dtr["DISEASE"].ToString(), dtr["ORGNAME"].ToString(), dtr["ORGPHONE"].ToString(), dtr["GSTNO"].ToString(), dtr["ADDRESS"].ToString(), dtr["UHNID"].ToString() });
                    k++;
                    i += 1;
                }
                cr = new RECEPTION_reception_preg_reciept();
                rodc.Load(Server.MapPath("~/REPORTS/patient_registration_reciept.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new RECEPTION_reception_preg_reciept();
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