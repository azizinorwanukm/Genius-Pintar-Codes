<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="menu_left.ascx.vb" Inherits="araken.pcisadmin.menu_left" %>

<table id="newspaper-a" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Login Profile</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Login ID:<asp:Label ID="lblLoginID" runat="server" Text="" CssClass="fblabel_view"></asp:Label></td>
        </tr>
        <tr>
            <td>Name:<asp:Label ID="lblFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label></td>
        </tr>
        <tr>
            <td>User Type:<asp:Label ID="lblUserType" runat="server" Text="" CssClass="fblabel_view"></asp:Label></td>
        </tr>
    </tbody>
</table>
&nbsp;
<table id="newspaper-b" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Profil Pelajar</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="admin.studentprofile.search.aspx" rel="nofollow" target="_self">Carian Pelajar</a></td>
        </tr>
        <tr>
            <td><a href="admin.studentprofile.create.aspx" rel="nofollow" target="_self">Daftar Pelajar Baru</a></td>
        </tr>
    </tbody>
</table>
&nbsp;
<table id="newspaper-c" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Ujian PCIS</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="admin.pcis.progress.aspx" rel="nofollow" target="_self">Status Ujian</a></td>
        </tr>
        <tr>
            <td><a href="admin.pcis.mark.aspx" rel="nofollow" target="_self">Markah Ujian</a></td>
        </tr>
        <tr>
            <td><a href="admin.pcis.jsc.aspx" rel="nofollow" target="_self">Kelayakan Ke JSC</a></td>
        </tr>
    </tbody>
</table>
&nbsp;
<table id="newspaper-d" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Ringkasan Ujian PCIS</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="admin.pcis.dobyear.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Umur</a></td>
        </tr>
<%--        <tr>
            <td><a href="admin.pcis.gender.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Jantina</a></td>
        </tr>--%>
    </tbody>
</table>
&nbsp;

<table id="newspaper-e" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Lain-lain</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="admin.pcis.config.list.aspx" rel="nofollow" target="_self">Sistem Konfigurasi</a></td>
        </tr>
        <tr>
            <td><a href="admin.pcis.exam.year.list.aspx" rel="nofollow" target="_self">Kemaskini Tahun Ujian</a></td>
        </tr>
        <tr>
            <td><a href="admin.pcisadmin.list.aspx" rel="nofollow" target="_self">Senarai Pengguna Sistem</a></td>
        </tr>
    </tbody>
</table>
