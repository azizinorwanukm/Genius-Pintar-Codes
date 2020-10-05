<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.default.aspx.vb" Inherits="permatapintar.ukm2_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    
    <h1>Araken I-PROFILE</h1>

    <p class="moreline">
        &nbsp;
    </p>
    <p class="contant1">
        <asp:Label ID="ukm2_default_01" runat="server" Text=""></asp:Label>
    </p>
    <p class="moreline">
        &nbsp;
    </p>
    <table width="100%" style="border: none;">
        <tr>
            <td colspan="2">
                <h2>
                    <asp:Label ID="ukm2_default_02" runat="server" Text=""></asp:Label></h2>
            </td>
        </tr>
        <tr>
            <td style="width: 15%;">
                <asp:Label ID="ukm2_default_03" runat="server" Text=""></asp:Label>
            </td>
            <td>:<asp:Label ID="lblMYKAD" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ukm2_default_04" runat="server" Text=""></asp:Label>
            </td>
            <td>:<asp:Label ID="lblStudentFullname" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ukm2_default_05" runat="server" Text=""></asp:Label>
            </td>
            <td>:<asp:Label ID="lblStudentForm" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ukm2_default_06" runat="server" Text=""></asp:Label>
            </td>
            <td>:<asp:Label ID="lblSchoolName" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ukm2_default_07" runat="server" Text=""></asp:Label>
            </td>
            <td>:<asp:Label ID="lblSchoolCity" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ukm2_default_08" runat="server" Text=""></asp:Label>
            </td>
            <td>:<asp:Label ID="lblSchoolState" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ukm2_default_11" runat="server" Text=""></asp:Label>
            </td>
            <td>:<asp:Label ID="lblSelectedLang" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Start Date Time</td>
            <td>
                :<asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnStart" runat="server" Text="Mula >>" CssClass="mybutton" />&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>


</asp:Content>
