<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kpp_studentlist_checklist_update.aspx.vb" Inherits="permatapintar.kpp_studentlist_checklist_update" %>

<%@ Register Src="~/commoncontrol/kpp_studentlist_checklist_update.ascx" TagPrefix="uc1" TagName="kpp_studentlist_checklist_update" %>
<%@ Register Src="~/commoncontrol/studentprofile_header.ascx" TagPrefix="uc1" TagName="studentprofile_header" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header runat="server" ID="studentprofile_header" />
    &nbsp;
    <uc1:kpp_studentlist_checklist_update runat="server" id="kpp_studentlist_checklist_update" />
</asp:Content>
