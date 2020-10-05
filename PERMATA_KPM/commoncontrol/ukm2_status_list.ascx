<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm2_status_list.ascx.vb"
    Inherits="permatapintar.ukm2_status_list" %>

<script type="text/javascript" lang="javascript">

    function calSelect(myTextbox) {
        myCal.select(document.getElementById('<%= txtExamStart.ClientID %>'), 'PERMATApintar', 'yyyyMMdd');
    }

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
        <td class="fbtd_left">Tahun Ujian:
        </td>
        <td>
            <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
        <td>Negeri:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolState" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Status:
        </td>
        <td>
            <select name="selStatus" id="selStatus" style="width: 200px;" runat="server">
                <option value="ALL">ALL</option>
                <option value="NEW">NEW</option>
                <option value="SYSTEM">SYSTEM</option>
                <option value="DONE">DONE</option>
            </select>
        </td>
        <td>Tahun Lahir:
        </td>
        <td>
            <asp:DropDownList ID="ddlDOB_Year" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>Nama Pusat:
        </td>
        <td>
            <asp:TextBox ID="txtPusatName" runat="server" Width="200px" MaxLength="150"></asp:TextBox>
        </td>
        <td>Kehadiran:</td>
        <td>
            <select name="selIsHadir" id="selIsHadir" style="width: 200px;" runat="server">
                <option value="ALL">ALL</option>
                <option value="Y" selected="selected">Y</option>
                <option value="N">N</option>
            </select></td>
    </tr>
    <tr>
        <td>Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>
        </td>
        <td>MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td>Tarikh MULA:</td>
        <td>

            <script type="text/javascript" id="myjscal">
                var myCal = new CalendarPopup("calDiv");
                myCal.showNavigationDropdowns();
            </script>
            <asp:TextBox ID="txtExamStart" runat="server" Width="150px" MaxLength="250"></asp:TextBox>
            <a href="#" onclick="calSelect(this)" title="calSelect(this)" name="PERMATApintar" id="PERMATApintar">
                <img src="img/department-store-emoticon.png" alt="X" width="15" height="15" onclick="calSelect(this)" title="calSelect(this)" />
            </a>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4"></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
            <asp:Label ID="lblInfo" runat="server" Text="[Kosongkan Tarikh Ujian untuk melihat semua rekod]" ForeColor="Red" Font-Italic="true"></asp:Label>&nbsp;
        </td>
        <td style="text-align: right;">Sort By:<select name="selSort" id="selSort" style="width: 200px;" runat="server">
            <option value="0" selected="selected">ExamStart</option>
            <option value="1">UKM2%</option>
        </select></td>
    </tr>
</table>
&nbsp;<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pelajar.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentID"
                Width="100%" PageSize="25" EnableModelValidation="True">
                <RowStyle BackColor="#EFF3FB" />
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
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MULA" SortExpression="ExamStart">
                        <ItemTemplate>
                            <asp:Label ID="ExamStart" runat="server" Text='<%# Bind("ExamStart") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TAMAT">
                        <ItemTemplate>
                            <asp:Label ID="ExamEnd" runat="server" Text='<%# Bind("ExamEnd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STOP Time">
                        <ItemTemplate>
                            <asp:Label ID="ExamStopTime" runat="server" Text='<%# Eval("ExamStopTime", "{0:dd-MM-yyyy hh:mm tt}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IP Address">
                        <ItemTemplate>
                            <asp:Label ID="HostAddress" runat="server" Text='<%# Bind("HostAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="PusatState" runat="server" Text='<%# Bind("PusatState") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Pusat">
                        <ItemTemplate>
                            <asp:Label ID="PusatName" runat="server" Text='<%# Bind("PusatName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOB">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Laman">
                        <ItemTemplate>
                            <asp:Label ID="LastPage" runat="server" Text='<%# Bind("LastPage") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lang">
                        <ItemTemplate>
                            <asp:Label ID="SelectedLang" runat="server" Text='<%# Bind("SelectedLang")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hadir">
                        <ItemTemplate>
                            <asp:Label ID="IsHadir" runat="server" Text='<%# Bind("IsHadir") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Login">
                        <ItemTemplate>
                            <asp:Label ID="IsLogin" runat="server" Text='<%# Bind("IsLogin") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="UKM2%">
                        <ItemTemplate>
                            <asp:Label ID="ukm2TotalPercentage" runat="server" Text='<%# Bind("ukm2TotalPercentage", "{0:n2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList ID="ddlMenudesc" runat="server" Width="200px">
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnExecute" runat="server" Text="Execute" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <select name="selLang" id="selLang" style="width: 200px;" runat="server">
                <option value="">Pilih Bahasa</option>
                <option value="ms-MY">B. MALAYSIA</option>
                <option value="en-US">B. ENGLISH</option>
            </select>
        </td>
    </tr>
</table>

<div id="calDiv" style="position: absolute; visibility: hidden; background-color: white;"></div>
