<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm2_state_summary.ascx.vb"
    Inherits="permatapintar.ukm2_state_summary" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>Carian.
        </td>
    </tr>
    <tr>
        <td>Tahun Ujian:<asp:DropDownList ID="ddlExamYear" runat="server" Width="200px">
        </asp:DropDownList>
            &nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="SchoolState"
                Width="100%" PageSize="25" AllowSorting="true">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="SchoolState" runat="server" Text='<%# Bind("SchoolState") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hadir=N">
                        <ItemTemplate>
                            <asp:Label ID="isHadir_N" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hadir=N. B">
                        <ItemTemplate>
                            <asp:Label ID="isHadir_NB" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hadir=N. LB">
                        <ItemTemplate>
                            <asp:Label ID="isHadir_NLB" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Hadir=Y">
                        <ItemTemplate>
                            <asp:Label ID="isHadir_Y" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hadir=Y. B">
                        <ItemTemplate>
                            <asp:Label ID="isHadir_YB" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hadir=Y. LB">
                        <ItemTemplate>
                            <asp:Label ID="isHadir_YLB" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah">
                        <ItemTemplate>
                            <asp:Label ID="isHadir_ALL" runat="server" Text=''></asp:Label>
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
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>

</table>
[B=BANDAR] [LB=LUAR BANDAR] Jika terdapat perbezaan bermakna sekolah tidak dinyatakan B atau LB.
