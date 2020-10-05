<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_pengajar_penempatan_kursus.aspx.vb" Inherits="KPP_MS.admin_pengajar_penempatan_kursus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div style="width: 100%; background-color: lightblue; text-align: center">
        <p style="background-color: royalblue">Carian Nama Pengajar</p>
        <div class="row" style="background-color: lightblue">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server"> Pengajar ID : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="staff_id" Style="width: 90%" runat="server" Text=""></asp:TextBox>
                <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580"><i class="fa fa-search w3-large w3-text-white"></i></button>
            </div>
            <div class="col-md-6">
                <asp:DropDownList ID="ddlsubject_ID" runat="server" AutoPostBack="false" class="form-control selectpicker show-tick">
                </asp:DropDownList>
            </div>
        </div>
        <br />
    </div>
    <br />

    <div style="width: 100%; background-color: lightblue; text-align: center">
        <p style="background-color: royalblue">Daftar Kursus Pelajar</p>
        <br />

        <asp:GridView ID="datRespondent" runat="server" class="table table-responsive w3-text-black" AutoGenerateColumns="False"
            BackColor="White" DataKeyNames="subject_ID"
            Width="90%" HeaderStyle-HorizontalAlign="Left">
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID Subject">
                    <ItemTemplate>
                        <asp:Label ID="subject_ID" class="id1" runat="server" Text='<%# Bind("subject_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name Kursus">
                    <ItemTemplate>
                        <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Bind("subject_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Pilih" />
            </Columns>
            <HeaderStyle BackColor="#005580" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
        <br />
        <div style="text-align: center">
            <asp:Button ID="btnSimpan" runat="server" class="btn btn-info" Style="background-color: #005580" Text="Simpan" />
        </div>
    </div>

    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
