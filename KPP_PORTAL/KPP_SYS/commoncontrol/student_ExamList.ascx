<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_ExamList.ascx.vb" Inherits="KPP_SYS.student_ExamList" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="periksa_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="exam_info()" value="0">Examination Results <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="exam_info">

        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-3 w3-text-black" style="text-align: left">
                <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left">
                <asp:DropDownList ID="ddlexam" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlexam_SelectedIndexChanged" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
        </div>
        <p></p>
        <div style="overflow-y: scroll;overflow-x: hidden; height: 300px"  class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#d9d9d9" DataKeyNames="ID" BorderStyle="None" GridLines="None"
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
                    <asp:TemplateField HeaderText="Subject Name" ItemStyle-Width="400">
                        <ItemTemplate>
                            <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject Code" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="subject_code" class="id1" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="grade" class="id1" runat="server" Text='<%# Eval("grade") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grades" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="grades" class="id1" runat="server" Text='<%# Eval("gpa") %>'></asp:Label>
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
