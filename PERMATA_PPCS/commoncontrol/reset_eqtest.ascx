<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="reset_eqtest.ascx.vb" Inherits="permatapintar.reset_eqtest1" %>
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
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">PPCS Date:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td class="fbtd_left">Email Address:
        </td>
        <td>
            <asp:TextBox ID="txtEmailAdd" runat="server" Width="200px" MaxLength="150"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>&nbsp;
        </td>
        <td>AlumniID:</td>
        <td>
            <asp:TextBox ID="txtAlumniID" runat="server" Width="200px" MaxLength="150"></asp:TextBox>&nbsp;</td>
    </tr>
    <tr>
        <td>Completed:</td>
        <td>
            <select name="selIsCompleted" id="selIsCompleted" runat="server" style="width: 200px;">
                <option value="" selected="selected">ALL</option>
                <option value="Y">Y</option>
                <option value="N">N</option>
            </select></td>
        <td>Survey ID:</td>
        <td>
            <asp:DropDownList ID="ddlSurveyID" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="fbsection_sap" colspan="4">&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
            
        </td>
        <td></td>
        <td>Order By:</td>
        <td>
            <select name="selOrderBy" id="selOrderBy" style="width: 200px;" runat="server">
                <option value="1">Last Update</option>
            </select>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Menduduki Emotional Quotient Test
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="EQTestID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LastUpdate">
                        <ItemTemplate>
                            <asp:Label ID="LastUpdate" runat="server" Text='<%# Bind("LastUpdate")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Penuh">
                        <ItemTemplate>
                            <asp:Label ID="Fullname" runat="server" Text='<%# Bind("Fullname")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AlumniID">
                        <ItemTemplate>
                            <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("AlumniID")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="EmailAdd" runat="server" Text='<%# Bind("EmailAdd")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SurveyID">
                        <ItemTemplate>
                            <asp:Label ID="SurveyID" runat="server" Text='<%# Bind("SurveyID")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LastPage">
                        <ItemTemplate>
                            <asp:Label ID="LastPage" runat="server" Text='<%# Bind("LastPage")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IsCompleted">
                        <ItemTemplate>
                            <asp:Label ID="IsCompleted" runat="server" Text='<%# Bind("IsCompleted")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="#CCCCCC" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnIsComplete" runat="server" Text="Set IsCompleted" CssClass="fbbutton" />&nbsp;
            <select name="selSetIsCompleted" id="selSetIsCompleted" runat="server" style="width: 100px;">
                <option value="Y">Y</option>
                <option value="N">N</option>
            </select>
        </td>
    </tr>

</table>
<asp:Button ID="btnExport" runat="server" Text="Export " CssClass="fbbutton" />&nbsp;
<asp:Button ID="btnDelete" runat="server" Text="Delete " CssClass="fbbutton" />&nbsp;