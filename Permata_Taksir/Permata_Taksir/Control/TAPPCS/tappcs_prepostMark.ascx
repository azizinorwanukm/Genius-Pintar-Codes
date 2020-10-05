<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="tappcs_prepostMark.ascx.vb" Inherits="UKM3.tappcs_prepostMark" %>

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
        <td>
            PPCS Date :
        </td>
        <td>
            <asp:dropdownlist ID="ddlPpcsDate" runat="server" Width="200px" MaxLength="150" ></asp:dropdownlist>
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
</table>


    &nbsp;<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table class="fbform">
       <tr class="fbform_header">
            <td colspan="2">Senarai Pelajar
            </td>
        </tr>
    <tr>
        <td colspan="2">
            <div style ="width:1030px;">
                   <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                CellPadding="2" ForeColor="#333333" GridLines="Both" DataKeyNames="student_ID" 
                 PageSize="25" CssClass="gridview_footer" >
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
                    <asp:TemplateField HeaderText="Nama Pelajar">
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
                    <asp:TemplateField HeaderText="Pre-Test">
                        <ItemTemplate>
                            <asp:TextBox Width ="35px" ID="txt_preTest" runat="server" Text='<%# Bind("pretest") %>' ></asp:TextBox>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Post-Test">
                        <ItemTemplate>
                            <asp:TextBox Width ="35px" ID="txt_postTest" runat="server" Text='<%# Bind("posttest") %>'></asp:TextBox>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Post Test - Pre Test">
                        <ItemTemplate>
                            <asp:label Width ="35px" ID="lbl_perbezaan" runat="server" text='<%# Bind("difference") %>'></asp:label>
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

    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini Markah" CssClass="fbbutton" />&nbsp;
        </td>
        <td><asp:Label runat ="server" Text="(Sila Tandakan CheckBox untuk mengemaskini markah pelajar yang dikehendaki)" ForeColor ="Red" Font-Bold ="true" ></asp:Label></td>
    </tr>
</table>