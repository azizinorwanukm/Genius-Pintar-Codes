<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_survey_online.aspx.vb" Inherits="permatapintar.admin_survey_online" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Lain-lain><a href="settings.aspx" rel="nofollow" target="_self">Sistem Konfigurasi V2</a>>Survey ID
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnDisplayAdd" runat="server" Text="Tambah Survey ID" CssClass="fbbutton" />&nbsp;
    <asp:TextBox ID="txtYear" runat="server" Width="200px" MaxLength="20" placeholder="contoh, PPCS_DIS_2018" Visible="false"></asp:TextBox>&nbsp;
    <asp:Button ID="btnAddYear" runat="server" Text="Tambah Survey ID" CssClass="fbbutton" Visible="false" />&nbsp;
    <asp:Button ID="btnCancelAdd" runat="server" Text="Batal" CssClass="fbbutton" Visible="false" />&nbsp;
    <br />
    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Font-Bold="true" />&nbsp;
    <br />
    <table>
        <tr>
            <td>Survey ID EQ Test Terkini : </td>
            <td><asp:DropDownList ID="ddlDefault" runat="server"></asp:DropDownList></td>
            <td><asp:Button ID="btnEQ" runat="server" Text="Tukar" CssClass="fbbutton"  /></td>
        </tr>
        <tr>
            <td>Survey ID Sains test Terkini : </td>
            <td><asp:DropDownList ID="ddlSains" runat="server"></asp:DropDownList></td>
            <td><asp:Button ID="btnSains" runat="server" Text="Tukar" CssClass="fbbutton"  /></td>
        </tr>
        <tr>
            <td>Survey ID Stress Test Terkini : </td>
            <td><asp:DropDownList ID="ddlStress" runat="server"></asp:DropDownList></td>
            <td><asp:Button ID="btnStress" runat="server" Text="Tukar" CssClass="fbbutton"  />&nbsp;</td>
        </tr>
    </table>
   
    <br /><br />
    <div>
        <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CellPadding="2" ForeColor="#333333" GridLines="Both" DataKeyNames="master_surveyid" RowDataBound="datRespondent_RowDataBound"
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
                
                <asp:TemplateField HeaderText="Survey ID" >
                    <ItemTemplate>
                        <asp:Label ID="lbl_ExamYear" runat="server" Text='<%# Bind("SurveyID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
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
    <asp:Button ID="btnDelYear" runat="server" Text="Buang Survey ID" CssClass="fbbutton" />&nbsp;*Sekiranya Survey ID itu telah digunakan, ia tidak boleh dibuang.
    <br /><br />
    Nota<br />
    * Kolum Papar dropdown adalah untuk mewujudkan/menghilangkan Survey ID dalam dropdown Survey ID yang dijumpai dalam kesuluruhan laman admin permatapintar.
</asp:Content>
