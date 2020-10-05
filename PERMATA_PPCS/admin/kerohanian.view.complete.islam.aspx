<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="kerohanian.view.complete.islam.aspx.vb" Inherits="permatapintar.kerohanian_view_complete_islam5" %>

<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="ukm2" TagPrefix="uc1" %>
<%@ Register Src="../questionscontrol/spiritual.muslim.01.ascx" TagName="kerohanian"
    TagPrefix="uc2" %>
<%@ Register Src="../questionscontrol/spiritual.muslim.02.ascx" TagName="kerohanian"
    TagPrefix="uc3" %>
<%@ Register Src="../questionscontrol/spiritual.muslim.03.ascx" TagName="kerohanian"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Soalselidik Kerohanian (View only)
            </td>
        </tr>
    </table>
    <uc1:ukm2 ID="ukm21" runat="server" />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <b>Mukasurat 1</b>
    <uc2:kerohanian ID="kerohanian1" runat="server" />
    <b>Mukasurat 2</b>
    <uc3:kerohanian ID="kerohanian2" runat="server" />
    <b>Mukasurat 3</b>
    <uc4:kerohanian ID="kerohanian3" runat="server" />
</asp:Content>
