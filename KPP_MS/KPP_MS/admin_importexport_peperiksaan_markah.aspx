<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_importexport_peperiksaan_markah.aspx.vb" Inherits="KPP_MS.admin_importexport_peperiksaan_markah" %>

<%@ Register Src="~/commoncontrol/Import_Export_student_marks.ascx" TagPrefix="uc1" TagName="Import_Export_student_marks" %>
<%@ Register Src="~/commoncontrol/import_exam_marks.ascx" TagPrefix="uc1" TagName="import_exam_marks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="importMarkahExam" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;margin-bottom:10px">
        <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Import Exam Marks</p>
        <uc1:import_exam_marks runat="server" id="import_exam_marks" />
    </div>
    </div>

    <div>
        <uc1:Import_Export_student_marks runat="server" id="Import_Export_student_marks" />
    </div>
</asp:Content>
