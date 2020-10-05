<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_pengurusan_am_kelas.aspx.vb" Inherits="KPP_MS.admin_pengurusan_am_kelas" %>

<%@ Register src="commoncontrol/class_List_Table.ascx" tagname="class_List_Table" tagprefix="uc1" %>

<%@ Register src="commoncontrol/class_Create.ascx" tagname="class_Create" tagprefix="uc2" %>
<%@ Register Src="~/commoncontrol/import_class.ascx" TagPrefix="uc1" TagName="import_class" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="editClass_info" style="width: 100%; background-color: #f2f2f2; text-align: center">
        <uc1:class_List_Table ID="class_List_Table" runat="server" />
    </div>

    
</asp:Content>
