<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod10.01.aspx.vb" Inherits="permata_upsi.upsi_mod10_01" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->

    <script src="../js/mod10.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod10_init($('#<%=btnNext.ClientID%>'), $('#<%=imgBlank.ClientID%>'), $('#<%=user_answer.ClientID%>'));
            $('#myModal').modal('show');
        });
    </script>

    <style>
        .dimension {
            width: 100px;
            height: 125px;
        }

        table {
            margin: 0 auto;
        }
    </style>

    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:label id="lblTitle" runat="server" text="CANTUMAN GAMBAR"></asp:label>
                </h3>

                <p style="margin-bottom: 5px" id="lblInstruction" runat="server" class="lead">Gambar-gambar dibawah boleh dicantum menjadi durian. Sila tekan atas gambar dan tekan di petak kosong yang disediakan. Tekan “Seterusnya” apabila kamu telah selesai mencantum semua gambar. </p>

                <ul >
                    <li id="lblInstruction1" runat="server" class="lead" style="margin-bottom:0px">Sekiranya mahu menukar gambar atau lokasi, tekan pada gambar tersebut dan tekan di lokasi baru.</li>
                    <li id="lblInstruction2" runat="server" class="lead">Tekan dua kali di atas gambar untuk menghilangkan gambar tersebut.</li>
                </ul>

                <asp:updatepanel id="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" Interval="1000" Enabled="false"></asp:Timer>
                        <asp:Button ID="btnStart" runat="server" class=" btn btn-default invisible" Text="Mula" OnClientClick="$(specialRow).css('visibility', 'visible');" />
                        <button id="timer_progress" runat="server" onclick="return false;" type="button" class="btn btn-danger pull-right">Time Remaining <span id="time_left" runat="server" class="badge "></span></button>
                    </ContentTemplate>
                </asp:updatepanel>

            </div>
        </div>
        <p></p>
        <div class="row">
            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-primary">

                    <div class="panel-body">

                        <div class="row invisible" id="specialRow">

                            <div class="col-md-12">

                                <input type="hidden" id="user_answer" runat="server" />
                                <input type="hidden" id="timeLeft" runat="server" />

                                <div class="row">
                                    <div class="col-md-12">

                                        <br />
                                        <table style="border-collapse: collapse; outline: 1px solid black; cursor: pointer;">
                                            <tr>
                                                <td>
                                                    <asp:image id="Image1" runat="server" cssclass="dimension" imageurl="~/images/mod10/blank.png" onclick="setImage(this,0);" />
                                                </td>
                                                <td>
                                                    <asp:image id="Image2" runat="server" cssclass="dimension" imageurl="~/images/mod10/blank.png" onclick="setImage(this,1);" />
                                                </td>
                                                <td>
                                                    <asp:image id="Image3" runat="server" cssclass="dimension" imageurl="~/images/mod10/blank.png" onclick="setImage(this,2);" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:image id="Image4" runat="server" cssclass="dimension" imageurl="~/images/mod10/blank.png" onclick="setImage(this,3);" />
                                                </td>
                                                <td>
                                                    <asp:image id="Image5" runat="server" cssclass="dimension" imageurl="~/images/mod10/blank.png" onclick="setImage(this,4);" />
                                                </td>
                                                <td>
                                                    <asp:image id="Image6" runat="server" cssclass="dimension" imageurl="~/images/mod10/blank.png" onclick="setImage(this,5);" />
                                                </td>

                                            </tr>

                                        </table>

                                    </div>
                                    <div class="col-md-12">

                                        <br />
                                        <asp:image id="imgBlank" imageurl="~/images/mod10/blank.png" style="display: none" runat="server" />
                                        <table style="border-spacing: 10px; border-collapse: separate;">
                                            <tr>
                                                <td>
                                                    <asp:image id="img01" runat="server" cssclass="dimension" imageurl="~/images/mod10/mod10.01.01.gif" data-choose="mod10.01.01" style="cursor: pointer" onclick="pushImage(this);" />
                                                </td>
                                                <td>
                                                    <asp:image id="img02" runat="server" cssclass="dimension" imageurl="~/images/mod10/mod10.01.02.gif" data-choose="mod10.01.02" style="cursor: pointer" onclick="pushImage(this);" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <br />
                        <br />

                        <asp:button id="btnNext" runat="server" text="Seterusnya >>" cssclass="center-block btn btn-outline btn-primary" style="display: none" />

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
