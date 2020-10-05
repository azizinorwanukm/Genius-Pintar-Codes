<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_update_coursePlacement.ascx.vb" Inherits="KPP_MS.student_update_coursePlacement" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Course </p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:Label CssClass="Label" runat="server"> Course Name : </asp:Label>
            <asp:DropDownList ID="ddl_courseName" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;border-radius: 25px;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:Label CssClass="Label" runat="server"> Class Name : </asp:Label>
            <asp:DropDownList ID="ddl_className" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;border-radius: 25px;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:Label CssClass="Label" ID="count_student" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            <button id="btnAdd" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search"><i class="fa fa-save w3-large w3-text-white"></i></button>
        </div>
    </div>
</div>
<p></p>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Course List</p>
    <br />
    <div style="overflow-y: scroll; overflow-x: hidden; height: 450px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="course_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="500">
                    <ItemTemplate>
                        <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="subject_code" class="id1" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Type" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="subject_type" class="id1" runat="server" Text='<%# Eval("subject_type") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit Hour" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="subject_CreditHour" class="id1" runat="server" Text='<%# Eval("subject_CreditHour") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
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
<br />
