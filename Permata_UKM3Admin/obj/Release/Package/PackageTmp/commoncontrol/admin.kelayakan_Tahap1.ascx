<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin.kelayakan_Tahap1.ascx.vb" Inherits="permatapintar.admin_kelayakan_Tahap1" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian Pelajar.
        </td>
    </tr>
    <tr>
        <td>
            UKM3 Session :
        </td>
        <td>
            <asp:dropdownlist ID="ddlSession" runat="server" Width="200px" MaxLength="150" ></asp:dropdownlist>
        </td>
    </tr>
   
    <tr>
        <td class="fbtd_left">Pencarian:
        </td>
        <td>
            <asp:TextBox ID="txtsearch" runat="server" Width="200px" MaxLength="200" placeholder="Search by Name/MYKAD"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnKembali" runat="server" Text="Kembali" CssClass="fbbutton"/>
        </td>
    </tr>
</table>
&nbsp;<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pelajar
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel scrollbars="Both" runat="server" size="auto">
                   <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                CellPadding="2" ForeColor="#333333" GridLines="Both" DataKeyNames="id" RowDataBound ="datRespondent_RowDataBound"
                 PageSize="25" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Pelajar" >
                        <ItemTemplate>
                            <asp:Label  width="120" ID="lbl_StudentFullname" runat="server" Text='<%# Bind("student_name") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. Kad Pengenalan">
                        <ItemTemplate>
                            <asp:Label ID="lbl_MYKAD" runat="server" Text='<%# Bind("student_Mykad") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alumni ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_alumniID" runat="server" Text='<%# Bind("AlumniID") %>' ></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kelas">
                        <ItemTemplate>
                            <asp:Label ID="lbl_kelas" runat="server" Text='<%# Bind("classcode") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  />
                        <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Skor UKM2">
                        <ItemTemplate>
                             <asp:label id="lbl_ukm2percentage" runat="server" text='<%# Bind("ukm2TotalPercentage", "{0:n2}") %>'></asp:label>
                         </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  />
                        <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mental Age UKM2">
                        <ItemTemplate>
                            <asp:Label ID="lbl_mentalAge" runat="server" Text='<%# Bind("mentalAge") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  />
                        <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ujian STEM">
                        <ItemTemplate>
                            <asp:label Width ="35px" ID="txt_stem" runat="server" Text='<%# Bind("marks_100") %>'></asp:label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pre-Test">
                        <ItemTemplate>
                            <asp:label Width ="35px" ID="txt_preTest" runat="server" Text='<%# Bind("pretest") %>'></asp:label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Post-Test">
                        <ItemTemplate>
                            <asp:label Width ="35px" ID="txt_postTest" runat="server" Text='<%# Bind("posttest") %>'></asp:label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Post Test - Pre Test">
                        <ItemTemplate>
                            <asp:label Width ="35px" ID="lbl_perbezaan" runat="server" Text='<%# Bind("difference") %>'></asp:label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EQ Test">
                        <ItemTemplate>
                            <asp:label Width ="35px" ID="lbl_meqi" runat="server" Text='<%# Bind("meqi") %>'></asp:label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Instr PPcs">
                        <ItemTemplate>
                            <asp:Label Width ="50px" ID="instrPPCS" runat="server" Text='<%# Bind("marks_ppcs") %>' ></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Sokong jawapan - setuju/tidak">
                        <ItemTemplate>
                            <asp:Label  Width="500px" ID="PPCS_stujutidak" runat="server" Text='<%# Bind("komen_ppcs") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ra PPcs">
                        <ItemTemplate>
                            <asp:Label Width ="50px" ID="instrRaPPCS" runat="server" Text='<%# Bind("marks_raPcs") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sokong jawapan - setuju/tidak">
                        <ItemTemplate>
                            <asp:Label Width="500px" ID="RaPPcs_stujutidak" runat="server" Text='<%# Bind("raPcs_komen") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Instr KPP">
                        <ItemTemplate>
                            <asp:Label Width ="50px" ID="instrKPP" runat="server" Text='<%# Bind("marks_kpp") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sokong jawapan - setuju/tidak">
                        <ItemTemplate>
                            <asp:Label Width="500px" ID="KPP_stujutidak" runat="server" Text='<%# Bind("kpp_komen") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText ="Markah Komposit" >
                        <ItemTemplate >
                            <asp:Label ID ="lbl_compoMark" runat ="server" Text="0"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="50" HeaderText="Kelayakan" >
                        <ItemTemplate>
                            <asp:Label ID="lblLayak" runat="server" Text='<%# Bind("Layak") %>'/>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                              
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
         </asp:Panel>                
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
    </tr>
</table>
<asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>