using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Regex_Pattern
/// </summary>
public class Regex_Pattern
{
	
        public string Unwanted_Nounphrase_with_colon_Pattern()
        {return "<NounPhrase>(.*?)</NounPhrase><COLON>:</COLON>";}
        public string Unwanted_ActionPhrase_with_colon_Pattern ()
        {return "<ActionPhrase(.*?)>(.*?)</ActionPhrase><COLON>:</COLON><ActionPhrase>";}
        public string Unwanted_ActionPhrase_with_colon_Pattern_1 ()
        {return "<ActionPhrase(.*?)>(.*?)</ActionPhrase><COLON>:</COLON>";}
        public string double_quote_val ()
        {return "\"(.*?)\"";}
        public string NounPhrase_Pattern ()
        {return "<NounPhrase>(.)*</NounPhrase>";}
        public string VerbPhrase_Pattern ()
        {return "<VerbPhrase>(.)*</VerbPhrase>";}
        public string PrepPhrase_Pattern ()
        {return "<PrepPhrase>(.)*</PrepPhrase>";}

        //var PrepPhrase_Pattern = "<PrepPhrase>(.|\n)*?/(.|\n)*?>";
        //var TimePhrase_Pattern ="<TimePhrase>(.|\n)*?</TimePhrase>";
        //var TempPhrase_Pattern ="<TempPhrase>(.|\n)*?</TempPhrase>";
        //var Molecule_Pattern = "<MOLECULE(.|\n)*?</MOLECULE>";
        public string VBD_Pattern ()
        { return "<VBD>allowed</VBD>"; }
        public string VBD_Pattern_kept()
        { return "<VBD>kept</VBD>"; }
        public string VB_Pattern ()
        {return "<VB-(.)*?>(.)*?</VB-(.)*?>";}
        public string VBN_Pattern ()
        {return "<VBN(.)*?>(.)*?</VBN(.)*?>";}
        public string VBG_Pattern()
        { return "<VBG(.)*?>(.)*?</VBG(.)*?>"; }
        public string VBP_Pattern()
        { return "<VBP(.)*?>(.)*?</VBP(.)*?>"; }
        public string RB_Pattern ()
        {return "<RB>(.)*?</RB>";}
        public string OSCAR_RN_Pattern ()
        {return "<OSCAR-RN>.*?</OSCAR-RN>";}
        public string STOP_Pattern ()
        { return "<STOP>.*?</STOP>"; }

        public string MOLECULE_Pattern()
        { return "<MOLECULE.*?>.*</MOLECULE.*?>"; }
        public string NN_CHEMENTITY_Pattern()
        { return "<NN-CHEMENTITY>.*?</NN-CHEMENTITY>"; }
        public string DT_Pattern()
        { return "<DT.*?>.*?</DT.*?>"; }
        public string TimePhrase_Pattern()
        { return "<TimePhrase>.*?</TimePhrase>"; }
        public string AtmospherePhrase_Pattern()
        { return "<AtmospherePhrase>.*?</AtmospherePhrase>"; }
        public string APPARATUS_Pattern()
        {  return "<APPARATUS>.*?</APPARATUS>";      }
        public string Yielde_Pattern()
        { return "<ActionPhrase type=\"Yield\">(.)*?</ActionPhrase>"; }
	
    
}