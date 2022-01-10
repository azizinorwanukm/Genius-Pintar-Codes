<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_tutorial.aspx.vb" Inherits="KPP_MS.pengajar_tutorial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .sc3::-webkit-scrollbar {
            height: 10px;
        }

        .sc3::-webkit-scrollbar-track {
            background-color: transparent;
        }

        .sc3::-webkit-scrollbar-thumb {
            background-color: #929B9E;
            border-radius: 3px;
        }

        .sc4::-webkit-scrollbar {
            width: 10px;
        }

        .sc4::-webkit-scrollbar-track {
            background-color: transparent;
        }

        .sc4::-webkit-scrollbar-thumb {
            background-color: #929B9E;
        }
    </style>

    <div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
        <%--Breadcrum--%>
        <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black font">
            Menu &nbsp; : Tutorial &nbsp; 
        </div>
    </div>

    <div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
        <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
            <button id="btnLecturer_Lecturer" runat="server" class="btn btn-info" style="display: inline-block; font-size: 0.8vw">Lecturer</button>
        </div>

        <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh; overflow-y: scroll;" class="sc4" runat="server">
            <div class="w3-text-black " style="text-align: left; padding-left: 1vw;">
                <p> 1. Register Class Tutorial</p>
                <video width="400" src="video/Lecturer Portal/Lecturer/Tutorial Lecturer Register Class.mp4" controls>
                </video>
            </div>

            &nbsp;

            <div class="w3-text-black " style="text-align: left; padding-left: 1vw;">
                <p> 2. Student Attendance Tutorial </p>
                <video width="400" src="video/Lecturer Portal/Lecturer/Tutorial Lecturer Student Attendance.mp4" controls>
                </video>
            </div>
        </div>

    </div>


</asp:Content>
