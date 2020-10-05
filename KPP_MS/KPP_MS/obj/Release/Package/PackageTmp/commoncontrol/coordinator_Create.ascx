<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="coordinator_Create.ascx.vb" Inherits="KPP_MS.coordinator_Create" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Register Coordinator</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px;">
        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlstaff" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px;">
        <asp:DropDownList ID="ddlcourse" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlsubject" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddllevel" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Backs &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
</div>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; border-radius: 25px; border: 5px solid #8c8c8c;margin-top:10px">
    <p class="gridViewRespond" style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Coordinator List</p>
    <br />

    <div style="overflow-y: scroll; overflow-x: hidden; height: 340px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="coordinator_ID" BorderStyle="None" GridLines="None" RowStyle-HorizontalAlign="Left"
            Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>                
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Level" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="level" class="id1" runat="server" Text='<%# Eval("coordinator_Level") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="year" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="year" class="id1" runat="server" Text='<%# Eval("year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>
</div>

