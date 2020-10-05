<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm3_config_exam_question.ascx.vb" Inherits="permatapintar.ukm3_config_exam_question" %>

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
            <td colspan="4">Soalan Carian 
            </td>
        </tr>
    <tr>
        <td>Tahun Soalan</td>
        <td>:</td>
        <td>
            <asp:DropDownList runat="server" ID="ddlYearselect" placeholder="Sila Pilih Tahun Soalan"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Jenis Soalan</td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="ddlJenissoalan" runat="server" Width="326px" MaxLength="200"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            <asp:Button id="btn_Search" runat="server" Text="Cari" CssClass="fbbutton" Width="130px"/>
        </td>
    </tr>
    
    </table>

    &nbsp;<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Soalan Penilaian
        </td>
    </tr>
    <tr>
        <td>
            <div id="exam_info" runat ="server">
                   <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Ques_id"
                Width="100%" PageSize="25" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                    <asp:Label ID="Ques_id" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                    <asp:TemplateField HeaderText="Soalan ">
                    <ItemTemplate>
                    <asp:Label ID="question" runat="server" Text='<%# Bind("Question") %>'></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                
                    <asp:TemplateField HeaderText="Set Soalan Penilaian">
                    <ItemTemplate>
                    <asp:Label ID="questiontype" runat="server" Text='<%# Bind("Question_type") %>'></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Tahun Soalan">
                    <ItemTemplate>
                    <asp:Label ID="questionyear" runat="server" Text='<%# Bind("Question_year") %>'></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                    <asp:TemplateField HeaderText="PPCS">
                    <ItemTemplate>
                    <asp:Label ID="ppcs_type" runat="server" Text='<%# Bind("Ppcs_type") %>'></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
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

            </div>

            
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btncreate" runat="server" Text="New Question" CssClass="fbbutton" PostBackUrl="~/ukm3_config_exam_question_new.aspx"/>&nbsp;
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="fbbutton" class="btn btn-primary active" />&nbsp;
        </td>
    </tr>
</table>