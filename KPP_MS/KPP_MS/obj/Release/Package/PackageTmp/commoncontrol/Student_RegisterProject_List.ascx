<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Student_RegisterProject_List.ascx.vb" Inherits="KPP_MS.Student_RegisterProject_List" %>

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>


<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pelajarCreate_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Research Project/Field </button>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddl_year" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddl_group" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
    </div>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-8 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Student : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txtStudent" Style="width: 75%; border-radius: 25px;" runat="server" Text="" placeholder="  Search by Name/ID/IC"></asp:TextBox>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; ">
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
    </div>
    <br />

    <div style="overflow-y: scroll; overflow-x: hidden; height: 400px" class="table-responsive">
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
                <asp:TemplateField HeaderText="Year" ItemStyle-Width="70">
                    <ItemTemplate>
                        <asp:Label ID="ri_year" class="id1" runat="server" Text='<%# Eval("ri_year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Group" ItemStyle-Width="70">
                    <ItemTemplate>
                        <asp:Label ID="ri_groupname" class="id1" runat="server" Text='<%# Eval("ri_groupname") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Project">
                    <ItemTemplate>
                        <asp:TextBox ID="ri_researchname"  class="id1" runat="server" Text='<%# Eval("ri_researchname") %>' Enabled="true"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Field">
                    <ItemTemplate>
                        <asp:TextBox ID="ri_researchfiled" class="id1" runat="server" Text='<%# Eval("ri_researchfiled") %>' Enabled="true"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Competition">
                    <ItemTemplate>
                        <asp:TextBox ID="ri_researchcompetition"  class="id1" runat="server" Text='<%# Eval("ri_researchcompetition") %>' Enabled="true"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>


    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; text-align: left; padding-left: 23px; margin-bottom: 10px">
        <button id="btnAdd" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; display: inline" title="ADD Course">Save &#160; <i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="btnBack" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px; display: inline" title="Back">Back &#160; <i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
    
</div>
<div class="messagealert" id="alert_container" style="text-align: center"></div>