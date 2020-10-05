<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="kolej_semak.aspx.vb" Inherits="UKM_SEMAKAN.kolej_semak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>Selamat Datang ke Laman Semakan Kolej PERMATApintar® UKM</h2>

    <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table id="mycustomtable">
        <tr>
            <th colspan="2">SEMAKAN KELAYAKAN ANDA KE KOLEJ PERMATApintar® UKM</th>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label1" runat="server" Text="MYKAD\MYKID#:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMYKAD" runat="server" Text="" Width="150px"></asp:TextBox>&nbsp;<asp:Button
                    ID="btnSemak" runat="server" Text="Semak" CssClass="fbbutton" />
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Sila masukkan MYKAD/MYKID# sepertimana didaftarkan didalam profil pelajar."></asp:Label>
    </div>
    
</asp:Content>
