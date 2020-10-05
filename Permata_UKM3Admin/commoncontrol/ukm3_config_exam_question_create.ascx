<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm3_config_exam_question_create.ascx.vb" Inherits="permatapintar.ukm3_config_exam_question_create" %>


<style type="text/css">
    table, td, th {    
    text-align: left;
}

table {
    border-collapse: collapse;
    width: 70%;
}

th, td {
    padding: 3px;
}

    .auto-style1 {
        width: 244px;
    }
    
</style>


<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Kemasukan Soalan Baru
        </td>
    </tr>
    <tr style ="margin-top:10px;">
        <td class="auto-style1">
            *Soalan :
        </td>
        <td>
            <asp:TextBox ID="txtsoalan" runat="server" Width="438px" TextMode="MultiLine" Height="79px"></asp:TextBox>
            (<asp:Label id ="countdown_comment" runat ="server">200</asp:Label> Perkataan Lagi)
        </td>
    </tr>
    <tr style ="margin-top:10px;">
        <td class="auto-style1">
            *Instruktor :
        </td>
        <td>
            <asp:DropDownList ID="instruktor_Selec" runat ="server">

            </asp:DropDownList>
        </td>
    </tr>

      <tr>
        <td class="auto-style1">
            *Tahun :
        </td>
        <td>
            <asp:TextBox ID="txttahun" runat="server" Width="250px" MaxLength="254" TextMode="SingleLine"></asp:TextBox>
            &nbsp;
        </td>
    </tr>
      <tr>
        <td class="auto-style1">
            *Jenis Soalan :
        </td>
        <td>
            <asp:DropDownList id="question_Select" runat ="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr class="fbform_msg">
        <td class="auto-style1">

        </td>
    <td>
            <Button ID="btnsimpan" runat="server" Text="Daftar" class="btn btn-primary active">Daftar</Button>
        
            <Button ID = "btnreset" runat="server" Text="reset" class = "btn btn-primary active">Reset</Button>

            <Button ID = "btnback" runat = "server" text = "back" class = "btn btn-primary active">Back</Button>

            <asp:Label ID="lblMsg_invi" runat="server" Text="" ForeColor="Red"></asp:Label>

            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>

    </td>
        </tr>
</table>

