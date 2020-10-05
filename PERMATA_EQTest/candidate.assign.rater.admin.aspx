<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="candidate.assign.rater.admin.aspx.vb" Inherits="UKM_eSurvey.candidate_assign_rater_admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Admin: Please proceed to nominate assessors for the following categories:<br />
    </p>
    <table id="mycustomtable">
        <tr>
            <th>
                Assessor
            </th>
            <th>
                Description
            </th>
        </tr>
        <tr>
            <td>
                Manager/ Supervisor
            </td>
            <td>
                The person you report to for work assignments and performance reviews.
            </td>
        </tr>
        <tr class="alt">
            <td>
                Peer
            </td>
            <td>
                Individuals who are at the same organizational level as you, and within the same
                division. The individual may report to a different manager.
            </td>
        </tr>
        <tr>
            <td>
                Internal Customer
            </td>
            <td>
                Individuals who are at the same organizational level as you, but external to your
                division. The output of your work is used as input by the individual.
            </td>
        </tr>
        <tr class="alt">
            <td>
                Direct Subordinate
            </td>
            <td>
                The people who report to you for work assignments and performance reviews.
            </td>
        </tr>
        <tr>
            <td>
                Indirect Subordinate
            </td>
            <td>
                The people who does not report to you for work assignments and performance reviews
                but your decisions directly impact the person. The person should preferably be only
                one organisational level below you.
            </td>
        </tr>
    </table>
    &nbsp;
    <p>
        You may not nominate the same individual twice. Once you have nominated all the
        assessors, please click on the [Confirm] button. Upon confirmation, an email will
        be submitted to the assessors notifying them of your nomination.</p>
    <p>
        Email will only be sent to the new assessor(s) only. Validation for one (1) Manager and minimum one (1) for other groups still will be checked when you click [Confirm].
    </p>
    <p>
        If you have any questions about this online assessment, please feel free to contact
        the administrator at extension #2975.</p>
    <table id="myframe">
        <tr>
            <td>
                1.<asp:TextBox ID="txtSupervisor" runat="server" Text="MANAGER" ReadOnly="true" CssClass="inputlabel"></asp:TextBox>&nbsp;
            </td>
            <td align="right">
                <asp:LinkButton ID="lnkSupervisorAdd" runat="server">Add</asp:LinkButton>&nbsp;|<asp:LinkButton
                    ID="lnkSupervisorRemove" runat="server">Remove</asp:LinkButton>
            &nbsp;</th>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="datSupervisor" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="50" DataKeyNames="UserMarkID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fullname">
                            <ItemTemplate>
                                <asp:Label ID="Fullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="40%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Address">
                            <ItemTemplate>
                                <asp:Label ID="EmailAdd" runat="server" Text='<%# Bind("EmailAdd") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="40%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSupervisor" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    <table id="myframe">
        <tr>
            <td>
                2.<asp:TextBox ID="txtPeers" runat="server" Text="PEERS" ReadOnly="true" CssClass="inputlabel"></asp:TextBox>&nbsp;
            </td>
            <td align="right">
                <asp:LinkButton ID="lnkPeerAdd" runat="server">Add</asp:LinkButton>&nbsp;|<asp:LinkButton
                    ID="lnkPeerRemove" runat="server">Remove</asp:LinkButton>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="datPeer" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="50" DataKeyNames="UserMarkID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fullname">
                            <ItemTemplate>
                                <asp:Label ID="Fullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="40%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Address">
                            <ItemTemplate>
                                <asp:Label ID="EmailAdd" runat="server" Text='<%# Bind("EmailAdd") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="40%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkPeer" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <table id="myframe">
        <tr>
            <td>
                3.<asp:TextBox ID="txtInternalCust" runat="server" Text="INTERNAL CUSTOMERS" ReadOnly="true"
                    CssClass="inputlabel" Width="200px"></asp:TextBox>
            </td>
            <td align="right">
                <asp:LinkButton ID="lnkInternalCustAdd" runat="server">Add</asp:LinkButton>&nbsp;|<asp:LinkButton
                    ID="lnkInternalCustRemove" runat="server">Remove</asp:LinkButton>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="datInternalCust" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="50" DataKeyNames="UserMarkID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fullname">
                            <ItemTemplate>
                                <asp:Label ID="Fullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="40%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Address">
                            <ItemTemplate>
                                <asp:Label ID="EmailAdd" runat="server" Text='<%# Bind("EmailAdd") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="40%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkInternalCust" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <table id="myframe">
        <tr>
            <td>
                4.<asp:TextBox ID="txtDirectSub" runat="server" Text="DIRECT SUBORDINATES" ReadOnly="true"
                    CssClass="inputlabel" Width="200px"></asp:TextBox>
            </td>
            <td align="right">
                <asp:LinkButton ID="lnkDirectSubAdd" runat="server">Add</asp:LinkButton>&nbsp;|<asp:LinkButton
                    ID="lnkDirectSubRemove" runat="server">Remove</asp:LinkButton>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="datDircetSub" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="50" DataKeyNames="UserMarkID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fullname">
                            <ItemTemplate>
                                <asp:Label ID="Fullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="40%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Address">
                            <ItemTemplate>
                                <asp:Label ID="EmailAdd" runat="server" Text='<%# Bind("EmailAdd") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="40%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkDirectSub" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <table id="myframe">
        <tr>
            <td>
                5.<asp:TextBox ID="txtIndirectSub" runat="server" Text="INDIRECT SUBORDINATES" ReadOnly="true"
                    CssClass="inputlabel" Width="200px"></asp:TextBox>
            </td>
            <td align="right">
                <asp:LinkButton ID="lnkIndirectSubAdd" runat="server">Add</asp:LinkButton>&nbsp;|<asp:LinkButton
                    ID="lnkIndirectSubRemove" runat="server">Remove</asp:LinkButton>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="datIndirectSub" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="50" DataKeyNames="UserMarkID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fullname">
                            <ItemTemplate>
                                <asp:Label ID="Fullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="40%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Address">
                            <ItemTemplate>
                                <asp:Label ID="EmailAdd" runat="server" Text='<%# Bind("EmailAdd") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="40%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIndirectSub" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <table id="mysupervisor">
        <tr>
            <td colspan="4">
                <asp:Button ID="btnConfirm" runat="server" Text="Confirm " CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <p>
        <asp:Label ID="lblCandidatename" runat="server" Text="" Visible="false"></asp:Label></p>
</asp:Content>
