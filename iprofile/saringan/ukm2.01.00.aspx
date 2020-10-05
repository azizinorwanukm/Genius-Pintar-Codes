<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.01.00.aspx.vb" Inherits="permatapintar.ukm2_01_00" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" />
    </a>--%>
    <h1>Araken I-PROFILE</h1>
    <p>
        &nbsp;</p>
    <h2>
        <asp:Label ID="lblSample" runat="server" Text=""></asp:Label></h2>
    <h2>
        <asp:Label ID="ukm2_0100_01" runat="server" Text=""></asp:Label></h2>
    <p>
        &nbsp;</p>
    <asp:Label ID="lbl01_instruction_sample" runat="server" Text="Anda dikehendaki menyusun bentuk yang disediakan untuk membentuk rajah yang dipamerkan di bawah. <b>Klik dan heret</b> bentuk pilihan ke ruangan yang disediakan. 
    Ini adalah contoh dan latihan semata-mata, tiada permarkahan diambil."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td class="mytablemain_td">
                <asp:Label ID="ukm2_0100_02" runat="server" Text=""></asp:Label>
                <table class="mytableleft">
                    <tr>
                        <td class="mytableleft_td">
                            <div id="dZone1" style="width: 80px; height: 80px" class="DefaultDropZoneColor">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="mytableleft_td">
                            <div id="dZone2" style="width: 80px; height: 80px" class="DefaultDropZoneColor">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="mytablemain_td">
                <asp:Label ID="ukm2_0100_03" runat="server" Text=""></asp:Label>
                <table class="mytableright">
                    <tr>
                        <td class="mytableright_td">
                            <img src="images/01.01.01.gif" alt="answer3" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="mytablemain_td">
                <table class="mytablebottom">
                    <tr>
                        <td colspan="5">
                            <asp:Label ID="ukm2_0100_04" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="mytablebottom_td">
                            <div id="Ans1_a" class="dragElement">
                                <img src="images/answer1.gif" id="Img1" alt="ans1" />
                            </div>
                        </td>
                        <td class="mytablebottom_td">
                            <div id="Ans2_a" class="dragElement">
                                <img src="images/answer2.gif" id="Img2" alt="ans1" />
                            </div>
                        </td>
                        <td class="mytablebottom_td">
                            <div id="Ans3_a" class="dragElement">
                                <img src="images/answer3.gif" id="Img3" alt="ans1" />
                            </div>
                        </td>
                        <td class="mytablebottom_td">
                            <div id="Ans4_a" class="dragElement">
                                <img src="images/answer4.gif" id="Img4" alt="ans1" />
                            </div>
                        </td>
                        <td class="mytablebottom_td">
                            <div id="Ans5_a" class="dragElement">
                                <img src="images/answer5.gif" id="Img5" alt="ans1" />
                            </div>
                        </td>
                        <td class="mytablebottom_td">
                            <div id="Ans6_a" class="dragElement">
                                <img src="images/answer6.gif" id="Img6" alt="ans1" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnStart" runat="server" Text="Mula >>" CssClass="mybutton" />
                <asp:Label ID="ukm2_0100_05" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <input type="hidden" id="Ans1" name="Ans1" runat="server" />
                <input type="hidden" id="Ans2" name="Ans2" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>
     <asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>

    <script language="javascript" type="text/javascript">
        var mouseState = 'up';
        var clone = null;
        var totalPurchase = 0.0;
        var dropZoneArray = new Array(2);
        dropZoneArray[0] = "dZone1";
        dropZoneArray[1] = "dZone2";

        var titlePattern = ".+_lblTitle$"
        var pricePattern = ".+_lblPrice$"
        var dragElementPattern = ".+_a$";
        var uniqueNumber = 1;
        var ZoneIndex = 0;

        function ResetColor() {
            document.getElementById("dZone1").className = 'DefaultDropZoneColor';
            document.getElementById("dZone2").className = 'DefaultDropZoneColor';
        }

        function IsInDropZone(evtTarget) {
            var result = false;
            // iterate through the array and find it the id exists 
            for (i = 0; i < dropZoneArray.length; i++) {
                if (evtTarget.id == dropZoneArray[i]) {
                    result = true;
                    break;
                }
            }
            return result;
        }

        function SelectDropZone(evtTarget) {
            var result = 0;
            // iterate through the array and find it the id exists 
            for (i = 0; i < dropZoneArray.length; i++) {
                if (evtTarget.id == dropZoneArray[i]) {
                    result = i;
                    break;
                }
            }
            return result;
        }

        function MakeElementDraggable(obj) {
            var startX = 0;
            var startY = 0;

            function InitiateDrag(e) {
                mouseState = 'down';
                var evt = e || window.event;
                startX = parseInt(evt.clientX);
                startY = parseInt(evt.clientY);

                clone = obj.cloneNode(true);
                clone.style.position = 'absolute';
                clone.style.top = parseInt(startY) + parseInt(document.body.scrollTop) + parseInt(document.documentElement.scrollTop) + 'px';
                clone.style.left = parseInt(startX) + parseInt(document.body.scrollLeft) + parseInt(document.documentElement.scrollLeft) + 'px';

                document.body.appendChild(clone);
                document.onmousemove = Drag;
                document.onmouseup = Drop;

                return false;
            }

            function Drop(e) {
                var evt = e || window.event;
                var evtTarget = evt.target || evt.srcElement;
                var dZone = document.getElementById(dropZoneArray[ZoneIndex]);

                //redundant checking
                if ((ZoneIndex == 0) && (aspnetForm.ctl00$ContentPlaceHolder1$Ans1.value.length == 0)) {
                    AddAnswer();
                }
                if ((ZoneIndex == 1) && (aspnetForm.ctl00$ContentPlaceHolder1$Ans2.value.length == 0)) {
                    AddAnswer();
                }

                document.onmouseup = null;
                document.onmousemove = null;

                document.body.removeChild(clone);
                mouseState = 'up';
                ResetColor();
            }

            function AddAnswer(e) {
                var myimg = GetAnsImg();
                var evt = e || window.event;
                var evtTarget = evt.target || evt.srcElement;

                var dZone = document.getElementById(dropZoneArray[ZoneIndex]);
                var spaceNode = document.createTextNode('');
                var paragraphElement = document.createElement('p');

                // create the delete button 
                var deleteButton = document.createElement('button');
                deleteButton.value = 'Delete';
                deleteButton.innerHTML = 'Delete';

                var showImg = document.createElement('img');
                showImg.src = myimg;
                showImg.onclick = DeleteItem;

                var item = document.createElement('div');
                item.id = 'itemDiv' + uniqueNumber;

                item.appendChild(paragraphElement);
                item.appendChild(spaceNode);
                item.appendChild(showImg);
                dZone.appendChild(item);

                //insert the answer to hidden
                InsertAnswer(ZoneIndex, myimg, item.id);
                uniqueNumber++;
            }

            function InsertAnswer(zoneid, imgid, divid) {
                if (zoneid == 0) {
                    aspnetForm.ctl00$ContentPlaceHolder1$Ans1.value = zoneid + "," + imgid + "," + divid;
                }
                if (zoneid == 1) {
                    aspnetForm.ctl00$ContentPlaceHolder1$Ans2.value = zoneid + "," + imgid + "," + divid;
                }
            }

            function DeleteItem(e) {
                var evt = e || window.event;
                var evtTarget = evt.target || evt.srcElement;

                if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) { //test for MSIE x.x;
                    price = evtTarget.parentElement.childNodes[2].nodeValue;
                    evtTarget.parentElement.parentElement.removeChild(evtTarget.parentElement);

                    //delete answer 1
                    if (IsMatch(aspnetForm.ctl00$ContentPlaceHolder1$Ans1.value, ".+" + evtTarget.parentElement.id + "$")) {
                        aspnetForm.ctl00$ContentPlaceHolder1$Ans1.value = "";
                    }
                    //delete answer 2
                    if (IsMatch(aspnetForm.ctl00$ContentPlaceHolder1$Ans2.value, ".+" + evtTarget.parentElement.id + "$")) {
                        aspnetForm.ctl00$ContentPlaceHolder1$Ans2.value = "";
                    }
                }
                else {
                    price = evtTarget.parentNode.childNodes[2].nodeValue;
                    evtTarget.parentNode.parentNode.removeChild(evtTarget.parentNode);
                    //no firefox is allowed

                    //delete answer 1
                    if (IsMatch(aspnetForm.ctl00$ContentPlaceHolder1$Ans1.value, ".+" + evtTarget.parentNode.id + "$")) {
                        aspnetForm.ctl00$ContentPlaceHolder1$Ans1.value = "";
                    }
                    //delete answer 2
                    if (IsMatch(aspnetForm.ctl00$ContentPlaceHolder1$Ans2.value, ".+" + evtTarget.parentNode.id + "$")) {
                        aspnetForm.ctl00$ContentPlaceHolder1$Ans2.value = "";
                    }

                }
            }

            function GetAnsImg() {
                var myimg = '';

                if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) { //test for MSIE x.x;
                    var ieversion = new Number(RegExp.$1) // capture x.x portion and store as a number
                    //alert("You're using IE:" + ieversion);

                    if (ieversion >= 9)
                        myimg = (clone.childNodes[1].src);
                    else if (ieversion >= 8)
                        myimg = (clone.childNodes[0].src);
                    else
                        myimg = (clone.childNodes[0].src);
                }
                else {
                    myimg = (clone.childNodes[1].src);
                    //alert("You're using :" + navigator.userAgent);
                }


                return myimg;
            }

            function IsFireFox() {
                if (navigator.appName == 'Netscape')
                    return true;
                else return false;
            }

            function Drag(e) {
                if (mouseState == 'down') {
                    // only drag when the mouse is down  
                    var evt = e || window.event;
                    var evtTarget = evt.target || evt.srcElement;

                    clone.style.top = evt.clientY + parseInt(document.body.scrollTop) + parseInt(document.documentElement.scrollTop) + 'px';
                    clone.style.left = evt.clientX + parseInt(document.body.scrollLeft) + parseInt(document.documentElement.scrollLeft) + 'px';

                    // Check if we are in the drop Zone 
                    if (IsInDropZone(evtTarget)) {
                        dropZoneObject = evt.srcElement;
                        evtTarget.className = 'highlightDropZone';
                        //firefoxZoneIndex=SelectDropZone(evtTarget)//SelectDropZone(evt.srcElement);  
                        ZoneIndex = SelectDropZone(evt.srcElement);
                    }
                    else {
                        ResetColor();
                    }
                }
            }
            obj.onmousedown = InitiateDrag;
        }

        function IsMatch(id, pattern) {
            var regularExpresssion = new RegExp(pattern);
            if (id.match(regularExpresssion)) return true;
            else return false;
        }

        var divElements = document.getElementsByTagName("div");
        for (i = 0; i < divElements.length; i++) {
            if (IsMatch(divElements[i].id, dragElementPattern)) {
                MakeElementDraggable(divElements[i]);
            }
        }
    </script>

</asp:Content>
