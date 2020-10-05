﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm1_toplist.aspx.vb" Inherits="permatapintar.ukm1_toplist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Lain-lain><a href="settings.aspx" rel="nofollow" target="_self">Sistem Konfigurasi V2</a>>Senarai Penyertaan Tertinggi UKM1
            </td>
        </tr>
    </table>
    <table class="fbform" style="font-size: 12px;">
        <tr class="fbform_header">
            <td>Penyertaan Tertinggi pada <br />
                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label><br />
                <asp:Button ID="btnKemaskini" runat="server" Text="Kemaskini Senarai" CssClass="fbbutton" />&nbsp;
                <asp:Label ID="lblDebug" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="margin:0 0 0 0" colspan="2">
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="SchoolID"
                    Width="100%" PageSize="25" AllowSorting="true" HeaderStyle-HorizontalAlign="Left">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Sekolah" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="SchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Negeri" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="SchoolState" runat="server" Text='<%# Bind("SchoolState")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#Pelajar" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="Jumlah" runat="server" Text='<%# Bind("Jumlah")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
        </tr>
        <tr class="fbform_msg">
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=" JUMLAH REKOD#:10"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="chkRuleAge" runat="server" Text="Pelajar berumur 8-15 tahun (8 tahun - beragama Islam sahaja)" Checked="true" Enabled="false" />
            </td>
        </tr>
</table>
</asp:Content>
