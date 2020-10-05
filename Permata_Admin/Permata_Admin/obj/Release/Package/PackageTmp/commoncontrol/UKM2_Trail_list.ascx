<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UKM2_Trail_list.ascx.vb" Inherits="permatapintar.UKM2_Trail_list" %>

<script type="text/javascript" lang="javascript">

    function calSelect(myTextbox) {
        myCal.select(document.getElementById('<%= txtDateCreated.ClientID %>'), 'PERMATApintar', 'yyyyMMdd');
    }

</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian UKM2 Trail
        </td>
    </tr>
    <tr>
        <td>Tahun Ujian:</td>
        <td>
            <asp:DropDownList ID="ddlExamYearUKM2" runat="server" Width="200px">
            </asp:DropDownList></td>
        <td class="fbtd_left">Log Time:
        </td>
        <td>
            <script type="text/javascript" id="myjscal">
                var myCal = new CalendarPopup("calDiv");
                myCal.showNavigationDropdowns();
            </script>
            <asp:TextBox ID="txtDateCreated" runat="server" Width="150px" MaxLength="250"></asp:TextBox>
            <a href="#" onclick="calSelect(this)" title="calSelect(this)" name="PERMATApintar" id="PERMATApintar">
                <img src="img/department-store-emoticon.png" alt="X" width="15" height="15" onclick="calSelect(this)" title="calSelect(this)" />
            </a>

        </td>
    </tr>

    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari  " CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblInfo" runat="server" Text="Nota: Kosongkan Log Time untuk carian semua rekod." ForeColor="Red" Font-Italic="true"></asp:Label></td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>UKM2 Audit Trail.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="UKM2TrailID"
                Width="100%" PageSize="25">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExamStart">
                        <ItemTemplate>
                            <asp:Label ID="ExamStart" runat="server" Text='<%# Bind("ExamStart") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="StudentFullname">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PusatName">
                        <ItemTemplate>
                            <asp:Label ID="PusatName" runat="server" Text='<%# Bind("PusatName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PusatCity">
                        <ItemTemplate>
                            <asp:Label ID="PusatCity" runat="server" Text='<%# Bind("PusatCity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExamStart">
                        <ItemTemplate>
                            <asp:Label ID="ExamStart" runat="server" Text='<%# Bind("ExamStart") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HostAddress">
                        <ItemTemplate>
                            <asp:Label ID="HostAddress" runat="server" Text='<%# Bind("HostAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HostName">
                        <ItemTemplate>
                            <asp:Label ID="HostName" runat="server" Text='<%# Bind("HostName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Browser">
                        <ItemTemplate>
                            <asp:Label ID="Browser" runat="server" Text='<%# Bind("Browser") %>'></asp:Label>
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
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
<asp:Label ID="lblDateCreated" runat="server" Text="" Visible="false"></asp:Label>
<div id="calDiv" style="position: absolute; visibility: hidden; background-color: white;"></div>
