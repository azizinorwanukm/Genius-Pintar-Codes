<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="permata_upsi._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PCIS</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="Jamain Johari" />

    <!-- Bootstrap Core CSS -->
    <link href="bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="bower_components/metisMenu/dist/metisMenu.min.css" rel="stylesheet" />

    <!-- Timeline CSS -->
    <link href="dist/css/timeline.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="dist/css/sb-admin-2.css" rel="stylesheet" />

    <!-- Morris Charts CSS -->
    <link href="bower_components/morrisjs/morris.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="bower_components/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <!-- jQuery -->
    <script src="bower_components/jquery/dist/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="bower_components/bootstrap/dist/js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtmykad.ClientID%>").keypress(function (e) {

                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {

                    return false;
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <div class="container">
                <br />
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12" style="text-align: center">
                        <h2>PERMATA Children Intelligence Scale</h2>
                    </div>
                    <!--/span-->
                </div>
                <!--/row-->
                <div class="row">
                    <div class="col-md-4 col-md-offset-4">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <div class="login-panel panel panel-default">
                                    <div class="panel-heading" style="text-align: center">
                                        <h3 class="panel-title">
                                            <asp:Label ID="lblInstruction" runat="server">Sila Masukkan No. MyKid dan Pilihan Bahasa</asp:Label></h3>
                                    </div>
                                    <div class="panel-body">

                                        <fieldset>
                                            <div class="form-group">
                                                <input class="form-control" placeholder="No MyKid" id="txtmykad" type="text" autofocus runat="server" />
                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList class="form-control" ID="dlstLanguage" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value="BM" Text="Bahasa Melayu"></asp:ListItem>
                                                    <asp:ListItem Value="BI" Text="English"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <asp:Button ID="btnLogin" runat="server" Text="Log Masuk" class="btn btn-lg btn-success btn-block" />
                                            <br />
                                            <div id="lblalert" class="alert alert-danger" style="display: none" onclick="$(this).hide()"></div>
                                        </fieldset>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>


            </div>

            <div class="row">
                <div class="col-md-10 col-md-offset-1" style="text-align: center">
                    <h5>Program ini adalah kerjasama antara NCDRC, UPSI | Bahagian PERMATA | Pusat PERMATAPintar. Ianya dibuka kepada kanak-kanak yang berumur antara 2 hingga 4 tahun sahaja.
                    </h5>
                    <h5>Ujian ini dibuka secara online bermula <b>15 Julai 2017 sehingga 31 Ogos 2017</b> dan kanak-kanak boleh mengambil ujian pada bila-bila masa mengikut keselesaan dengan bantuan orang dewasa. Orang dewasa HANYA dibenarkan membacakan soalan kepada kanak-kanak dan menekan/menaip jawapan yang diberikan oleh kanak-kanak tersebut. Sila baca dan patuhi arahan yang diberikan di setiap muka.
                    </h5>
                    <h5>Sekiranya ujian ini dilakukan oleh pihak sekolah/Taska/pusat aktiviti bersama kanak-kanak, pihak tersebut bertanggungjawab untuk mendapatkan kebenaran dari ibu/bapa/penjaga kanak-kanak sebelum membenarkan kanak-kanak untuk menduduki ujian ini.
                    </h5>
                    <h5>
                        Kanak-kanak tidak diminta untuk menamatkan ujian dalam satu sesi yang panjang. Mereka <b>boleh mengambil ujian, berhenti dan menyambung semula </b>ujian pada bila-bila masa mengikut keselesaan dengan bantuan orang dewasa.
                    </h5>
                    <h5>
                        Orang dewasa <b>HANYA</b> dibenarkan membacakan soalan kepada kanak-kanak dan menekan/menaip jawapan yang diberikan oleh kanak-kanak tersebut. Sila baca dan patuhi arahan yang diberikan di setiap muka.
                    </h5>
                    <h5>Penilaian dan pemilihan bagi mengikuti perkhemahan sains akan dibuat selepas tarikh tutup ujian. Nama calon yang berjaya akan dimaklumkan melalui email sekitar November 2017. Oleh itu, sila pastikan maklumat email yang akan dimasukkan di bahagian profil adalah tepat dan terkini serta boleh dihubungi. 
                    </h5>

                </div>
                <!--/span-->
            </div>


        </div>

        <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="lblModalTitle">Perhatian</h4>
                    </div>
                    <div class="modal-body">
                        <div id="lblModalInstruction">
                            <p>Ujian ini telah DITAMATKAN</p>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnOK" runat="server" class="btn btn-default" data-dismiss="modal">OK</button>
                    </div>
                </div>

            </div>
        </div>

    </form>
</body>
</html>
