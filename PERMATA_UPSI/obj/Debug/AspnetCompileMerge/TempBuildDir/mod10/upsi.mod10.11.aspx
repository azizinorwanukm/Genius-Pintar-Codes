<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod10.11.aspx.vb" Inherits="permata_upsi.upsi_mod10_11" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->

    <script src="../js/mod10.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod10_init($('#<%=btnNext.ClientID%>'), $('#<%=imgBlank.ClientID%>'), $('#<%=user_answer.ClientID%>'));
            
        });
    </script>
    
    <style>
        .dimension{
            width:100px;height:71px;
        }
        table{
            margin:0 auto;
        }
    </style>

    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="CANTUMAN GAMBAR"></asp:Label>
                </h3>

                <p style="margin-bottom:5px" id="lblInstruction" runat="server" class="lead">Gambar-gambar dibawah boleh dicantum menjadi durian. Sila tekan atas gambar dan tekan di petak kosong yang disediakan. Tekan “Seterusnya” apabila kamu telah selesai mencantum semua gambar. </p>
                <ul >
                    <li id="lblInstruction1" runat="server" class="lead" style="margin-bottom:0px">Sekiranya mahu menukar gambar atau lokasi, tekan pada gambar tersebut dan tekan di lokasi baru.</li>
                    <li id="lblInstruction2" runat="server" class="lead">Tekan dua kali di atas gambar untuk menghilangkan gambar tersebut.</li>
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
                                       
                                <input type="hidden" id="user_answer" runat="server" />
                                <input type="hidden" id="timeLeft" runat="server" />            
                                             
                                <div class="row">
                                    <div class="col-md-12">
                                        
                                        <br />                                        
                                        <table style="border-collapse: collapse; outline: 1px solid black; cursor: pointer;">
                                            <tr>
                                                <td>
                                                    <asp:Image ID="Image1" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,0);" /></td>
                                                <td>
                                                    <asp:Image ID="Image2" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,1);" /></td> 
                                                <td>
                                                    <asp:Image ID="Image7" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,2);" /></td>      
                                                <td>
                                                    <asp:Image ID="Image8" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,3);" /></td>                                        
                                            </tr>    
                                            <tr>
                                                
                                                <td>
                                                    <asp:Image ID="Image9" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,4);" /></td> 
                                                <td>
                                                    <asp:Image ID="Image10" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,5);" /></td>    
                                                <td>
                                                    <asp:Image ID="Image11" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,6);" /></td>
                                                <td>
                                                    <asp:Image ID="Image12" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,7);" /></td>                                          
                                            </tr>
                                            <tr>                                                 
                                                <td>
                                                    <asp:Image ID="Image13" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,8);" /></td>    
                                                <td>
                                                    <asp:Image ID="Image14" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,9);" /></td>
                                                <td>
                                                    <asp:Image ID="Image15" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,10);" /></td>
                                                <td>
                                                    <asp:Image ID="Image16" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,11);" /></td>                                          
                                            </tr> 
                                            <tr>                                                 
                                                <td>
                                                    <asp:Image ID="Image17" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,12);" /></td>    
                                                <td>
                                                    <asp:Image ID="Image18" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,13);" /></td>
                                                <td>
                                                    <asp:Image ID="Image19" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,14);" /></td>
                                                <td>
                                                    <asp:Image ID="Image20" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/blank.png" onclick="setImage(this,15);" /></td>                                          
                                            </tr>                                          
                                        </table>
                                        
                                    </div>
                                    <div class="col-md-12">
                                        
                                        <br />
                                        <asp:Image ID="imgBlank" ImageUrl="~/images/mod10/blank.png" Style="display: none" runat="server" />
                                        <table style="border-spacing: 10px;border-collapse: separate;" >
                                            <tr>
                                                <td>
                                                    <asp:Image ID="img01" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/mod10.11.01.gif" data-choose="mod10.11.01" Style="cursor: pointer" onclick="pushImage(this);" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="img02" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/mod10.11.02.gif" data-choose="mod10.11.02" Style="cursor: pointer" onclick="pushImage(this);" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="img03" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/mod10.11.03.gif" data-choose="mod10.11.03" Style="cursor: pointer" onclick="pushImage(this);" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="img04" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/mod10.11.04.gif" data-choose="mod10.11.04" Style="cursor: pointer" onclick="pushImage(this);" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="img05" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/mod10.11.05.gif" data-choose="mod10.11.05" Style="cursor: pointer" onclick="pushImage(this);" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="img06" runat="server" CssClass="dimension" ImageUrl="~/images/mod10/mod10.11.06.gif" data-choose="mod10.11.06" Style="cursor: pointer" onclick="pushImage(this);" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <br />
                        <br />

                        <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="center-block btn btn-outline btn-primary" Style="display: none" />

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
