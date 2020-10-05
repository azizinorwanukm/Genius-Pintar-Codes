<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_import.ascx.vb" Inherits="permatapintar.studentprofile_import" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Negeri:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td class="fbtd_left">PPD:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolPPD" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>
        </td>
        <td>Bandar:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolCity" runat="server" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Kod Sekolah:
        </td>
        <td>
            <asp:TextBox ID="txtSchoolCode" runat="server" Width="250px" MaxLength="150"></asp:TextBox>
        </td>
        <td>Nama Sekolah:
        </td>
        <td>
            <asp:TextBox ID="txtSchoolname" runat="server" Width="250px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>IsDeleted:</td>
        <td>
            <select name="selIsDeleted" id="selIsDeleted" style="width: 250px;" runat="server">
                <option value="Y">Y</option>
                <option value="N" selected="selected">N</option>
            </select></td>
        <td></td>
        <td>
            <asp:CheckBox ID="chkXXX" runat="server" Text="Kod Sekolah XXX" />&nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="4">Import Senarai Pelajar</td>
    </tr>
    <tr>
         <td>Pilih Fail Excel:
        </td>
         <td>
            <asp:FileUpload ID="FlUploadcsv" runat="server" />&nbsp;
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed"
                ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnUpload" runat="server" Text="Muatnaik " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
 </table>
&nbsp;
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>