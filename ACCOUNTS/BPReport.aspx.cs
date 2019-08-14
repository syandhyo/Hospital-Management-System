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
    DataSet2 ds2 = new DataSet2();
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
        if (IsPostBack)
        {
            try
            {
                using (SqlCommand cm = new SqlCommand("SP_Referal_Prcnt_RPT", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                    cm.Parameters.Add("@REF", SqlDbType.VarChar).Value = DropDownList1.SelectedValue;
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    // da = new SqlDataAdapter("select C.NAME,B.UHID,CONVERT(DECIMAL(18,2),SUM(A.PCHARGE/2))  AS PHARMACY,A.PP,CONVERT(DECIMAL(18,2),SUM(A.LCHARGE/2))AS LCHARGE,A.LP,CONVERT(DECIMAL(18,2),SUM(A.BCHARGE/2))AS BCHARGE,A.BP,CONVERT(DECIMAL(18,2),((SUM(A.PCHARGE/2)/100)*A.PP+(SUM(A.LCHARGE/2)/100)*A.LP+(SUM(A.BCHARGE/2)/100)*A.BP)) AS TOTALAMOUNT,ISNULL(SUM(D.AMT),0)AS PAIDAMT from REF_TRAN A,PATIENT_REG_TABLE B,Broker_Table C,BROKER_PAY_TABLE D WHERE A.REF='" + DropDownList1.SelectedValue + "' AND A.REF=C.ID AND D.REC=A.REF AND A.UHID=B.UHID AND C.ID=D.REC GROUP BY A.REF,A.PP,A.LP,A.BP,B.UHID,D.REC,C.NAME", con);
                    ds = new DataSet();
                    DataTable dt1 = new DataTable();
                    da.Fill(ds, "STOCK");
                    dt1 = ds.Tables["STOCK"];
                    int i = 0, k = 0;
                    ds2.BPREPORT.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.BPREPORT.Rows.Add(new string[] { dtr["NAME"].ToString(), dtr["UHID"].ToString(), dtr["PHARMACY"].ToString(), dtr["PP"].ToString(), dtr["LCHARGE"].ToString(), dtr["LP"].ToString(), dtr["BCHARGE"].ToString(), dtr["BP"].ToString(), dtr["TOTALAMOUNT"].ToString(), dtr["PAIDAMT"].ToString() });
                        k++;
                        i += 1;
                    }
                    cr = new ACCOUNTS_Payslip();
                    rodc.Load(Server.MapPath("~/MATREPORT/bpreport.rpt"));
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
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        {
            //Label1.Text = dr["FYEAR"].ToString();
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();

        SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT Month FROM tblPayroll", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        //dropbedno.SelectedIndex = 0;
        DropDownList1.DataSource = dt1;
        DropDownList1.DataTextField = "Month";
        DropDownList1.DataValueField = "Month";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "-----Select------");

        using (SqlCommand com = new SqlCommand("USP_empbroker", con))
        {
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da3 = new SqlDataAdapter(com);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            //dropbedno.SelectedIndex = 0;
            DropDownList1.DataSource = dt3;
            DropDownList1.DataTextField = "NAME";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "NONE");
        }
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cm = new SqlCommand("SP_Referal_Prcnt_RPT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cm.Parameters.Add("@REF", SqlDbType.VarChar).Value = DropDownList1.SelectedValue;
                SqlDataAdapter da = new SqlDataAdapter(cm);
                // da = new SqlDataAdapter("select C.NAME,B.UHID,CONVERT(DECIMAL(18,2),SUM(A.PCHARGE/2))  AS PHARMACY,A.PP,CONVERT(DECIMAL(18,2),SUM(A.LCHARGE/2))AS LCHARGE,A.LP,CONVERT(DECIMAL(18,2),SUM(A.BCHARGE/2))AS BCHARGE,A.BP,CONVERT(DECIMAL(18,2),((SUM(A.PCHARGE/2)/100)*A.PP+(SUM(A.LCHARGE/2)/100)*A.LP+(SUM(A.BCHARGE/2)/100)*A.BP)) AS TOTALAMOUNT,ISNULL(SUM(D.AMT),0)AS PAIDAMT from REF_TRAN A,PATIENT_REG_TABLE B,Broker_Table C,BROKER_PAY_TABLE D WHERE A.REF='" + DropDownList1.SelectedValue + "' AND A.REF=C.ID AND D.REC=A.REF AND A.UHID=B.UHID AND C.ID=D.REC GROUP BY A.REF,A.PP,A.LP,A.BP,B.UHID,D.REC,C.NAME", con);
                ds = new DataSet();
                DataTable dt1 = new DataTable();
                da.Fill(ds, "STOCK");
                dt1 = ds.Tables["STOCK"];
                int i = 0, k = 0;
                ds2.BPREPORT.Rows.Clear();
                while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.BPREPORT.Rows.Add(new string[] { dtr["NAME"].ToString(), dtr["UHID"].ToString(), dtr["PHARMACY"].ToString(), dtr["PP"].ToString(), dtr["LCHARGE"].ToString(), dtr["LP"].ToString(), dtr["BCHARGE"].ToString(), dtr["BP"].ToString(), dtr["TOTALAMOUNT"].ToString(), dtr["PAIDAMT"].ToString() });
                        k++;
                        i += 1;
                    }
                  cr = new ACCOUNTS_Payslip();
                  rodc.Load(Server.MapPath("~/MATREPORT/bpreport.rpt"));
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