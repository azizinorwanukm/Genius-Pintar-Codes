<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pcis_exam_year_list.ascx.vb" Inherits="araken.pcisadmin.pcis_exam_year_list" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">pcis_exam_year:
        </td>
        <td>
            <asp:TextBox ID="txtdescription" runat="server" Width="250px" MaxLength="150"></asp:TextBox>&nbsp;
        </td>
        <td></td>
        <td>&nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4"></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />

        </td>
    </tr>
</table>
&nbsp;
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai pcis_exam_year.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id"
                Width="100%" PageSize="25">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="id">
                        <ItemTemplate>
                            <asp:Label ID="id" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="pcis_exam_year">
                        <ItemTemplate>
                            <asp:Label ID="description" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="start_date">
                        <ItemTemplate>
                            <asp:label id="start_date" runat="server" text='<%# Eval("start_date", "{0:yyyy-MM-dd}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="end_date">
                        <ItemTemplate>
                            <asp:label id="end_date" runat="server" text='<%# Eval("end_date", "{0:yyyy-MM-dd}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Pilih" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnCreate" runat="server" Text="Tambah" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
Nota:<br />
TERKINI pcis_setting exam_year id=<asp:Label ID="lblexam_year" runat="server" Text="" Font-Bold="true"></asp:Label>
