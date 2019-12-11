using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for First_NounPhrase_Sentences
/// </summary>
public class First_NounPhrase_Sentences
{


    Regex_Pattern Regx_ptn = new Regex_Pattern();
    XML_to_Text class_XML_to_Text = new XML_to_Text();
    public string First_NounPhrase_Sentences_NN(XmlNode NounPhrase)
    {
        string nount_string = NounPhrase.InnerXml;
        MatchCollection PrepPhrase_unwanted = Regex.Matches(nount_string, "<PrepPhrase(.)*?>(.)*?</PrepPhrase(.)*?>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        for (int i=0;i<PrepPhrase_unwanted.Count;i++)
        {
            nount_string = nount_string.Replace(PrepPhrase_unwanted[i].ToString(), "");
        }
        MatchCollection Unmatched_unwanted = Regex.Matches(nount_string, "<Unmatched(.)*?>(.)*?</Unmatched(.)*?>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        for (int i = 0; i < Unmatched_unwanted.Count; i++)
        {
            nount_string = nount_string.Replace(Unmatched_unwanted[i].ToString(), "");
        }
        MatchCollection ActionPhrase_unwanted = Regex.Matches(nount_string, "<ActionPhrase(.)*?>(.)*?</ActionPhrase(.)*?>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        for (int i = 0; i < ActionPhrase_unwanted.Count; i++)
        {
            nount_string = nount_string.Replace(ActionPhrase_unwanted[i].ToString(), "");
        }
        MatchCollection MOLECULE_unwanted = Regex.Matches(nount_string, "<MOLECULE(.)*?>(.)*?</MOLECULE(.)*?>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        for (int i = 0; i < MOLECULE_unwanted.Count; i++)
        {
            nount_string = nount_string.Replace(MOLECULE_unwanted[i].ToString(), "");
        }
        

        string Return_NounPhrase =  "";


        if (nount_string.Contains("<DT"))
        {

            Return_NounPhrase = class_XML_to_Text.Return_XML_to_Text(nount_string);
                 
           
        }
        return Return_NounPhrase;
    }


}