<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="admin.studentprofile.view.popup.aspx.vb" Inherits="permatapintar.admin_studentprofile_view_popup" %>

<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view" TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/StudentUni_list.ascx" TagName="StudentUni_list" TagPrefix="uc2" %>
<%--<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc4" %>
<%@ Register Src="commoncontrol/studentuni_view.ascx" TagName="studentuni_view" TagPrefix="uc2" %>--%>
<%@ Register Src="commoncontrol/ukm1_history_list.ascx" TagName="ukm1_history_list" TagPrefix="uc5" %>
<%@ Register Src="commoncontrol/ukm2_history_list.ascx" TagName="ukm2_history_list" TagPrefix="uc6" %>
<%@ Register Src="commoncontrol/ppcs_history_list.ascx" TagName="ppcs_history_list" TagPrefix="uc7" %>
<%@ Register Src="commoncontrol/studentschool_list.ascx" TagName="studentschool_list" TagPrefix="uc8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sistem Pengurusan dan Pemantauan Ujian UKM1 & UKM2</title>
    <meta name="robots" content="noindex" />
    <meta content="" name="Keywords" />
    <meta content="Global" name="Distribution" />
    <meta content="jjamain@yahoo.com" name="Author" />
    <meta content="index,follow" name="Robots" />
    <link href="~/css/portal.default.css" rel="stylesheet" type="text/css" />
    <link href="~/css/table_style.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
</head>
<body class="fbbody">
    <form id="form1" runat="server">
        <div>
            <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
            &nbsp;<uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
            <%-- &nbsp;<uc4:studentschool_view ID="studentschool_view1" runat="server" />--%>
             &nbsp;<uc8:studentschool_list ID="studentschool_list1" runat="server" />
            <%--&nbsp;<uc2:studentuni_view ID="studentuni_view1" runat="server" />--%>
             &nbsp;<uc2:StudentUni_list ID="StudentUni_list1" runat="server" />
            &nbsp;<uc5:ukm1_history_list ID="ukm1_history_list1" runat="server" />
            &nbsp;<uc6:ukm2_history_list ID="ukm2_history_list1" runat="server" />
            &nbsp;<uc7:ppcs_history_list ID="ppcs_history_list1" runat="server" />
            &nbsp;
    
        </div>
        <asp:Label ID="lblLoginID" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblFullname" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblUserType" runat="server" Text="" Visible="false"></asp:Label>

    </form>
</body>
</html>

