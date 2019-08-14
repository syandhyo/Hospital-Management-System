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

public partial class RECEPTION_medbill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    SqlDataReader dr;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc1 = new ReportDocument();
    RECEPTION_medbill cr;
    string fr, to;
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
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        { 
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();
        string S = Session["HU"].ToString();
      
            using (SqlCommand cmd = new SqlCommand("MED_BILL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = S;
                cmd.ExecuteNonQuery();

                da1 = new SqlDataAdapter(cmd);
                ds1 = new DataSet();
                DataTable dt11 = new DataTable();
                da1.Fill(ds1, "STOCK");
                dt11 = ds1.Tables["STOCK"];
                int i1 = 0, k1 = 0;
                ds2.MEDRE.Rows.Clear();
                while (i1 < dt11.Rows.Count)
                {
                    dtr = dt11.Rows[i1];
                    ds2.MEDRE.Rows.Add(new string[] { dtr["CAT"].ToString(),dtr["ID"].ToString(), dtr["INVDATE"].ToString(), dtr["IPNO"].ToString(), dtr["PNAME"].ToString(), dtr["NAME"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["QTY"].ToString(), dtr["RATE"].ToString(), dtr["AMOUNT"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["PAMT"].ToString(), dtr["BAMT"].ToString(), dtr["ONAME"].ToString(), dtr["ADDRESS"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["GSTNO"].ToString(), dtr["PID"].ToString() });
                    k1++;
                    i1 += 1;
                }
                cr = new RECEPTION_medbill();
                rodc1.Load(Server.MapPath("~/REPORTS/medallbill.rpt"));
                rodc1.SetDataSource(dt11);
                CrystalReportViewer1.ReportSource = rodc1;
                CrystalReportViewer1.DataBind();
              
            }
        
        con.Close();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new RECEPTION_medbill();
        cr.rodc1.Close();
        cr.rodc1.Clone();
        cr.rodc1.Dispose();
        rodc1 = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
    }
    protected void CloseReport()
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
            if (CrystalReportViewer1 != null)
            {
                CrystalReportViewer1.ReportSource = null;
                CrystalReportViewer1.Dispose();
                cr.rodc1.Close();
                cr.rodc1.Clone();
                cr.rodc1.Dispose();
                rodc1 = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
        }
        catch(Exception ex)
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

            rodc1.PrintOptions.PrinterName = GetDefaultPrinter();
            rodc1.PrintToPrinter(1, false, 0, 0);
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