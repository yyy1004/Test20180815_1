<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统登录</title>
    <link rel='icon' href='Images/zsh.ico' type='image/x-ico' />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <table width="620" border="0" align="center" cellpadding="0" cellspacing="0">
                <tbody>
                    <tr>
                        <td width="620">
                            <img alt="login_p_img02" height="11" src="/Images/login_p_img02.gif" width="650"/></td>
                    </tr>
                    <tr>
                        <td align="center" background="/images/login_p_img03.gif">
                            <br />
                            <table width="570" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="570" border="0">
                                            <tbody>
                                                <tr>
                                                    <td width="504" height="120" align="center" valign="middle" style="width: 504px">
                                                        <img alt="logo0012" src="Images/logo%20.png" 
                                                            style="height: 102px; width: 546px"/></td></tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <img  alt="a_te01" src="/images/a_te01.gif" width="570" height="3"/></td>
                                </tr>
                                <tr>
                                    <td align="center" background="/images/a_te02.gif">
                                        <table width="520" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="123" height="120">
                                                    <img alt="login_p_img05" height="95" src="/Images/login_p_img05.gif" width="123" border="0"/></td>
                                                <td align="center">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                            <tr>
                                                                <td width="220" height="25" valign="top">
                                                                    用户名：
                                                                    <input class="nemo01" value="" tabindex="1" maxlength="22" size="17" name="user" id="txtUsername"
                                                                        runat="server" style="width: 136px; height: 19px"/>
                                                                </td>
                                                                <td width="80" rowspan="2" align="right" valign="middle">
                                                                    <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="/Images/login_p_img11.gif"
                                                                        OnClick="btnLogin_Click"></asp:ImageButton><a href="#"></a></td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="bottom" height="25">
                                                                    密　码：
                                                                    <input name="user" type="password" class="nemo01" tabindex="1" size="17" maxlength="22"
                                                                        id="txtPwd" runat="server" style="width: 136px; height: 19px"/>
                                                                </td>
                                                            </tr>
                                                    </table>
                                                    <asp:Label ID="lblMsg" runat="server" BackColor="Transparent"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#d5d5d5">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="70" align="center">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img alt="login_p_img04" height="11" src="images/login_p_img04.gif" width="650"/></td>
                    </tr>
                </tbody>
            </table>
            <br />
        </div>
    </form>
</body>
</html>
