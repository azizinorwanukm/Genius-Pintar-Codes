<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_dicipline.ascx.vb" Inherits="KPP_MS.student_dicipline" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="dicHidden" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="dicipline_info()" value="0">Dicipline Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;font-size:14px" id="dic_info">

        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Name: </asp:Label>
                <asp:Label CssClass="Label" ID="Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> class : </asp:Label>
                <asp:Label CssClass="Label" ID="class" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Date : </asp:Label>
                <asp:Label CssClass="Label" ID="CurrentDate" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Merit Balance : </asp:Label>
                <asp:Label CssClass="Label" ID="Merit" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Total Compound : </asp:Label>
                <asp:Label CssClass="Label" ID="Compound" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> RM </asp:Label>
            </div>


        </div>


        <br />
        <br />

        <div style="overflow-y: scroll; overflow-x: hidden; height: 250px" class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="9"
                BackColor="#d9d9d9" DataKeyNames="disiplin_id" BorderStyle="None" GridLines="None" ShowHeaderWhenEmpty="true"
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

                    <asp:TemplateField HeaderText="Dicipline Type">
                        <ItemTemplate>
                            <asp:Label ID="case_name" class="id1" runat="server" Text='<%# Eval("Case_Name") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Merit Point">
                        <ItemTemplate>
                            <asp:Label ID="Merit" class="id1" runat="server" Text='<%# Eval("Dicipline_Merit") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Compound">
                        <ItemTemplate>
                            <asp:Label ID="Compound" class="id1" runat="server" Text='<%# Eval("Dicipline_Compound") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="View Detail">
                        <ItemTemplate>
                            <asp:ImageButton Width="22" Height="22" ID="btnEdit" CommandName="Edit" runat="server" ImageUrl="~/img/Eye_Care_Services-512.png" />
                        </ItemTemplate>

                    </asp:TemplateField>


                </Columns>
                <EmptyDataTemplate>
                    <asp:Label align="center" runat="server" Class="id1">No diciplinary record</asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />

            </asp:GridView>


        </div>
        <div id="dic_view" style="display: none;" runat="server">
            <div class="gridViewRespond" style="width: 90%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 1px solid #8c8c8c; margin-left: auto; margin-bottom: auto; margin-right: auto; margin-top: auto">
                <p style="background-color: #800000; display: inline-block; width: 90%; border-radius: 25px">Case Detail</p>

                <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">

                    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                        <p></p>
                        <!-- Date And Time -->
                        <asp:Label CssClass="Label" runat="server"> Date : </asp:Label>
                        <asp:Label CssClass="Label" class="form-control" ID="dic_date" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:Label>

                    </div>

                    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                        <p></p>
                        <!-- Nama Pelapor -->
                        <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Complainant Name : </asp:Label>
                        <asp:Label CssClass="Label" class="form-control" ID="person_charge" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:Label>
                    </div>


                </div>


                <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">

                    <div class="col-md-8 w3-text-black" style="text-align: left; padding-left: 23px">
                        <p></p>
                        <!-- Detail Case -->
                        <asp:Label CssClass="Label" runat="server" Style="width: 20%">Detail Case : </asp:Label>
                        <asp:TextBox ID="case_box" runat="server" TextMode="Multiline" class="form-control" Height="90" Style="width: 100%; border-radius: 25px" disabled="false"></asp:TextBox>
                    </div>

                    <div class="col-md-8 w3-text-black" style="text-align: left; padding-left: 23px">
                        <p></p>
                        <!--Action Box -->
                        <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Action : </asp:Label>
                        <asp:TextBox ID="action_box" runat="server" TextMode="Multiline" Class="form-control" Height="90" Style="width: 100%; border-radius: 25px;" Enabled="false"></asp:TextBox>
                    </div>

                </div>

                <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
                    <button id="btnback" class="btn btn-info" runat="server" style="background-color: #ffdb4d; border-radius: 25px;"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
                </div>
            </div>
        </div>
        <br />
    </div>

</div>
<br />
