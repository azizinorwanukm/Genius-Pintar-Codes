<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="alumni_student_workStatus.ascx.vb" Inherits="KPP_MS.alumni_student_workStatus" %>
<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }
</style>



<div class="gridViewRespond1" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pelajar_kerja" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_workStatus()" value="0">Work Status <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="work_status">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">

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
                <asp:TemplateField ItemStyle-Width="300" HeaderText ="Employer Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_employerName" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="300" HeaderText =" Position ">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Position" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="300" HeaderText ="Date From/To">
                    <ItemTemplate>
                        <asp:Label ID="lbl_datesfomto" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
            </Columns>
            </asp:GridView>
                </div>
            <br />
        </div>
        <br />
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
            <button id="btn_Addwork" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"> Add Work Status </i></button>
        </div>
        <p></p>
    </div>
</div>
    </div>
<br />
