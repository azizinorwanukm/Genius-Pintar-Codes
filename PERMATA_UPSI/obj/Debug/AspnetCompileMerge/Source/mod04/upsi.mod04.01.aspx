<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod04.01.aspx.vb" Inherits="permata_upsi.upsi_mod04_01" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->

    <script src="../js/mod04.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
                      
            mod04_init($('#<%=btnNext.ClientID%>'), $('#<%=user_answer.ClientID%>'));
            $('#myModal').modal('show');
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
                                                    <asp:Image ID="ImageButton1" ImageUrl="~/images/mod04/mod04.01.01.png" runat="server" /></td>
                                                <td style="width:100px"></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton2" ImageUrl="~/images/mod04/mod04.01.02.png" runat="server" style="cursor:pointer" data-choose="mod04.01.02" onclick="setImage(this,0);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton3" ImageUrl="~/images/mod04/mod04.01.03.png" runat="server" style="cursor:pointer" data-choose="mod04.01.03" onclick="setImage(this,0);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton4" ImageUrl="~/images/mod04/mod04.01.04.png" runat="server" style="cursor:pointer" data-choose="mod04.01.04" onclick="setImage(this,0);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton5" ImageUrl="~/images/mod04/mod04.01.05.png" runat="server" style="cursor:pointer" data-choose="mod04.01.05" onclick="setImage(this,0);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton6" ImageUrl="~/images/mod04/mod04.01.06.png" runat="server" style="cursor:pointer" data-choose="mod04.01.06" onclick="setImage(this,0);" /></td>
                                            </tr>
                                            <tr class="tr">
                                                <td style="background-color:#E0E0E0 ;width:1%">
                                                    <asp:Image ID="ImageButton7" ImageUrl="~/images/mod04/mod04.01.07.png" runat="server" /></td>
                                                <td style="width:100px"></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton8" ImageUrl="~/images/mod04/mod04.01.08.png" runat="server" style="cursor:pointer" data-choose="mod04.01.08" onclick="setImage(this,1);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton9" ImageUrl="~/images/mod04/mod04.01.09.png" runat="server" style="cursor:pointer" data-choose="mod04.01.09" onclick="setImage(this,1);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton10" ImageUrl="~/images/mod04/mod04.01.10.png" runat="server" style="cursor:pointer" data-choose="mod04.01.10" onclick="setImage(this,1);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton11" ImageUrl="~/images/mod04/mod04.01.11.png" runat="server" style="cursor:pointer" data-choose="mod04.01.11" onclick="setImage(this,1);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton12" ImageUrl="~/images/mod04/mod04.01.12.png" runat="server" style="cursor:pointer" data-choose="mod04.01.12" onclick="setImage(this,1);" /></td>
                                            </tr>
                                            <tr class="tr">
                                                <td style="background-color:#E0E0E0 ;width:1%">
                                                    <asp:Image ID="ImageButton13" ImageUrl="~/images/mod04/mod04.01.13.png" runat="server" /></td>
                                                <td style="width:100px"></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton14" ImageUrl="~/images/mod04/mod04.01.14.png" runat="server" style="cursor:pointer" data-choose="mod04.01.14" onclick="setImage(this,2);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton15" ImageUrl="~/images/mod04/mod04.01.15.png" runat="server" style="cursor:pointer" data-choose="mod04.01.15" onclick="setImage(this,2);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton16" ImageUrl="~/images/mod04/mod04.01.16.png" runat="server" style="cursor:pointer" data-choose="mod04.01.16" onclick="setImage(this,2);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton17" ImageUrl="~/images/mod04/mod04.01.17.png" runat="server" style="cursor:pointer" data-choose="mod04.01.17" onclick="setImage(this,2);" /></td>
                                                <td class="td">
                                                    <asp:Image ID="ImageButton18" ImageUrl="~/images/mod04/mod04.01.18.png" runat="server" style="cursor:pointer" data-choose="mod04.01.18" onclick="setImage(this,2);" /></td>
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
