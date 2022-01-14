<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="configuration_setting.ascx.vb" Inherits="KPP_SYS.configuration_setting" %>

<style>
    .ddl{
        border-radius:25px;
    }
</style>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="manage_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="management_info()" value="0">Register New Setting <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="setting_info">
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> School ID : <i class="fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="school_ID" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Parameter : <i class="fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parameter" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Value : <i class="fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Value" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Code : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Code" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Type : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Type" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Idx : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="idx" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Description : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="description" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
        </div>
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
            <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
        </div>
    </div>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Manage Setting</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px;margin-bottom:5px">
        <div class="col-md-6 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
    </div>
    <br />
    <div style="overflow-y: scroll;overflow-x: hidden; height: 450px"  class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="ID" BorderStyle="None" GridLines="None" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating"
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
                <asp:TemplateField HeaderText="Parameter" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="Parameter" class="id1" runat="server" Text='<%# Eval("Parameter") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtParameter" Width="100px" class="id1" runat="server" Text='<%# Eval("Parameter") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Value" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="Value" class="id1" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtValue" Width="100px" class="id1" runat="server" Text='<%# Eval("Value") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Code" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="Code" class="id1" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCode" Width="150px" class="id1" runat="server" Text='<%# Eval("Code") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="Type" class="id1" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtType" Width="100px" class="id1" runat="server" Text='<%# Eval("Type") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="idx" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="idx" class="id1" runat="server" Text='<%# Eval("idx") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtidx" Width="100px" class="id1" runat="server" Text='<%# Eval("idx") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="Description" class="id1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDescription" Width="100px" class="id1" runat="server" Text='<%# Eval("Description") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" UpdateImageUrl="~/img/save.png" CancelImageUrl="~/img/cancel.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
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
</div>

