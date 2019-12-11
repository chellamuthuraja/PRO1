using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for NounPhrase_Sentences
/// </summary>
public class NounPhrase_Sentences
{

    Regex_Pattern Regx_ptn = new Regex_Pattern();
    XML_to_Text class_XML_to_Text = new XML_to_Text();
    public string []NounPhrase_Sentences_Class(XmlNode NounPhrase)
	{
        MatchCollection NounPhrase_unwanted = Regex.Matches(NounPhrase.InnerXml, "<FW>\"</FW>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        string []Return_NounPhrase = {"",""};


        if (NounPhrase_unwanted.Count == 0)
        {
            string MOLECULE_Pattern = Regx_ptn.MOLECULE_Pattern();

            MatchCollection NounPhrase_MOLECULE = Regex.Matches(NounPhrase.InnerXml, MOLECULE_Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            if (NounPhrase_MOLECULE.Count > 0)
            {
                Return_NounPhrase[0] = class_XML_to_Text.Return_XML_to_Text(NounPhrase.InnerXml);
                Return_NounPhrase[1] = "1";
            }
            else
            {

                Return_NounPhrase[0] = class_XML_to_Text.Return_XML_to_Text(NounPhrase.InnerXml);
                Return_NounPhrase[1] = "0";

            }
        }
        return Return_NounPhrase;
	}

  
}