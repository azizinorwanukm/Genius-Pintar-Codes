<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanPeperiksaan_peperiksaanKelasDetail.aspx.vb" Inherits="KPP_MS.admin_laporanPeperiksaan_peperiksaanKelasDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div style="width: 100%; background-color: lightblue;">
        <p style="background-color: #005580; text-align: center">Exam Result Report</p>

        <div style="width: 100%; text-align: center">
            <div class="row" style="background-color: lightblue">
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <asp:Label CssClass="Label" runat="server"> Exam Name : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtexam_Name" Style="width: 97%" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <asp:Label CssClass="Label" runat="server"> Exam Year : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtexam_Year" Style="width: 97%" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <asp:Label CssClass="Label" runat="server"> Class Name : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtclass_Name" Style="width: 97%" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <asp:Label CssClass="Label" runat="server"> Subject Name : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtsubject_Name" Style="width: 97%" runat="server" Text=""></asp:TextBox>
                </div>
            </div>
            <br />
        </div>

        <br />
        <asp:GridView ID="datRespondent" runat="server" class="table table-responsive w3-text-black" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
            BackColor="White" DataKeyNames="student_ID" Width="90%" HeaderStyle-HorizontalAlign="Left">
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Name">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Bind("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student ID">
                    <ItemTemplate>
                        <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Bind("student_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student IC">
                    <ItemTemplate>
                        <asp:Label ID="student_Mykad" class="id1" runat="server" Text='<%# Bind("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grade">
                    <ItemTemplate>
                        <asp:Label ID="grade" class="id1" runat="server" Text='<%# Bind("grade") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#005580" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
        <br />
    </div>
    <br />
</asp:Content>
