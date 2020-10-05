<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Student_RegisterMentor_List.ascx.vb" Inherits="KPP_MS.Student_RegisterMentor_List" %>

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>


<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pelajarCreate_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Research Mentor/Co Mentor </button>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddl_year" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddl_group" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
    </div>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px; text-align: left; padding-left: 23px">
        <div class="col-md-10 w3-text-black" style="text-align: left; ">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Mentor : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txtmentor" Style="width: 90%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-10 w3-text-black" style="text-align: left;">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Faculty : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txtmentor_faculty" Style="width: 90%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-10 w3-text-black" style="text-align: left; ">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Co Mentor : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txtcomentor" Style="width: 85%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-10 w3-text-black" style="text-align: left;">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Faculty : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txtcomentor_faculty" Style="width: 90%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; text-align: left; padding-left: 23px; margin-bottom: 5px">
            <button id="btnAdd" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; display: inline" title="ADD Course">Save &#160; <i class="fa fa-save w3-large w3-text-white"></i></button>
            <button id="btnBack" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px; display: inline" title="Back">Back &#160; <i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
        </div>
    </div>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button type="button" id="list_mentor" runat="server" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Mentor/Co Mentor List </button>

    <div style="overflow-y: scroll; overflow-x: hidden; height: 400px; margin-top:5px" class="table-responsive" id="gridvew_list" runat="server">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="ri_id" BorderStyle="None" GridLines="None" RowStyle-HorizontalAlign="Left"
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
                <asp:TemplateField HeaderText="Year" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="ri_year" class="id1" runat="server" Text='<%# Eval("ri_year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Group" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="ri_groupname" class="id1" runat="server" Text='<%# Eval("ri_groupname") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mentor" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="ri_mentor" class="id1" runat="server" Text='<%# Eval("ri_mentor") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Faculty" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="ri_mentorfaculty" class="id1" runat="server" Text='<%# Eval("ri_mentorfaculty") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CO Mentor" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="ri_comentor" class="id1" runat="server" Text='<%# Eval("ri_comentor") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Faculty" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="ri_comentorfaculty" class="id1" runat="server" Text='<%# Eval("ri_comentorfaculty") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>
</div>
<div class="messagealert" id="alert_container" style="text-align: center"></div>
