<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="spiritual.view.complete.others.aspx.vb" Inherits="permatapintar.spiritual_view_complete_others" %>

<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="ukm2" TagPrefix="uc1" %>
<%@ Register Src="../questionscontrol/spiritual.others.01.ascx" TagName="kerohanian"
    TagPrefix="uc2" %>
<%@ Register Src="../questionscontrol/spiritual.others.02.ascx" TagName="kerohanian"
    TagPrefix="uc3" %>
<%@ Register Src="../questionscontrol/spiritual.others.03.ascx" TagName="kerohanian"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr>
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