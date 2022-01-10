<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_Create_Invoice.ascx.vb" Inherits="KPP_MS.payment_Create_Invoice" %>

<style>
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
    }
</style>

<script type="text/javascript">
    function ShowMessage(message, messagetype) {
        var cssclass;
        switch (messagetype) {
            case 'Success':
                cssclass = 'alert-success'
                break;
            case 'Error':
                cssclass = 'alert-danger'
                break;
            default:
                cssclass = 'alert-info'
        }
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; text-align:left -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        setTimeout(function () {
            $("#alert_div").fadeTo(5000, 500).slideUp(500, function () {
                $("#alert_div").remove();
            });
        }, 3000);
    }
</script>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }

    function CheckAllStudent(Checkbox) {
        var GridVwHeaderChckboxStd = document.getElementById("<%=datStdRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckboxStd.rows.length; i++) {
            GridVwHeaderChckboxStd.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Payment &nbsp; / &nbsp; Payment Management &nbsp; / &nbsp;
        <asp:HyperLink runat="server" ID="previousPage"> View Payment Items </asp:HyperLink>
        &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF;">
        <button id="btnGenerateInvoice" runat="server" style="display: inline-block; font-size: 0.8vw">Generate Invoice </button>
        <button id="btnViewInvoice" runat="server" style="display: inline-block; font-size: 0.8vw">View Invoice </button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="GenerateInvoice" runat="server" class="sc4">

        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlYear_List" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList ID="ddlLevel_List" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 23vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="FIM_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAllEmp(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="FIM_Year" class="id1" runat="server" Text='<%# Eval("FIM_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="FIM_Item" class="id1" runat="server" Text='<%# Eval("FIM_Item") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="FIM_Quantity" class="id1" runat="server" Text='<%# Eval("FIM_Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price (RM)" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="FIM_Price" class="id1" runat="server" Text='<%# Eval("FIM_Price") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark" ItemStyle-Width="35%">
                        <ItemTemplate>
                            <asp:Label ID="FIM_Remark" class="id1" runat="server" Text='<%# Eval("FIM_Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Fee Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <br />

        <div style="overflow-y: scroll; height: 26vh" class="table-responsive sc4 font">
            <asp:GridView ID="datStdRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="std_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectStudent" Text="" runat="server" onclick="CheckAllStudent(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckStudent" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Campus" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="Parameter" class="id1" runat="server" Text='<%# Eval("Parameter") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <button id="Btnsimpan" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Generate Student Invoice</button>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="ViewInvoice" runat="server" class="sc4">

        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddl_VI_year" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList ID="ddl_VI_level" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
            <asp:GridView ID="datStdFeeRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="II_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="37%">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice No" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="II_InvNo" class="id1" runat="server" Text='<%# Eval("II_InvNo") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="II_Year" class="id1" runat="server" Text='<%# Eval("II_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Amount" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="II_FullAmount" class="id1" runat="server" Text='<%# Eval("II_FullAmount") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Outstanding" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="II_Outstanding" class="id1" runat="server" Text='<%# Eval("II_Outstanding") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="II_Status" class="id1" runat="server" Text='<%# Eval("II_Status") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="View" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Fee Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>

</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
