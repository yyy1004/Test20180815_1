<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>巨鲸齿轮箱在线监测系统</title>
    <link rel="shortcut icon" href="/Images/slo.ico" type="image/x-icon" />
    <link href="CSS/Login.css" rel="stylesheet" />
    <style type="text/css">
        #mima::after {
            display: inline-block;
            width: 100%;
            content: '';
            height: 0;
        }

        html, body {
            width: 100%;
            height: 100%;
            overflow: hidden;
        }

        body {
            /*background: #54afea;*/
        }

        #particles {
            width: 100%;
            height: 100%;
            overflow: hidden;
        }

        #login_table {
            padding: 5px;
            box-shadow: 4px 5px 3px #2b8bc8;
            background-color: rgba(255, 255, 255,0.8);
        }

        @media only screen and (max-width:1400px) {
            #Image2 {
                width: 1000px;
            }
        }

        @media only screen and (max-width: 1153px) {
            .auto-style2 {
                width: 98%;
            }

            #login_table {
                background: white;
                z-index: 10;
                left: 24%;
                top: 200px;
                position: absolute;
                box-shadow: 7px 7px 5px 0 rgba(0, 0, 0,0.5);
            }
        }

        @media only screen and (max-width: 568px) {
            #login_table {
                left: 1%;
                top: 20%;
                position: absolute;
            }
        }

        .auto-style1 {
            height: 50px;
            width: 100px;
        }

        #form1 {
            width: 100%;
            height: 100%;
            background-image: url(/images/login-background.png);
            background-position-y: -80px;
            background-position-x: 80px;
            background-size: 85%;
        }

        .gc-background {
            width: 100%;
            height: 100%;
            background-image: url(/images/login-gc.png);
            background-position-x: left;
            background-position-y: bottom;
            background-repeat: no-repeat;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="gc-background">
            <table width="100%">
                <%--巨鲸logo--%>
                <tr>
                    <td class="auto-style2" style="padding-bottom: 15px;">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/JJ-logo.png" Style="" ImageAlign="Left" />
                    </td>
                </tr>
                <tr>
                    <%--登录框 左边边框--%>
                    <td class="auto-style2" style="width: 57%;">
                        <asp:Image ID="Image2" runat="server" Width="100%" Style="margin-left: 20px; margin-top: 20px" />
                    </td>
                    <%--登录框--%>
                    <td id="rt" style="">
                        <table width="30%" id="login_table" align="center" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td class="table-con" align="center" style="width: 520px; height: 276px;">
                                        <table width="96%" height="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr class="con-top">
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" style="height: 65px">
                                                        <tbody>
                                                            <tr style="width: 520px; height: 94px/*; background-color: red*/;">
                                                                <td style="border-right: 1px solid #b2cfe1; /*background-color: green*/" class="auto-style3">
                                                                    <img alt="" style="margin-right: 0px; margin-left: 30px; margin-top: 10px; /*background-color: white*/" class="auto-style1" src="../Images/littlelogo.png" /></td>
                                                                <td width="100%" align="center" valign="middle" class="auto-style5" style="display: inline-block; /*background-color: gray; */height: 94px">

                                                                    <a href="#" style="text-decoration: none; color: #0070b8; font-size: 30px; padding-left: 2px/*; background-color: brown*/; display: block; margin-top: 30px;">巨鲸齿轮箱在线监测系统</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr class="con-down">
                                                <td align="center" style="border-top: 1px solid #b2cfe1;">
                                                    <table width="520px" border="0" cellspacing="0" cellpadding="0" style="height: 180px;">
                                                        <tr>
                                                            <td class="down-1" align="center" style="padding-top: 0px; height: 100%;">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" border="0" style="display: block; height: 80px; width: 248px; margin-left: 0px">
                                                                                <%--用户名--%>
                                                                                <tr>
                                                                                    <td width="220" height="25" valign="top" style="padding-bottom: 5px; color: #318c6e;">
                                                                                        <span style="display: inline-block; width: 3em;">用户名</span>:
                                                                                    <input class="nemo01" value="" tabindex="1" maxlength="22" size="17" name="user" id="txtUsername"
                                                                                        runat="server" style="width: 139px; height: 26px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <%--密码--%>
                                                                                <tr>
                                                                                    <td valign="bottom" height="25" style="color: #318c6e; padding-bottom: 5px;">
                                                                                        <span id="mima" style="display: inline-block; width: 3em; text-align: justify; line-height: 12px;">密码</span>:
                                                                                    <input name="user" type="password" class="nemo01" tabindex="1" size="17" maxlength="22"
                                                                                        id="txtPwd" runat="server" style="width: 139px; height: 26px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <%--密码错误提示--%>
                                                                                        <asp:Label ID="lblMsg" runat="server" BackColor="Transparent" Style="color: red; font-size: 14px;"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>

                                                                        </td>
                                                                        <td>
                                                                            <%--登录按钮--%>
                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="height: 48px;margin-top: -8px;">
                                                                                <tr>
                                                                                    <td height="50px">
                                                                                        <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="/Images/login_p_img11.gif" OnClick="btnLogin_Click" Height="55px" Width="88px"></asp:ImageButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <%--信息提示--%>
                                                                <div class="foot" style="font-size: 12px; color: #576b62; width: 100%; height: 40px; margin-top: 8px;">
                                                                    <div class="tip" style="width: 193px; padding-bottom: 2px;">为更好体验请下载<a class="explorer" href="/File/Chrome_Setup.exe">谷歌浏览器</a></div>
                                                                    <div class="owner" style="width: 215px; margin: auto; padding-top: 8px;">巨鲸传动©版权所有 版本号:V0.9.1.1</div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </form>
</body>
</html>
