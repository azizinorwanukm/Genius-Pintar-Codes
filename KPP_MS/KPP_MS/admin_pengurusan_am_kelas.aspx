<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_pengurusan_am_kelas.aspx.vb" Inherits="KPP_MS.admin_pengurusan_am_kelas" %>

<%@ Register Src="commoncontrol/class_List_Table.ascx" TagName="class_List_Table" TagPrefix="uc1" %>

<%@ Register Src="commoncontrol/class_Create.ascx" TagName="class_Create" TagPrefix="uc2" %>
<%@ Register Src="~/commoncontrol/import_class.ascx" TagPrefix="uc1" TagName="import_class" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:class_List_Table ID="class_List_Table" runat="server" />

</asp:Content>
