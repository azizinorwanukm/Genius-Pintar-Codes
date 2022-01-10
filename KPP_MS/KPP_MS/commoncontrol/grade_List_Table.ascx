<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="grade_List_Table.ascx.vb" Inherits="KPP_MS.grade_List_Table" %>

<style>
    .sc3::-webkit-scrollbar {
        height: 10px;
    }

    .sc3::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc3::-webkit-scrollbar-thumb {
        background-color: #929B9E;
        border-radius: 3px;
    }

    .sc4::-webkit-scrollbar {
        width: 10px;
    }

    .sc4::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc4::-webkit-scrollbar-thumb {
        background-color: #929B9E;
    }
</style>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black font">
        Menu &nbsp; : &nbsp; General Management &nbsp; / &nbsp; Grade Management
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 76vh" class="sc4">

        <div style="overflow-y: scroll; height: 73vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" 
                BackColor="#FFFAFA" DataKeyNames="grade_ID" BorderStyle="None" GridLines="None" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating"
                Width="97%" RowStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Width="80">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade"  ItemStyle-Width="23%">
                        <ItemTemplate>
                            <asp:Label ID="grade_Name" class="id1" runat="server" Text='<%# Eval("grade_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgrade_Name" Width="80px" class="id1" runat="server" Text='<%# Eval("grade_Name") %>' />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lowest"  ItemStyle-Width="23%">
                        <ItemTemplate>
                            <asp:Label ID="grade_min_range" class="id1" runat="server" Text='<%# Eval("grade_min_range") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgrade_min_range" Width="80px" class="id1" runat="server" Text='<%# Eval("grade_min_range") %>' />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Highest"  ItemStyle-Width="23%">
                        <ItemTemplate>
                            <asp:Label ID="grade_max_range" class="id1" runat="server" Text='<%# Eval("grade_max_range") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgrade_max_range" Width="80px" class="id1" runat="server" Text='<%# Eval("grade_max_range") %>' />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GPA"  ItemStyle-Width="23%">
                        <ItemTemplate>
                            <asp:Label ID="gpa" class="id1" runat="server" Text='<%# Eval("gpa") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgpa" Width="80px" class="id1" runat="server" Text='<%# Eval("gpa") %>' />
                        </EditItemTemplate>   
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" UpdateImageUrl="~/img/correct image 2.png" CancelImageUrl="~/img/minus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton Width="25" Height="25" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Grade Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>
</div>
