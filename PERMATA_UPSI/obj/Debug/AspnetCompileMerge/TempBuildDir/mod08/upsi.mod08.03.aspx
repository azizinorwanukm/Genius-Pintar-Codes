<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod08.03.aspx.vb" Inherits="permata_upsi.upsi_mod08_03" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <script src="../js/mod08.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod08_init($('#<%=btnNext.ClientID%>'), $('#<%=user_answer.ClientID%>'), 2);
        });
    </script>
    <style>        
        img{border-style: solid;border-color: white;border-width: 2px !important;}
    </style>

    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="PENGESANAN"></asp:Label>
                </h3>
                <p class="lead">
                    <asp:Label ID="lblInstruction" runat="server" Text="Sekarang apabila anda tekan butang Mula, banyak gambar-gambar akan dipaparkan di skrin. Tekan pada gambar tumbuh-tumbuhan sahaja. Buat secepat yang mungkin."></asp:Label>
                </p>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" Interval="1000" Enabled="false"></asp:Timer>
                        <asp:Button ID="btnStart" runat="server" class=" btn btn-default invisible" Text="Mula" OnClientClick="$(specialRow).css('visibility', 'visible');" />
                        <button onclick="return false;" type="button" class="btn btn-danger pull-right">Time Remaining <span id="time_left" runat="server" class="badge "></span></button>
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

                                        <table style="width:100%">
                                            <tr >                                            
                                                <td class="td">
                                                    <asp:Image ID="Image1" ImageUrl="~/images/mod08/mod08.03.01.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.01" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image2" ImageUrl="~/images/mod08/mod08.03.02.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.02" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image3" ImageUrl="~/images/mod08/mod08.03.03.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.03" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image4" ImageUrl="~/images/mod08/mod08.03.04.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.04" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image5" ImageUrl="~/images/mod08/mod08.03.05.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.05" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image6" ImageUrl="~/images/mod08/mod08.03.06.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.06" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image7" ImageUrl="~/images/mod08/mod08.03.07.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.07" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image8" ImageUrl="~/images/mod08/mod08.03.08.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.08" onclick="setImage(this);" />
                                                </td>
                                            </tr>
                                            <tr class="tr">                                            
                                                <td class="td">
                                                    <asp:Image ID="Image9" ImageUrl="~/images/mod08/mod08.03.09.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.09" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image10" ImageUrl="~/images/mod08/mod08.03.10.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.10" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image11" ImageUrl="~/images/mod08/mod08.03.11.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.11" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image12" ImageUrl="~/images/mod08/mod08.03.12.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.12" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image13" ImageUrl="~/images/mod08/mod08.03.13.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.13" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image14" ImageUrl="~/images/mod08/mod08.03.14.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.14" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image15" ImageUrl="~/images/mod08/mod08.03.15.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.15" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image16" ImageUrl="~/images/mod08/mod08.03.16.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.16" onclick="setImage(this);" />
                                                </td>
                                            </tr>
                                            <tr class="tr">                                            
                                                <td class="td">
                                                    <asp:Image ID="Image17" ImageUrl="~/images/mod08/mod08.03.17.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.17" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image18" ImageUrl="~/images/mod08/mod08.03.18.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.18" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image19" ImageUrl="~/images/mod08/mod08.03.19.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.19" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image20" ImageUrl="~/images/mod08/mod08.03.20.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.20" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image21" ImageUrl="~/images/mod08/mod08.03.21.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.21" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image22" ImageUrl="~/images/mod08/mod08.03.22.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.22" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image23" ImageUrl="~/images/mod08/mod08.03.23.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.23" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image24" ImageUrl="~/images/mod08/mod08.03.24.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.24" onclick="setImage(this);" />
                                                </td>
                                            </tr>
                                            <tr class="tr">                                           
                                                <td class="td">
                                                    <asp:Image ID="Image25" ImageUrl="~/images/mod08/mod08.03.25.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.25" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image26" ImageUrl="~/images/mod08/mod08.03.26.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.26" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image27" ImageUrl="~/images/mod08/mod08.03.27.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.27" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image28" ImageUrl="~/images/mod08/mod08.03.28.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.28" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image29" ImageUrl="~/images/mod08/mod08.03.29.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.29" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image30" ImageUrl="~/images/mod08/mod08.03.30.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.30" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image31" ImageUrl="~/images/mod08/mod08.03.31.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.31" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image32" ImageUrl="~/images/mod08/mod08.03.32.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.32" onclick="setImage(this);" />
                                                </td>
                                            </tr>
                                            <tr class="tr">                                            
                                                <td class="td">
                                                    <asp:Image ID="Image33" ImageUrl="~/images/mod08/mod08.03.33.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.33" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image34" ImageUrl="~/images/mod08/mod08.03.34.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.34" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image35" ImageUrl="~/images/mod08/mod08.03.35.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.35" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image36" ImageUrl="~/images/mod08/mod08.03.36.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.36" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image37" ImageUrl="~/images/mod08/mod08.03.37.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.37" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image38" ImageUrl="~/images/mod08/mod08.03.38.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.38" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image39" ImageUrl="~/images/mod08/mod08.03.39.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.39" onclick="setImage(this);" />
                                                </td>
                                                <td class="td">
                                                    <asp:Image ID="Image40" ImageUrl="~/images/mod08/mod08.03.40.png" runat="server" CssClass=" center-block " style="cursor:pointer" data-choose="mod08.03.40" onclick="setImage(this);" />
                                                </td>
                                            </tr>
                                        </table>
                                    
                                    </div>
                                </div>
                                <br />
                                <input type="hidden" id="user_answer" runat="server" />
                                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="center-block btn btn-outline btn-primary" />
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

</asp:Content>
