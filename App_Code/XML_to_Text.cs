using LemmaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

/// <summary>
/// Summary description for XML_to_Text
/// </summary>
public class XML_to_Text
{
    public string Return_XML_to_Text(string xml_string)
    {
        xml_string =" "+ xml_string + " ";

        xml_string = xml_string.Replace("<SYM>", "");
        xml_string = xml_string.Replace("</SYM>", "");

        MatchCollection cel_replace = Regex.Matches(xml_string, "<CD>(.*?)</CD><NN-TEMP>°C</NN-TEMP>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        foreach (Match total_match in cel_replace)
        {
            string temp = Regex.Replace(total_match.ToString(), "<.*?>", "");
            xml_string = xml_string.Replace(total_match.ToString(), temp);
        }
            

        //MatchCollection sub_replace = Regex.Matches(xml_string, "&lt;<NN>sup</NN>&gt;(.*?)&lt;/sup&gt;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //foreach (Match total_match in sub_replace)
        //{
        //    string temp = Regex.Replace(total_match.ToString(), "<.*?>", "");
        //    xml_string = xml_string.Replace(total_match.ToString(), temp);
        //}
        //MatchCollection sup_replace = Regex.Matches(xml_string, "&lt;<NN>sub</NN>&gt;(.*?)&lt;/sub&gt;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //foreach (Match total_match in sup_replace)
        //{
        //    string temp = Regex.Replace(total_match.ToString(), "<.*?>", "");
        //    xml_string = xml_string.Replace(total_match.ToString(), temp);
        //}
        //MatchCollection ital_replace = Regex.Matches(xml_string, "&lt;<NN>ital</NN>&gt;(.*?)&lt;/ital&gt;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //foreach (Match total_match in ital_replace)
        //{
        //    string temp = Regex.Replace(total_match.ToString(), "<.*?>", "");
        //    xml_string = xml_string.Replace(total_match.ToString(), temp);
        //}
        //MatchCollection bold_replace = Regex.Matches(xml_string, "&lt;<JJ>bold</JJ>&gt;(.*?)&lt;/bold&gt;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //foreach (Match total_match in bold_replace)
        //{
        //    string temp = Regex.Replace(total_match.ToString(), "<.*?>", "");
        //    xml_string = xml_string.Replace(total_match.ToString(), temp);
        //}


        xml_string = Regex.Replace(xml_string.ToString(), "<.*?>", " ");
        while (xml_string.Contains("  "))
        {
            xml_string = Regex.Replace(xml_string, "  ", " ");
        }


        xml_string = xml_string.Replace(" &lt;/sup&gt; ", "&lt;/sup&gt;");
        xml_string = xml_string.Replace(" &lt;/sub&gt; ", "&lt;/sub&gt;");
        xml_string = xml_string.Replace(" &lt;/ital&gt; ", "&lt;/ital&gt;");
        xml_string = xml_string.Replace(" &lt;/bold &gt; ", "&lt;/bold&gt;");
        xml_string = xml_string.Replace(" &lt;/para&gt; ", "&lt;/para&gt;");

        xml_string = xml_string.Replace(" &gt; ", "&gt;");
        xml_string = xml_string.Replace(" &lt; ", "&lt;");

          MatchCollection double_replace = Regex.Matches(xml_string, "(?<=\")( (.*?) )(?=\")", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        foreach (Match total_match in double_replace)
        {
            xml_string = xml_string.Replace(total_match.ToString(), total_match.ToString().Trim());
        }
            
        

        //xml_string = Regex.Replace(xml_string, " &lt;sub", "&lt;sub");
        //xml_string = Regex.Replace(xml_string, "&lt;/sub&gt; ", "&lt;/sub&gt;");

        //xml_string = Regex.Replace(xml_string, " &lt;sup", "&lt;sup");
        //xml_string = Regex.Replace(xml_string, "&lt;/sup&gt; ", "&lt;/sup&gt;");

        //xml_string = Regex.Replace(xml_string, " &lt;ital", "&lt;ital");
        //xml_string = Regex.Replace(xml_string, "&lt;/ital&gt; ", "&lt;/ital&gt;");

        //xml_string = Regex.Replace(xml_string, " &lt;bold", "&lt;bold");
        //xml_string = Regex.Replace(xml_string, "&lt;/bold&gt; ", "&lt;/bold&gt;");


        return xml_string.Trim();
	}
    public string Return_Replace(string return_output)
    {
        while (return_output.Contains("  "))
        {
            return_output = return_output.Replace("  ", " ");
        }

        return_output = return_output.Replace(" rt ", " room temperature ").Replace("  ", " ");
        return_output = return_output.Replace(" RT ", " room temperature ").Replace("  ", " ");
        return_output = return_output.Replace(" abs ", " absolute ").Replace("  ", " ");
        return_output = return_output.Replace(" conc ", " concentrated ").Replace("  ", " ");
        return_output = return_output.Replace(" dil ", " dilute ").Replace("  ", " ");
        return_output = return_output.Replace(" sol ", " solution ").Replace("  ", " ");
        return_output = return_output.Replace(" temp ", " temperature ").Replace("  ", " ");
        return_output = return_output.Replace(" vol ", " volume ").Replace("  ", " ");

        return_output = return_output.Replace(" rt.", " room temperature.").Replace("  ", " ");
        return_output = return_output.Replace(" abs.", " absolute.").Replace("  ", " ");
        return_output = return_output.Replace(" conc.", " concentrated.").Replace("  ", " ");
        return_output = return_output.Replace(" dil.", " dilute.").Replace("  ", " ");
        return_output = return_output.Replace(" sol.", " solution.").Replace("  ", " ");
        return_output = return_output.Replace(" temp.", " temperature.").Replace("  ", " ");
        return_output = return_output.Replace(" vol.", " volume.").Replace("  ", " ");

        return_output = return_output.Replace(" h ", " hours ").Replace("  ", " ");
        return_output = return_output.Replace(" min ", " minutes ").Replace("  ", " ");
        return_output = return_output.Replace(" equiv ", " equivalents").Replace("  ", " ");

        return_output = return_output.Replace(" h.", " hours.").Replace("  ", " ");
        return_output = return_output.Replace(" min.", " minutes.").Replace("  ", " ");
        return_output = return_output.Replace(" equiv.", " equivalents.").Replace("  ", " ");

        return_output = return_output.Replace(" h,", " hours,").Replace("  ", " ");
        return_output = return_output.Replace(" min,", " minutes,").Replace("  ", " ");
        return_output = return_output.Replace(" equiv,", " equivalents,").Replace("  ", " ");

       

        return_output = return_output.Replace("sieve", "sieves ").Replace("  ", " ");
        return_output = return_output.Replace("sieve.", "sieves.").Replace("  ", " ");
        return_output = return_output.Replace("sieve)", "sieves)").Replace("  ", " ");
        return_output = return_output.Replace("an additional hours", "an additional hour").Replace("  ", " ");
        return_output = return_output.Replace("an another hours", "an another hour").Replace("  ", " ");
        return_output = return_output.Replace("a additional", "an additional").Replace("  ", " ");
        return_output = return_output.Replace("Reextract", "Re-extract").Replace("  ", " ");
        return_output = return_output.Replace(" 0.5 hours", " 0.5 hour").Replace("  ", " ");
        return_output = return_output.Replace(" 1 hours", " 1 hour").Replace("  ", " ");
        return_output = return_output.Replace(" 1 minutes", " 1 minute").Replace("  ", " ");
        return_output = return_output.Replace("minutes period", "minute period").Replace("  ", " ");
        return_output = return_output.Replace("hours period", "hour period").Replace("  ", " ");
        return_output = return_output.Replace("minutes interval", "minute interval").Replace("  ", " ");
        return_output = return_output.Replace("hours interval", "hour interval").Replace("  ", " ");
        return_output = return_output.Replace(" HO.", "HCl.").Replace("  ", " ");
        return_output = return_output.Replace(" HO ", "HCl").Replace("  ", " ");
        return_output = return_output.Replace("K <sub>2</sub>CO<sub>3</sub>", "K<sub>2</sub>CO<sub>3</sub>").Replace("  ", " ");
        return_output = return_output.Replace("Na <sub>2</sub>CO<sub>3</sub>", "Na<sub>2</sub>CO<sub>3</sub>").Replace("  ", " ");
        return_output = return_output.Replace("under Ar.", "under argon.").Replace("  ", " ");
        return_output = return_output.Replace("Carryout", "Carry out").Replace("  ", " ");
        return_output = return_output.Replace("saturate HCl", "saturated HCl").Replace("  ", " ");
        return_output = return_output.Replace("saturate NaHCO", "saturated NaHCO").Replace("  ", " ");
        return_output = return_output.Replace("saturate H<sub>2</sub>SO<sub>4</sub>", "saturated H<sub>2</sub>SO<sub>4</sub>").Replace("  ", " ");
        return_output = return_output.Replace("saturate H2SO4", "saturated H2SO4").Replace("  ", " ");
        return_output = return_output.Replace("saturate Na<sub>2</sub>SO<sub>4</sub>", "saturated Na<sub>2</sub>SO<sub>4</sub>").Replace("  ", " ");
        return_output = return_output.Replace("saturate Na2SO4", "saturated Na2SO4").Replace("  ", " ");
        return_output = return_output.Replace("concentrate HCl", "concentrated HCl").Replace("  ", " ");
        return_output = return_output.Replace("concentrate NaHCO", "concentrated NaHCO").Replace("  ", " ");
        return_output = return_output.Replace("concentrate H<sub>2</sub>SO<sub>4</sub>", "concentrated H<sub>2</sub>SO<sub>4</sub>").Replace("  ", " ");
        return_output = return_output.Replace("concentrate H2SO4", "concentrated H2SO4").Replace("  ", " ");
        return_output = return_output.Replace("concentrate Na<sub>2</sub>SO<sub>4</sub>", "concentrated Na<sub>2</sub>SO<sub>4</sub>").Replace("  ", " ");
        return_output = return_output.Replace("concentrate Na2SO4", "concentrated Na2SO4").Replace("  ", " ");
        return_output = return_output.Replace("reduce pressure", "reduced pressure").Replace("  ", " ");
        return_output = return_output.Replace("saturated Na<sub>2</sub>SO<sub>4</sub> aqueous", "saturated aqueous Na<sub>2</sub>SO<sub>4</sub> ").Replace("  ", " ");
        return_output = return_output.Replace("/hexane)", "/hexanes)").Replace("  ", " ");
        return_output = return_output.Replace("/minutes", "/min").Replace("  ", " ");
        return_output = return_output.Replace("/ minutes", "/min").Replace("  ", " ");
        return_output = return_output.Replace("/minute", "/min").Replace("  ", " ");
        return_output = return_output.Replace("/ minute", "/min").Replace("  ", " ");
        return_output = return_output.Replace("niL/", "mL/").Replace("  ", " ");
        return_output = return_output.Replace("rnL/", "mL/").Replace("  ", " ");
        return_output = return_output.Replace("-l-", " -1-").Replace("  ", " ");
        return_output = return_output.Replace(" he ", " the ").Replace("  ", " ");
        return_output = return_output.Replace("lawesson's reagent ", "Lawesson's reagent ").Replace("  ", " ");
        return_output = return_output.Replace("wang resin", "Wang resin").Replace("  ", " ");
        return_output = return_output.Replace("dess-martin", "Dess-Martin").Replace("  ", " ");
        return_output = return_output.Replace("fehling's reagent", "Fehling's reagent").Replace("  ", " ");
        return_output = return_output.Replace("fenton's reagent", "Fenton's reagent").Replace("  ", " ");
        return_output = return_output.Replace("grignard reagent", "Grignard reagent").Replace("  ", " ");
        return_output = return_output.Replace("millon's reagent", "Millon's reagent").Replace("  ", " ");
        return_output = return_output.Replace("raney nickel", "Raney nickel").Replace("  ", " ");
        return_output = return_output.Replace("tollens' reagent", "Tollens' reagent").Replace("  ", " ");
        return_output = return_output.Replace("barfoeds reagent", "Barfoeds reagent").Replace("  ", " ");
        return_output = return_output.Replace("benedict's reagent ", "Benedict's reagent ").Replace("  ", " ");
        return_output = return_output.Replace("bunsen burner ", "Bunsen burner ").Replace("  ", " ");
        return_output = return_output.Replace("erlenmeyer flask ", "Erlenmeyer flask ").Replace("  ", " ");
        return_output = return_output.Replace("florence flask", "Florence flask").Replace("  ", " ");
        return_output = return_output.Replace("pyrex filter", "Pyrex filter").Replace("  ", " ");
        return_output = return_output.Replace("schelen tube", "Schelen tube").Replace("  ", " ");
        return_output = return_output.Replace("amberlyst", "Amberlyst").Replace("  ", " ");
        return_output = return_output.Replace("aerosil", "Aerosil").Replace("  ", " ");
        return_output = return_output.Replace("cloisite", "Cloisite").Replace("  ", " ");
        return_output = return_output.Replace("hexcel ", "Hexcel ").Replace("  ", " ");
        return_output = return_output.Replace("celite", "Celite").Replace("  ", " ");
        return_output = return_output.Replace("colour", "color").Replace("  ", " ");
        return_output = return_output.Replace("coloured", "colored").Replace("  ", " ");
        return_output = return_output.Replace("colourless", "colorless").Replace("  ", " ");
        return_output = return_output.Replace("crystallisation", "crystallization").Replace("  ", " ");
        return_output = return_output.Replace("crystallise", "crystallize").Replace("  ", " ");
        return_output = return_output.Replace("crystallised", "crystallized").Replace("  ", " ");
        return_output = return_output.Replace("crystallises", "crystallizes").Replace("  ", " ");
        return_output = return_output.Replace("crystallising", "crystallizing").Replace("  ", " ");
        return_output = return_output.Replace("grey", "gray").Replace("  ", " ");
        return_output = return_output.Replace("odour", "odor").Replace("  ", " ");
        return_output = return_output.Replace("odourless", "odorless").Replace("  ", " ");
        return_output = return_output.Replace("sulphate", "sulfate").Replace("  ", " ");
        return_output = return_output.Replace("sulphide", "sulfide").Replace("  ", " ");
        return_output = return_output.Replace("sulphur", "sulfur").Replace("  ", " ");
        return_output = return_output.Replace("vaporise", "vaporize").Replace("  ", " ");
        return_output = return_output.Replace("vaporised", "vaporized").Replace("  ", " ");
        return_output = return_output.Replace("vaporises", "vaporizes").Replace("  ", " ");
        return_output = return_output.Replace("vaporising", "vaporizing").Replace("  ", " ");
        return_output = return_output.Replace("vapour", "vapor").Replace("  ", " ");
        return_output = return_output.Replace("vapours", "vapors").Replace("  ", " ");
        return_output = return_output.Replace("Filter-off", "Filter off").Replace("  ", " ");
        return_output = return_output.Replace("Co-evaporate", "Coevaporate").Replace("  ", " ");
        return_output = return_output.Replace("Re-distill", "Redistill").Replace("  ", " ");
        return_output = return_output.Replace("Re-dissolve", "Redissolve").Replace("  ", " ");
        return_output = return_output.Replace("Coconcentrate", "Co-concentrate").Replace("  ", " ");
        return_output = return_output.Replace("minutes<sup>-1</sup>", "minute<sup>-1</sup>").Replace("  ", " ");
        return_output = return_output.Replace("resulted", "resulting/ results").Replace("  ", " ");
        return_output = return_output.Replace("as indicates ", "as indicated").Replace("  ", " ");
        return_output = return_output.Replace(", and ", " and ").Replace("  ", " ");
        return_output = return_output.Replace("by aqueous", "with aqueous").Replace("  ", " ");
        return_output = return_output.Replace("disted ", "distilled").Replace("  ", " ");
        return_output = return_output.Replace("by saturated", "with saturated").Replace("  ", " ");
        return_output = return_output.Replace("reextract", "re-extract").Replace("  ", " ");
        return_output = return_output.Replace("reextraction", "re-extraction").Replace("  ", " ");
        return_output = return_output.Replace("reevaporate", "re-evaporate").Replace("  ", " ");
        return_output = return_output.Replace("reexchange", "re-exchange").Replace("  ", " ");
        return_output = return_output.Replace("distil ", "distill").Replace("  ", " ");
        return_output = return_output.Replace("After complete the reaction, ", "After the reaction is complete, ").Replace("  ", " ");
        return_output = return_output.Replace("hours stirring", "hours of stirring").Replace("  ", " ");
        return_output = return_output.Replace("minutes stirring", "minutes of stirring").Replace("  ", " ");
        return_output = return_output.Replace("minutes stirring", "minutes of stirring").Replace("  ", " ");
        return_output = return_output.Replace("hours shaking", "hours of shaking").Replace("  ", " ");
        return_output = return_output.Replace("Purify by", "Purify the product by").Replace("  ", " ");
        return_output = return_output.Replace("minutes-1", "min-1").Replace("  ", " ");
        return_output = return_output.Replace("Pd<sub>C</sub>l<sub>2</sub>", "PdCl<sub>2</sub>").Replace("  ", " ");




       
        return_output = return_output.Replace("0.1 h ", "0.1 hour ").Replace("  ", " ");
        return_output = return_output.Replace("0.2 h ", "0.2 hour ").Replace("  ", " ");
        return_output = return_output.Replace("0.3 h ", "0.3 hour ").Replace("  ", " ");
        return_output = return_output.Replace("0.4 h ", "0.4 hour ").Replace("  ", " ");
        return_output = return_output.Replace("0.5 h ", "0.5 hour ").Replace("  ", " ");
        return_output = return_output.Replace("0.6 h ", "0.6 hour ").Replace("  ", " ");
        return_output = return_output.Replace("0.7 h ", "0.7 hour ").Replace("  ", " ");
        return_output = return_output.Replace("0.8 h ", "0.8 hour ").Replace("  ", " ");
        return_output = return_output.Replace("0.9 h ", "0.9 hour ").Replace("  ", " ");
        return_output = return_output.Replace("1.0 h ", "1.0 hour ").Replace("  ", " ");
        return_output = return_output.Replace("1 h ", "1.0 hour ").Replace("  ", " ");

        return_output = return_output.Replace("0.1 h,", "0.1 hour,").Replace("  ", " ");
        return_output = return_output.Replace("0.2 h,", "0.2 hour,").Replace("  ", " ");
        return_output = return_output.Replace("0.3 h,", "0.3 hour,").Replace("  ", " ");
        return_output = return_output.Replace("0.4 h,", "0.4 hour,").Replace("  ", " ");
        return_output = return_output.Replace("0.5 h,", "0.5 hour,").Replace("  ", " ");
        return_output = return_output.Replace("0.6 h,", "0.6 hour,").Replace("  ", " ");
        return_output = return_output.Replace("0.7 h,", "0.7 hour,").Replace("  ", " ");
        return_output = return_output.Replace("0.8 h,", "0.8 hour,").Replace("  ", " ");
        return_output = return_output.Replace("0.9 h,", "0.9 hour,").Replace("  ", " ");
        return_output = return_output.Replace("1.0 h,", "1.0 hour,").Replace("  ", " ");
        return_output = return_output.Replace("1 h,", "1.0 hour,").Replace("  ", " ");

        return_output = return_output.Replace("0.1 h.", "0.1 hour.").Replace("  ", " ");
        return_output = return_output.Replace("0.2 h.", "0.2 hour.").Replace("  ", " ");
        return_output = return_output.Replace("0.3 h.", "0.3 hour.").Replace("  ", " ");
        return_output = return_output.Replace("0.4 h.", "0.4 hour.").Replace("  ", " ");
        return_output = return_output.Replace("0.5 h.", "0.5 hour.").Replace("  ", " ");
        return_output = return_output.Replace("0.6 h.", "0.6 hour.").Replace("  ", " ");
        return_output = return_output.Replace("0.7 h.", "0.7 hour.").Replace("  ", " ");
        return_output = return_output.Replace("0.8 h.", "0.8 hour.").Replace("  ", " ");
        return_output = return_output.Replace("0.9 h.", "0.9 hour.").Replace("  ", " ");
        return_output = return_output.Replace("1.0 h.", "1.0 hour.").Replace("  ", " ");
        return_output = return_output.Replace("1 h.", "1.0 hour.").Replace("  ", " ");

        return_output = return_output.Replace("1h,", "1 hours,").Replace("  ", " ");
        return_output = return_output.Replace("2h,", "2 hours,").Replace("  ", " ");
        return_output = return_output.Replace("3h,", "3 hours,").Replace("  ", " ");
        return_output = return_output.Replace("4h,", "4 hours,").Replace("  ", " ");
        return_output = return_output.Replace("5h,", "5 hours,").Replace("  ", " ");
        return_output = return_output.Replace("6h,", "6 hours,").Replace("  ", " ");
        return_output = return_output.Replace("7h,", "7 hours,").Replace("  ", " ");
        return_output = return_output.Replace("8h,", "8 hours,").Replace("  ", " ");
        return_output = return_output.Replace("9h,", "9 hours,").Replace("  ", " ");
        return_output = return_output.Replace("0h,", "0 hours,").Replace("  ", " ");

        return_output = return_output.Replace("1h ", "1 hours").Replace("  ", " ");
        return_output = return_output.Replace("2h ", "2 hours").Replace("  ", " ");
        return_output = return_output.Replace("3h ", "3 hours").Replace("  ", " ");
        return_output = return_output.Replace("4h ", "4 hours").Replace("  ", " ");
        return_output = return_output.Replace("5h ", "5 hours").Replace("  ", " ");
        return_output = return_output.Replace("6h ", "6 hours").Replace("  ", " ");
        return_output = return_output.Replace("7h ", "7 hours").Replace("  ", " ");
        return_output = return_output.Replace("8h ", "8 hours").Replace("  ", " ");
        return_output = return_output.Replace("9h ", "9 hours").Replace("  ", " ");
        return_output = return_output.Replace("0h ", "0 hours").Replace("  ", " ");


        return_output = return_output.Replace("0.1 min ", "0.1 minute ").Replace("  ", " ");
        return_output = return_output.Replace("0.2 min ", "0.2 minute ").Replace("  ", " ");
        return_output = return_output.Replace("0.3 min ", "0.3 minute ").Replace("  ", " ");
        return_output = return_output.Replace("0.4 min ", "0.4 minute ").Replace("  ", " ");
        return_output = return_output.Replace("0.5 min ", "0.5 minute ").Replace("  ", " ");
        return_output = return_output.Replace("0.6 min ", "0.6 minute ").Replace("  ", " ");
        return_output = return_output.Replace("0.7 min ", "0.7 minute ").Replace("  ", " ");
        return_output = return_output.Replace("0.8 min ", "0.8 minute ").Replace("  ", " ");
        return_output = return_output.Replace("0.9 min ", "0.9 minute ").Replace("  ", " ");
        return_output = return_output.Replace("1.0 min ", "1.0 minute ").Replace("  ", " ");
        return_output = return_output.Replace("1 min ", "1.0 minute ").Replace("  ", " ");

        return_output = return_output.Replace("0.1 min,", "0.1 minute,").Replace("  ", " ");
        return_output = return_output.Replace("0.2 min,", "0.2 minute,").Replace("  ", " ");
        return_output = return_output.Replace("0.3 min,", "0.3 minute,").Replace("  ", " ");
        return_output = return_output.Replace("0.4 min,", "0.4 minute,").Replace("  ", " ");
        return_output = return_output.Replace("0.5 min,", "0.5 minute,").Replace("  ", " ");
        return_output = return_output.Replace("0.6 min,", "0.6 minute,").Replace("  ", " ");
        return_output = return_output.Replace("0.7 min,", "0.7 minute,").Replace("  ", " ");
        return_output = return_output.Replace("0.8 min,", "0.8 minute,").Replace("  ", " ");
        return_output = return_output.Replace("0.9 min,", "0.9 minute,").Replace("  ", " ");
        return_output = return_output.Replace("1.0 min,", "1.0 minute,").Replace("  ", " ");
        return_output = return_output.Replace("1 min,", "1.0 minute,").Replace("  ", " ");

        return_output = return_output.Replace("0.1 min.", "0.1 minute.").Replace("  ", " ");
        return_output = return_output.Replace("0.2 min.", "0.2 minute.").Replace("  ", " ");
        return_output = return_output.Replace("0.3 min.", "0.3 minute.").Replace("  ", " ");
        return_output = return_output.Replace("0.4 min.", "0.4 minute.").Replace("  ", " ");
        return_output = return_output.Replace("0.5 min.", "0.5 minute.").Replace("  ", " ");
        return_output = return_output.Replace("0.6 min.", "0.6 minute.").Replace("  ", " ");
        return_output = return_output.Replace("0.7 min.", "0.7 minute.").Replace("  ", " ");
        return_output = return_output.Replace("0.8 min.", "0.8 minute.").Replace("  ", " ");
        return_output = return_output.Replace("0.9 min.", "0.9 minute.").Replace("  ", " ");
        return_output = return_output.Replace("1.0 min.", "1.0 minute.").Replace("  ", " ");
        return_output = return_output.Replace("1 min.", "1.0 minute.").Replace("  ", " ");


        return_output = return_output.Replace("0.1 equiv ", "0.1 equivalent ").Replace("  ", " ");
        return_output = return_output.Replace("0.2 equiv ", "0.2 equivalent ").Replace("  ", " ");
        return_output = return_output.Replace("0.3 equiv ", "0.3 equivalent ").Replace("  ", " ");
        return_output = return_output.Replace("0.4 equiv ", "0.4 equivalent ").Replace("  ", " ");
        return_output = return_output.Replace("0.5 equiv ", "0.5 equivalent ").Replace("  ", " ");
        return_output = return_output.Replace("0.6 equiv ", "0.6 equivalent ").Replace("  ", " ");
        return_output = return_output.Replace("0.7 equiv ", "0.7 equivalent ").Replace("  ", " ");
        return_output = return_output.Replace("0.8 equiv ", "0.8 equivalent ").Replace("  ", " ");
        return_output = return_output.Replace("0.9 equiv ", "0.9 equivalent ").Replace("  ", " ");
        return_output = return_output.Replace("1.0 equiv ", "1.0 equivalent ").Replace("  ", " ");
        return_output = return_output.Replace("1 equiv ", "1.0 equivalent ").Replace("  ", " ");

        return_output = return_output.Replace("0.1 equiv,", "0.1 equivalent,").Replace("  ", " ");
        return_output = return_output.Replace("0.2 equiv,", "0.2 equivalent,").Replace("  ", " ");
        return_output = return_output.Replace("0.3 equiv,", "0.3 equivalent,").Replace("  ", " ");
        return_output = return_output.Replace("0.4 equiv,", "0.4 equivalent,").Replace("  ", " ");
        return_output = return_output.Replace("0.5 equiv,", "0.5 equivalent,").Replace("  ", " ");
        return_output = return_output.Replace("0.6 equiv,", "0.6 equivalent,").Replace("  ", " ");
        return_output = return_output.Replace("0.7 equiv,", "0.7 equivalent,").Replace("  ", " ");
        return_output = return_output.Replace("0.8 equiv,", "0.8 equivalent,").Replace("  ", " ");
        return_output = return_output.Replace("0.9 equiv,", "0.9 equivalent,").Replace("  ", " ");
        return_output = return_output.Replace("1.0 equiv,", "1.0 equivalent,").Replace("  ", " ");
        return_output = return_output.Replace("1 equiv,", "1.0 equivalent,").Replace("  ", " ");

        return_output = return_output.Replace("0.1 equiv.", "0.1 equivalent.").Replace("  ", " ");
        return_output = return_output.Replace("0.2 equiv.", "0.2 equivalent.").Replace("  ", " ");
        return_output = return_output.Replace("0.3 equiv.", "0.3 equivalent.").Replace("  ", " ");
        return_output = return_output.Replace("0.4 equiv.", "0.4 equivalent.").Replace("  ", " ");
        return_output = return_output.Replace("0.5 equiv.", "0.5 equivalent.").Replace("  ", " ");
        return_output = return_output.Replace("0.6 equiv.", "0.6 equivalent.").Replace("  ", " ");
        return_output = return_output.Replace("0.7 equiv.", "0.7 equivalent.").Replace("  ", " ");
        return_output = return_output.Replace("0.8 equiv.", "0.8 equivalent.").Replace("  ", " ");
        return_output = return_output.Replace("0.9 equiv.", "0.9 equivalent.").Replace("  ", " ");
        return_output = return_output.Replace("1.0 equiv.", "1.0 equivalent.").Replace("  ", " ");
        return_output = return_output.Replace("1 equiv.", "1.0 equivalent.").Replace("  ", " ");

        return_output = return_output.Replace(" eq. ", " equivalent.").Replace("  ", " ");
        return_output = return_output.Replace(" eq ", " equivalent ").Replace("  ", " ");

        return_output = return_output.Replace(" ( ", " (").Replace("  ", " ");
        return_output = return_output.Replace(" ) ", ") ").Replace("  ", " ");
        while (return_output.Contains(" ,") || return_output.Contains(" ."))
        {
            return_output = return_output.Replace(" ,", ", ").Replace("  ", " ");
            return_output = return_output.Replace(" .", ". ").Replace("  ", " ");
        }
        return_output = return_output.Replace(" is ", " ").Replace("  ", " ");
        return_output = return_output.Replace("The", "the").Replace("  ", " ");
        //return_output = return_output.Replace(" then at", "at").Replace("  ", " ");
        
        return_output = Regex.Replace(return_output, "&amp;", "&").Replace("  ", " ");

        return_output = return_output.Replace(", which", " ").Replace("  ", " ");
        return_output = return_output.Replace(", to which", " ").Replace("  ", " ");
        return_output = return_output.Replace(" A ", " a ").Replace("  ", " ");
        return_output = return_output.Replace(" to to ", " to ").Replace("  ", " ");
        return_output = return_output.Replace("Next, ", " next ").Replace("  ", " ");
        return_output = return_output.Replace(" After", " after").Replace("  ", " ");
       

        

        //return_output = return_output.Replace(". ", ".").Replace("  ", " ");
  

        return_output = return_output.Trim() + ".";
        return return_output.Trim();
    }
    public string Return_Replace_Rule(string return_output)
    {
        while (return_output.Contains("  "))
        {
            return_output = return_output.Replace("  ", " ");
        }


        return_output = return_output.Replace(">/<", ">YY<").Replace("  ", " ");


        return_output = return_output.Replace("<para>", "").Replace("  ", " ");
        return_output = return_output.Replace("</para>", "").Replace("  ", " ");

        return_output = return_output.Replace("(", " ( ").Replace("  ", " ");
        return_output = return_output.Replace(")", " ) ").Replace("  ", " ");

        return_output = return_output.Replace(".1,", ". 1,").Replace("  ", " ");
        return_output = return_output.Replace(".2,", ". 2,").Replace("  ", " ");
        return_output = return_output.Replace(".3,", ". 3,").Replace("  ", " ");
        return_output = return_output.Replace(".4,", ". 4,").Replace("  ", " ");
        return_output = return_output.Replace(".5,", ". 5,").Replace("  ", " ");
        return_output = return_output.Replace(".6,", ". 6,").Replace("  ", " ");
        return_output = return_output.Replace(".7,", ". 7,").Replace("  ", " ");
        return_output = return_output.Replace(".8,", ". 8,").Replace("  ", " ");
        return_output = return_output.Replace(".9,", ". 9,").Replace("  ", " ");
        return_output = return_output.Replace(".0,", ". 0,").Replace("  ", " ");

        return_output = return_output.Replace(" t he ", " the ").Replace("  ", " ");
        return_output = return_output.Replace(" o f ", " of ").Replace("  ", " ");
        return_output = return_output.Replace(" w as ", " was ").Replace("  ", " ");
        return_output = return_output.Replace(" m g,", " mg,").Replace("  ", " ");
        return_output = return_output.Replace("A fterw ards,", "Afterwards,").Replace("  ", " ");
        return_output = return_output.Replace("&#x00B0;&#x0043;", "&#x00B0;&#x0043").Replace("  ", " ");

        return_output = return_output.Replace("After that ", "After that, ").Replace("  ", " ");
        return_output = return_output.Replace(", it then was ", " and was ").Replace("  ", " ");
        return_output = return_output.Replace(", to which ", ". ").Replace("  ", " ");
        


        while (return_output.Contains("  "))
        {
            return_output = return_output.Replace("  ", " ");
        }
        return return_output.Trim();



    }
    public string Return_mistake_Replace(string return_output)
    {
        while (return_output.Contains("  "))
        {
            return_output = return_output.Replace("  ", " ");
        }
        return_output = return_output.Replace("&lt;", "<").Replace("  ", " ");
        return_output = return_output.Replace("&gt;", ">").Replace("  ", " ");

        return_output = Regex.Replace(return_output,">YY<", ">/<").Replace("  ", " ");

        return_output = return_output.Replace(" °C", " °C").Replace("  ", " ");
       
        return_output = return_output.Replace(" to.", "").Replace("  ", " ");
        return_output = return_output.Replace(" and.", ".").Replace("  ", " ");
        return_output = return_output.Replace(" and .", ".").Replace("  ", " ");
        return_output = return_output.Replace(" .", ".").Replace("  ", " ");
        //return_output = return_output.Replace(";.", " ").Replace("  ", " ");

        return_output = return_output.Replace("> [ ", ">[").Replace("  ", " ");

        return_output = return_output.Replace(" changed ", " changes ").Replace("  ", " ");
        return_output = return_output.Replace(" then ", " ").Replace("  ", " ");

        return_output = return_output.Replace(" to up to", " up to ").Replace("  ", " ");

        return_output = return_output.Replace(" and then.", ".").Replace("  ", " ");
        return_output = return_output.Replace("Add next ", "Add ").Replace("  ", " ");

        return_output = return_output.Replace(" kept ", " keep ").Replace("  ", " ");
        return_output = return_output.Replace(" left ", " leave ").Replace("  ", " ");

        return_output = return_output.Replace("&#x00B0;&#x0043", "&#x00B0;&#x0043;").Replace("  ", " ");

        //return_output = Regex.Replace(return_output, " < sub > ", "<sub>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " </sub > ", "</sub>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " < sub >", "<sub>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " </sub >", "</sub>").Replace("  ", " ");

        //return_output = Regex.Replace(return_output, " < sup > ", "<sup>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " </sup > ", "</sup>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " < sup >", "<sup>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " </sup >", "</sup>").Replace("  ", " ");

        //return_output = Regex.Replace(return_output, " < ital > ", "<ital>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " </ital > ", "</ital>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " < ital >", "<ital>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " </ital >", "</ital>").Replace("  ", " ");

        //return_output = Regex.Replace(return_output, " < bold > ", "<bold>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " </bold > ", "</bold>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " < bold >", "<bold>").Replace("  ", " ");
        //return_output = Regex.Replace(return_output, " </bold >", "</bold>").Replace("  ", " ");

        //MatchCollection sub_replace = Regex.Matches(return_output, "<sub>(.*)</sub>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //foreach (Match total_match in sub_replace)
        //{
        //    return_output = return_output.Replace(total_match.ToString(), total_match.ToString() + " ");
        //}
        //MatchCollection sup_replace = Regex.Matches(return_output, "<sup>(.*)</sup>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //foreach (Match total_match in sup_replace)
        //{
        //    return_output = return_output.Replace(total_match.ToString(), total_match.ToString() + " ");
        //}
        //MatchCollection ital_replace = Regex.Matches(return_output, "<ital>(.*)</ital>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //foreach (Match total_match in ital_replace)
        //{

        //    return_output = return_output.Replace(total_match.ToString(), total_match.ToString() + " ");
        //}
        //MatchCollection bold_replace = Regex.Matches(return_output, "<bold>(.*)</bold>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //foreach (Match total_match in bold_replace)
        //{

        //    return_output = return_output.Replace(total_match.ToString(), total_match.ToString() + " ");
        //}
        

        //return_output = return_output.Replace(" (", "(").Replace("  ", " ");
        return_output = return_output.Replace("( ", "(").Replace("  ", " ");
        return_output = return_output.Replace(" )", ")").Replace("  ", " ");
        //return_output = return_output.Replace(") ", ")").Replace("  ", " ");
        return_output = return_output.Replace(" =", "=").Replace("  ", " ");
        return_output = return_output.Replace("= ", "=").Replace("  ", " ");
        return_output = return_output.Replace(", ,", ", ").Replace("  ", " ");
        return_output = return_output.Replace(",.", " ").Replace("  ", " ");

    
        
        while (return_output.Contains(" :") || return_output.Contains(": "))
        {
            return_output = return_output.Replace(" :", ":").Replace("  ", " ");
            return_output = return_output.Replace(": ", ":").Replace("  ", " ");
        }
        while (return_output.Contains(" %"))
        {
            return_output = return_output.Replace(" %", "% ").Replace("  ", " ");
        }

        while (return_output.Contains(".."))
        {
            return_output = return_output.Replace("..", ".").Replace("  ", " ");
        }


        return_output = return_output.Replace(",,", ",").Replace("  ", " ");
        return_output = return_output.Replace("% )", "%)").Replace("  ", " ");


        while (return_output.Contains("  "))
        {
            return_output = return_output.Replace("  ", " ");
        }


        return return_output;
    }

}