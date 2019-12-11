using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for PrepPhrase_Sentences
/// </summary>
public class PrepPhrase_Sentences
{
    Regex_Pattern Regx_ptn = new Regex_Pattern();
    XML_to_Text class_XML_to_Text = new XML_to_Text();
    string[] Return = { "", "" };
    public string []PrepPhrase_Sentences_Class(XmlNode PrepPhrase)
    {
        string return_End_String = "";
        string return_PrepPhrase = "";

        string PrepPhrase_input = PrepPhrase.InnerXml;
        string NounPhrase_Pattern = Regx_ptn.NounPhrase_Pattern();
        string APPARATUS_Pattern = Regx_ptn.APPARATUS_Pattern();
        string NN_CHEMENTITY_Pattern = Regx_ptn.NN_CHEMENTITY_Pattern();
        string DT_Pattern = Regx_ptn.DT_Pattern();

        MatchCollection NounPhrase = Regex.Matches(PrepPhrase_input, NounPhrase_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
        MatchCollection PrepPhrase_match = Regex.Matches(PrepPhrase_input, NounPhrase_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);

        string PrepPhrase_of_Noun = "";

        if (NounPhrase.Count > 0)
        {
            PrepPhrase_of_Noun = NounPhrase[0].ToString();
            PrepPhrase_input = PrepPhrase_input.Replace(NounPhrase[0].ToString(), "");
        }
        if (PrepPhrase_input.Contains("<TO>To</TO>") || PrepPhrase_input.Contains("<IN>At</IN>"))
        {
            return_End_String = "to";
            MatchCollection APPARATUS = Regex.Matches(PrepPhrase_of_Noun, APPARATUS_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection DT = Regex.Matches(PrepPhrase_of_Noun, DT_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection NN_CHEMENTITY = Regex.Matches(PrepPhrase_of_Noun, NN_CHEMENTITY_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            if (APPARATUS.Count > 0)
            {
                PrepPhrase_of_Noun = PrepPhrase_of_Noun.Replace(APPARATUS[0].ToString(), "");
                return_End_String = return_End_String + " " + class_XML_to_Text.Return_XML_to_Text(APPARATUS[0].ToString());
            }
            else
            {
                if (DT.Count > 0)
                {
                    PrepPhrase_of_Noun = PrepPhrase_of_Noun.Replace(DT[0].ToString(), "");
                    return_End_String = return_End_String + " " + class_XML_to_Text.Return_XML_to_Text(DT[0].ToString());
                }
                if (NN_CHEMENTITY.Count > 0)
                {
                    PrepPhrase_of_Noun = PrepPhrase_of_Noun.Replace(NN_CHEMENTITY[0].ToString(), "");
                    return_End_String = return_End_String + " " + class_XML_to_Text.Return_XML_to_Text(NN_CHEMENTITY[0].ToString());
                }
            }

            return_PrepPhrase = class_XML_to_Text.Return_XML_to_Text(PrepPhrase_of_Noun);
            return_End_String = class_XML_to_Text.Return_XML_to_Text(return_End_String);
        }
        else
        {
            return_PrepPhrase = class_XML_to_Text.Return_XML_to_Text(PrepPhrase.InnerXml);
        }
        //else if (PrepPhrase_of_Noun == "")
        //{
        //    return_PrepPhrase = class_XML_to_Text.Return_XML_to_Text(PrepPhrase_input);
        //}
        //else
        //{
        //    return_PrepPhrase = class_XML_to_Text.Return_XML_to_Text(PrepPhrase_of_Noun);
        //}
        Return[0] = return_PrepPhrase;
        Return[1] = return_End_String.Replace("this","the");
        return Return;
    }
  
}