<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_examStudentQuestion.ascx.vb" Inherits="permatapintar.config_examStudentQuestion" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<table class="fbform">
        <tr class="fbform_header">
            <td >Soalan Carian 
            </td>
        </tr>
    <tr>
        <td>Tahun Soalan : <asp:DropDownList runat="server" ID="ddlYearselect" placeholder="Sila Pilih Tahun Soalan"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td >
            <asp:Button id="btn_Search" runat="server" Text="Cari" CssClass="fbbutton" />
        </td>
    </tr>
    
    </table>
    &nbsp;<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Soalan
        </td>
    </tr>
    <tr>
        <td>
            <div id="exam_info" runat ="server">
                   <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id"
                Width="100%" PageSize="25" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                    <asp:Label ID="Ques_id" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                     <asp:TemplateField HeaderText="Tahun Soalan">
                    <ItemTemplate>
                    <asp:Label ID="lbl_examYear" runat="server" Text='<%# Bind("examyear") %>'></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama Ujian">
                    <ItemTemplate>
                    <asp:Label ID="lbl_ExamName" runat="server" Text='<%# Bind("exam_name") %>'></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                    <asp:TemplateField HeaderText="Jumlah Soalan">
                    <ItemTemplate>
                    <asp:Label ID="lbl_jumlahSoalan" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                    
                     <asp:CommandField SelectText="Kemaskini" ShowSelectButton="True" HeaderText="Pilih"/>                              
                
                     <%-- <asp:CommandField selectText="Delete" ShowSelectButton="True" />   --%>
                
                  </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>

            </div>

            
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btncreate" runat="server" Text="Cipta Ujian Baru" CssClass="fbbutton" PostBackUrl="~/config_examStudentQuestionSetup.aspx"/>&nbsp;
        </td>
    </tr>
</table>