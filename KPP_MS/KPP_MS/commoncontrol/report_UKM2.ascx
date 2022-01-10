<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="report_UKM2.ascx.vb" Inherits="KPP_MS.report_UKM2" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">UKM 2 Data</p>
    <br />

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px; margin-bottom: 5px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="btn btn-default font ddl"></asp:DropDownList>
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
                        <asp:Label ID="student_Name" runat="server" Text='<%# Bind("student_Name") %>' Width='270px'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Mykad">
                    <ItemTemplate>
                        <asp:Label ID="student_Mykad" runat="server" Text='<%# Bind("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Alumni ID">
                    <ItemTemplate>
                        <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("AlumniID") %>' Width='100px'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UKM1%">
                    <ItemTemplate>
                        <asp:Label ID="UKM1TotalPercentage" runat="server" Text='<%# Bind("UKM1TotalPercentage") %>' Width='100px'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UKM2%">
                    <ItemTemplate>
                        <asp:Label ID="UKM2TotalPercentage" runat="server" Text='<%# Bind("UKM2TotalPercentage") %>' Width='100px'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Post-Test">
                    <ItemTemplate>
                        <asp:Label ID="Mental_Age_Year" runat="server" Text='<%# Bind("Mental_Age_Year") %>' Width='120px'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IQ">
                    <ItemTemplate>
                        <asp:Label ID="Student_IQ" runat="server" Text='<%# Bind("Student_IQ") %>' Width='130px'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WMI">
                    <ItemTemplate>
                        <asp:Label ID="WMI" runat="server" Text='<%# Bind("WMI") %>' Width='50px'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>    
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

    <asp:HiddenField ID="hiddenAccess" runat="server" />

</div>
