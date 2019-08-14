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
public partial class ACCOUNTS_Payslip : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds, ds1, ds3;
    DataRow dtr, dtr1;
    SqlDataReader dr, dr1, dr2;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    ACCOUNTS_Payslip cr;
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if(!IsPostBack)
        {
          binddata();
        }
        //if (IsPostBack)
        //{
        //    using (SqlCommand cm = new SqlCommand("SP_Payslip_RPT", con))
        //    {
        //        cm.CommandType = CommandType.StoredProcedure;
        //        cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
        //        SqlDataAdapter da = new SqlDataAdapter(cm);
        //        // da = new SqlDataAdapter("SELECT tblPayroll.id, tblStaff.EMPID,tblStaff.Sname, tblStaff.Basic, tblStaff.HRA, tblStaff.Con, tblStaff.Med, tblPayroll.PF, tblPayroll.PT, tblPayroll.TDS, tblPayroll.Month, tblPayroll.Year,ORG_TABLE.NAME, ORG_TABLE.PHONE, ORG_TABLE.GSTNO, ORG_TABLE.ADDRESS, ORG_TABLE.EMAIL, ORG_TABLE.PHONE2,tblPayroll.WorkingDays,tblPayroll.OtherDeductions,tblPayroll.TotalSal,tblPayroll.Dm,tblPayroll.Ad,tblPayroll.totald,tblPayroll.netpay FROM tblPayroll INNER JOIN tblStaff ON tblPayroll.Empid = tblStaff.Empid INNER JOIN ORG_TABLE ON tblStaff.ORGID = ORG_TABLE.ID", con);
        //        ds = new DataSet();
        //        DataTable dt1 = new DataTable();
        //        da.Fill(ds, "STOCK");
        //        dt1 = ds.Tables["STOCK"];
        //        int i = 0, k = 0;
        //        ds2.PayRoll_Bill.Rows.Clear();
        //        while (i < dt1.Rows.Count)
        //        {
        //            dtr = dt1.Rows[i];
        //            ds2.PayRoll_Bill.Rows.Add(new string[] { dtr["id"].ToString(), dtr["Sname"].ToString(), dtr["Basic"].ToString(), dtr["HRA"].ToString(), dtr["Con"].ToString(), dtr["Med"].ToString(), dtr["PF"].ToString(), dtr["PT"].ToString(), dtr["TDS"].ToString(), dtr["Month"].ToString(), dtr["Year"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["GSTNO"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["PHONE2"].ToString(), dtr["EMPID"].ToString(), dtr["WorkingDays"].ToString(), dtr["OtherDeductions"].ToString(), dtr["TotalSal"].ToString(), dtr["Dm"].ToString(), dtr["Ad"].ToString(), dtr["totald"].ToString(), dtr["netpay"].ToString() });
        //            k++;
        //            i += 1;
        //        }
        //        cr = new ACCOUNTS_Payslip();
        //        rodc.Load(Server.MapPath("~/REPORTS/payroll2.rpt"));
        //        rodc.SetDataSource(dt1);
        //        CrystalReportViewer1.ReportSource = rodc;
        //        CrystalReportViewer1.DataBind();
        //    }
          
        //}
        con.Close();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new ACCOUNTS_Payslip();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
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

        SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT Month FROM tblPayroll", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        //dropbedno.SelectedIndex = 0;
        DropDownList1.DataSource = dt1;
        DropDownList1.DataTextField = "Month";
        DropDownList1.DataValueField = "Month";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "-----Select------");

        SqlDataAdapter da2 = new SqlDataAdapter("SELECT DISTINCT Year FROM tblPayroll", con);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        //dropbedno.SelectedIndex = 0;
        DropDownList2.DataSource = dt2;
        DropDownList2.DataTextField = "Year";
        DropDownList2.DataValueField = "Year";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "-----Select------");
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Month')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (DropDownList2.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Year.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            using (SqlCommand cm = new SqlCommand("SP_Payslip_RPT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cm.Parameters.Add("@Month", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
                cm.Parameters.Add("@Year", SqlDbType.VarChar).Value = DropDownList2.SelectedItem.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cm);
                // da = new SqlDataAdapter("SELECT tblPayroll.id, tblStaff.EMPID,tblStaff.Sname, tblStaff.Basic, tblStaff.HRA, tblStaff.Con, tblStaff.Med, tblPayroll.PF, tblPayroll.PT, tblPayroll.TDS, tblPayroll.Month, tblPayroll.Year,ORG_TABLE.NAME, ORG_TABLE.PHONE, ORG_TABLE.GSTNO, ORG_TABLE.ADDRESS, ORG_TABLE.EMAIL, ORG_TABLE.PHONE2,tblPayroll.WorkingDays,tblPayroll.OtherDeductions,tblPayroll.TotalSal,tblPayroll.Dm,tblPayroll.Ad,tblPayroll.totald,tblPayroll.netpay FROM tblPayroll INNER JOIN tblStaff ON tblPayroll.Empid = tblStaff.Empid INNER JOIN ORG_TABLE ON tblStaff.ORGID = ORG_TABLE.ID", con);
                ds = new DataSet();
                DataTable dt1 = new DataTable();
                adp.Fill(ds, "STOCK");
                dt1 = ds.Tables["STOCK"];
                int i = 0, k = 0;
                ds2.PayRoll_Bill.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.PayRoll_Bill.Rows.Add(new string[] { dtr["id"].ToString(), dtr["Sname"].ToString(), dtr["Basic"].ToString(), dtr["HRA"].ToString(), dtr["Con"].ToString(), dtr["Med"].ToString(), dtr["PF"].ToString(), dtr["PT"].ToString(), dtr["TDS"].ToString(), dtr["Month"].ToString(), dtr["Year"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["GSTNO"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["PHONE2"].ToString(), dtr["EMPID"].ToString(), dtr["WorkingDays"].ToString(), dtr["OtherDeductions"].ToString(), dtr["TotalSal"].ToString(), dtr["Dm"].ToString(), dtr["Ad"].ToString(), dtr["totald"].ToString(), dtr["netpay"].ToString() });
                    k++;
                    i += 1;
                }
                cr = new ACCOUNTS_Payslip();
                rodc.Load(Server.MapPath("~/REPORTS/payroll2.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();

                con.Close();
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