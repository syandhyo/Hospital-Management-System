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

public partial class PHARMACYSTORE_pharmacy_party_report : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    string fr, to;
    PHARMACYSTORE_pharmacy_party_report cr;
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
                da = new SqlDataAdapter("select distinct PNAME AS NAME FROM PUR_TABLE where ORGID='" + lblorgid.Text + "'", con);
                DataTable ds = new DataTable();
                da.Fill(ds);
                dropparty.DataSource = ds;
                dropparty.DataTextField = "NAME";
                dropparty.DataValueField = "NAME";
                dropparty.DataBind();
                dropparty.Items.Insert(0, "Please Select");
            }
            if (IsPostBack)
            {

                fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
                to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
                //da = new SqlDataAdapter("SELECT A.INVOICENO AS INVOICENO ,A.PNAME AS PNAME,CONVERT(VARCHAR(10),A.INVDATE,103) AS INVDATE,A.TOTALPRICE AS TOTALPRICE,A.TOTALDISCAMT AS TOTALDISCAMT,A.TOTALAMT AS TOTALAMT,A.GSTAMT AS GSTAMT,A.GT AS GT,B.NAME AS NAME,B.HSNCODE AS HSNCODE ,B.BATCHNO AS BATCHNO,B.EXPDATE AS EXPDATE,B.QTY AS QTY,B.UNIT AS UNIT,B.PRICE AS PRICE,B.DISC AS DISC,B.DISCAMT AS DISCAMT,B.AMOUNT AS AMOUNT,B.CGST AS CGST,B.SGST AS SGST,B.IGST AS IGST,B.GSTAMT AS GSTAMT1,B.TOTALAMT AS TOTALAMT1,B.FQTY AS FQTY FROM PUR_TABLE A, PURCHASE_TABLE B WHERE A.ID=B.ID AND A.INVDATE BETWEEN'" + fr + "' AND '" + to + "'AND A.PNAME='" + dropparty.Text + "' ORDER BY A.INVDATE ASC", con);
                //ds = new DataSet();
                //DataTable dt1 = new DataTable();
                //da.Fill(ds, "STOCK");
                using (SqlCommand cmd = new SqlCommand("phrmc_PartyRpt", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                    cmd.Parameters.Add("@FDATE", SqlDbType.VarChar).Value = fr;
                    cmd.Parameters.Add("@TDATE", SqlDbType.VarChar).Value = to;
                    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropparty.Text;
                    da1 = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da1.Fill(ds, "STOCK");
                    dt1 = ds.Tables["STOCK"];
                    int i = 0, k = 0;
                    ds2.PARTYPO.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.PARTYPO.Rows.Add(new string[] { dtr["INVOICENO"].ToString(), dtr["PNAME"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["QTY"].ToString(), dtr["UNIT"].ToString(), dtr["PRICE"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["SGST"].ToString(), dtr["IGST"].ToString(), dtr["GSTAMT1"].ToString(), dtr["TOTALAMT1"].ToString(), dtr["FQTY"].ToString() });
                        k++;
                        i += 1;
                    }
                    cr = new PHARMACYSTORE_pharmacy_party_report();
                    rodc.Load(Server.MapPath("~/REPORTS/party.rpt"));
                    TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                    fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                    TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                    todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
                    TextObject heading = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txt1"];
                    heading.Text = "PARTY WISE PURCHASE REPORT";
                    rodc.SetDataSource(dt1);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();
                }
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
        cr = new PHARMACYSTORE_pharmacy_party_report();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropparty.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Party Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (TextBox1.Text == "")
            {
                string message = "alert('* Please Select Month.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (TextBox2.Text == "")
            {
                string message = "alert('* Please Select Year.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            fr = Convert.ToDateTime(TextBox1.Text).ToString("dd-MM-yyyy");
            to = Convert.ToDateTime(TextBox2.Text).ToString("dd-MM-yyyy");
            
            using (SqlCommand cmd = new SqlCommand("phrmc_PartyRpt", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@FDATE", SqlDbType.VarChar).Value = fr;
                cmd.Parameters.Add("@TDATE", SqlDbType.VarChar).Value = to;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropparty.Text;
                da1 = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da1.Fill(ds, "STOCK");
                dt1 = ds.Tables["STOCK"];
                int i = 0, k = 0;
                ds2.PARTYPO.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.PARTYPO.Rows.Add(new string[] { dtr["INVOICENO"].ToString(), dtr["PNAME"].ToString(), dtr["INVDATE"].ToString(), dtr["TOTALPRICE"].ToString(), dtr["TOTALDISCAMT"].ToString(), dtr["TOTALAMT"].ToString(), dtr["GSTAMT"].ToString(), dtr["GT"].ToString(), dtr["NAME"].ToString(), dtr["HSNCODE"].ToString(), dtr["BATCHNO"].ToString(), dtr["EXPDATE"].ToString(), dtr["QTY"].ToString(), dtr["UNIT"].ToString(), dtr["PRICE"].ToString(), dtr["DISC"].ToString(), dtr["DISCAMT"].ToString(), dtr["AMOUNT"].ToString(), dtr["CGST"].ToString(), dtr["SGST"].ToString(), dtr["IGST"].ToString(), dtr["GSTAMT1"].ToString(), dtr["TOTALAMT1"].ToString(), dtr["FQTY"].ToString() });
                    k++;
                    i += 1;
                }
                cr = new PHARMACYSTORE_pharmacy_party_report();
                rodc.Load(Server.MapPath("~/REPORTS/party.rpt"));
                TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
                TextObject heading = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txt1"];
                heading.Text = "PARTY WISE PURCHASE REPORT";
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
            
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            
        }
    }
}