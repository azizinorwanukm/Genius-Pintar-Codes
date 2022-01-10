<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="homeroom_assessment_remark.ascx.vb" Inherits="KPP_MS.homeroom_assessment_remark" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }

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
        border-radius: 3px;
    }
</style>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 20px" class="w3-card-2">
    <%--Breadcrum--%>
    <div style="padding-top: 10px; padding-left: 15px; padding-bottom: 10px" class="w3-text-black">
        Menu : Homeroom / Student Remark
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 20px;" class="w3-card-2">

    <div style="padding-top: 10px; padding-left: 15px; padding-bottom: 15px; border-bottom: 3px solid #567572FF;">
        <div class="w3-text-black" style="text-align: left; padding-left: 10px; display: inline-block">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlyear" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 20px; display: inline-block;">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Examination : </asp:Label>
            <asp:DropDownList ID="ddlexam_Name" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 20px; display: inline-block">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList ID="ddlLevel" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 20px; display: inline-block;">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Class : </asp:Label>
            <asp:DropDownList ID="ddlClass" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        
    </div>

    <div style="padding-top: 20px; padding-left: 15px; padding-bottom: 10px; overflow-y: scroll; white-space: nowrap; height: 68vh" class="sc4">
        <%--<button id="BtnPrint" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; margin-left: 23px" title="Print">Print &#160;<i class="fa fa-print w3-large w3-text-white"></i></button>--%>

        <div class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
                Width="97%" HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAllEmp(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="600">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student IC" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="student_Mykad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class" ItemStyle-Width="70">
                        <ItemTemplate>
                            <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Examination" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="exam_Name" class="id1" runat="server" Text='<%# Eval("exam_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                            <asp:ImageButton Width="22" Height="22" ID="btnRemark" CommandName="Delete" runat="server" ImageUrl="~/img/plus image 2.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Remark Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>
</div>


<div class="gridViewRespond" id="Answer_Test" runat="server" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c; display: none;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Add Student Remark</p>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px;">
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 15px">
            <asp:Label CssClass="Label" runat="server"> Student Name : </asp:Label>
            <asp:Label CssClass="Label" ID="std_Name" runat="server"></asp:Label>
        </div>
    </div>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px;">
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 15px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Student Mykad : </asp:Label>
            <asp:Label CssClass="Label" ID="std_Mykad" runat="server"></asp:Label>
        </div>
        <div class="col-md-2 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Class : </asp:Label>
            <asp:Label CssClass="Label" ID="std_Class" runat="server"></asp:Label>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Examination : </asp:Label>
            <asp:Label CssClass="Label" ID="std_Exam" runat="server"></asp:Label>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Year : </asp:Label>
            <asp:Label CssClass="Label" ID="std_Year" runat="server"></asp:Label>
        </div>
    </div>

    <br />

    <div style="overflow-y: scroll; overflow-x: hidden; height: 330px" class="table-responsive">
        <asp:GridView ID="GridView1" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None" RowStyle-HorizontalAlign="Left"
            Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Content" ItemStyle-Width="650">
                    <ItemTemplate>
                        <asp:Label ID="description" class="id1" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="Remark ( 1 - 5 )" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:RadioButtonList ID="rbtn_One" runat="server" SelectedValue='<%# Eval("asremark")%>' RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="&nbsp;&nbsp; &nbsp;&nbsp;"></asp:ListItem>
                            <asp:ListItem Value="2" Text="&nbsp;&nbsp; &nbsp;&nbsp;"></asp:ListItem>
                            <asp:ListItem Value="3" Text="&nbsp;&nbsp; &nbsp;&nbsp; "></asp:ListItem>
                            <asp:ListItem Value="4" Text="&nbsp;&nbsp; &nbsp;&nbsp; "></asp:ListItem>
                            <asp:ListItem Value="5" Text="&nbsp;&nbsp; &nbsp;&nbsp; "></asp:ListItem>
                        </asp:RadioButtonList>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>--%>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>

    <div style="text-align: left; padding-left: 20px;">
        <textarea id="txtMessage" runat="Server" rows="5" cols="110" style="color: black" />
    </div>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px; margin-bottom: 5px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
    </div>

    <p></p>

</div>
