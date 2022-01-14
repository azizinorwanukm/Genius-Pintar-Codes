<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_Update.ascx.vb" Inherits="KPP_SYS.Disiplin_Update" %>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
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
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Insert New Disiplinary Action Detail</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <!-- Student Ic Number -->
                <asp:Label CssClass="Label" runat="server"> Search Student : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="student_Mykad" Style="width: 80%; border-radius: 25px;" runat="server" Text="" Placeholder="Student IC Number without '-' / Student ID "></asp:TextBox>
                <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 15px;" title="Search"><i class="fa fa-search w3-medium w3-text-white"></i></button>
                <!-- Student Id Hidden -->
                <asp:TextBox CssClass="textbox" class="form-control" ID="student_id" Style="width: 50%; border-radius: 25px;" runat="server" Text="" Visible="false"></asp:TextBox>
                <!-- Student Id Hidden -->
                <asp:TextBox CssClass="textbox" class="form-control" ID="Hidden_IC" Style="width: 50%; border-radius: 25px;" runat="server" Text="" Visible="false"></asp:TextBox>
            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!-- Date And Time -->
                <asp:Label CssClass="Label" runat="server"> Date : <i class="fa fa-calendar w3-medium w3-text-black"></i></asp:Label>
                <asp:TextBox CssClass="ddl datepicker" ID="CurrentDate" runat="server" />
            </div>
        </div>

        <div style="display: none" id="std_details">
            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <!-- Student Name -->
                    <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Name : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="Student_name" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <!--student Kelas-->
                    <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Class : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="Student_class" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!--disiplin drop down -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Diciplinary Type : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlDiciplinetype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="disiplin_onselectedindexchanged" CssClass="btn btn-default font ddl" Style="width: 60%"></asp:DropDownList>
            </div>
        </div>

        <div style="display: none" id="dis_type">
            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px;">
                <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                    <p></p>
                    <!--Merit Point -->
                    <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Demerit (Points) : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="Merit" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                    <p></p>
                    <!--Compound-->
                    <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Compound (RM) : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="student_compound" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                    <p></p>
                    <!--checkbox untuk bill -->
                    <asp:Label CssClass="Label" runat="server" Style="width: 20%">Bill :</asp:Label>
                    <asp:CheckBox CssClass="textbox" class="form-control" ID="Checkbox2" runat="server" />
                </div>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <!--id pelapor -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Complainant id  : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Pelapor_id" Style="width: 80%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
                <button id="searchComplainant" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 15px;" title="Search"><i class="fa fa-search w3-medium w3-text-white"></i></button>
            </div>

            <div style="display: none" id="stf_details">
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px;">
                    <!-- Nama Pelapor -->
                    <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Complainant Name : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="Person_charge" Style="width: 100%; border-radius: 25px; width: 80%" runat="server" Text=""></asp:TextBox>
                </div>
            </div>
    </div>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-8 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <!-- Detail Case -->
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Detail Case : </asp:Label>
            <asp:TextBox ID="Detail_case" runat="server" TextMode="Multiline" class="form-control" Height="90" Style="width: 100%; border-radius: 25px"></asp:TextBox>
        </div>
    </div>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-8 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <!--Action Box -->
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Action : </asp:Label>
            <asp:TextBox ID="Action_box" runat="server" TextMode="Multiline" Class="form-control" Height="90" Style="width: 100%; border-radius: 25px;"></asp:TextBox>
        </div>
    </div>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;"><i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
</div>
</div>
