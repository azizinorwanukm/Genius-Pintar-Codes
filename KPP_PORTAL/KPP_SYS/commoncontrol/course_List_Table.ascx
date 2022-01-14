<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="course_List_Table.ascx.vb" Inherits="KPP_SYS.course_List_Table" %>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Course Data</p>
    <div class="table w3-text-black gridViewRespond">
        <div class="form-group" style="padding-right: 10px; background-color: #f2f2f2; display: inline-block; width: 100%; margin-bottom: 5px;">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
                <asp:TextBox ID="searchTextBox" CssClass="textbox" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="by Course Name or Course Code"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 10px">
                <p></p>
                <asp:LinkButton runat="server" ID="searchBtn" Text="<i class='fa fa-search w3-large w3-text-white'></i>" ValidationGroup="edt" OnClick="searchBtn_Click" CssClass="btn btn-info" Style="border-radius: 25px; background-color: #005580;" />
            </div>
        </div>
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top:20px ;margin-bottom: 10px; text-align: left; padding-left: 23px">
            <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="filterSems" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filterSems_Changed"></asp:DropDownList>

            <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="filterFoundationLvl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filterFoundationLvl_Changed"></asp:DropDownList>

            <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="filterType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filterType_Changed"></asp:DropDownList>

            <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="filterYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filterYear_Changed"></asp:DropDownList>
        </div>
    </div>
    <p></p>
</div>
<br />

<!--Note: Div yg dibawah closing div dalam file admin_pengurusan_am_kursus, jadi jagan edit / tambah clos tag urk div ini-->
<div id="editCourse_info" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c">

    <p class="gridViewRespond" style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Course List</p>
    <br />

    <div style="overflow-y: scroll; overflow-x:hidden; height: 450px" class="table-responsive" id="displayData">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False" AllowPaging="True" PageSize="9"
            BackColor="#d9d9d9" DataKeyNames="subject_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="subjectName" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="subjectCode" class="id1" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="subjectType" class="id1" runat="server" Text='<%# Eval("subject_type") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Student Year" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="subject_studentYear" class="id1" runat="server" Text='<%# Eval("subject_StudentYear") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Credit Hour" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="subject_credithour" class="id1" runat="server" Text='<%# Eval("subject_CreditHour") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sem" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="subject_sem" class="id1" runat="server" Text='<%# Eval("subject_sem") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="std_Number" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="std_Number" class="id1" runat="server" Text='<%# Eval("std_number") %>'></asp:Label>
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
    </div>
    <p></p>
