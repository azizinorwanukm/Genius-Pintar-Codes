<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod09.02.aspx.vb" Inherits="permata_upsi.upsi_mod9_02" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <script src="../js/mod09.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod09_init($('#<%=btnNext.ClientID%>'), $('#<%=imgBlank.ClientID%>'), $('#<%=user_answer.ClientID%>'), $('#<%=lblInstruction.ClientID%>'), $('#<%=hfInstruction1.ClientID%>').val(), $('#<%=hfInstruction2.ClientID%>').val());

        });
    </script>
    <style>        
        img{border-style: solid;border-color: white;border-width: 1px !important;}
        table{margin:0 auto}
        table.table1  td {
            border:1px solid black;text-align:center;width:110px;
        }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="LOKASI HAIWAN"></asp:Label>
                </h3>
                <p class="lead" style="margin-bottom:5px">
                    <asp:Label ID="lblInstruction" runat="server" Text="Sila tekan butang Mula dibawah."></asp:Label><asp:HiddenField ID="hfInstruction1" runat="server" /><asp:HiddenField ID="hfInstruction2" runat="server" />
                </p>
                <ul id="instruction" style="display:none;">
                    <li id="lblInstruction2" runat="server" class="lead" style="margin-bottom:0px"></li>
                    <li id="lblInstruction3" runat="server" class="lead"></li>
                </ul>            
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" Interval="1000" Enabled="false"></asp:Timer>
                        <asp:Button ID="btnStart" runat="server" class=" btn btn-default invisible" Text="Mula" />
                        <button id="timer_progress" runat="server" onclick="return false;" type="button" class="btn btn-danger pull-right">Time Remaining <span id="time_left" runat="server" class="badge "></span></button>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
        </div>
        <p></p>
        <div class="row">


            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-primary">

                    <div class="panel-body">

                        <div class="row">
                        <div class="col-sm-12">

                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="row question" style="display:none;">
                                        <div class="col-sm-12">
                                            <table class="table1" >
                                            <tr>
                                                <td style="border:1px solid black">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/mod09/blank.png" /></td>
                                                <td>
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/mod09/01.gif" /></td>
                                                <td>
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/mod09/blank.png" /></td>
                                                                                               
                                            </tr>
                                            
                                        </table>
                                        </div>
                                    </div>
                                    <div class="waiting row" style="display:none;" ><div class="col-sm-12" style="text-align:center"><img src="/images/spinner.gif" width="50"  /></div></div>
                                    <div class="row answer" style="display:none;">
                                        <div class="col-sm-12">
                                        <table class="table1" style="cursor: pointer;">
                                            <tr>
                                                <td>
                                                    <asp:Image ID="Image24" runat="server" ImageUrl="~/images/mod09/blank.png" onclick="setImage(this,0);" /></td>
                                                <td>
                                                    <asp:Image ID="Image25" runat="server" ImageUrl="~/images/mod09/blank.png" onclick="setImage(this,1);" /></td>
                                                <td>
                                                    <asp:Image ID="Image26" runat="server" ImageUrl="~/images/mod09/blank.png" onclick="setImage(this,2);" /></td>                                                                                              
                                            </tr>
                                          
                                        </table>
                                        </div>
                                        <div class="col-sm-12"><br /></div>
                                        <div class="col-sm-12">
                                        <table style="border-collapse: collapse; cursor: pointer;">
                                            <tr>
                                                <td>
                                                    <asp:Image ID="Image13" runat="server" ImageUrl="~/images/mod09/01.gif" data-choose="mod09.02.01" Style="cursor: pointer" onclick="pushImage(this);" /></td>
                                                
                                            </tr>
                                           
                                        </table>
                                        </div>

                                        

                                    </div>
                                    
                                </div>
                            </div>

                        </div>
                            </div>
                            <br />
                            <br />

                            <asp:Image ID="imgBlank" ImageUrl="~/images/mod09/blank.png" Style="display: none" runat="server" />
                            <input type="hidden" id="user_answer" runat="server" />
                            <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="center-block btn btn-outline btn-primary" Style="display: none" />

                    </div>
                    <!-- /panel-body -->

                </div>
            </div>
            <!-- /.col-lg-12 -->
        </div>
    <!-- /.row -->
    </div>
    <!-- /.container-fluid -->


</asp:Content>
