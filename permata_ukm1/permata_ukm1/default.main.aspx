<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.main.Master"
    CodeBehind="default.main.aspx.vb" Inherits="permatapintar.default_main" %>

<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="jquery-ui.css" />
    <script type="text/javascript" src="jquery-1.10.2.js"></script>
    <script type="text/javascript" src="jquery-ui.js"></script>


    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-47831317-1', 'permatapintar.edu.my');
        ga('send', 'pageview');

        $(function () {
            $("#dialog-message").dialog({
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });
        });

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblMsgTop" runat="server" ForeColor="Red" Text=""></asp:Label>
    &nbsp;<uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc2:studentschool_view ID="studentschool_view1" runat="server" />
    &nbsp;<uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="fbbutton" />
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Anda dikehendaki mengemaskini Maklumat Pelajar, Maklumat Sekolah dan Ibubapa/Penjaga."></asp:Label>
    </div>

    <div id="dialog-message" title="PERHATIAN">
        <p>UJIAN UKM1</p>
        <p>
            Kepada pelajar yang bertukar sekolah, sila KEMASKINI maklumat sekolah anda bagi memudahkan pihak PERMATApintar untuk menghubungi anda jika anda layak ke ujian seterusnya.
        </p>
    </div>

</asp:Content>
