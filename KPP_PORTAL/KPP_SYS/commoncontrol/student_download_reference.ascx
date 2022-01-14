<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_download_reference.ascx.vb" Inherits="KPP_SYS.student_download_reference" %>

<script type="text/javascript">
    function ShowMessage(message, messagetype) {
        var cssclass;
        switch (messagetype) {
            case 'Success':
                cssclass = 'alert-success'
                break;
            case 'Error':
                cssclass = 'alert-danger'
                break;
            default:
                cssclass = 'alert-info'
        }
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; text-align:left -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        setTimeout(function () {
            $("#alert_div").fadeTo(5000, 500).slideUp(500, function () {
                $("#alert_div").remove();
            });
        }, 3000);
    }
</script>

<script>
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#blah').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#uploadPhoto").change(function () {
        readURL(this);
    });
</script>

<style>
    .messagealert {
        width: 40%;
        position: fixed;
        bottom: 25px;
        right: 0px;
        z-index: 100000;
        padding: 0;
        font-size: 15px;
    }

    .sc3::-webkit-scrollbar {
        height: 10px;
    }

    .sc3::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc3::-webkit-scrollbar-thumb {
        background-color: #929B9E;
        border-radius: 3px;
    }

    .sc4::-webkit-scrollbar {
        width: 10px;
    }

    .sc4::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc4::-webkit-scrollbar-thumb {
        background-color: #929B9E;
    }
</style>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp References &nbsp / &nbsp 
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">

    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnDownloadReferences" runat="server" style="display: inline-block; font-size: 0.8vw">Download Reference </button>
        <button id="btnUploadReferences" runat="server" style="display: inline-block; font-size: 0.8vw">Upload Reference </button>
        <button id="btnViewInformation" runat="server" style="display: inline-block; font-size: 0.8vw">View Information </button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="DownloadReference" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LA" runat="server">
            <button id="btnDownloadF1_LA" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LB" runat="server">
            <button id="btnDownloadF1_LB" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LC" runat="server">
            <button id="btnDownloadF1_LC" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LD" runat="server">
            <button id="btnDownloadF1_LD" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LD1" runat="server">
            <button id="btnDownloadF1_LD1" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LE" runat="server">
            <button id="btnDownloadF1_LE" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LE1" runat="server">
            <button id="btnDownloadF1_LE1" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LE2" runat="server">
            <button id="btnDownloadF1_LE2" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LE3" runat="server">
            <button id="btnDownloadF1_LE3" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LF" runat="server">
            <button id="btnDownloadF1_LF" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LF1" runat="server">
            <button id="btnDownloadF1_LF1" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="F1_LSS" runat="server">
            <button id="btnDownloadF1_LSS" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="LA" runat="server">
            <button id="btnDownloadLA" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="LA1" runat="server">
            <button id="btnDownloadLA1" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="LA2" runat="server">
            <button id="btnDownloadLA2" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="LA3" runat="server">
            <button id="btnDownloadLA3" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="LB" runat="server">
            <button id="btnDownloadLB" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="LB1" runat="server">
            <button id="btnDownloadLB1" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh" id="LC" runat="server">
            <button id="btnDownloadLC" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw; color: blue" class="btn btn-default font"></button>
        </div>

        
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="UploadReference" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Payment Type </asp:Label>
            &nbsp; : &nbsp; &nbsp; &nbsp;
            <asp:RadioButton ID="rbtn_FullPayment" Text="&nbsp; Full Payment &nbsp;&nbsp;&nbsp;" class="font" runat="server" GroupName="PT" AutoPostBack="true" />
            <asp:RadioButton ID="rbtn_Installment" Text="&nbsp; Installment &nbsp;&nbsp;&nbsp;" runat="server" class="font" GroupName="PT" AutoPostBack="true" />
            <asp:RadioButton ID="rbtn_Scholarship" Text="&nbsp; Sponsorship" runat="server" class="font" GroupName="PT" AutoPostBack="true" />
        </div>

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr runat="server" id="R1">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Academic Fee Receipt </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImportRAF" runat="server" filename="" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImportRAF" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImportRAF" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="R2">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Muafakat Fee Receipt </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImportRMF" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImportRMF" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImportRMF" Style="display: inline-block"></asp:RegularExpressionValidator>

                </td>
            </tr>
            <tr runat="server" id="R3">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Attachment A </asp:Label></td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;                    
                <asp:FileUpload ID="FileUploadImportAA" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImportAA" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImportAA" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="R4">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Attachment A3 </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;                    
                    <asp:FileUpload ID="FileUploadImportAA3" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImportAA3" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImportAA3" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="R5">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Parents' Request Letter </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;                   
                    <asp:FileUpload ID="FileUploadImportPAL" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImportPAL" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImportPAL" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="R6">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Father's Payslip </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImportFP" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImportFP" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImportFP" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="R7">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Mother's Payslip </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImportMP" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImportMP" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImportMP" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="F1_R1">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Academic Fee Receipt </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImport_F1RAF" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImport_F1RAF" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImport_F1RAF" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="F1_R2">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Muafakat Fee Receipt </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImport_F1RMF" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImport_F1RMF" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImport_F1RMF" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="F1_R3">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Attachment E </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImport_F1AE" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImport_F1AE" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImport_F1AE" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="F1_R4">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%">Attachment E3 </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImport_F1AE3" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImport_F1AE3" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImport_F1AE3" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="F1_R5">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Parents’ Request Letter </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImport_F1PAL" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImport_F1PAL" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImport_F1PAL" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="F1_R6">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Father’s Payslip </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImport_F1FP" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImport_F1FP" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImport_F1FP" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="F1_R7">
                <td>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Mother's Payslip </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp :  &nbsp; &nbsp; &nbsp;
                    <asp:FileUpload ID="FileUploadImport_F1MP" runat="server" class="btn btn-default font" Style="display: inline-block; font-size: 0.8vw; color: black" onchange="readURL(this)" />
                    <asp:RegularExpressionValidator ID="regexValidatorImport_F1MP" runat="server" ErrorMessage="Only PDF file are allowed" ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FileUploadImport_F1MP" Style="display: inline-block"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 3vh">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%; color: red"><b> *** Notes : Please Upload Documents In PDF Format Only *** </b></asp:Label>
            <br />
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%; color: red"><b> *** Notes : Please Do Not Used Symbol &nbsp;' &nbsp; Or &nbsp; " &nbsp;  On File Name *** </b></asp:Label>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <p></p>
            <button id="btnUploadDocuments" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Upload Reference Information</button>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="ViewInformation" runat="server" class="sc4">
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlView_Year" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="FDStudent_id" BorderStyle="None" GridLines="None"
                Width="97%" HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="FDStudent_Year" class="id1" runat="server" Text='<%# Eval("FDStudent_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date & Time" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="FDStudent_DateTime" class="id1" runat="server" Text='<%# Eval("FDStudent_DateTime") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reference Name" ItemStyle-Width="70%">
                        <ItemTemplate>
                            <asp:Label ID="FDStudent_Name" class="id1" runat="server" Text='<%# Eval("FDStudent_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Reference Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>


</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
