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
public partial class login2 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand com;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    int i, no, no1, sl;
    string num1 = "SJ000";
    string STR = "";
    DataMathods OBJ_METHOD = new DataMathods();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        
        try
        {

            string chkuser = "select NAME,ORGID,ID,STATUS,PASSWORD,BRANCH_ID,slno,USERTYPE,STORE_ID from  LOGIN_TABLE where USERNAME ='" + txtusername.Text + "' and  PASSWORD ='" + txtpassword.Text + "' and STATUS='ACTIVE'";

            DataSet dsusr = OBJ_METHOD.Get_DataSet(chkuser);

            //com = new SqlCommand("select NAME,ORGID,ID,STATUS,PASSWORD,BRANCH_ID from  LOGIN_TABLE where USERNAME ='" + txtusername.Text + "'", con);
            // SqlDataReader dr;
            
            if (dsusr.Tables[0].Rows.Count>0)
            {
                
                    if (Convert.ToInt32(dsusr.Tables[0].Rows[0]["slno"].ToString())==1)
                    {
                        Session["USLNO"] = dsusr.Tables[0].Rows[0]["slno"].ToString();
                        Session["UID"] = dsusr.Tables[0].Rows[0]["ID"].ToString();
                        Session["NAME"] = dsusr.Tables[0].Rows[0]["NAME"].ToString();
                        Session["ORGID"] = dsusr.Tables[0].Rows[0]["ORGID"].ToString();
                        STR = dsusr.Tables[0].Rows[0]["USERTYPE"].ToString();
                        if (STR == "pharma")
                        {
                            Session["STOREID"] = dsusr.Tables[0].Rows[0]["STORE_ID"].ToString();
                        }
                        Session["out"] = "ACTIVE";
                        Session["denyper"] = "";
                        Session["denymod"] = "";
                        Response.Redirect("~/AuthenticateUser.aspx");
                    }
                    else
                    {
                        Session["USLNO"] = dsusr.Tables[0].Rows[0]["slno"].ToString();
                        Session["UID"] = dsusr.Tables[0].Rows[0]["ID"].ToString();
                        Session["NAME"] = dsusr.Tables[0].Rows[0]["NAME"].ToString();
                        Session["ORGID"] = dsusr.Tables[0].Rows[0]["ORGID"].ToString();
                        Session["out"] = "ACTIVE";
                        STR = dsusr.Tables[0].Rows[0]["USERTYPE"].ToString();
                        Session["USERTYPE"] = dsusr.Tables[0].Rows[0]["USERTYPE"].ToString();
                        if (STR == "pharma")
                        {
                            Session["STOREID"] = dsusr.Tables[0].Rows[0]["STORE_ID"].ToString();
                            string getactivebranchfyid = "select ID,FYEAR,ORGID from  FYEAR1_TABLE where  STATUS='Active' and   BRANCH_ID=" + dsusr.Tables[0].Rows[0]["BRANCH_ID"].ToString();

                            DataSet dsfyid = OBJ_METHOD.Get_DataSet(getactivebranchfyid, false, false, null);
                            if (dsfyid.Tables[0].Rows.Count > 0)
                            {
                                Session["BRANCH_FYR"] = Convert.ToInt32(dsfyid.Tables[0].Rows[0][0]);
                                Session["Branch"] = dsusr.Tables[0].Rows[0]["BRANCH_ID"].ToString();


                                string strper = "select PER from USER_PERMISSION_TABLE where selected=0 and USER_LOGIN_SLNO =" + Session["USLNO"].ToString();
                                DataSet dsper = OBJ_METHOD.Get_DataSet(strper);
                                if (dsper.Tables[0].Rows.Count > 0)
                                {
                                    string sp = "";
                                    for (int i = 0; i < dsper.Tables[0].Rows.Count; i++)
                                    {
                                        sp = sp + "," + dsper.Tables[0].Rows[i][0].ToString();
                                    }
                                    Session["denyper"] = sp.Substring(1);
                                }

                                string strmod = "select MODULE from USER_MODULE_TABLE where selected=0 and USER_LOGIN_SLNO =" + Session["USLNO"].ToString();
                                DataSet dsmod = OBJ_METHOD.Get_DataSet(strmod);
                                if (dsmod.Tables[0].Rows.Count > 0)
                                {
                                    string sp = "";
                                    for (int i = 0; i < dsmod.Tables[0].Rows.Count; i++)
                                    {
                                        sp = sp + "," + dsmod.Tables[0].Rows[i][0].ToString();
                                    }
                                    Session["denymod"] = sp.Substring(1);
                                }
                                Response.Redirect("~/PHARMACYSTORE/home.aspx");
                            }
                            else
                            {

                                Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid  Username. Try Again.')</script>");

                            }
                        }
                        else
                        {
                            string getactivebranchfyid = "select ID,FYEAR,ORGID from  FYEAR1_TABLE where  STATUS='Active' and   BRANCH_ID=" + dsusr.Tables[0].Rows[0]["BRANCH_ID"].ToString();

                            DataSet dsfyid = OBJ_METHOD.Get_DataSet(getactivebranchfyid, false, false, null);
                            if (dsfyid.Tables[0].Rows.Count > 0)
                            {
                                Session["BRANCH_FYR"] = Convert.ToInt32(dsfyid.Tables[0].Rows[0][0]);
                                Session["Branch"] = dsusr.Tables[0].Rows[0]["BRANCH_ID"].ToString();


                                string strper = "select PER from USER_PERMISSION_TABLE where selected=0 and USER_LOGIN_SLNO =" + Session["USLNO"].ToString();
                                DataSet dsper = OBJ_METHOD.Get_DataSet(strper);
                                if (dsper.Tables[0].Rows.Count > 0)
                                {
                                    string sp = "";
                                    for (int i = 0; i < dsper.Tables[0].Rows.Count; i++)
                                    {
                                        sp = sp + "," + dsper.Tables[0].Rows[i][0].ToString();
                                    }
                                    Session["denyper"] = sp.Substring(1);
                                }

                                string strmod = "select MODULE from USER_MODULE_TABLE where selected=0 and USER_LOGIN_SLNO =" + Session["USLNO"].ToString();
                                DataSet dsmod = OBJ_METHOD.Get_DataSet(strmod);
                                if (dsmod.Tables[0].Rows.Count > 0)
                                {
                                    string sp = "";
                                    for (int i = 0; i < dsmod.Tables[0].Rows.Count; i++)
                                    {
                                        sp = sp + "," + dsmod.Tables[0].Rows[i][0].ToString();
                                    }
                                    Session["denymod"] = sp.Substring(1);
                                }



                                Response.Redirect("~/home.aspx");
                            }
                            else
                            {

                                Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid  Username. Try Again.')</script>");

                            }
                        }


                        
                    }
                


            }
            else
            {

                
                Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid  Username. Try Again.')</script>");
                return;
            }

            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}