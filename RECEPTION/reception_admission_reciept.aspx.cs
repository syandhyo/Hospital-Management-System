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

public partial class RECEPTION_reception_admission_reciept : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds, ds1, ds3;
    DataRow dtr, dtr1;
    SqlDataReader dr, dr1, dr2;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    ReportDocument rodc1 = new ReportDocument();
    ReportDocument rodc2 = new ReportDocument();
    RECEPTION_reception_admission_reciept cr1;
    RECEPTION_reception_admission_reciept cr2;
    RECEPTION_reception_admission_reciept cr;
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
        catch
        {

        }

    }
    protected void CloseReport1()
    {
        try
        {
            if (rodc1 != null)
            {
                Sections objSections = rodc1.ReportDefinition.Sections;
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
                rodc1.Close();
                rodc1.Dispose();
                cr.rodc1.Close();
                cr.rodc1.Clone();
                cr.rodc1.Dispose();
                rodc1 = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            if (CrystalReportViewer2 != null)
            {
                CrystalReportViewer2.ReportSource = null;
                CrystalReportViewer2.Dispose();
                cr.rodc1.Close();
                cr.rodc1.Clone();
                cr.rodc1.Dispose();
                rodc1 = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void CloseReport2()
    {
        try
        {
            if (rodc2 != null)
            {
                Sections objSections = rodc2.ReportDefinition.Sections;
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
                rodc2.Close();
                rodc2.Dispose();
                cr.rodc2.Close();
                cr.rodc2.Clone();
                cr.rodc2.Dispose();
                rodc2 = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            if (CrystalReportViewer3 != null)
            {
                CrystalReportViewer3.ReportSource = null;
                CrystalReportViewer3.Dispose();
                cr.rodc2.Close();
                cr.rodc2.Clone();
                cr.rodc2.Dispose();
                rodc2 = null;
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
            string S = Session["AID"].ToString();
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

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, S.ToString());

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_REPORT", false, true, SQL_PARAMS1);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                Ds.Tables[0].TableName = "Table1";
                dt1 = Ds.Tables["Table1"];
                int i = 0, k = 0;
                ds2.ADRE.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.ADRE.Rows.Add(new string[] { dtr["ID"].ToString(), dtr["PTYPE"].ToString(), dtr["NAME"].ToString(), dtr["AGE"].ToString(), dtr["GENDER"].ToString(), dtr["PHONE"].ToString(), dtr["ECONTACT"].ToString(), dtr["PREF"].ToString(), dtr["DATE"].ToString(), dtr["PADDRESS"].ToString(), dtr["TADDRESS"].ToString(), dtr["RGFEE"].ToString(), dtr["DISEASE"].ToString(), dtr["BEDNO"].ToString(), dtr["ORGNAME"].ToString(), dtr["ORGPHONE"].ToString(), dtr["GSTNO"].ToString(), dtr["ADDRESS"].ToString(), dtr["VN"].ToString(), dtr["WARD"].ToString(), dtr["FAMILY"].ToString(), dtr["UHID"].ToString() });
                    k++;
                    i += 1;
                }
                cr = new RECEPTION_reception_admission_reciept();
                rodc.Load(Server.MapPath("~/REPORTS/adm_reciept.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();
                /// Report for IP Consultation
                cr1 = new RECEPTION_reception_admission_reciept();
                rodc1.Load(Server.MapPath("~/REPORTS/IPConsult.rpt"));
                rodc1.SetDataSource(dt1);
                CrystalReportViewer2.ReportSource = rodc1;
                CrystalReportViewer2.DataBind();
                cr2 = new RECEPTION_reception_admission_reciept();
                rodc2.Load(Server.MapPath("~/REPORTS/pregform.rpt"));
                rodc2.SetDataSource(dt1);
                CrystalReportViewer3.ReportSource = rodc2;
                CrystalReportViewer3.DataBind();
            }
            #region oldcode
            //using (SqlCommand cmd = new SqlCommand("RECP_ADMISSION_REPORT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = S.ToString();
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    da = new SqlDataAdapter(cmd);
            //    //da = new SqlDataAdapter("select A.ID,A.PTYPE,A.NAME,A.AGE,A.GENDER,A.PHONE AS PHONE,A.ECONTACT,A.PREF,A.DATE,A.PADDRESS,A.TADDRESS,A.RGFEE,A.DISEASE,A.BEDNO,B.NAME AS ORGNAME,B.PHONE+','+B.PHONE2 AS ORGPHONE,B.GSTNO,B.ADDRESS AS ADDRESS,A.VN AS VN,A.WARD,A.FAMILY,A.UHID FROM ADMISSION_TABLE A, ORG_TABLE B WHERE  A.VN='" + S.ToString() + "' AND A.ORGID=B.ID AND A.ORGID='" + lblorgid.Text + "'", con);
            //    ds = new DataSet();
            //    DataTable dt1 = new DataTable();
            //    da.Fill(ds, "STOCK");
            //    dt1 = ds.Tables["STOCK"];
            //    int i = 0, k = 0;
            //    ds2.ADRE.Rows.Clear();
            //    while (i < dt1.Rows.Count)
            //    {
            //        dtr = dt1.Rows[i];
            //        ds2.ADRE.Rows.Add(new string[] { dtr["ID"].ToString(), dtr["PTYPE"].ToString(), dtr["NAME"].ToString(), dtr["AGE"].ToString(), dtr["GENDER"].ToString(), dtr["PHONE"].ToString(), dtr["ECONTACT"].ToString(), dtr["PREF"].ToString(), dtr["DATE"].ToString(), dtr["PADDRESS"].ToString(), dtr["TADDRESS"].ToString(), dtr["RGFEE"].ToString(), dtr["DISEASE"].ToString(), dtr["BEDNO"].ToString(), dtr["ORGNAME"].ToString(), dtr["ORGPHONE"].ToString(), dtr["GSTNO"].ToString(), dtr["ADDRESS"].ToString(), dtr["VN"].ToString(), dtr["WARD"].ToString(), dtr["FAMILY"].ToString(), dtr["UHID"].ToString() });
            //        k++;
            //        i += 1;
            //    }
            //    cr = new RECEPTION_reception_admission_reciept();
            //    rodc.Load(Server.MapPath("~/REPORTS/adm_reciept.rpt"));
            //    rodc.SetDataSource(dt1);
            //    CrystalReportViewer1.ReportSource = rodc;
            //    CrystalReportViewer1.DataBind();
            //    /// Report for IP Consultation
            //    cr1 = new RECEPTION_reception_admission_reciept();
            //    rodc1.Load(Server.MapPath("~/REPORTS/IPConsult.rpt"));
            //    rodc1.SetDataSource(dt1);
            //    CrystalReportViewer2.ReportSource = rodc1;
            //    CrystalReportViewer2.DataBind();
            //    cr2 = new RECEPTION_reception_admission_reciept();
            //    rodc2.Load(Server.MapPath("~/REPORTS/pregform.rpt"));
            //    rodc2.SetDataSource(dt1);
            //    CrystalReportViewer3.ReportSource = rodc2;
            //    CrystalReportViewer3.DataBind();
            //}
            #endregion
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
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        try
        {
            CloseReport();
            CloseReport1();
            CloseReport2();
            cr.rodc.Close();
            cr.rodc.Dispose();
            cr1.rodc1.Close();
            cr1.rodc1.Dispose();
            cr2.rodc2.Close();
            cr2.rodc2.Dispose();
            rodc = null;
            rodc2 = null;
            rodc1 = null;
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        //}
    }
    protected void CrystalReportViewer3_Unload(object sender, EventArgs e)
    {
        try
        {
            CloseReport();
            CloseReport1();
            CloseReport2();
            cr.rodc.Close();
            cr.rodc.Dispose();
            cr1.rodc1.Close();
            cr1.rodc1.Dispose();
            cr2.rodc2.Close();
            cr2.rodc2.Dispose();
            rodc = null;
            rodc2 = null;
            rodc1 = null;
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
}