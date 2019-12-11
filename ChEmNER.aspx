<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChEmNER.aspx.cs" Inherits="ChEmNER" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1,IE=8" />
    <title>ChEMER - Chemical Entity Extractor</title>
    <link href="IMG/Scope.ico" type="image/x-icon" rel="shortcut icon" />
    <link href="IMG/Scope.ico" type="image/x-icon" rel="icon" />
    <link href="ColoredBox.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        body {
            margin: 8px 8px 8px 8px;
            border: 0;
            width: 99%;
            /**/ background: #fff;
            min-width: 950px;
            height: 100%;
        }

        .header {
            clear: both;
            float: left;
            /*background: #fff;*/
            width: 100%;
            border-top-left-radius: 15px;
            -moz-border-radius-toprleft: 15px;
            border-top-right-radius: 15px;
            -moz-border-radius-topright: 15px;
            height: 100%;
        }

        .menu {
            clear: both;
            position: relative;
            float: left;
            /*background: #fff;*/
            width: 100%;
            z-index: 100;
            height: 100%;
        }

        .container {
            width: 100%;
            height: 100%;
            /*background: #192533;*/
            float: left;
            overflow: hidden;
            min-height: 690px;
            clear: both;
        }

        .col1 {
            float: left;
            width: 100%;
            /*background: #fff;*/
            display: inline;
            height: 100%;
            min-height: 500px;
        }

        .col2 {
            float: left;
            width: 0%;
            /*background: #fff;*/
            height: 100%;
        }

        .col3 {
            float: left;
            width: 100%;
        }

        .col4 {
            float: left;
            width: 0%;
            /*background: #fff;*/
        }

        .footer {
            clear: both;
            float: left;
            /*background: #fff;*/
            width: 100%;
            /*margin-bottom: 4px;*/
            /*border-bottom-left-radius: 15px;
            -moz-border-radius-bottomleft: 15px;
            border-bottom-right-radius: 15px;
            -moz-border-radius-bottomright: 15px;*/
            height: 100%;
        }

        .irfooter {
            /*margin: 8px auto 0;*/
            height: 30px;
            margin-bottom: 10px;
            /*border-top: #cecfce 1px solid;*/
            text-align: right;
            font: 11px arial;
            color: #666;
        }

        #NavContainer {
            background: transparent url('IMG/Yellow.jpg');
            margin: 0;
            width: 100%;
            position: relative;
            float: left;
            /*text-align: right;*/
            height: 33px;
        }

        *.tab1 {
            border-right: #cecfce 1px solid;
            /*background: #fff;*/
            height: 100%;
            min-height: 620px;
        }

        .splitheight {
            /*background: #fff;*/
            height: 100%;
            min-height: 700px;
        }

        .tabstrip {
            padding-left: 0px !important;
            text-align: left !important;
        }

        .RadUpload .ruBrowse {
            background-position: 0 -45px !important;
            height: 24px !important;
            width: 115px !important;
        }
    </style>

    <script type="text/javascript">

        //Disable Backspace
        function cancelBack() {
            if ((event.keyCode == 8 ||
                   (event.keyCode == 37 && event.altKey) ||
                   (event.keyCode == 39 && event.altKey))
                    &&
                   (event.srcElement.form == null || event.srcElement.isTextEdit == false)
                  ) {
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }

        //Prevent Page refresh F5------------------

        document.onkeydown = function () {
            switch (event.keyCode) {
                case 116: //F5 button
                    event.returnValue = false;
                    event.keyCode = 0;
                    return false;
                case 82: //R button
                    if (event.ctrlKey) {
                        event.returnValue = false;
                        event.keyCode = 0;
                        return false;
                    }
            }
        }

    </script>

    <%--    <script type="text/javascript">

        // disable right click----------------------------
        function catch_click(e) {
            if (!e) var e = window.event;

            var right_click = (e.which ? (e.which == 3) : (e.button == 2));

            if (right_click) {
                alert('Right clicking on this page is not allowed.');
                return false;
            }
        }
        document.onmousedown = catch_click;
        if (document.captureEvents) document.captureEvents(Event.MOUSEDOWN);
    </script>--%>

    <script type="text/javascript">
        function center_align(id) {
            //Calculate Page width and height 
            var pageWidth = window.innerWidth;
            var pageHeight = window.innerHeight;
            if (typeof pageWidth != "number") {
                if (document.compatMode == "CSS1Compat") {
                    pageWidth = document.documentElement.clientWidth;
                    pageHeight = document.documentElement.clientHeight;
                } else {
                    pageWidth = document.body.clientWidth;
                    pageHeight = document.body.clientHeight;
                }
            }
            var divobj = document.getElementById(id);
            //For CSS StyleSheet use: 
            if (navigator.appName == "Microsoft Internet Explorer")
                computedStyle = divobj.currentStyle;
            else computedStyle = document.defaultView.getComputedStyle(divobj, null);
            //Get Div width and height from StyleSheet 
            var divWidth = computedStyle.width.replace('px', '');
            var divHeight = computedStyle.height.replace('px', '');
            //For Inline styling use:  
            var divLeft = (pageWidth - parseInt(divWidth)) / 2;
            var divTop = (pageHeight - parseInt(divHeight)) / 2;
            //Set Left and top coordinates for the div tag 
            divobj.style.left = divLeft + "px";
            divobj.style.top = divTop + "px";
        }
    </script>



</head>
<body onkeydown="cancelBack()" onload="goforit();">
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" AsyncPostBackTimeout="36000000"
            ScriptMode="Release" />
        <div class="header">
            <table width="100%" cellpadding="1" cellspacing="0" border="0">
                <tr>
                    <td width="60%" colspan="1" valign="top" style="width: 256px;">
                        <img src="IMG/Chemner-Text2.png" style="-moz-border-top-left-radius: 15px; -moz-border-radius-toprleft: 15px; margin-left: 0px;" />
                    </td>
                    <td width="40%" align="right" valign="top" height="3px">
                        <table border="0">
                            <tr>
                                <td style="color: #666666; float: left; font: 11px arial; margin-top: 1px; margin-bottom: 0px; valign: bottom;">Welcome
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #666666; float: left; font: 12px arial; margin-top: 1px; margin-bottom: 0px; valign: bottom;">
                                    <span id="clock" style="font-size: 12px;"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #666666; float: right; font: 12px arial; margin-top: 1px; margin-bottom: 0px; valign: bottom;">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/IMG/Logout.png"
                                        OnClick="ImageButton1_Click" ToolTip="Logout" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <div id="NavContainer" style="margin-left: 9px">
        </div>

        <div class="container">
            <asp:UpdatePanel ID="Main" runat="server">
                <ContentTemplate>
                    <div class="col1" style="margin-left: 10px; margin-right: 10px;">
                        <div id="Hideall" runat="server">
                           <table style="width:100%;height:100%"><tr><td>
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Width="98%" Height="200px" 
                                Skin="WebBlue">
                                <telerik:RadPane ID="RadPane1" runat="server" Width="100%"  Scrolling="Y">
                                    <div id="Source" align="center" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <table style="width:100%;height:165px">
                                                  
                                                    <tr ><td colspan="3">
                                                      <%--  <asp:TextBox ID="RadTextBox1" runat="server" TextMode="MultiLine" Width="100%" Height="150px" Font-Names="Trebuchet MS" Font-Size="16px"   RenderMode="Lightweight"  EmptyMessage="Enter comment" ToolTip="Enter some text to analyze or browse a file to upload" ></asp:TextBox>--%>
                                                        <telerik:RadTextBox ID="RadTextBox1" runat="server" Skin="Telerik" 
                                                    Height="150px" Width="98%" Font-Names="Trebuchet MS" Font-Size="16px" ToolTip="Enter some text to analyze or browse a file to upload"
                                                    ForeColor="#3A3E6B" EmptyMessage="Enter some text to analyze or browse a file to upload" Style="margin-left: 5px" Wrap="true" Rows="50" TextMode="MultiLine">
                                                </telerik:RadTextBox>

                                                         </td>
                                                     
                                                    </tr>
                                                      <tr>
                                          
                                                    <td> <%--<telerik:RadButton ID="RadButton2" runat="server" OnClick="RadButton2_Click" Text="Upload"
                                                    Style="margin-left: 10px" Font-Names="Trebuchet MS" Font-Size="16px" Skin="Telerik" Width="80px" Visible="false">
                                                </telerik:RadButton>--%>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<telerik:RadButton ID="RadButton3" runat="server" Text="Run ChEmER"
                                                    OnClick="RadButton3_Click" Width="120px" Style="margin-left: 538px"
                                                    Font-Names="Trebuchet MS" Font-Size="16px"
                                                    Skin="Telerik">
                                                </telerik:RadButton></td>
                                                         <td><asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                    <ProgressTemplate>

                                                        <img alt="progress" height="100" src="IMG/Processing1.gif" width="100" style="margin-left: 10px; height: 31px; width: 32px;" />
                                                        <asp:Label ID="lblProcessing" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="#3A3E6B"
                                                            Text="" Font-Names="Trebuchet MS"></asp:Label>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress></td>
                                                    </tr>
                                                </table>
                                               
                                            </ContentTemplate>
                                            <Triggers>
                                             <%--   <asp:PostBackTrigger ControlID="RadButton2" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                                </td>
                                                                 </tr>
                               <tr><td>
                                   <div id="divsplitter" runat="server" >
                                  <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="98%" Height="500px" Skin="WebBlue">
                                <telerik:RadPane ID="LeftPane" runat="server" Width="30%"  Scrolling="Y">
                                    
                                    <div id="Div11" style="margin: 0 auto;">
                                          <asp:Label ID="Label4" Text="  Tagged Output" runat="server" Font-Names="Trebuchet MS" Font-Size="14px" Width="100%" Style="margin-left: 0px" Visible="true" ForeColor="White" BackColor="#FF9933"></asp:Label>
                                               <asp:Label ID="Label12" runat="server" Font-Names="Trebuchet MS" Font-Size="14px" Width="95%" Style="margin-left: 10px" Visible="False" ForeColor="White"></asp:Label>
                                                   
                                        </div>
                                    </telerik:RadPane>
                                   
                                    <telerik:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Both" Height="100%">
                                </telerik:RadSplitBar>
                            
                                <telerik:RadPane ID="MiddlePane1" runat="server" Width="35%"  Scrolling="Y">
                                    
                                    <div id="Div2" style="margin: 0 auto;">
                                        <asp:Label ID="Label5" Text="" runat="server" Font-Names="Trebuchet MS" Font-Size="14px" Width="100%" Style="margin-left: 0px" Visible="true" ForeColor="White" BackColor="#FF9933"></asp:Label>
                                        <telerik:RadGrid ID="RadGrid2" runat="server" AutoGenerateColumns="true"
                                            Skin="Telerik" CellSpacing="0" GridLines="None" Visible="false" Font-Size="12px" Font-Names="Trebuchet MS">
                                            <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                            <SelectedItemStyle />
                                            <MasterTableView AutoGenerateColumns="true" NoMasterRecordsText="Empty">
                                                <%--  <Columns>
                                                <telerik:GridTemplateColumn Display="true" HeaderText="Slno">                                                                            
                                                    <ItemTemplate>
                                                        <%# Container.DataSetIndex+1 %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Chemical Entity" AllowFiltering="true" DataField="NAME" HeaderStyle-Font-Size="16px"
                                                        >
                                                        <ItemTemplate>
                                                            <asp:Label ID="Filename" runat="server" Text='<%# bind("NAME") %>' Font-Names="Trebuchet MS" Font-Size="16px"></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>                                                  
                                                </Columns>--%>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </telerik:RadPane>
                                <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Both" Height="100%">
                                </telerik:RadSplitBar>
                                <telerik:RadPane ID="RightPane1" runat="server" Width="35%" Scrolling="Y" >
                                    
                                    <div id="Div3" style="margin: 0 auto;">
                                        <asp:Label ID="Label6" Text="  Procedural Steps" runat="server" Font-Names="Trebuchet MS" Font-Size="14px" Width="100%" Style="margin-left: 0px" Visible="true" ForeColor="White" BackColor="#FF9933"></asp:Label>
                                         <asp:Label ID="Label2" runat="server" Font-Names="Trebuchet MS" Font-Size="14px" Width="95%" Style="margin-left: 10px" ForeColor="White"></asp:Label>
                                        </div>
                                </telerik:RadPane>
                                
                            </telerik:RadSplitter>
                                   </div> </td></tr></table>
                        </div>
                        <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <%--    <div align="center">
                            <br />
                            <telerik:RadButton ID="RadDraft" runat="server" Text="Submit" Style="margin-left: 30px"
                                Skin="Telerik" Font-Names="Trebuchet MS" Font-Size="11px" Width="80px" OnClick="RadDraft_Click">
                            </telerik:RadButton>
                            <telerik:RadButton ID="RadButton1" runat="server" Text="Next" Style="margin-left: 30px"
                                Skin="Telerik" Font-Names="Trebuchet MS" Font-Size="11px" Width="80px" OnClick="RadButton1_Click"
                                ValidationGroup="Comments">
                            </telerik:RadButton>
                        </div>--%>
                    </div>
                    <div class="col2"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="footer">
            <div class="irfooter">
                <p style="font-family: Segoe UI, Trebuchet MS, Arial,Verdana; font-size: 14px;">
                    Copyright © 2016 Scope E-Knowledge Center
                </p>
            </div>
        </div>
        <!-- javascript ================================================== -->
        <script src="ChEmNER/jquery.js" type="text/javascript"></script>
        <script src="ChEmNER/time.js" type="text/javascript"></script>
        <script src="ChEmNER/backstretch.min.js" type="text/javascript"></script>
        <script src="ChEmNER/login.js" type="text/javascript"></script>
    </form>
</body>
</html>
