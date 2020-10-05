<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pengarah_laporan_peperiksaan_kelas_table.ascx.vb" Inherits="KPP_MS.pengarah_laporan_peperiksaan_kelas_table" %>

<style>
    .ddl {
        border-radius: 25px;
    }

    .centerHeader {
        text-align: center;
    }

    .lblAbsent {
        font-size: 15px;
    }

    .lblAttend {
        font-size: 15px;
    }
</style>

<br />
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Examination Results by Class</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlExam" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" CssClass=" btn btn-default ddl" Style="width: 200px;"></asp:DropDownList>
            <p></p>
        </div>
    </div>

    <div style="overflow-y: scroll; overflow-x: hidden; height: 420px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="exam_ID" BorderStyle="None" GridLines="None"
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

                <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="subjectName" class="id1" runat="server" Text='<%# Eval("course_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Subject Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="subjectCode" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Student" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="StudentName" class="id1" runat="server" Text='<%# Eval("Student Number") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="A+" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aplus" class="id1" runat="server" Text='<%# Eval("A+") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="A" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aa" class="id1" runat="server" Text='<%# Eval("A") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="A-" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aminus" class="id1" runat="server" Text='<%# Eval("A-") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                               
                <asp:TemplateField HeaderText="B+" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aplus" class="id1" runat="server" Text='<%# Eval("B+") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                               
                <asp:TemplateField HeaderText="B" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aa" class="id1" runat="server" Text='<%# Eval("B") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="B-" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aminus" class="id1" runat="server" Text='<%# Eval("B-") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="C+" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aplus" class="id1" runat="server" Text='<%# Eval("C+") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                                
                <asp:TemplateField HeaderText="C" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aa" class="id1" runat="server" Text='<%# Eval("C") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

               <asp:TemplateField HeaderText="D" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aminus" class="id1" runat="server" Text='<%# Eval("D") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="E" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aplus" class="id1" runat="server" Text='<%# Eval("E") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="G" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="aa" class="id1" runat="server" Text='<%# Eval("G") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Student List</p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 420px" class="table-responsive">
        <asp:GridView ID="GridViewStudentList" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
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

                <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="15">
                    <ItemTemplate>
                        <asp:Label ID="StudentID" class="id1" runat="server" Text='<%# Eval("std_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="StudentName" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Student MyKad" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="StudentMyKad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
   
                <asp:TemplateField HeaderText="GPA" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="GPA" class="id1" runat="server" Text='<%# Eval("Total GPA", "{0:n}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
</div>
