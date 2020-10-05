<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_Ukm3_History.ascx.vb" Inherits="KPP_MS.student_Ukm3_History" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="UKM3_history" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_Ukm3()" value="0">UKM3 History <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="std_ukm3">
        <div style="overflow-y: scroll; overflow-x: scroll; height: 200px; margin-top: 5px; margin-left: 10px; margin-right: 10px" class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#d9d9d9" DataKeyNames="id" BorderStyle="None" GridLines="None" Height="100" Width="97%"
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
                    <asp:TemplateField HeaderText="Year">
                        <ItemTemplate>
                            <asp:Label ID="ukm3Year" runat="server" Text='<%# Bind("ukm3Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STEM Exam">
                        <ItemTemplate>
                            <asp:Label ID="marks_100" runat="server" Text='<%# Bind("marks_100") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pre-Test">
                        <ItemTemplate>
                            <asp:Label ID="markPretest" runat="server" Text='<%# Bind("markPretest") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Post-Test">
                        <ItemTemplate>
                            <asp:Label ID="markPosttest" runat="server" Text='<%# Bind("markPosttest") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Instr PPCS">
                        <ItemTemplate>
                            <asp:Label ID="insPPCS100mark" runat="server" Text='<%# Bind("insPPCS100mark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comment">
                        <ItemTemplate>
                            <asp:Label ID="instruktorExam_Komen" runat="server" Text='<%# Bind("instruktorExam_Komen") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RA PPCS">
                        <ItemTemplate>
                            <asp:Label ID="insRAPPCS100mark" runat="server" Text='<%# Bind("insRAPPCS100mark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comment">
                        <ItemTemplate>
                            <asp:Label ID="instruktorExam_Komen" runat="server" Text='<%# Bind("instruktorExam_Komen") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Instr KPP">
                        <ItemTemplate>
                            <asp:Label ID="insKPP100mark" runat="server" Text='<%# Bind("insKPP100mark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comment">
                        <ItemTemplate>
                            <asp:Label ID="instruktorExam_Komen_kpp" runat="server" Text='<%# Bind("instruktorExam_Komen_kpp") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Composit Mark">
                        <ItemTemplate>
                            <asp:Label ID="compo_Mark" runat="server" Text='<%# Bind("compo_Mark") %>'></asp:Label>
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
