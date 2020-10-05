<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_config_exam_question.aspx.vb" Inherits="permatapintar.ukm3_paper_config_master" %>

<%@ Register Src="~/commoncontrol/ukm3_config_exam_question.ascx" TagPrefix="uc1" TagName="ukm3_config_exam_question" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentplaceholder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Config>Config Soalan Penilaian Instruktor"></asp:label>
            </td>
        </tr>
    </table>
   <uc1:ukm3_config_exam_question runat="server" id="ukm3_config_exam_question" />

</asp:Content>