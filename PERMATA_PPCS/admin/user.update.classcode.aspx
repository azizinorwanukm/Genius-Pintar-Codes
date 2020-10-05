<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="user.update.classcode.aspx.vb" Inherits="permatapintar.user_update_classcode" %>

<%@ Register Src="../commoncontrol/admin_ppcsusers_view.ascx" TagName="admin_ppcsusers_view"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform" border="0px">
        <tr class="fbform_header">
            <td colspan="2">
                Pengurusan Am>Menentukan
                <asp:Label ID="lblUserType" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
                untuk Kelas:
                <asp:Label ID="lblClassNameBM" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>&nbsp;
                Sessi PPCS:<asp:Label ID="lblPPCSDate" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:admin_ppcsusers_view ID="admin_ppcsusers_view1" runat="server" />
    <table class="fbform" border="0px">
        <tr style="text-align: left">
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnOK" runat="server" Text=" Confirm " CssClass="fbbutton" />
                &nbsp; &nbsp;<asp:Button ID="btnCancel" runat="server" Text="  Back " CssClass="fbbutton" />
            </td>
        </tr>
    </table>
</asp:Content>
