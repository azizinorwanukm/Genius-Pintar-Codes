<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm3_config_exam_question_updated.ascx.vb" Inherits="permatapintar.ukm3_config_exam_question_updated" %>

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
        width: 268px;
    }
    .auto-style3 {
        width: 438px;
    }
    .auto-style4 {
        width: 15%;
    }

</style>

<script language = "Javascript">

    function tbLimit() {

var tbObj=event.srcElement;
if (tbObj.value.length==tbObj.maxLength*1) return false;

}

function tbCount(visCnt) {
var tbObj=event.srcElement;

if (tbObj.value.length>tbObj.maxLength*1) tbObj.value=tbObj.value.substring(0,tbObj.maxLength*1);
if (visCnt) visCnt.innerText=tbObj.maxLength-tbObj.value.length;

}

</script>



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
        <td>

        </td>
    <td>
            <Button ID="btnupdate" runat="server" Text="Update" class="btn btn-primary active">Update</Button>
            <Button ID="btnback" runat="server" Text="Back" class="btn btn-primary active" >Back</Button>

            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>

    </td>
        </tr>
</table>

