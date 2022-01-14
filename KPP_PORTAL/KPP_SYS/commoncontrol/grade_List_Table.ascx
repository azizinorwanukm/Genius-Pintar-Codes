<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="grade_List_Table.ascx.vb" Inherits="KPP_SYS.grade_List_Table" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Grade List</p>
    <br />

    <div  class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="15"
            BackColor="#d9d9d9" DataKeyNames="grade_ID" BorderStyle="None" GridLines="None" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating"
            Width="97%" RowStyle-HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grade" >
                    <ItemTemplate >
                        <asp:Label ID="grade_Name" class="id1" runat="server" Text='<%# Eval("grade_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtgrade_Name" width="80px" class="id1" runat="server" Text='<%# Eval("grade_Name") %>'/>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lowest" >
                    <ItemTemplate>
                        <asp:Label ID="grade_min_range" class="id1" runat="server" Text='<%# Eval("grade_min_range") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtgrade_min_range" width="80px" class="id1" runat="server" Text='<%# Eval("grade_min_range") %>'/>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Highest">
                    <ItemTemplate>
                        <asp:Label ID="grade_max_range" class="id1" runat="server" Text='<%# Eval("grade_max_range") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtgrade_max_range" width="80px" class="id1" runat="server" Text='<%# Eval("grade_max_range") %>'/>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GPA">
                    <ItemTemplate>
                        <asp:Label ID="gpa" class="id1" runat="server" Text='<%# Eval("gpa") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtgpa" width="80px" class="id1" runat="server" Text='<%# Eval("gpa") %>'/>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" UpdateImageUrl="~/img/save.png" CancelImageUrl="~/img/cancel.png"  ControlStyle-Width="22px" ControlStyle-Height="22px" />
                <asp:TemplateField HeaderText="Delete">
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