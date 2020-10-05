<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod07.01.aspx.vb" Inherits="permata_upsi.upsi_mod07_01" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <script src="../js/mod07.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod07_init($('#<%=btnNext.ClientID%>'), $('#<%=user_answer.ClientID%>'), 2);
            $('#myModal').modal('show');
        });
    </script>
    <style>        
        img{border-style: solid;border-color: white;border-width: 2px !important;}
    </style>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="KONSEP BERGAMBAR"></asp:Label>
                </h3>
                <p class="lead">
                    <asp:Label ID="lblInstruction" runat="server" Text="Lihat pada gambar-gambar di barisan pertama dan kedua. Pilih satu gambar dari baris pertama yang sama kumpulan dengan satu gambar di baris kedua."></asp:Label>
                </p>
            </div>
        </div>
        <p></p>
        <div class="row">
            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-body">

                        <div class="col-sm-12">

                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="row" style="outline: 1px solid black;">
                                        <div class="col-sm-6">
                                            <asp:Image ID="ImageButton1" ImageUrl="~/images/mod07/mod07.01.01.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod07.01.01" onclick="setImage(this,0);" />
                                        </div>
                                        <div class="col-sm-6 ">
                                            <asp:Image ID="ImageButton2" ImageUrl="~/images/mod07/mod07.01.02.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod07.01.02" onclick="setImage(this,0);" />
                                        </div>
                                    </div>
                                <br />
                                <br />
                           
                                <div class="row" style="outline: 1px solid black;">
                                    <div class="col-sm-6" >
                                        <asp:Image ID="ImageButton3" ImageUrl="~/images/mod07/mod07.01.03.png" runat="server" CssClass="  center-block " style="cursor:pointer" data-choose="mod07.01.03" onclick="setImage(this,1);" />
                                    </div>
                                    <div class="col-sm-6" >
                                        <asp:Image ID="ImageButton4" ImageUrl="~/images/mod07/mod07.01.04.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod07.01.04" onclick="setImage(this,1);" />
                                    </div>
                                </div>
                                </div>
                            </div>
                            <input type="hidden" id="user_answer" runat="server" />
                            <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="center-block btn btn-outline btn-primary" Style="display: none" />
                        </div>

                    </div>
                    <!-- /panel-body -->

                </div>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
    </div>

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
