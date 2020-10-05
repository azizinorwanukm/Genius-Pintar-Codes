<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod05.31.aspx.vb" Inherits="permata_upsi.upsi_mod5_31" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <script type="text/javascript">
        $(document).ready(function () {



            var $ImageButton1 = $('#<%=ImageButton1.ClientID%>');
            var $ImageButton2 = $('#<%=ImageButton2.ClientID%>');
            var $ImageButton3 = $('#<%=ImageButton3.ClientID%>');
            var $ImageButton4 = $('#<%=ImageButton4.ClientID%>');
            var $ImageButton5 = $('#<%=ImageButton5.ClientID%>');
            var $ImageButton6 = $('#<%=ImageButton6.ClientID%>');
            var $ImageButton7 = $('#<%=ImageButton7.ClientID%>');
            var $ImageButton8 = $('#<%=ImageButton8.ClientID%>');
            var $ImageButton9 = $('#<%=ImageButton9.ClientID%>');
            var $ImageButton10 = $('#<%=ImageButton10.ClientID%>');
            var $ImageButton11 = $('#<%=ImageButton11.ClientID%>');
            var $ImageButton12 = $('#<%=ImageButton12.ClientID%>');
            var $ImageButton13 = $('#<%=ImageButton13.ClientID%>');
            var $ImageButton14 = $('#<%=ImageButton14.ClientID%>');
            var $ImageButton15 = $('#<%=ImageButton15.ClientID%>');
            var $MainLbl = $('#<%=lblmod05_01.ClientID%>');
            var $button1 = $('#<%=button1.ClientID%>');
            var AnsArray = [];


            $button1.click(function () {

                $ImageButton1.css('visibility', 'visible');
                $ImageButton2.css('visibility', 'visible');
                $ImageButton3.css('visibility', 'visible');
                $ImageButton4.css('visibility', 'visible');
                $ImageButton5.css('visibility', 'visible');
                $MainLbl.text('Lihat pada gambar ini.');
                startCountdown(5);
            });



            

            $ImageButton5.click(function () {
                AnsArray.push('btn5');
                $(this).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }
            });

            $ImageButton6.click(function () {
                AnsArray.push('btn6');
                $(this).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }
            });

            $ImageButton7.click(function () {
                AnsArray.push('btn7');
                $($(this)).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }



            });

            $ImageButton8.click(function () {
                AnsArray.push('btn8');
                $($(this)).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }



            });


            $ImageButton9.click(function () {
                AnsArray.push('btn9');
                $($(this)).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }



            });

            $ImageButton10.click(function () {
                AnsArray.push('btn10');
                $($(this)).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }



            });

            $ImageButton11.click(function () {
                AnsArray.push('btn11');
                $($(this)).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }



            });

            $ImageButton12.click(function () {
                AnsArray.push('btn12');
                $($(this)).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }



            });

             $ImageButton13.click(function () {
                AnsArray.push('btn13');
                $($(this)).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }



             });

             $ImageButton14.click(function () {
                AnsArray.push('btn14');
                $($(this)).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }



             });

             $ImageButton15.click(function () {
                AnsArray.push('btn15');
                $($(this)).css({
                    'border-style': 'solid',
                    'border-color': 'Gray',
                    'border-width': '2px'
                });
                if (AnsArray.length == 5) {
                     <%-- //Ajax call to server & db here
            $.ajax({
                type: "POST",
                url: "upsi.mod01.01.aspx/SendClientDataToServer",
                data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    alert(response.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            
        });--%>
                    //Change the url here
                    window.location = 'upsi.mod01.01.aspx';
                }



            });

            

            function startCountdown(timeLeft) {

                var interval = setInterval(countdown, 1000);
                update();

                function countdown() {
                    if (--timeLeft > 0) {
                        update();
                    } else {
                        clearInterval(interval);
                        update();
                        completed()
                        //Show the alert message , change the message as per your need



                    };
                };

                function update() {

                    seconds = timeLeft % 5;
                    document.getElementById('time-left').innerHTML = seconds;

                };

                function completed() {
                    $ImageButton1.css('visibility', 'hidden');
                    $ImageButton2.css('visibility', 'hidden');
                    $ImageButton3.css('visibility', 'hidden');
                    $ImageButton4.css('visibility', 'hidden');
                    $ImageButton5.css('visibility', 'hidden');
                    $MainLbl.text('Sekarang tekan pada gambar yang ditunjukkan sebentar tadi.');
                    $ImageButton6.css('visibility', 'visible');
                    $ImageButton7.css('visibility', 'visible');
                    $ImageButton8.css('visibility', 'visible');
                    $ImageButton9.css('visibility', 'visible');
                    $ImageButton10.css('visibility', 'visible');
                    $ImageButton11.css('visibility', 'visible');
                    $ImageButton12.css('visibility', 'visible');
                    $ImageButton13.css('visibility', 'visible');
                    $ImageButton14.css('visibility', 'visible');
                    $ImageButton15.css('visibility', 'visible');
                    
                    
                };

            };


        });


    </script>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="Label1" runat="server" Text="MEMORI FOTOGRAFI"></asp:Label>
                </h3>
                <p class="lead">
                    <asp:Label ID="lblmod05_01" runat="server" Text="Sila tekan butang Mula dibawah."></asp:Label>
                </p>
                <p>

                    <asp:Button ID="button1" runat="server" Text="Mula" OnClientClick="return false; " />
                    <button onclick="return false;" type="button" class="btn btn-danger pull-right invisible">Time Remaining <span id="time-left" class="badge "></span></button>
                </p>
            </div>
            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-primary">

                    <div class="panel-body">




                        <div class="col-md-12">

                            <div class="row">
                                <div class="col-md-12">

                                    <div class="row">

                                        <div class="col-md-2 col-md-offset-1">
                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/mod05.31.01.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
                                        </div>
                                        <div class="col-md-2 ">
                                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/mod05.31.02.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/mod05.31.03.png" runat="server" CssClass="  center-block invisible" OnClientClick="return false;" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/mod05.31.04.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
                                        </div>
                                        <div class="col-md-2 ">
                                            <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/mod05.31.05.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
                                        </div>

                                    </div>
                                    <div class="row">


                                        
                                        
                                        <div class="col-md-2 col-md-offset-1">
                                            <asp:ImageButton ID="ImageButton6" ImageUrl="~/images/mod05.31.06.png" runat="server" CssClass="center-block invisible" OnClientClick="return false;" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:ImageButton ID="ImageButton7" ImageUrl="~/images/mod05.31.07.png" runat="server" CssClass="center-block invisible" OnClientClick="return false;" />
                                        </div>                                    
                                        <div class="col-md-2">
                                            <asp:ImageButton ID="ImageButton8" ImageUrl="~/images/mod05.31.04.png" runat="server" CssClass="center-block invisible" OnClientClick="return false;" />
                                        </div>
                                        <div class="col-md-2 ">
                                            <asp:ImageButton ID="ImageButton9" ImageUrl="~/images/mod05.31.08.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
                                        </div>
                                         <div class="col-md-2">
                                            <asp:ImageButton ID="ImageButton10" ImageUrl="~/images/mod05.31.09.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
                                        </div>
                                        </div>
                                    <div class="row">
                                        
                                       
                                        <div class="col-md-2 col-md-offset-1">
                                            <asp:ImageButton ID="ImageButton11" ImageUrl="~/images/mod05.31.02.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
                                        </div>
                                        <div class="col-md-2 ">
                                            <asp:ImageButton ID="ImageButton12" ImageUrl="~/images/mod05.31.10.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
                                        </div>
                                         <div class="col-md-2 ">
                                            <asp:ImageButton ID="ImageButton13" ImageUrl="~/images/mod05.31.05.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
                                        </div>
                                        <div class="col-md-2 ">
                                            <asp:ImageButton ID="ImageButton14" ImageUrl="~/images/mod05.31.01.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
                                        </div>
                                         <div class="col-md-2 ">
                                            <asp:ImageButton ID="ImageButton15" ImageUrl="~/images/mod05.31.03.png" runat="server" CssClass=" center-block invisible" OnClientClick="return false;" />
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
    </div>
    <!-- /.container-fluid -->

    <!-- /#page-wrapper -->
</asp:Content>

