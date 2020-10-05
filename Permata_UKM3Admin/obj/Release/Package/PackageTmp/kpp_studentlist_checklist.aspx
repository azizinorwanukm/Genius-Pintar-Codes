<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kpp_studentlist_checklist.aspx.vb" Inherits="permatapintar.kpp_studentlist_checklist1" %>

<%@ Register Src="~/commoncontrol/kpp_studentlist_checklist.ascx" TagPrefix="uc1" TagName="kpp_studentlist_checklist" %>
<%@ Register Src="~/commoncontrol/studentprofile_header.ascx" TagPrefix="uc1" TagName="studentprofile_header" %>



<asp:Content runat="server" ID="uc1" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc1:studentprofile_header runat="server" ID="studentprofile_header" />
    &nbsp;
<uc1:kpp_studentlist_checklist runat="server" ID="kpp_studentlist_checklist" />
</asp:Content>
