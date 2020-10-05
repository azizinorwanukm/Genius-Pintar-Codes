<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod02.01.aspx.vb" Inherits="permata_upsi.upsi_mod02_01" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <script type="text/javascript">
        $(document).ready(function () {
            
            $('#myModal').modal('show');
        });
    </script>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="MAKLUMAT"></asp:Label>
                </h3>
                <p class="lead">
                    <asp:Label ID="lblInstruction" runat="server" Text="Cuba lihat benda-benda di bawah. Tekan pada benda yang boleh anda minum."></asp:Label>
                </p>

            </div>
        </div>
        <div class="row">
            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-primary">

                    <div class="panel-body">

                        <div class="row">

                            <div class="col-md-12">

                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="row">
                                            <div class="col-md-6" style="text-align: center">
                                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/mod02/mod02.01.01.gif" runat="server" CommandArgument="mod02.01.01" /></div>
                                            <div class="col-md-6" style="text-align: center">
                                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/mod02/mod02.01.02.gif" runat="server" CommandArgument="mod02.01.02" /></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6" style="text-align: center">
                                                <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/mod02/mod02.01.03.gif" runat="server" CommandArgument="mod02.01.03" /></div>
                                            <div class="col-md-6" style="text-align: center">
                                                <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/mod02/mod02.01.04.gif" runat="server" CommandArgument="mod02.01.04" /></div>
                                        </div>

                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                    <!-- /panel-body -->

                </div>
            </div>
            <!-- /.col-lg-12 -->

        </div>
        <!-- /.row -->

    </div>
    <!-- /.container-fluid -->
     
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
