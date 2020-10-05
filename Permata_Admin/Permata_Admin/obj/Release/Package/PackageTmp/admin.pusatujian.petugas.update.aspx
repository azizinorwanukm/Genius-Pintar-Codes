<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.pusatujian.petugas.update.aspx.vb" Inherits="permatapintar.admin_pusatujian_petugas_update" %>

<%@ Register Src="commoncontrol/pusatujian_petugas_update.ascx" TagName="pusatujian_petugas_update"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/pusatujian_petugas_list_history.ascx" TagName="pusatujian_petugas_list_history"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pusatujian_petugas_update ID="pusatujian_petugas_update1" runat="server" />
    &nbsp;<uc2:pusatujian_petugas_list_history ID="pusatujian_petugas_list_history1" runat="server" />

</asp:Content>
