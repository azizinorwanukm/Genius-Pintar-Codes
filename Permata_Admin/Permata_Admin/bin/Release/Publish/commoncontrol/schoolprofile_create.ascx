<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="schoolprofile_create.ascx.vb"
    Inherits="permatapintar.schoolprofile_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Sekolah Baru
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            Kod Sekolah
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolCode" runat="server" Width="250px" MaxLength="50" Text=""></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Nama Sekolah
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolName" runat="server" Width="250px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Alamat Sekolah
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolAddress" runat="server" Width="250px" MaxLength="500"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Poskod
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolPostcode" runat="server" Width="80px" MaxLength="5"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Bandar
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolCity" runat="server" Width="250px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            PPD
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolPPD" runat="server" Width="250px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Negeri
        </td>
        <td>
            :<asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>*
        </td>
    </tr>
    <tr>
        <td>
            Negara
        </td>
        <td>
            :<select name="selSchoolCountry" id="selSchoolCountry" style="width: 250px;" runat="server">
                <option value="MALAYSIA" selected="SELECTED">MALAYSIA</option>
                <option value="AFGHANISTAN">AFGHANISTAN</option>
                <option value="ALBANIA">ALBANIA</option>
                <option value="ALGERIA">ALGERIA</option>
                <option value="AMERICAN SAMOA">AMERICAN SAMOA</option>
                <option value="ANDORRA">ANDORRA</option>
                <option value="ANGOLA">ANGOLA</option>
                <option value="ANGUILLA">ANGUILLA</option>
                <option value="ANTARCTICA">ANTARCTICA</option>
                <option value="ANTIGUA AND BARBUDA">ANTIGUA AND BARBUDA</option>
                <option value="ARGENTINA">ARGENTINA</option>
                <option value="ARMENIA">ARMENIA</option>
                <option value="ARUBA">ARUBA</option>
                <option value="AUSTRALIA">AUSTRALIA</option>
                <option value="AUSTRIA">AUSTRIA</option>
                <option value="AZERBAIJAN">AZERBAIJAN</option>
                <option value="BAHAMAS">BAHAMAS</option>
                <option value="BAHRAIN">BAHRAIN</option>
                <option value="BANGLADESH">BANGLADESH</option>
                <option value="BARBADOS">BARBADOS</option>
                <option value="BELARUS">BELARUS</option>
                <option value="BELGIUM">BELGIUM</option>
                <option value="BELIZE">BELIZE</option>
                <option value="BENIN">BENIN</option>
                <option value="BERMUDA">BERMUDA</option>
                <option value="BHUTAN">BHUTAN</option>
                <option value="BOLIVIA">BOLIVIA</option>
                <option value="BOSNIA AND HERZEGOVINA">BOSNIA AND HERZEGOVINA</option>
                <option value="BOTSWANA">BOTSWANA</option>
                <option value="BOUVET ISLAND">BOUVET ISLAND</option>
                <option value="BRAZIL">BRAZIL</option>
                <option value="BRITISH INDIAN OCEAN TERRITORY">BRITISH INDIAN OCEAN TERRITORY</option>
                <option value="BRUNEI DARUSSALAM">BRUNEI DARUSSALAM</option>
                <option value="BULGARIA">BULGARIA</option>
                <option value="BURKINA FASO">BURKINA FASO</option>
                <option value="BURUNDI">BURUNDI</option>
                <option value="CAMBODIA">CAMBODIA</option>
                <option value="CAMEROON">CAMEROON</option>
                <option value="CANADA">CANADA</option>
                <option value="CAPE VERDE">CAPE VERDE</option>
                <option value="CAYMAN ISLANDS">CAYMAN ISLANDS</option>
                <option value="CENTRAL AFRICAN REPUBLIC">CENTRAL AFRICAN REPUBLIC</option>
                <option value="CHAD">CHAD</option>
                <option value="CHILE">CHILE</option>
                <option value="CHINA">CHINA</option>
                <option value="CHRISTMAS ISLAND">CHRISTMAS ISLAND</option>
                <option value="COCOS (KEELING) ISLANDS">COCOS (KEELING) ISLANDS</option>
                <option value="COLOMBIA">COLOMBIA</option>
                <option value="COMOROS">COMOROS</option>
                <option value="CONGO">CONGO</option>
                <option value="CONGO, THE DEMOCRATIC REPUBLIC">CONGO, THE DEMOCRATIC REPUBLIC</option>
                <option value="COOK ISLANDS">COOK ISLANDS</option>
                <option value="COSTA RICA">COSTA RICA</option>
                <option value="COTE D'IVOIRE">COTE D'IVOIRE</option>
                <option value="CROATIA">CROATIA</option>
                <option value="CUBA">CUBA</option>
                <option value="CYPRUS">CYPRUS</option>
                <option value="CZECH REPUBLIC">CZECH REPUBLIC</option>
                <option value="DENMARK">DENMARK</option>
                <option value="DJIBOUTI">DJIBOUTI</option>
                <option value="DOMINICA">DOMINICA</option>
                <option value="DOMINICAN REPUBLIC">DOMINICAN REPUBLIC</option>
                <option value="ECUADOR">ECUADOR</option>
                <option value="EGYPT">EGYPT</option>
                <option value="EL SALVADOR">EL SALVADOR</option>
                <option value="EQUATORIAL GUINEA">EQUATORIAL GUINEA</option>
                <option value="ERITREA">ERITREA</option>
                <option value="ESTONIA">ESTONIA</option>
                <option value="ETHIOPIA">ETHIOPIA</option>
                <option value="FALKLAND ISLANDS (MALVINAS)">FALKLAND ISLANDS (MALVINAS)</option>
                <option value="FAROE ISLANDS">FAROE ISLANDS</option>
                <option value="FIJI">FIJI</option>
                <option value="FINLAND">FINLAND</option>
                <option value="FRANCE">FRANCE</option>
                <option value="FRENCH GUIANA">FRENCH GUIANA</option>
                <option value="FRENCH POLYNESIA">FRENCH POLYNESIA</option>
                <option value="FRENCH SOUTHERN TERRITORIES">FRENCH SOUTHERN TERRITORIES</option>
                <option value="GABON">GABON</option>
                <option value="GAMBIA">GAMBIA</option>
                <option value="GEORGIA">GEORGIA</option>
                <option value="GERMANY">GERMANY</option>
                <option value="GHANA">GHANA</option>
                <option value="GIBRALTAR">GIBRALTAR</option>
                <option value="GREECE">GREECE</option>
                <option value="GREENLAND">GREENLAND</option>
                <option value="GRENADA">GRENADA</option>
                <option value="GUADELOUPE">GUADELOUPE</option>
                <option value="GUAM">GUAM</option>
                <option value="GUATEMALA">GUATEMALA</option>
                <option value="GUINEA">GUINEA</option>
                <option value="GUINEA-BISSAU">GUINEA-BISSAU</option>
                <option value="GUYANA">GUYANA</option>
                <option value="HAITI">HAITI</option>
                <option value="HEARD ISLAND AND MCDONALD ISLANDS">HEARD ISLAND AND MCDONALD ISLANDS</option>
                <option value="HOLY SEE (VATICAN CITY STATE)">HOLY SEE (VATICAN CITY STATE)</option>
                <option value="HONDURAS">HONDURAS</option>
                <option value="HONG KONG">HONG KONG</option>
                <option value="HUNGARY">HUNGARY</option>
                <option value="ICELAND">ICELAND</option>
                <option value="INDIA">INDIA</option>
                <option value="INDONESIA">INDONESIA</option>
                <option value="IRAN">IRAN</option>
                <option value="IRAQ">IRAQ</option>
                <option value="IRELAND">IRELAND</option>
                <option value="ISRAEL">ISRAEL</option>
                <option value="ITALY">ITALY</option>
                <option value="JAMAICA">JAMAICA</option>
                <option value="JAPAN">JAPAN</option>
                <option value="JORDAN">JORDAN</option>
                <option value="KAZAKHSTAN">KAZAKHSTAN</option>
                <option value="KENYA">KENYA</option>
                <option value="KIRIBATI">KIRIBATI</option>
                <option value="KOREA, DEMOCRATIC PEOPLE'S REPUBLIC">KOREA, DEMOCRATIC PEOPLE'S REPUBLIC</option>
                <option value="KOREA, REPUBLIC OF">KOREA, REPUBLIC</option>
                <option value="KUWAIT">KUWAIT</option>
                <option value="KYRGYZSTAN">KYRGYZSTAN</option>
                <option value="LAO PEOPLE'S DEMOCRATIC REPUBLIC">LAO PEOPLE'S DEMOCRATIC REPUBLIC</option>
                <option value="LATVIA">LATVIA</option>
                <option value="LEBANON">LEBANON</option>
                <option value="LESOTHO">LESOTHO</option>
                <option value="LIBERIA">LIBERIA</option>
                <option value="LIBYAN ARAB JAMAHIRIYA">LIBYAN ARAB JAMAHIRIYA</option>
                <option value="LIECHTENSTEIN">LIECHTENSTEIN</option>
                <option value="LITHUANIA">LITHUANIA</option>
                <option value="LUXEMBOURG">LUXEMBOURG</option>
                <option value="MACAO">MACAO</option>
                <option value="MACEDONIA">MACEDONIA</option>
                <option value="MADAGASCAR">MADAGASCAR</option>
                <option value="MALAWI">MALAWI</option>
                <option value="MALAYSIA">MALAYSIA</option>
                <option value="MALDIVES">MALDIVES</option>
                <option value="MALI">MALI</option>
                <option value="MALTA">MALTA</option>
                <option value="MARSHALL ISLANDS">MARSHALL ISLANDS</option>
                <option value="MARTINIQUE">MARTINIQUE</option>
                <option value="MAURITANIA">MAURITANIA</option>
                <option value="MAURITIUS">MAURITIUS</option>
                <option value="MAYOTTE">MAYOTTE</option>
                <option value="MEXICO">MEXICO</option>
                <option value="MICRONESIA, FEDERATED STATES OF">MICRONESIA, FEDERATED STATES OF</option>
                <option value="MOLDOVA, REPUBLIC OF">MOLDOVA, REPUBLIC OF</option>
                <option value="MONACO">MONACO</option>
                <option value="MONGOLIA">MONGOLIA</option>
                <option value="MONTSERRAT">MONTSERRAT</option>
                <option value="MOROCCO">MOROCCO</option>
                <option value="MOZAMBIQUE">MOZAMBIQUE</option>
                <option value="MYANMAR">MYANMAR</option>
                <option value="NAMIBIA">NAMIBIA</option>
                <option value="NAURU">NAURU</option>
                <option value="NEPAL">NEPAL</option>
                <option value="NETHERLANDS">NETHERLANDS</option>
                <option value="NETHERLANDS ANTILLES">NETHERLANDS ANTILLES</option>
                <option value="NEW CALEDONIA">NEW CALEDONIA</option>
                <option value="NEW ZEALAND">NEW ZEALAND</option>
                <option value="NICARAGUA">NICARAGUA</option>
                <option value="NIGER">NIGER</option>
                <option value="NIGERIA">NIGERIA</option>
                <option value="NIUE">NIUE</option>
                <option value="NORFOLK ISLAND">NORFOLK ISLAND</option>
                <option value="NORTHERN MARIANA ISLANDS">NORTHERN MARIANA ISLANDS</option>
                <option value="NORWAY">NORWAY</option>
                <option value="OMAN">OMAN</option>
                <option value="PAKISTAN">PAKISTAN</option>
                <option value="PALAU">PALAU</option>
                <option value="PALESTINIAN TERRITORY, OCCUPIED">PALESTINIAN TERRITORY, OCCUPIED</option>
                <option value="PANAMA">PANAMA</option>
                <option value="PAPUA NEW GUINEA">PAPUA NEW GUINEA</option>
                <option value="PARAGUAY">PARAGUAY</option>
                <option value="PERU">PERU</option>
                <option value="PHILIPPINES">PHILIPPINES</option>
                <option value="PITCAIRN">PITCAIRN</option>
                <option value="POLAND">POLAND</option>
                <option value="PORTUGAL">PORTUGAL</option>
                <option value="PUERTO RICO">PUERTO RICO</option>
                <option value="QATAR">QATAR</option>
                <option value="REUNION">REUNION</option>
                <option value="ROMANIA">ROMANIA</option>
                <option value="RUSSIAN FEDERATION">RUSSIAN FEDERATION</option>
                <option value="RWANDA">RWANDA</option>
                <option value="SAINT HELENA">SAINT HELENA</option>
                <option value="SAINT KITTS AND NEVIS">SAINT KITTS AND NEVIS</option>
                <option value="SAINT LUCIA">SAINT LUCIA</option>
                <option value="SAINT PIERRE AND MIQUELON">SAINT PIERRE AND MIQUELON</option>
                <option value="SAINT VINCENT AND THE GRENADINES">SAINT VINCENT AND THE GRENADINES</option>
                <option value="SAMOA">SAMOA</option>
                <option value="SAN MARINO">SAN MARINO</option>
                <option value="SAO TOME AND PRINCIPE">SAO TOME AND PRINCIPE</option>
                <option value="SAUDI ARABIA">SAUDI ARABIA</option>
                <option value="SENEGAL">SENEGAL</option>
                <option value="SERBIA AND MONTENEGRO">SERBIA AND MONTENEGRO</option>
                <option value="SEYCHELLES">SEYCHELLES</option>
                <option value="SIERRA LEONE">SIERRA LEONE</option>
                <option value="SINGAPORE">SINGAPORE</option>
                <option value="SLOVAKIA">SLOVAKIA</option>
                <option value="SLOVENIA">SLOVENIA</option>
                <option value="SOLOMON ISLANDS">SOLOMON ISLANDS</option>
                <option value="SOMALIA">SOMALIA</option>
                <option value="SOUTH AFRICA">SOUTH AFRICA</option>
                <option value="SPAIN">SPAIN</option>
                <option value="SRI LANKA">SRI LANKA</option>
                <option value="SUDAN">SUDAN</option>
                <option value="SURINAME">SURINAME</option>
                <option value="SVALBARD AND JAN MAYEN">SVALBARD AND JAN MAYEN</option>
                <option value="SWAZILAND">SWAZILAND</option>
                <option value="SWEDEN">SWEDEN</option>
                <option value="SWITZERLAND">SWITZERLAND</option>
                <option value="SYRIAN ARAB REPUBLIC">SYRIAN ARAB REPUBLIC</option>
                <option value="TAIWAN, PROVINCE OF CHINA">TAIWAN, PROVINCE OF CHINA</option>
                <option value="TAJIKISTAN">TAJIKISTAN</option>
                <option value="TANZANIA, UNITED REPUBLIC OF">TANZANIA, UNITED REPUBLIC OF</option>
                <option value="THAILAND">THAILAND</option>
                <option value="TIMOR-LESTE">TIMOR-LESTE</option>
                <option value="TOGO">TOGO</option>
                <option value="TOKELAU">TOKELAU</option>
                <option value="TONGA">TONGA</option>
                <option value="TRINIDAD AND TOBAGO">TRINIDAD AND TOBAGO</option>
                <option value="TUNISIA">TUNISIA</option>
                <option value="TURKEY">TURKEY</option>
                <option value="TURKMENISTAN">TURKMENISTAN</option>
                <option value="TURKS AND CAICOS ISLANDS">TURKS AND CAICOS ISLANDS</option>
                <option value="TUVALU">TUVALU</option>
                <option value="UGANDA">UGANDA</option>
                <option value="UKRAINE">UKRAINE</option>
                <option value="UNITED ARAB EMIRATES">UNITED ARAB EMIRATES</option>
                <option value="UNITED KINGDOM">UNITED KINGDOM</option>
                <option value="UNITED STATES">UNITED STATES</option>
                <option value="URUGUAY">URUGUAY</option>
                <option value="UZBEKISTAN">UZBEKISTAN</option>
                <option value="VANUATU">VANUATU</option>
                <option value="VENEZUELA">VENEZUELA</option>
                <option value="VIET NAM">VIET NAM</option>
                <option value="VIRGIN ISLANDS, BRITISH">VIRGIN ISLANDS, BRITISH</option>
                <option value="VIRGIN ISLANDS, U.S.">VIRGIN ISLANDS, U.S.</option>
                <option value="WALLIS AND FUTUNA">WALLIS AND FUTUNA</option>
                <option value="WESTERN SAHARA">WESTERN SAHARA</option>
                <option value="YEMEN">YEMEN</option>
                <option value="ZAMBIA">ZAMBIA</option>
                <option value="ZIMBABWE">ZIMBABWE</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>
            Jenis Sekolah
        </td>
        <td>
            :<asp:DropDownList ID="ddlSchoolType" runat="server" Width="250px">
            </asp:DropDownList>
            *
        </td>
    </tr>
    
    <tr>
        <td>
            Tel.#
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolNoTel" runat="server" Width="250px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Fax#
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolNoFax" runat="server" Width="250px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Email
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolEmail" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Lokasi
        </td>
        <td>
            :<select name="selSchoolLokasi" id="selSchoolLokasi" style="width: 250px;" runat="server">
                <option value="B">BANDAR</option>
                <option value="LB">LUAR BANDAR</option>
            </select>*
        </td>
    </tr>
    
    <tr>
        <td>
            SchoolID
        </td>
        <td>
            :<asp:Label ID="lblSchoolID" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnCreate" runat="server" Text="Daftar Sekolah Baru" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="fbbutton" />
        </td>
    </tr>
</table>
