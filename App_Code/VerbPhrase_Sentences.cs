using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for VerbPhrase_Sentences
/// </summary>
public class VerbPhrase_Sentences
{
    Regex_Pattern Regx_ptn = new Regex_Pattern();
    XML_to_Text class_XML_to_Text = new XML_to_Text();


    public string[] VerbPhrase_Sentences_Class(string VerbPhrase_InnerXml)
    {
        string VerbPhrase_input = VerbPhrase_InnerXml;
        string[] Return_Verb = { "", "" };
        string VB_String = "";
        string VerbPhrase_String = "";

        MatchCollection verb_to_Pattern_match = Regex.Matches(VerbPhrase_input, "<IN-WITH>with</IN-WITH>(.*?)<VB(.*?)>(.*ing)</VB(.*?)>(.*?)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        if (verb_to_Pattern_match.Count == 0)
        {
            string VB_Pattern = Regx_ptn.VB_Pattern();
            string VBN_Pattern = Regx_ptn.VBN_Pattern();
            string VBG_Pattern = Regx_ptn.VBG_Pattern();
            string VBD_Pattern = Regx_ptn.VBD_Pattern();
            string VBD_Pattern_left = Regx_ptn.VBD_Pattern_kept();
            string VBP_Pattern = Regx_ptn.VBP_Pattern();
            string OSCAR_RN_Pattern = Regx_ptn.OSCAR_RN_Pattern();


            MatchCollection VB = Regex.Matches(VerbPhrase_input, VB_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection VBN = Regex.Matches(VerbPhrase_input, VBN_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection VBG = Regex.Matches(VerbPhrase_input, VBG_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection VBD = Regex.Matches(VerbPhrase_input, VBD_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection VBD_kept = Regex.Matches(VerbPhrase_input, VBD_Pattern_left, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection VBP = Regex.Matches(VerbPhrase_input, VBP_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection OSCAR_RN = Regex.Matches(VerbPhrase_input, OSCAR_RN_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);



            if (VB.Count > 0)
            {
                VB_String = class_XML_to_Text.Return_XML_to_Text(VB[0].ToString());
                VerbPhrase_input = Regex.Replace(VerbPhrase_input, VB[0].ToString(), "");
            }
            else if (VBN.Count > 0 && (VB_String == "" || VB_String.Substring(VB_String.Length - 3, 2) != "ed"))
            {
                VB_String = class_XML_to_Text.Return_XML_to_Text(VBN[0].ToString());
                VerbPhrase_input = Regex.Replace(VerbPhrase_input, VBN[0].ToString(), "");
            }
            else if (VBG.Count > 0 && (VB_String == "" || VB_String.Substring(VB_String.Length - 3, 2) != "ed"))
            {
                VB_String = class_XML_to_Text.Return_XML_to_Text(VBG[0].ToString());
                VerbPhrase_input = Regex.Replace(VerbPhrase_input, VBG[0].ToString(), "");
            }
            else if (VBD.Count > 0 && (VB_String == "" || VB_String.Substring(VB_String.Length - 3, 2) != "ed"))
            {
                VB_String = class_XML_to_Text.Return_XML_to_Text(VBD[0].ToString());
                VerbPhrase_input = Regex.Replace(VerbPhrase_input, VBD[0].ToString(), "");
            }
            else if (VBD_kept.Count > 0 && (VB_String == "" || VB_String.Substring(VB_String.Length - 3, 2) != "ed"))
            {
                VB_String = class_XML_to_Text.Return_XML_to_Text(VBD_kept[0].ToString());
                VerbPhrase_input = Regex.Replace(VerbPhrase_input, VBD_kept[0].ToString(), "");
            }
            else if (VBP.Count > 0 && (VB_String == "" || VB_String.Substring(VB_String.Length - 3, 2) != "ed"))
            {
                VB_String = class_XML_to_Text.Return_XML_to_Text(VBP[0].ToString());
                VerbPhrase_input = Regex.Replace(VerbPhrase_input, VBP[0].ToString(), "");
            }
            else if (OSCAR_RN.Count > 0 && (VB_String == "" || VB_String.Substring(VB_String.Length - 3, 2) != "ed"))
            {
                VB_String = class_XML_to_Text.Return_XML_to_Text(OSCAR_RN.ToString());
                VerbPhrase_input = Regex.Replace(VerbPhrase_input, OSCAR_RN[0].ToString(), "");
            }
            if (VB_String != "")
            {
                // VerbPhrase_input = Regex.Replace(VerbPhrase_input, "<VB(.)*?>(.)*?</VB(.)*?>", "");
                string[] remove_vb = new string[] { "am", "is", "are", "was", "were", "has", "have", "had", "been", "be", "will", "shall", "would", "should", "can", "could", "may", "might" };
                foreach (string remove in remove_vb)
                {
                    VerbPhrase_input = Regex.Replace(VerbPhrase_input, ("<VB(.)*?>" + remove + "</VB(.)*?>"), "");
                    // VerbPhrase_input = VerbPhrase_input.Replace(remove, "");
                }
            }
        }

        VerbPhrase_String = class_XML_to_Text.Return_XML_to_Text(VerbPhrase_input);
        Return_Verb[0] = VB_String;
        Return_Verb[1] = VerbPhrase_String;
        return Return_Verb;
    }
    
}