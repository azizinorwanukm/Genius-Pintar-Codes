<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_student_detail.ascx.vb" Inherits="KPP_MS.Disiplin_student_detail" %>
<%@ Register Assembly="TextboxioControl" Namespace="TextboxioControl" TagPrefix="textboxio" %>


<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../textboxio/textboxio.js"></script>
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

<script type="text/javascript">

    function date_Click() {
        if (divcalendar.style.display == "none")
            divcalendar.style.display = "";
        else
            divcalendar.style.display = "none";
    }
</script>

<asp:MultiView ID="disiplinDetailView" runat="server">
    <asp:View ID="caseDetail" runat="server">

        <div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
            <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Disciplin Detail</p>
            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
                <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
                        <!-- Student Name -->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Name : </asp:Label>
                            <asp:Label CssClass="Label" runat="server" ID="StudentNameLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
                        </div>

                        <!--student ID-->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student ID :</asp:Label>
                            <asp:Label CssClass="Label" runat="server" ID="StudentIdLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
                        </div>

                        <!-- Student MyKad -->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student MyKad : </asp:Label>
                            <asp:Label CssClass="Label" runat="server" ID="StudentMyKadLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
                        </div>

                        <!-- Student Class -->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Class : </asp:Label>
                            <asp:Label CssClass="Label" runat="server" ID="StudentClassLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
                        </div>

                        <!-- Date And Time -->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server"> Date : </asp:Label>
                            <asp:TextBox CssClass="ddl datepicker" ID="CurrentDate" runat="server" />
                            &#160; <i class="fa fa-calendar w3-medium w3-text-black"></i>
                        </div>
                    </div>

                    <div>
                        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
                            <!--disiplin drop down -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Diciplinary Category : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                                <asp:DropDownList ID="ddlCaseType" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl" Style="width: 100%"></asp:DropDownList>
                            </div>
                            <!--Complainant Dropdown list-->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Complainant Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                                <asp:DropDownList ID="ddlReporter" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl" Style="width: 100%"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
                            <!-- Detail Case -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Detail Case : </asp:Label>
                                <asp:TextBox ID="Detail_case" runat="server" TextMode="Multiline" class="form-control" Height="90" Style="width: 100%; border-radius: 25px"></asp:TextBox>
                            </div>
                            <!--Action Box -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Action : </asp:Label>
                                <asp:TextBox ID="Action_box" runat="server" TextMode="Multiline" Class="form-control" Height="90" Style="width: 100%; border-radius: 25px;"></asp:TextBox>
                            </div>
                        </div>

                        <div id="counselingDiv" class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;" runat="server">

                            <!-- Counseling Status -->
                            <div class="col-md-6 w3-text-black" style="text-align:left; padding-left: 23px;">
                                <asp:Label runat="server" CssClass="Label" Style="width: 20%">Counseling Status : </asp:Label>
                                <asp:Label ID="kslgStatusLbl" runat="server" CssClass="Label" Style="width: 20%"></asp:Label>
                            </div>
                            <!-- Counseling Session -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Detail Case : </asp:Label>
                                <asp:TextBox ID="kslgSession" runat="server" TextMode="Multiline" class="form-control" Height="90" Style="width: 100%; border-radius: 25px" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
                            <!-- Demerit Mark -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Demerit Mark : </asp:Label>
                                <asp:TextBox ID="demerit_mark" runat="server" TextMode="Multiline" class="form-control" Style="border-radius: 25px; height: 40px; width: 20%"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
                            <button id="BtnSimpanCase" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;">Update &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
                            <button id="BtnBackCase" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="warningLetterList" runat="server">

        <div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
            <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Warning Letters List</p>
            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
                <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
                        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
                            
                            <!-- Student Name -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Name : </asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlListStudentName" Style="width: 20%; font-weight: bold;"></asp:Label>
                            </div>

                            <!--student ID-->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student ID :</asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlStudentID" Style="width: 20%; font-weight: bold;"></asp:Label>
                            </div>

                            <!-- Student MyKad -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student MyKad : </asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlStudentMyKad" Style="width: 20%; font-weight: bold;"></asp:Label>
                            </div>

                            <!-- Student Class -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Class : </asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlClassName" Style="width: 20%; font-weight: bold;"></asp:Label>
                            </div>

                        </div>

                        <div>
                            <div style="overflow-y: scroll; overflow-x: hidden; height: 450px" class="table-responsive">
                                <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="9"
                                    BackColor="#d9d9d9" DataKeyNames="disiplin_id" BorderStyle="None" GridLines="None" ShowHeaderWhenEmpty="true" onitemediting="OnItemEditing"
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

                                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:Label ID="Dicipline_Date" class="id1" runat="server" Text='<%# Eval("Dicipline_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Case" ItemStyle-Width="200">
                                            <ItemTemplate>
                                                <asp:Label ID="case_Name" class="id1" runat="server" Text='<%# Eval("case_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case Category" ItemStyle-Width="200">
                                            <ItemTemplate>
                                                <asp:Label ID="case_Category" class="id1" runat="server" Text='<%# Eval("case_Category") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Counseling Status" ItemStyle-Width="200">
                                            <ItemTemplate>
                                                <asp:Label ID="counseling_status" class="id1" runat="server" Text='<%# Eval("kslr_status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <%--<asp:CommandField HeaderText="Write/Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />--%>
                                        <asp:TemplateField HeaderText="Write/Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton Width="22" Height="22" ID="btnEdit" CommandName="Edit" runat="server" ImageUrl="~/img/edit-11-512.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label align="center" runat="server" Class="id1">No warning letter available</asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="warningLetterContent" runat="server">
        <div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
            <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Warning Letters</p>
            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
                <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
                        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
                            
                            <!-- Student Name -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Name : </asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlStudentNameLbl" Style="width: 20%; font-weight: bold;" ></asp:Label>
                            </div>

                            <!--student ID-->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student ID(MyKad) :</asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlStudentIDLbl" Style="width: 20%; font-weight: bold;"></asp:Label>(<asp:Label CssClass="Label" runat="server" ID="wlStudentMyKadLbl" Style="width: 20%; font-weight: bold;"></asp:Label>)
                            </div>

                            <!-- Student Class -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Class : </asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlStudentClassLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
                            </div>

                            <!-- Case Name -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Case Name : </asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlCaseLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
                            </div>
                            <!-- Case Category -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Case Category : </asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlCaseCategoryLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
                            </div>
                            <!-- Case Date -->
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Case Date : </asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlCaseDate" Style="width: 20%; font-weight: bold;"></asp:Label>
                            </div>
                        </div>

                        <div ID="wlCounselingDiv" class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;" runat="server">
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Counseling Status : </asp:Label>
                                <asp:Label CssClass="Label" runat="server" ID="wlCounselingStatus" Style="width: 20%; font-weight: bold;"></asp:Label>
                                <br />
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Counseling Session : </asp:Label>
                                <asp:TextBox ID="wlCounselingSession" runat="server" TextMode="Multiline" class="form-control" Height="90" Style="width: 100%; border-radius: 25px" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>  
                        <div ID="wlContentDiv" class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;" runat="server">
                            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                                <asp:DropDownList ID="ddlLetterType" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl" Style="width: 100%"></asp:DropDownList>
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Title : </asp:Label>
                                <asp:TextBox ID="wlTitle" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Content : </asp:Label>
                                <textboxio:Textboxio runat="server" ID="letterContent" Content="Insert new content here"/>
                            </div>
                        </div>
                        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
                            <button id="wlSaveBtn" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
                            <button id="wlCancelBtn" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Cancel &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>