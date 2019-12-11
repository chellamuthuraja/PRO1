using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Make_Sentence
/// </summary>
public class Make_Sentence
{
    Regex_Pattern Regx_ptn = new Regex_Pattern();
    public Make_Sentence(string input)
	{
        string Unwanted_Nounphrase_with_colon_Pattern = Regx_ptn.Unwanted_Nounphrase_with_colon_Pattern();
        string Unwanted_ActionPhrase_with_colon_Pattern = Regx_ptn.Unwanted_ActionPhrase_with_colon_Pattern();
        string Unwanted_ActionPhrase_with_colon_Pattern_1 = Regx_ptn.Unwanted_ActionPhrase_with_colon_Pattern_1();
        string double_quote_val = Regx_ptn.double_quote_val();
        string NounPhrase_Pattern = Regx_ptn.NounPhrase_Pattern();
        string VerbPhrase_Pattern = Regx_ptn.VerbPhrase_Pattern();
        string PrepPhrase_Pattern = Regx_ptn.PrepPhrase_Pattern();
        string VBD_Pattern = Regx_ptn.VBD_Pattern();
        string VB_Pattern = Regx_ptn.VB_Pattern();
        string VBN_Pattern = Regx_ptn.VBN_Pattern();
        string RB_Pattern = Regx_ptn.RB_Pattern();
        string OSCAR_RN_Pattern = Regx_ptn.OSCAR_RN_Pattern();
        string STOP_Pattern = Regx_ptn.STOP_Pattern();
        string MOLECULE_Pattern = Regx_ptn.MOLECULE_Pattern();
        string NN_CHEMENTITY_Pattern = Regx_ptn.NN_CHEMENTITY_Pattern();
        string DT_Pattern = Regx_ptn.DT_Pattern();
        string TimePhrase_Pattern = Regx_ptn.TimePhrase_Pattern();
        string AtmospherePhrase_Pattern = Regx_ptn.AtmospherePhrase_Pattern();
        string APPARATUS_Pattern = Regx_ptn.APPARATUS_Pattern();


	}
}