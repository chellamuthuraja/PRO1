<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Extract.aspx.cs" Inherits="COLOR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="MAINCSS/jquery-latest.js" type="text/javascript"></script>
    <%--<script src="MAINCSS/functions.js" type="text/javascript"></script> --%>   
    <link href="MAINCSS/style-extract.css" rel="stylesheet" type="text/css" />
    <title>ChEmNER</title>
    <style type="text/css">       
    </style>
      <!-- javascript ==================================================Start-->
    
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {           
            var fields = document.getElementsByTagName('input');
            var nooffields = fields.length;
            for (var i = 0; i < nooffields; i++) {
                if (fields[i].type == 'checkbox') {
                    var checkboxname = fields[i].name;
                    fields[i].checked == true;
                    highlight(checkboxname, true);
                }
            }

        });       

        function highlight(name, checked) {
            $('span[name="' + name + '"]').each(function () {
                if (checked) {
                    $(this).addClass(name);
                }
                else {
                    $(this).removeClass(name);
                }
            }

            );
        }
    </script>


</head>

<body>
<form id="ActionsForm" runat="server" >
    <br />
    <br />
    <br />
     <div id='taggedReaction'>                       
     <span name="Document"><span name="Sentence"><span name="Add"><span name="PrepPhrase"><span name="TO">To</span> <span name="NounPhrase"><span name="DT">a</span> <span name="JJ-CHEM">stirred</span> <span name="NN-CHEMENTITY">solution</span> <span name="PrepPhrase"><span name="IN-OF">of</span> <span name="NounPhrase"><span name="Dissolve"><span name="Other"><span name="OSCARCM"><span name="OSCAR-CM">4-hydroxypiperidine</span> </span> <span name="Quantity"><span name="_-LRB-">(</span> <span name="MASS"><span name="CD">0.97</span> <span name="NN-MASS">g</span> </span> <span name="COMMA">,</span> <span name="AMOUNT"><span name="CD">9.60</span> <span name="NN-AMOUNT">mmol</span> </span> <span name="_-RRB-">)</span> </span> </span> <span name="IN-IN">in</span> <span name="JJ-CHEM">anhydrous</span> <span name="Solvent"><span name="OSCARCM"><span name="OSCAR-CM">dimethylformamide</span> </span> <span name="Quantity"><span name="_-LRB-">(</span> <span name="VOLUME"><span name="CD">20</span> <span name="NN-VOL">mL</span> </span> <span name="_-RRB-">)</span> </span> </span> </span> </span> </span> </span> </span> <span name="TempPhrase"><span name="IN">at</span> <span name="CD">0</span> <span name="NN-TEMP">&deg;C</span> </span> <span name="VerbPhrase"><span name="VBD">was</span> <span name="VB-ADD">added</span> </span> <span name="NounPhrase"><span name="Other"><span name="OSCARCM"><span name="OSCAR-CM">1-(bromomethyl)-4-methoxybenzene</span> </span> <span name="Quantity"><span name="_-LRB-">(</span> <span name="MASS"><span name="CD">1.93</span> <span name="NN-MASS">g</span> </span> <span name="COMMA">,</span> <span name="AMOUNT"><span name="CD">9.60</span> <span name="NN-AMOUNT">mmol</span> </span> <span name="_-RRB-">)</span> </span> </span> <span name="CC">and</span> <span name="Other"><span name="OSCARCM"><span name="OSCAR-CM">triethylamine</span> </span> <span name="Quantity"><span name="_-LRB-">(</span> <span name="MASS"><span name="CD">2.16</span> <span name="NN-MASS">g</span> </span> <span name="COMMA">,</span> <span name="AMOUNT"><span name="CD">21.4</span> <span name="NN-AMOUNT">mmol</span> </span> <span name="_-RRB-">)</span> </span> </span> </span> </span> <span name="STOP">.</span> </span> <span name="Sentence"><span name="Heat"><span name="NounPhrase"><span name="DT-THE">The</span> <span name="JJ-CHEM">reaction</span> <span name="NN-CHEMENTITY">mixture</span> </span> <span name="VerbPhrase"><span name="VBD">was</span> <span name="RB-CONJ">then</span> <span name="VB-HEAT">warmed</span> <span name="TempPhrase"><span name="TO">to</span> <span name="NN-TEMP">room</span> <span name="NN-TEMP">temperature</span> </span> </span> </span> <span name="CC">and</span> <span name="Stir"><span name="VerbPhrase"><span name="VB-STIR">stirred</span> <span name="TimePhrase"><span name="NN-TIME">overnight</span> </span> </span> </span> <span name="STOP">.</span> </span> <span name="Sentence"><span name="Concentrate"><span name="TimePhrase"><span name="IN-AFTER">After</span> <span name="DT">this</span> <span name="NN-TIME">time</span> </span> <span name="NounPhrase"><span name="DT-THE">the</span> <span name="NN-CHEMENTITY">mixture</span> </span> <span name="VerbPhrase"><span name="VBD">was</span> <span name="VB-CONCENTRATE">concentrated</span> <span name="PrepPhrase"><span name="IN-UNDER">under</span> <span name="NounPhrase"><span name="JJ">reduced</span> <span name="NN-PRESSURE">pressure</span> </span> </span> </span> </span> <span name="CC">and</span> <span name="Dissolve"><span name="NounPhrase"><span name="DT-THE">the</span> <span name="JJ-CHEM">resulting</span> <span name="NN-CHEMENTITY">residue</span> </span> <span name="VerbPhrase"><span name="VBD">was</span> <span name="VB-DISSOLVE">dissolved</span> <span name="PrepPhrase"><span name="IN-IN">in</span> <span name="Solvent"><span name="OSCARCM"><span name="OSCAR-CM">ethyl</span> <span name="OSCAR-CM">acetate</span> </span> <span name="Quantity"><span name="_-LRB-">(</span> <span name="VOLUME"><span name="CD">40</span> <span name="NN-VOL">mL</span> </span> <span name="_-RRB-">)</span> </span> </span> </span> </span> </span> <span name="COMMA">,</span> <span name="Wash"><span name="VerbPhrase"><span name="VB-WASH">washed</span> <span name="PrepPhrase"><span name="IN-WITH">with</span> <span name="NounPhrase"><span name="Solvent"><span name="OSCAR-CM">water</span> </span> <span name="Quantity"><span name="_-LRB-">(</span> <span name="VOLUME"><span name="CD">20</span> <span name="NN-VOL">mL</span> </span> <span name="_-RRB-">)</span> </span> </span> <span name="CC">and</span> <span name="Solvent"><span name="OSCARCM"><span name="OSCAR-CM">brine</span> </span> <span name="Quantity"><span name="_-LRB-">(</span> <span name="VOLUME"><span name="CD">20</span> <span name="NN-VOL">mL</span> </span> <span name="_-RRB-">)</span> </span> </span> </span> </span> </span> </span> <span name="Dry"><span name="VerbPhrase"><span name="IN-BEFORE">before</span> <span name="VBG">being</span> <span name="VB-DRY">dried</span> <span name="PrepPhrase"><span name="IN-OVER">over</span> <span name="NounPhrase"><span name="Other"><span name="OSCARCM"><span name="OSCAR-CM">sodium</span> <span name="OSCAR-CM">sulfate</span> </span> </span> </span> </span> </span> </span> <span name="STOP">.</span> </span> <span name="Sentence"><span name="Filter"><span name="NounPhrase"><span name="DT-THE">The</span> <span name="JJ-CHEM">drying</span> <span name="NN">agent</span> </span> <span name="VerbPhrase"><span name="VBD">was</span> <span name="VB-FILTER">filtered</span> <span name="IN-OFF">off</span> </span> </span> <span name="CC">and</span> <span name="Concentrate"><span name="NounPhrase"><span name="DT-THE">the</span> <span name="NN">filtrate</span> </span> <span name="VerbPhrase"><span name="VB-CONCENTRATE">concentrated</span> <span name="PrepPhrase"><span name="IN-UNDER">under</span> <span name="NounPhrase"><span name="JJ">reduced</span> <span name="NN-PRESSURE">pressure</span> </span> </span> </span> </span> <span name="STOP">.</span> </span> <span name="Sentence"><span name="Yield"><span name="NounPhrase"><span name="DT-THE">The</span> <span name="NN-CHEMENTITY">residue</span> </span> <span name="VerbPhrase"><span name="VB-YIELD">obtained</span> </span> </span> <span name="Purify"><span name="VerbPhrase"><span name="VBD">was</span> <span name="VB-PURIFY">purified</span> <span name="PrepPhrase"><span name="IN-BY">by</span> <span name="NounPhrase"><span name="NN-FLASH">flash</span> <span name="NN-CHROMATOGRAPHY">chromatography</span> <span name="MIXTURE"><span name="_-LRB-">(</span> <span name="Solvent"><span name="OSCARCM"><span name="OSCAR-CM">silica</span> </span> <span name="NN-CHEMENTITY">gel</span> </span> <span name="COMMA">,</span> <span name="Solvent"><span name="Quantity"><span name="PERCENT"><span name="CD">0-5</span> <span name="NN-PERCENT">%</span> </span> </span> <span name="OSCARCM"><span name="OSCAR-CM">methanol</span> <span name="DASH">/</span> <span name="OSCAR-CM">methylene</span> <span name="OSCAR-CM">chloride</span> </span> </span> <span name="_-RRB-">)</span> </span> </span> </span> </span> </span> <span name="Yield"><span name="VerbPhrase"><span name="TO">to</span> <span name="VB-YIELD">afford</span> </span> <span name="NounPhrase"><span name="Other"><span name="OSCARCM"><span name="OSCAR-CM">1-(4-methoxybenzyl)piperidin-4-ol</span> </span> <span name="IN-AS">as</span> <span name="DT">a</span> <span name="JJ">brown</span> <span name="NN-STATE">oil</span> <span name="Quantity"><span name="_-LRB-">(</span> <span name="MASS"><span name="CD">1.70</span> <span name="NN-MASS">g</span> </span> <span name="COMMA">,</span> <span name="PERCENT"><span name="CD">80</span> <span name="NN-PERCENT">%</span> </span> <span name="_-RRB-">)</span> </span> </span> </span> </span> <span name="STOP">.</span> </span> </span>                       
    </div>
    <br />    
<div id="content-primary">                      
        <span name='Filter'><asp:CheckBox ID="Filter" runat="server" onclick='highlight("Filter",checked)' Checked="true"/>Filter</span>        
        <span name='Yield'><asp:CheckBox ID="Yield" runat="server" onclick='highlight("Yield",checked)' Checked="true"/>Yield</span>
        <span name='Heat'><asp:CheckBox ID="Heat" runat="server" onclick='highlight("Heat",checked)' Checked="true"/>Heat</span>
        <span name='Wash'><asp:CheckBox ID="Wash" runat="server" onclick='highlight("Wash",checked)' Checked="true"/>Wash</span>
        <span name='Concentrate'><asp:CheckBox ID="Concentrate" runat="server" onclick='highlight("Concentrate",checked)' Checked="true"/>Concentrate</span>
        <span name='Dry'><asp:CheckBox ID="Dry" runat="server" onclick='highlight("Dry",checked)' Checked="true"/>Dry</span>
        <span name='Dissolve'><asp:CheckBox ID="Dissolve" runat="server" onclick='highlight("Dissolve",checked)' Checked="true"/>Dissolve</span>
        <span name='Add'><asp:CheckBox ID="Add" runat="server" onclick='highlight("Add",checked)' Checked="true"/>Add</span>
        <span name='Stir'><asp:CheckBox ID="Stir" runat="server" onclick='highlight("Stir",checked)' Checked="true"/>Stir</span>
        <span name='Purify'><asp:CheckBox ID="Purify" runat="server" onclick='highlight("Purify",checked)' Checked="true"/>Purify</span>
        <span name='Solvent'><asp:CheckBox ID="Solvent" runat="server" onclick='highlight("Solvent",checked)' Checked="true"/>Solvent</span>
</div>

</form>  
</body>
</html>
