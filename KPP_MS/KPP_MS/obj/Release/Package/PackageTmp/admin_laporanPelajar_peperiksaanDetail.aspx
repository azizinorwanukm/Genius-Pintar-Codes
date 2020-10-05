<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanPelajar_peperiksaanDetail.aspx.vb" Inherits="KPP_MS.admin_laporanPelajar_peperiksaanDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div style="width: 100%; background-color: lightblue;">
        <p style="background-color: #005580; text-align: center">Exam Result Report</p>

        <div style="width: 100%; text-align: center">
            <div class="row" style="background-color: lightblue">
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <asp:Label CssClass="Label" runat="server"> Student ID : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtstudent_id" Style="width: 97%" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <asp:Label CssClass="Label" runat="server"> Student Name : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtstudent_Name" Style="width: 97%" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <asp:Label CssClass="Label" runat="server"> Class Name : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtclass_Name" Style="width: 97%" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                    <asp:Label CssClass="Label" runat="server"> Exam Name : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtexam_Name" Style="width: 97%" runat="server" Text=""></asp:TextBox>
                </div>
            </div>
            <br />
        </div>

        <br />
        <asp:GridView ID="datRespondent" runat="server" class="table table-responsive w3-text-black" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
            BackColor="White" DataKeyNames="course_ID" Width="90%" HeaderStyle-HorizontalAlign="Left">
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Name">
                    <ItemTemplate>
                        <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Bind("subject_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Code">
                    <ItemTemplate>
                        <asp:Label ID="subject_code" class="id1" runat="server" Text='<%# Bind("subject_code") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Type">
                    <ItemTemplate>
                        <asp:Label ID="subject_type" class="id1" runat="server" Text='<%# Bind("subject_type") %>'></asp:Label>
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

    <asp:Label ID="lblmsg" runat="server" class="w3-text-black"></asp:Label>
</asp:Content>
