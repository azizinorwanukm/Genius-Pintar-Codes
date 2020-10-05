﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="listCourse.aspx.vb" Inherits="permatapintar.listCourse" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Senarai Kursus
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                Nama Kursus :
                <asp:TextBox ID="searchusername" runat="server" MaxLength="254" Width="250px"></asp:TextBox>&nbsp;<asp:Button
                    ID="btnsearch" runat="server" Text=" Cari " CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="courseID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Kursus">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtcourse" runat="server" Text='<%# Bind("courseName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblcourse" runat="server" Text='<%# Bind("courseName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kod Kursus">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtcourseCode" runat="server" Text='<%# Bind("courseCode") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblcourseCode" runat="server" Text='<%# Bind("courseCode") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
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
                Total records:<asp:Label ID="lblTotal" runat="server" Text="" ForeColor="Black"></asp:Label>
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
