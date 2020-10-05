<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_offer_status.ascx.vb"
    Inherits="permatapintar.ppcs_offer_status" %>

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
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td>Sessi PPCS:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Status PPCS:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSStatus" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
        <td>Status Tawaran:
        </td>
        <td>
            <select name="selStatusTawaran" id="selStatusTawaran" style="width: 200px;" runat="server">
                <option value="ALL">ALL</option>
                <option value="TERIMA" selected="selected">TERIMA</option>
                <option value="TOLAK">TOLAK</option>
            </select>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Jantina:
        </td>
        <td>
            <select name="selStudentGender" id="selStudentGender" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="LELAKI">LELAKI</option>
                <option value="PEREMPUAN">PEREMPUAN</option>
            </select>
        </td>
        <td>Tahun Lahir:
        </td>
        <td>
            <asp:DropDownList ID="ddlDOB_Year" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
        </td>
        <td>Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="200px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
        <td style="text-align: right;">Sort By:<select name="selSort" id="selSort" style="width: 200px;" runat="server">
            <option value="0">Tarikh</option>
            <option value="1" selected="selected">Fullname</option>
        </select></td>
    </tr>
</table>
&nbsp;<asp:Label ForeColor="Red" ID="lblMsgTop" runat="server" Text=""></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pelajar.&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentID"
                Width="100%" PageSize="25">
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
                    <asp:TemplateField HeaderText="Fullname">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AlumniID">
                        <ItemTemplate>
                            <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("AlumniID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Race">
                        <ItemTemplate>
                            <asp:Label ID="StudentRace" runat="server" Text='<%# Bind("StudentRace") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Religion">
                        <ItemTemplate>
                            <asp:Label ID="StudentReligion" runat="server" Text='<%# Bind("StudentReligion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student City">
                        <ItemTemplate>
                            <asp:Label ID="StudentCity" runat="server" Text='<%# Bind("StudentCity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student State">
                        <ItemTemplate>
                            <asp:Label ID="StudentState" runat="server" Text='<%# Bind("StudentState") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="D?">
                        <ItemTemplate>
                            <asp:Label ID="DisplayStatus" runat="server" Text='<%# Bind("DisplayStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status PPCS">
                        <ItemTemplate>
                            <asp:Label ID="PPCSStatus" runat="server" Text='<%# Bind("PPCSStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status Tawaran">
                        <ItemTemplate>
                            <asp:Label ID="StatusTawaran" runat="server" Text='<%# Bind("StatusTawaran") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="StatusDate" runat="server" Text='<%# Bind("StatusDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sebab Tolak">
                        <ItemTemplate>
                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                <asp:Label ID="StatusReason" runat="server" Text='<%# Bind("StatusReason") %>' ToolTip='<%#Bind("StatusReason")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catatan">
                        <ItemTemplate>
                            <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                <asp:Label ID="lblCatatan" runat="server" Text='<%# Bind("Catatan") %>' ToolTip='<%#Bind("Catatan")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Kursus">
                        <ItemTemplate>
                            <asp:Label ID="CourseCode" runat="server" Text='<%# Bind("CourseCode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Papar" />

                    
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

    <tr>
        <td>Nota: D=Display Status? Y/N selected students to view the PPCS offer status in SEMAK website.<br />
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList ID="ddlMenudesc" runat="server" Width="200px">
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnExecute" runat="server" Text="Execute" CssClass="fbbutton" />&nbsp;
            Sebab Tolak:&nbsp;<asp:TextBox ID="txtStatusReason" runat="server" Width="300px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkALL" Text="Reset All Display Status" runat="server" />
        </td>
    </tr>
</table>

