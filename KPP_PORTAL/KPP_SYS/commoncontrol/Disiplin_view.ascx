<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_view.ascx.vb" Inherits="KPP_SYS.Disiplin_view" %>

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

    $(document).ready(function () {
        var accessMenu = document.getElementById('<%= hiddenaccess.ClientID %>').value;

        if (accessMenu == "PPE") {
            document.getElementById('btnReg').style.display = "block";
        }
        else if (accessMenu == "SA") {
            document.getElementById('btnReg').style.display = "block";
        }
    })

    function date_Click() {
        if (divcalendar.style.display == "none")
            divcalendar.style.display = "";
        else
            divcalendar.style.display = "none";
    }
</script>


<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $('.datepicker').datepicker({ dateFormat: 'dd MM yy' }).val();
        });

    </script>
</head>

<!--header-->
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Student Data</p>
    <!--direct search -->
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left">
        <div class="w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtstudent" Style=" border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / ID / IC"></asp:TextBox>
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search"><i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>

    </div>
    <!--dropdown search -->
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 10px; margin-top: 10px; margin-bottom: 10px; text-align: left;">
        <div class="w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_selectedindexchange" CssClass="btn btn-default font ddl"></asp:DropDownList>
            <asp:DropDownList ID="ddlLevelNaming" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLevelNaming_selectedindexchange" CssClass="btn btn-default font ddl"></asp:DropDownList>
            <asp:DropDownList ID="ddlClassnaming" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClassnaming_SelectedIndexChanged" CssClass="btn btn-default font ddl"></asp:DropDownList>
            <asp:DropDownList ID="ddlCasenaming" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCasenaming_SelectedIndexChanged" CssClass="btn btn-default font ddl"></asp:DropDownList>
        </div>
    </div>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 10px; margin-top: 10px; margin-bottom: 10px; text-align: left; padding-left: 10px">
        <div class="w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Date From : <i class="fa fa-calendar w3-medium w3-text-black"></i></asp:Label>
            <asp:TextBox CssClass="ddl datepicker" ID="StartDate" runat="server" />
           <asp:Label CssClass="Label" runat="server">      To : <i class="fa fa-calendar w3-medium w3-text-black"></i></asp:Label>
            <asp:TextBox CssClass="ddl datepicker" ID="EndDate" runat="server" />
        </div>
    </div>
</div>

<br />

<!--table untuk display data search -->
<div id="dic_table" style="display: block;" runat="server">
    <div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

        <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student List With Case</p>
        <br />

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

                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="300">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Class Name" ItemStyle-Width="90">
                        <ItemTemplate>
                            <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Case" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="case_Name" class="id1" runat="server" Text='<%# Eval("case_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit">
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
                    <asp:Label align="center" runat="server" Class="id1">No students conduct any diciplinary action</asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <div id="btnReg" style="display: none">
            <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-bottom: 10px;">
                <button id="btnRegDicipline" runat="server" type="button" class="btn btn-info" style="background-color: #009900; border-radius: 25px;" title="Add Student"><i class="fa fa-plus w3-large w3-text-white"></i></button>
                <br />
            </div>
        </div>
    </div>
</div>
<br />

<div id="dic_info" style="display: none;" runat="server">

    <div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
        <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Edit Dicipline Data</p>
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!-- Student Ic Number -->
                <asp:Label CssClass="Label" runat="server"> Student Ic Number : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="student_Mykad" Style="width: 100%; border-radius: 25px;" runat="server" Text="" Enabled="false"></asp:TextBox>

            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!-- Date And Time -->
                <asp:Label CssClass="Label" runat="server"> Date : <i class="fa fa-calendar w3-medium w3-text-black"></i> </asp:Label>
                <asp:TextBox CssClass="ddl datepicker" ID="SaveDate" runat="server" />

            </div>
        </div>
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!--student id -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Id : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i>  </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="student_id" Style="width: 100%; border-radius: 25px;" runat="server" Text="" Enabled="false"></asp:TextBox>

            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!-- Student Name -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Name : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Student_name" Style="width: 100%; border-radius: 25px;" runat="server" Text="" Enabled="false"></asp:TextBox>

            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!--student Kelas-->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Class : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Student_class" Style="width: 100%; border-radius: 25px;" runat="server" Text="" Enabled="false"></asp:TextBox>

            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!--id pelapor -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Complainant id  : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="pelapor_id" Style="width: 100%; border-radius: 25px;" runat="server" Text="" Enabled="false"></asp:TextBox>
            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!-- Nama Pelapor -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Complainant Name : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Person_charge" Style="width: 100%; border-radius: 25px;" runat="server" Text="" Enabled="false"></asp:TextBox>
            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!--disiplin drop down -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Diciplinary Type : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlDiciplinetype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="disiplin_onselectedindexchanged" CssClass="btn btn-default font ddl" Style="width: 100%"></asp:DropDownList>
            </div>

            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!--Merit Point -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Merit Point : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Merit" Style="width: 30%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
                <asp:Label CssClass="Label" runat="server" Style="width: 10%"> Points </asp:Label>
            </div>

            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!--Compound-->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Compound : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="student_compound" Style="width: 30%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
                <asp:Label CssClass="Label" runat="server" Style="width: 10%"> RM </asp:Label>
            </div>

            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!--checkbox untuk bill -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Bill :</asp:Label>
                <asp:CheckBox CssClass="textbox" class="form-control" ID="Checkbox2" runat="server" />
            </div>
        </div>



        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">

            <div class="col-md-8 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!-- Detail Case -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Detail Case : </asp:Label>
                <asp:TextBox ID="Detail_case" runat="server" TextMode="Multiline" class="form-control" Height="90" Style="width: 100%; border-radius: 25px"></asp:TextBox>
            </div>

            <div class="col-md-8 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <!--Action Box -->
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Action : </asp:Label>
                <asp:TextBox ID="Action_box" runat="server" TextMode="Multiline" Class="form-control" Height="90" Style="width: 100%; border-radius: 25px;"></asp:TextBox>
            </div>

        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
            <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;"><i class="fa fa-save w3-large w3-text-white"></i></button>
            <button id="btnback" class="btn btn-info" runat="server" style="background-color: #ffdb4d; border-radius: 25px;"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
        </div>

    </div>
</div>
<asp:HiddenField ID="hiddenaccess" runat="server" />
<asp:HiddenField ID="hiddenfield1" runat="server" />

