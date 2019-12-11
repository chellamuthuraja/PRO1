using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using uk.ac.cam.ch.wwmm.chemicaltagger;
using nu.xom;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net;

public partial class ChEmNER : System.Web.UI.Page
{
    string strClrQuantity = string.Empty;
    string strClrQuantityUnit = string.Empty;
    public SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ChemTag"].ConnectionString);
    public SqlConnection con_chem = new SqlConnection(WebConfigurationManager.ConnectionStrings["ChemTag"].ConnectionString);
    POSContainer posContainer = new POSContainer();
    ChemistrySentenceParser chemistrySentenceParser;

    string OSCAEREntitys = string.Empty, Quantity_tag = string.Empty, Quantity_Unittag = string.Empty, NN_CHEMENTITYEntitys = string.Empty, TemperatureEntity = string.Empty, TimeEntity = string.Empty;
    string[] strValues;
    DataTable dt = new DataTable();
    List<string[]> rowList = new List<string[]>();
    public int Productcount = 0;
    public int Reactantcount = 0;
    public string input = "";
    public string Quantity = "";
    public string strQuantityUnit = string.Empty;
    string REGNO = "";
    string NRNNUM = "";


    List<string> TotChemList = new List<string>();
    List<string> OthersList = new List<string>();
    //DataTable TotChemList = new DataTable();
    DataSet dsproduct = new DataSet();
    //Helper help = new Helper();

    public string strTanos = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Convert.ToString(Session["UserName"]) != "")
            {
                Label1.Text = Session["UserName"].ToString();
                posContainer = ChemistryPOSTagger.getDefaultInstance().runTaggers("");
                chemistrySentenceParser = new ChemistrySentenceParser(posContainer);
                chemistrySentenceParser.parseTags();
                divsplitter.Visible = false;
            }
            else
            {
                Response.Redirect("Login.aspx");

            }
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

  
    protected void RadButton2_Click(object sender, EventArgs e)
    {

        //if (RadUpload1.UploadedFiles.Count > 0)
        //{
        //    string LinkContent = string.Empty;
        //    string Filename = string.Empty;
        //    foreach (UploadedFile file in RadUpload1.UploadedFiles)
        //    {
        //        Filename = file.GetName().ToString();
        //        file.SaveAs(Server.MapPath("~/Upload/" + file.GetName()));
        //        StreamReader str = new StreamReader(Server.MapPath("~/Upload/" + file.GetName()));
        //        LinkContent += str.ReadToEnd() + "\n";
        //        str.Close();

        //    }

        //    RadTextBox1.Text = LinkContent;
        //    RadTextBox1.Visible = true;
        //}
    }
    XML_to_Text class_XML_to_Text = new XML_to_Text();
    //string OSCAEREntitys = string.Empty, Quantity_tag = string.Empty, Quantity_Unittag = string.Empty, NN_CHEMENTITYEntitys = string.Empty, TemperatureEntity = string.Empty, TimeEntity = string.Empty, strSentence = string.Empty;
    protected void RadButton3_Click(object sender, EventArgs e)
    {
     //   this.RadTextBox1.Size.Height = 100;
        RadTextBox1.Height = 100;
        divsplitter.Visible = true;
        DataTable xsd = new DataTable();
        xsd.Columns.Add("ChemicalName");
        // xsd.Columns.Add("stringContent");
        xsd.Columns.Add("Quantity");
        xsd.Columns.Add("QuantityUnit");
        Session["chemical_dt"] = null;
        Session["chemical_dt"] = (DataTable)xsd;
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("Description", typeof(string));

        //if (RadTextBox1.Text.Replace("Enter some text to analyze", "") == "")
        //{
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Enter some text to analyze');", true);
        //}

       // else
        //{
            string input_text = RadTextBox1.Text;
            //input_text = para_2_sentence.IsSurrounded(input_text);
           
            //input_text = class_XML_to_Text.Return_Replace_Rule(input_text);

            //input_text = para_2_sentence.stripNonValidXMLCharacters(input_text);
           

            // Calling ChemistryPOSTagger
             posContainer = ChemistryPOSTagger.getDefaultInstance().runTaggers(input_text);

            // Returns a string of TAG TOKEN format (e.g.: DT The NN cat VB sat IN on DT the NN matt)
            // Call ChemistrySentenceParser either by passing the POSContainer or by InputStream
             chemistrySentenceParser = new ChemistrySentenceParser(posContainer);

            chemistrySentenceParser.parseTags();

            // Return an XMLDoc
            Document doc = chemistrySentenceParser.makeXMLDocument();
            string xml_to_str = doc.toXML();

            SolventExtract(xml_to_str);
            //Others(xml_to_str);
            //YieldExtract(xml_to_str);

            //Changes
            string ColouredText = string.Empty;
            ColouredText = " " + RadTextBox1.Text;
            string OSCAEREntitys = string.Empty;
            string OSCAER = string.Empty;
            DataTable dt = tblcolumns();

            MatchCollection MoleNE_Entity = Regex.Matches(doc.toXML(), @"(<MOLECULE>|<MOLECULE role=""(?<dummy>.*?)"">)(?<MOLECULE>.*?)</MOLECULE>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection UNNamedMolecule = Regex.Matches(doc.toXML(), @"<UNNAMEDMOLECULE>(?<UNNAMEDMOLECULE>.*?)</UNNAMEDMOLECULE>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //MatchCollection MoleNE_Entity = Regex.Matches(doc.toXML(), @"<MOLECULE>(?<MOLECULE>.*?)</MOLECULE>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (MoleNE_Entity.Count > 0)
            {
                foreach (Match MolematchedItems in MoleNE_Entity)
                {
                    string aa = MolematchedItems.ToString();
                    MatchCollection NE_Entity = Regex.Matches(aa, @"<OSCARCM>(?<OSCARCM>.*?)</OSCARCM>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (NE_Entity.Count > 0)
                    {
                        foreach (Match matchedItems in NE_Entity)
                        {
                            OSCAEREntitys = matchedItems.Groups["OSCARCM"].Value;
                            OSCAEREntitys = OSCAEREntitys.Replace("<DASH>", "<OSCAR-CM>");
                            OSCAEREntitys = OSCAEREntitys.Replace("</DASH>", "</OSCAR-CM>");
                            MatchCollection OSCAEREM = Regex.Matches(OSCAEREntitys, @"<OSCAR-CM>(?<OSCAR>.*?)</OSCAR-CM>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            if (OSCAEREM.Count == 1)
                            {
                                foreach (Match matchedItems1 in OSCAEREM)
                                {
                                    OSCAER = matchedItems1.Groups["OSCAR"].Value;
                                    DataRow dr = dt.NewRow();
                                    dr["NAME"] = OSCAER;
                                    Quantity_Value_(aa, ColouredText, OSCAER, xsd);
                                    dt.Rows.Add(dr);

                                    string Replaceword = " " + OSCAER + " ";
                                    string Replaceword1 = " " + OSCAER + "";
                                    string Replaceword2 = "" + OSCAER + " ";
                                    string Replaceword3 = "" + OSCAER + "";

                                    string[] quantity = strClrQuantity.TrimEnd('~').Split('~');
                                    string[] quantityUnit = strClrQuantityUnit.TrimEnd('~').Split('~');
                                    int length = quantity.Length;

                                    for (int i = 0; i < length; i++)
                                    {
                                        string text = quantity[i] + " " + quantityUnit[i];
                                        string text1 = quantity[i] + quantityUnit[i];
                                        string append = string.Empty;
                                        int index = ColouredText.IndexOf(text);
                                        string matchText = text;

                                        matchText = " " + matchText;



                                        if (text.Trim() != string.Empty && ColouredText.Contains(matchText))
                                        {
                                            append = matchText;
                                            text = text.Replace(quantity[i], "<span style='color:red'><b>" + quantity[i] + "</b></span>");
                                            text = text.Replace(quantityUnit[i], "<span style='color:green'><b>" + quantityUnit[i] + "</b></span>");
                                            ColouredText = ColouredText.Replace(append, " " + text);
                                        }
                                        else if (text1.Trim() != string.Empty && ColouredText.Contains(" " + text1))
                                        {
                                            append = text1;
                                            text1 = text1.Replace(quantity[i], "<span style='color:red'><b>" + quantity[i] + "</b></span>");
                                            text1 = text1.Replace(quantityUnit[i], "<span style='color:green'><b>" + quantityUnit[i] + "</b></span>");
                                            ColouredText = ColouredText.Replace(append, " " + text1);
                                        }
                                    }
                                    quantity = null;
                                    quantityUnit = null;

                                    if (ColouredText.Contains(Replaceword))
                                    {
                                        string color_HTMLview = "<span style='color:DeepPink'><b>" + Replaceword + "</b></span>";
                                        //ColouredText = Regex.Replace(ColouredText, Replaceword, color_HTMLview, RegexOptions.IgnoreCase);
                                        ColouredText = ColouredText.Replace(Replaceword, color_HTMLview);
                                    }
                                    else if (ColouredText.Contains(Replaceword1))
                                    {
                                        string color_HTMLview = "<span style='color:DeepPink'><b>" + Replaceword1 + "</b></span>";
                                        ColouredText = ColouredText.Replace(Replaceword1, color_HTMLview);
                                    }
                                    else if (ColouredText.Contains(Replaceword2))
                                    {
                                        string color_HTMLview = "<span style='color:DeepPink'><b>" + Replaceword2 + "</b></span>";
                                        ColouredText = ColouredText.Replace(Replaceword2, color_HTMLview);
                                    }
                                    else if (ColouredText.Contains(Replaceword3))
                                    {
                                        string color_HTMLview = "<span style='color:DeepPink'><b>" + Replaceword3 + "</b></span>";
                                        ColouredText = ColouredText.Replace(Replaceword3, color_HTMLview);
                                    }

                                }
                            }

                            if (OSCAEREM.Count > 1)
                            {
                                OSCAER = string.Empty;
                                foreach (Match matchedItems1 in OSCAEREM)
                                {
                                    OSCAER += matchedItems1.Groups["OSCAR"].Value + " ";
                                }

                                OSCAER = OSCAER.Replace(" / ", "/");
                                DataRow dr = dt.NewRow();
                                dr["NAME"] = OSCAER.Trim();

                                Quantity_Value_(aa, ColouredText, OSCAER, xsd);

                                dt.Rows.Add(dr);

                                string Replaceword = " " + OSCAER.Trim() + " ";
                                string Replaceword1 = " " + OSCAER.Trim() + "";
                                string Replaceword2 = "" + OSCAER.Trim() + " ";
                                string Replaceword3 = "" + OSCAER.Trim() + "";

                                string[] quantity = strClrQuantity.TrimEnd('~').Split('~');
                                string[] quantityUnit = strClrQuantityUnit.TrimEnd('~').Split('~');
                                int length = quantity.Length;
                                for (int i = 0; i < length; i++)
                                {
                                    string text = quantity[i] + " " + quantityUnit[i];
                                    string text1 = quantity[i] + quantityUnit[i];
                                    string append = string.Empty;
                                    int index = ColouredText.IndexOf(text);
                                    string matchText = text;

                                    matchText = " " + matchText;



                                    if (text.Trim() != string.Empty && ColouredText.Contains(matchText))
                                    {
                                        append = matchText;
                                        text = text.Replace(quantity[i], "<span style='color:red'><b>" + quantity[i] + "</b></span>");
                                        text = text.Replace(quantityUnit[i], "<span style='color:green'><b>" + quantityUnit[i] + "</b></span>");
                                        ColouredText = ColouredText.Replace(append, " " + text);
                                    }
                                    else if (text1.Trim() != string.Empty && ColouredText.Contains(" " + text1))
                                    {
                                        append = text1;
                                        text1 = text1.Replace(quantity[i], "<span style='color:red'><b>" + quantity[i] + "</b></span>");
                                        text1 = text1.Replace(quantityUnit[i], "<span style='color:green'><b>" + quantityUnit[i] + "</b></span>");
                                        ColouredText = ColouredText.Replace(append, " " + text1);
                                    }
                                }
                                quantity = null;
                                quantityUnit = null;
                                if (ColouredText.Contains(Replaceword))
                                {
                                    string color_HTMLview = "<span style='color:DeepPink'><b>" + Replaceword + "</b></span>";
                                    //ColouredText = Regex.Replace(ColouredText, Replaceword, color_HTMLview, RegexOptions.IgnoreCase);
                                    ColouredText = ColouredText.Replace(Replaceword, color_HTMLview);
                                }
                                else if (ColouredText.Contains(Replaceword1))
                                {
                                    string color_HTMLview = "<span style='color:DeepPink'><b>" + Replaceword1 + "</b></span>";
                                    ColouredText = ColouredText.Replace(Replaceword1, color_HTMLview);
                                }
                                else if (ColouredText.Contains(Replaceword2))
                                {
                                    string color_HTMLview = "<span style='color:DeepPink'><b>" + Replaceword2 + "</b></span>";
                                    ColouredText = ColouredText.Replace(Replaceword2, color_HTMLview);
                                }
                                else if (ColouredText.Contains(Replaceword3))
                                {
                                    string color_HTMLview = "<span style='color:DeepPink'><b>" + Replaceword3 + "</b></span>";
                                    ColouredText = ColouredText.Replace(Replaceword3, color_HTMLview);
                                }
                            }
                        }
                    }

                }
            }

            Label12.Text = "<br/>" + ColouredText;
            ////RadTextBox1.Visible = false;
            Label12.Visible = true;
            //RemoveDuplicateRows(dt, "NAME");

            //if (dt.Rows.Count > 0)
            //{
            //    RadGrid2.DataSource = Session["chemical_dt"];
            //    //RadGrid2.DataSource = dt;
            //    RadGrid2.DataBind();
            //    RadGrid2.Visible = true;
            //}

            string temp=para_2_sentence.get_para_to_sentence(doc);

            Label2.Text = "<br/>" + temp.Replace("\n", "<br/><br/>");
        //}
            fnBindGrid();

    }
    para_to_sentence para_2_sentence = new para_to_sentence();
    public void Quantity_Value_(string Mole, string CDescriptor, string chemfinal, DataTable xsd)
    {
        strClrQuantity = string.Empty;
        strClrQuantityUnit = string.Empty;
        string strQuantity = string.Empty, strQuantityUnit = string.Empty, strPressure = string.Empty, strPressureUnit = string.Empty, strTemperature = string.Empty, strTemperatureUnit = string.Empty, strDuration = string.Empty, strDurationUnit = string.Empty;
        MatchCollection NE_QUANTITY = Regex.Matches(Mole, @"<QUANTITY>(?<QUANTITY>.*?)</QUANTITY>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        if (NE_QUANTITY.Count > 0)
        {
            Quantity_tag = string.Empty;
            foreach (Match matchedItems8 in NE_QUANTITY)
            {
                Quantity_tag = matchedItems8.Groups["QUANTITY"].Value;
                MatchCollection NE_MASS_VOLUME = Regex.Matches(Quantity_tag, @"(<VOLUME>|<AMOUNT>|<PERCENT>|<MOLAR>|<PH>|<MASS>)(?<QUANTITY_VAL>.*?)(</VOLUME>|</AMOUNT>|</PERCENT>|</MOLAR>|</PH>|</MASS>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection MASS_VOLUME_VAL_Unit1 = Regex.Matches(Quantity_tag, @"(<NN-MASS>|<NN-AMOUNT>|<NN-VOL>|<NN-PERCENT>|<NN-MOLAR>|<NN-PH>)(?<VALUE_Unit>.*?)(</NN-MASS>|</NN-AMOUNT>|</NN-VOL>|</NN-PERCENT>|</NN-MOLAR>|</NN-PH>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                foreach (Match NE_MASS_VOL in NE_MASS_VOLUME)
                {
                    strQuantity = string.Empty;
                    strQuantityUnit = string.Empty;
                    Quantity_tag = NE_MASS_VOL.Groups["QUANTITY_VAL"].Value;

                    Match MASS_VOLUME_VAL = Regex.Match(Quantity_tag, @"<CD>(?<VALUE>.*?)</CD>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    strQuantity = Regex.Replace(MASS_VOLUME_VAL.Value.Trim(), "<.*?>", String.Empty);
                    strClrQuantity += strQuantity + "~";
                    Match MASS_VOLUME_VAL_Unit = Regex.Match(Quantity_tag, @"(<NN-MASS>|<NN-AMOUNT>|<NN-VOL>|<NN-PERCENT>|<NN-MOLAR>|<NN-PH>)(?<VALUE_Unit>.*?)(</NN-MASS>|</NN-AMOUNT>|</NN-VOL>|</NN-PERCENT>|</NN-MOLAR>|</NN-PH>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    strQuantityUnit = Regex.Replace(MASS_VOLUME_VAL_Unit.Value.Trim(), "<.*?>", String.Empty);
                    strClrQuantityUnit += strQuantityUnit + "~";
                    chemical_splitdata(CDescriptor, chemfinal, strQuantity.Trim(), strQuantityUnit.Trim(), strPressure.Trim(), strPressureUnit.Trim(), strTemperature.Trim(), strTemperatureUnit.Trim(), strDuration.Trim(), strDurationUnit.Trim(), xsd);
                }
            }
        }
        else
        {
            chemical_splitdata(CDescriptor, chemfinal, strQuantity.Trim(), strQuantityUnit.Trim(), strPressure.Trim(), strPressureUnit.Trim(), strTemperature.Trim(), strTemperatureUnit.Trim(), strDuration.Trim(), strDurationUnit.Trim(), xsd);
        }
    }
    void chemical_splitdata(string CDescriptor, string chename, string strQuantity, string strQuantityUnit, string strPressure, string strPressureUnit, string strTemperature, string strTemperatureUnit, string strDuration, string strDurationUnit, DataTable xsd)
    {
        xsd.Rows.Add();
        int maxi = xsd.Rows.Count - 1;
        xsd.Rows[maxi]["ChemicalName"] = chename.Trim();
        // xsd.Rows[maxi]["stringContent"] = CDescriptor;
        xsd.Rows[maxi]["Quantity"] = strQuantity;
        xsd.Rows[maxi]["QuantityUnit"] = strQuantityUnit;
        Session["chemical_dt"] = (DataTable)xsd;
    }

    private DataTable tblcolumns()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("NAME", typeof(string));
        return dt;
    }
    public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
    {
        System.Collections.Hashtable hTable = new System.Collections.Hashtable();
        ArrayList duplicateList = new ArrayList();
        foreach (DataRow dtRow in dTable.Rows)
        {
            if (hTable.Contains(dtRow[colName]))
                duplicateList.Add(dtRow);
            else
                hTable.Add(dtRow[colName], string.Empty);
        }
        foreach (DataRow dtRow in duplicateList)
            dTable.Rows.Remove(dtRow);
        return dTable;

    }

    public int GetNthIndex(string s, char t, int n)
    {
        int count = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == t)
            {
                count++;
                if (count == n)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    public void SolventExtract(string xml_to_str)
    {
        //string strsolvant = txtExtract.Text;
        //POSContainer posContainer = ChemistryPOSTagger.getDefaultInstance().runTaggers(strsolvant);

        //ChemistrySentenceParser chemistrySentenceParser = new ChemistrySentenceParser(posContainer);

        //chemistrySentenceParser.parseTags();
        //nu.xom.Document doc = chemistrySentenceParser.makeXMLDocument();
        //string xml_to_str = doc.toXML();

        if (con.State != ConnectionState.Open)
            con.Open();
        SqlCommand cmd1 = new SqlCommand("truncate table tbl_ChemTag_Chemtagger_Opsin", con);
        cmd1.ExecuteNonQuery();


        MatchCollection MOLECULE = Regex.Matches(xml_to_str, @"(<MOLECULE>|<MOLECULE role=""(?<dummy>.*?)"">)(?<MOLECULE>.*?)</MOLECULE>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        if (MOLECULE.Count > 0)
        {

            foreach (Match matchedItems4 in MOLECULE)
            {
                OSCAEREntitys = string.Empty;
                string MOLECULEs = matchedItems4.Groups[0].Value;
                MatchCollection NE_OSCARCM = Regex.Matches(MOLECULEs, @"<OSCARCM>(?:(?:(?!</OSCARCM>).)+)</OSCARCM>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection NE_QUANTITY = Regex.Matches(MOLECULEs, @"<QUANTITY>(?:(?:(?!</QUANTITY>).)+)</QUANTITY>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                //MatchCollection NE_QUANTITY = Regex.Matches(Quantity_tag, @"(<VOLUME>|<AMOUNT>|<PERCENT>|<MOLAR>|<PH>|<MASS>)(?<QUANTITY_VAL>.*?)(</VOLUME>|</AMOUNT>|</PERCENT>|</MOLAR>|</PH>|</MASS>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                string s = string.Empty;
                foreach (Match matchedItems5 in NE_OSCARCM)
                {
                    string strQuantity = string.Empty;
                    string squantity = string.Empty;
                    OSCAEREntitys = matchedItems5.Value;
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "<OSCARCM>", " ");
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "</OSCARCM>", " ");
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "<OSCAR-CM>", " ");
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "</OSCAR-CM>", " ");
                    s = s + Regex.Replace(OSCAEREntitys, "<.*?>", String.Empty) + " ";
                    s = s + ',';
                    s = s.TrimStart(' ');
                    s = s.TrimEnd(',');
                    strValues = s.Split(new string[] { "," }, StringSplitOptions.None);
                    if (NE_QUANTITY.Count > 0)
                    {
                        foreach (Match matchedItems6 in NE_QUANTITY)
                        {
                            strQuantity = matchedItems6.Value;
                            MatchCollection NE_MASS_VOLUME = Regex.Matches(strQuantity, @"(<VOLUME>|<AMOUNT>|<PERCENT>|<MOLAR>|<PH>|<MASS>)(?<QUANTITY_VAL>.*?)(</VOLUME>|</AMOUNT>|</PERCENT>|</MOLAR>|</PH>|</MASS>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            //strQuantity = Regex.Replace(strQuantity, "<QUANTITY>", " ");
                            //strQuantity = Regex.Replace(strQuantity, "</QUANTITY>", " ");
                            //strQuantity = Regex.Replace(strQuantity, "<VOLUME>", " ");
                            //strQuantity = Regex.Replace(strQuantity, "</VOLUME>", " ");
                            //strQuantity = Regex.Replace(strQuantity, "<CD>", " ");
                            //strQuantity = Regex.Replace(strQuantity, "</CD>", " ");
                            //strQuantity = Regex.Replace(strQuantity, "<NN-VOL>", " ");
                            //strQuantity = Regex.Replace(strQuantity, "</NN-VOL>", " ");
                            foreach (Match NE_MASS_VOL in NE_MASS_VOLUME)
                            {
                                strQuantity = string.Empty;
                                strQuantityUnit = string.Empty;
                                Quantity_tag = NE_MASS_VOL.Groups["QUANTITY_VAL"].Value;

                                Match MASS_VOLUME_VAL = Regex.Match(Quantity_tag, @"<CD>(?<VALUE>.*?)</CD>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                                strQuantity = Regex.Replace(MASS_VOLUME_VAL.Value.Trim(), "<.*?>", String.Empty);
                                strClrQuantity += strQuantity + "~";
                                Match MASS_VOLUME_VAL_Unit = Regex.Match(Quantity_tag, @"(<NN-MASS>|<NN-AMOUNT>|<NN-VOL>|<NN-PERCENT>|<NN-MOLAR>|<NN-PH>)(?<VALUE_Unit>.*?)(</NN-MASS>|</NN-AMOUNT>|</NN-VOL>|</NN-PERCENT>|</NN-MOLAR>|</NN-PH>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                                strQuantityUnit = Regex.Replace(MASS_VOLUME_VAL_Unit.Value.Trim(), "<.*?>", String.Empty);
                                strClrQuantityUnit += strQuantityUnit + "~";
                                //chemical_splitdata(CDescriptor, chemfinal, strQuantity.Trim(), strQuantityUnit.Trim(), strPressure.Trim(), strPressureUnit.Trim(), strTemperature.Trim(), strTemperatureUnit.Trim(), strDuration.Trim(), strDurationUnit.Trim(), xsd);
                                //squantity = squantity + Regex.Replace(strQuantity, "<.*?>", String.Empty) + " ";
                                Insert("S", s, strQuantity, strQuantityUnit);
                            }
                            

                        }
                    }
                    else
                    {
                        strQuantityUnit = string.Empty;
                        Insert("S", s, strQuantity, strQuantityUnit);
                    }
                    //foreach (var values in strValues.Distinct().ToArray())
                    //{
                    //    if (values != "")
                    //    {
                    //        //TotChemList.Rows.Add(new object[] { values });
                    //        TotChemList.Add(values.TrimEnd().TrimStart());
                    //        string[] arr = null;
                    //        string Chemtage = "";
                    //        if (values.Contains("|"))
                    //        {
                    //            arr = values.Split('|').ToArray();
                    //            foreach (string value in arr)
                    //            {
                    //                Chemtage = fnChemtag(value);

                    //                if (Chemtage != "")
                    //                {
                    //                    if (Chemtage != "")
                    //                    {
                    //                        int ind = GetNthIndex(Chemtage, '/', 2);
                    //                        int index = GetNthIndex(Chemtage, '/', 1);

                    //                        int indexs = (ind - index) - 1;
                    //                        Chemtage = Chemtage.Substring(index + 1, indexs);
                    //                        //Chemtage = Chemtage.Remove(Chemtage.Length - 1);
                    //                    }
                    //                }
                    //                Insert("S", s, Chemtage, squantity);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            Chemtage = fnChemtag(values);
                    //            if (Chemtage != "")
                    //            {
                    //                int ind = GetNthIndex(Chemtage, '/', 2);
                    //                int index = GetNthIndex(Chemtage, '/', 1);

                    //                int indexs = (ind - index) - 1;
                    //                Chemtage = Chemtage.Substring(index + 1, indexs);
                    //                //Chemtage = Chemtage.Remove(Chemtage.Length - 1);
                    //            }
                    //            Insert("S", s, Chemtage, squantity);
                    //        }

                    //    }
                    //}
                }
            }
        }
    }


    public void YieldExtract(string xml_to_str)
    {
        string strsolvant = RadTextBox1.Text; 
        //txtExtract.Text;

        //POSContainer posContainer = ChemistryPOSTagger.getDefaultInstance().runTaggers(strsolvant);

        //ChemistrySentenceParser chemistrySentenceParser = new ChemistrySentenceParser(posContainer);

        //chemistrySentenceParser.parseTags();
        //nu.xom.Document doc = chemistrySentenceParser.makeXMLDocument();
        //string xml_to_str = doc.toXML();


        MatchCollection MOLECULE = Regex.Matches(xml_to_str, @"(<ActionPhrase type=""Yield"">(?:(?:(?!</ActionPhrase>).)+)</ActionPhrase>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        if (MOLECULE.Count > 0)
        {
            //NounPhrase


            foreach (Match matchedItems4 in MOLECULE)
            {
                string s = string.Empty;
                string strQuantity = string.Empty;
                string squantity = string.Empty;
                OSCAEREntitys = matchedItems4.Value;
                MatchCollection NE_QUANTITY = Regex.Matches(OSCAEREntitys, @"<QUANTITY>(?:(?:(?!</QUANTITY>).)+)</QUANTITY>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                s = s + Regex.Replace(OSCAEREntitys, "<.*?>", " ") + "";


                foreach (Match matchedItems6 in NE_QUANTITY)
                {
                    strQuantity = matchedItems6.Value;
                    strQuantity = Regex.Replace(strQuantity, "<QUANTITY>", " ");
                    strQuantity = Regex.Replace(strQuantity, "</QUANTITY>", " ");
                    strQuantity = Regex.Replace(strQuantity, "<VOLUME>", " ");
                    strQuantity = Regex.Replace(strQuantity, "</VOLUME>", " ");
                    strQuantity = Regex.Replace(strQuantity, "<CD>", " ");
                    strQuantity = Regex.Replace(strQuantity, "</CD>", " ");
                    strQuantity = Regex.Replace(strQuantity, "<NN-VOL>", " ");
                    strQuantity = Regex.Replace(strQuantity, "</NN-VOL>", " ");
                    squantity = squantity + Regex.Replace(strQuantity, "<.*?>", String.Empty) + " ";
                }
                strValues = s.Split(new string[] { "," }, StringSplitOptions.None);

                if (s != "")
                {
                    string Chemtage = "";
                    Insert("Yield", s, Chemtage, squantity);


                }


            }


        }
    }

    public void Others(string xml_to_str)
    {

        //string strsolvant = txtExtract.Text;
        //POSContainer posContainer = ChemistryPOSTagger.getDefaultInstance().runTaggers(strsolvant);

        //ChemistrySentenceParser chemistrySentenceParser = new ChemistrySentenceParser(posContainer);

        //chemistrySentenceParser.parseTags();
        //nu.xom.Document doc = chemistrySentenceParser.makeXMLDocument();
        //string xml_to_str = doc.toXML();

        //MatchCollection MOLECULE = Regex.Matches(xml_to_str, @"(<MOLECULE>(((?!<MOLECULE role=""Solvent""[^>]*?>)(?![^<]*?</MOLECULE>).)+)</MOLECULE>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        MatchCollection MOLECULE = Regex.Matches(xml_to_str, @"(<MOLECULE>(?:(?:(?!</MOLECULE>).)+)</MOLECULE>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        MatchCollection UNNAMEDMOLECULE = Regex.Matches(xml_to_str, @"(<UNNAMEDMOLECULE>(?:(?:(?!</UNNAMEDMOLECULE>).)+)</UNNAMEDMOLECULE>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        if (MOLECULE.Count > 0)
        {

            foreach (Match matchedItems4 in MOLECULE)
            {
                OSCAEREntitys = string.Empty;
                string MOLECULEs = matchedItems4.Groups[0].Value;
                MatchCollection NE_OSCARCM = Regex.Matches(MOLECULEs, @"<OSCARCM>(?:(?:(?!</OSCARCM>).)+)</OSCARCM>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection NE_CHEMENTITY = Regex.Matches(MOLECULEs, @"<NN-CHEMENTITY>(?:(?:(?!</NN-CHEMENTITY>).)+)</NN-CHEMENTITY>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection NE_REFERENCETOCOMPOUND = Regex.Matches(MOLECULEs, @"<REFERENCETOCOMPOUND>(?:(?:(?!</REFERENCETOCOMPOUND>).)+)</REFERENCETOCOMPOUND>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection NE_QUANTITY = Regex.Matches(MOLECULEs, @"<QUANTITY>(?:(?:(?!</QUANTITY>).)+)</QUANTITY>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                foreach (Match matchedItems5 in NE_OSCARCM)
                {
                    string s = string.Empty;
                    string chemword = "";
                    string strQuantity = string.Empty;
                    string squantity = string.Empty;
                    OSCAEREntitys = matchedItems5.Value;
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "<OSCARCM>", " ");
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "</OSCARCM>", " ");
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "<OSCAR-CM>", " ");
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "</OSCAR-CM>", " ");
                    s = s + Regex.Replace(OSCAEREntitys, "<.*?>", String.Empty) + " ";
                    s = s + ',';
                    s = s.TrimStart(' ');
                    s = s.TrimEnd(',');


                    string t = string.Empty;
                    foreach (Match matchedItems in NE_CHEMENTITY)
                    {

                        OSCAEREntitys = matchedItems.Value;
                        OSCAEREntitys = Regex.Replace(OSCAEREntitys, "<NN-CHEMENTITY>", " ");
                        OSCAEREntitys = Regex.Replace(OSCAEREntitys, "</NN-CHEMENTITY>", " ");
                        OSCAEREntitys = Regex.Replace(OSCAEREntitys, "<NN-CHEMENTITY>", " ");
                        OSCAEREntitys = Regex.Replace(OSCAEREntitys, "</NN-CHEMENTITY>", " ");
                        t = t + Regex.Replace(OSCAEREntitys, "<.*?>", String.Empty) + " ";
                        t = t + ',';
                        t = t.TrimStart(' ');
                        s += t.TrimEnd(',');

                    }

                    string r = string.Empty;
                    foreach (Match matchedItemsr in NE_REFERENCETOCOMPOUND)
                    {

                        OSCAEREntitys = matchedItemsr.Value;
                        OSCAEREntitys = Regex.Replace(OSCAEREntitys, "<REFERENCETOCOMPOUND>", " ");
                        OSCAEREntitys = Regex.Replace(OSCAEREntitys, "</REFERENCETOCOMPOUND>", " ");
                        OSCAEREntitys = Regex.Replace(OSCAEREntitys, "<REFERENCETOCOMPOUND>", " ");
                        OSCAEREntitys = Regex.Replace(OSCAEREntitys, "</REFERENCETOCOMPOUND>", " ");
                        r = r + Regex.Replace(OSCAEREntitys, "<.*?>", String.Empty) + " ";
                        r = r + ',';
                        r = r.TrimStart(' ');
                        s += r.TrimEnd(',');

                    }

                    strValues = s.Split(new string[] { "," }, StringSplitOptions.None);
                    foreach (Match matchedItems6 in NE_QUANTITY)
                    {
                        strQuantity = matchedItems6.Value;
                        //strQuantity = Regex.Replace(strQuantity, "<QUANTITY>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "</QUANTITY>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "<VOLUME>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "</VOLUME>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "<CD>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "</CD>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "<NN-VOL>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "</NN-VOL>", " ");
                        //squantity = squantity + Regex.Replace(strQuantity, "<.*?>", String.Empty) + " ";
                    }

                    TotChemList = TotChemList.ConvertAll(d => d.ToLower());

                    foreach (var values in strValues.Distinct().ToArray())
                    {




                        string val = values.TrimStart().TrimEnd();

                        val = val.Replace("   ", " ");
                        val = val.Replace("  ", " ");


                        if (val == "Iron")
                        {
                            string udfhg = "dfgdfg";
                        }
                        OthersList.Add(val.TrimEnd().TrimStart());

                        if (val != "")
                        {

                            string[] arr = null;
                            string Chemtage = "";
                            if (values.Contains("|"))
                            {
                                arr = values.Split('|').ToArray();
                                foreach (string value in arr)
                                {

                                    string[] stringArray = TotChemList.Distinct().ToArray();
                                    int pos = Array.IndexOf(stringArray, val.TrimEnd().TrimStart().ToLower());
                                    if (pos <= -1)
                                    {
                                        Chemtage = fnChemtag(value);
                                        if (Chemtage != "")
                                        {
                                            int ind = GetNthIndex(Chemtage, '/', 2);
                                            int index = GetNthIndex(Chemtage, '/', 1);

                                            int indexs = (ind - index) - 1;
                                            Chemtage = Chemtage.Substring(index + 1, indexs);
                                            //Chemtage = Chemtage.Remove(Chemtage.Length - 1);
                                        }
                                        Insert("O", s, Chemtage, squantity);
                                        //insertdt.Rows.Add(new object[] { "", "", "", "", "", item });
                                    }



                                }
                            }
                            else
                            {

                                string[] stringArray = TotChemList.Distinct().ToArray();
                                int pos = Array.IndexOf(stringArray, val.TrimEnd().TrimStart().ToLower());
                                if (pos <= -1)
                                {
                                    Chemtage = fnChemtag(s);
                                    Chemtage = Chemtage.TrimEnd().TrimStart();

                                    if (Chemtage != "")
                                    {
                                        if (Chemtage.Count(x => x == '/') > 2)
                                        {
                                            int ind = GetNthIndex(Chemtage, '/', 2);
                                            int index = GetNthIndex(Chemtage, '/', 1);

                                            int indexs = (ind - index) - 1;
                                            Chemtage = Chemtage.Substring(index + 1, indexs);
                                        }
                                        else if (Chemtage.Count(x => x == '/') >= 1)
                                        {
                                            int ind = GetNthIndex(Chemtage, '/', 1);
                                            int index = GetNthIndex(Chemtage, '/', 1);

                                            int indexs = (Chemtage.Length - ind) - 1;
                                            Chemtage = Chemtage.Substring(index + 1, indexs);
                                            string[] strArr = Chemtage.Split('/');
                                            Chemtage = strArr[0].ToString();


                                        }

                                    }
                                    Insert("O", s, Chemtage, squantity);

                                }
                            }

                        }
                    }
                }
            }
        }

        if (UNNAMEDMOLECULE.Count > 0)
        {

            foreach (Match matchedItems4 in UNNAMEDMOLECULE)
            {
                OSCAEREntitys = string.Empty;
                string UNNAMEDMOLECULEs = matchedItems4.Groups[0].Value;
                MatchCollection NE_OSCARCM = Regex.Matches(UNNAMEDMOLECULEs, @"<REFERENCETOCOMPOUND>(?:(?:(?!</REFERENCETOCOMPOUND>).)+)</REFERENCETOCOMPOUND>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                //MatchCollection NE_CHEMENTITY = Regex.Matches(MOLECULEs, @"<NN-CHEMENTITY>(?:(?:(?!</NN-CHEMENTITY>).)+)</NN-CHEMENTITY>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                //MatchCollection NE_REFERENCETOCOMPOUND = Regex.Matches(MOLECULEs, @"<REFERENCETOCOMPOUND>(?:(?:(?!</REFERENCETOCOMPOUND>).)+)</REFERENCETOCOMPOUND>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                //MatchCollection NE_QUANTITY = Regex.Matches(UNNAMEDMOLECULEs, @"<QUANTITY>(?:(?:(?!</QUANTITY>).)+)</QUANTITY>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection NE_QUANTITY = Regex.Matches(Quantity_tag, @"(<VOLUME>|<AMOUNT>|<PERCENT>|<MOLAR>|<PH>|<MASS>)(?<QUANTITY_VAL>.*?)(</VOLUME>|</AMOUNT>|</PERCENT>|</MOLAR>|</PH>|</MASS>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                foreach (Match matchedItems5 in NE_OSCARCM)
                {
                    string s = string.Empty;

                    string strQuantity = string.Empty;
                    string squantity = string.Empty;
                    OSCAEREntitys = matchedItems5.Value;
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "<REFERENCETOCOMPOUND>", " ");
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "</REFERENCETOCOMPOUND>", " ");
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "<CD>", " ");
                    OSCAEREntitys = Regex.Replace(OSCAEREntitys, "</CD>", " ");
                    s = s + Regex.Replace(OSCAEREntitys, "<.*?>", String.Empty) + " ";
                    s = s + ',';
                    s = s.TrimStart(' ');
                    s = s.TrimEnd(',');


                    string t = string.Empty;


                    strValues = s.Split(new string[] { "," }, StringSplitOptions.None);
                    foreach (Match matchedItems6 in NE_QUANTITY)
                    {
                        strQuantity = matchedItems6.Value;
                        //strQuantity = Regex.Replace(strQuantity, "<QUANTITY>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "</QUANTITY>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "<VOLUME>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "</VOLUME>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "<CD>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "</CD>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "<NN-VOL>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "</NN-VOL>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "<NN-VOL>", " ");
                        //strQuantity = Regex.Replace(strQuantity, "</NN-VOL>", " ");
                        Match MASS_VOLUME_VAL = Regex.Match(strQuantity, @"<CD>(?<VALUE>.*?)</CD>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        strQuantity = Regex.Replace(MASS_VOLUME_VAL.Value.Trim(), "<.*?>", String.Empty);
                        strClrQuantity += strQuantity + "~";
                        Match MASS_VOLUME_VAL_Unit = Regex.Match(Quantity_tag, @"(<NN-MASS>|<NN-AMOUNT>|<NN-VOL>|<NN-PERCENT>|<NN-MOLAR>|<NN-PH>)(?<VALUE_Unit>.*?)(</NN-MASS>|</NN-AMOUNT>|</NN-VOL>|</NN-PERCENT>|</NN-MOLAR>|</NN-PH>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        //strQuantityUnit = Regex.Replace(MASS_VOLUME_VAL_Unit.Value.Trim(), "<.*?>", String.Empty);
                        squantity = squantity + Regex.Replace(strQuantity, "<.*?>", String.Empty) + " ";
                    }

                    TotChemList = TotChemList.ConvertAll(d => d.ToLower());

                    foreach (var values in strValues.Distinct().ToArray())
                    {




                        string val = values.TrimStart().TrimEnd();

                        val = val.Replace("   ", " ");
                        val = val.Replace("  ", " ");



                        OthersList.Add(val.TrimEnd().TrimStart());

                        if (val != "")
                        {

                            string[] arr = null;
                            string Chemtage = "";
                            if (values.Contains("|"))
                            {
                                arr = values.Split('|').ToArray();
                                foreach (string value in arr)
                                {

                                    string[] stringArray = TotChemList.Distinct().ToArray();
                                    int pos = Array.IndexOf(stringArray, val.TrimEnd().TrimStart().ToLower());
                                    if (pos <= -1)
                                    {
                                        Chemtage = fnChemtag(value);
                                        if (Chemtage != "")
                                        {
                                            int ind = GetNthIndex(Chemtage, '/', 2);
                                            int index = GetNthIndex(Chemtage, '/', 1);

                                            int indexs = (ind - index) - 1;
                                            Chemtage = Chemtage.Substring(index + 1, indexs);
                                            //Chemtage = Chemtage.Remove(Chemtage.Length - 1);
                                        }
                                        Insert("O", s, Chemtage, squantity);
                                        //insertdt.Rows.Add(new object[] { "", "", "", "", "", item });
                                    }



                                }
                            }
                            else
                            {

                                string[] stringArray = TotChemList.Distinct().ToArray();
                                int pos = Array.IndexOf(stringArray, val.TrimEnd().TrimStart().ToLower());
                                if (pos <= -1)
                                {
                                    Chemtage = fnChemtag(s);
                                    Chemtage = Chemtage.TrimEnd().TrimStart();

                                    if (Chemtage != "")
                                    {
                                        if (Chemtage.Count(x => x == '/') > 2)
                                        {
                                            int ind = GetNthIndex(Chemtage, '/', 2);
                                            int index = GetNthIndex(Chemtage, '/', 1);

                                            int indexs = (ind - index) - 1;
                                            Chemtage = Chemtage.Substring(index + 1, indexs);
                                        }
                                        else if (Chemtage.Count(x => x == '/') >= 1)
                                        {
                                            int ind = GetNthIndex(Chemtage, '/', 1);
                                            int index = GetNthIndex(Chemtage, '/', 1);

                                            int indexs = (Chemtage.Length - ind) - 1;
                                            Chemtage = Chemtage.Substring(index + 1, indexs);
                                        }

                                    }
                                    Insert("O", s, Chemtage, squantity);

                                }
                            }

                        }
                    }
                }
            }
        }
    }

    public void Insert(string Rolecode, string svalues, string Quantity, string QuantityUnit)
        //public void Insert(string Rolecode, string svalues, string Opsintag, string Quantity)
    {

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        svalues = svalues.Replace("   ", " ");
        svalues = svalues.Replace("  ", " ");
        svalues.TrimEnd().TrimStart();

        REGNO = "";
        NRNNUM = "";


        //if (con_chem.State == ConnectionState.Closed)
        //{
        //    con_chem.Open();
        //}

        //SqlCommand CMDS = new SqlCommand("DBO_CRAFT_ChekRegAndNreg", con_chem);
        //CMDS.Parameters.AddWithValue("@Opsintag", Opsintag);
        ////CMDS.Parameters.AddWithValue("@strTanNos", hdnTanNo.Value);
        //CMDS.Parameters.AddWithValue("@strName", svalues);
        //CMDS.CommandType = CommandType.StoredProcedure;
        //DataTable dtno = new DataTable();
        //SqlDataReader dr;
        //dr = CMDS.ExecuteReader();

        //if (dr.Read())
        //{
        //    REGNO = "";
        //    NRNNUM = "";
        //    REGNO = Convert.ToString(dr[0]);
        //    NRNNUM = Convert.ToString(dr[1]);
        //}


        //dr.Close();

        //if (con_chem.State == ConnectionState.Open)
        //{
        //    con_chem.Close();
        //}




        #region "Un Comment"
        /*
        if (Opsintag != string.Empty)
        {
            if (con_craft.State == ConnectionState.Closed)
            {
                con_craft.Open();
            }
            SqlCommand CMDS = new SqlCommand("SELECT iNRNreg,iNRNNum FROM [tbl_Craft_IndReferenceDoc] WHERE strMl_formula='" + Opsintag.TrimEnd().TrimStart() + "' And strTANno='" + hdnTanNo.Value + "'", con_craft);


            DataTable dtno = new DataTable();

            SqlDataReader dr;
            dr = CMDS.ExecuteReader();


            if (dr.Read())
            {
                REGNO = "";
                NRNNUM = "";
                REGNO = Convert.ToString(dr[0]);
                NRNNUM = Convert.ToString(dr[1]);
            }


            dr.Close();

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                REGNO = "";
                NRNNUM = "";


                if (con_craft.State == ConnectionState.Open)
                {
                    con_craft.Close();
                }

                SqlCommand CMDS1 = new SqlCommand("Select  strNUM_9000,strREG_NUM From TBL_CRAFT_ORGREF where strName='" + svalues.TrimEnd().TrimStart() + "'", con_craft);
                DataTable dtno1 = new DataTable();
                SqlDataReader dr1;
                dr1 = CMDS1.ExecuteReader();
                if (dr1.Read())
                {

                    REGNO = "";
                    NRNNUM = "";
                    REGNO = Convert.ToString(dr1[0]);
                    NRNNUM = Convert.ToString(dr1[1]);

                }
                else
                {
                    REGNO = "";
                    NRNNUM = "";

                }
                dr1.Close();
            }    
        }

        else if (svalues != "" && Opsintag == string.Empty)
        {

            if (con_craft.State == ConnectionState.Closed)
            {
                con_craft.Open();
            }

            SqlCommand CMDS1 = new SqlCommand("Select  strNUM_9000,strREG_NUM From TBL_CRAFT_ORGREF where strName='" + svalues.TrimEnd().TrimStart() + "'", con_craft);
            DataTable dtno1 = new DataTable();
            SqlDataReader dr1;
            dr1 = CMDS1.ExecuteReader();
            if (dr1.Read())
            {

                REGNO = "";
                NRNNUM = "";
                REGNO = Convert.ToString(dr1[0]);
                NRNNUM = Convert.ToString(dr1[1]);

            }
            else
            {
                REGNO = "";
                NRNNUM = "";

            }
            dr1.Close();


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        */
        #endregion



        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd = new SqlCommand("DBP_ChemTag_Chemtagger_Opsin", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@indication", 1);
        cmd.Parameters.AddWithValue("@strExtraction", svalues);
        cmd.Parameters.AddWithValue("@strQuantity", Quantity);
        cmd.Parameters.AddWithValue("@strRoleCode", Rolecode);
        cmd.Parameters.AddWithValue("@strstage", "1");
        //cmd.Parameters.AddWithValue("@strOpsinSource", Opsintag);
        cmd.Parameters.AddWithValue("@strOpsinSGML", QuantityUnit);
        //cmd.Parameters.AddWithValue("@strREGNUM", Convert.ToString(REGNO));
        //cmd.Parameters.AddWithValue("@strNRNNUM", Convert.ToString(NRNNUM));
        cmd.Parameters.AddWithValue("@iStatus", "0");
        cmd.Parameters.AddWithValue("@strCreatedBy", "Admin");
        cmd.Parameters.AddWithValue("@dtCreatedDate", DateTime.Now);
        cmd.Parameters.AddWithValue("@strLastModifiedBy", "Admin");
        cmd.Parameters.AddWithValue("@strLastModifiedDate", DateTime.Now);
        cmd.ExecuteNonQuery();

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }


    }

    public string fnChemtag(string svalues)
    {


        svalues = svalues.TrimEnd().TrimStart();

        //-------------*******************
        string html = string.Empty;
        WebRequest requests = WebRequest.Create("http://opsin.ch.cam.ac.uk/opsin/" + svalues + ".stdinchi");
        //if (requests.ContentLength == -1)
        //{
        //    html = "";
        //}
        //else
        //{

        try
        {
            using (WebResponse responses = requests.GetResponse())
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://opsin.ch.cam.ac.uk/opsin/" + svalues + ".stdinchi");
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

            }
        }
        catch (WebException e)
        {
            using (WebResponse responses = e.Response)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)responses;
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                using (Stream data = responses.GetResponseStream())
                using (var reader = new StreamReader(data))
                {
                    html = "";
                    //string text = reader.ReadToEnd();
                    //Console.WriteLine(text);
                }
            }
        }
        // }
        return html;


    }

    public void fnBindGrid()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("DBP_ChemTag_Chemtagger_Opsin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@indication", 2);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                RadGrid2.DataSource = dt;
                    RadGrid2.DataBind();
                    RadGrid2.Visible = true;
                //gvExtract.DataSource = dt;
                //gvExtract.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }


}