<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.11.02.aspx.vb" Inherits="permatapintar.ukm2_11_02" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function clicked_on(strValue) {
            aspnetForm.ctl00$ContentPlaceHolder1$Sel01.value = strValue;
            document.onmousedown = CreateX;
        }

        function CreateX(e) {
            //ASSIGN 0 or 1
            var evt = e || window.event;
            var evtTarget = evt.target || evt.srcElement;
            //alert(evtTarget.id);

            //DISPLAY X
            if ((evtTarget.id == "0") || (evtTarget.id == "1")) {
                var the_element = document.getElementById("itemDiv01");
                if (the_element !== null) {
                    the_element.parentNode.removeChild(the_element);
                }

                var item = document.createElement('div');
                item.id = 'itemDiv01'
                var spaceNode = document.createTextNode('X');
                item.appendChild(spaceNode);

                startX = event.clientX;
                startY = event.clientY;

                item.className = "PictSel";
                item.style.border = '0px';
                item.style.position = 'absolute';
                item.style.top = parseInt(startY) + parseInt(document.body.scrollTop) + parseInt(document.documentElement.scrollTop) + 'px';
                item.style.left = parseInt(startX) + parseInt(document.body.scrollLeft) + parseInt(document.documentElement.scrollLeft) + 'px';
                document.body.appendChild(item);

                //To get the result
                aspnetForm.ctl00$ContentPlaceHolder1$Sel01.value = evtTarget.id;
            }
        }
    </script>

    <map name="shapes" id="shapes">
        <area onfocus="if(this.blur)this.blur()" class="mapnoborder" alt="X" shape="poly"
            coords="170,195,50,350,90,350,195,225"
            href="javascript:clicked_on('1');" id="1" />

        <area onfocus="if(this.blur)this.blur()" class="mapnoborder" alt="X" shape="rect"
            coords="0,0,400,400"
            href="javascript:clicked_on('0');" id="0" />

    </map>

    <input type="hidden" id="Sel01" runat="server" />
    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_1100_header" runat="server" Text=""></asp:Label>[02/38]</h2>
    <asp:Label ID="lbl11_instruction" runat="server" Text="Perhatikan gambar berikut. <b>Sila tunjuk dan klik pada bahagian yang tidak lengkap</b> untuk setiap paparan. Tanda X akan ditanda pada kawasan pilihan anda."></asp:Label>

    <table class="mytablemain">
        <tr>
            <td>
                <div id="divPic">
                    <div id="itemDiv01"></div>
                    <img src="images/11.02.jpg" alt="pic1" style="cursor: default;" usemap="#shapes" border='0' />
                </div>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Button ID="btnLangkau" runat="server" Text="Langkau Modul" CssClass="mybutton" /><img src="images/white-space.png" width="400px" alt="" />
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

</asp:Content>
