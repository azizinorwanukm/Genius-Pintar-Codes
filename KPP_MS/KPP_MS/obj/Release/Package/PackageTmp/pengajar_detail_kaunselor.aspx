<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_detail_kaunselor.aspx.vb" Inherits="KPP_MS.pengajar_detail_kaunselor" %>

<%@ Register Src="~/commoncontrol/lecturer_counselor_Detail_Case.ascx" TagPrefix="uc1" TagName="lecturer_counselor_Detail_Case" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:lecturer_counselor_Detail_Case runat="server" id="lecturer_counselor_Detail_Case" />
</asp:Content>
