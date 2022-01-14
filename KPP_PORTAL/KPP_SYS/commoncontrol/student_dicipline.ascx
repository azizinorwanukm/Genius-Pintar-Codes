<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_dicipline.ascx.vb" Inherits="KPP_SYS.student_dicipline" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="dicHidden" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="dicipline_info()" value="0">Dicipline Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none; font-size: 14px" id="dic_info">

         <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-3 w3-text-black" style="text-align: left">
                <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
        </div>
        <p></p>
        <div style="overflow-y: scroll; overflow-x: hidden; height: 250px" class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="9"
                BackColor="#d9d9d9" DataKeyNames="disiplin_id" BorderStyle="None" GridLines="None" ShowHeaderWhenEmpty="true"
                Width="97%" HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="Dicipline_Date" class="id1" runat="server" Text='<%# Eval("Dicipline_Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="400">
                        <ItemTemplate>
                            <asp:Label ID="Detail_Case" class="id1" runat="server" Text='<%# Eval("Detail_Case") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="250">
                        <ItemTemplate>
                            <asp:Label ID="Dicipline_Action" class="id1" runat="server" Text='<%# Eval("Dicipline_Action") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <EmptyDataTemplate>
                    <asp:Label align="center" runat="server" Class="id1">No diciplinary record</asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>

</div>
<br />
