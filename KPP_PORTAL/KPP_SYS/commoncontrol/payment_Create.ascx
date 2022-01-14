<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_Create.ascx.vb" Inherits="KPP_SYS.payment_Create" %>

<script type="text/javascript">
    function payment_info() {
        if (document.getElementById("payment_information").value == 0) {
            document.getElementById("payment_baru").style.display = "block";
            document.getElementById("payment_information").value = 1;
        }

        else if (document.getElementById("payment_information").value == 1) {
            document.getElementById("payment_baru").style.display = "none";
            document.getElementById("payment_information").value = 0;
        }
    }

</script>

 <script type="text/javascript">

     $(document).ready(function () {
         var accessMenu = document.getElementById('<%= hiddenAccess.ClientID %>').value;

         if (accessMenu == "1") {
             document.getElementById("Payment_Type").style.display = "block";
         }
     })
</script>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="payment_information" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="payment_info()" value="0">Register New Payment <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="payment_baru">
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Description : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Description_Payment" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Price (Male RM) : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Std_Male" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Price (Female RM) : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Std_Female" Style="width: 100%; border-radius: 25px;" runat="server" Text="" Placeholder="  Price Per Unit"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Note : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Note" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Year: <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Level : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Type : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack = "true" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div id="Payment_Type" class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px; display:none">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Payment Type : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlPaymentType" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
        </div>
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
            <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
        </div>
    </div>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddlYear_List" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddlLevel_List" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 400px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="Payment_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Description" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="Description" class="id1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price (Male RM)" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="Std_Male" class="id1" runat="server" Text='<%# Eval("Std_Male") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price (Female RM)" ItemStyle-Width="250">
                    <ItemTemplate>
                        <asp:Label ID="Std_Female" class="id1" runat="server" Text='<%# Eval("Std_Female") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="Type" class="id1" runat="server" Text='<%# Eval("Payment_Type") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Year" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="Year" class="id1" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Level" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Note" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="Note" class="id1" runat="server" Text='<%# Eval("Note") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <div><p></p></div>

    <asp:HiddenField ID="hiddenAccess" runat="server" />
</div>