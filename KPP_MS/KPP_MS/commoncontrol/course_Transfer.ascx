<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="course_Transfer.ascx.vb" Inherits="KPP_MS.course_Transfer" %>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pelajarTransfer_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_transfer()" value="0">Transfer Course <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="transfer_info">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px">
            <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default ddl" ID="ddl_Year" runat="server" AutoPostBack="false"></asp:DropDownList>
            <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default ddl" ID="ddl_Level" runat="server" AutoPostBack="false"></asp:DropDownList>
            <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default ddl" ID="ddl_Sem" runat="server" AutoPostBack="false"></asp:DropDownList>
            <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default ddl" ID="ddl_type" runat="server" AutoPostBack="false"></asp:DropDownList>
        </div>
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px; margin-bottom: 10px;">
                <asp:Label CssClass="Label" runat="server"> Course Name : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="txtcourse_Name" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / Code"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160; <i class="fa fa-search w3-large w3-text-white"></i></button>
            </div>
        </div>
        <p></p>

        <div style="overflow-y: scroll; overflow-x: hidden; height: 280px" class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#d9d9d9" DataKeyNames="subject_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
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
                    <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="400">
                        <ItemTemplate>
                            <asp:Label ID="subjectName" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="subjectCode" class="id1" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Type" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="subjectType" class="id1" runat="server" Text='<%# Eval("subject_type") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Year" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="subject_studentYear" class="id1" runat="server" Text='<%# Eval("subject_StudentYear") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit Hour" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="subject_credithour" class="id1" runat="server" Text='<%# Eval("subject_CreditHour") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sem" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="subject_sem" class="id1" runat="server" Text='<%# Eval("subject_sem") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
            <asp:DropDownList ID="ddlyear_Transfer" runat="server" AutoPostBack="false" class=" btn btn-default " Style="width: 190px; border-radius: 25px;"></asp:DropDownList>
            <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160; <i class="fa fa-save w3-large w3-text-white"></i></button>
        </div>
        <br />
    </div>
</div>



