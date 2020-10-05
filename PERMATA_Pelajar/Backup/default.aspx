<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="default.aspx.vb" Inherits="permatapintar._default1" %>

<%@ Register Src="commoncontrol/user_check.ascx" TagName="user_check" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:user_check ID="user_check1" runat="server" />
    &nbsp;<div class="info">
        • Kemudahan yang disediakan untuk kegunaan pelajar PERMATApintar.<br />
        • Sebagai contoh untuk mengemaskini profil peribadi dan menyemak kelayakan ke Ujian
        UKM2<br />
        • Maklumat ini akan disimpan untuk kegunaan Ujian UKM1, UKM2 dan Program
        Perkhemahan Cuti Sekolah.</div>
</asp:Content>
