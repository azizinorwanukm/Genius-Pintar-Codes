<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="parent_studentHostel.ascx.vb" Inherits="KPP_SYS.parent_studentHostel" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="asrama_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="hostel_info()" value="0">Hostel Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="hostel_info">
        <br />
        <div style="overflow-y: scroll; overflow-x: hidden; height: 300px" class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
                BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
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
                    <asp:TemplateField HeaderText="Hostel Name" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="hostel_Name" class="id1" runat="server" Text='<%# Eval("hostel_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="block_Name" class="id1" runat="server" Text='<%# Eval("block_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Floor" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="block_Level" class="id1" runat="server" Text='<%# Eval("block_Level") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Room No" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="room_Name" class="id1" runat="server" Text='<%# Eval("room_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="year" class="id1" runat="server" Text='<%# Eval("year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>
</div>
<br />
