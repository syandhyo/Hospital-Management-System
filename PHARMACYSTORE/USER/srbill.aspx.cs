using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;

public partial class PHARMACYSTORE_USER_sbill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
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
        lblorgid.Text = Session["ORGID"].ToString();
        lblsid.Text = Session["SID"].ToString();
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        SqlDataReader dr = COM.ExecuteReader();
        if (dr.Read())
        {
            //Label1.Text = dr["FYEAR"].ToString();
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();


           da1 = new SqlDataAdapter("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,' ' AS INDID,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SR_TABLE A,SRETURN_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + lblsid.Text + "' AND A.ID = B.ID AND C.ORGID=A.ORGID", con);
            ds = new DataSet();
            da1.Fill(ds, "123");
            dt1 = ds.Tables["123"];
            int i = 0;
            ds2.DataTable1.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.DataTable1.Rows.Add(new string[] { dtr["INVOICE"].ToString(), dtr["CTYPE"].ToString(), dtr["PNAME"].ToString(), dtr["STATECODE"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["IGST"].ToString(), dtr["BGSTAMT"].ToString(), dtr["BTOTALAMT"].ToString(), dtr["CNAME"].ToString(), dtr["CPHONE"].ToString(), dtr["CGSTNO"].ToString(), dtr["CSTATECODE"].ToString(), dtr["CADDRESS"].ToString(), dtr["INDID"].ToString() });
                i += 1;
            }
            PHARMACYSTORE_USER_sbill cr = new PHARMACYSTORE_USER_sbill();
            rodc.Load(Server.MapPath("~/salebill.rpt"));
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();
           // rodc.PrintOptions.PrinterName = getde
           // rodc.PrintToPrinter(1, false, 0, 0);
            cr.rodc.Close();
            cr.rodc.Dispose();
            con.Close();
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