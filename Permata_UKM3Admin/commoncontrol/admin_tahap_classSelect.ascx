<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_tahap_classSelect.ascx.vb" Inherits="permatapintar.admin_tahap_classSelect" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<table class="fbform">
    <tr class="fbform_header">
        <td>Pengurusan Am>Menentukan
                <asp:Label ID="lblUserType" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
</table>

<table style="width :100%" class="fbform">
    <tr class="fbform_header">
        <td colspan="4" >Carian Pelajar.
        </td>
    </tr>
    <tr>
        <td style="text-align :right">UKM3 Session :
        </td>
        <td>
            <asp:DropDownList ID="ddlSession" runat="server" Width="200px" AutoPostBack="true"></asp:DropDownList>
        </td>
        <td style="text-align :right">Jantina :
        </td>
        <td>
            <asp:DropDownList ID="ddlJantina" runat="server" Width="200px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="text-align :right">Kelas :
        </td>
        <td>
            <asp:DropDownList ID="ddlClass" runat="server" Width="200px"></asp:DropDownList></td>
        <td style="text-align :right">Kelayakan :
        </td>
        <td>
            <asp:DropDownList ID="ddlPilihKelayakan" runat="server" Width="200px"></asp:DropDownList></td>
    </tr>   
    <tr>
        <td style="text-align :right">Carian:
        </td>
        <td>
            <asp:TextBox ID="txtsearch" runat="server" Width="200px" MaxLength="200" placeholder="Search by Name/MYKAD"></asp:TextBox>

        </td>
    </tr>
    <%-- <tr>
        <td>
            Tahun :
        </td>
        <td>
            <asp:DropDownList ID ="ddlYear" runat ="server" ></asp:DropDownList>
        </td>
    </tr>--%>
      <tr>
        
        <td class="fbform_sap" style="text-align :right">
            Sort By :
            </td>
          <td> <asp:DropDownList runat="server" ID ="ddlSortbys" >
                <asp:ListItem Value ="1">Ujian Stem (Paling Rendah) </asp:ListItem>                
                <asp:ListItem Value ="2">Ujian Stem (Paling Tinggi) </asp:ListItem>
                <asp:ListItem Value ="3">Komposit (Paling Rendah) </asp:ListItem>
                <asp:ListItem value ="4">Komposit (Paling Tinggi)</asp:ListItem>
                      </asp:DropDownList>
        </td>
    </tr>
     <tr>
        <td>Mean STEM (Tahap 1) :
        </td>
        <td>
            <asp:label ID="txtMean" runat="server" Width="200px"></asp:label></td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>


&nbsp;<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Pelajar
        </td>
    </tr>
    <tr>
        <td colspan="2">
            
                <asp:Panel ScrollBars="Both" runat="server" Width ="100%">
                    <div style ="width :300%; height :500px">
                    <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        CellPadding="2" ForeColor="#333333" GridLines="Both" DataKeyNames="id" RowDataBound="datRespondent_RowDataBound"
                        CssClass="gridview_footer">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Pelajar" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_StudentFullname" runat="server" Text='<%# Bind("student_name") %>'></asp:Label>
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
                    <asp:TemplateField HeaderText="Markah UKM2%" >
                        <ItemTemplate >
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
                            <asp:label id="lbl_stem" runat="server" Text='<%# Bind("marks_100") %>'></asp:label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pre-Test">
                        <ItemTemplate>
                            <asp:label ID="txt_preTest" runat="server" Text='<%# Bind("pretest") %>'></asp:label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Post-Test">
                        <ItemTemplate>
                            <asp:label ID="txt_postTest" runat="server" Text='<%# Bind("posttest") %>'></asp:label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Post Test - Pre Test">
                        <ItemTemplate>
                            <asp:label ID="lbl_perbezaan" runat="server" Text='<%# Bind("difference") %>'></asp:label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EQ Test">
                        <ItemTemplate>
                            <asp:label ID="lbl_meqi" runat="server" Text='<%# Bind("meqi") %>'></asp:label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Instr PPcs">
                        <ItemTemplate>
                            <asp:Label ID="instrPPCS" runat="server" Text='<%# Bind("marks_ppcs") %>' ></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Sokong jawapan - setuju/tidak">
                        <ItemTemplate>
                            <asp:Label ID="PPCS_stujutidak" runat="server" Text='<%# Bind("komen_ppcs") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ra PPcs">
                        <ItemTemplate>
                            <asp:Label ID="instrRaPPCS" runat="server" Text='<%# Bind("marks_raPcs") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sokong jawapan - setuju/tidak">
                        <ItemTemplate>
                            <asp:Label ID="RaPPcs_stujutidak" runat="server" Text='<%# Bind("raPcs_komen") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Instr KPP">
                        <ItemTemplate>
                            <asp:Label ID="instrKPP" runat="server" Text='<%# Bind("marks_kpp") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sokong jawapan - setuju/tidak">
                        <ItemTemplate>
                            <asp:Label ID="KPP_stujutidak" runat="server" Text='<%# Bind("kpp_komen") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText ="Markah Komposit" >
                        <ItemTemplate >
                            <asp:Label ID ="lbl_compoMark" runat ="server" Text='<%# Bind("compo") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="50" HeaderText="Kelayakan">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_kelayakan" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblLayak" runat="server" Text='<%# Bind("Layak") %>' Visible="false" />
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
                        </div> 
                </asp:Panel>
            
        </td>
    </tr>

    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnKemaskini" runat="server" Text="Kemaskini Markah Komposit" CssClass ="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
