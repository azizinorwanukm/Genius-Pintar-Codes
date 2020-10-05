<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="resultMonthly.aspx.vb" Inherits="permatapintar.resultMonthly" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr>
            <td>
                Rekod Penilaian Pelajar
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Tahun / Bulan :
                <select name="year" id="year" style="width: 100px;" runat="server">
                    <option value="" selected="selected">Tahun</option>
                    <option value="2010">2010</option>
                    <option value="2011">2011</option>
                    <option value="2012">2012</option>
                    <option value="2013">2013</option>
                    <option value="2014">2014</option>
                    <option value="2015">2015</option>
                </select>
                <select name="month" id="month" style="width: 95px;" runat="server">
                    <option value="" selected="selected">Bulan</option>
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
                </select><asp:Button ID="btnGenerate" runat="server" Text=" Lihat " CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <table class="fbform">
        <tr>
            <td>
                Pelajar :
                <asp:Label ID="lblFullname" runat="server" Text="" ForeColor="white"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="DateUpdate"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombor ID">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtICnumber" runat="server" Text='<%# Bind("ICnumber") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblICnumber" runat="server" Text='<%# Bind("ICnumber") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tarikh Di Kemaskini">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtdateUpdate" runat="server" Text='<%# Bind("DateUpdate") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbldateUpdate" runat="server" Text='<%# Bind("DateUpdate") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dikemaskini Oleh">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtupdateBy" runat="server" Text='<%# Bind("UpdateBy") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblupdateBy" runat="server" Text='<%# Bind("UpdateBy") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ringkasan">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtRemark" runat="server" Text='<%# Bind("Q001Remarks") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Q001Remarks") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status Penilaian">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtEvalStatus" runat="server" Text='<%# Bind("EvalStatus") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEvalStatus" runat="server" Text='<%# Bind("EvalStatus") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField SelectText="Lihat" ShowSelectButton="true" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" CssClass="cssPager" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                Jumlah Rekod:<asp:Label ID="lblTotal" runat="server" Text="" ForeColor="Black"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Black"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
