<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_examQuestionConfig.aspx.vb" Inherits="permatapintar.ukm3_examQuestionConfig" %>

<%@ Register Src="~/commoncontrol/config_examStudentQuestion.ascx" TagPrefix="uc1" TagName="config_examStudentQuestion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Ujian STEM>Config Ujian STEM"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:config_examStudentQuestion runat="server" ID="config_examStudentQuestion" />
</asp:Content>
