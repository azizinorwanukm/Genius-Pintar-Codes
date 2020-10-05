<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_displayppcs.aspx.vb" Inherits="permatapintar.admin_displayppcs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Lain-lain><a href="settings.aspx" rel="nofollow" target="_self">Sistem Konfigurasi V2</a>>Paparan Keputusan PPCS
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnDisplayAdd" runat="server" Text="Tambah Tarikh PPCS" CssClass="fbbutton" />&nbsp;
    <asp:TextBox ID="txtYear" runat="server" Width="200px" MaxLength="20" placeholder="contoh, PPCS Dis 2018" Visible="false"></asp:TextBox>&nbsp;
    <asp:Button ID="btnAddYear" runat="server" Text="Tambah Tarikh" CssClass="fbbutton" Visible="false" />&nbsp;
    <asp:Button ID="btnCancelAdd" runat="server" Text="Batal" CssClass="fbbutton" Visible="false" />&nbsp;
    <br />
    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Font-Bold="true" />&nbsp;
    <br />
    Tarikh PPCS Terkini : <asp:DropDownList ID="ddlDefault" runat="server"></asp:DropDownList>&nbsp;
    <asp:Button ID="btnDefault" runat="server" Text="Tukar" CssClass="fbbutton"  />&nbsp;
    <br /><br />
    <div>
        <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CellPadding="2" ForeColor="#333333" GridLines="Both" DataKeyNames="ppcsid" RowDataBound="datRespondent_RowDataBound"
            PageSize="25" CssClass="gridview_footer">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Tarikh PPCS" >
                    <ItemTemplate>
                        <asp:Label ID="lbl_ExamYear" runat="server" Text='<%# Bind("PPCSDate") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-Width="50" HeaderText="Papar PPCS">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlPpcs" runat="server"></asp:DropDownList>
                        <asp:Label ID="lblPpcs" runat="server" Text='<%# Bind("displayHistory") %>' Visible="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="50" HeaderText="Papar Dropdown">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlDdl" runat="server"></asp:DropDownList>
                        <asp:Label ID="lblDdl" runat="server" Text='<%# Bind("displayDdl") %>' Visible="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
            
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    
    </div>
    
    <br />
    <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
    <asp:Button ID="btnBack" runat="server" Text="Kembali" CssClass="fbbutton" />
    <br /><br />
    <asp:Button ID="btnDelYear" runat="server" Text="Buang Tarikh PPCS" CssClass="fbbutton" />&nbsp;*Sekiranya tarikh itu telah digunakan, ia tidak boleh dibuang.
    <br /><br />
    Nota<br />
    * Kolum Papar PPCS adalah untuk memaparkan keputusan PPCS pada laman Pelajar bagi sesi PPCS yang berkaitan. <br />
    * Kolum Papar dropdown adalah untuk mewujudkan/menghilangkan sesi dalam dropdown sesi PPCS yang dijumpai dalam kesuluruhan laman admin permatapintar.
</asp:Content>
