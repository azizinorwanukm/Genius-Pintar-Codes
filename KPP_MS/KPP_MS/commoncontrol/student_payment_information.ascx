<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_payment_information.ascx.vb" Inherits="KPP_MS.student_payment_information" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="bayaran_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="payment_info()" value="0">Fee Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="payment_info">

        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-2 w3-text-black" style="text-align: left">
                <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
        </div>
        <p></p>
        <div style="overflow-y: scroll; overflow-x: hidden; height: 380px" class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#d9d9d9" DataKeyNames="Transaction_ID" BorderStyle="None" GridLines="None" Width="97%" HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>                   
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
            </asp:GridView>
        </div>
    </div>
</div>
<br />
