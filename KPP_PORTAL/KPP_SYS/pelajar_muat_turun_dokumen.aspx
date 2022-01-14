<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_muat_turun_dokumen.aspx.vb" Inherits="KPP_SYS.pelajar_muat_turun_dokumen" %>

<%@ Register Src="~/commoncontrol/student_download_reference.ascx" TagPrefix="uc1" TagName="student_download_reference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_download_reference runat="server" id="student_download_reference" />
</asp:Content>
