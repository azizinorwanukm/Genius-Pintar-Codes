<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.TransactionLog.list.aspx.vb" Inherits="permatapintar.admin_TransactionLog_list" %>


<%@ Register Src="commoncontrol/TransactionLog_list.ascx" TagName="TransactionLog_list" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:TransactionLog_list ID="TransactionLog_list1" runat="server" />
    &nbsp;
    
</asp:Content>
