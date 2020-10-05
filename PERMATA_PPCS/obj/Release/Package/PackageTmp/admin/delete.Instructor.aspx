<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="delete.Instructor.aspx.vb" Inherits="permatapintar.delete_Instructor"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Tenaga Pengajar
            </td>
        </tr>
        <tr>
            <td colspan="2">Butir-butir Akaun
            </td>
        </tr>
        <tr>
            <td>*Email:
            </td>
            <td>
                <asp:TextBox ID="txtemail" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td colspan="2">Butir-butir Pengajar
            </td>
        </tr>
        <tr>
            <td>*Nama penuh:
            </td>
            <td>
                <asp:TextBox ID="txtfullname" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>*Alamat:
            </td>
            <td>
                <asp:TextBox ID="txtaddress" runat="server" Width="250px" MaxLength="254"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>*No. Telefon:
            </td>
            <td>
                <asp:TextBox ID="txtcontactno" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>*IC Number:
            </td>
            <td>
                <asp:TextBox ID="txtIC" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>*Nama Kursus:
            </td>
            <td>
                <asp:DropDownList ID="txtcourse" runat="server" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>*Kod Kursus:
            </td>
            <td>
                <asp:DropDownList ID="txtcourseCode" runat="server" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>*Pembantu Trainee Assistant (TA):
            </td>
            <td>
                <asp:DropDownList ID="txtTA" runat="server" Width="250px" BackColor="LightGray">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>*Pembantu Resident Assistant (RA):
            </td>
            <td>
                <asp:DropDownList ID="txtRA" runat="server" Width="250px" BackColor="LightGray">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr class="fbform_msg">
            <td>&nbsp;</td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Button ID="btndelete" runat="server" Text=" Hapus " CssClass="fbbutton" />
                &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
