<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_CocurricularList.ascx.vb" Inherits="KPP_MS.student_CocurricularList" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Cocurricular Data</p>
    <br />

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px; margin-bottom: 5px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlKokoType" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlKokoName" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 5px; text-align: left;">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtstudent" Style="width: 85%; border-radius: 25px;" runat="server" Text="" placeholder="   By Name / Mykad / Alumni ID"></asp:TextBox>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 13px">
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
    </div>

    <div style="overflow-y: scroll; overflow-x: scroll; height: 450px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Student Name">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" runat="server" Text='<%# Bind("student_Name") %>' Width='230px'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Mykad">
                    <ItemTemplate>
                        <asp:Label ID="student_Mykad" runat="server" Text='<%# Bind("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AlumniID">
                    <ItemTemplate>
                        <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("AlumniID") %>' Width='70px'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Class">
                    <ItemTemplate>
                        <asp:Label ID="Kelas" runat="server" Text='<%# Bind("Kelas") %>' Width='50px'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Uniform">
                    <ItemTemplate>
                        <asp:Label ID="Uniform" runat="server" Text='<%# Bind("Uniform") %>' Width='300px'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Persatuan">
                    <ItemTemplate>
                        <asp:Label ID="Persatuan" runat="server" Text='<%# Bind("Persatuan") %>' Width='300px'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sukan">
                    <ItemTemplate>
                        <asp:Label ID="Sukan" runat="server" Text='<%# Bind("Sukan") %>' Width='200px'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RumahSukan">
                    <ItemTemplate>
                        <asp:Label ID="RumahSukan" runat="server" Text='<%# Bind("RumahSukan") %>' Width='200px'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

    <asp:HiddenField ID="hiddenAccess" runat="server" />

</div>
