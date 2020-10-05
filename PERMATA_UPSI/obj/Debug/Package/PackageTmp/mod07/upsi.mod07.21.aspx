<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod07.21.aspx.vb" Inherits="permata_upsi.upsi_mod07_21" %>


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
                                        <div class="col-sm-4">
                                            <asp:Image ID="Image1" ImageUrl="~/images/mod07/mod07.21.01.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.21.01" onclick="setImage(this,0);" />
                                        </div>
                                        <div class="col-sm-4 ">
                                            <asp:Image ID="Image2" ImageUrl="~/images/mod07/mod07.21.02.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.21.02" onclick="setImage(this,0);" />
                                        </div>
                                        <div class="col-sm-4 ">
                                            <asp:Image ID="Image3" ImageUrl="~/images/mod07/mod07.21.03.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.21.03" onclick="setImage(this,0);" />
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                    <div class="row" style="outline: 1px solid black;">
                                        <div class="col-sm-4">
                                            <asp:Image ID="Image4" ImageUrl="~/images/mod07/mod07.21.04.png" runat="server" CssClass="  center-block " Style="cursor: pointer" data-choose="mod07.21.04" onclick="setImage(this,1);" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Image ID="Image5" ImageUrl="~/images/mod07/mod07.21.05.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.21.05" onclick="setImage(this,1);" />
                                        </div>
                                        <div class="col-sm-4 ">
                                            <asp:Image ID="Image6" ImageUrl="~/images/mod07/mod07.21.06.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.21.06" onclick="setImage(this,1);" />
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
