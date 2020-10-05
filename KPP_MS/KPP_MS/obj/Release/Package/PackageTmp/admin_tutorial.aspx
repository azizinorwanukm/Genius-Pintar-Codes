<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_tutorial.aspx.vb" Inherits="KPP_MS.admin_tutorial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function subject_reg() {
            if (document.getElementById("tutorial_subject_reg").value == 0) {
                document.getElementById("subject_reg_tutorial").style.display = "block";
                document.getElementById("tutorial_subject_reg").value = 1;
            }

            else if (document.getElementById("tutorial_subject_reg").value == 1) {
                document.getElementById("subject_reg_tutorial").style.display = "none";
                document.getElementById("tutorial_subject_reg").value = 0;
            }
        }

        function class_reg() {
            if (document.getElementById("tutorial_class_reg").value == 0) {
                document.getElementById("class_reg_tutorial").style.display = "block";
                document.getElementById("tutorial_class_reg").value = 1;
            }

            else if (document.getElementById("tutorial_class_reg").value == 1) {
                document.getElementById("class_reg_tutorial").style.display = "none";
                document.getElementById("tutorial_class_reg").value = 0;
            }
        }

        function student_reg() {
            if (document.getElementById("tutorial_std_reg").value == 0) {
                document.getElementById("std_reg_tutorial").style.display = "block";
                document.getElementById("tutorial_std_reg").value = 1;
            }

            else if (document.getElementById("tutorial_std_reg").value == 1) {
                document.getElementById("std_reg_tutorial").style.display = "none";
                document.getElementById("tutorial_std_reg").value = 0;
            }
        }

        function coordinator_reg() {
            if (document.getElementById("tutorial_coordinator_reg").value == 0) {
                document.getElementById("coordinator_reg_tutorial").style.display = "block";
                document.getElementById("tutorial_coordinator_reg").value = 1;
            }

            else if (document.getElementById("tutorial_coordinator_reg").value == 1) {
                document.getElementById("coordinator_reg_tutorial").style.display = "none";
                document.getElementById("tutorial_coordinator_reg").value = 0;
            }
        }
    </script>

    <style>
        .ddl {
            border-radius: 25px;
        }
    </style>

    <div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
        <button id="tutorial_subject_reg" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="subject_reg()" value="0">Course Registration Tutorial <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
        <div style="display: none;" id="subject_reg_tutorial">
            <video width="400" controls>
                <source src="video/test.mp4" type="video/mp4">
            </video>
            <p></p>
        </div>
    </div>
    <br />

    <div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
        <button id="tutorial_class_reg" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="class_reg()" value="0">Class Registration Tutorial <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
        <div style="display: none;" id="class_reg_tutorial">
            <video width="400" controls>
                <source src="video/test.mp4" type="video/mp4">
            </video>
            <p></p>
        </div>
    </div>
    <br />

    <div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
        <button id="tutorial_std_reg" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_reg()" value="0">Student Registration Tutorial <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
        <div style="display: none;" id="std_reg_tutorial">
            <video width="400" controls>
                <source src="video/test.mp4" type="video/mp4">
            </video>
            <p></p>
        </div>
    </div>
    <br />

    <div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
        <button id="tutorial_coordinator_reg" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="coordinator_reg()" value="0">Coordinator Registration Tutorial <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
        <div style="display: none;" id="coordinator_reg_tutorial">
            <video width="400" controls>
                <source src="video/test.mp4" type="video/mp4">
            </video>
            <p></p>
        </div>
    </div>
    <br />

</asp:Content>
