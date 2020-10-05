<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="homeroom_assessment_remark.ascx.vb" Inherits="KPP_MS.homeroom_assessment_remark" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
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

<div class="gridViewRespond" id="Search_Test" style="width: 100%; background-color: #f2f2f2; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; text-align: center; width: 100%; border-radius: 25px">Search Information</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-left:13px">
        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlexam_Name" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl"></asp:DropDownList>
        <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
    </div>
    <p></p>

    <div style="overflow-y: scroll; overflow-x: hidden; height: 350px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None" RowStyle-HorizontalAlign="Left"
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
                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="300">
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
                        <asp:ImageButton Width="22" Height="22" ID="btnRemark" CommandName="Delete" runat="server" ImageUrl="~/img/update.png" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>

    <div class="w3-text-black" style="margin-left: 13px; margin-top: 10px; margin-bottom: 10px">
<%--        <p>Please choose the preferably language :</p>
        <asp:RadioButton ID="rbtn_Malay" Text="Malay &nbsp;&nbsp" runat="server" GroupName="printing_language" />
        <asp:RadioButton ID="rbtn_English" Text="English" runat="server" GroupName="printing_language" />--%>
        <button id="BtnPrint" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; margin-left: 23px" title="Print">Print &#160;<i class="fa fa-print w3-large w3-text-white"></i></button>
    </div>

</div>
<br />

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
                <asp:TemplateField HeaderText="Remark ( 1 - 5 )" ItemStyle-Width="200">
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
                </asp:TemplateField>
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
