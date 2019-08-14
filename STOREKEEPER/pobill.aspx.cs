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


public partial class STOREKEEPER_pobill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet2 ds2 = new DataSet2();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    STOREKEEPER_pobill cr;
    SqlDataReader dr;
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
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new STOREKEEPER_pobill();
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
            lbluid.Text = Session["UID"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();
            binddata();
            string S = Session["PONO"].ToString();
            using (SqlCommand cmd = new SqlCommand("STORE_PO_BILL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = S.ToString();
                da1 = new SqlDataAdapter(cmd);
                //da1 = new SqlDataAdapter(" SELECT C.NAME1 AS VENDORNAME,C.ADDRESS1,C.CITY AS VCITY,C.CONTACT_PER,C.CONTACT_PER_NO,C.GST AS VGST,C.TEL_NO AS PHONE,C.PIN AS VPIN,C.STATE AS VSTATE,A.PONO,CONVERT(VARCHAR(10),A.DATEOFISSUE,103) AS DATEOFISSUE,A.REFNO,CONVERT(VARCHAR(10),A.REFDATE,103)AS REFDATE,A.ORGNAME,A.PIN,A.CITY,A.STATE,A.GSTIN,A.ADDRESS,A.GRANDTOTAL,A.GSTAMOUNT,A.TOTALPRICE,A.TC1 as TC1,A.TC2,A.TC3,A.TC4,A.TC5,A.TC6,A.TC7,A.TC8,A.TC9,A.TC10,A.TC11,A.TC12,A.TC13,A.TC14,A.TC15,A.TC16,B.AMOUNT,B.CGST,B.GSTAMT,B.IGST,B.ITEMNAME,B.PRICE,B.QTY,B.SGST,B.TOTALAMT,B.UNIT FROM PO_TABLE A,PO_ITEM_TABLE B,VENDER_MASTER_TABLE C WHERE A.PONO=B.ID AND A.VENDOR=C.ID AND A.PONO='" + S.ToString() + "'", con);

                ds = new DataSet();
                da1.Fill(ds, "123");
                dt1 = ds.Tables["123"];
                int i = 0;
                ds2.POBILL.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.POBILL.Rows.Add(new string[] { dtr["VENDORNAME"].ToString(), dtr["ADDRESS1"].ToString(), dtr["VCITY"].ToString(), dtr["CONTACT_PER"].ToString(), dtr["CONTACT_PER_NO"].ToString(), dtr["VGST"].ToString(), dtr["PHONE"].ToString(), dtr["VPIN"].ToString(), dtr["VSTATE"].ToString(), dtr["PONO"].ToString(), dtr["DATEOFISSUE"].ToString(), dtr["REFNO"].ToString(), dtr["REFDATE"].ToString(), dtr["ORGNAME"].ToString(), dtr["PIN"].ToString(), dtr["CITY"].ToString(), dtr["STATE"].ToString(), dtr["GSTIN"].ToString(), dtr["ADDRESS"].ToString(), dtr["GRANDTOTAL"].ToString(), dtr["GSTAMOUNT"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TC1"].ToString(), dtr["TC2"].ToString(), dtr["TC3"].ToString(), dtr["TC4"].ToString(), dtr["TC5"].ToString(), dtr["TC6"].ToString(), dtr["TC7"].ToString(), dtr["TC8"].ToString(), dtr["TC9"].ToString(), dtr["TC10"].ToString(), dtr["TC11"].ToString(), dtr["TC12"].ToString(), dtr["TC13"].ToString(), dtr["TC14"].ToString(), dtr["TC15"].ToString(), dtr["TC16"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["GSTAMT"].ToString(), dtr["IGST"].ToString(), dtr["ITEMNAME"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["SGST"].ToString(), dtr["TOTALAMT"].ToString(), dtr["UNIT"].ToString() });
                    i += 1;
                }
                cr = new STOREKEEPER_pobill();
                rodc.Load(Server.MapPath("~/MATREPORT/poreport.rpt"));
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
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
        con.Close();
    }
}