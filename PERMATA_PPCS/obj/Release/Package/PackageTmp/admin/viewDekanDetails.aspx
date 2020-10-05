<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="viewDekanDetails.aspx.vb" Inherits="permatapintar.viewDekanDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr>
            <td colspan="2">
                Pengurus Akademik
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Butir-butir Akaun
            </td>
        </tr>
        <tr>
            <td>
                *Email:
            </td>
            <td>
                <asp:TextBox ID="txtemail" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Butir-butir Dekan
            </td>
        </tr>
        <tr>
            <td>
                *Nama penuh:
            </td>
            <td>
                <asp:TextBox ID="txtfullname" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
         <tr>
            <td>
                *IC Number:
            </td>
            <td>
                <asp:TextBox ID="txtIC" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                *Alamat:
            </td>
            <td>
                <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Rows="5" Width="250px"
                    MaxLength="254"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                *No. Telefon:
            </td>
            <td>
                <asp:TextBox ID="txtcontactno" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
       
        <tr style="text-align: left">
            <td>
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnadd" runat="server" Text=" Kemaskini " CssClass="fbbutton" />
                &nbsp; &nbsp;<asp:Button ID="btndelete" runat="server" Text=" Hapus " CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
