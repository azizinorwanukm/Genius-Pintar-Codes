<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_login_berjaya.aspx.vb" Inherits="KPP_MS.admin_login_berjaya" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
        <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Announcement</p>

        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
            <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label ID="Label1" runat="server" Text="Label"> Hello.. This is the test for dummy announcement.. Later will provide a form for a proper announcement for this system..</asp:Label>

            </div>
        </div>
        <p></p>
    </div>
    <br />

</asp:Content>
