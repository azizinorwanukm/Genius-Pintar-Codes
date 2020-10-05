<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_detail_disiplin.aspx.vb" Inherits="KPP_MS.admin_detail_disiplin" %>

<%@ Register Src="~/commoncontrol/Disiplin_Case_Detail.ascx" TagPrefix="uc1" TagName="Disiplin_Case_Detail" %>
<%@ Register Src="~/commoncontrol/Disiplin_Letter_List.ascx" TagPrefix="uc1" TagName="Disiplin_Letter_List" %>
<%@ Register Src="~/commoncontrol/Disiplin_Warning_Letter_Form.ascx" TagPrefix="uc1" TagName="Disiplin_Warning_Letter_Form" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

    <asp:MultiView runat="server" ID="disiplinMultiView">
        <asp:View ID="caseDetailView" runat="server">
            <uc1:Disiplin_Case_Detail runat="server" id="Disiplin_Case_Detail" />
        </asp:View>
        <asp:View ID="warningLetterList" runat="server">
            <uc1:Disiplin_Letter_List runat="server" id="Disiplin_Letter_List" />
        </asp:View>
        <asp:View ID="warningLetterForm" runat="server">
            <uc1:Disiplin_Warning_Letter_Form runat="server" id="Disiplin_Warning_Letter_Form" />
        </asp:View>
    </asp:MultiView>
    <div class="messagealert" id="alert_container" style="text-align: center"></div>
</asp:Content>
