<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_assessment_config.ascx.vb" Inherits="KPP_MS.admin_assessment_config" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="admin_access_list" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Admin Accessibility List</button>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddlformat" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 380px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="asconfig_ID" BorderStyle="None" GridLines="None" OnRowCancelingEdit="AssessmentCancelEditing" OnRowUpdating="AssessmentUpdate"
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
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="description" class="id1" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtdescription" Width="450px" class="id1" runat="server" Text='<%# Eval("description") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Scoring">
                    <ItemTemplate>
                        <asp:RadioButton ID="rbtn_one" Text="1 &nbsp;" runat="server" GroupName="score" />
                        <asp:RadioButton ID="rbtn_two" Text="2 &nbsp;" runat="server" GroupName="score" />
                        <asp:RadioButton ID="rbtn_three" Text="3 &nbsp;" runat="server" GroupName="score" />
                        <asp:RadioButton ID="rbtn_four" Text="4 &nbsp;" runat="server" GroupName="score" />
                        <asp:RadioButton ID="rbtn_five" Text="5 &nbsp;" runat="server" GroupName="score" />
                    </ItemTemplate>
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
    <p></p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px;">
        <button id="btnAdd" runat="server" class="btn btn-info" style="background-color: #009900; border-radius: 25px;" title="Save">Add <i class="fa fa-plus-circle w3-large w3-text-white"></i></button>
    </div>
</div>
<br />