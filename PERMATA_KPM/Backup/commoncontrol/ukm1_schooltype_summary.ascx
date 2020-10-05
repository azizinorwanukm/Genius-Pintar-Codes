<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm1_schooltype_summary.ascx.vb"
    Inherits="permatapintar.ukm1_schooltype_summary" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Ringkasan Ujian Jenis Sekolah.[<asp:Label ID="lblDatetime" runat="server" Text=""></asp:Label>]
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Tahun:<asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="SchoolType"
                Width="100%" PageSize="95">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jenis Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="SchoolType" runat="server" Text='<%# Bind("SchoolType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah">
                        <ItemTemplate>
                            <asp:Label ID="nTotal" runat="server" Text='<%# Bind("nTotal") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:HyperLinkField DataNavigateUrlFields="schooltype" DataTextField="nTotal" DataTextFormatString=""
                        HeaderText="Jumlah" DataNavigateUrlFormatString="~\admin.ukm1.schooltype.list.aspx?schooltype={0}" />--%>
                    <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="PILIH" />
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
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
</div>
