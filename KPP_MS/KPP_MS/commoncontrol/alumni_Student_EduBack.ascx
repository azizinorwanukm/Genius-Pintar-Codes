<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="alumni_Student_EduBack.ascx.vb" Inherits="KPP_MS.alumni_Student_EduBack" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pelajar_EduBack" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_eduback()" value="0">Education Background <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="education_Back">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <asp:Label runat="server" CssClass="w3-text-black">SPM Year</asp:Label>
            <asp:DropDownList CssClass="btn btn-default font ddl" runat ="server" id="ddl_spmYear"></asp:DropDownList>
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            <asp:Label runat="server" CssClass="w3-text-black">Year Of Graduate From KPP</asp:Label>
            <asp:DropDownList CssClass="btn btn-default font ddl" runat ="server" id="ddl_graduateKPPYear"></asp:DropDownList>
        </div>
        <div></div>
    
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <br />

    <div style="overflow-y: scroll;overflow-x: hidden; height: 450px"  class="table-responsive">
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
                <asp:TemplateField ItemStyle-Width="300" HeaderText ="Qualification">
                    <ItemTemplate>
                        <asp:Label ID="std_qualify" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Institute / University" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="student_insUni" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Location" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="std_location" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Country" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="std_country" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course " ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="std_course" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Field" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="std_field" class="id1" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Year Of Graduate" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="std_yearGrad" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Age On Graduation" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="std_ageGrad" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Result" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="std_result" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Scholarship" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="std_scholarship" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                <%--<asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                    </ItemTemplate>
                </asp:TemplateField>--%>              
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
            <button id="btnStudentUpdate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Add Data"><i class="fa fa-save w3-large w3-text-white"> Add Data</i></button>
            <asp:Label ID="Label1" CssClass="w3-text-black" runat="server"></asp:Label>
        </div>
    <asp:HiddenField ID="hiddenAccess" runat="server" />

</div>
    
</div>
    </div>
<br />