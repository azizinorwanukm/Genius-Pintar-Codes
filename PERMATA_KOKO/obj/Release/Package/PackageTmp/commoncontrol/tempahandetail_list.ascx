<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="tempahandetail_list.ascx.vb" Inherits="permatapintar.tempahandetail_list" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Tempahan&nbsp;|&nbsp;<asp:LinkButton ID="lnkRefresh" runat="server">Refresh</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="TempahanID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kemudahan">
                        <ItemTemplate>
                            <asp:Label ID="Kemudahan" runat="server" Text='<%# Bind("Kemudahan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="BookingDate" runat="server" Text='<%# Bind("BookingDate")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pemohon">
                        <ItemTemplate>
                            <asp:Label ID="Pemohon" runat="server" Text='<%# Bind("Pemohon")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Tel#">
                        <ItemTemplate>
                            <asp:Label ID="ContactNo" runat="server" Text='<%# Bind("ContactNo")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="StatusTempahan" runat="server" Text='<%# Bind("StatusTempahan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="07">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time07").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="08">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time08").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="09">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time09").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="10">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time10").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="11">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time11").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="12">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time12").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="01">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time13").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="02">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time14").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="03">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time15").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="04">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time16").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="05">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time17").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="06">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time18").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="07">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time19").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="08">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time20").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="09">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time21").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="10">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time22").ToString()), "X", " ")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="11">
                        <ItemTemplate>
                            <%#IIf(Boolean.Parse(Eval("Time23").ToString()), "X", " ")%>
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
        <td class="fbform_sap">&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Eksport" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>