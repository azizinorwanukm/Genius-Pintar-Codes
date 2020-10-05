<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="rappcs_studentlist_checklist.ascx.vb" Inherits="UKM3.rappcs_studentlist_checklist" %>

<style type="text/css">
    .auto-style1 {
        height: 10px;
    }
    p.maintext{
        color:red;
        font-weight:bold;
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
        <td colspan="4">Maklumat Pelajar
        </td>
    </tr>
    <tr>
        <td style="width:100px">Nama Pelajar</td>
        <td> : </td>
        <td>
            <asp:Label ID="lblNama" runat="server"></asp:Label></td>

    </tr>

    <tr>
        <td style="width:100px">MYKAD</td>
        <td> : </td>
        <td>
            <asp:Label ID="lblMykad" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td style="width:100px">Kod Kelas</td>
        <td> : </td>
        <td>
            <asp:Label ID="lblKodKelas" runat="server"></asp:Label>
        </td>
    </tr>

</table>

<br />


<table class="fbform">
    <tr><td><p class="maintext">Sila pilih skor di bawah bagi setiap soalan. Skala skor bermula dari 1 sehingga 5.</p> 
    </td></tr>
    <tr class="fbform_header">
        <td>Tahun Soalan &nbsp;<asp:DropDownList ID="ddlYear" runat ="server" width ="200px" OnSelectedIndexChanged ="ddlyear_onselectedindexChanged" AutoPostBack="true"></asp:DropDownList>
        </td>
     
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Ques_id"
                Width="100%" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1  %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Penilaian">
                        <ItemTemplate>
                            <asp:Label ID="question" runat="server" Text='<%# Bind("Question") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Skor(0-5)">
                 
                        <ItemTemplate>
                           
                        <asp:RadioButton ID="radio0"   GroupName="marks" runat="server" />
                        <asp:RadioButton ID="radio1"   GroupName="marks" runat="server" />
                        <asp:RadioButton ID="radio2"  GroupName="marks" runat="server"/>
                        <asp:RadioButton ID="radio3"  GroupName="marks" runat="server"/>
                        <asp:RadioButton ID="radio4"  GroupName="marks" runat="server"/> 
                            
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="center" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign ="center" />
                    </asp:TemplateField>
                        
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>

    </tr>
    </table>
<table id="InfoTable" runat="server" class="fbform">
       <tr class ="auto-style1">
        <td></td>
    </tr>
    <tr>
         <td>
           Komen mengenai sikap dan tingkah laku pelajar secara keseluruhan :
        </td>          
    </tr>
    <tr>
             <td>
                  <asp:TextBox ID="txtComment" runat="server" Height="70px" TextMode="MultiLine" Width="1000px"></asp:TextBox>
             &nbsp;</td>
    </tr>
    <tr>
        <td>
            PENCAPAIAN (NYATAKAN JIKA ADA) :
          </td>    
    </tr>
    <tr>
        <td>
                        <asp:TextBox ID="txtPencapaian" runat="server" Height="70px" TextMode="MultiLine" Width="1000px"></asp:TextBox>
            &nbsp;</td>
    </tr>
    <tr>
         <td>
            Secara keseluruhan, Adakah anda MENYOKONG jika pelajar ini diterima masuk ke Kolej PERMATApintar NEGARA?
             <asp:RadioButtonList ID="rbl_sokong" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem value = "Y" Text ="Setuju"></asp:ListItem>
                 <asp:ListItem value = "N" Text ="Tidak Setuju"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr class="fbform_msg">
       
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>  
            <asp:Button ID="btnSimpan" runat="server" Text="Simpan" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnKembali" runat="server" Text="Kembali" CssClass="fbbutton" />&nbsp;
            
            <asp:Button ID="btnSahkan" runat="server" Text="Sahkan" CssClass="fbbutton" Visible="false" />&nbsp;
        </td>
    </tr>

</table>
  <table class="fbform" id ="table_msg" runat="server">
        <tr class="fbform_msg">
       
        <td>
            <asp:Label ID="lblMsg_invi" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
  </table>