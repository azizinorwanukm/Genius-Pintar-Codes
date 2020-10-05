<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod04.08.aspx.vb" Inherits="permata_upsi.upsi_mod04_08" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <script src="../js/mod04.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod04_init($('#<%=btnNext.ClientID%>'), $('#<%=user_answer.ClientID%>'));
        });
    </script>
    <style>        
        td{height:150px;vertical-align:middle !important}    
        .td img{border-style: solid;border-color: white;border-width: 2px !important;}
        .tr{padding:100px; border-bottom-style:solid;border-bottom-color:black;border-bottom-width:4px;}
    </style>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="PENCARIAN SIMBOL"></asp:Label>
                </h3>
                <p class="lead">
                    <asp:Label ID="lblInstruction" runat="server" Text="Lihat haiwan di sebelah kiri. Tekan pada gambar haiwan di sebelah kanan yang sama dengan haiwan tersebut."></asp:Label>
                </p>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" Interval="1000" Enabled="false"></asp:Timer>
                        <asp:Button ID="btnStart" runat="server" class=" btn btn-default invisible" Text="Mula" OnClientClick="$(specialRow).css('visibility', 'visible');" />
                        <button onclick="return false;" type="button" style="display:none;">Time Remaining <span id="time_left" runat="server" class="badge "></span></button>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
        </div>

        <p></p>


        <div class="row">
            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-primary">

                    <div class="panel-body">


                        <div class="row invisible" id="specialRow">



                            <div class="col-sm-12">


                                <div class="row">
                                    <div class="col-sm-12">

                                        <input type="hidden" id="user_answer" runat="server" />
                                        <table class="table" style="border-top-style:solid;border-top-color:black;border-top-width:4px;">
                                            <tr class="tr">
                                                <td style="background-color:#E0E0E0 ;width:1%">
                                                    <asp:Image ID="ImageButton1" ImageUrl="~/images/mod04/mod04.08.01.png" runat="server" /></td>
                                                <td style="width:100px"></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton2" ImageUrl="~/images/mod04/mod04.08.02.png" runat="server" style="cursor:pointer" data-choose="mod04.08.02" onclick="setImage(this,0);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton3" ImageUrl="~/images/mod04/mod04.08.03.png" runat="server" style="cursor:pointer" data-choose="mod04.08.03" onclick="setImage(this,0);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton4" ImageUrl="~/images/mod04/mod04.08.04.png" runat="server" style="cursor:pointer" data-choose="mod04.08.04" onclick="setImage(this,0);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton5" ImageUrl="~/images/mod04/mod04.08.05.png" runat="server" style="cursor:pointer" data-choose="mod04.08.05" onclick="setImage(this,0);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton6" ImageUrl="~/images/mod04/mod04.08.06.png" runat="server" style="cursor:pointer" data-choose="mod04.08.06" onclick="setImage(this,0);" /></td>
                                            </tr>
                                            <tr class="tr">
                                                <td style="background-color:#E0E0E0 ;width:1%">
                                                    <asp:Image ID="ImageButton7" ImageUrl="~/images/mod04/mod04.08.07.png" runat="server" /></td>
                                                <td style="width:100px"></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton8" ImageUrl="~/images/mod04/mod04.08.08.png" runat="server" style="cursor:pointer" data-choose="mod04.08.08" onclick="setImage(this,1);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton9" ImageUrl="~/images/mod04/mod04.08.09.png" runat="server" style="cursor:pointer" data-choose="mod04.08.09" onclick="setImage(this,1);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton10" ImageUrl="~/images/mod04/mod04.08.10.png" runat="server" style="cursor:pointer" data-choose="mod04.08.10" onclick="setImage(this,1);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton11" ImageUrl="~/images/mod04/mod04.08.11.png" runat="server" style="cursor:pointer" data-choose="mod04.08.11" onclick="setImage(this,1);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton12" ImageUrl="~/images/mod04/mod04.08.12.png" runat="server" style="cursor:pointer" data-choose="mod04.08.12" onclick="setImage(this,1);" /></td>
                                            </tr>
                                            <tr class="tr">
                                                <td style="background-color:#E0E0E0 ;width:1%">
                                                    <asp:Image ID="ImageButton13" ImageUrl="~/images/mod04/mod04.08.13.png" runat="server" /></td>
                                                <td style="width:100px"></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton14" ImageUrl="~/images/mod04/mod04.08.14.png" runat="server" style="cursor:pointer" data-choose="mod04.08.14" onclick="setImage(this,2);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton15" ImageUrl="~/images/mod04/mod04.08.15.png" runat="server" style="cursor:pointer" data-choose="mod04.08.15" onclick="setImage(this,2);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton16" ImageUrl="~/images/mod04/mod04.08.16.png" runat="server" style="cursor:pointer" data-choose="mod04.08.16" onclick="setImage(this,2);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton17" ImageUrl="~/images/mod04/mod04.08.17.png" runat="server" style="cursor:pointer" data-choose="mod04.08.17" onclick="setImage(this,2);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton18" ImageUrl="~/images/mod04/mod04.08.18.png" runat="server" style="cursor:pointer" data-choose="mod04.08.18" onclick="setImage(this,2);" /></td>
                                            </tr>
                                            
                                        </table>
                                    </div>
                                </div>

                            </div>

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
    <script type="text/javascript">


        $("#TestJS").click(function () {
            alert("Picture 1 clicked!");
        })






    </script>
    <!-- /#page-wrapper -->
    <asp:Label ID="lblImg01" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg02" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg03" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg04" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg05" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg06" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg07" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg08" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg09" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg10" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg11" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg12" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg13" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg14" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblImg15" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblSelectedImageURL" runat="server" Text=""></asp:Label>

</asp:Content>
