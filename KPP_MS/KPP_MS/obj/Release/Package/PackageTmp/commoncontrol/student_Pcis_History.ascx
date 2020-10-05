<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_Pcis_History.ascx.vb" Inherits="KPP_MS.student_Pcis_History" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="PCIS_history" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_Pcis()" value="0">PCIS History <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="std_pcis">
        <div style="overflow-y:scroll ;overflow-x: scroll; height: 200px; margin-top:5px" class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="false"
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
                    <asp:TemplateField HeaderText="Ujian Mula">
                        <ItemTemplate>
                            <asp:Label ID="ExamStart" runat="server" Text='<%# Eval("test_start", "{0:dd/MM/yyyy hh:mm:tt}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ujian Tamat">
                        <ItemTemplate>
                            <asp:Label ID="ExamEnd" runat="server" Text='<%# Eval("test_end", "{0:dd/MM/yyyy hh:mm:tt}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="PAPN">
                        <ItemTemplate>
                            <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("learningcentrename") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD1">
                        <ItemTemplate>
                            <asp:Label ID="mod1" runat="server" Text='<%# Bind("mod1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD2">
                        <ItemTemplate>
                            <asp:Label ID="mod2" runat="server" Text='<%# Bind("mod2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD3">
                        <ItemTemplate>
                            <asp:Label ID="mod3" runat="server" Text='<%# Bind("mod3") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD4">
                        <ItemTemplate>
                            <asp:Label ID="mod4" runat="server" Text='<%# Bind("mod4") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD5">
                        <ItemTemplate>
                            <asp:Label ID="mod5" runat="server" Text='<%# Bind("mod5") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD6">
                        <ItemTemplate>
                            <asp:Label ID="mod6" runat="server" Text='<%# Bind("mod6") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD7">
                        <ItemTemplate>
                            <asp:Label ID="mod7" runat="server" Text='<%# Bind("mod7") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD8">
                        <ItemTemplate>
                            <asp:Label ID="mod8" runat="server" Text='<%# Bind("mod8") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD9">
                        <ItemTemplate>
                            <asp:Label ID="mod9" runat="server" Text='<%# Bind("mod9") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD10">
                        <ItemTemplate>
                            <asp:Label ID="mod10" runat="server" Text='<%# Bind("mod10") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah">
                        <ItemTemplate>
                            <asp:Label ID="total" runat="server" Text='<%# Bind("total") %>'></asp:Label>
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