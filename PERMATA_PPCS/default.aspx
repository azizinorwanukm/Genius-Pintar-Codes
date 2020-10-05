<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="default.aspx.vb" Inherits="permatapintar._default1" %>

<%@ Register Src="commoncontrol/ukm2.ppcs.login.control.ascx" TagName="ukm2" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm2 ID="ukm21" runat="server" />
    &nbsp;<div class="warning">
        Jika anda dapati tiba-tiba sistem menjadi terlalu perlahan, berkemungkinan sistem
        sedang dikemaskini. Sila LOGOUT dan LOGIN semula. Sistem akan pulih seperti sediakala.<br />
        <br />
        *Selalunya sistem akan dikemaskini selepas pukul 9 malam.</div>
</asp:Content>
