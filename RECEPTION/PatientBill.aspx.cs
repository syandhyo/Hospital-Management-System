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

public partial class RECEPTION_PatientBill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    SqlDataReader dr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    ReportDocument rodc1 = new ReportDocument();
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
        lblorgid.Text = Session["ORGID"].ToString();
        binddata();
        //string S = Session["PAYMENT"].ToString();
        if (!IsPostBack)
        {
            using (SqlCommand cmd1 = new SqlCommand("SP_PROVISIONAL_BILLIP", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "provisional_ip";
                cmd1.Parameters.Add("@c_id", SqlDbType.VarChar).Value = Session["IPDNO"].ToString();
                da = new SqlDataAdapter(cmd1);
                ds = new DataSet();
                DataTable dt1 = new DataTable();
                da.Fill(ds, "STOCK");
                dt1 = ds.Tables["STOCK"];
                int i = 0, k = 0;
                ds2.PROBILL.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.PROBILL.Rows.Add(new string[] { dtr["BEDNO"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["DATETIME"].ToString(), dtr["DESCRIPTION"].ToString(), dtr["CHARGES"].ToString(), dtr["VN"].ToString(), dtr["CATEGORY"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["PNAME"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["GENDER"].ToString(), dtr["UHID"].ToString() });
                    k++;
                    i += 1;
                }
                //da = new SqlDataAdapter("select A.BEDNO,A.VOUCHERNO,A.DATETIME,A.DESCRIPTION,A.CHARGES,A.VN,A.CATEGORY,ISNULL( B.CREDIT,0)AS CREDIT,-+B.DEBIT AS DEBIT,C.ADDRESS,C.EMAIL,C.GSTNO,C.NAME,C.PHONE+','+C.PHONE2 AS PHONE,C.REGNO,D.NAME AS PNAME,D.AGE,D.PADDRESS,D.GENDER from PA_TRANS A,PA_MASTER B,ORG_TABLE C,ADMISSION_TABLE D WHERE A.VN=B.VN AND A.VN=D.VN AND A.VN='" + Session["IPDNO"].ToString() + "'", con);
                //ds = new DataSet();
                //DataTable dt1 = new DataTable();
                //da.Fill(ds, "STOCK");
                //dt1 = ds.Tables["STOCK"];
                //int i = 0, k = 0;
                //ds2.PROBILL.Rows.Clear();
                //while (i < dt1.Rows.Count)
                //{
                //    dtr = dt1.Rows[i];
                //    ds2.PROBILL.Rows.Add(new string[] { dtr["BEDNO"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["DATETIME"].ToString(), dtr["DESCRIPTION"].ToString(), dtr["CHARGES"].ToString(), dtr["VN"].ToString(), dtr["CATEGORY"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["PNAME"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["GENDER"].ToString() });
                //    k++;
                //    i += 1;
                //}
                RECEPTION_PatientBill cr = new RECEPTION_PatientBill();
                rodc.Load(Server.MapPath("~/REPORTS/probill.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();
                cr.rodc.Close();
                cr.rodc.Dispose();
            }


        }
        con.Close();
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