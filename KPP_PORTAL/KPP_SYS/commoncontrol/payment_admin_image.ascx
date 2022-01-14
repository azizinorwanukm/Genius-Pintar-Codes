<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_admin_image.ascx.vb" Inherits="KPP_SYS.payment_admin_image" %>

<link rel="stylesheet" href="js/jquery-ui.css" />
<script type="text/javascript" src="js/jquery-1.10.2.js"></script>
<script type="text/javascript" src="js/jquery-ui.js"></script>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }

    .modalBackground {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
        z-index: 10000;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student Information</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Name : </asp:Label>
            <asp:Label CssClass="Label" ID="student_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> IC Number : </asp:Label>
            <asp:Label CssClass="Label" ID="student_Mykad" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student ID : </asp:Label>
            <asp:Label CssClass="Label" ID="student_ID" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Gender : </asp:Label>
            <asp:Label CssClass="Label" ID="student_Sex" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
    </div>
</div>
<br />
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Payment Information</p>
    <br />
    <div style="overflow-y: scroll; overflow-x: hidden; height: 380px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="Transaction_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Item" ItemStyle-Width="500">
                    <ItemTemplate>
                        <asp:Label ID="Description" class="id1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price (RM)" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="Std_Male" class="id1" runat="server" Text='<%# Eval("Std_Male") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PRICE (RM)" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="Std_Female" class="id1" runat="server" Text='<%# Eval("Std_Female") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payment Status" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="Status" class="id1" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 5px; text-align: left; padding-left: 23px">
        <button id="Btnkemaskini" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Paid"><i class="fa fa-save w3-large w3-text-white"></i>Paid </button>
        <button id="Btnpending" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Pending"><i class="fa fa-save w3-large w3-text-white"></i>Pending </button>
    </div>
</div>
<br />
