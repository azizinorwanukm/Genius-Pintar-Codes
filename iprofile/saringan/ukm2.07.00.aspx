﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.07.00.aspx.vb" Inherits="permatapintar.ukm2_07_00" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/jscript">
<!-- 
    // 1996 by Christoph Bergmann... http://acc.de/cb
    // Please keep this note...
    // global variables
    var max1 = 0;
    var max2 = 0;
    var max3 = 0;
    var max4 = 0;
    var max5 = 0;
    var max6 = 0;
    //textlist
    function textlist1() {
        max1 = textlist1.arguments.length;
        for (i = 0; i < max1; i++)
            this[i] = textlist1.arguments[i];
    }

    var x1 = 0; pos1 = 0; x2 = 0; pos2 = 0; x3 = 0; pos3 = 0; x4 = 0; pos4 = 0; x5 = 0; pos5 = 0; x6 = 0; pos6 = 0;

    //textticker
    function textticker1() {
        var strDigit = aspnetForm.ctl00$ContentPlaceHolder1$st01.value;
        tl = new textlist1(strDigit + "     ");
        var l = tl[0].length;
        aspnetForm.txt_01.value = tl[x1].substring(0, pos1) + " ";
        if (pos1++ == l) {
            pos1 = 0;
            setTimeout("textticker1()", 90000000); //buat lama sebab x kasi tgk lagi
            x1++;
            if (x1 == max1)
                x1 = 0;
            l = tl[x1].length;
            aspnetForm.txt_01.value = "";
            aspnetForm.ctl00$ContentPlaceHolder1$txt07_01.disabled = false;
        } else
            setTimeout("textticker1()", 1000); //masa text
    }


    function btn_Onclick(strDigit, strBtn) {
        if (strBtn == "1") {
            aspnetForm.ctl00$ContentPlaceHolder1$st01.value = strDigit;
            aspnetForm.btn01.disabled = true;
            textticker1();
        }
    }

    // end -->
    </script>


    <input id="st01" name="st01" type="hidden" runat="server" />
   <%-- <a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="lblSample" runat="server" Text=""></asp:Label></h2>
    <h2>
        <asp:Label ID="ukm2_0700_header" runat="server" Text=""></asp:Label></h2>
    <p>&nbsp;</p>
    <asp:Label ID="lbl07_instruction_sample" runat="server" Text="Lihat nombor dan abjad yang dipaparkan pada skrin anda selepas menekan butang [Mula]. Taipkan semula <b>nombor mengikut urutan menaik diikuti oleh abjad urutan menaik</b> dalam ruangan yang disediakan.
        Ini adalah contoh dan latihan semata-mata, tiada permarkahan diambil."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="lbl07_01" runat="server" Text="" CssClass="lbl02"></asp:Label>
                <asp:Label ID="ukm2_0700_01" runat="server" Text="1. Tekan butang [Mula] untuk mula menjawab."></asp:Label><br />
                <input type="button" id="btn01" name="btn01" value="Mula   " onclick='Javascript: btn_Onclick("E 5 C 1", "1");'
                    class="mybutton" />

                <input type="text" name="txt_01" value='' class="InputDigit" size="25" readonly="readonly" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Label ID="ukm2_0700_02" runat="server" Text="2. Taipkan apa yang dipaparkan di sini (Jawapan soalan ini ialah 1 5 C E)."></asp:Label><br />
                <asp:TextBox ID="txt07_01" runat="server" TextMode="SingleLine" Rows="1" Width="250px"
                    CssClass="myInput" Text="" Enabled="false"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="mybutton" />
                <asp:Label ID="ukm2_0700_03" runat="server" Text="Tekan [Seterusnya >>] jika anda sudah bersedia dan faham apa yang perlu dilakukan."></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
