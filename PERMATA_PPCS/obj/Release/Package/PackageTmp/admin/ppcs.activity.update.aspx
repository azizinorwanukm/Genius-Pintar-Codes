<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="ppcs.activity.update.aspx.vb" Inherits="permatapintar.ppcs_activity_update" %>
<%@ Register src="../commoncontrol/activity.list.ascx" tagname="activity" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr>
            <td>
                Kemaskini Aktiviti ID
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini Aktiviti " CssClass="fbbutton" />
                &nbsp; &nbsp;
            </td>
        </tr>
                <tr>
            <td>
                <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>

    </table>

    <uc1:activity ID="activity1" runat="server" />
</asp:Content>
