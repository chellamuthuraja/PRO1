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
using System.Data.OleDb;
using Microsoft.VisualBasic;
using System.IO;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Summary description for clsLibrary
/// </summary>
public class clsLibrary
{

    public static string constr = Convert.ToString(ConfigurationManager.ConnectionStrings["ChemTag"]);
    public static string SiteTitle = "Craft ";
    string str;
    public string rtn_Html;
    public clsLibrary()
    {
        string str = "";
        //using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("includes/Title.txt")))
        //{
        //    str = sr.ReadToEnd();
        //}
        clsLibrary.SiteTitle = str;
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable bindDataTable()
    {
        try
        {

            DataTable dt = new DataTable();
            string strQry = "select strTM,strTP,strPR,strPH from tbl_Craft_RestrictedCharacters";
            SqlDataAdapter sda = new SqlDataAdapter(strQry, constr);
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;// throw ex;
        }
    }
    public DataTable BatchList(string Query)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(Query, constr);
            ada.SelectCommand.CommandTimeout = 0;
            ada.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ErrorLog("In ExecuteDataSet Query  " + Query, ex);
            return null;

        }
    }

    public DataSet ExecuteDataSet(string Query)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dt = new SqlDataAdapter(Query, constr);
            dt.SelectCommand.CommandTimeout = 0;
            dt.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            ErrorLog("In ExecuteDataSet Query  " + Query, ex);
            return null;

        }
    }
    public DataTable ExecuteSPDatatable(string SpName, string[] ParamNames, string[] ParamValues)
    {
        try
        {

            DataTable ds = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(SpName, constr);
            sda.SelectCommand.CommandText = SpName;
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.CommandTimeout = 0;
            for (int i = 0; i < ParamNames.Length && i < ParamValues.Length; i++)
            {

                if (ParamNames[i].IndexOf("XML") > 0 || ParamNames[i].IndexOf("TanPreview") > 0 || ParamNames[i].IndexOf("xml") > 0)
                {
                    //sda.SelectCommand.Parameters.AddWithValue(ParamNames[i], ParamValues[i]);
                    sda.SelectCommand.Parameters.Add(ParamNames[i], SqlDbType.NText).Value = ParamValues[i];
                }
                else
                {
                    sda.SelectCommand.Parameters.AddWithValue(ParamNames[i], ParamValues[i]);
                }
            }
            sda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            ErrorLog("In ExecuteSPDataSet sPnAME  " + SpName, ex);
            return null;// throw ex;
        }
    }
    public DataSet ExecuteSPDataSet(string SpName, string[] ParamNames, string[] ParamValues)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(SpName, constr);
            sda.SelectCommand.CommandText = SpName;
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.CommandTimeout = 0;
            for (int i = 0; i < ParamNames.Length && i < ParamValues.Length; i++)
            {

                if (ParamNames[i].IndexOf("XML") > 0 || ParamNames[i].IndexOf("TanPreview") > 0 || ParamNames[i].IndexOf("xml") > 0)
                {
                    //sda.SelectCommand.Parameters.AddWithValue(ParamNames[i], ParamValues[i]);
                    sda.SelectCommand.Parameters.Add(ParamNames[i], SqlDbType.NText).Value = ParamValues[i];
                }
                else
                {
                    sda.SelectCommand.Parameters.AddWithValue(ParamNames[i], ParamValues[i]);
                }
            }
            sda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            ErrorLog("In ExecuteSPDataSet sPnAME  " + SpName, ex);
            return null;// throw ex;
        }
    }
    public DataTable ExecuteDataTable(string Query)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(Query, constr);
            sda.SelectCommand.CommandTimeout = 0;
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ErrorLog("In ExecuteDataTable Query  " + Query, ex);

            return null;
        }
    }
    public DataTable ExecuteSPDataTable(string SpName, string[] ParamNames, string[] ParamValues)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(SpName, constr);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.CommandTimeout = 0;
            for (int i = 0; i < ParamNames.Length && i < ParamValues.Length; i++)
            {
                sda.SelectCommand.Parameters.AddWithValue(ParamNames[i], ParamValues[i]);
            }
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ErrorLog("In ExecuteSPDataTable SPName  " + SpName, ex);
            return null;
        }
    }

    public string ExecuteSP(string SpName, string[] ParamNames, string[] ParamValues)
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(SpName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            for (int i = 0; i < ParamNames.Length && i < ParamValues.Length; i++)
            {
                cmd.Parameters.AddWithValue(ParamNames[i], ParamValues[i]);
            }
            if (con.State == ConnectionState.Closed) con.Open();
            string str = Convert.ToString(cmd.ExecuteScalar());
            if (con.State == ConnectionState.Open) con.Close();
            return str;
        }
        catch (Exception ex)
        {
            ErrorLog("In ExecuteSP SPName  " + SpName, ex);
            return null;
        }
    }
    public string ExecuteQuery(string Query)
    {
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand(Query, con);
        cmd.CommandTimeout = 0;

        if (con.State == ConnectionState.Closed) con.Open();
        string str = Convert.ToString(cmd.ExecuteScalar());
        if (con.State == ConnectionState.Open) con.Close();
        return str;
    }
    public void FillDropDown(string query, string TextField, string ValueField, DropDownList ddl)
    {
        SqlDataAdapter sda = new SqlDataAdapter(query, constr);
        DataSet ds = new DataSet();
        sda.SelectCommand.CommandTimeout = 0;
        sda.Fill(ds);
        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataSource = ds.Tables[0];
        ddl.DataBind();

    }

    public void FillDropDown(string SPName, string TextField, string ValueField, DropDownList ddl, string[] ParamNames, string[] ParamValues)
    {
        SqlDataAdapter sda = new SqlDataAdapter(SPName, constr);
        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        sda.SelectCommand.CommandTimeout = 0;
        for (int i = 0; i < ParamNames.Length && i < ParamValues.Length; i++)
        {
            sda.SelectCommand.Parameters.AddWithValue(ParamNames[i], ParamValues[i]);
        }
        DataSet ds = new DataSet();
        sda.Fill(ds);
        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataSource = ds.Tables[0];
        ddl.DataBind();

    }
    public void FillDropDown(string SPName, string[] TextField, string[] ValueField, DropDownList[] ddl, string[] ParamNames, string[] ParamValues)
    {
        SqlDataAdapter sda = new SqlDataAdapter(SPName, constr);
        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        sda.SelectCommand.CommandTimeout = 0;
        for (int i = 0; i < ParamNames.Length && i < ParamValues.Length; i++)
        {
            sda.SelectCommand.Parameters.AddWithValue(ParamNames[i], ParamValues[i]);
        }
        DataSet ds = new DataSet();
        sda.Fill(ds);
        for (int i = 0; i < ds.Tables.Count && i < TextField.Length && i < ValueField.Length && i < ddl.Length; i++)
        {
            ddl[i].DataTextField = TextField[i];
            ddl[i].DataValueField = ValueField[i];
            ddl[i].DataSource = ds.Tables[i];
            ddl[i].DataBind();
        }

    }
    public void FillDropDown(string query, string[] TextField, string[] ValueField, DropDownList[] ddl)
    {
        SqlDataAdapter sda = new SqlDataAdapter(query, constr);
        DataSet ds = new DataSet();
        sda.SelectCommand.CommandTimeout = 0;
        sda.Fill(ds);
        for (int i = 0; i < ds.Tables.Count && i < TextField.Length && i < ValueField.Length && i < ddl.Length; i++)
        {
            ddl[i].DataTextField = TextField[i];
            ddl[i].DataValueField = ValueField[i];
            ddl[i].DataSource = ds.Tables[i];
            ddl[i].DataBind();
        }

    }
    public CheckBoxList FillCheckBoxList(string query, string TextField, string ValueField, CheckBoxList chklst)
    {
        SqlDataAdapter sda = new SqlDataAdapter(query, constr);
        sda.SelectCommand.CommandTimeout = 0;
        DataSet ds = new DataSet();
        sda.Fill(ds);
        chklst.DataTextField = TextField;
        chklst.DataValueField = ValueField;
        chklst.DataSource = ds.Tables[0];
        chklst.DataBind();
        return chklst;
    }
    public CheckBoxList FillCheckBoxList(string SPName, string TextField, string ValueField, CheckBoxList chklst, string[] ParamNames, string[] ParamValues)
    {
        SqlDataAdapter sda = new SqlDataAdapter(SPName, constr);
        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        sda.SelectCommand.CommandTimeout = 0;
        for (int i = 0; i < ParamNames.Length && i < ParamValues.Length; i++)
        {
            sda.SelectCommand.Parameters.AddWithValue(ParamNames[i], ParamValues[i]);
        }
        DataSet ds = new DataSet();
        sda.Fill(ds);
        chklst.DataTextField = TextField;
        chklst.DataValueField = ValueField;
        chklst.DataSource = ds.Tables[0];
        chklst.DataBind();
        return chklst;
    }
    public string RepeatString(string str, int length)
    {
        string str1 = "";
        for (int i = 0; i < length; i++)
        {
            str1 += str;
        }
        return str1;
    }
    public static string RepeatString1(string str, int length)
    {
        string str1 = "";
        for (int i = 0; i < length; i++)
        {
            str1 += str;
        }
        return str1;
    }
    public static void Export(string fileName, GridView gv, string Header)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);


        //   Create a form to contain the grid   
        Table table = new Table();
        table.GridLines = GridLines.Both;

        //   add the header row to the table 
        if (Header != "")
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            tc.ColumnSpan = gv.HeaderRow.Cells.Count;
            tc.Attributes.Add("align", "center");
            tc.Font.Bold = true;
            tc.Font.Size = 14;
            tc.Text = Header;
            tr.Controls.Add(tc);
            table.Controls.Add(tr);
        }
        if (!(gv.HeaderRow == null))
        {
            PrepareControlForExport(gv.HeaderRow);
            table.Rows.Add(gv.HeaderRow);
        }
        //   add each of the data rows to the table   
        foreach (GridViewRow row in gv.Rows)
        {
            PrepareControlForExport(row);


            table.Rows.Add(row);
        }
        //   add the footer row to the table   
        if (!(gv.FooterRow == null))
        {
            PrepareControlForExport(gv.FooterRow);
            table.Rows.Add(gv.FooterRow);
        }
        //   render the table into the htmlwriter   
        table.RenderControl(htw);

        //   render the htmlwriter into the response   
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }

            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }
    public static void WriteLog(Exception ex, string Module)
    {
        try
        {
            if (ex.Message.ToLower().Contains("Thread was being aborted".ToLower()))
                return;
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLog" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(FilePath, true))
            {
                sw.WriteLine(" Error Occured in Module :" + Module + DateTime.Now.ToString());
                for (int i = 0; i < HttpContext.Current.Session.Count; i++)
                {
                    sw.WriteLine(HttpContext.Current.Session.Keys[i] + ":" + HttpContext.Current.Session[i]);
                }
                sw.WriteLine(" Error Message :" + ex.Message);

            }
        }
        catch
        {

        }
    }
    public void BulkCopy(string FilePath, string ExcelQuery, string DestinationTable)
    {
        try
        {
            string ExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" +
                FilePath + "; Extended Properties='Excel 12.0;IMEX=1;HDR=YES;'";
            OleDbDataAdapter oda = new OleDbDataAdapter(ExcelQuery, ExcelConnectionString);
            oda.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            oda.Fill(dt);

            SqlBulkCopy sbc = new SqlBulkCopy(constr);
            sbc.DestinationTableName = DestinationTable;
            sbc.WriteToServer(dt);
            oda.Dispose();
        }
        catch (Exception ex)
        {
            ErrorLog("In BulkCopy ", ex);

        }
    }
    public static void FillReferenceDoc()
    {
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter dt = new SqlDataAdapter("DBP_Craft_GetNRNReference ", constr);
            dt.Fill(ds);
            ds.Tables[0].TableName = "ReferenceDoc";
            ds.Tables[1].TableName = "ReferenceDoc1";

        }
        catch (Exception ex)
        {
            ErrorLog1("FillReferenceDoc()", ex);
        }
        HttpContext.Current.Application["DTReferenceDoc"] = ds;
    }
    public static void NewFillReferenceDoc(string strtan)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter dt = new SqlDataAdapter("DBP_Craft_GetNRNReference_New '" + strtan + "'", constr);
            dt.Fill(ds);
            ds.Tables[0].TableName = "ReferenceDoc";
            ds.Tables[1].TableName = "ReferenceDoc1";

        }
        catch (Exception ex)
        {
            ErrorLog1("FillReferenceDoc()", ex);
        }
        HttpContext.Current.Application["DTReferenceDoc"] = ds;
    }
    public void BindGrid(string query, GridView gv)
    {
        try
        {
            DataTable dt = ExecuteDataTable(query);
            gv.DataSource = dt;
            gv.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLog("In BindGrid ", ex);

        }
    }
    public void BulkCopy2(string FilePath, string ExcelQuery, string DestinationTable)
    {
        try
        {
            string ExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" +
                FilePath + "; Extended Properties='Excel 12.0;IMEX=1;HDR=YES;'";

            string paddingcharacter = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            paddingcharacter += "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

            OleDbDataAdapter oda = new OleDbDataAdapter(ExcelQuery, ExcelConnectionString);
            oda.SelectCommand.CommandTimeout = 0;
            DataTable dt1 = new DataTable();
            oda.Fill(dt1);

            string ExcelQuery2 = "select Max(len(TAN)),Max(len(RN)),Max(len(RNID)),Max(len(INN)),Max(len(CAN)),Max(len(SYN)),Max(len(INH)),Max(len(INOH)),Max(len(INO)),Max(len(ONH)) From [Sheet1$] ";
            OleDbDataAdapter oda2 = new OleDbDataAdapter(ExcelQuery2, ExcelConnectionString);
            DataTable dt2 = new DataTable();
            // oda2.Fill(dt2);

            dt2.Columns.Add("col1");
            dt2.Columns.Add("col2");
            dt2.Columns.Add("col3");
            dt2.Columns.Add("col4");
            dt2.Columns.Add("col5");
            dt2.Columns.Add("col6");
            dt2.Columns.Add("col7");
            dt2.Columns.Add("col8");
            dt2.Columns.Add("col9");
            dt2.Columns.Add("col10");
            DataRow dr = dt1.NewRow();
            dr[0] = "MaxColumnvalueRow";
            string[] str = { "2000", "2000", "2000", "2000", "2000", "2000", "2000", "2000", "2000", "2000" };
            dt2.Rows.Add(str);
            for (int i = 1; i < dt2.Columns.Count; i++)
            {
                try
                {
                    if (dt1.Columns[i].DataType.ToString() == "System.String")
                    {
                        dr[i] = paddingcharacter;
                    }
                    else if (dt1.Columns[i].DataType.ToString() == "System.Int32" || dt1.Columns[i].DataType.ToString() == "System.Int16" || dt1.Columns[i].DataType.ToString() == "System.Int64" || dt1.Columns[i].DataType.ToString() == "System.Double")
                    {
                        //dr[i] = RepeatString("0", (int)Conversion.Val(dt2.Rows[0][i]));
                     //   dr[i] = RepeatString("1", ((int)Conversion.Val(dt2.Rows[0][i])) + 1);
                    }
                    else if (dt1.Columns[i].DataType.ToString() == "System.DateTime")
                    {
                        dr[i] = DateTime.Now.ToString();
                    }
                }
                catch
                {
                }

            }

            dt1.Rows.InsertAt(dr, 0);
            SqlBulkCopy sbc = new SqlBulkCopy(constr);
            sbc.DestinationTableName = DestinationTable;
            sbc.WriteToServer(dt1);
            oda.Dispose();
        }
        catch (Exception ex)
        {
            ErrorLog("In BulkCopy2 ", ex);

        }
    }
    public string SetTitle()
    {
        try
        {
            string str = "";
            using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("includes/Version.txt")))
            {
                str = sr.ReadToEnd();

            }
            SiteTitle = str;
            return str;

        }
        catch (Exception ex)
        {
            ErrorLog("In SetTile ", ex);
            return "";
        }
    }
    public void ErrorLog(string Message, Exception ex)
    {
        try
        {
            if (ex.Message == "Thread was being aborted." || ex.Message == "Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.")
            {
                return;
            }
            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("Log")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("Log"));
                }
            }
            catch { }
            string FileName = HttpContext.Current.Server.MapPath("Log//ErrorLog " + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            using (StreamWriter sw = new StreamWriter(FileName, true))
            {
                sw.WriteLine(RepeatString(" ", 60));
                sw.WriteLine(RepeatString("=", 60));
                sw.WriteLine("USer Session: " + HttpContext.Current.Session["UserName"]);
                sw.WriteLine("USer Role: " + HttpContext.Current.Session["UserCatg"]);
                sw.WriteLine("Time: " + DateTime.Now);
                sw.WriteLine(RepeatString("=", 60));
                sw.WriteLine("HttpContext.Current.Request.Url.OriginalString :" + HttpContext.Current.Request.Url.OriginalString);
                try
                {
                    sw.WriteLine("HttpContext.Current.Request.UrlReferrer.OriginalString :" + HttpContext.Current.Request.UrlReferrer.OriginalString);
                }
                catch { }
                sw.WriteLine(RepeatString("=", 60));
                sw.WriteLine(RepeatString(" ", 60));
                sw.WriteLine(Message);
                sw.WriteLine(ex.Message.ToString());
                sw.WriteLine(RepeatString("=", 60));
            }
        }
        catch
        {
        }
    }
    public static void ErrorLog1(string Message, Exception ex)
    {
        try
        {
            if (ex.Message == "Thread was being aborted.")
            {
                return;
            }
            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("Log")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("Log"));
                }
            }
            catch { }
            string FileName = HttpContext.Current.Server.MapPath("ErrorLog " + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            using (StreamWriter sw = new StreamWriter(FileName, true))
            {
                sw.WriteLine(RepeatString1(" ", 60));
                sw.WriteLine(RepeatString1("=", 60));
                sw.WriteLine("USer Session: " + HttpContext.Current.Session["UserName"]);
                sw.WriteLine("USer Role: " + HttpContext.Current.Session["UserCatg"]);
                sw.WriteLine("Time: " + DateTime.Now);
                sw.WriteLine(RepeatString1("=", 60));
                sw.WriteLine("HttpContext.Current.Request.Url.OriginalString :" + HttpContext.Current.Request.Url.OriginalString);
                try
                {
                    sw.WriteLine("HttpContext.Current.Request.UrlReferrer.OriginalString :" + HttpContext.Current.Request.UrlReferrer.OriginalString);
                }
                catch { }
                sw.WriteLine(RepeatString1("=", 60));
                sw.WriteLine(RepeatString1(" ", 60));
                sw.WriteLine(Message);
                sw.WriteLine(ex.Message.ToString());
                sw.WriteLine(RepeatString1("=", 60));
            }
        }
        catch
        {
        }


    }

    public string fn_NRNRGE_NumberCheck(Dictionary<string, string> NRNReg)
    {

        StringBuilder HtmlTableFormat = new StringBuilder();
        HtmlTableFormat.Append("<table border style=\"1px solid #FFFFFF\"><tr><td>NRN Num</td><td>NRN Reg</td><td>PStatus</td><td>RStatus</td><td>AStatus</td><td>CStatus</td><td>SStatus</td></tr>");
        foreach (KeyValuePair<string, string> pair in NRNReg)
        {
            if (pair.Value != string.Empty)
            {

                string query = "select iPstatus,iRstatus,iAstatus,iCstatus,iSstatus  from TBL_CRAFT_RoleCode where iREG_NUM=" + pair.Value + ";";
                SqlDataAdapter sda = new SqlDataAdapter(query, constr);
                DataTable dt = new DataTable();
                sda.SelectCommand.CommandTimeout = 0;
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        HtmlTableFormat.Append("<tr><td>" + pair.Key + "</td><td>" + pair.Value + "</td>");

                        if (dr["iPstatus"].ToString() != "0")
                        {
                            HtmlTableFormat.Append("<td>1</td>");

                        }
                        else if (dr["iPstatus"].ToString() == "0")
                        {
                            HtmlTableFormat.Append("<td>0</td>");
                        }
                        else if (dr["iPstatus"].ToString() == null)
                        {
                            HtmlTableFormat.Append("<td>Not Ok</td>");
                        }

                        if (dr["iRstatus"].ToString() != "0")
                        {
                            HtmlTableFormat.Append("<td>1</td>");

                        }
                        else if (dr["iRstatus"].ToString() == "0")
                        {
                            HtmlTableFormat.Append("<td>0</td>");
                        }
                        else if (dr["iRstatus"].ToString() == null)
                        {
                            HtmlTableFormat.Append("<td>Not Ok</td>");
                        }

                        if (dr["iAstatus"].ToString() != "0")
                        {
                            HtmlTableFormat.Append("<td>1</td>");

                        }
                        else if (dr["iAstatus"].ToString() == "0")
                        {
                            HtmlTableFormat.Append("<td>0</td>");
                        }
                        else if (dr["iAstatus"].ToString() == null)
                        {
                            HtmlTableFormat.Append("<td>Not Ok</td>");
                        }

                        if (dr["iCstatus"].ToString() != "0")
                        {
                            HtmlTableFormat.Append("<td>1</td>");
                        }
                        else if (dr["iCstatus"].ToString() == "0")
                        {
                            HtmlTableFormat.Append("<td>0</td>");
                        }
                        else if (dr["iCstatus"].ToString() == null)
                        {
                            HtmlTableFormat.Append("<td>Not Ok</td>");
                        }

                        if (dr["iSstatus"].ToString() != "0")
                        {
                            HtmlTableFormat.Append("<td>1</td>");
                        }
                        else if (dr["iSstatus"].ToString() == "0")
                        {
                            HtmlTableFormat.Append("<td>0k</td>");
                        }
                        else if (dr["iSstatus"].ToString() == null)
                        {
                            HtmlTableFormat.Append("<td>Not Ok</td>");
                        }

                        HtmlTableFormat.Append("</tr>");
                    }
                }
                else
                {
                    HtmlTableFormat.Append("<tr><td>" + pair.Key + "</td><td>" + pair.Value + "</td><td colspan=\"5\">Not Generated<td></tr>");
                }
            }
            else
            {
                HtmlTableFormat.Append("<tr><td>" + pair.Key + "</td><td>" + pair.Value + "</td><td colspan=\"5\">Not Generated<td></tr>");
            }
        }

        HtmlTableFormat.Append("</table>");
        rtn_Html = HtmlTableFormat.ToString();
        return rtn_Html;
    }
}
