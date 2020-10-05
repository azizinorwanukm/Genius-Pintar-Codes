<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_alumni.ascx.vb" Inherits="KPP_MS.admin_alumni1" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<script type="text/javascript">

    $(document).ready(function () {
        var accessMenu = document.getElementById('<%= hiddenAccess.ClientID %>').value;

        if (accessMenu == "PPE") {
            document.getElementById('btnReg').style.display = "block";
        }
        else if (accessMenu == "SA") {
            document.getElementById('btnReg').style.display = "block";
        }
    });


</script>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Student Data</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" CssClass="btn btn-default ddl"></asp:DropDownList>        
        <asp:DropDownList ID="ddlLevelnaming" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLevelnaming_SelectedIndexChanged" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlSemnaming" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSemnaming_SelectedIndexChanged" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlClassnaming" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClassnaming_SelectedIndexChanged" CssClass="btn btn-default ddl"></asp:DropDownList>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left">
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtstudent" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / ID / IC"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 13px">
            <p></p>
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
    </div>    
    <p></p>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student List</p>
    <br />

    <div style="overflow-y: scroll;overflow-x: hidden; height: 450px"  class="table-responsive">
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
                <asp:TemplateField ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Image ID="student_Photo" class="id1 w3-circle" runat="server" ImageUrl='<%# Eval("student_Photo") %>' Width="50" Height="40"></asp:Image>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="300">
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
                <asp:TemplateField HeaderText="Student IC" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_IC" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Session " ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_session" class="id1" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Class Name " ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Level" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Sem" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Sem" class="id1" runat="server" Text='<%# Eval("student_Sem") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>--%>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                <%--<asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                    </ItemTemplate>
                </asp:TemplateField>--%>              
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <div id="btnReg" style="display: none">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-bottom: 10px;">
            <button id="btnRegNewStudent" runat="server" type="button" class="btn btn-info" style="background-color: #009900; border-radius: 25px;" title="Add Student">Add &#160;<i class="fa fa-plus w3-large w3-text-white"></i></button>
            <br />
        </div>
    </div>


    <asp:HiddenField ID="hiddenAccess" runat="server" />

</div>