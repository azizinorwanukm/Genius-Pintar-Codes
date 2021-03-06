<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Student_marks_koko.ascx.vb" Inherits="KPP_MS.Student_marks_koko" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<%-- Dropdown --%>
<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Search Student Data</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:DropDownList ID="ddl_year" runat="server" AutoPostBack="true" class="btn btn-default font ddl" Style="border-radius: 25px;"></asp:DropDownList>
            <asp:DropDownList ID="ddl_exam" runat="server" AutoPostBack="true" class="btn btn-default font ddl" Style="border-radius: 25px;" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddl_level" runat="server" AutoPostBack="true" class="btn btn-default font ddl" Style="border-radius: 25px;" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddl_class" runat="server" AutoPostBack="true" class="btn btn-default font ddl" Style="border-radius: 25px;" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px; margin-top: 5px;">            
            <asp:DropDownList ID="ddl_coccuriculum" runat="server" AutoPostBack="true" class="btn btn-default font ddl" Style="border-radius: 25px;" OnSelectedIndexChanged="ddlCoccuriculum_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddl_list" runat="server" AutoPostBack="true" class="btn btn-default font ddl" Style="border-radius: 25px;" OnSelectedIndexChanged="ddlList_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student List</p>
    <br />
    <div style="overflow-y: scroll; overflow-x: hidden; height: 500px" class="table-responsive">
        <asp:GridView ID="datRespondentUni1" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
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
                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="450">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Class" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="class_name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Exam Name" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="exam_name" class="id1" runat="server" Text='<%# Eval("exam_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cocurriculum" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="cocurriculum" class="id1" runat="server" Text='<%# Eval("Nama") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Marks"  ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="txtmarks" Width="40px" class="id1" runat="server" Text='<%# Eval("Jumlah") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grade" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="grade" class="id1" runat="server" Text='<%# Eval("Gred") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />

        </asp:GridView>

    </div>
</div>
<p></p>
<asp:HiddenField ID="HiddenField1" runat="server" />
