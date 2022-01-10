<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="report_UKM1_PPCS.ascx.vb" Inherits="KPP_MS.report_UKM1_PPCS" %>

    <style>
        .sc3::-webkit-scrollbar {
            height: 10px;
        }

        .sc3::-webkit-scrollbar-track {
            background-color: transparent;
        }

        .sc3::-webkit-scrollbar-thumb {
            background-color: #929B9E;
            border-radius: 3px;
        }

        .sc4::-webkit-scrollbar {
            width: 10px;
        }

        .sc4::-webkit-scrollbar-track {
            background-color: transparent;
        }

        .sc4::-webkit-scrollbar-thumb {
            background-color: #929B9E;
        }
    </style>

    <div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
        <%--Breadcrum--%>
        <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black font">
            Menu &nbsp; : &nbsp; Report &nbsp; / &nbsp; UKM1 - PPCS Report
            &nbsp; / &nbsp;
            <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
        </div>
    </div>

    <div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
        <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
            <button id="btnViewStudent_UKM1" runat="server" style="display: inline-block; font-size: 0.8vw">UKM 1 Report</button>
            <button id="btnViewStudent_UKM2" runat="server" style="display: inline-block; font-size: 0.8vw">UKM 2 Report</button>
            <button id="btnViewStudent_PPCS" runat="server" style="display: inline-block; font-size: 0.8vw">PPCS Report</button>
            <button id="btnViewStudent_UKM3" runat="server" style="display: inline-block; font-size: 0.8vw">UKM 3 Report</button>
        </div>

        <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ViewUKM1Report" runat="server">
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlYear_UKM1" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlLevel_UKM1" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlSemester_UKM1" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>

            <br />
            <br />

           <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
                <asp:GridView ID="datRespondentUKM1" runat="server" class="table w3-text-black " AutoGenerateColumns="False" BackColor="#FFFAFA"
                    DataKeyNames="std_ID" BorderStyle="None" GridLines="None" Width="97%" HeaderStyle-HorizontalAlign="Left">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="student_Name" runat="server" Text='<%# Bind("student_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="student_ID" runat="server" Text='<%# Bind("student_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="ExamYear" runat="server" Text='<%# Bind("ExamYear") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ModA" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="ModA" runat="server" Text='<%# Bind("ModA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ModB" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="ModB" runat="server" Text='<%# Bind("ModB") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ModC" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="ModC" runat="server" Text='<%# Bind("ModC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Score" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="TotalScore" runat="server" Text='<%# Bind("TotalScore") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total%" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="TotalPercentage" runat="server" Text='<%# Bind("TotalPercentage", "{0:F2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1"> <b> No UKM1 Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>

        <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ViewUKM2Report" runat="server">
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlYear_UKM2" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlLevel_UKM2" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlSemester_UKM2" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>

            <br />
            <br />

           <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
                <asp:GridView ID="datRespondentUKM2" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
                    Width="97%" HeaderStyle-HorizontalAlign="Left">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="student_Name" runat="server" Text='<%# Bind("student_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="7.78%">
                            <ItemTemplate>
                                <asp:Label ID="student_ID" runat="server" Text='<%# Bind("student_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UKM2 Year" ItemStyle-Width="7.78%">
                            <ItemTemplate>
                                <asp:Label ID="ExamYear" runat="server" Text='<%# Bind("ExamYear") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VCI" ItemStyle-Width="7.78%">
                            <ItemTemplate>
                                <asp:Label ID="VCI" runat="server" Text='<%# Bind("VCI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PRI" ItemStyle-Width="7.78%">
                            <ItemTemplate>
                                <asp:Label ID="PRI" runat="server" Text='<%# Bind("PRI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WMI" ItemStyle-Width="7.78%">
                            <ItemTemplate>
                                <asp:Label ID="WMI" runat="server" Text='<%# Bind("WMI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PSI" ItemStyle-Width="7.78%">
                            <ItemTemplate>
                                <asp:Label ID="PSI" runat="server" Text='<%# Bind("PSI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Score" ItemStyle-Width="7.78%">
                            <ItemTemplate>
                                <asp:Label ID="TotalScore" runat="server" Text='<%# Bind("TotalScore") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total%" ItemStyle-Width="7.78%">
                            <ItemTemplate>
                                <asp:Label ID="TotalPercentage" runat="server" Text='<%# Bind("TotalPercentage") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mental Age" ItemStyle-Width="7.78%">
                            <ItemTemplate>
                                <asp:Label ID="Mental_Age_Year" runat="server" Text='<%# Bind("Mental_Age_Year")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IQ" ItemStyle-Width="7.78%">
                            <ItemTemplate>
                                <asp:Label ID="Student_IQ" runat="server" Text='<%# Bind("Student_IQ")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1"> <b> No UKM2 Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>

        <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ViewPPCSReport" runat="server">
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlYear_PPCS" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlLevel_PPCS" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlSemester_PPCS" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>

            <br />
            <br />

            <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
                <asp:GridView ID="datRespondentPPCS" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
                    Width="97%" HeaderStyle-HorizontalAlign="Left">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="student_Name" runat="server" Text='<%# Bind("student_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="student_ID" runat="server" Text='<%# Bind("student_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PPCS Year" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="PPCSDate" runat="server" Text='<%# Bind("PPCSDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Course" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CourseCode" runat="server" Text='<%# Bind("CourseCode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="ClassCode" runat="server" Text='<%# Bind("ClassCode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="PPCSStatus" runat="server" Text='<%# Bind("PPCSStatus") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StatusTawaran" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="StatusTawaran" runat="server" Text='<%# Bind("StatusTawaran") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sebab Tolak" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="StatusReason" runat="server" Text='<%# Bind("StatusReason") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1"> <b> No PPCS Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>

        <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ViewUKM3Report" runat="server">
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlYear_UKM3" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlLevel_UKM3" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester : </asp:Label>
                <asp:DropDownList CssClass="btn btn-default font" ID="ddlSemester_UKM3" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
            </div>

            <br />
            <br />

           <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
                <asp:GridView ID="datRespondentUKM3" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
                    Width="97%" HeaderStyle-HorizontalAlign="Left">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="student_Name" runat="server" Text='<%# Bind("student_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="student_ID" runat="server" Text='<%# Bind("student_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UKM3 Year" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="ukm3Year" runat="server" Text='<%# Bind("ukm3Year") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UKM3 Year" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="ukm3Year" runat="server" Text='<%# Bind("ukm3Year") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="STEM Exam" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="marks_100" runat="server" Text='<%# Bind("marks_100") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pre-Test" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="markPretest" runat="server" Text='<%# Bind("markPretest") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Post-Test" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="markPosttest" runat="server" Text='<%# Bind("markPosttest") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Instr PPCS" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="insPPCS100mark" runat="server" Text='<%# Bind("insPPCS100mark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RA PPCS" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="insRAPPCS100mark" runat="server" Text='<%# Bind("insRAPPCS100mark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Instr KPP" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="insKPP100mark" runat="server" Text='<%# Bind("insKPP100mark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Composit Mark" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="compo_Mark" runat="server" Text='<%# Bind("compo_Mark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1"> <b> No UKM3 Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>
    </div>