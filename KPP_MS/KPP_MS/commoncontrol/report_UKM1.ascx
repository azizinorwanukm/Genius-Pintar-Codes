<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="report_UKM1.ascx.vb" Inherits="KPP_MS.report_UKM1" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">UKM 1 Data</p>
    <br />

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px; margin-bottom: 5px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="btn btn-default font ddl"></asp:DropDownList>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 5px; text-align: left;">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtstudent" Style="width: 85%; border-radius: 25px;" runat="server" Text="" placeholder="   By Name / Mykad / Alumni ID"></asp:TextBox>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 13px">
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
    </div>

    <div style="overflow-y: scroll; overflow-x: scroll; height: 450px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
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
                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Mykad" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Mykad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Alumni ID" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="AlumniID" class="id1" runat="server" Text='<%# Eval("AlumniID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Year " ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="DOB_Year" class="id1" runat="server" Text='<%# Eval("DOB_Year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Religion" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Religion" class="id1" runat="server" Text='<%# Eval("student_Religion") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Code" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="SchoolCode" class="id1" runat="server" Text='<%# Eval("SchoolCode") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Exam Start" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="ExamStart" class="id1" runat="server" Text='<%# Eval("ExamStart") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Exam End" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="ExamEnd" class="id1" runat="server" Text='<%# Eval("ExamEnd") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Question Year" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="QuestionYear" class="id1" runat="server" Text='<%# Eval("QuestionYear") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Score" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="TotalScore" class="id1" runat="server" Text='<%# Eval("TotalScore") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UKM1%" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="TotalPercentage" class="id1" runat="server" Text='<%# Eval("TotalPercentage", "{0: F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ModA" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="ModA" class="id1" runat="server" Text='<%# Eval("ModA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ModB" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="ModB" class="id1" runat="server" Text='<%# Eval("ModB") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ModC" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="ModC" class="id1" runat="server" Text='<%# Eval("ModC") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

    <asp:HiddenField ID="hiddenAccess" runat="server" />

</div>
