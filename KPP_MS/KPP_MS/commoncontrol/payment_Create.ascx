<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_Create.ascx.vb" Inherits="KPP_MS.payment_Create" %>

<style>
    .sc3::-webkit-scrollbar {
        height: 8px;
    }

    .sc3::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc3::-webkit-scrollbar-thumb {
        background-color: #929B9E;
    }

    .sc4::-webkit-scrollbar {
        width: 10px;
        height: 8px;
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

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Payment &nbsp; / &nbsp; Payment Management &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF;">
        <button id="btnRegisterPayment" runat="server" style="display: inline-block; font-size: 0.8vw">Register Payment Items</button>
        <button id="btnViewPayment" runat="server" style="display: inline-block; font-size: 0.8vw">View Payment Items</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="RegisterPayment" runat="server" class="sc4">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year  </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="ddlYear" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level  </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:CheckBox ID="CB_F1" Text="&nbsp; Foundation 1 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" />
                    <asp:CheckBox ID="CB_F2" Text="&nbsp; Foundation 2 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" />
                    <asp:CheckBox ID="CB_F3" Text="&nbsp; Foundation 3 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" />
                    <asp:CheckBox ID="CB_L1" Text="&nbsp; Level 1 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" />
                    <asp:CheckBox ID="CB_L2" Text="&nbsp; Level 2 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" />
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Fee Type </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlType" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Fee Item Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="Inv_Name" Style="width: 30vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Fee Item Quantity </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="Inv_Quantity" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Gender </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:RadioButton ID="rbtn_Male" Text="&nbsp; Male &nbsp;&nbsp;&nbsp; " runat="server" GroupName="gender" />
                    <asp:RadioButton ID="rbtn_Female" Text="&nbsp; Female &nbsp;&nbsp;&nbsp;" runat="server" GroupName="gender" />
                    <asp:RadioButton ID="rbtn_Both" Text="&nbsp; Both" runat="server" GroupName="gender" />
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Religion </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:RadioButton ID="rbtn_Muslim" Text="&nbsp; Muslim &nbsp;&nbsp;&nbsp; " runat="server" GroupName="religon" />
                    <asp:RadioButton ID="rbtn_NonMuslim" Text="&nbsp; Non-Muslim  &nbsp;&nbsp;&nbsp;" runat="server" GroupName="religon" />
                    <asp:RadioButton ID="rbtn_All" Text="&nbsp; Both" runat="server" GroupName="religon" />
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Price (RM) </asp:Label>
                </td>
                <td colspan="5">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Std_Price" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Remark </asp:Label>
                </td>
                <td colspan="5">
                    <p></p>
                    &nbsp : &nbsp
                      <asp:TextBox runat="server" ID="Inv_Remark" Style="width: 50vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

        </table>

        <br />
        <br />

        <button id="btnAddPayment" runat="server" class="btn btn-success" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Add New Payment Items</button>
    </div>


    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="ViewPayment" runat="server" class="sc4">

        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlYear_List" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Fee Type : </asp:Label>
            <asp:DropDownList ID="ddlType_List" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList ID="ddlLevel_List" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black w3-right font" style="text-align: left; padding-left: 1vw; padding-right: 1vw; display: inline-block">
            <button id="Add_Student" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Assign Student Payment</button>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="FIM_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="FIM_Year" class="id1" runat="server" Text='<%# Eval("FIM_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="45%">
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
                    <asp:TemplateField HeaderText="Price (RM)" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="FIM_Price" class="id1" runat="server" Text='<%# Eval("FIM_Price") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="FIM_Remark" class="id1" runat="server" Text='<%# Eval("FIM_Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="View" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton Width="25" Height="25" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Fee Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>


