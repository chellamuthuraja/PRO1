using LemmaSharp;
using nu.xom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using uk.ac.cam.ch.wwmm.chemicaltagger;

/// <summary>
/// Summary description for para_to_sentence
/// </summary>
public class para_to_sentence
{
    public string get_para_to_sentence(Document para)
	{
        //try
        //{
        string Text_sentences = ChemTagger(para);
       
        while (Text_sentences.Contains("\n\n"))
        {
            Text_sentences = Regex.Replace(Text_sentences,"\n\n", "\n");
        }

        return Text_sentences;

        //}
        //catch (Exception e)
        //{
        //    throw;

        //}
	}

    public  string stripNonValidXMLCharacters(string s)
    {
        StringBuilder _validXML = new StringBuilder(s.Length, s.Length); // Used to hold the output.
        char current; // Used to reference the current character.
        char[] charArray = s.ToCharArray();

        if (string.IsNullOrEmpty(s)) return string.Empty; // vacancy test.

        for (int i = 0; i < charArray.Length; i++)
        {
            current = charArray[i]; // NOTE: No IndexOutOfBoundsException caught here; it should not happen.
            if ((current == 0x9) ||
            (current == 0xA) ||
            (current == 0xD) ||
            ((current >= 0x20) && (current <= 0xD7FF)) ||
            ((current >= 0xE000) && (current <= 0xFFFD)) ||
            ((current >= 0x10000) && (current <= 0x10FFFF)))
                _validXML.Append(current);
        }

        return _validXML.ToString();
    }
    enum NodeTypes
    {
        HasChildren,
        IsNode,
        IsAttribute
    }

    XML_to_Text class_XML_to_Text = new XML_to_Text();
    Regex_Pattern Regx_ptn = new Regex_Pattern();
    int sentence_count = 0;
    public string ChemTagger(Document input_text)
    {
        //input_text = IsSurrounded(input_text);
        string Return_String = "";
        //input_text = class_XML_to_Text.Return_Replace_Rule(input_text);

        //input_text = stripNonValidXMLCharacters(input_text);
        //posContainer = ChemistryPOSTagger.getDefaultInstance().runTaggers(input_text);
        //chemistrySentenceParser = new ChemistrySentenceParser(posContainer);
        //chemistrySentenceParser.parseTags();
        Document doc = input_text;


        string xml_to_str = doc.toXML();
        string encodedXml = String.Format("<pre>{0}</pre>", HttpUtility.HtmlEncode(xml_to_str));

       
        xml_to_str = xml_to_str.Replace("<?xml version=\"1.0\"?>", "");
        //xml_to_str = Regex.Replace(xml_to_str, "<Document>", "");
        //xml_to_str = Regex.Replace(xml_to_str, "</Document>", "");
       

        PrepPhrase_Sentences PrepPhrase_class = new PrepPhrase_Sentences();
        NounPhrase_Sentences NounPhrase_class = new NounPhrase_Sentences();
        VerbPhrase_Sentences VerbPhrase_class = new VerbPhrase_Sentences();
        First_NounPhrase_Sentences First_NounPhrase_class = new First_NounPhrase_Sentences();
        string input_1 = xml_to_str.ToString();


        XmlDocument doc_root_level = new XmlDocument();
        doc_root_level.LoadXml(input_1);
        sentence_count = 0;
        foreach (XmlNode docnode_level in doc_root_level.ChildNodes)
        {
            foreach (XmlNode node_Sentence in docnode_level.ChildNodes)
            {
                if (node_Sentence.Name == "Sentence")
                {
                    sentence_count = sentence_count + 1;
                    input_1 = modify_ActionPhrase(node_Sentence.OuterXml);

                    XmlDocument doc_root = new XmlDocument();
                    doc_root.LoadXml(input_1);

                    int verb_no = 0;

                    string NounPhrase_first = "";
                    foreach (XmlNode docnode in doc_root.ChildNodes)
                    {
                        if (docnode.InnerXml.Contains("VerbPhrase"))
                        {
                            int verb_node = 0;
                            string PrepPhrase_first = ""; string[] PrepPhrase = { "", "" }; string NounPhrase = ""; string NounPhrase_molecule = ""; string[] NounPhrase_1 = { "", "" }; string[] VerbPhrase = { "", "" }; string TimePhrase = ""; string AtmospherePhrase = "";
                            string verb_comman = "";
                            string verb_first = "";
                            string previous_node = "";

                            foreach (XmlNode node in docnode.ChildNodes)
                            {

                                    if (node.NextSibling != null && node.InnerText == "and" && Regex.Matches(node.NextSibling.OuterXml, "<Unmatched><VerbPhrase>").Count == 0 && (VerbPhrase[0] != "" || VerbPhrase[1] != ""))
                                    {
                                        string n_1 = NounPhrase_first;
                                        if (NounPhrase.Contains(NounPhrase_first) || NounPhrase.Length > 2)
                                        {
                                            n_1 = "";
                                        }
                                    
                                        string out_val = PrepPhrase_first + " " + LemmatizeOne(VerbPhrase[0], PrepPhrase_first) + " " + verb_first + " " + n_1 + " " + NounPhrase_molecule + " " + NounPhrase + " " + PrepPhrase[0] + " " + PrepPhrase[1] + " " + VerbPhrase[1] + " " + TimePhrase + " " + AtmospherePhrase;
                                        Return_String = Return_String + "\n" + class_XML_to_Text.Return_Replace(out_val);

                                        NounPhrase_first = NounPhrase_first.Replace("which", "");
                                        previous_node = ""; verb_first = ""; PrepPhrase_first = ""; PrepPhrase[0] = ""; PrepPhrase[1] = ""; NounPhrase = ""; NounPhrase_molecule = ""; NounPhrase_1[0] = ""; NounPhrase_1[0] = ""; VerbPhrase[0] = ""; VerbPhrase[1] = ""; TimePhrase = ""; AtmospherePhrase = "";

                                    }

                                    // status = 0;
                                    if (node.Name == "ActionPhrase")
                                    {
                                        foreach (XmlNode ActionPhrase_node in node)
                                        {
                                            string verb_remove = Regex.Replace(node.OuterXml, "<Unmatched><VerbPhrase>(.*?)</VerbPhrase></Unmatched>", "");

                                            if (!verb_remove.Contains("VerbPhrase") && VerbPhrase[0] != "")
                                            {
                                                 
                                                switch (previous_node)
                                                {
                                                    case "PrepPhrase":
                                                        PrepPhrase[0] = PrepPhrase[0] + " " + class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml); ;
                                                        break;
                                                    case "NounPhrase":
                                                        NounPhrase = NounPhrase + " " + class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml); ;
                                                        break;
                                                    case "VerbPhrase":
                                                        VerbPhrase[1] = VerbPhrase[1] + " " + class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml); ;
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                switch (ActionPhrase_node.Name)
                                                {
                                                    case "PrepPhrase":

                                                        PrepPhrase = PrepPhrase_class.PrepPhrase_Sentences_Class(ActionPhrase_node);
                                                        previous_node = "PrepPhrase";
                                                        break;
                                                    case "NounPhrase":
                                                        NounPhrase_1 = NounPhrase_class.NounPhrase_Sentences_Class(ActionPhrase_node);
                                                        if (NounPhrase_molecule == "" && NounPhrase_1[1] == "1")
                                                        {
                                                            NounPhrase_molecule = NounPhrase_molecule + " " + NounPhrase_1[0];
                                                        }
                                                        else
                                                        {
                                                            NounPhrase = NounPhrase + " " + NounPhrase_1[0];
                                                        }


                                                        string F_NN = First_NounPhrase_class.First_NounPhrase_Sentences_NN(ActionPhrase_node);
                                                        if (F_NN != "" && NounPhrase_first == "")
                                                        {
                                                            NounPhrase_first = F_NN;
                                                            NounPhrase_molecule = NounPhrase_molecule.Replace(F_NN, "");

                                                        }

                                                        previous_node = "NounPhrase";
                                                        break;
                                                    case "VerbPhrase":

                                                        string[] VerbPhrase_temp = { "", "" };
                                                        VerbPhrase_temp = VerbPhrase_class.VerbPhrase_Sentences_Class(ActionPhrase_node.InnerXml + verb_comman);
                                                        if (VerbPhrase[0] == "")
                                                        {
                                                            VerbPhrase[0] = VerbPhrase_temp[0].ToString();
                                                        }

                                                        VerbPhrase[1] = VerbPhrase[1] + " " + VerbPhrase_temp[1].ToString();
                                                        if (verb_comman != "")
                                                            verb_comman = "";
                                                        previous_node = "VerbPhrase";
                                                        break;
                                                    case "Unmatched":
                                                        if (PrepPhrase_first == "" && ActionPhrase_node.ChildNodes[0] != null && ActionPhrase_node.ChildNodes[1] != null && ActionPhrase_node.ChildNodes[1].Name == "COMMA" && Regex.Matches(ActionPhrase_node.InnerXml, "<VerbPhrase>(.*?)<VB(.*?)>(.*ed)</VB(.*?)>(.*?)</VerbPhrase>").Count == 0)
                                                        {
                                                            PrepPhrase_first = class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml);
                                                        }

                                                        else
                                                        {
                                                            switch (previous_node)
                                                            {
                                                                case "PrepPhrase":
                                                                    PrepPhrase[0] = PrepPhrase[0] + " " + class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml); ;
                                                                    break;
                                                                case "NounPhrase":
                                                                    NounPhrase = NounPhrase + " " + class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml); ;
                                                                    break;
                                                                case "VerbPhrase":
                                                                    VerbPhrase[1] = VerbPhrase[1] + " " + class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml);
                                                                    break;
                                                                default:
                                                                    verb_first = verb_first + " " + class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml);
                                                                    break;
                                                            }

                                                        }

                                                        break;
                                                    default:
                                                        if (PrepPhrase_first == "" && node.ChildNodes[0] != null && node.ChildNodes[0].InnerText.Contains("after") && node.ChildNodes.Count == 1 && node.ChildNodes[0].Name == "TimePhrase" && Regex.Matches(node.InnerXml, "<VerbPhrase>(.*?)<VB(.*?)>(.*ed)</VB(.*?)>(.*?)</VerbPhrase>").Count == 0)
                                                        {
                                                            PrepPhrase_first = class_XML_to_Text.Return_XML_to_Text(node.InnerXml);
                                                        }
                                                        switch (previous_node)
                                                        {
                                                            case "PrepPhrase":
                                                                PrepPhrase[0] = PrepPhrase[0] + " " + class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml); ;
                                                                break;
                                                            case "NounPhrase":
                                                                NounPhrase = NounPhrase + " " + class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml); ;
                                                                break;
                                                            case "VerbPhrase":
                                                                VerbPhrase[1] = VerbPhrase[1] + " " + class_XML_to_Text.Return_XML_to_Text(ActionPhrase_node.InnerXml); ;
                                                                break;
                                                        }
                                                        break;
                                                }
                                            }
                                           
                                            
                                        }
                                    }
                                    else if (VerbPhrase[0] != "")
                                    {
                                        switch (previous_node)
                                        {
                                            case "PrepPhrase":
                                                PrepPhrase[0] = PrepPhrase[0] + " " + class_XML_to_Text.Return_XML_to_Text(node.InnerXml); ;
                                                break;
                                            case "NounPhrase":
                                                NounPhrase = NounPhrase + " " + class_XML_to_Text.Return_XML_to_Text(node.InnerXml); ;
                                                break;
                                            case "VerbPhrase":
                                                VerbPhrase[1] = VerbPhrase[1] + " " + class_XML_to_Text.Return_XML_to_Text(node.InnerXml); ;
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        switch (node.Name)
                                        {
                                            case "PrepPhrase":

                                                PrepPhrase = PrepPhrase_class.PrepPhrase_Sentences_Class(node);
                                                previous_node = "PrepPhrase";
                                                break;
                                            case "NounPhrase":
                                                NounPhrase_1 = NounPhrase_class.NounPhrase_Sentences_Class(node);
                                                if (NounPhrase_molecule == "" && NounPhrase_1[1] == "1")
                                                {
                                                    NounPhrase_molecule = NounPhrase_molecule + " " + NounPhrase_1[0];
                                                }
                                                else
                                                {
                                                    NounPhrase = NounPhrase + " " + NounPhrase_1[0];
                                                }


                                                string F_NN = First_NounPhrase_class.First_NounPhrase_Sentences_NN(node);
                                                if (F_NN != "" && NounPhrase_first == "")
                                                {
                                                    NounPhrase_first = F_NN;
                                                    NounPhrase_molecule = NounPhrase_molecule.Replace(F_NN, "");

                                                }

                                                previous_node = "NounPhrase";
                                                break;
                                            case "VerbPhrase":

                                                string[] VerbPhrase_temp = { "", "" };
                                                VerbPhrase_temp = VerbPhrase_class.VerbPhrase_Sentences_Class(node.InnerXml + verb_comman);
                                                if (VerbPhrase[0] == "")
                                                {
                                                    VerbPhrase[0] = VerbPhrase_temp[0].ToString();
                                                }

                                                VerbPhrase[1] = VerbPhrase[1] + " " + VerbPhrase_temp[1].ToString();
                                                if (verb_comman != "")
                                                    verb_comman = "";
                                                previous_node = "VerbPhrase";
                                                break;
                                            case "Unmatched":


                                                if (PrepPhrase_first == "" && node.ChildNodes[0] != null && node.ChildNodes[1] != null && node.ChildNodes[1].Name == "COMMA" && Regex.Matches(node.InnerXml, "<VerbPhrase>(.*?)<VB(.*?)>(.*ed)</VB(.*?)>(.*?)</VerbPhrase>").Count == 0)
                                                {
                                                    PrepPhrase_first = class_XML_to_Text.Return_XML_to_Text(node.InnerXml);
                                                }
                                                else
                                                {
                                                    switch (previous_node)
                                                    {
                                                        case "PrepPhrase":
                                                            PrepPhrase[0] = PrepPhrase[0] + " " + class_XML_to_Text.Return_XML_to_Text(node.InnerXml); ;
                                                            break;
                                                        case "NounPhrase":
                                                            NounPhrase = NounPhrase + " " + class_XML_to_Text.Return_XML_to_Text(node.InnerXml); ;
                                                            break;
                                                        case "VerbPhrase":
                                                            VerbPhrase[1] = VerbPhrase[1] + " " + class_XML_to_Text.Return_XML_to_Text(node.InnerXml); ;
                                                            break;
                                                    }

                                                }

                                                break;
                                            default:
                                                switch (previous_node)
                                                {
                                                    case "PrepPhrase":
                                                        PrepPhrase[0] = PrepPhrase[0] + " " + class_XML_to_Text.Return_XML_to_Text(node.InnerXml); ;
                                                        break;
                                                    case "NounPhrase":
                                                        NounPhrase = NounPhrase + " " + class_XML_to_Text.Return_XML_to_Text(node.InnerXml); ;
                                                        break;
                                                    case "VerbPhrase":
                                                        VerbPhrase[1] = VerbPhrase[1] + " " + class_XML_to_Text.Return_XML_to_Text(node.InnerXml); ;
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                              

                            }
                            if (VerbPhrase[0] != "" || VerbPhrase[1] != "")
                            {
                                string n_1 = NounPhrase_first;
                                if (NounPhrase.Contains(NounPhrase_first) || NounPhrase.Length > 2)
                                {
                                    n_1 = "";
                                }
                                string out_val = PrepPhrase_first + " " + LemmatizeOne(VerbPhrase[0], PrepPhrase_first) + " " + verb_first + " " + n_1 + " " + NounPhrase_molecule + " " + NounPhrase + " " + PrepPhrase[0] + " " + PrepPhrase[1] + " " + VerbPhrase[1] + " " + TimePhrase + " " + AtmospherePhrase;
                                Return_String = Return_String + "\n" + class_XML_to_Text.Return_Replace(out_val);
                            }
                           
                        }
                        else
                        {
                            Return_String = Return_String + "\n" + class_XML_to_Text.Return_Replace(class_XML_to_Text.Return_XML_to_Text(docnode.InnerXml));
                        }
                    }
                }
            }
        }

        string[] Return_String_out = Regex.Split(Return_String, "\n");
        int inc = 1;
        string Return_String_1 = "";
        foreach (string s in Return_String_out)
        {
            string ss = s.Trim();
           

            //MatchCollection After_match = Regex.Matches(ss, "(After)(.*?)(,)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //if (After_match.Count > 0)
            //{
            //    ss = ss.Replace(After_match[0].ToString(), "");
            //    ss = After_match[0].ToString()+" "+ss;
            //}
            if (ss.Length > 10)
            {
                ss = s.Substring(0, 1).ToUpper() + s.Substring(1); 
                Return_String_1 = Return_String_1 + ss.Replace(". ", " ").Trim() + ".\n";

                inc = inc + 1;
            }
        }

        Return_String_1 = class_XML_to_Text.Return_mistake_Replace(Return_String_1);

        return Return_String_1.Replace(" .",".");

    }

    ILemmatizer lmtz = new LemmatizerPrebuiltCompact(LemmaSharp.LanguagePrebuilt.English);
    private string LemmatizeOne(string word, string status)
    {
        string []wordLower = word.ToLower().Trim().Split(' ');
        string wordLower_str = "";
        foreach (string words in wordLower)
        {
            string lemma = lmtz.Lemmatize(words);
            if (status.Length < 2)
                wordLower_str = wordLower_str + " " + (lemma.ToLower());
            else
                wordLower_str = wordLower_str + " " + (lemma.ToLower());
        }
        return wordLower_str;
    }


    public string modify_ActionPhrase(string xml_to_str)
    {
        string input_1 = xml_to_str.ToString();
        if (sentence_count == 1 && input_1.Contains("<ActionPhrase type=\"Synthesize\">"))
        {
           input_1= Regex.Replace(input_1, "<ActionPhrase type=\"Synthesize\">(.*?)</ActionPhrase>", "");
        }
        
            //input_1 = input_1.Replace("<COMMA>,</COMMA>", "<ActionPhrase type=\"Scope\"></ActionPhrase>");
            if(Regex.Matches(input_1,"<VBD>was</VBD><VB-CHARGE>charged</VB-CHARGE>").Count==0)
            {
            input_1 = input_1.Replace("<VerbPhrase><VB-CHANGE>changed</VB-CHANGE></VerbPhrase>", "<Unmatched><VB-CHANGE>changed</VB-CHANGE></Unmatched>");
            }

            input_1 = Regex.Replace(input_1, "<Unmatched><COMMA>,</COMMA></Unmatched>", "<COMMA>,</COMMA>");
            

            input_1 = Regex.Replace(input_1, "</VB-COOL><RB>down</RB>", " down </VB-COOL>");
            input_1 = Regex.Replace(input_1, "</VBN><RP>out</RP>", " out</VBN>");


            MatchCollection multiple_verb_matches = Regex.Matches(input_1, "<ActionPhrase(.*?)><VerbPhrase><VBD>(.*?)</VBD><VB(.*?)>.*?ed</VB(.*?)></VerbPhrase></ActionPhrase><COMMA>,</COMMA><ActionPhrase(.*?)><VerbPhrase><VB(.*?)>.*?ed</VB(.*?)></VerbPhrase></ActionPhrase><COMMA>,</COMMA><CC>and</CC>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match total_match in multiple_verb_matches)
            {
                MatchCollection multi_verb_matches_11 = Regex.Matches(total_match.ToString(), "</VerbPhrase></ActionPhrase><COMMA>,</COMMA><ActionPhrase(.*?)><VerbPhrase>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                input_1 = input_1.Replace(multi_verb_matches_11[0].ToString(), multi_verb_matches_11[0].ToString().Replace("<COMMA>,</COMMA>", "<CC>and</CC>"));
            }
            multiple_verb_matches = Regex.Matches(input_1, "<VerbPhrase><VB(.*?)>.*?ed</VB(.*?)><PrepPhrase><IN>at</IN>(.*?)</VerbPhrase></ActionPhrase><ActionPhrase(.*?)><VerbPhrase><VB(.*?)>(was|were|are|is)</VB(.*?)><VB(.*?)>.*?ed</VB(.*?)></VerbPhrase>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match total_match in multiple_verb_matches)
            {
                MatchCollection multi_verb_matches_11 = Regex.Matches(total_match.ToString(), "<VerbPhrase><VB(.*?)>.*?ed</VB(.*?)><PrepPhrase><IN>at</IN>(.*?)</VerbPhrase>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                input_1 = input_1.Replace(multi_verb_matches_11[0].ToString(), "<Unmatched>" + multi_verb_matches_11[0].ToString() + "</Unmatched>");
            }


            MatchCollection yield_matches = Regex.Matches(input_1, "(<ActionPhrase type=\"Yield\">)(.*?)(</ActionPhrase>)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match total_match in yield_matches)
            {
                MatchCollection yield_matches_1 = Regex.Matches(total_match.ToString(), "(?<=<ActionPhrase type=\"Yield\">)(.*?)(?=</ActionPhrase>)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                input_1 = input_1.Replace(total_match.ToString(), yield_matches_1[0].ToString());
            }

            MatchCollection total_verb_matches = Regex.Matches(input_1, "<VerbPhrase>(.*?)</VerbPhrase>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match total_match in total_verb_matches)
            {
                MatchCollection to_verb_matches = Regex.Matches(total_match.ToString(), "<VerbPhrase><TO>to</TO><VB(.*?)>(.*?)</VerbPhrase>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                foreach (Match to_verb_matches_1 in to_verb_matches)
                {
                    input_1 = input_1.Replace(to_verb_matches_1.ToString(), "<Unmatched>" + to_verb_matches_1.ToString() + "</Unmatched>");
                }
                MatchCollection ing_verb_matches = Regex.Matches(total_match.ToString(), "<VerbPhrase><IN-WITH>with</IN-WITH>(.*?)<VB(.*?)>(.*ing)</VB(.*?)>(.*?)</VerbPhrase>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                foreach (Match ing_verb_matches_1 in ing_verb_matches)
                {
                    input_1 = input_1.Replace(ing_verb_matches_1.ToString(), "<Unmatched>" + ing_verb_matches_1.ToString() + "</Unmatched>");
                }
                ing_verb_matches = Regex.Matches(total_match.ToString(), "<VerbPhrase><IN-BY>by</IN-BY><VB(.*?)>(.*ing)</VB(.*?)></VerbPhrase>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                foreach (Match ing_verb_matches_1 in ing_verb_matches)
                {
                    input_1 = input_1.Replace(ing_verb_matches_1.ToString(), "<Unmatched>" + ing_verb_matches_1.ToString() + "</Unmatched>");
                }
                ing_verb_matches = Regex.Matches(total_match.ToString(), "<VerbPhrase><VB(.*?)>(.*ing)</VB(.*?)></VerbPhrase>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                foreach (Match ing_verb_matches_1 in ing_verb_matches)
                {
                    input_1 = input_1.Replace(ing_verb_matches_1.ToString(), "<Unmatched>" + ing_verb_matches_1.ToString() + "</Unmatched>");
                }
                ing_verb_matches = Regex.Matches(total_match.ToString(), "<VerbPhrase><IN-UNDER>under</IN-UNDER><VB(.*?)>(.*ing)</VB(.*?)></VerbPhrase>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                foreach (Match ing_verb_matches_1 in ing_verb_matches)
                {
                    input_1 = input_1.Replace(ing_verb_matches_1.ToString(), "<Unmatched>" + ing_verb_matches_1.ToString() + "</Unmatched>");
                }

                MatchCollection multi_verb_matches = Regex.Matches(total_match.ToString(), "<VerbPhrase><VB(.*?)>(.*ed)</VB(.*?)>(.*?)<PrepPhrase><IN-WITH>with</IN-WITH>(.*?)</VerbPhrase>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                if (multi_verb_matches.Count == 1 && Regex.Matches(total_match.ToString(), "<VerbPhrase><VB(.*?)>(was|were|are|is)</VB(.*?)><VB(.*?)>(.*ed)</VB(.*?)>").Count == 0)
                {
                    input_1 = input_1.Replace(multi_verb_matches[0].ToString(), "<Unmatched>" + multi_verb_matches[0].ToString() + "</Unmatched>");
                }

            }
            MatchCollection multi_verb_matches_1 = Regex.Matches(input_1.ToString(), "(<VerbPhrase><IN>that</IN><VB(.*?)>.*?ed</VB(.*?)></VerbPhrase>)(<VerbPhrase><VB(.*?)>(was|were|are|is)</VB(.*?)><VB(.*?)>.*?ed</VB(.*?)>)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            if (multi_verb_matches_1.Count == 1)
            {
                MatchCollection temp_match = Regex.Matches(multi_verb_matches_1[0].ToString(), "(<VerbPhrase><IN>that</IN><VB(.*?)>.*?ed</VB(.*?)></VerbPhrase>)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                input_1 = input_1.Replace(temp_match[0].ToString(), "<Unmatched>" + temp_match[0].ToString() + "</Unmatched>");
            }



            XmlDocument doc_root = new XmlDocument();
            doc_root.LoadXml(input_1);


            if (doc_root.FirstChild.FirstChild.ChildNodes.Count >= 3 && doc_root.FirstChild.FirstChild.ChildNodes[2].Name == "COMMA")
            {
                string temp = (doc_root.FirstChild.FirstChild.ChildNodes[0].OuterXml.ToString() + doc_root.FirstChild.FirstChild.ChildNodes[1].OuterXml.ToString() + doc_root.FirstChild.FirstChild.ChildNodes[2].OuterXml.ToString());
                if (Regex.Matches(temp, "<VerbPhrase>(.*?)<VB(.*?)>(.*ed)</VB(.*?)>(.*?)</VerbPhrase>").Count == 0)
                {
                    input_1 = input_1.Replace(temp, "<Unmatched><ActionPhrase type=\"scope\">" + temp + "</ActionPhrase>" + doc_root.FirstChild.FirstChild.ChildNodes[2].OuterXml.ToString() + "</Unmatched>");
                }
            }
            else if (doc_root.FirstChild.FirstChild.ChildNodes.Count >= 2 && doc_root.FirstChild.FirstChild.ChildNodes[1].Name == "COMMA")
            {
                string temp = (doc_root.FirstChild.FirstChild.ChildNodes[0].OuterXml.ToString() + doc_root.FirstChild.FirstChild.ChildNodes[1].OuterXml.ToString());
                if (Regex.Matches(temp, "<VerbPhrase>(.*?)<VB(.*?)>(.*ed)</VB(.*?)>(.*?)</VerbPhrase>").Count == 0)
                {
                    input_1 = input_1.Replace(temp, "<Unmatched>" + temp + "</Unmatched>");
                }
            }
            else if (doc_root.FirstChild.ChildNodes[1] != null && doc_root.FirstChild.ChildNodes[1].Name == "COMMA")
            {
                string temp = (doc_root.FirstChild.ChildNodes[0].OuterXml.ToString() + doc_root.FirstChild.ChildNodes[1].OuterXml.ToString());
                if (Regex.Matches(temp, "<VerbPhrase>(.*?)<VB(.*?)>(.*ed)</VB(.*?)>(.*?)</VerbPhrase>").Count == 0)
                {
                    input_1 = input_1.Replace(temp, "<Unmatched>" + temp + "</Unmatched>");
                }
            }


            input_1 = Regex.Replace(input_1, "<Unmatched><RB-CONJ>Then</RB-CONJ></Unmatched>", " ");

           
        

        
        return input_1;
    }
    //POSContainer posContainer = new POSContainer();
    //ChemistrySentenceParser chemistrySentenceParser;
    protected void Page_Load(object sender, EventArgs e)
    {
        //posContainer = ChemistryPOSTagger.getDefaultInstance().runTaggers("");
        //chemistrySentenceParser = new ChemistrySentenceParser(posContainer);
        //chemistrySentenceParser.parseTags();
    }

    public string IsSurrounded(string text)
    {
        int level = 0, level_1 = 0;
        string error = "";
        List<int> brace_postion = new List<int>();

        foreach (char c in text)
        {
            if (c == '(')
            {
                // opening brace detected
                brace_postion.Add(level);
                level_1++;
            }

            if (c == ')')
            {
                level_1--;
                if (brace_postion.Count > 0)
                {
                    brace_postion.RemoveAt(brace_postion.Count - 1);
                    if (level_1 < 0)
                    {
                        // closing brace detected, without a corresponding opening brace
                        text = text.Remove(level, 1);
                    }
                }
            }
            level++;
        }

        if (brace_postion.Count > 0)
        {
            // more open than closing braces
            foreach (int p in brace_postion)
            {
                text = text.Remove(p, 1);
            }

        }
        return text;
    }

}