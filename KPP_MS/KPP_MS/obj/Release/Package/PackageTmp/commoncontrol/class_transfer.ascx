<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="class_transfer.ascx.vb" Inherits="KPP_MS.class_transfer" %>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px">
    <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="ddl_Year" runat="server" AutoPostBack="true"></asp:DropDownList>
    <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="ddl_type" runat="server" AutoPostBack="true"></asp:DropDownList>
    <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="ddl_Level" runat="server" AutoPostBack="true"></asp:DropDownList>
    <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="ddl_Sem" runat="server" AutoPostBack="true"></asp:DropDownList>    
</div>
<%--<div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px; margin-bottom: 10px;">
        <asp:Label CssClass="Label" runat="server"> Course Name : </asp:Label>
        <asp:TextBox CssClass="textbox" class="form-control" ID="txtcourse_Name" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / Code"></asp:TextBox>
    </div>
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160; <i class="fa fa-search w3-large w3-text-white"></i></button>
    </div>
</div>--%>
<p></p>

<div style="overflow-y: scroll; overflow-x: hidden; height: 280px" class="table-responsive">
    <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
        BackColor="#d9d9d9" DataKeyNames="class_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
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
            <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Class Name" ItemStyle-Width="200">
                <ItemTemplate>
                    <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Class Year" ItemStyle-Width="200">
                <ItemTemplate>
                    <asp:Label ID="class_Year" class="id1" runat="server" Text='<%# Eval("class_Year") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Class Type" ItemStyle-Width="200">
                <ItemTemplate>
                    <asp:Label ID="class_type" class="id1" runat="server" Text='<%# Eval("class_type") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Class Level" ItemStyle-Width="200">
                <ItemTemplate>
                    <asp:Label ID="class_Level" class="id1" runat="server" Text='<%# Eval("class_Level") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Class Sem" ItemStyle-Width="200">
                <ItemTemplate>
                    <asp:Label ID="class_sem" class="id1" runat="server" Text='<%# Eval("class_sem") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
    </asp:GridView>
</div>

<div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
    <asp:DropDownList ID="ddlyear_Transfer" runat="server" AutoPostBack="false" class=" btn btn-default font " Style="width: 190px; border-radius: 25px;"></asp:DropDownList>
    <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160; <i class="fa fa-save w3-large w3-text-white"></i></button>
</div>
<br />
