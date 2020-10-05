<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.03.01.aspx.vb" Inherits="permatapintar.ukm2_03_01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/jscript">
<!-- 
    // 1996 by Christoph Bergmann... http://acc.de/cb
    // Please keep this note...
    // global variables
    var max1 = 0;
    
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

            //enable current box only
            aspnetForm.txt_01.value = "";
            aspnetForm.ctl00$ContentPlaceHolder1$txt03_01.disabled = false;
        } else
            setTimeout("textticker1()", 1000); //masa text
    }

    function btn_Onclick() {
        //alert("test");
        aspnetForm.ctl00$ContentPlaceHolder1$st01.value = aspnetForm.ctl00$ContentPlaceHolder1$lblQ.value;
        aspnetForm.ctl00$ContentPlaceHolder1$btnstart.disabled = true;
        textticker1();
    }

    // end -->
    </script>

    
    
    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_0300_header" runat="server" Text=""></asp:Label>. No#:<asp:Label ID="lblCount" runat="server" Text=""></asp:Label>&nbsp;
        
    </h2>
    <asp:Label ID="lbl03_instruction" runat="server" Text="Sila beri perhatian pada skrin anda.  Lihat nombor yang dipaparkan dan taipkan nombor itu semula <b>mengikut urutan seperti yang dipaparkan</b> dari hadapan ke belakang pada ruangan yang disediakan."></asp:Label>

    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="lbl03_01" runat="server" Text="" CssClass="lbl02"></asp:Label>
                <input type="button" id="btnstart" name="btnstart" value="Mula  " onclick="btn_Onclick();" class="mybutton" runat="server" />
                <input type="text" name="txt_01" value='' class="InputDigit" size="25" maxlength="25" readonly="readonly" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt03_01" runat="server" TextMode="SingleLine" Rows="1" Width="250px" CssClass="myInput" Enabled="false" onpaste="return false"></asp:TextBox></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnLangkau" runat="server" Text="Langkau Modul" CssClass="mybutton" />
                <img src="images/white-space.png" width="400px" alt="" />
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="mybutton" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>

    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

    <input id="lblQ" name="lblQ" type="hidden" runat="server" />
    <input id="st01" name="st01" type="hidden" runat="server" />

</asp:Content>
