<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_Update.ascx.vb" Inherits="KPP_MS.Disiplin_Update" %>
<%@ Register Assembly="TextboxioControl" Namespace="TextboxioControl" TagPrefix="textboxio" %>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="textboxio/textboxio.js"></script>
    <script>
        $(function () {
            $('.datepicker').datepicker({ dateFormat: 'dd MM yy' }).val();
        });
    </script>
</head>

<style type="text/css">
    .ddl {
        border-radius: 25px;
    }

    .CalendarCssClass {
        background-color: #990000;
        font-family: Century;
        text-transform: lowercase;
        width: 750px;
        border: 1px solid Olive;
    }
</style>

<script type="text/javascript">

    $(document).ready(function () {
        var acc1 = document.getElementById('<%= HiddenField1.ClientID %>').value;
        var acc2 = document.getElementById('<%= HiddenField2.ClientID %>').value;
        var acc3 = document.getElementById('<%= HiddenField3.ClientID %>').value;

        if (acc1 == "block") {
            document.getElementById("std_details").style.display = "block";
        }

        if (acc2 == "block") {
            document.getElementById("dis_type").style.display = "block";
        }
        if (acc3 == "block") {
            document.getElementById("stf_details").style.display = "block";
        }

    });

    function date_Click() {
        if (divcalendar.style.display == "none")
            divcalendar.style.display = "";
        else
            divcalendar.style.display = "none";
    }
</script>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
<asp:HiddenField ID="HiddenField1" runat="server" />
<asp:HiddenField ID="HiddenField2" runat="server" />
<asp:HiddenField ID="HiddenField3" runat="server" />

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Insert New Disciplinary Action Detail</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
            <div class="col-md-8 w3-text-black" style="text-align: left; padding-left: 23px">
                <!-- Student Ic Number -->
                <asp:Label CssClass="Label" runat="server"> Search Student : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="student_Mykad" Style="width: 53%; border-radius: 25px;" runat="server" Text="" Placeholder="Mykad Number without '-' / Student ID "></asp:TextBox>
                <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 15px;" title="Search">Search &#160;<i class="fa fa-search w3-medium w3-text-white"></i></button>
            </div>

            <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 10px">
                <!-- Date And Time -->
                <asp:Label CssClass="Label" runat="server"> Date : </asp:Label>
                <asp:TextBox CssClass="ddl datepicker" ID="CurrentDate" runat="server" />
                &#160; <i class="fa fa-calendar w3-medium w3-text-black"></i>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
            <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                <!-- Student Name -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Name : </asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="StudentNameLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
            </div>
        </div>
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 2px;">
            <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                <!--student Kelas-->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student ID :</asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="StudentIDLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
            </div>
        </div>
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 2px;">
            <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                <!--student Kelas-->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Class Name :</asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="StudentClassLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
            <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                <!--disiplin drop down -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Disciplinary Category : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlDiciplinetype" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl"></asp:DropDownList>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <!-- Detail Case -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Detail Case : </asp:Label>
                <asp:TextBox ID="Detail_case" runat="server" TextMode="Multiline" class="form-control" Height="90" Style="width: 100%; border-radius: 25px"></asp:TextBox>
            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 10px">
                <!--Action Box -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Action : </asp:Label>
                <asp:DropDownList ID="ddlActionType" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
                <textboxio:Textboxio runat="server" Visible="false" ID="txtLetterContent" Content="Write letter content here..." />
            </div>
        </div>


        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
            <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                <!--Counseling-->
                <asp:Label CssClass="Label" runat="server" Style="color: black;">Need Counseling Guidance? : </asp:Label>
                <asp:CheckBox ID="needCounseling" AutoPostBack="true" runat="server" Checked="false" />
            </div>
        </div>

        <div id="showCounselingDiv" visible="false" runat="server">
            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <asp:DropDownList runat="server" ID="ddlCounselingStaff" AutoPostBack="false" Style="width: 100%; border-radius: 25px;" CssClass="ddl btn btn-default"></asp:DropDownList>
                </div>
            </div>

            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <asp:Label CssClass="Label" runat="server"> Counseling Date : </asp:Label>
                    <asp:TextBox CssClass="ddl datepicker" ID="CounselingDate" runat="server" Width="60%" />&#160; <i class="fa fa-calendar w3-medium w3-text-black"></i>
                </div>
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 10px">
                    <asp:Label CssClass="Label" runat="server"> Time : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtstart_time" Style="width: 40%; border-radius: 25px;" runat="server" Placeholder=" Ex : 08:45 AM "></asp:TextBox>
                    <asp:Label CssClass="Label" runat="server"> To : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtend_time" Style="width: 40%; border-radius: 25px;" runat="server" Placeholder=" Ex : 03:45 PM "></asp:TextBox>
                </div>
            </div>

            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
                <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                    <!--Code Session-->
                    <asp:Label CssClass="Label" runat="server">Code Session : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>&nbsp;
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtcode_session" Style="width: 25%; border-radius: 25px;" runat="server" Placeholder=" EX: BI999 / BK999"></asp:TextBox>
                </div>
                <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px;margin-top:5px">
                    <!--Client Classification-->
                    <asp:Label CssClass="Label" runat="server">Client Classification : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>&nbsp;
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtclient_classification" Style="width: 25%; border-radius: 25px;" runat="server" Placeholder=" EX: PP / PB"></asp:TextBox>
                </div>
                <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px;margin-top:5px">
                    <!--Type of Counselling-->
                    <asp:Label CssClass="Label" runat="server">Type of Interview : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>&nbsp;
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txttype_interview" Style="width: 25%; border-radius: 25px;" runat="server" Placeholder=" EX: B / PS / MA / MK"></asp:TextBox>
                </div>
            </div>
        </div>


        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
            <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
            <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
        </div>
    </div>
</div>
