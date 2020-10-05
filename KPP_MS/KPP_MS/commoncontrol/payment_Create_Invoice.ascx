<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_Create_Invoice.ascx.vb" Inherits="KPP_MS.payment_Create_Invoice" %>

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

<style>
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

    function CheckAllStd(Checkbox) {
        var GridVwHeaderChckbox_std = document.getElementById("<%=gridRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox_std.rows.length; i++) {
            GridVwHeaderChckbox_std.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>


<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px"> Fee Item List</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddlYear_List" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 275px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="FIM_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Year" ItemStyle-Width="70">
                    <ItemTemplate>
                        <asp:Label ID="FIM_Year" class="id1" runat="server" Text='<%# Eval("FIM_Year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" ItemStyle-Width="500">
                    <ItemTemplate>
                        <asp:Label ID="FIM_Item" class="id1" runat="server" Text='<%# Eval("FIM_Item") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="FIM_Quantity" class="id1" runat="server" Text='<%# Eval("FIM_Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price (RM)" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="FIM_Price" class="id1" runat="server" Text='<%# Eval("FIM_Price") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark" ItemStyle-Width="250">
                    <ItemTemplate>
                        <asp:Label ID="FIM_Remark" class="id1" runat="server" Text='<%# Eval("FIM_Remark") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>                
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <p></p>
    </div>
</div>

<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px"> Student List </p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddl_Year" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddl_Level" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddl_Class" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 275px" class="table-responsive">
        <asp:GridView ID="gridRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
            Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkboxSelectAll_Student" Text="" runat="server" onclick="CheckAllStd(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect_Std" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Year" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:Label ID="year" class="id1" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
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
                <asp:TemplateField HeaderText="Class" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>   
                <asp:CommandField HeaderText="Detail" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/Eye_Care_Services-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <p></p>
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; display: inline" >Generate Invoice &#160;<i class="fa fa-plus-circle w3-large w3-text-white"></i></button>
         <button id="BtnBack" runat="server"  class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px; display: inline" >Back &#160; <i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
</div>
<div class="messagealert" id="alert_container" style="text-align: center"></div> 