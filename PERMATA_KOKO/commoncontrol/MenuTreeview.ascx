<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MenuTreeview.ascx.vb" Inherits="permatapintar.MenuTreeview" %>

<asp:PlaceHolder ID="ControlPlaceHolder" EnableTheming="true" runat="server"></asp:PlaceHolder>
<asp:XmlDataSource ID="MenuXmlDataSource" DataFile="menu_instruktor.xml" runat="server"></asp:XmlDataSource>

<br />
<br />
<asp:Label ID="Message" runat="server" />