using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;


public partial class _Default : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand com;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    int i, no, no1, sl;
    string num1 = "SJ000";
    DataMathods OBJ_METHOD = new DataMathods();
    protected void Page_Load(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);
        ImageButton1.Attributes.Add("style", "cursor: pointer;");
        try
        {

            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
               // cmd.CommandText = "select A.MODULE,A.selected,B.NAME from USER_MODULE_TABLE A,ORG_TABLE B where A.ID='" + Session["UID"].ToString() + "'";
                string usrassignedmods = "select A.MODULE,A.selected,B.NAME from USER_MODULE_TABLE A,ORG_TABLE B where A.USER_LOGIN_SLNO='" + Session["USLNO"].ToString() + "'";
                DataSet dsmods = OBJ_METHOD.Get_DataSet(usrassignedmods);
                //cmd.Connection = con;

                //using (SqlDataReader sdr = cmd.ExecuteReader())
                //{
                    //while (sdr.Read())
                if (Session["USLNO"].ToString() == "1")
                {
                    string strorg = "select B.NAME from ORG_TABLE B ";
                    DataSet dsorg = OBJ_METHOD.Get_DataSet(strorg);
                    Label1.Text = dsorg.Tables[0].Rows[0]["NAME"].ToString();
                    LinkButtonADMIN.Visible = true;
                    LinkButtonACCOUNTS.Visible = true;

                    LinkButtonLAB.Visible = true;
               
                    LinkButtonNURSE.Visible = true;
                
                    LinkButtonOT.Visible = true;
                    LinkButtonPHARMACY.Visible = true;
                
                    LinkButtonRECEPTION.Visible = true;
                
                    LinkButtonSTOCK.Visible = true;
                
                    LinkButtonSTOREKEEPER.Visible = true;
                
                    LinkButtonRADIO.Visible = true;
                    LinkButtonASSET.Visible = true;
                }
                else
                {
                    if (dsmods.Tables[0].Rows.Count > 0)
                    {
                        //Label1.Text = sdr["NAME"].ToString();
                        Label1.Text = dsmods.Tables[0].Rows[0]["NAME"].ToString();
                        for (int i = 0; i < dsmods.Tables[0].Rows.Count; i++)
                        {
                            string modulename = dsmods.Tables[0].Rows[i]["MODULE"].ToString();
                            string selected = dsmods.Tables[0].Rows[i]["selected"].ToString();

                            if (modulename == LinkButtonADMIN.Text && selected == "True")
                            {
                                LinkButtonADMIN.Visible = true;
                            }
                            else if (modulename == LinkButtonACCOUNTS.Text && selected == "True")
                            {
                                LinkButtonACCOUNTS.Visible = true;
                            }
                            else if (modulename == LinkButtonLAB.Text && selected == "True")
                            {
                                LinkButtonLAB.Visible = true;
                            }
                            else if (modulename == LinkButtonNURSE.Text && selected == "True")
                            {
                                LinkButtonNURSE.Visible = true;
                            }
                            else if (modulename == LinkButtonOT.Text && selected == "True")
                            {
                                LinkButtonOT.Visible = true;
                            }
                            else if (modulename == LinkButtonPHARMACY.Text && selected == "True")
                            {
                                LinkButtonPHARMACY.Visible = true;
                            }
                            else if (modulename == LinkButtonRECEPTION.Text && selected == "True")
                            {
                                LinkButtonRECEPTION.Visible = true;
                            }
                            else if (modulename == LinkButtonSTOCK.Text && selected == "True")
                            {
                                LinkButtonSTOCK.Visible = true;
                            }
                            else if (modulename == LinkButtonSTOREKEEPER.Text && selected == "True")
                            {
                                LinkButtonSTOREKEEPER.Visible = true;
                            }
                            else if (modulename == LinkButtonRADIO.Text && selected == "True")
                            {
                                LinkButtonRADIO.Visible = true;
                            }
                            else if (modulename == LinkButtonASSET.Text && selected == "True")
                            {
                                LinkButtonASSET.Visible = true;
                            }
                        }
                    }
                }
                //}

            }
            //con.Close();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/index.aspx");
        }
     
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        System.Web.Security.FormsAuthentication.SignOut();
        Session["out"] = "INACTIVE";
        Response.Redirect("~/index.aspx");
    }
    protected void LinkButtonADMIN_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/Adminhome.aspx");
    }
    protected void LinkButtonSTOREKEEPER_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/STOREKEEPER/Home.aspx");
    }
    protected void LinkButtonACCOUNTS_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("~/ACCOUNTS/Default.aspx");
    }
    protected void LinkButtonLAB_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/LABORATORY/Home.aspx");
    }
    protected void LinkButtonNURSE_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("~/NURSE/Home.aspx");
    }
    protected void LinkButtonOT_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/OT/Home.aspx");
    }
    protected void LinkButtonPHARMACY_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("~/PHARMACYSTORE/StoreType.aspx");
    }
    protected void LinkButtonRECEPTION_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("~/RECEPTION/Default.aspx");
    }
    protected void LinkButtonSTOCK_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/GENERALSTOCK/Home.aspx");
    }
    protected void LinkButtonRADIOLOGY_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/RADIOLOGY/Home.aspx");
    }
    protected void LinkButtonASSET_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/ASSET/Home.aspx");
    }
}