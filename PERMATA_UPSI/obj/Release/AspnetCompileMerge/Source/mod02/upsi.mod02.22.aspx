<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod02.22.aspx.vb" Inherits="permata_upsi.upsi_mod02_22" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    
    <script src="../js/mod02.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod02_init($('#<%=btnNext.ClientID%>'), $('#<%=user_answer.ClientID%>'));
        });
    </script>
    <style>
        .box1 {
            position:absolute;
            cursor: pointer;
            top:50px;
            left:75px;
            width:100px;
            height:100px;
            border:1px solid black;
            background-color:transparent
        }
        .box2 {
            position:absolute;
            cursor: pointer;
            top:260px;
            left:180px;
            width:100px;
            height:100px;
            border:1px solid black;
            background-color:transparent
        }
        .box3 {
            position:absolute;
            cursor: pointer;
            top:195px;
            left:690px;
            width:100px;
            height:100px;
            border:1px solid black;
            background-color:transparent
        }
        .box4 {
            position:absolute;
            cursor: pointer;
            top:300px;
            left:740px;
            width:100px;
            height:100px;
            border:1px solid black;
            background-color:transparent
        }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="MAKLUMAT"></asp:Label>
                </h3>
                <p class="lead">
                    <asp:Label ID="lblInstruction" runat="server" Text="Tekan atas tempat dari mana kangaroo berasal."></asp:Label>
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
                                <div style="position:relative;width:1024px;height:450px; background-image: url('/images/mod02/map.png');background-size:100%;margin-left: auto; margin-right: auto;">
                                    <div class="box1" data-choose="mod02.22.01" onclick="setImage(this);"></div>
                                    <div class="box2" data-choose="mod02.22.02" onclick="setImage(this);"></div>
                                    <div class="box3" data-choose="mod02.22.03" onclick="setImage(this);"></div>
                                    <div class="box4" data-choose="mod02.22.04" onclick="setImage(this);"></div>
                                </div>

                            </div>
                            <input type="hidden" id="user_answer" runat="server" />
                            <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="center-block btn btn-outline btn-primary" style="display:none" />
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
