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


public partial class PHARMACYSTORE_pharmacy_sale_bill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    PHARMACYSTORE_pharmacy_sale_bill cr;
    DataMathods OBJ_METHOD = new DataMathods();
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

            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
            if (!IsPostBack)
            {
              
            }
            if (IsPostBack)
            {
                DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + txtinvoice.Text + "' AND A.ID = B.ID AND C.ORGID=A.ORGID AND A.Branch_ID=B.Branch_ID AND A.Branch_ID='" + Session["Branch"] + "'", false, false);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    //da1 = new SqlDataAdapter("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + txtinvoice.Text + "' AND A.ID = B.ID AND C.ORGID=A.ORGID", con);
                    DataTable dt1 = new DataTable();
                    Ds.Tables[0].TableName = "123";
                    dt1 = Ds.Tables["123"];
                    int i = 0;
                    ds2.DataTable1.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.DataTable1.Rows.Add(new string[] { dtr["INVOICE"].ToString(), dtr["CTYPE"].ToString(), dtr["PNAME"].ToString(), dtr["STATECODE"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["IGST"].ToString(), dtr["BGSTAMT"].ToString(), dtr["BTOTALAMT"].ToString(), dtr["CNAME"].ToString(), dtr["CPHONE"].ToString(), dtr["CGSTNO"].ToString(), dtr["CSTATECODE"].ToString(), dtr["CADDRESS"].ToString() });
                        i += 1;
                    }
                    cr = new PHARMACYSTORE_pharmacy_sale_bill();
                    rodc.Load(Server.MapPath("~/REPORTS/salebill.rpt"));
                    rodc.SetDataSource(dt1);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();
                }
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
        cr = new PHARMACYSTORE_pharmacy_sale_bill();
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
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + txtinvoice.Text + "' AND A.ID = B.ID AND C.ORGID=A.ORGID AND A.Branch_ID=B.Branch_ID AND A.Branch_ID='" + Session["Branch"] + "'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                Ds.Tables[0].TableName = "123";
                dt1 = Ds.Tables["123"];
                //da1 = new SqlDataAdapter("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + txtinvoice.Text + "' AND A.ID = B.ID AND C.ORGID=A.ORGID", con);
                //ds = new DataSet();
                //da1.Fill(ds, "123");
                //dt1 = ds.Tables["123"];
                int i = 0;
                ds2.DataTable1.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.DataTable1.Rows.Add(new string[] { dtr["INVOICE"].ToString(), dtr["CTYPE"].ToString(), dtr["PNAME"].ToString(), dtr["STATECODE"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["IGST"].ToString(), dtr["BGSTAMT"].ToString(), dtr["BTOTALAMT"].ToString(), dtr["CNAME"].ToString(), dtr["CPHONE"].ToString(), dtr["CGSTNO"].ToString(), dtr["CSTATECODE"].ToString(), dtr["CADDRESS"].ToString() });
                    i += 1;
                }
                cr = new PHARMACYSTORE_pharmacy_sale_bill();
                rodc.Load(Server.MapPath("~/REPORTS/salebill.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();

                tab1.Visible = true;
                con.Close();
                btnPrint.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + txtinvoice.Text + "' AND A.ID = B.ID AND C.ORGID=A.ORGID AND A.Branch_ID=B.Branch_ID AND A.Branch_ID='" + Session["Branch"] + "'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                Ds.Tables[0].TableName = "123";
                dt1 = Ds.Tables["123"];
                //da1 = new SqlDataAdapter("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + txtinvoice.Text + "' AND A.ID = B.ID AND C.ORGID=A.ORGID", con);
                //ds = new DataSet();
                //da1.Fill(ds, "123");
                //dt1 = ds.Tables["123"];
                int i = 0;
                ds2.DataTable1.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.DataTable1.Rows.Add(new string[] { dtr["INVOICE"].ToString(), dtr["CTYPE"].ToString(), dtr["PNAME"].ToString(), dtr["STATECODE"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["IGST"].ToString(), dtr["BGSTAMT"].ToString(), dtr["BTOTALAMT"].ToString(), dtr["CNAME"].ToString(), dtr["CPHONE"].ToString(), dtr["CGSTNO"].ToString(), dtr["CSTATECODE"].ToString(), dtr["CADDRESS"].ToString() });
                    i += 1;
                }
                cr = new PHARMACYSTORE_pharmacy_sale_bill();
                rodc.Load(Server.MapPath("~/REPORTS/salebill.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Butparty_Click(object sender, System.EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT ID as ID,PNAME AS NAME,CONVERT(VARCHAR(10),INVDATE,105) AS DATE FROM SA_TABLE WHERE PNAME = '" + droppartyname.Text + "' AND ORGID='" + lblorgid.Text + "' AND Branch_ID='" + Session["Branch"] + "' ORDER BY ID DESC", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                //DataTable dt1 = new DataTable();
                //Ds.Tables[0].TableName = "123";
                //dt1 = Ds.Tables["123"];
                //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,PNAME AS NAME,CONVERT(VARCHAR(10),INVDATE,105) AS DATE FROM SA_TABLE WHERE PNAME = '" + droppartyname.Text + "' AND ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = Ds;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
                tab1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + slno.ToString() + "' AND A.ID = B.ID AND C.ORGID=A.ORGID AND A.Branch_ID=B.Branch_ID AND A.Branch_ID='" + Session["Branch"] + "'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                Ds.Tables[0].TableName = "123";
                dt1 = Ds.Tables["123"];
                //da1 = new SqlDataAdapter("SELECT A.ID AS INVOICE,A.RTYPE AS CTYPE,A.PNAME AS PNAME,A.STATECODE AS STATECODE,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE,B.BATCHNO AS BATCHNO,CONVERT(VARCHAR(10),B.EXPDATE,103) AS EXPDATE,B.PRICE AS PRICE,B.QTY AS QTY,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.IGST AS IGST,B.GSTAMT AS BGSTAMT,B.TOTALAMT AS BTOTALAMT,C.NAME AS CNAME,C.PHONE AS CPHONE,C.GSTNO AS CGSTNO,C.STATECODE AS CSTATECODE,C.ADDRESS AS CADDRESS FROM SA_TABLE A,SALE_TABLE B,PHARMACY_STORE_MASTER C WHERE A.ID='" + slno.ToString() + "' AND A.ID = B.ID AND C.ORGID=A.ORGID", con);
                //ds = new DataSet();
                //da1.Fill(ds, "123");
                //dt1 = ds.Tables["123"];
                int i = 0;
                ds2.DataTable1.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.DataTable1.Rows.Add(new string[] { dtr["INVOICE"].ToString(), dtr["CTYPE"].ToString(), dtr["PNAME"].ToString(), dtr["STATECODE"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["PRICE"].ToString(), dtr["QTY"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["IGST"].ToString(), dtr["BGSTAMT"].ToString(), dtr["BTOTALAMT"].ToString(), dtr["CNAME"].ToString(), dtr["CPHONE"].ToString(), dtr["CGSTNO"].ToString(), dtr["CSTATECODE"].ToString(), dtr["CADDRESS"].ToString() });
                    i += 1;
                }
                cr = new PHARMACYSTORE_pharmacy_sale_bill();
                rodc.Load(Server.MapPath("~/REPORTS/salebill.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();

                tab1.Visible = true;

                btnPrint.Visible = true;
            }
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
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT ID as ID,PNAME AS NAME,CONVERT(VARCHAR(10),INVDATE,105) AS DATE FROM SA_TABLE WHERE PNAME = '" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "' AND Branch_ID='" + Session["Branch"] + "'", false, false);
             if (Ds.Tables[0].Rows.Count > 0)
             {
                 
                 //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,PNAME AS NAME,CONVERT(VARCHAR(10),INVDATE,105) AS DATE FROM SA_TABLE WHERE PNAME = '" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
                 //DataTable dt = new DataTable();
                 //da.Fill(dt);
                 // GridView1.SelectedIndex = 0;
                 GridView1.DataSource = Ds;
                 GridView1.DataKeyNames = new string[] { "ID" };
                 GridView1.DataBind();
                 tab1.Visible = false;
                 btnPrint.Visible = true;
             }
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