<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.course.01.aspx.vb" Inherits="permatapintar.ukm2_course_01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table class="tableform">
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 5%;">
                &nbsp;
            </td>
            <td colspan="2">
                <%--<a href="#">
                    <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
                        height="35" border="0" class="logo" /></a>--%>
                <h1>Araken I-PROFILE</h1>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="vertical-align: top;" colspan="2">
                <h2><asp:Label ID="lblcourse_header" runat="server" Text="Program Perkhemahan Cuti Sekolah"
                        CssClass="lblHeader"></asp:Label></h2>
                <asp:Label ID="lblcourse_instruction" runat="server" Text="Pilih modul-modul mengikut susunan keutamaan minat anda."></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblcourse_scale" runat="server" Text="Skala"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <asp:Label ID="lblcourse_rate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 5%;">
                &nbsp;
            </td>
            <td style="width: 50%;">
                1. <asp:Label ID="lblcourse_11" runat="server" Text=""></asp:Label>
            </td>
            <td style="width: 45%;">
                <asp:RadioButtonList ID="Course01" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Mandatory"
                    ControlToValidate="Course01"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                2. <asp:Label ID="lblcourse_12" runat="server" Text=""></asp:Label>
            </td>
            <td style="width: 45%;">
                <asp:RadioButtonList ID="Course02" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Mandatory"
                    ControlToValidate="Course02"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                3. <asp:Label ID="lblcourse_13" runat="server" Text=""></asp:Label>
            </td>
            <td style="width: 45%;">
                <asp:RadioButtonList ID="Course03" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Mandatory"
                    ControlToValidate="Course03"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                4. <asp:Label ID="lblcourse_14" runat="server" Text=""></asp:Label>
            </td>
            <td style="width: 45%;">
                <asp:RadioButtonList ID="Course04" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Mandatory"
                    ControlToValidate="Course04"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                5. <asp:Label ID="lblcourse_02" runat="server" Text=""></asp:Label>
            </td>
            <td style="width: 45%;">
                <asp:RadioButtonList ID="Course05" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="1">1&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">2&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3">3&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">4&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="5">5&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="6">6&nbsp;&nbsp;</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Mandatory"
                    ControlToValidate="Course05"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnBack" runat="server" Text="Back " CssClass="mybutton" />&nbsp;
                <asp:Button ID="btnSubmit" runat="server" Text="Submit " CssClass="mybutton" />
            </td>
        </tr>
    </table>
    
</asp:Content>
