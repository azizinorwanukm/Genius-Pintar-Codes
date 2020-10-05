<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_exam.ascx.vb" Inherits="permatapintar.student_exam" %>

<style type="text/css">
    .auto-style1 {
        width: 17px;
    }
    .auto-style2 {
        width: 4px;
    }
</style>

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
            <td colspan="10">Paparan Markah
            </td>
        </tr>
    
     <tr>
        <td>
            UKM3 Session 
        </td>
         <td>:</td> 
        <td>
            <asp:dropdownlist ID="ddlSession" runat="server" Width="200px" MaxLength="150" AutoPostBack="true" ></asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td>
            Kelas
        </td>
        <td>:</td>
        <td><asp:DropDownList ID="ddlClass" runat="server" Width="200px" ></asp:DropDownList></td>
    </tr>
    
  
    <tr>
        <td class="fbtd_left" ><asp:label CssClass="align-left" runat="server">Carian</asp:label>
        </td>
          <td class="auto-style2">:</td>
        <td class="auto-style1">
            <asp:TextBox ID="txtsearch" runat="server" Width="237px" MaxLength="200" placeholder="Search by Name/ID/IC"></asp:TextBox></td>
    </tr>
<%--    <tr>
          <td class="fbtd_left">Markah Instruktor</td>
        <td class="auto-style2">:</td>
            <td class="auto-style1"><asp:DropDownList ID="ddlmarkahins" runat="server" Width="250px">
                </asp:DropDownList></td>
    </tr>--%>
    <tr>
           <td colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
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
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id"
                Width="100%" PageSize="25" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("student_Mykad") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alumni ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_alumniID" runat="server" Text='<%# Bind("AlumniID") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class">
                        <ItemTemplate>
                            <asp:Label ID="ClassCode" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Markah /80">
                        <ItemTemplate>
                            <asp:Label ID="ClassCode" runat="server" Text='<%# Bind("marks_80") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Markha /100">
                        <ItemTemplate>
                            <asp:Label ID="ClassCode" runat="server" Text='<%# Bind("marks_100") %>'></asp:Label>
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
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
    </tr>
</table>
     

