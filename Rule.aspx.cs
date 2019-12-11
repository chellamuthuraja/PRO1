using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using uk.ac.cam.ch.wwmm.chemicaltagger;
using nu.xom;
using System.Text.RegularExpressions;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

       // Calling ChemistryPOSTagger
        POSContainer posContainer = ChemistryPOSTagger.getDefaultInstance().runTaggers(TextBox1.Text);

        // Returns a string of TAG TOKEN format (e.g.: DT The NN cat VB sat IN on DT the NN matt)
        // Call ChemistrySentenceParser either by passing the POSContainer or by InputStream
        ChemistrySentenceParser chemistrySentenceParser = new ChemistrySentenceParser(posContainer);

        chemistrySentenceParser.parseTags();

        // Return an XMLDoc
        Document doc = chemistrySentenceParser.makeXMLDocument();

        string ColouredText = string.Empty;
        ColouredText = TextBox1.Text;
        string OSCAEREntitys = string.Empty;
        string OSCAER = string.Empty;
        MatchCollection NE_Entity = Regex.Matches(doc.toXML(), @"<OSCARCM>(?<OSCARCM>.*?)</OSCARCM>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        if (NE_Entity.Count > 0)
        {
            foreach (Match matchedItems in NE_Entity)
            {
                OSCAEREntitys = matchedItems.Groups["OSCARCM"].Value;
                MatchCollection OSCAEREM = Regex.Matches(OSCAEREntitys, @"<OSCAR-CM>(?<OSCAR>.*?)</OSCAR-CM>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                if (OSCAEREM.Count > 0)
                {
                    foreach (Match matchedItems1 in OSCAEREM)
                    {
                        OSCAER = matchedItems1.Groups["OSCAR"].Value;
                        string color_HTMLview = "<span style='color:DarkViolet'><b>" + OSCAER + "</b></span>";
                        string Replaceword = "\\b" + OSCAER.Trim() + "\\b";
                        ColouredText = Regex.Replace(ColouredText, Replaceword, color_HTMLview, RegexOptions.IgnoreCase);

                    }
                }
            }
        }


    }
}