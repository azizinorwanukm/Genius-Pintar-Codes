<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="hostel_list_managae.ascx.vb" Inherits="KPP_MS.hostel_list_managae" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Hostel Data</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; margin-bottom: 5px; text-align: left; padding-left: 23px">
        <%--<asp:DropDownList ID="ddlHostelName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHostelName_SelectedIndexChanged" CssClass="btn btn-default ddl"></asp:DropDownList>--%>
        <asp:DropDownList ID="ddlBlockName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlockName_SelectedIndexChanged" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlBlockLevel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlockLevel_SelectedIndexChanged" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlYearList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYearList_SelectedIndexChanged" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlSemList" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
    </div>
    <p></p>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Hostel List</p>
    <br />

    <div style="overflow-y: scroll; overflow-x: hidden; height: 450px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
            BackColor="#d9d9d9" DataKeyNames="hostel_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Block" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="hostel_BlockNames" class="id1" runat="server" Text='<%# Eval("HostelName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Floor" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="hostel_BlockLevels" class="id1" runat="server" Text='<%# Eval("HostelLevel") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Room No" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="hostel_RoomNumbers" class="id1" runat="server" Text='<%# Eval("hostel_RoomNumbers") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Year" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="year" class="id1" runat="server" Text='<%# Eval("year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Semester" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="sem" class="" runat="server" Text='<%# Eval("HostelSem") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label align="center" runat="server" Class="id1">No hostel registered</asp:Label>
            </EmptyDataTemplate>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

</div>