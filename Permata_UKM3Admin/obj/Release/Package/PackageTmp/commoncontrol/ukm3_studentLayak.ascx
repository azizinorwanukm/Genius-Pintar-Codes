<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm3_studentLayak.ascx.vb" Inherits="permatapintar.ukm3_studentLayak1" %>
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
        <td colspan="4">Carian .
        </td>
    </tr>
    <tr>
        <td>
            UKM3 Session :
        </td>
        <td>
            <asp:dropdownlist ID="ddlSession" runat="server" Width="200px" MaxLength="150" AutoPostBack="true" ></asp:dropdownlist>
        </td>
        <td>
            Jantina :
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlJantina">
                <asp:ListItem Value ="0">--SILA PILIH--</asp:ListItem>
                <asp:ListItem Value ="1">LELAKI</asp:ListItem>                
                <asp:ListItem Value ="2">PEREMPUAN</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Kelas :
        </td>
        <td><asp:DropDownList ID="ddlKelas" runat="server" Width="200px" ></asp:DropDownList></td>
        <td>Program Pendidikan:
        </td>
        <td>
            <asp:DropDownList ID="ddlProgram" runat="server" Width="200px">
                <asp:listitem value="0">--SILA PILIH--</asp:listitem>
                <asp:listitem value="1">ASAS 1</asp:listitem>
                <asp:listitem value="2">TAHAP 1</asp:listitem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Carian Pelajar :
        </td>
        <td>
            <asp:TextBox ID="txt_nama" runat="server" Width="200px" MaxLength="200" placeholder="Nama,IC,Alumni ID" ></asp:TextBox></td>
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
        <td>Senarai Pelajar
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id,student_id"
                Width="100%" PageSize="25" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alumni ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_alumniID" runat="server" Text='<%# Bind("alumniID") %>' ></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jantina">
                        <ItemTemplate>
                            <asp:Label ID="student_sex" runat="server" Text='<%# Bind("StudentGender") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Course">
                        <ItemTemplate>
                            <asp:Label ID="CourseCode" runat="server" Text='<%# Bind("CourseCode")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Kelas">
                        <ItemTemplate>
                            <asp:Label ID="lbl_kelas" runat="server" Text='<%# Bind("ClassCode") %>' ></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Layak">
                        <ItemTemplate>
                            <asp:Label ID="PPMT" runat="server" Text='<%# Bind("PPMT")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StatusTawaran">
                        <ItemTemplate>
                            <asp:Label ID="StatusTawaran" runat="server" Text='<%# Bind("StatusTawaran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Program">
                        <ItemTemplate>
                            <asp:Label ID="Program" runat="server" Text='<%# Bind("Program")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Session">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ujian" runat="server" Text='<%# Bind("sessionName") %>' ></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField> 
                      <%--<asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Papar" />--%>
                
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
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
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    
    <tr>
        <td>
            <asp:Button ID="btnDelete" runat="server" Text="Buang Dari UKM3" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    </table>
<asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>
