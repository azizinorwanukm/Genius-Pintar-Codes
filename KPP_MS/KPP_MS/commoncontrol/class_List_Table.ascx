<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="class_List_Table.ascx.vb" Inherits="KPP_MS.class_List_Table" %>

<style>
    #searchBtn {
        background-color: #005580;
        border-radius: 25px;
    }

    lable {
        color: black;
    }

    h5 {
        color: black;
    }

    #filterLect {
        margin: 5px;
    }

    #filterYear {
        margin: 5px;
    }

    .ddl {
        border-radius: 25px;
    }
</style>

<div id="div_search" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c">
    <p class="gridViewRespond" style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Filter List</p>
    <div class="table w3-text-black gridViewRespond">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top:10px; margin-bottom:5px; text-align: left; padding-left: 23px">
            <asp:DropDownList CssClass="btn btn-default ddl" ID="filterYear" runat="server" AutoPostBack="True" ></asp:DropDownList>
            <asp:DropDownList CssClass="btn btn-default ddl" ID="filterLect" runat="server" AutoPostBack="True" ></asp:DropDownList>
        </div>
    </div>
</div>
<br />

<!--Note: Div yg dibawah closing div dalam file admin_pengurusan_am_kelas, jadi jagan edit / tambah closing tag utk div ini-->
<div id="editClass_info" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c">

    <p class="gridViewRespond" style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Class List</p>
    <br />
    <div style="overflow-y: scroll; overflow-x: hidden; height: 450px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="class_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Class Name" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="class_name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Class Year" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="class_year" class="id1" runat="server" Text='<%# Eval("class_year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Staff ID" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="staff_ID" class="id1" runat="server" Text='<%# Eval("staff_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Staff Name" ItemStyle-Width="350">
                    <ItemTemplate>
                        <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Capacity" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="Std_Number" class="id1" runat="server" Text='<%# Eval("std_number") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px; text-align: left; padding-left: 23px">
            <button id="btnRegNewClass" type="button" class="btn btn-info" style="background-color: #009900; border-radius: 25px;" runat="server">Add Class &#160;<i class="fa fa-plus-circle w3-large w3-text-white"></i></button>
        </div>
    </div>
</div>
