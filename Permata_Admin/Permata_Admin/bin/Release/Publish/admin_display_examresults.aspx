<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_display_examresults.aspx.vb" Inherits="permatapintar.admin_display_examresults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Lain-lain><a href="settings.aspx" rel="nofollow" target="_self">Sistem Konfigurasi V2</a>>Paparan Keputusan UKM1 & UKM2
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnDisplayAdd" runat="server" Text="Tambah Tahun" CssClass="fbbutton" />&nbsp;
    <asp:TextBox ID="txtYear" runat="server" Width="120px" MaxLength="5" placeholder="contoh: 2020" Visible="false"></asp:TextBox>&nbsp;
    <asp:Button ID="btnAddYear" runat="server" Text="Tambah Tahun" CssClass="fbbutton" Visible="false" />&nbsp;
    <asp:Button ID="btnCancelAdd" runat="server" Text="Batal" CssClass="fbbutton" Visible="false" />&nbsp;
    <br />
    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Font-Bold="true" />&nbsp;
    <br />
    Tarikh UKM1 Terkini : <asp:DropDownList ID="ddlDefault" runat="server"></asp:DropDownList>&nbsp;
    <asp:Button ID="btnDefault" runat="server" Text="Tukar" CssClass="fbbutton"  />&nbsp;
    <br /><br />
    <div>
        <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CellPadding="2" ForeColor="#333333" GridLines="Both" DataKeyNames="examyearid" RowDataBound="datRespondent_RowDataBound"
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
                
                <asp:TemplateField HeaderText="Exam Year" >
                    <ItemTemplate>
                        <asp:Label ID="lbl_ExamYear" runat="server" Text='<%# Bind("ExamYear") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-Width="50" HeaderText="Papar UKM1">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_UKM1" runat="server"></asp:DropDownList>
                        <asp:Label ID="lblUKM1" runat="server" Text='<%# Bind("displayUKM1") %>' Visible="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-Width="50" HeaderText="Papar UKM2">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_UKM2" runat="server"></asp:DropDownList>
                        <asp:Label ID="lblUKM2" runat="server" Text='<%# Bind("displayUKM2") %>' Visible="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="50" HeaderText="Papar Dropdown">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Ddl" runat="server"></asp:DropDownList>
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
    <asp:Button ID="btnDelYear" runat="server" Text="Buang Tahun" CssClass="fbbutton" />&nbsp;*Sekiranya tahun itu telah digunakan, tahun tidak boleh dibuang.
    <br /><br />
    Nota<br />
    * Kolum Papar UKM1 dan Papar UKM2 adalah untuk memaparkan keputusan peperiksaan UKM1 dan UKM2 masing-masing pada laman Pelajar bagi tahun peperiksaan yang berkaitan. <br />
    * Kolum Papar dropdown adalah untuk mewujudkan/menghilangkan sesi dalam dropdown tahun peperiksaan yang dijumpai dalam kesuluruhan laman admin permatapintar.
</asp:Content>
