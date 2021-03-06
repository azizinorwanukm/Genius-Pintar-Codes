<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod07.33.aspx.vb" Inherits="permata_upsi.upsi_mod07_33" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/mod07.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod07_init($('#<%=btnNext.ClientID%>'), $('#<%=user_answer.ClientID%>'), 3);
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
                                            <asp:Image ID="Image1" ImageUrl="~/images/mod07/mod07.33.01.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.33.01" onclick="setImage(this,0);" />
                                        </div>
                                        <div class="col-sm-4 ">
                                            <asp:Image ID="Image2" ImageUrl="~/images/mod07/mod07.33.02.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.33.02" onclick="setImage(this,0);" />
                                        </div>
                                        <div class="col-sm-4 ">
                                            <asp:Image ID="Image3" ImageUrl="~/images/mod07/mod07.33.03.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.33.03" onclick="setImage(this,0);" />
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                    <div class="row" style="outline: 1px solid black;">
                                        <div class="col-sm-4">
                                            <asp:Image ID="Image4" ImageUrl="~/images/mod07/mod07.33.04.png" runat="server" CssClass="  center-block " Style="cursor: pointer" data-choose="mod07.33.04" onclick="setImage(this,1);" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Image ID="Image5" ImageUrl="~/images/mod07/mod07.33.05.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.33.05" onclick="setImage(this,1);" />
                                        </div>
                                        <div class="col-sm-4 ">
                                            <asp:Image ID="Image6" ImageUrl="~/images/mod07/mod07.33.06.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.33.06" onclick="setImage(this,1);" />
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                    <div class="row" style="outline: 1px solid black;">
                                        <div class="col-sm-4">
                                            <asp:Image ID="Image7" ImageUrl="~/images/mod07/mod07.33.07.png" runat="server" CssClass="  center-block " Style="cursor: pointer" data-choose="mod07.33.07" onclick="setImage(this,2);" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Image ID="Image8" ImageUrl="~/images/mod07/mod07.33.08.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.33.08" onclick="setImage(this,2);" />
                                        </div>
                                        <div class="col-sm-4 ">
                                            <asp:Image ID="Image9" ImageUrl="~/images/mod07/mod07.33.09.png" runat="server" CssClass=" center-block " Style="cursor: pointer" data-choose="mod07.33.09" onclick="setImage(this,2);" />
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
