<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm3_adminRaPPCS_resetPenilaian.ascx.vb" Inherits="permatapintar.ukm3_adminRaPPCS_resetPenilaian" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">
           <asp:label runat ="server" > Reset Penilaian KPP </asp:label> 
        </td>
    </tr>
     <tr style ="object-position :left ">
        <td style ="text-align :right ">
            <asp:label runat ="server" > Session</asp:label>
        </td>         
        <td>:</td>
        <td>
            <asp:DropDownList runat ="server" ID ="ddlSesi"></asp:DropDownList>
        </td>
    </tr>
    <tr><td colspan ="4"></td></tr>
    <tr>
        <td colspan="4" style ="text-align :center ">
           <asp:label runat ="server" Font-Bold ="true" >  Reset Mengikut Kelas </asp:label> 
        </td>
    </tr>
   
    <tr style ="object-position :left ">
        <td style ="text-align :right ">
            <asp:label runat ="server" >Kelas</asp:label> 
        </td>
        <td>:</td>
        <td><asp:DropDownList runat ="server" ID="ddlKelas"></asp:DropDownList></td>
    </tr>
    <tr><td colspan ="4"></td></tr>
    <tr>
        <td colspan ="4" style ="text-align :center ">
           <asp:label runat ="server"  Font-Bold ="true">  Reset Mengikut Nama Instruktor </asp:label> 
        </td>
    </tr>
    <tr style ="object-position :left ">
        <td style ="text-align :right ">
           <asp:label runat ="server" >  Nama Instruktor</asp:label> 
        </td>         
        <td>:</td>
        <td>
            <asp:DropDownList runat="server" ID="ddlNamaInstruktor"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button runat ="server" ID="btnConfirm" Text ="Pembatalan Pengesahan" />
        </td>
    </tr>
</table>
