<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod06.00.aspx.vb" Inherits="permata_upsi.upsi_mod06_00" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->

    <script type="text/javascript">
        $(document).ready(function () {
            
            //$('#myModal').modal('show');
        });
    </script>

    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="PERSAMAAN"></asp:Label>
                </h3>

                <p style="margin-bottom: 5px" id="lblInstruction" runat="server" class="lead">Isikan jawapan dalam ruang yang disediakan.</p>
                <p style="margin-bottom: 5px" id="example" runat="server" class="lead">Isikan jawapan dalam ruang yang disediakan.</p>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" Interval="1000" Enabled="false"></asp:Timer>
                        <asp:Button ID="btnStart" runat="server" class=" btn btn-default invisible" Text="Mula" />
                        <button id="timer_progress" runat="server" onclick="return false;" type="button" class="btn btn-danger pull-right invisible">Time Remaining <span id="time_left" runat="server" class="badge "></span></button>
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

                        <br />
                        
                        <div class="col-md-12">

                            <div class="row">
                                <div class="col-md-12">

                                    <div style="margin:0 auto;text-align:center"><asp:Label ID="lblQuestion" runat="server" Text="NASI dan ROTI, kedua-duanya adalah "></asp:Label><asp:TextBox ID="txtAnswer" runat="server" Width="250">makanan</asp:TextBox></div>
                                    
                                </div>
                            </div>

                        </div>
                        
                        <br />
                        <br />                        
                        <br />

                        <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="center-block btn btn-outline btn-primary" />

                    </div>
                    <!-- /panel-body -->

                </div>
            </div>
            <!-- /.col-lg-12 -->

        </div>
        <!-- /.row -->
    </div>
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">                    
                    <h4 class="modal-title" id="lblModalTitle" runat="server">Perhatian</h4>
                </div>
                <div class="modal-body">
                    <div id="lblModalInstruction" runat="server"><p>Duty of an assistant is limited only to:</p><ol type="1"><li>Read the questions to the child <b>without any further guiding/prompting.</b></li><li>Click on the answer selected by the child.</li></ol><b>Please do not give any extra instructions or help.</b></div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnOK" runat="server" class="btn btn-default" data-dismiss="modal">OK</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>

