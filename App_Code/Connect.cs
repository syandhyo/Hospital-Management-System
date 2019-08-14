using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
/// <summary>
/// Summary description for CODE
/// </summary>
public class Connect
{
	public SqlConnection con;
    DataSet ds;
    public SqlDataAdapter da;
    SqlCommand cmd;
    public void LOAD()
    {
        con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["abcd"].ToString());
    }
    public DataSet RetDataset(string Table, string Fields, string Cond)
    {
        try
        {
            LOAD();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            da = new SqlDataAdapter("select " + Fields + " from " + Table + " " + Cond, con);
            ds = new DataSet();
            da.Fill(ds, Table);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return ds;
        }
        catch (Exception ee)
        {
            return ds;
        }
    }
    public bool Check(string query)
    {
        bool b = false;
        try
        {
            LOAD();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd = new SqlCommand(query, con);
            SqlDataReader dr= cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                b= true;
            }
            else {
                dr.Close();
                b= false;
            }
            return b;
        }
        catch (Exception ee)
        {
            return true;
        }
    }
    public DataSet RetDataset(string qry)
     {
        try
        {
            LOAD();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            da = new SqlDataAdapter(qry, con);
            ds = new DataSet();
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return ds;
        }
        catch (Exception ee)
        {
            return ds;
        }
    }
    public static bool Bind_GridView(GridView gv, DataSet ds)
    {
        try
        {
            gv.DataSource = ds;
            gv.DataBind();
            return true;
        }
        catch (Exception ee)
        {
            return false;
        }
    }
    public static bool Bind_GridView(GridView gv, DataSet ds, int tbl)
    {
        try
        {
            gv.DataSource = ds.Tables[tbl];
            gv.DataBind();
            return true;
        }
        catch (Exception ee)
        {
            return false;
        }
    }
    public bool Insert(string Table, string Fields, string Value)
    {
        try
        {
            LOAD();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd = new SqlCommand("insert into " + Table + "(" + Fields + ") values(" + Value + ")", con);
            cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return true;
        }
        catch (Exception ee)
        {
            return false;
        }
    }
    public bool Update(string Table, string Value, string Cond)
    {
        try
        {
            LOAD();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd = new SqlCommand("update " + Table + " set " + Value + Cond, con);
            cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return true;
        }
        catch (Exception ee)
        {
            return false;
        }
    }
    public void Filldatalist(string Table, string Fields, string Cond, DataList dl)
    {
        try
        {
            LOAD();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            da = new SqlDataAdapter("select " + Fields + " from " + Table + " " + Cond, con);
            ds = new DataSet();
            da.Fill(ds, Table);
            dl.DataSource = ds;
            dl.DataBind();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ee)
        {
        }
    }
    public void FillGridView(string Table, string Fields, string Cond, GridView dl)
    {
        try
        {
            LOAD();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            da = new SqlDataAdapter("select " + Fields + " from " + Table + " " + Cond, con);
            ds = new DataSet();
            da.Fill(ds, Table);
            dl.DataSource = ds;
            dl.DataBind();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ee)
        {
        }
    }
    public static bool BindDropDownList(DataSet ds,string ValueField,string TextField, DropDownList dll)
    {
        //ds = CODE.ReturnDS(x);//select YY_MM_DT from MONTHLY_PAYOUT_DTL");
        try
        {
            dll.DataSource = ds.Tables[0];
            dll.DataValueField = ValueField;
            dll.DataTextField = TextField;
            dll.DataBind();
            return true;
        }
        catch (Exception ee)
        {
            return false;
        }
    }
    public SqlCommand ReturnCommand(string SP_Name, string[] Parameter, string[] Fields)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {            
            
            cmd = new SqlCommand(SP_Name, con);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            if (Parameter.Length == Fields.Length)
            {
                for (int x = 0; x < Parameter.Length; x++)
                {
                    cmd.Parameters.AddWithValue(Parameter[x].ToString(), Fields[x].ToString());
                }
            }
            return cmd;
        }
        catch (Exception ee)
        {
            return cmd;
        }
    }
    public bool ExecuteCmd(SqlCommand cmd)
    {
        try
        {
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception ee)
        {
            con.Close();
            return false;
        }
    }
    public DataSet RetDataset_DataAdapter(SqlCommand cmd)
    {
        try
        {
            ds = new DataSet();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.SelectCommand.Connection = con;
                da.Fill(ds);
            }
            return ds;
        }
        catch (Exception ee)
        {
            return ds;
        }
    }

    //====================================================================
    public void Clr_Txt(TextBox[] txt)
    {
        for (int x = 0; x <= txt.Length - 1; x++)
        {
            txt[x].Text = "";
        }
    }
    public void Clr_HTM(HtmlControl[] htm, bool v)
    {
        if (v == true)
        {
            for (int x = 0; x <= htm.Length - 1; x++)
            {
                htm[x].Visible = true;
            }
        }
        else
        {
            for (int x = 0; x <= htm.Length - 1; x++)
            {
                htm[x].Visible = false;
            }
        }
    }
    public void EnableControl(WebControl[] cnt, bool v)
    {
        if (v == true)
        {
            for (int x = 0; x <= cnt.Length - 1; x++)
            {
                cnt[x].Enabled = true;
            }
        }
        else
        {
            for (int x = 0; x <= cnt.Length - 1; x++)
            {
                cnt[x].Enabled = false;
            }
        }
    }
    //====================================================================
    public string RandomString(int intLength)
    {
        string str1 = "abcdefghijklmnopqrstuvwxyz0123456789";
        char[] chrChars;
        chrChars = str1.ToCharArray();
        StringBuilder strReturn = new StringBuilder();
        Random grtRandom = new Random();
        do
        {
            int y = grtRandom.Next(1, 36);
            strReturn.Append(chrChars[y]);
        } while (strReturn.Length != intLength);
        return strReturn.ToString();
    }
    public static int RandomNumber()
    {
        Random random = new Random();
        return random.Next(1000000, 9999999);
    }
    //====================================================================
    public static void ExportToExcel(DataSet ds, HttpResponse Response)
    {
        Response.Clear();
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
        dg.DataSource = ds.Tables[0];
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    //===============================================
    public  void inserting(string query, string tb)
    {
        LOAD();
        string str = query;
        string table = tb;
        cmd = new SqlCommand();
        cmd.CommandText = "insert into " + tb + " values(" + query + ")";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }


    }

    public void FillDropDownList(string strQuery, string TextField, string ValueField, DropDownList drp)
    {
        LOAD();
        SqlDataAdapter adp = new SqlDataAdapter(strQuery, con);
        ds = new DataSet();
        adp.Fill(ds);
        if(ds!=null)
        {
            drp.DataSource=ds;
            drp.DataTextField = ds.Tables[0].Columns[TextField].ToString();
            drp.DataValueField = ds.Tables[0].Columns[ValueField].ToString();
            drp.DataBind();
            drp.Items.Insert(0, "--SELECT--");
        }
    }
  
    public void FillDropDownList1(string strQuery, string TextField, string ValueField, DropDownList drp)
    {
        LOAD();
        SqlDataAdapter adp = new SqlDataAdapter(strQuery, con);
        ds = new DataSet();
        adp.Fill(ds);
        if (ds != null)
        {
            drp.DataSource = ds;
            drp.DataTextField = ds.Tables[0].Columns[TextField].ToString();
            drp.DataValueField = ds.Tables[0].Columns[ValueField].ToString();
            drp.DataBind();
            //drp.Items.Insert(0, "--SELECT--");
        }
    }
    public void FillChkboxList(string strQuery, string TextField, string ValueField, CheckBoxList chkbl)
    {
        LOAD();
        SqlDataAdapter adp = new SqlDataAdapter(strQuery, con);
        ds = new DataSet();
        adp.Fill(ds);
        chkbl.DataSource = ds;
        chkbl.DataTextField = ds.Tables[0].Columns[TextField].ToString();
        chkbl.DataValueField = ds.Tables[0].Columns[ValueField].ToString();
        chkbl.DataBind();
    }
    public object ReadOneData(string query)
    {
        LOAD();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        cmd = new SqlCommand(query, con);
        object o = cmd.ExecuteScalar();
        return o;
    }

    public int GenerateId(string table, string field)
    {
        LOAD();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        cmd = new SqlCommand("Select count(" + field + ") from " + table, con);
        int i = 0;
        i = (int)cmd.ExecuteScalar();
        i++;
        return i;
    }
    public int GenerateMaxId(string table, string field)
    {
        LOAD();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        cmd = new SqlCommand("Select count(" + field + ") from " + table, con);
        int i = 0;
        i = (int)cmd.ExecuteScalar();
        if(i!=0)
        {
            cmd = new SqlCommand("Select max(" + field + ") from " + table, con);
            i = (int)cmd.ExecuteScalar();
        }
        i++;
        return i;
    }
    public void DML(string query)
    {
        try
        {
            LOAD();
            da = new SqlDataAdapter(query, con);
            ds = new DataSet();
            da.Fill(ds);
        }
        catch (Exception e) { }
    }
}
