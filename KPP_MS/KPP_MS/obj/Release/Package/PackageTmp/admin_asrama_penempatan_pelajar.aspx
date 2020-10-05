<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_asrama_penempatan_pelajar.aspx.vb" Inherits="KPP_MS.admin_asrama_penempatan_pelajar" %>

<%@ Register Src="~/commoncontrol/student_AddHostel.ascx" TagPrefix="uc1" TagName="student_AddHostel" %>
<%@ Register Src="~/commoncontrol/hostel_EditStudentPlacement.ascx" TagPrefix="uc1" TagName="hostel_EditStudentPlacement" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; text-align:left; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

            setTimeout(function () {
                $("#alert_div").fadeTo(5000, 500).slideUp(500, function () {
                    $("#alert_div").remove();
                });
            }, 3000);
        }
    </script>

    <%--<asp:MultiView runat="server" ID="hostelPlacement">
        <asp:View ID="hsotelPlacementList" runat="server">
            <div>
                <uc1:student_AddHostel runat="server" id="student_AddHostel" />
            </div>
        </asp:View>
        <asp:View ID="hostelEditPlacement" runat="server">
            <div>
                <uc1:hostel_EditStudentPlacement runat="server" ID="hostel_EditStudentPlacement" />
            </div>
        </asp:View>
    </asp:MultiView>--%>
    <div>
        <uc1:student_AddHostel runat="server" id="student_AddHostel" />
    </div>
    <div class="messagealert" id="alert_container" style="text-align: center"></div> 
</asp:Content>
