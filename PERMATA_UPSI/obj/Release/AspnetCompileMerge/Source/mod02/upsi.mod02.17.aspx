<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod02.17.aspx.vb" Inherits="permata_upsi.upsi_mod02_17" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    
    <style type="text/css">
       .finger{opacity:0.8;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.hover').hover(
                    function () { $(this).removeClass('finger') },
                    function () { $(this).addClass('finger') }
                );
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
                    <asp:Label ID="lblInstruction" runat="server" Text="Yang mana satu jari kelingking?"></asp:Label>
                </p>

            </div>
        </div>
        <div class="row">
            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-primary">

                    <div class="panel-body">

                        <div class="row">

                            <div class="col-xs-12">

                                <div class="row">
                                                                                                         
                                    <div class="col-xs-2 col-xs-offset-4" style="height: 400px; width: 58px;">
                                        <div class="hover finger">
                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/mod02/mod02.17.01.gif" runat="server" CommandArgument="mod02.17.01" />
                                        </div>
                                    </div>
                                    <div class="col-xs-2" style="height: 400px; width: 58px;;">
                                        <div class="hover finger">
                                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/mod02/mod02.17.02.gif" runat="server" CommandArgument="mod02.17.02" />
                                        </div>
                                    </div>
                                    <div class="col-xs-2" style="height: 400px; width: 58px;;">
                                        <div class="hover finger">
                                            <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/mod02/mod02.17.03.gif" runat="server" CommandArgument="mod02.17.03" />
                                        </div>
                                    </div>
                                    <div class="col-xs-2" style="height: 400px; width: 58px;;">
                                        <div class="hover finger">
                                            <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/mod02/mod02.17.04.gif" runat="server" CommandArgument="mod02.17.04" />
                                        </div>
                                    </div>
                                    <div class="col-xs-2" style="height: 400px; width: 58px;;">
                                        <div class="hover finger">
                                            <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/mod02/mod02.17.05.gif" runat="server" CommandArgument="mod02.17.05" />
                                        </div>
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

    
</asp:Content>
