<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod03.01.aspx.vb" Inherits="permata_upsi.upsi_mod03_01" %>


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
                    <asp:Label ID="lblTitle" runat="server" Text="PENAAKULAN ABSTRAK"></asp:Label>

                </h3>
                <p class="lead">
                    <asp:Label ID="lblInstruction" runat="server" Text="Pilih satu gambar di bawah untuk dimasukkan ke dalam kotak bertanda soal."></asp:Label>
                    <span id="time-left" class="badge invisible  "></span>
                </p>

            </div>
        </div>
        <div class="row">
            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-primary">

                    <div class="panel-body">

                        <div class="row">

                            <div class="col-sm-12">
                                <div class="row">
                                    <table border="1" style="margin: auto;">
                                        <tr>
                                            <td><asp:Image ID="img1" ImageUrl="~/images/mod03/mod03.00.01.png" runat="server" CssClass="center-block" /></td>
                                            <td><asp:Image ID="img2" ImageUrl="~/images/mod03/mod03.00.01.png" runat="server" CssClass="center-block" /></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Image ID="img3" ImageUrl="~/images/mod03/mod03.00.01.png" runat="server" CssClass="center-block" /></td>
                                            <td><asp:Image ID="img4" ImageUrl="~/images/mod03/mod03.00.QQ.png" runat="server" CssClass="center-block" /></td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <table style="width: 99%; margin: auto;" border="1">
                                        <tr>
                                            <td><asp:ImageButton ID="ImageButton1" ImageUrl="~/images/mod03/mod03.00.01.png" runat="server" CssClass="center-block" CommandArgument="mod03.00.01" /></td>
                                            <td><asp:ImageButton ID="ImageButton2" ImageUrl="~/images/mod03/mod03.00.02.png" runat="server" CssClass="center-block" CommandArgument="mod03.00.02" /></td>
                                            <td><asp:ImageButton ID="ImageButton3" ImageUrl="~/images/mod03/mod03.00.03.png" runat="server" CssClass="center-block" CommandArgument="mod03.00.03" /></td>
                                            <td><asp:ImageButton ID="ImageButton4" ImageUrl="~/images/mod03/mod03.00.04.png" runat="server" CssClass="center-block" CommandArgument="mod03.00.04" /></td>                                            
                                        </tr>
                                    </table>
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
