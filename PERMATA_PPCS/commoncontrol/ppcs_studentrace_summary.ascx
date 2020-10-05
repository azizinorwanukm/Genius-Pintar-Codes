<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_studentrace_summary.ascx.vb"
    Inherits="permatapintar.ppcs_studentrace_summary1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Ringkasan Bangsa. PPCS Status=LAYAK&nbsp;[<asp:Label ID="lblDatetime"
            runat="server" Text=""></asp:Label>]
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Sessi PPCS:</td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>

        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbaside_sap">
            <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Bangsa.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentRace"
                Width="100%" PageSize="95">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bangsa">
                        <ItemTemplate>
                            <asp:Label ID="StudentRace" runat="server" Text='<%# Bind("StudentRace") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah">
                        <ItemTemplate>
                            <asp:Label ID="nTotal" runat="server" Text='<%# Bind("nTotal") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Ringkasan Bangsa dan Tahun Lahir.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Bangsa:
        </td>
        <td>
            <asp:DropDownList ID="ddlStudentRace" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
        <td>Tahun Lahir:
        </td>
        <td>
            <asp:DropDownList ID="ddlDOB_Year" runat="server" AutoPostBack="false" Width="100px">
            </asp:DropDownList>

        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbaside_sap" colspan="3">
            <asp:Label ID="lblMsgMid" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="3">
            <asp:Button ID="btnSearchRace" runat="server" Text="Cari " CssClass="fbbutton" /></td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Ringkasan Bangsa dan Tahun Lahir. 
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="dat_Race_State" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="SchoolState"
                Width="100%" PageSize="95">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="SchoolState" runat="server" Text='<%# Bind("SchoolState") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah">
                        <ItemTemplate>
                            <asp:Label ID="TotalStudent" runat="server" Text='<%# Bind("TotalStudent") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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
    <tr>
        <td>
            <i>Nota: Jumlah mugkin tidak sama kerana ada pelajar yang tidak menentukan
            sekolah mereka. Negeri adalah berdasarkan sekolah pelajar.</i>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>
