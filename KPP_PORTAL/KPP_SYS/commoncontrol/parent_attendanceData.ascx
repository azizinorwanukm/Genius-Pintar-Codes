<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="parent_attendanceData.ascx.vb" Inherits="KPP_SYS.parent_attendanceData" %>

<style>
    .ddl {
        border-radius: 25px;
    }

    .centerHeader {
        text-align: center;
    }

    .lblAbsent {
        font-size: 15px;
    }

    .lblAttend {
        font-size: 15px;
    }
</style>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 20px" class="w3-card-2">
    <%--Breadcrum--%>
    <div style="padding-top: 10px; padding-left: 15px; padding-bottom: 10px" class="w3-text-black">
        Menu : Report / Attendance Report
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 20px;" class="w3-card-2">
    <div style="padding-top: 10px; padding-left: 15px; padding-bottom: 15px; border-bottom: 3px solid #567572FF;">
        <div class="w3-text-black" style="text-align: left; padding-left: 10px; display: inline-block">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlYear" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 20px; display: inline-block">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Months : </asp:Label>
            <asp:DropDownList ID="ddlMonth" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 20px; display: inline-block">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Semester : </asp:Label>
            <asp:DropDownList ID="ddlSemester" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
    </div>

    <div style="padding-top: 20px; padding-left: 15px; padding-bottom: 10px; padding-right: 10px; overflow-y: scroll; height: 68vh">

        <div class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="course_ID" BorderStyle="None" GridLines="None" ShowHeaderWhenEmpty="true"
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

                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="500">
                        <ItemTemplate>
                            <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Subject Code" ItemStyle-Width="300">
                        <ItemTemplate>
                            <asp:Label ID="subject_code" class="id1" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="1" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday1" class="id1" runat="server" Text='<%# Eval("1") %>' ToolTip='<%# Eval("R1") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="2" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday2" class="id1" runat="server" Text='<%# Eval("2") %>' ToolTip='<%# Eval("R2") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="3" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday3" class="id1" runat="server" Text='<%# Eval("3") %>' ToolTip='<%# Eval("R3") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="4" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday4" class="id1" runat="server" Text='<%# Eval("4") %>' ToolTip='<%# Eval("R4") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="5" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday5" class="id1" runat="server" Text='<%# Eval("5") %>' ToolTip='<%# Eval("R5") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="6" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday6" class="id1" runat="server" Text='<%# Eval("6") %>' ToolTip='<%# Eval("R6") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="7" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday7" class="id1" runat="server" Text='<%# Eval("7") %>' ToolTip='<%# Eval("R7") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="8" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday8" class="id1" runat="server" Text='<%# Eval("8") %>' ToolTip='<%# Eval("R8") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="9" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday9" class="id1" runat="server" Text='<%# Eval("9") %>' ToolTip='<%# Eval("R9") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="10" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday10" class="id1" runat="server" Text='<%# Eval("10") %>' ToolTip='<%# Eval("R10") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="11" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday11" class="id1" runat="server" Text='<%# Eval("11") %>' ToolTip='<%# Eval("R11") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="12" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday12" class="id1" runat="server" Text='<%# Eval("12") %>' ToolTip='<%# Eval("R12") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="13" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday13" class="id1" runat="server" Text='<%# Eval("13") %>' ToolTip='<%# Eval("R13") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="14" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday14" class="id1" runat="server" Text='<%# Eval("14") %>' ToolTip='<%# Eval("R14") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="15" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday15" class="id1" runat="server" Text='<%# Eval("15") %>' ToolTip='<%# Eval("R15") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="16" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday16" class="id1" runat="server" Text='<%# Eval("16") %>' ToolTip='<%# Eval("R16") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="17" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday17" class="id1" runat="server" Text='<%# Eval("17") %>' ToolTip='<%# Eval("R17") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="18" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday18" class="id1" runat="server" Text='<%# Eval("18") %>' ToolTip='<%# Eval("R18") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="19" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday19" class="id1" runat="server" Text='<%# Eval("19") %>' ToolTip='<%# Eval("R19") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="20" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday20" class="id1" runat="server" Text='<%# Eval("20") %>' ToolTip='<%# Eval("R20") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="21" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday21" class="id1" runat="server" Text='<%# Eval("21") %>' ToolTip='<%# Eval("R21") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="22" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday22" class="id1" runat="server" Text='<%# Eval("22") %>' ToolTip='<%# Eval("R22") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="23" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday23" class="id1" runat="server" Text='<%# Eval("23") %>' ToolTip='<%# Eval("R23") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="24" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday24" class="id1" runat="server" Text='<%# Eval("24") %>' ToolTip='<%# Eval("R24") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="25" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday25" class="id1" runat="server" Text='<%# Eval("25") %>' ToolTip='<%# Eval("R25") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="26" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday26" class="id1" runat="server" Text='<%# Eval("26") %>' ToolTip='<%# Eval("R26") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="27" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday27" class="id1" runat="server" Text='<%# Eval("27") %>' ToolTip='<%# Eval("R27") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="28" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday28" class="id1" runat="server" Text='<%# Eval("28") %>' ToolTip='<%# Eval("R28") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="29" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday29" class="id1" runat="server" Text='<%# Eval("29") %>' ToolTip='<%# Eval("R29") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="30" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday30" class="id1" runat="server" Text='<%# Eval("30") %>' ToolTip='<%# Eval("R30") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="31" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="lblday31" class="id1" runat="server" Text='<%# Eval("31") %>' ToolTip='<%# Eval("R31") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Absence" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="TtlAbsence" class="id1" runat="server" Text='<%# Eval("Total Absence") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Percentage" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <asp:Label ID="Percentage" class="id1" runat="server" Text='<%# Eval("Percentage") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1"> <b> No Attendance Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>
</div>
