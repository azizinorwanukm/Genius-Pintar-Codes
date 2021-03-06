<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="menu_left.ascx.vb" Inherits="permatapintar.menu_left" %>


TEMPORARILY NOT USED
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
<table id="newspaper-a" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Maklumat Pelajar</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="admin.studentprofile.search.aspx" rel="nofollow" target="_self">Carian Pelajar</a></td>
        </tr>
        <tr>
            <td><a href="admin.schoolprofile.list.aspx" rel="nofollow" target="_self">Daftar Pelajar Baru</a></td>
        </tr>
        <tr>
            <td><a href="admin.ppcs.alumni.list.aspx" rel="nofollow" target="_self">Senarai Alumni</a></td>
        </tr>
        <tr>
            <td><a href="admin.ppcs.merge.main.list.aspx" rel="nofollow" target="_self">Merge Account</a></td>
        </tr>
        <tr>
            <td><a href="admin.studentprofile.pindah.aspx" rel="nofollow" target="_self">Pindah Sekolah</a></td>
        </tr>
    </tbody>
</table>
&nbsp;
<table id="newspaper-a" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Profil Sekolah</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="admin.schoolprofile.studentprofile.select.aspx" rel="nofollow" target="_self">Carian Sekolah</a></td>
        </tr>
        <tr>
            <td><a href="admin.schoolprofile.create.aspx" rel="nofollow" target="_self">Daftar Sekolah Baru</a></td>
        </tr>
        <tr>
            <td><a href="admin.schoolprofile.schoolPPD.update.aspx" rel="nofollow" target="_self">Kemaskini Nama PPD</a></td>
        </tr>
        <tr>
            <td><a href="admin.schoolprofile.schoolcity.update.aspx" rel="nofollow" target="_self">Kemaskini Nama Bandar</a></td>
        </tr>
        <tr>
            <td><a href="admin.schoolprofile.select.pindah.aspx" rel="nofollow" target="_self">Pindah Sekolah</a></td>
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
            <td><a href="admin.ukm1.progress.aspx" rel="nofollow" target="_self">Senarai Pelajar Terkini</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm1.schoolprofile.list.aspx" rel="nofollow" target="_self">Senarai Sekolah</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm1.state.student.list.aspx" rel="nofollow" target="_self">Senarai Negeri</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm1.schoolprofile.select.aspx" rel="nofollow" target="_self">Pindah Sekolah</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm2.kelayakan.aspx" rel="nofollow" target="_self">Kelayakan UKM2: UMUR</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm2.kelayakan.sekolah.aspx" rel="nofollow" target="_self">Kelayakan UKM2: SEKOLAH</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm2.kelayakan.totalscore.aspx" rel="nofollow" target="_self">Kelayakan UKM2: MARKAH</a></td>
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
            <td><a href="admin.ukm1.schoolprofile.list.mas.aspx" rel="nofollow" target="_self">Ringkasan Ujian Sekolah</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm1.schoolstate.summary.aspx" rel="nofollow" target="_self">Ringkasan Ujian Negeri</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm1.schoolppd.summary.aspx" rel="nofollow" target="_self">Ringkasan Ujian PPD</a></td>
        </tr>
        <tr>
            <td><a href="kpm.ukm1.examend.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Tarikh</a></td>
        </tr>
        <tr>
            <td><a href="kpm.ukm1.dobyear.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Umur</a></td>
        </tr>
        <tr>
            <td><a href="kpm.ukm1.studentgender.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Jantina</a></td>
        </tr>
        <tr>
            <td><a href="kpm.ukm1.studentrace.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Bangsa</a></td>
        </tr>
        <tr>
            <td><a href="kpm.ukm1.schoollokasi.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Lokasi</a></td>
        </tr>
        <tr>
            <td><a href="kpm.ukm1.schooltype.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Jenis Sekolah</a></td>
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
            <td><a href="admin.schoolprofile.pusatujian.select.aspx" rel="nofollow" target="_self">Daftar Pusat Ujian</a></td>
        </tr>
        <tr>
            <td><a href="admin.pusatujian.list.aspx" rel="nofollow" target="_self">Senarai Pusat Ujian</a></td>
        </tr>
        <tr>
            <td><a href="admin.pusatujian.schedule.aspx" rel="nofollow" target="_self">Jadual Pusat Ujian</a></td>
        </tr>
        <tr>
            <td><a href="admin.pusatujian.petugas.create.aspx" rel="nofollow" target="_self">Daftar Petugas</a></td>
        </tr>
        <tr>
            <td><a href="admin.pusatujian.list.all.aspx" rel="nofollow" target="_self">Senarai Petugas</a></td>
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
            <td><a href="admin.ukm2.ukm2.list.aspx" rel="nofollow" target="_self">Senarai Pelajar</a></td>
        </tr>
        <tr>
            <td><a href="admin.pusatujian.kehadiran.aspx" rel="nofollow" target="_self">Kehadiran Pelajar</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm2.status.aspx" rel="nofollow" target="_self">Status Ujian UKM2</a></td>
        </tr>
        <tr>
            <td><a href="admin.ppcs.kelayakan.aspx" rel="nofollow" target="_self">Kelayakan ke PPCS</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm2.analysis.aspx" rel="nofollow" target="_self">Analisa UKM2</a></td>
        </tr>
    </tbody>
</table>
&nbsp;
<table id="newspaper-d" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Ringkasan Ujian UKM2</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="ukm2.schoolprofile.list.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Sekolah</a></td>
        </tr>
        <tr>
            <td><a href="ukm2.schoolstate.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Negeri</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm2.schoolppd.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian PPD</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm2.dobyear.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Umur</a></td>
        </tr>
        <tr>
            <td><a href="ukm2.studentgender.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Jantina</a></td>
        </tr>
        <tr>
            <td><a href="ukm2.studentrace.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Bangsa</a></td>
        </tr>
        <tr>
            <td><a href="ukm2.schoollokasi.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Lokasi</a></td>
        </tr>
        <tr>
            <td><a href="ukm2.schooltype.summary.aspx"
                rel="nofollow" target="_self">Ringkasan Ujian Jenis Sekolah</a></td>
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
            <td><a href="admin.ppcs.user.assign.aspx" rel="nofollow" target="_self" title="">Pilih Petugas</a></td>
        </tr>
        <tr>
            <td><a href="admin.ppcs.user.list.aspx" rel="nofollow" target="_self" title="">Senarai Petugas</a></td>
        </tr>
        <tr>
            <td><a href="admin.ppcs.student.search.aspx" rel="nofollow" target="_self">Carian Pelajar</a></td>
        </tr>
        <tr>
            <td><a href="admin.ppcs.offer.status.aspx" rel="nofollow" target="_self">Status Tawaran</a></td>
        </tr>
        <tr>
            <td><a href="admin.ppcs.list.kehadiran.aspx" rel="nofollow" target="_self" title="">Kehadiran Pelajar</a></td>
        </tr>
        <tr>
            <td><a href="admin.ukm3.kelayakan.aspx" rel="nofollow" target="_self" title="">Kelayakan ke UKM3</a></td>
        </tr>
         <tr>
            <td><a href="admin.ppcs.report.aspx" rel="nofollow" target="_self" title="">Laporan PPCS</a></td>
        </tr>

    </tbody>
</table>
&nbsp;
<table id="newspaper-e" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Ringkasan PPCS</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="ppcs.schoolprofile.list.aspx" rel="nofollow" target="_self">Ringkasan Sekolah</a></td>
        </tr>
        <tr>
            <td><a href="ppcs.schoolstate.summary.aspx" rel="nofollow" target="_self">Ringkasan Negeri</a></td>
        </tr>
         <tr>
            <td><a href="admin.ppcs.schoolppd.summary.aspx" rel="nofollow" target="_self">Ringkasan PPD</a></td>
        </tr>
        <tr>
            <td><a href="admin.ppcs.dobyear.summary.aspx" rel="nofollow" target="_self">Ringkasan Umur</a></td>
        </tr>
        <tr>
            <td><a href="ppcs.studentgender.summary.aspx" rel="nofollow" target="_self">Ringkasan Jantina</a></td>
        </tr>
        <tr>
            <td><a href="ppcs.studentrace.summary.aspx" rel="nofollow" target="_self">Ringkasan Bangsa</a></td>
        </tr>
        <tr>
            <td><a href="ppcs.schoollokasi.summary.aspx" rel="nofollow" target="_self">Ringkasan Lokasi</a></td>
        </tr>
        <tr>
            <td><a href="ppcs.schooltype.summary.aspx" rel="nofollow" target="_self">Ringkasan Jenis Sekolah</a></td>
        </tr>

    </tbody>
</table>
&nbsp;
<%--<table id="newspaper-f" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Alumni PPCS</th>
        </tr>
    </thead>
    <tbody>

        <tr>
            <td><a href="admin.ppcs.alumni.list.update.aspx" rel="nofollow" target="_self">Kelayakan PPCS</a></td>
        </tr>

    </tbody>
</table>
&nbsp;--%>

<table id="newspaper-g" summary="PERMATApintar Menu">
    <thead>
        <tr>
            <th scope="col">Lain-lain</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="https://drive.google.com/open?id=1XFGA-xaQ-bhje_ymzEQyW6iSI1NWtJ3QzLE_HYpIJ7o" rel="nofollow" target="_blank">Change Request</a></td>
        </tr>
        <tr>
            <td><a href="admin.master.config.updated.aspx?configID=1" rel="nofollow" target="_self">Sistem Konfigurasi</a></td>
        </tr>
        <tr>
            <td><a href="admin.displaystatus.updated.aspx" rel="nofollow" target="_self">DisplayStatus</a></td>
        </tr>
        <tr>
            <td><a href="admin.master.state.updated.aspx" rel="nofollow" target="_self">Pengesahan Pusat Ujian</a></td>
        </tr>
        <tr>
            <td><a href="admin.userprofile.list.aspx" rel="nofollow" target="_self">Senarai Pengguna Sistem</a></td>
        </tr>
        <tr>
            <td><a href="admin.security_login_trail.list.aspx" rel="nofollow" target="_self">Aktiviti Sistem</a></td>
        </tr>
        <tr>
            <td><a href="admin.TransactionLog.list.aspx" rel="nofollow" target="_self">Transaction Log</a></td>
        </tr>
    </tbody>
</table>
