<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_peperiksaan_transkrip.aspx.vb" Inherits="KPP_MS.admin_peperiksaan_transkrip" EnableEventValidation="false"%>
<%@ Register src="commoncontrol/exam_Transcript.ascx" tagname="exam_Transcript" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:exam_Transcript ID="exam_Transcript" runat="server" />

</asp:Content>
