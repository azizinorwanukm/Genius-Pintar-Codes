<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pengarah_examination_slip.ascx.vb" Inherits="KPP_MS.pengarah_examination_slip" %>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>


<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;margin-top:30px">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Information</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Student Name : </asp:Label>
            <asp:TextBox CssClass="txtstudent" ID="txtstudent" Style="width: 100%; border-radius: 25px;" runat="server" placeholder="   Search By Name / ID / IC"></asp:TextBox>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px;">
        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlexam_Name" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlstudent_Year" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl"></asp:DropDownList>
        <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search"><i class="fa fa-search w3-large w3-text-white"></i></button>
    </div>
    <asp:Label ID="LBLttlpercentage" runat="server" class="w3-text-black" Text=""></asp:Label>
    <asp:Label ID="LBLpngs" runat="server" class="w3-text-black" Text=""></asp:Label>
    <p></p>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="gridViewRespond" style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student List</p>
    <br />

    <div style="overflow-y: scroll;overflow-x: hidden; height: 450px"  class="table-responsive">
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
                <asp:TemplateField HeaderText="Name" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IC" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="student_MyKad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Level" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GPA" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="png" class="id1" runat="server" Text='<%# Eval("png") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGPA" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="pngs" class="id1" runat="server" Text='<%# Eval("pngs") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>

    <div class="w3-text-black" style="margin-left: 23px; margin-top: 10px; margin-bottom: 10px">
        <p>Please choose the preferably language :</p>
        <asp:RadioButton ID="rbtn_Malay" Text="Malay &nbsp;&nbsp" runat="server" GroupName="printing_language" />
        <asp:RadioButton ID="rbtn_English" Text="English" runat="server" GroupName="printing_language" />
        <button id="Btnprint" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; margin-left: 23px" title="Print">Print &#160;<i class="fa fa-print w3-large w3-text-white"></i></button>
        <button id="BtnExport" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Print">Export &#160; <i class="fa fa-list-alt w3-large w3-text-white"></i></button>
    </div>
</div>
