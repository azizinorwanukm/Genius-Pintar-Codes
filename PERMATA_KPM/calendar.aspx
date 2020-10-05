<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="calendar.aspx.vb" Inherits="permatapintar.calendar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fake popup</title>

    <script type="text/javascript">
        function popupCalendar() {
            var dateField = document.getElementById('dateField'); // toggle the div 
            if (dateField.style.display == 'none')
                dateField.style.display = 'block';
            else dateField.style.display = 'none';
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="dateField" style="display: none;">
        <asp:Calendar ID="calDate" runat="server" />
    </div>
    <asp:TextBox ID="txtDate" runat="server" />
    <img src="img/cal.jpg" onclick="popupCalendar()" alt="X" />
    
    </form>
</body>
</html>
