<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kpp_studentlist_checklist.ascx.vb" Inherits="permatapintar.kpp_studentlist_checklist" %>

<style type="text/css">

    .auto-style1 {
        height: 10px;
    }
    p.maintext{
        color:red;
        font-weight:bold;
    }
    label, input[type="radio"] {
  font-size:20px;
  vertical-align:text-bottom;
  text-underline-position:auto;
  text-anchor:middle;
}
    .rdbtext {
        display:block;
        font-size: 10px;
        position: inherit;
        text-size-adjust:150%;
       text-anchor:middle;
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
    <tr><td><p class="maintext">For every statement, please make a reference to the student that you would like to observe: <br />
            Mark strongly agree (SA) to strongly disagree (SD), or DK if you don’t know.</p> 
    </td></tr>
    <tr class="fbform_header">
        <td>Tahun Soalan &nbsp;<asp:DropDownList ID="ddlYear" runat ="server" width ="200px" OnSelectedIndexChanged ="ddlyear_onselectedindexChanged" AutoPostBack="true"></asp:DropDownList>
        </td>
     
    </tr>
    <tr class="who">
        <td class="auto-style2"> My student/the student: </td>
    </tr>
    <tr>
        <td class="td1">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Ques_id"
                Width="100%" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Penilaian">
                        <ItemTemplate>
                            <asp:Label ID="question" runat="server" Text='<%# Bind("Question") %>' Style="display:block;float:left;clear:left;width:450px;"></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="tfield1" HeaderText="SA 10 - 1 SD">
                 
                        <ItemTemplate>
                        
                        <asp:RadioButton style="width:100px;" ID="radio0" GroupName="marks" runat="server" />
                        <asp:RadioButton style="width:100px;" ID="radio1" GroupName="marks" runat="server" />
                        <asp:RadioButton style="width:100px;" ID="radio2" GroupName="marks" runat="server"/>
                        <asp:RadioButton style="width:100px;" ID="radio3" GroupName="marks" runat="server"/>
                        <asp:RadioButton style="width:100px;" ID="radio4" GroupName="marks" runat="server"/> 
                        <asp:RadioButton style="width:100px;" ID="radio5" GroupName="marks" runat="server" />
                        <asp:RadioButton style="width:100px;" ID="radio6" GroupName="marks" runat="server" />
                        <asp:RadioButton style="width:100px;" ID="radio7" GroupName="marks" runat="server"/>
                        <asp:RadioButton style="width:100px;" ID="radio8" GroupName="marks" runat="server"/>
                        <asp:RadioButton style="width:100px;" ID="radio9" GroupName="marks" runat="server"/> 
                               
                                </ItemTemplate>
                        
                         <HeaderStyle HorizontalAlign="center" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign ="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DK">
                        <ItemTemplate>
                        <asp:RadioButton style="transform:scale(2.5)" Id ="radioDK" GroupName="marks" runat="server" />
                            <br />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign ="center" />

                    </asp:TemplateField>
                   <%-- <asp:TemplateField>
                        
                        <ItemTemplate>
                            <asp:Label runat="server" Text="Give comment : " TextMode="MultiLine">
                                <asp:TextBox runat="server">

                                </asp:TextBox>
                        </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
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
           <asp:Label ID="lbl_komen" runat="server" Text="Komen Dan Sebab"></asp:Label>
        </td>          
    </tr>
    <tr>
             <td>
                  <asp:TextBox ID="txt_Comment" runat="server" Height="70px" TextMode="MultiLine" Width="1000px"></asp:TextBox>
            
        </td>
    </tr>
    <tr>
        <td>
            PENCAPAIAN (NYATAKAN JIKA ADA) :
          </td>    
    </tr>
    <tr>
        <td>
                        <asp:TextBox ID="txtPencapaian" runat="server" Height="70px" TextMode="MultiLine" Width="1000px"></asp:TextBox>
            
        </td>
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
            <asp:Button ID="btnSimpan" runat="server" Text="Simpan" CssClass="fbbutton" />
        </td>
    </tr>

</table>
  <table class="fbform" id ="table_msg" runat="server">
        <tr class="fbform_msg">
       
        <td>
            <asp:Label ID="lblMsg_invi" runat="server" Text="" ForeColor="Red"></asp:Label>

        </td>
    </tr>
  </table>
