<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_access.ascx.vb" Inherits="KPP_MS.admin_access" %>

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
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; text-align:left -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        setTimeout(function () {
            $("#alert_div").fadeTo(5000, 500).slideUp(500, function () {
                $("#alert_div").remove();
            });
        }, 3000);
    }
</script>

<script type="text/javascript">
    $(function () {
        $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');
        $('.tree li.parent_li > span').on('click', function (e) {
            var children = $(this).parent('li.parent_li').find(' > ul > li');
            if (children.is(":visible")) {
                children.hide('fast');
                $(this).attr('title', 'Expand this branch').find(' > i').addClass('fa-plus-square').removeClass('fa-minus-square');
            } else {
                children.show('fast');
                $(this).attr('title', 'Collapse this branch').find(' > i').addClass('fa-minus-square').removeClass('fa-plus-square');
            }
            e.stopPropagation();
        });
    });
</script>

<style>
    .ulliboxcss {
        padding: 5px 15px;
        display: inline-block;
        border: 1px solid #ccc;
        border-radius: 4px;
        background-color: white;
        box-sizing: border-box;
    }

    .messagealert {
        width: 40%;
        position: fixed;
        bottom: 25px;
        right: 0px;
        z-index: 100000;
        padding: 0;
        font-size: 15px;
    }

    .sc3::-webkit-scrollbar {
        height: 10px;
    }

    .sc3::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc3::-webkit-scrollbar-thumb {
        background-color: #929B9E;
        border-radius: 3px;
    }

    .sc4::-webkit-scrollbar {
        width: 10px;
    }

    .sc4::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc4::-webkit-scrollbar-thumb {
        background-color: #929B9E;
    }

    .tree {
        border-radius: 0px;
    }

        .tree li {
            list-style-type: none;
            margin: 0;
            padding: 10px 5px 0 5px;
            position: relative
        }

            /* Horizontal Line Connection Positioning*/
            .tree li::before, .tree li::after {
                content: '';
                left: -20px;
                position: absolute;
                right: auto
            }

            .tree li::before {
                border-left: 1px dashed black;
                height: 100%;
                top: 0;
                width: 1px
            }

            .tree li::after {
                border-top: 1px dashed black;
                height: 20px;
                top: 25px;
                width: 25px
            }

            .tree li span {
                -moz-border-radius: 5px;
                -webkit-border-radius: 5px;
                border: 1px solid #999;
                border-radius: 5px;
                display: inline-block;
                padding: 3px 8px;
                text-decoration: none
            }

            .tree li.parent_li > span {
                cursor: pointer
            }

        .tree > ul > li::before, .tree > ul > li::after {
            border: 0
        }

        .tree li:last-child::before {
            height: 25px
        }

        .tree li.parent_li > span:hover, .tree li.parent_li > span:hover + ul li span {
            background: lightgray;
            border: 1px solid #94a0b4;
            color: black
        }
</style>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Setting &nbsp; / &nbsp; User Configuration &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnViewUserAccess" runat="server" style="display: inline-block; font-size: 0.8vw">View User Access</button>
        <button id="btnRegisterUserAccess" runat="server" style="display: inline-block; font-size: 0.8vw">Register User Access</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="ViewUserAccess" runat="server" class="sc4">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Users </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddl_ViewStaffName" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Position </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddl_ViewStaffPosition" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
        </table>

        <br />

        <div style="padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 46vh;" id="displayTreeViewUserAccess" runat="server" class="sc4 w3-text-black font">

            <div id="collapse_Menu_GeneralManagement" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_GM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; General Management &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_GM_EM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Examination Management &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_GM_EM_VE"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Examination &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_GM_EM_VE_EB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span></li>
                                                <li runat="server" id="MENU_GM_EM_VE_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>

                                        <li runat="server" id="MENU_GM_EM_RE"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Examination &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_GM_EM_RE_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_GM_GM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Grade Management &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_GM_GM_EB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span></li>
                                        <li runat="server" id="MENU_GM_GM_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_GM_AM"><span>&nbsp; <b>Sub Menu 1</b> : &nbsp; Assessment Management &nbsp;</span></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div id="collapse_Menu_Student" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_STD"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Student &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_STD_COURSEM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Course Management &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_STD_COURSEM_VC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Course &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_COURSEM_VC_EB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span></li>
                                                <li runat="server" id="MENU_STD_COURSEM_VC_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_STD_COURSEM_RC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Course &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_COURSEM_RC_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_STD_COURSEM_TC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Transfer Course &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_COURSEM_TC_TB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Transfer Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_STD_CLASSM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Class Managemement &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_STD_CLASSM_VC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Class &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_CLASSM_VC_EB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span></li>
                                                <li runat="server" id="MENU_STD_CLASSM_VC_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_STD_CLASSM_RC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Class &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_CLASSM_RC_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_STD_CLASSM_TC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Transfer Class &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_CLASSM_TC_TB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Transfer Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_STD_SM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Student Managemement &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_STD_SM_TB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Transfer Button &nbsp;</span></li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_STD_SS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Search Student &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_STD_SS_VS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Student &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_SS_VS_VB"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Button Function 1</b> : &nbsp; View Button &nbsp;</span>
                                                    <ul>
                                                        <li runat="server" id="MENU_STD_SS_VS_VB_SI"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 3</b> : &nbsp; Student Information &nbsp;</span>
                                                            <ul>
                                                                <li runat="server" id="MENU_STD_SS_VS_VB_SI_UB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Update Button &nbsp;</span></li>
                                                            </ul>
                                                        </li>
                                                        <li runat="server" id="MENU_STD_SS_VS_VB_FI"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 3</b> : &nbsp; Family Information &nbsp;</span>
                                                            <ul>
                                                                <li runat="server" id="MENU_STD_SS_VS_VB_FI_UB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Update Button &nbsp;</span></li>
                                                            </ul>
                                                        </li>
                                                        <li runat="server" id="MENU_STD_SS_VS_VB_COURSEI"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 3</b> : &nbsp; Course Information &nbsp;</span>
                                                            <ul>
                                                                <li runat="server" id="MENU_STD_SS_VS_VB_COURSEI_EB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Edit Button &nbsp;</span></li>
                                                                <li runat="server" id="MENU_STD_SS_VS_VB_COURSEI_DB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Delete Button &nbsp;</span></li>
                                                            </ul>
                                                        </li>
                                                        <li runat="server" id="MENU_STD_SS_VS_VB_COCURRICULARI"><span>&nbsp; <b>Sub Menu 3</b> : &nbsp; Cocurricular Information &nbsp;</span></li>
                                                        <li runat="server" id="MENU_STD_SS_VS_VB_EI"><span>&nbsp; <b>Sub Menu 3</b> : &nbsp; Examination Information &nbsp;</span></li>
                                                        <li runat="server" id="MENU_STD_SS_VS_VB_HI"><span>&nbsp; <b>Sub Menu 3</b> : &nbsp; Hostel Information &nbsp;</span></li>
                                                        <li runat="server" id="MENU_STD_SS_VS_VB_DI"><span>&nbsp; <b>Sub Menu 3</b> : &nbsp; Discipline Information &nbsp;</span></li>
                                                        <li runat="server" id="MENU_STD_SS_VS_VB_UP"><span>&nbsp; <b>Sub Menu 3</b> : &nbsp; UKM1 - PPCS &nbsp;</span></li>
                                                        <li runat="server" id="MENU_STD_SS_VS_VB_RI"><span>&nbsp; <b>Sub Menu 3</b> : &nbsp; Reference Information &nbsp;</span>
                                                            <ul>
                                                                <li runat="server" id="MENU_STD_SS_VS_VB_RI_VB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; View Button &nbsp;</span></li>
                                                                <li runat="server" id="MENU_STD_SS_VS_VB_RI_DOWNLOADB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Download Button &nbsp;</span></li>
                                                                <li runat="server" id="MENU_STD_SS_VS_VB_RI_DB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Delete Button &nbsp;</span></li>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                </li>
                                                <li runat="server" id="MENU_STD_SS_VS_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_STD_SS_RS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Student &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_SS_RS_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_STD_SS_IS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Import Student &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_SS_IS_IB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Import Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_STD_CCP"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Class & Course Placement &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_STD_CCP_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_STD_ATT"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Attendance &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_STD_ATT_VA"><span>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Attendance &nbsp;</span></li>
                                        <li runat="server" id="MENU_STD_ATT_UA"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Update Attendance &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_ATT_UA_UB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Update Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_STD_VI"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; View Information &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_STD_VI_VCOURSE"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Course &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_VI_VCOURSE_AB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Add A Dropout Student Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_STD_VI_VCLASS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Class &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_VI_VCLASS_UB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Update Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_STD_VIVCOCURRICULUM"><span>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Cocurriculum &nbsp;</span></li>
                                        <li runat="server" id="MENU_STD_VI_VH"><span>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Hostel &nbsp;</span></li>
                                        <li runat="server" id="MENU_STD_VI_VR"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Religion &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STD_VI_VR_UB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Update Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div id="collapse_Menu_Staff" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_STF"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Staff &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_STF_SS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Search Staff &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_STF_SS_VS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Staff &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STF_SS_VS_EB"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span>
                                                    <ul>
                                                        <li runat="server" id="MENU_STF_SS_VS_EB_SI"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 3</b> : &nbsp; Staff Information &nbsp;</span>
                                                            <ul>
                                                                <li runat="server" id="MENU_STF_SS_VS_EB_SI_UB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Update Button &nbsp;</span></li>
                                                            </ul>
                                                        </li>
                                                        <li runat="server" id="MENU_STF_SS_VS_EB_CI"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 3</b> : &nbsp; Course Information &nbsp;</span>
                                                            <ul>
                                                                <li runat="server" id="MENU_STF_SS_VS_EB_CI_DB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Delete Button &nbsp;</span></li>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                </li>
                                                <li runat="server" id="MENU_STF_SS_VS_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_STF_SS_RS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Staff &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STF_SS_RS_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_STF_SS_IS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Import Staff &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STF_SS_IS_IB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Import Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_STF_CP"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Course Placement &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_STF_CP_VSC"><span>&nbsp; <b>Sub Menu 2</b> : &nbsp; view Staff Course &nbsp;</span> </li>
                                        <li runat="server" id="MENU_STF_CP_RSC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Staff Course &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_STF_CP_RSC_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div id="collapse_Menu_Coordinator" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_COO"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Coordinator &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_COO_SC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Search Coordinator &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_COO_SC_VC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Coordiantor &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_COO_SC_VC_EB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span>
                                                </li>
                                                <li runat="server" id="MENU_COO_SC_VC_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_COO_SC_RC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Coordiantor &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_COO_SC_RC_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div id="collapse_Menu_Discipline" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_DISC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Discipline &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_DISC_DM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Discipline Management &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_DISC_DM_VD"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Discipline &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_DISC_DM_VD_EB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span>
                                                </li>
                                                <li runat="server" id="MENU_DISC_DM_VD_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_DISC_DM_RD"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Discipline &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_DISC_DM_RD_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_DISC_CM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Case Management &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_DISC_CM_VC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Case &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_DISC_CM_VC_EB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span>
                                                </li>
                                                <li runat="server" id="MENU_DISC_CM_VC_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_DISC_CM_RC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Case &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_DISC_CM_RC_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div id="collapse_Menu_Counselor" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_COUN"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Counselor &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_COUN_CM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Counselor Management &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_COUN_CM_VM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Management &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_COUN_CM_VM_EB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span>
                                                </li>
                                                <li runat="server" id="MENU_COUN_CM_VM_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_COUN_CM_SDM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Self Development Management &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_COUN_CM_SDM_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_COUN_CM_PDM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Personality Development Management &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_COUN_CM_PDM_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_COUN_SDM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Self Development &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_COUN_SDM_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_COUN_PDM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Personality Development &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_COUN_PDM_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_COUN_POR"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Portfolio &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_COUN_POR_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_COUN_CA"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Counselling Activity &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_COUN_CA_SC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Student Counselor &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_COUN_CA_SC_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_COUN_CA_VCR"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Counselor Report &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_COUN_CA_VCR_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span>
                                                </li>
                                                <li runat="server" id="MENU_COUN_CA_VCR_VB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; View Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_COUN_SM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Scholarship Management &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_COUN_SM_LS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; List Scholarship &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_COUN_SM_LS_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                                <li runat="server" id="MENU_COUN_SM_LS_EB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span></li>
                                                <li runat="server" id="MENU_COUN_SM_LS_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_COUN_SM_SS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Student Scholarship &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_COUN_SM_SS_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span>
                                                </li>
                                                <li runat="server" id="MENU_COUN_SM_SS_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div id="collapse_Menu_Research" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_RES"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Research &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_RES_RSS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Register Student/Supervisor &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_RES_RSS_VSGS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Student Group & Supervisor &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_RES_RSS_VSGS_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_RES_RSS_RSGS"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Student Group & Supervisor &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_RES_RSS_RSGS_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_RES_RPF"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Register Project/Field &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_RES_RPF_VSP"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Student Project &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_RES_RPF_VSP_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_RES_RPF_RSP"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Student Project &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_RES_RPF_RSP_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_RES_RM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Register Mentor &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_RES_RM_VSM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Student Mentor &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_RES_RM_VSM_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_RES_RM_RSN"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Student Mentor &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_RES_RM_RSN_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div id="collapse_Menu_Examination" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_EXAM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Examination &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_EXAM_ER"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Examination Result &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_EXAM_ER_AR"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Academic Result &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_EXAM_ER_AR_UB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Update Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_EXAM_ER_CR"><span>&nbsp; <b>Sub Menu 2</b> : &nbsp; Cocurriculum Result &nbsp;</span></li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_EXAM_ET"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Examination Transcript &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_EXAM_ET_CET"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Current Examination Transcript &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_EXAM_ET_CET_GEN"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Generate GPA & CGPA Button &nbsp;</span></li>
                                                <li runat="server" id="MENU_EXAM_ET_CET_BI"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Print In BI Button &nbsp;</span></li>
                                                <li runat="server" id="MENU_EXAM_ET_CET_BM"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Print In BM Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_EXAM_ET_OT"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Official Transcript &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_EXAM_ET_OT_BI"><span>&nbsp; <b>Print In BI Function 1</b> : &nbsp; Update Button &nbsp;</span></li>
                                                <li runat="server" id="MENU_EXAM_ET_OT_BM"><span>&nbsp; <b>Print In BM Function 1</b> : &nbsp; Update Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_EXAM_IE"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Import Examination &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_EXAM_IE_IER"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Import Examination Result &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_EXAM_IE_IER_BI"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Import Examination Result Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_EXAM_IE_IGC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Import GPA & CGPA &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_EXAM_IE_IGC_BI"><span>&nbsp; <b>Print In BI Function 1</b> : &nbsp; Import GPA & CGPA Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div id="collapse_Menu_Hostel" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_HST"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Hostel &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_HST_HM"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Hostel Management &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_HST_HM_VH"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View Hostel &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_HST_HM_VH_EB"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Button Function 1</b> : &nbsp; Edit Button &nbsp;</span>
                                                    <ul>
                                                        <li runat="server" id="MENU_HST_HM_VH_EB_EHI"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 3</b> : &nbsp; Edit Hostel Information &nbsp;</span>
                                                            <ul>
                                                                <li runat="server" id="MENU_HST_HM_VH_EB_EHI_UB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Update Button &nbsp;</span></li>
                                                            </ul>
                                                        </li>
                                                        <li runat="server" id="MENU_HST_HM_VH_EB_ERI"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 3</b> : &nbsp; Edit Room Information &nbsp;</span>
                                                            <ul>
                                                                <li runat="server" id="MENU_HST_HM_VH_EB_ERI_EB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Edit Button &nbsp;</span></li>
                                                                <li runat="server" id="MENU_HST_HM_VH_EB_ERI_DB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Delete Button &nbsp;</span></li>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                </li>
                                                <li runat="server" id="MENU_HST_HM_VH_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_HST_HM_RH"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register Hostel &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_HST_HM_RH_RB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_HST_SP"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; Student Placement &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_HST_SP_UB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Update Button &nbsp;</span></li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_HST_VHI"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; View Information &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_HST_VHI_DB"><span>&nbsp; <b>Button Function 1</b> : &nbsp; Delete Button &nbsp;</span></li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div id="collapse_Menu_Cocurricular" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_CC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Co-Curricular &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_CC_CCM"><span>&nbsp; <b>Sub Menu 1</b> : &nbsp; Co-Curricular Management &nbsp;</span>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div id="collapse_Menu_Report" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_RPT"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Report &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_RPT_ER"><span>&nbsp; <b>Sub Menu 1</b> : &nbsp; Examination Report &nbsp;</span>
                                </li>
                                <li runat="server" id="MENU_RPT_CLASSER"><span>&nbsp; <b>Sub Menu 1</b> : &nbsp; Class Examination Report &nbsp;</span>
                                </li>
                                <li runat="server" id="MENU_RPT_COURSESER"><span>&nbsp; <b>Sub Menu 1</b> : &nbsp; Courses Examination Report &nbsp;</span>
                                </li>
                                <li runat="server" id="MENU_RPT_SRL"><span>&nbsp; <b>Sub Menu 1</b> : &nbsp; Student Ranking List &nbsp;</span>
                                </li>
                                <li runat="server" id="MENU_RPT_AR"><span>&nbsp; <b>Sub Menu 1</b> : &nbsp; Attendance Report &nbsp;</span>
                                </li>
                                <li runat="server" id="MENU_RPT_FR"><span>&nbsp; <b>Sub Menu 1</b> : &nbsp; Financial Report &nbsp;</span>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>


            <div id="collapse_Menu_Setting" runat="server" class="panel-collapse collapse in">
                <br />
                <div class="tree ">
                    <ul>
                        <li runat="server" id="MENU_SET"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Menu</b> : &nbsp; Setting &nbsp; </span>
                            <ul>
                                <li runat="server" id="MENU_SET_UC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; User Configuration &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_SET_UC_VUA"><span>&nbsp; <b>Sub Menu 2</b> : &nbsp; View User Access &nbsp;</span>
                                        </li>
                                        <li runat="server" id="MENU_SET_UC_RUA"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register User Access &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_SET_UC_RUA_RB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_SET_SC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; System Configuration &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_SET_SC_VSC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; View System Configuration &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_SET_SC_VSC_EB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Edit Button &nbsp;</span></li>
                                                <li runat="server" id="MENU_SET_SC_VSC_DB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Delete Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                        <li runat="server" id="MENU_SET_SC_RSC"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 2</b> : &nbsp; Register System Configuration &nbsp;</span>
                                            <ul>
                                                <li runat="server" id="MENU_SET_SC_RSC_RB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Register Button &nbsp;</span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li runat="server" id="MENU_SET_UA"><span><i class="fa fa-minus-square"></i>&nbsp; <b>Sub Menu 1</b> : &nbsp; User Access &nbsp;</span>
                                    <ul>
                                        <li runat="server" id="MENU_SET_UA_UB"><span>&nbsp; <b>Button Function 2</b> : &nbsp; Update Button &nbsp;</span></li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="RegisterUserAccess" runat="server" class="sc4">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Users </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddlStaffName" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Position </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddlStaffPosition" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Menu </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddlMainMenu" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>

            <tr id="displayStatusSubMenu1" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Sub Menu 1 </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddlSubMenu1" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>

            <tr id="displayStatusSubMenu2" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Sub Menu 2 </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddlSubMenu2" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>

            <tr id="displayButtonFunction1" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Button Function 1 </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <table class="w3-text-black font textboxcss" style="text-align: left; background-color: #FFFFFF">
                        <tr runat="server" id="F1_R1">
                            <td runat="server" id="F1_R1_C1_P1">
                                <asp:CheckBox ID="check_ViewButton_F1" runat="server" AutoPostBack="true" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; View Button </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>

                            <td runat="server" id="F1_R1_C1_P2">
                                <asp:CheckBox ID="check_EditButton_F1" runat="server" AutoPostBack="true" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Edit Button  </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>

                            <td runat="server" id="F1_R1_C1_P3">
                                <asp:CheckBox ID="check_UpdateButton_F1" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Update Button </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>

                            <td runat="server" id="F1_R1_C1_P4">
                                <asp:CheckBox ID="check_DeleteButton_F1" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Delete Button </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>
                        </tr>
                        <tr runat="server" id="F1_R2">
                            <td runat="server" id="F1_R2_C2_P1">
                                <asp:CheckBox ID="check_RegisterButton_F1" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Register Button </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>

                            <td runat="server" id="F1_R2_C2_P2">
                                <asp:CheckBox ID="check_ImportButton_F1" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Import Button </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>

                            <td runat="server" id="F1_R2_C2_P3">
                                <asp:CheckBox ID="check_TransferButton_F1" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Transfer Button </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>

                            <td runat="server" id="F1_R2_C2_P4">
                                <asp:CheckBox ID="check_DropoutButton_F1" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Add A Droupout Student Button </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>
                        </tr>
                        <tr runat="server" id="F1_R3">
                            <td runat="server" id="F1_R3_C3_P1">
                                <asp:CheckBox ID="check_GenerateButton_F1" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Generate GPA & CGPA Button </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>

                            <td runat="server" id="F1_R3_C3_P2">
                                <asp:CheckBox ID="check_PrintBIButton_F1" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Print In BI Button </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>

                            <td runat="server" id="F1_R3_C3_P3">
                                <asp:CheckBox ID="check_PrintBMButton_F1" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Print In BM Button </asp:Label>
                                &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr id="displayStatusSubMenu3" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Sub Menu 3 </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddlSubMenu3" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>

            <tr id="displayButtonFunction2" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Button Function 2 </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <table class="w3-text-black font textboxcss" style="text-align: left; background-color: #FFFFFF">
                        <tr runat="server" id="F2_R1">
                            <td runat="server" id="F2_R1_C1_P1">
                                <asp:CheckBox ID="check_EditButton_F2" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Edit Button </asp:Label>
                            </td>

                            <td runat="server" id="F2_R1_C1_P2">&nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                                <asp:CheckBox ID="check_DeleteButton_F2" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Delete Button </asp:Label>
                            </td>

                            <td runat="server" id="F2_R1_C1_P3">
                                <asp:CheckBox ID="check_UpdateButton_F2" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Update Button </asp:Label>
                            </td>

                            <td runat="server" id="F2_R1_C1_P4">&nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; 
                                <asp:CheckBox ID="check_DownloadButton_F2" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; Download Button </asp:Label>
                            </td>

                            <td runat="server" id="F2_R1_C1_P5">
                                <asp:CheckBox ID="check_ViewButton_F2" runat="server" />
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> &nbsp; View Button </asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <p></p>
            <button id="btnSaveRegisterUserAccess" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Register User Access</button>
        </div>
    </div>

</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
