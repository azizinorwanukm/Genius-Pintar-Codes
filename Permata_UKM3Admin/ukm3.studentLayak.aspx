<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3.studentLayak.aspx.vb" Inherits="permatapintar.ukm3_studentLayak" %>

<%@ Register Src="~/commoncontrol/ukm3_studentLayak.ascx" TagPrefix="uc1" TagName="ukm3_studentLayak" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Pelajar>Senarai Pelajar"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:ukm3_studentLayak runat="server" ID="ukm3_studentLayak" />
</asp:Content>
