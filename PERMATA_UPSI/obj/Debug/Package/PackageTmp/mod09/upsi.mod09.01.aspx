<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod09.01.aspx.vb" Inherits="permata_upsi.upsi_mod9_01" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <script src="../js/mod09.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod09_init($('#<%=btnNext.ClientID%>'), $('#<%=imgBlank.ClientID%>'), $('#<%=user_answer.ClientID%>'), $('#<%=lblInstruction.ClientID%>'), $('#<%=hfInstruction1.ClientID%>').val(), $('#<%=hfInstruction2.ClientID%>').val());
            $('#myModal').modal('show');
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
                                        <div class="col-sm-12" style="text-align:center">
                                            <table class="table1">
                                            <tr>
                                                
                                                <td>
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/mod09/blank.png" /></td>                                               
                                                <td>
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/images/mod09/04.gif" /></td>                                                
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
                                                                                               
                                            </tr>
                                            
                                        </table>
                                        </div>
                                        <div class="col-sm-12"><br /></div>
                                        <div class="col-sm-12">
                                        <table style="border-collapse: collapse; cursor: pointer;">                                            
                                            <tr>
                                                
                                                <td>
                                                    <asp:Image ID="Image16" runat="server" ImageUrl="~/images/mod09/04.gif" data-choose="mod09.01.01" Style="cursor: pointer" onclick="pushImage(this);" /></td>  
                                                                                             
                                            </tr>
                                        </table>
                                        </div>

                                    </div>
                                    
                                </div>
                            </div>

                        </div>

                        <asp:Image ID="imgBlank" ImageUrl="~/images/mod09/blank.png" Style="display: none" runat="server" />
                        <input type="hidden" id="user_answer" runat="server" />

                        </div>
                        <br />
                        <br />
                        
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

    <!-- /#page-wrapper -->
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">                    
                    <h4 class="modal-title" id="lblModalTitle" runat="server">Perhatian</h4>
                </div>
                <div class="modal-body">
                    <p id="lblModalInstruction" runat="server">Tugas pembantu <b>hanya untuk membacakan soalan</b> kepada kanak-kanak <b>tanpa membimbing kanak-kanak</b> dan membantu menekan jawapan yang dipilih oleh kanak-kanak tersebut. <b>Jangan beri arahan atau bantuan selain itu.</b></p>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnOK" runat="server" class="btn btn-default" data-dismiss="modal">OK</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
