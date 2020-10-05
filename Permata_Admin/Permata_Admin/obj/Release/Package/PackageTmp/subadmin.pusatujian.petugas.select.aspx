<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master"
    CodeBehind="subadmin.pusatujian.petugas.select.aspx.vb" Inherits="permatapintar.subadmin_pusatujian_petugas_select" %>

<%@ Register Src="commoncontrol/pusatujian_petugas_select.ascx" TagName="pusatujian_petugas_select"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;<uc1:pusatujian_petugas_select ID="pusatujian_petugas_select1" runat="server" />
</asp:Content>
