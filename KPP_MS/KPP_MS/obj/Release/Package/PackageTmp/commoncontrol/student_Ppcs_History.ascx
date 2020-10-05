<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_Ppcs_History.ascx.vb" Inherits="KPP_MS.student_Ppcs_History" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="PPCS_history" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_Ppcs()" value="0">PPCS History <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="std_ppcs">
        <div style="overflow-y: scroll; overflow-x: scroll; height: 200px; margin-top: 5px; margin-left: 10px; margin-right: 10px" class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#d9d9d9" DataKeyNames="PPCSID" BorderStyle="None" GridLines="None" Height="100" Width="97%"
                HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PPCSDate">
                        <ItemTemplate>
                            <asp:Label ID="PPCSDate" runat="server" Text='<%# Bind("PPCSDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PPCSCourse">
                        <ItemTemplate>
                            <asp:Label ID="CourseCode" runat="server" Text='<%# Bind("CourseCode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PPCSClass">
                        <ItemTemplate>
                            <asp:Label ID="ClassCode" runat="server" Text='<%# Bind("ClassCode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NamaAsrama">
                        <ItemTemplate>
                            <asp:Label ID="NamaAsrama" runat="server" Text='<%# Bind("NamaAsrama") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NoBilik">
                        <ItemTemplate>
                            <asp:Label ID="NoBilik" runat="server" Text='<%# Bind("NoBilik") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PPCSStatus">
                        <ItemTemplate>
                            <asp:Label ID="PPCSStatus" runat="server" Text='<%# Bind("PPCSStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StatusTawaran">
                        <ItemTemplate>
                            <asp:Label ID="StatusTawaran" runat="server" Text='<%# Bind("StatusTawaran") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sebab Tolak">
                        <ItemTemplate>
                            <asp:Label ID="StatusReason" runat="server" Text='<%# Bind("StatusReason") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
        <p></p>
    </div>
</div>
<br />
