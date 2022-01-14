<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_imagePayment.ascx.vb" Inherits="KPP_SYS.student_imagePayment" %>

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
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Invoice Information</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-2 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 200px" class="table-responsive">
        <asp:GridView ID="GridView1" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="II_ID" BorderStyle="None" GridLines="None" Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reference No" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="ref_No" class="id1" runat="server" Text='<%# Eval("II_RefNo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="date" class="id1" runat="server" Text='<%# Eval("II_Date") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type Of Payment" ItemStyle-Width="350">
                    <ItemTemplate>
                        <asp:Label ID="II_PaymentType" class="id1" runat="server" Text='<%# Eval("II_PaymentType") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Detail" ButtonType="image" ShowDeleteButton="true" DeleteImageUrl="~/img/Eye_Care_Services-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
</div>
<br />

<div id="Invoice_Detail" runat="server" class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Invoice Detail</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <div class="col-md-4 w3-text-black ddl" style="text-align: left; padding-left: 23px; margin-top: 10px; background-color: plum">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Outstanding Amount (RM) : </asp:Label>
            <asp:Label CssClass="Label" ID="ttl_InvPrice" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 280px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="IT_ID" BorderStyle="None" GridLines="None" Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>               
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item" ItemStyle-Width="450">
                    <ItemTemplate>
                        <asp:Label ID="ID_Item" class="id1" runat="server" Text='<%# Eval("ID_Item") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="IT_Quantity" class="id1" runat="server" Width="50" Text='<%# Eval("IT_Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price (RM)" ItemStyle-Width="125">
                    <ItemTemplate>
                        <asp:Label ID="IT_Price" class="id1" runat="server" Text='<%# Eval("IT_Price") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="IT_Status" class="id1" runat="server" Text='<%# Eval("IT_Status") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date" ItemStyle-Width="125">
                    <ItemTemplate>
                        <asp:Label ID="IT_Date" class="id1" runat="server" Text='<%# Eval("IT_Date") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
</div>
