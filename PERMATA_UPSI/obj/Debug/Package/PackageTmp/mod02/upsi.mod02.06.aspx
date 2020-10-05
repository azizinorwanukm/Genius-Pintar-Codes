<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod02.06.aspx.vb" Inherits="permata_upsi.upsi_mod02_06" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->

    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="MAKLUMAT"></asp:Label>
                </h3>
                <p class="lead">
                    <asp:Label ID="lblInstruction" runat="server" Text="Tekan pada orang yang umurnya paling tua. "></asp:Label>
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
                                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/mod02/mod02.06.01.gif" runat="server" CommandArgument="mod02.06.01" />
                                            </div>
                                            <div class="col-md-6" style="text-align: center">
                                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/mod02/mod02.06.02.gif" runat="server" CommandArgument="mod02.06.02" />
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-6" style="text-align: center">
                                                <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/mod02/mod02.06.03.gif" runat="server" CommandArgument="mod02.06.03" />
                                            </div>
                                            <div class="col-md-6" style="text-align: center">
                                                <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/mod02/mod02.06.04.gif" runat="server" CommandArgument="mod02.06.04" />
                                            </div>

                                        </div>




                                    </div>
                                </div>

                            </div>




                        </div>
                    </div>

                </div>
            </div>
            <!-- /.col-lg-12 -->

        </div>
        <!-- /.row -->
    </div>
    <!-- /.container-fluid -->

</asp:Content>
