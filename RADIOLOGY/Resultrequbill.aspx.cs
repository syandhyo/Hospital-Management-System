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

public partial class RADIOLOGY_Resultrequbill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    SqlDataReader dr;
    DataSet2 ds2 = new DataSet2();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    ReportDocument rodc1 = new ReportDocument();
    RADIOLOGY_Resultrequbill cr;
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
        string R = Session["RADID"].ToString();
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
        if(Session["mode"] == "true")
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("RADIO_REQUISATION_REPORT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_OP";
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = R.ToString();
                    da1 = new SqlDataAdapter(cmd);
                    //da1 = new SqlDataAdapter("select d.NAME orgNm,d.ADDRESS orgAdd,d.PHONE orgph,d.EMAIL orgemail,a.ID  reqId,a.DATE  reqDt,a.TESTRESULT reqTest,a.PACKAGE reqpack,a.FINDING reqfindg,a.IMPRESSION reqimpres,b.DATE  rdiogDt,b.ID rdiogId,c.FNAME regNm,c.AGE regAg,c.GENDER regGendr,c.REFFRALNAME regrefNm,e.DOCTORNAME consNm,e.PHOTO signPh,e.DOCTORNAME dNmconslRd,f.DOCTORNAME dNmreqradGp,e.PHOTO phtConsRd,f.PHOTO phtRdGp   from  TBL_RESULTREQN a,TBL_RADIOLGYREQ b,REGISTRATION_TBL c,ORG_TABLE d,TBL_DOCTORENTY e,TBL_DOCTORENTY f where a.REQID=b.ID   and b.OPDNO=c.ID and  a.CONSULTRADIO=e.ID and f.ID=a.RADIOGRAPH and a.ID='" + R.ToString() + "'", con);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);

            }
        }
        else if( Session["mode"] == "false")
        {
            try
            {
                using (SqlCommand cmd1 = new SqlCommand("RADIO_REQUISATION_REPORT", con))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_IP";
                    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = R.ToString();
                    da1 = new SqlDataAdapter(cmd1);
                    //da1 = new SqlDataAdapter("select top 1 d.NAME orgNm,d.ADDRESS orgAdd,d.PHONE orgph,d.EMAIL orgemail,a.ID  reqId,a.DATE  reqDt,a.TESTRESULT reqTest,a.PACKAGE reqpack,a.FINDING reqfindg,a.IMPRESSION reqimpres,b.DATE  rdiogDt,b.ID rdiogId,g.PNAME regNm,c.AGE regAg,c.GENDER regGendr, c.REFFRALNAME regrefNm,e.DOCTORNAME consNm,e.PHOTO signPh,e.DOCTORNAME dNmconslRd,f.DOCTORNAME dNmreqradGp,e.PHOTO phtConsRd, f.PHOTO phtRdGp  from  TBL_RESULTREQN a,TBL_RADIOLGYREQ b,REGISTRATION_TBL c,ORG_TABLE d,TBL_DOCTORENTY e,TBL_DOCTORENTY  f,BED_TABLE g where a.REQID=b.ID  and b.OPDNO=g.VN and a.CONSULTRADIO=e.ID and f.ID=a.RADIOGRAPH   and a.ID='" + R.ToString() + "'", con);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);

            }
        }
        //da1 = new SqlDataAdapter("select a.OPDNO,a.NAME,a.ID,a.DATE,b.INV,b.PRICEIT,c.NAME AS NAMEORG,c.PHONE,c.GSTNO,c.ADDRESS,a.USERID from  TBL_RADIOLGYREQ a,TBL_RADIOLGYRE_ITEM b,ORG_TABLE c where a.ID=b.ID and a.ID='" + R.ToString() + "'", con);
       
        
        ds = new DataSet();
        DataTable dt11 = new DataTable();
        da1.Fill(ds, "STOCK");
        dt11 = ds.Tables["STOCK"];
        int i1 = 0, k1 = 0;
        ds2.RADIOLOG_REPORT.Rows.Clear();
        while (i1 < dt11.Rows.Count)
        {
            dtr = dt11.Rows[i1];
            ds2.RADIOLOG_REPORT.Rows.Add(new string[] { dtr["orgNm"].ToString(), dtr["orgAdd"].ToString(), dtr["orgph"].ToString(), dtr["orgemail"].ToString(), dtr["reqId"].ToString(), dtr["reqDt"].ToString(), dtr["reqTest"].ToString(), dtr["reqpack"].ToString(), dtr["reqfindg"].ToString(), dtr["reqimpres"].ToString(), dtr["rdiogDt"].ToString(), dtr["rdiogId"].ToString(), dtr["regNm"].ToString(), dtr["regAg"].ToString(), dtr["regGendr"].ToString(), dtr["regrefNm"].ToString(), dtr["consNm"].ToString(), dtr["signPh"].ToString(), dtr["dNmconslRd"].ToString(), dtr["dNmreqradGp"].ToString(), dtr["phtConsRd"].ToString(), dtr["phtRdGp"].ToString() });
            k1++;
            i1 += 1;
        }

        cr = new RADIOLOGY_Resultrequbill();
        rodc.Load(Server.MapPath("~/MATREPORT/RadiologyReport.rpt"));
        rodc.SetDataSource(dt11);
        //rodc1.Load(Server.MapPath("~/REPORTS/labreport.rpt"));
        //rodc1.SetDataSource(dt11);
        // rodc1.Subreports[0].SetDataSource(ds2.Tables["WIDAL"]);
        CrystalReportViewer1.ReportSource = rodc;
        //CrystalReportViewer2.ReportSource = rodc1;
        CrystalReportViewer1.DataBind();
        // CrystalReportViewer2.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        con.Close();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        CloseReport();
        //if (rodc != null)
        //{
        cr = new RADIOLOGY_Resultrequbill();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        rodc1.Close();
        rodc1.Dispose();
        cr.rodc1.Close();
        cr.rodc1.Clone();
        cr.rodc1.Dispose();
        rodc1 = null;
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