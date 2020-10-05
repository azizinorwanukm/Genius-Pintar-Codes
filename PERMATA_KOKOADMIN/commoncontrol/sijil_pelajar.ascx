<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="sijil_pelajar.ascx.vb" Inherits="permatapintar.sijil_pelajar" %>
<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[9].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }

</script>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:
        </td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="200px">
            </asp:DropDownList>
        </td>
        <td>Peperiksaan:</td>
        <td>
            <select name="selPeperiksaan" id="selPeperiksaan" style="width: 200px;" runat="server">
                <option value="P1">SEMESTER 1</option>
                <option value="P2">SEMESTER 2</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>Kelas:</td>
        <td>
            <asp:DropDownList ID="ddlKelas" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
        <td>Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td colspan="4">
            <asp:Button ID="btnLoad" runat="server" Text="Cari " CssClass="fbbutton" />
        </td>
    </tr>
</table>
<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Pelajar|<asp:Label ID="lblKOKOName" runat="server" Text=''></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="StudentID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NAMA PELAJAR">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KELAS">
                        <ItemTemplate>
                            <asp:Label ID="Kelas" runat="server" Text='<%# Bind("Kelas")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD#">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GRED">
                        <ItemTemplate>
                            <asp:Label ID="Gred" runat="server" Text='<%# Bind("Gred")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="15% KOKO">
                        <ItemTemplate>
                            <asp:Label ID="KOKO" runat="server" Text='<%# Bind("KOKO", "{0:N1}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                   <asp:CommandField SelectText="[Papar]" ShowSelectButton="True" HeaderText="PDF" />

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
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGenSijil" runat="server" Text="Jana Sijil" CssClass="fbbutton" Visible="true" />
            <asp:HyperLink ID="hyPDF" runat="server" Target="_blank" Visible="false">Klik disini.</asp:HyperLink>
        </td>
    </tr>
</table>
<asp:Label ID="lblStudentID" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
<asp:Label ID="lblStudentFullname" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
<asp:Label ID="lblMYKAD" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
<asp:Literal ID="litPencapaian" runat="server"></asp:Literal>