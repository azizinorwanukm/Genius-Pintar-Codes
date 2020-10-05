<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="default.aspx.vb" Inherits="UKM_SEMAKAN._default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>Selamat Datang ke Laman Semakan PERMATApintar</h2>
    <asp:Panel ID="pnlSTOPALL" runat="server" Visible="true">
        <h3>
            <asp:Label ID="lblSemak" runat="server" Text="SILA PILIH SEMAKAN YANG INGIN DIBUAT:"></asp:Label></h3>
    </asp:Panel>
    <hr />

    <asp:Panel ID="pnlUKM2" runat="server" Visible="false">
        <p>
            <asp:LinkButton ID="lnkUKM2Title" runat="server">*&nbsp;<asp:Label ID="lblUKM2Title" runat="server" Text="" Font-Bold="true"></asp:Label></asp:LinkButton>
        </p>
    </asp:Panel>

    <asp:Panel ID="pnlPPCS" runat="server" Visible="false">
        <p>
            <asp:LinkButton ID="lnkPPCSTitle" runat="server">*&nbsp;<asp:Label ID="lblPPCSTitle" runat="server" Text="" Font-Bold="true"></asp:Label></asp:LinkButton>
        </p>
    </asp:Panel>

    <asp:Panel ID="pnlASASI" runat="server" Visible="false">
        <p>
            <asp:LinkButton ID="lnkASASITitle" runat="server">*&nbsp;<asp:Label ID="lblASASITitle" runat="server" Text="" Font-Bold="true"></asp:Label></asp:LinkButton>
        </p>
    </asp:Panel>

    <asp:Panel ID="pnlPPMT" runat="server" Visible="false">
        <p>
            <asp:LinkButton ID="lnkPPMTTitle" runat="server">*&nbsp;<asp:Label ID="lblPPMTTitle" runat="server" Text="" Font-Bold="true"></asp:Label></asp:LinkButton></p>
    </asp:Panel>

     <asp:Panel ID="pnlKolej" runat="server" Visible="false">
        <p>
            <asp:LinkButton ID="lnkKOLEJitle" runat="server">*&nbsp;<asp:Label ID="lblKOLEJTitle" runat="server" Text="" Font-Bold="true"></asp:Label></asp:LinkButton></p>
    </asp:Panel>

    <p>&nbsp;</p>
    <p>* <a href="contactus.aspx">Hubungi Kami</a></p>



</asp:Content>
