<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod01.10.aspx.vb" Inherits="permata_upsi.upsi_mod1_10" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/mod01.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod01_init($('#<%=btnNext.ClientID%>'), $('#<%=imgBlank.ClientID%>'), $('#<%=user_answer.ClientID%>'), $('#<%=timeLeft.ClientID%>'));
            

        });
    </script>
    
    <!-- Page Content -->

    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="REKABENTUK BLOK"></asp:Label>
                </h3>
                <p style="margin-bottom:5px" id="lblInstruction1" runat="server" class="lead">Tekan blok di sebelah kanan dan kemudian tekan di mana-mana dalam Petak Kosong untuk membina Rekabentuk yang ditunjukkan. Tekan butang 'Seterusnya' setelah berpuas hati. </p>
                
                <ul >
                    <li id="lblInstruction2" runat="server" class="lead" style="margin-bottom:0px">Sekiranya mahu menukar blok atau lokasi, tekan pada blok tersebut dan tekan di lokasi baru.</li>
                    <li id="lblInstruction3" runat="server" class="lead">Tekan dua kali di atas blok untuk menghilangkan blok tersebut.</li>
                </ul>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" Interval="1000" Enabled="false"></asp:Timer>
                        <asp:Button ID="btnStart" runat="server" class=" btn btn-default invisible" Text="Mula" OnClientClick="$(specialRow).css('visibility', 'visible');" />
                        <button id="timer_progress" runat="server" onclick="return false;" type="button" class="btn btn-danger pull-right">Time Remaining <span id="time_left" runat="server" class="badge "></span></button>
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

                            <div class="col-md-12">

                                <div class="row">
                                    <div class="col-md-6">
                                        <span class="badge" id="title1" runat="server">Rekabentuk</span>
                                        <div style="margin-left: 300px;">
                                            <asp:Image ID="img00" ImageUrl="~/images/mod01/mod01.10.00.png" Width="100px"  runat="server"  />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <span class="badge" id="title2" runat="server">Petak Kosong</span>
                                        <br />
                                        <br />
                                        <input type="hidden" id="user_answer" runat="server" />
                                        <input type="hidden" id="timeLeft" runat="server" />
                                        <table style="border-collapse: collapse; margin-left: 100px;  outline: 1px solid black; width: 500px; cursor: pointer;">
                                            <tr>
                                                <td>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,0);" /></td>
                                                <td>
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,1);" /></td>
                                                <td>
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,2);" /></td>
                                                <td>
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,3);" /></td>
                                                <td>
                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,4);" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,5);" /></td>
                                                <td>
                                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,6);" /></td>
                                                <td>
                                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,7);" /></td>
                                                <td>
                                                    <asp:Image ID="Image9" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,8);" /></td>
                                                <td>
                                                    <asp:Image ID="Image10" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,9);" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Image ID="Image11" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,10);" /></td>
                                                <td>
                                                    <asp:Image ID="Image12" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,11);" /></td>
                                                <td>
                                                    <asp:Image ID="Image13" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,12);" /></td>
                                                <td>
                                                    <asp:Image ID="Image14" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,13);" /></td>
                                                <td>
                                                    <asp:Image ID="Image15" runat="server" ImageUrl="~/images/mod01/blank.png" onclick="setImage2(this,14);" /></td>
                                            </tr>
                                        </table>


                                    </div>
                                    <div class="col-md-6">
                                        <br />
                                        <p></p>

                                        <asp:Image ID="imgBlank" ImageUrl="~/images/mod01/blank.png" runat="server" Style="display: none" />
                                        <table border="0" style="width: 210px;" class="col-sm-offset-5 col-lg-offset-1">
                                            <tr style="height: 105px">
                                                <td>
                                                    <asp:Image ID="img01" ImageUrl="~/images/mod01/mod01.04.blue.png" data-choose="mod01.10.02" runat="server" Style="cursor: pointer" onclick="pushImage2(this);" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="img02" ImageUrl="~/images/mod01/mod01.01.01.png" data-choose="mod01.10.01" runat="server" Style="cursor: pointer" onclick="pushImage2(this);" />
                                                </td>
                                            </tr>
                                            <tr style="height: 105px">
                                                <td>
                                                    <asp:Image ID="img03" ImageUrl="~/images/mod01/mod01.09.01.png" data-choose="mod01.10.03" runat="server" Style="cursor: pointer" onclick="pushImage2(this);" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="img04" ImageUrl="~/images/mod01/mod01.09.02.png" data-choose="mod01.10.04" runat="server" Style="cursor: pointer" onclick="pushImage2(this);" />
                                                </td>
                                            </tr>
                                            <tr style="height: 105px">
                                                <td>
                                                    <asp:Image ID="img05" ImageUrl="~/images/mod01/mod01.09.03.png" data-choose="mod01.10.05" runat="server" Style="cursor: pointer" onclick="pushImage2(this);" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="img06" ImageUrl="~/images/mod01/mod01.09.04.png" data-choose="mod01.10.06" runat="server" Style="cursor: pointer" onclick="pushImage2(this);" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            
                        </div>
                        <br />
                        <br />
                        <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="center-block btn btn-outline btn-primary" />

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
