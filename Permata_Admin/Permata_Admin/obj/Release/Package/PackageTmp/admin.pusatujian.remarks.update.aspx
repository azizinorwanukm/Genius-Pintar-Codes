<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pusatujian.remarks.update.aspx.vb" Inherits="permatapintar.admin_pusatujian_remarks_update" %>

<%@ Register Src="commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td style="vertical-align: top;">
                Remarks:
            </td>
            <td>
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="10" Width="600px"></asp:TextBox>
            </td>
        </tr>
        <tr>
    <td class="fbform_sap" colspan="2">
        <asp:Label ID="lblMsg" runat="server" Text="" CssClass="fblabel_msg" ></asp:Label>
    </td>
</tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />
            </td>
        </tr>
    </table>
   
</asp:Content>
