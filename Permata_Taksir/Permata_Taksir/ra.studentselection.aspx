<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ra.studentselection.aspx.vb" Inherits="UKM3.ra_studentselection" %>

<%@ Register Src="~/Control/RAPPCS/ra_studentselection.ascx" TagPrefix="uc1" TagName="ra_studentselection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ra_studentselection runat="server" id="ra_studentselection" />
</asp:Content>
