<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm1_examstart_datelist.ascx.vb"
    Inherits="permatapintar.ukm1_examstart_datelist" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Ringkasan Ujian UKM1 Mengikut Tarikh&nbsp;&nbsp;[<asp:Label ID="lblDatetime" runat="server"
                Text=""></asp:Label>]<asp:Label ID="lblUserType" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Bulan:<select name="selExamMonth" id="selExamMonth" style="width: 100px;" runat="server">
                <option value="01">01</option>
                <option value="02">02</option>
                <option value="03">03</option>
                <option value="04">04</option>
                <option value="05">05</option>
                <option value="06">06</option>
                <option value="07">07</option>
                <option value="08">08</option>
                <option value="09">09</option>
                <option value="10">10</option>
                <option value="11">11</option>
                <option value="12">12</option>
            </select>&nbsp; Tahun:
            <select name="selExamYear" id="selExamYear" style="width: 100px;" runat="server">
                <option value="2014" selected="selected">2014</option>
            </select>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ExamEnd" Width="100%"
                PageSize="95">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh [YYYYMMDD]">
                        <ItemTemplate>
                            <asp:Label ID="ExamEnd" runat="server" Text='<%# Bind("ExamEnd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JUMLAH MULA#">
                        <ItemTemplate>
                            <asp:Label ID="StudentSUM_NEW" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JUMLAH TAMAT#">
                        <ItemTemplate>
                            <asp:Label ID="StudentSUM_DONE" runat="server" Text=''></asp:Label>
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
&nbsp;
<table class="fbform">
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Show Graph" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Senarai pelajar berdaftar berdasarkan Negeri dan Bandar. Sila pilih sekolah anda."></asp:Label></div>
