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

public partial class STOREKEEPER_GRNBill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet2 ds2 = new DataSet2();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    STOREKEEPER_GRNBill cr;
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
        cr = new STOREKEEPER_GRNBill();
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
            string S = Session["GRNNO"].ToString();
            binddata();
            using (SqlCommand cm = new SqlCommand("STORE_PURCHASE_BILL", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = S.ToString();
                da1 = new SqlDataAdapter(cm);
                //da1 = new SqlDataAdapter(" SELECT C.NAME1 AS VENDORNAME,C.ADDRESS1,C.CITY AS VCITY,C.CONTACT_PER,C.CONTACT_PER_NO,C.GST AS VGST,C.TEL_NO AS PHONE,C.PIN AS VPIN,C.STATE AS VSTATE,A.GRNNO,CONVERT(VARCHAR(10),A.GRNDATE,101) AS DATEOFISSUE,CONVERT(VARCHAR(10),A.PODATE,101)AS REFDATE,A.GRANDTOTAL,A.GSTAMOUNT,A.TOTALPRICE,B.AMOUNT,B.CGST,B.GSTAMT,B.IGST,B.ITEMNAME,B.PRICE,B.QTY,B.SGST,B.TOTALAMT,B.UNIT FROM GRN_TABLE A,GRN_ITEM_TABLE B,VENDER_MASTER_TABLE C WHERE A.GRNNO=B.GRNNO AND A.VENDOR=C.ID AND A.GRNNO='" + S.ToString() + "'", con);
                ds = new DataSet();
                da1.Fill(ds, "123");
                dt1 = ds.Tables["123"];
                int i = 0;
                ds2.GRNBILL.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.GRNBILL.Rows.Add(new string[] { dtr["VENDORNAME"].ToString(), dtr["ADDRESS1"].ToString(), dtr["VCITY"].ToString(), dtr["CONTACT_PER"].ToString(), dtr["CONTACT_PER_NO"].ToString(), dtr["VGST"].ToString(), dtr["PHONE"].ToString(), dtr["VPIN"].ToString(), dtr["VSTATE"].ToString(), dtr["GRNNO"].ToString(), dtr["DATEOFISSUE"].ToString(), dtr["REFDATE"].ToString(), dtr["GRANDTOTAL"].ToString(), dtr["GSTAMOUNT"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["GSTAMT"].ToString(), dtr["IGST"].ToString(), dtr["ITEMNAME"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["SGST"].ToString(), dtr["TOTALAMT"].ToString(), dtr["UNIT"].ToString() });
                    i += 1;
                }
                cr = new STOREKEEPER_GRNBill();
                rodc.Load(Server.MapPath("~/MATREPORT/grnreport.rpt"));
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