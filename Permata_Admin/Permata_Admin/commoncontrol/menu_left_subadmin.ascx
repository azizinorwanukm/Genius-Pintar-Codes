<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="menu_left_subadmin.ascx.vb" Inherits="permatapintar.menu_left_subadmin" %>

<table id="newspaper-d" summary="PERMATApintar Menu">
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
            <th scope="col">Ujian UKM1</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="subadmin.ukm1.progress.aspx" rel="nofollow" target="_self">Status Ujian Terkini</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ukm1.state.student.list.aspx" rel="nofollow" target="_self">Status Ujian Negeri</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.schoolprofile.select.aspx" rel="nofollow" target="_self">Senarai Pelajar Sekolah</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.schoolprofile.list.aspx" rel="nofollow" target="_self">Senarai Sekolah Negeri</a></td>
        </tr>
    </tbody>
</table>
&nbsp;
<table id="newspaper-b" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Ringkasan Ujian UKM1</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="subadmin.ukm1.schoolprofile.list.aspx" rel="nofollow" target="_self">Ringkasan Ujian Sekolah</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ukm1.schoolstate.summary.aspx" rel="nofollow" target="_self">Ringkasan Ujian Negeri</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ukm1.examend.summary.aspx" rel="nofollow" target="_self">Ringkasan Ujian Tarikh</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ukm1.dobyear.summary.aspx" rel="nofollow" target="_self">Ringkasan Ujian Umur</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ukm1.studentgender.summary.aspx" rel="nofollow" target="_self">Ringkasan Ujian Jantina</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ukm1.studentrace.summary.aspx" rel="nofollow" target="_self">Ringkasan Ujian Bangsa</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ukm1.schoollokasi.summary.aspx" rel="nofollow" target="_self">Ringkasan Ujian Lokasi</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ukm1.schooltype.summary.aspx" rel="nofollow" target="_self">Ringkasan Ujian Jenis Sekolah</a></td>
        </tr>

    </tbody>
</table>
&nbsp;
<table id="newspaper-c" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Pusat Ujian UKM2</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="subadmin.schoolprofile.pusatujian.select.aspx" rel="nofollow" target="_self">Daftar Pusat Ujian</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.pusatujian.list.aspx" rel="nofollow" target="_self">Senarai Pusat Ujian</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.pusatujian.petugas.create.aspx" rel="nofollow" target="_self">Daftar Petugas</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.pusatujian.list.all.aspx" rel="nofollow" target="_self">Senarai Petugas</a></td>
        </tr>
        <tr>
            <td></td>
        </tr>

    </tbody>
</table>
&nbsp;
<table id="newspaper-d" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Ujian UKM2</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="subadmin.ukm2.schoolprofile.select.aspx" rel="nofollow" target="_self">Daftar Pelajar Baru</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ukm2.ukm2.list.aspx" rel="nofollow" target="_self">Senarai Pelajar</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.pusatujian.kehadiran.aspx" rel="nofollow" target="_self">Kehadiran Pelajar</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ukm2.status.aspx" rel="nofollow" target="_self">Status Ujian UKM2</a></td>
        </tr>
    </tbody>
</table>
&nbsp;
<table id="newspaper-e" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">PPCS</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="subadmin.ppcs.list.kehadiran.aspx" rel="nofollow" target="_self" title="">Kehadiran Pelajar</a></td>
        </tr>
        <tr>
            <td><a href="subadmin.ppcs.student.search.aspx" rel="nofollow" target="_self">Carian Pelajar</a></td>
        </tr>
    </tbody>
</table>
&nbsp;
<table id="newspaper-f" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Alumni PPCS</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="subadmin.ppcs.alumni.list.aspx" rel="nofollow" target="_self">Senarai Alumni</a></td>
        </tr>
    </tbody>
</table>
&nbsp;
