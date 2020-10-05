<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod03.24.aspx.vb" Inherits="permata_upsi.upsi_mod03_24" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->

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
                                            <td>
                                                <asp:Image ID="img1" ImageUrl="~/images/mod03/mod03.24.05.png" runat="server" CssClass="center-block" /></td>
                                            <td>
                                                <asp:Image ID="img2" ImageUrl="~/images/mod03/mod03.24.06.png" runat="server" CssClass="center-block" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Image ID="img3" ImageUrl="~/images/mod03/mod03.QQ.png" runat="server" CssClass="center-block" /></td>
                                            <td>
                                                <asp:Image ID="img4" ImageUrl="~/images/mod03/mod03.24.04.png" runat="server" CssClass="center-block" /></td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <table style="width: 99%; margin: auto;" border="1">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/mod03/mod03.24.01.png" runat="server" CssClass="center-block" CommandArgument="mod03.24.01" /></td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/mod03/mod03.24.02.png" runat="server" CssClass="center-block" CommandArgument="mod03.24.02" /></td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/mod03/mod03.24.03.png" runat="server" CssClass="center-block" CommandArgument="mod03.24.03" /></td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/mod03/mod03.24.04.png" runat="server" CssClass="center-block" CommandArgument="mod03.24.04" /></td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/mod03/mod03.24.05.png" runat="server" CssClass="center-block" CommandArgument="mod03.24.05" /></td>
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

</asp:Content>
