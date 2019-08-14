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
using System.Web.Services;
using System.Drawing.Printing;

public partial class PHARMACYSTORE_USER_Bill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    PHARMACYSTORE_USER_Bill cr;
    [WebMethod]
    public static string[] GetCustomers(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select DISTINCT PNAME from SA_TABLE where PNAME like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["PNAME"]));
                    }
                }
                con.Close();
            }
        }
        return customers.ToArray();
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
        catch
        {

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
            lblorgid.Text = Session["ORGID"].ToString();

            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataReader dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
            if (!IsPostBack)
            {
                //da1 = new SqlDataAdapter("select distinct ID FROM SA_TABLE where ORGID='" + lblorgid.Text + "'", con);
                //DataTable ds1 = new DataTable();
                //da1.Fill(ds1);
                //DropDownList1.DataSource = ds1;
                //DropDownList1.DataTextField = "ID";
                //DropDownList1.DataValueField = "ID";
                //DropDownList1.DataBind();
                //SqlDataAdapter da2 = new SqlDataAdapter("select DISTINCT PNAME FROM SA_TABLE where PNAME NOT LIKE 'PA0%' AND  ORGID='" + lblorgid.Text + "'", con);
                //DataTable ds2 = new DataTable();
                //da2.Fill(ds2);
                //droppartyname.DataSource = ds2;
                //droppartyname.DataTextField = "PNAME";
                //droppartyname.DataValueField = "PNAME";
                //droppartyname.DataBind();
            }
            if (IsPostBack)
            {
                da1 = new SqlDataAdapter("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + txtinvoice.Text + "' AND A.ID = B.ID AND C.ORGID=A.ORGID", con);
                ds = new DataSet();
                da1.Fill(ds, "123");
                dt1 = ds.Tables["123"];
                int i = 0;
                ds2.DataTable1.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.DataTable1.Rows.Add(new string[] { dtr["INVOICE"].ToString(), dtr["CTYPE"].ToString(), dtr["PNAME"].ToString(), dtr["STATECODE"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["IGST"].ToString(), dtr["BGSTAMT"].ToString(), dtr["BTOTALAMT"].ToString(), dtr["CNAME"].ToString(), dtr["CPHONE"].ToString(), dtr["CGSTNO"].ToString(), dtr["CSTATECODE"].ToString(), dtr["CADDRESS"].ToString() });
                    i += 1;
                }
                cr = new PHARMACYSTORE_USER_Bill();
                rodc.Load(Server.MapPath("~/REPORTS/salebill.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();

                //rodc.PrintOptions.PrinterName = GetDefaultPrinter();
                //rodc.PrintToPrinter(1, false, 0, 0);

            }
            con.Close();
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
        cr = new PHARMACYSTORE_USER_Bill();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (txtinvoice.Text == "")
            {
                string message = "alert('*Please Enter Invoice No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtinvoice.Focus();
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            da1 = new SqlDataAdapter("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + txtinvoice.Text + "' AND A.ID = B.ID AND C.ORGID=A.ORGID", con);
            ds = new DataSet();
            da1.Fill(ds, "123");
            dt1 = ds.Tables["123"];
            int i = 0;
            ds2.DataTable1.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.DataTable1.Rows.Add(new string[] { dtr["INVOICE"].ToString(), dtr["CTYPE"].ToString(), dtr["PNAME"].ToString(), dtr["STATECODE"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["IGST"].ToString(), dtr["BGSTAMT"].ToString(), dtr["BTOTALAMT"].ToString(), dtr["CNAME"].ToString(), dtr["CPHONE"].ToString(), dtr["CGSTNO"].ToString(), dtr["CSTATECODE"].ToString(), dtr["CADDRESS"].ToString() });
                i += 1;
            }
            cr = new PHARMACYSTORE_USER_Bill();
            rodc.Load(Server.MapPath("~/REPORTS/salebill.rpt"));
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();

            tab1.Visible = true;
            con.Close();
            btnPrint.Visible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        da1 = new SqlDataAdapter("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + txtinvoice.Text + "' AND A.ID = B.ID AND C.ORGID=A.ORGID", con);
        ds = new DataSet();
        da1.Fill(ds, "123");
        dt1 = ds.Tables["123"];
        int i = 0;
        ds2.DataTable1.Rows.Clear();
        while (i < dt1.Rows.Count)
        {
            dtr = dt1.Rows[i];
            ds2.DataTable1.Rows.Add(new string[] { dtr["INVOICE"].ToString(), dtr["CTYPE"].ToString(), dtr["PNAME"].ToString(), dtr["STATECODE"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["IGST"].ToString(), dtr["BGSTAMT"].ToString(), dtr["BTOTALAMT"].ToString(), dtr["CNAME"].ToString(), dtr["CPHONE"].ToString(), dtr["CGSTNO"].ToString(), dtr["CSTATECODE"].ToString(), dtr["CADDRESS"].ToString() });
            i += 1;
        }
       cr = new PHARMACYSTORE_USER_Bill();
        rodc.Load(Server.MapPath("~/REPORTS/salebill.rpt"));
        rodc.SetDataSource(dt1);
        CrystalReportViewer1.ReportSource = rodc;
        CrystalReportViewer1.DataBind();
     
        //rodc.PrintOptions.PrinterName = GetDefaultPrinter();
        //rodc.PrintToPrinter(1, false, 0, 0);
        con.Close();
    }
    protected void Butparty_Click(object sender, System.EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,PNAME AS NAME,CONVERT(VARCHAR(10),INVDATE,105) AS DATE FROM SA_TABLE WHERE PNAME = '" + droppartyname.Text + "' AND ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.SelectedIndex = 0;
        GridView1.DataSource = dt;
        GridView1.DataKeyNames = new string[] { "ID" };
        GridView1.DataBind();
        tab1.Visible = false;
        con.Close();
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            da1 = new SqlDataAdapter("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + slno.ToString() + "' AND A.ID = B.ID AND C.ORGID=A.ORGID", con);
            ds = new DataSet();
            da1.Fill(ds, "123");
            dt1 = ds.Tables["123"];
            int i = 0;
            ds2.DataTable1.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.DataTable1.Rows.Add(new string[] { dtr["INVOICE"].ToString(), dtr["CTYPE"].ToString(), dtr["PNAME"].ToString(), dtr["STATECODE"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["IGST"].ToString(), dtr["BGSTAMT"].ToString(), dtr["BTOTALAMT"].ToString(), dtr["CNAME"].ToString(), dtr["CPHONE"].ToString(), dtr["CGSTNO"].ToString(), dtr["CSTATECODE"].ToString(), dtr["CADDRESS"].ToString() });
                i += 1;
            }
            cr = new PHARMACYSTORE_USER_Bill();
            rodc.Load(Server.MapPath("~/REPORTS/salebill.rpt"));
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();

            tab1.Visible = true;
            con.Close();
            btnPrint.Visible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Btnsearch_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('*Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,PNAME AS NAME,CONVERT(VARCHAR(10),INVDATE,105) AS DATE FROM SA_TABLE WHERE PNAME = '" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
            tab1.Visible = false;
            con.Close();
            btnPrint.Visible = true;
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
}