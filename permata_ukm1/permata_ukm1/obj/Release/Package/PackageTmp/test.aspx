<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="test.aspx.vb" Inherits="permatapintar.test" %>

<%@ OutputCache Duration="10" VaryByParam="p" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        THIS IS A TEST<br />
        <%= DateTime.Now.ToString() %>
        <br />
        <a href="?p=1">1</a><br />
        <a href="?p=2">2</a><br />
        <a href="?p=3">3</a><br />
        <a href="default.aspx">home</a><br />
    </div>
    </form>
</body>
</html>
