﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_config.ascx.vb" Inherits="KPP_SYS.Disiplin_config" %>

<style>
    .ddl {
        border-radius: 25px;
    }

    .CalendarCssClass {
        background-color: #990000;
        font-family: Century;
        text-transform: lowercase;
        width: 750px;
        border: 1px solid Olive;
    }
</style>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Register New Discipline Detail</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <!-- case name-->
            <asp:Label CssClass="Label" runat="server"> Case Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="case_Name" Style="width: 50%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
    </div>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <!-- Merit-->
            <asp:Label CssClass="Label" runat="server"> Demerit (Points) : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="Merit" Style="width: 50%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <!-- case name-->
            <asp:Label CssClass="Label" runat="server"> Compound Charge (RM) : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="Compound" Style="width: 50%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>

    </div>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;"><i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
</div>
<br />


<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px"> Discipline List</p>
    <br />

    <div style="overflow-y: scroll; overflow-x: hidden; height: 450px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="9"
            BackColor="#d9d9d9" DataKeyNames="case_ID" BorderStyle="None" GridLines="None"
            Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="case Name" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="case_name" class="id1" runat="server" Text='<%# Eval("case_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="case_box" Text='<%# Eval("case_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Demerit (Points)" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="Merit" class="id1" runat="server" Text='<%# Eval("merit") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="merit_box" Text='<%# Eval("merit") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Compound (RM)" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="compound" class="id1" runat="server" Text='<%# Eval("compound") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="compound_box" Text='<%# Eval("compound") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnEdit" CommandName="Edit" runat="server" ImageUrl="~/img/edit-11-512.png" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton runat="server" CommandName="Update" Text="Update" ImageUrl="~/img/update.png" Width="22" Height="22" />
                        <asp:ImageButton runat="server" Text="Cancel" CommandName="Cancel" ImageUrl="~/img/cancel.png" Width="22" Height="22" />
                    </EditItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="10">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />


        </asp:GridView>
    </div>


</div>
