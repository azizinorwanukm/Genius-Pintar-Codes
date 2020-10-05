<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pembantupelajar/main.Master"
    CodeBehind="laporan.harian.aspx.vb" Inherits="permatapintar.laporan_harian2" %>

<%@ Register src="../commoncontrol/ppcs_list_classid_session.ascx" tagname="ppcs_list_classid_session" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:ppcs_list_classid_session ID="ppcs_list_classid_session1" runat="server" />
&nbsp;
</asp:Content>
