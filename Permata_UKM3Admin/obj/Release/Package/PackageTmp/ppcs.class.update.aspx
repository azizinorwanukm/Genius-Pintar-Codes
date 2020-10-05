<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ppcs.class.update.aspx.vb" Inherits="permatapintar.ppcs_class_update" %>

<%@ Register Src="~/commoncontrol/ppcs_class_update.ascx" TagPrefix="uc1" TagName="ppcs_class_update" %>
<%@ Register Src="~/commoncontrol/ppcs_class_list.ascx" TagPrefix="uc1" TagName="ppcs_class_list" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:ppcs_class_update runat="server" ID="ppcs_class_update" />
    <uc1:ppcs_class_list runat="server" id="ppcs_class_list" />
</asp:Content>
