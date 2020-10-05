<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/nocheck.Master"
    CodeBehind="ukm2.student.search.aspx.vb" Inherits="permatapintar.ukm2_student_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table class="tableform">
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 5%;">
                &nbsp;
            </td>
            <td colspan="2">
                <%--<a href="#">
                    <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
                        height="35" border="0" class="logo" /></a>--%>
                <h1>Araken I-PROFILE</h1>

            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="vertical-align: top;" colspan="2">
                <h2>
                    <asp:Label ID="lblcourse_01" runat="server" Text="Semakan Kelayakan ke Ujian Saringan"
                        CssClass="lblHeader"></asp:Label></h2>
                <asp:Label ID="lbl15_instruction" runat="server" Text="*Nota: Masukkan MYKAD/MYKID/Surat Beranak. Tanpa tanda sempang atau ruang kosong."></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                *MYKAD/MYKID/Surat Beranak#:<asp:TextBox ID="txttokenid" runat="server" Width="250px"
                    MaxLength="250"></asp:TextBox>&nbsp;
                <asp:Button ID="btnSubmit" runat="server" Text="Semak >> " CssClass="mybutton" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="Tokenid"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <asp:Label ID="lableIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student ID">
                            <ItemTemplate>
                                <asp:Label ID="lblTokenid" runat="server" Text='<%# Bind("Tokenid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Penuh">
                            <ItemTemplate>
                                <asp:Label ID="lblRespFullname" runat="server" Text='<%# Bind("RespFullname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kod Sekolah">
                            <ItemTemplate>
                                <asp:Label ID="lblLastPage" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Sekolah">
                            <ItemTemplate>
                                <asp:Label ID="lblExamStart" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Negeri">
                            <ItemTemplate>
                                <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolState") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tel#">
                            <ItemTemplate>
                                <asp:Label ID="lblExamEnd" runat="server" Text='<%# Bind("ParentContactNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Penjaga">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("ParentFullname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pusat Ujian">
                            <ItemTemplate>
                                <asp:Label ID="lblExamLocation" runat="server" Text='<%# Bind("ExamLocation") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tarikh & Masa">
                            <ItemTemplate>
                                <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("ExamDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
    </table>
    
</asp:Content>
