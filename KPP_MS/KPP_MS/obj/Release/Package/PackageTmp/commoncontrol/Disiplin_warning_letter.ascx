<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_warning_letter.ascx.vb" Inherits="KPP_MS.Disiplin_warning_letter" %>
<%@ Register Assembly="TextboxioControl" Namespace="TextboxioControl" TagPrefix="textboxio" %>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="textboxio/textboxio.js"></script>
    <script>
        $(function () {
            $('.datepicker').datepicker({ dateFormat: 'dd MM yy' }).val();
        });
    </script>
</head>

<style type="text/css">
    .ddl {
        border-radius: 25px;
    }

    .CalendarCssClass {
        background-color: #990000;
        font-family: Century;
        text-transform: lowercase;
        width: 750px;
        border: 1px solid Olive;
    }
</style>

<asp:MultiView runat="server" ID="WarningLetterMultiView">
    <asp:View runat="server" ID="listWarningLetter">
        <div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

            <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Warning Letter Config</p>

            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left">
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px; margin-top: 5px">
                    <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
                    <asp:TextBox CssClass="textbox" class="form-control" ID="txtSearchContent" Style="border-radius: 25px; width: 85%" runat="server" Text="" placeholder="     By title"></asp:TextBox>
                </div>
                <div class="col-md-6 w3-text-black" style="text-align: left;">
                    <button id="Search" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
                </div>
            </div>

            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; height:200px">
                <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="9"
                    BackColor="#d9d9d9" DataKeyNames="id" BorderStyle="None" GridLines="None" ShowHeaderWhenEmpty="true" onitemediting="OnItemEditing"
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

                        <asp:TemplateField HeaderText="Title" >
                            <ItemTemplate>
                                <asp:Label ID="titleCol" class="id1" runat="server" Text='<%# Eval("title") %>' Width="400px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Year">
                            <ItemTemplate>
                                <asp:Label ID="yearCol" class="id1" runat="server" Text='<%# Eval("year") %>' Width="100px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />

                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label align="center" runat="server" Class="id1">No Warning Letter Created</asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>

            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left">
                <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px; margin-top: 5px">
                    <asp:Button runat="server" ID="createNewLetter" Text="Create Letter" class="btn btn-info" Style="background-color: #005580; border-radius: 25px;" />
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="editWarningLetter">
        <div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

            <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Warning Letter Page</p>

            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
                <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
                    <%--LETTER TITLE--%>
                    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                        <asp:Label CssClass="Label" runat="server" Style="width: 20%">Letter Title : </asp:Label>
                        <asp:TextBox ID="txtLetterTitle" runat="server" class="form-control" Style="width: 100%; border-radius: 25px"></asp:TextBox>
                    </div>
                    <%--LETTER CONTENT--%>
                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <textboxio:Textboxio runat="server" ID="txtLetterContent" Content="Write letter content here..." />
                        </div>
                    </div>

                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
                        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
                        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>