<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Cube" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>ChEmER</title>
    <link href="IMG/Scope.ico" type="image/x-icon" rel="shortcut icon" />
    <link href="IMG/Scope.ico" type="image/x-icon" rel="icon" />
    <link href="CUBECSS/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="CUBECSS/login.css" rel="stylesheet" type="text/css" />
    <link href="CUBECSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="CUBECSS/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="CUBECSS/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="CUBECSS/cubeportfolio.css" rel="stylesheet" type="text/css" />
    <link href="CUBECSS/iubenda.css" rel="stylesheet" type="text/css" />
    <link href="CUBECSS/styleall.css" rel="stylesheet" type="text/css" />
    <script src="CUBECSS/js_jquery.min.js" type="text/javascript"></script>
    <script src="CUBECSS/ie10-viewport-bug-workaround.js" type="text/javascript"></script>
    <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->
    <!-- javascript ==================================================Start-->
    <script language="JavaScript" type="text/javascript">

        //--------Only Numbers-----
        function fnOnlyNum(evt) {

            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode >= 48 && charCode <= 57) {
                return true;
            }
            else {
                return false;
            }
        }

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

        //---Textbox WaterMark-----------------

        function WaterMark(objtxt, event) {
            var defaultText = "Enter Name Here";
            var defaultpwdText = "Enter Password Here";
            if (objtxt.id == "txtempid" || objtxt.id == "txtPwd") {
                if (objtxt.value.length == 0 & event.type == "blur") {
                    if (objtxt.id == "txtempid") {
                        objtxt.value = defaultText;
                    }

                    if (objtxt.id == "txtPwd") {
                        document.getElementById("<%= txtTempPwd.ClientID %>").style.display = "block";
                        objtxt.style.display = "none";
                    }
                }
            }

            if ((objtxt.value == defaultText || objtxt.value == defaultpwdText) & event.type == "focus") {
                if (objtxt.id == "txtempid") {
                    objtxt.value = "";
                }

                if (objtxt.id == "txtTempPwd") {
                    objtxt.style.display = "none";
                    document.getElementById("<%= txtPwd.ClientID %>").style.display = "";
                    document.getElementById("<%= txtPwd.ClientID %>").focus();
                }
            }
        }

        //---ValidateCredentials--------------------

        function ValidateCredentials() {
            if (document.getElementById('txtempid').value == "Enter Name Here") {
                document.getElementById('txtempid').focus();
                alert("Enter Name Here");
                return (false);
            }

            if (document.getElementById('txtPwd').value == "Enter Password Here" || document.getElementById('txtPwd').value == "") {
                //document.getElementById('txtPwd').focus();
                alert("Enter Password Here");
                return (false);
            }          
         
        }     

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="container messages-container">
        <div class="row-fluid">
            <div class="col-xs-12">
            </div>
        </div>
    </div>
    <div class="demo-1">
        <div class="content">
            <div id="large-header" class="large-header">
                <canvas id="demo-canvas" class="hidden-xs hidden-sm"></canvas>
                <h1 class="main-title">
                    <%--<span class="thin">ChEmNER</span>--%></h1>
                <div class="main-tagline text">
                    <div class="content">                                            
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="container">
                                    <div id="login-wraper">                                                                        
                                        <legend style="color: White; font-family: Verdana; font-size: 16px">Sign In : </legend>
                                        <div class="body">
                                            <div id="First" align="center">
                                                <asp:TextBox ID="txtempid" autocomplete="off" runat="server" AutoCompleteType="Disabled"
                                                    onblur="WaterMark(this, event); " onfocus="WaterMark(this, event);" Text="Enter Name Here"
                                                    ToolTip="Enter Name Here" />
                                            </div>
                                            <div id="Second" align="center">
                                                <asp:TextBox ID="txtTempPwd" runat="server" autocomplete="off" AutoCompleteType="Disabled"
                                                    onfocus="WaterMark(this, event);" Text="Enter Password Here" ToolTip="Enter Password Here"></asp:TextBox>
                                                <asp:TextBox ID="txtPwd" runat="server" autocomplete="off" AutoCompleteType="Disabled"
                                                    onblur="WaterMark(this, event);" Style="display: none" Text="Enter Password Here"
                                                    TextMode="Password"></asp:TextBox>
                                            </div>
                                            <div id="Four" align="center" class="footer">
                                                <asp:Button ID="btnSubmit" CssClass="btn-primary" EnableViewState="true" Style="margin-bottom: 20px !important;
                                                    width: 100px;" runat="server" Text="LogIn" OnClick="Btnloginsubmit_Click" OnClientClick="return ValidateCredentials()" /><br />
                                                <asp:Label ID="lblWarning" runat="server" Text="Invalid Username or Password" Style="display: none;"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    
                </div>
            </div>
        </div>
    </div>
   <%-- <div class="blk" id="footer" style="padding-top: 5px;">
        <div class="container">
            <div class="row">
                <div class="col-sm-12 ">
                    <div class="centered text">
                        <div class="content">
                            <strong>ChEmNER</strong> - <strong>Recognition of CHEMICAL ENTITIES in Text</strong>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 centered">
                </div>
            </div>
        </div>
    </div>--%>
    <script src="CUBESCRIPT/bootstrap.min.js" type="text/javascript"></script>
    <script src="CUBESCRIPT/retina-1.1.0.js" type="text/javascript"></script>
    <script src="CUBESCRIPT/TweenLite.min.js" type="text/javascript"></script>
    <script src="CUBESCRIPT/EasePack.min.js" type="text/javascript"></script>
    <script src="CUBESCRIPT/rAF.js" type="text/javascript"></script>
    <script src="CUBESCRIPT/demo-1.js" type="text/javascript"></script>
    <script src="CUBESCRIPT/jquery.cubeportfolio.js" type="text/javascript"></script>
    <script src="CUBESCRIPT/theme.js" type="text/javascript"></script>
    </form>
</body>
</html>
