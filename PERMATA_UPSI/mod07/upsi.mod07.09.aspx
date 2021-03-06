<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod07.09.aspx.vb" Inherits="permata_upsi.upsi_mod07_09" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/mod07.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod07_init($('#<%=btnNext.ClientID%>'), $('#<%=user_answer.ClientID%>'), 2);
        });
    </script>
    <style>
        img {
            border-style: solid;
            border-color: white;
            border-width: 2px !important;
        }
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
                                            <asp:Image ID="ImageButton1" ImageUrl="~/images/mod07/mod07.09.01.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.09.01" onclick="setImage(this,0);" />
                                        </div>
                                        <div class="col-sm-6 ">
                                            <asp:Image ID="ImageButton2" ImageUrl="~/images/mod07/mod07.09.02.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.09.02" onclick="setImage(this,0);" />
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                    <div class="row" style="outline: 1px solid black;">
                                        <div class="col-sm-6">
                                            <asp:Image ID="ImageButton3" ImageUrl="~/images/mod07/mod07.09.03.png" runat="server" CssClass="  center-block " Style="cursor: pointer" data-choose="mod07.09.03" onclick="setImage(this,1);" />
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Image ID="ImageButton4" ImageUrl="~/images/mod07/mod07.09.04.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.09.04" onclick="setImage(this,1);" />
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

</asp:Content>
