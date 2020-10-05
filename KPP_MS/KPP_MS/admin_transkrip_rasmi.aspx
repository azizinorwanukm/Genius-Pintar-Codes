<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_transkrip_rasmi.aspx.vb" Inherits="KPP_MS.admin_transkrip_rasmi" %>
<%@ Register src="commoncontrol/exam_Official_Transcript.ascx" tagname="exam_Official_Transcript" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:exam_Official_Transcript ID="exam_Official_Transcript" runat="server" />

</asp:Content>
