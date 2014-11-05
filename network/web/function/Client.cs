// Type: PBServer.network.web.function.Client
// Assembly: PBServerС, Version=0.7.3.28, Culture=neutral, PublicKeyToken=null
// MVID: 4EA1E2FB-F867-4553-9FBB-41B5E9555B25
// Assembly location: C:\Users\Лана\Desktop\Server files\PBEU1\Debug\PBServerС.exe

using PBServer;
using PBServer.Properties;
using PBServer.src.managers;
using PBServer.src.model.accounts;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace PBServer.network.web.function
{
    public class Client
    {
        private string _footerS = "";
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;

        public Client(TcpClient tcpClient)
        {
            this._tcpClient = tcpClient;
            this._networkStream = tcpClient.GetStream();
            string[] request = this.getRequest();
            this._footerS = Resources._footer;
            this._footerS = this._footerS.Replace("$version$", ((object)Assembly.GetExecutingAssembly().GetName().Version).ToString());
            switch (request[0])
            {
                case "/":
                    this.Send(tcpClient, "", System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "//web//index.html"), "text/html");
                    break;
                case "/index.html":
                    this.Send(tcpClient, "", System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "//web//index.html"), "text/html");
                    break;
                case "/admin":
                    bool flag = false;
                    if (request[3] != null)
                        flag = this.LoginCheck(request[3]);
                    if (flag)
                    {
                        string str = Resources._mainpage.Replace("$serverinfo$", "");
                        this.Send(tcpClient, "", Resources._head + Resources._body + str + this._footerS, "text/html");
                        break;
                    }
                    else
                    {
                        this.Send(tcpClient, "", Resources._head + "<meta http-equiv='refresh' content='0; url=/admin/login'>", "text/html");
                        break;
                    }
                case "/admin/login":
                    this.Send(tcpClient, "", Resources._head + Resources._loginpage + this._footerS, "text/html");
                    break;
                case "/admin/auth":
                    Account account1 = AccountManager.getInstance().get(request[1]);
                    if (AccountManager.getInstance().dbstatus >= 0)
                    {
                        if (account1 != null)
                        {
                            if (account1.password == request[2])
                            {
                                string cookie = Utilits.tokenGenerator();
                                AccountManager.getInstance().setCookie(cookie, request[1]);
                                string str = Resources._Message.Replace("$message$", "<b><center><center>Please Wait..</center>");
                                this.Send(tcpClient, "auth=" + cookie, Resources._head + str + this._footerS, "text/html");
                                break;
                            }
                            else
                            {
                                string str = Resources._Message.Replace("$message$", "<center><b>Fault Autorize.</b></center>");
                                this.Send(tcpClient, "", Resources._head + str + this._footerS, "text/html");
                                break;
                            }
                        }
                        else
                        {
                            string str = Resources._Message.Replace("$message$", "<center><b>Fault Autorize.</b></center>");
                            this.Send(tcpClient, "", Resources._head + str + this._footerS, "text/html");
                            break;
                        }
                    }
                    else
                    {
                        string str = Resources._Message.Replace("$message$", "<center><b>Cannot Read DB.</b></center>");
                        this.Send(tcpClient, "", Resources._head + str + this._footerS, "text/html");
                        break;
                    }
                case "/admin/clients":
                    string clientpage = Resources._clientpage;
                    string str1 = AccountManager.getInstance().getOnlineAccounts().Count.ToString();
                    string str2 = "No Channel.";
                    string str3 = "No room.";
                    string str4 = "";
                    string newValue = "<tr><td>Player</td><td>Rank</td><td>EXP</td><td>GamePoint</td><td>Money</td><td>Channel</td><td>Room</td><td>State</td></tr>";
                    foreach (Account account2 in AccountManager.getInstance().getAccounts())
                    {
                        if (account2.getClient() != null)
                        {
                            int num;
                            if (account2.getClient().getChannelId() != -1)
                            {
                                num = account2.getClient().getChannelId();
                                str2 = num.ToString();
                            }
                            if (account2.getRoom() != null)
                            {
                                num = account2.getRoom().getRoomId();
                                str3 = num.ToString();
                                str4 = ((object)account2.getRoom().getSlotState(account2.getSlot())).ToString();
                            }
                            string str5 = newValue;
                            string[] strArray1 = new string[18];
                            strArray1[0] = str5;
                            strArray1[1] = "<tr><td>";
                            strArray1[2] = account2.getPlayerName();
                            strArray1[3] = "</td><td>";
                            string[] strArray2 = strArray1;
                            int index1 = 4;
                            num = account2.getRank();
                            string str6 = num.ToString();
                            strArray2[index1] = str6;
                            strArray1[5] = "</td><td>";
                            string[] strArray3 = strArray1;
                            int index2 = 6;
                            num = account2.getExp();
                            string str7 = num.ToString();
                            strArray3[index2] = str7;
                            strArray1[7] = "</td><td>";
                            string[] strArray4 = strArray1;
                            int index3 = 8;
                            num = account2.getGP();
                            string str8 = num.ToString();
                            strArray4[index3] = str8;
                            strArray1[9] = "</td><td>";
                            string[] strArray5 = strArray1;
                            int index4 = 10;
                            num = account2.getMoney();
                            string str9 = num.ToString();
                            strArray5[index4] = str9;
                            strArray1[11] = "</td><td>";
                            strArray1[12] = str2;
                            strArray1[13] = "</td><td>";
                            strArray1[14] = str3;
                            strArray1[15] = "</td><td>";
                            strArray1[16] = str4;
                            strArray1[17] = "</td></tr>";
                            newValue = string.Concat(strArray1);
                        }
                    }
                    string str10 = clientpage.Replace("$table$", newValue).Replace("$clients$", "Count Players online: " + str1);
                    this.Send(tcpClient, "", Resources._head + Resources._body + str10 + this._footerS, "text/html");
                    break;
                case "/admin/logout":
                    if (this.LoginCheck(request[3]))
                        this.ClearCookie(request[3]);
                    string str11 = Resources._Message.Replace("$message$", "Please Wait...");
                    this.Send(tcpClient, "", Resources._head + str11 + this._footerS, "text/html");
                    break;
                case "/api/register":
                    bool account3 = AccountManager.getInstance().CreateAccount(request[1], request[2]);
                    this.Send(tcpClient, "", "<api><function>register</function><result>" + account3.ToString() + "</result></api>", "text/xml");
                    break;
                case "/Resources/bootstrap.js":
                    this.Send(tcpClient, "", Resources.bootstrap, "text/html");
                    break;
                case "/Resources/css/bootstrap_style.css":
                    this.Send(tcpClient, "", Resources.bootstrap_style, "text/html");
                    break;
                case "/Resources/jquery-latest.js":
                    this.Send(tcpClient, "", Resources.jquery_latest, "text/html");
                    break;
                case "/Resources/css/login_style.css":
                    this.Send(tcpClient, "", Resources.login_style, "text/html");
                    break;
                case "/Resources/css/head_style.css":
                    this.Send(tcpClient, "", Resources._head, "text/html");
                    break;
            }
        }

        ~Client()
        {
        }

        public TcpClient getClient()
        {
            return this._tcpClient;
        }

        public bool LoginCheck(string cookes)
        {
            return AccountManager.getInstance().isCookie(cookes);
        }

        public void ClearCookie(string value)
        {
            AccountManager.getInstance().deleteCookie(value);
        }

        public string[] getRequest()
        {
            string str = "";
            byte[] numArray = new byte[4096];
            string[] strArray1 = new string[4096];
            int count;
            while ((count = this._networkStream.Read(numArray, 0, numArray.Length)) > 0)
            {
                str = str + Encoding.ASCII.GetString(numArray, 0, count);
                if (str.IndexOf("\r\n\r\n") >= 0 || str.Length > 4096)
                    break;
            }
            Match match = Regex.Match(str, "^\\w+\\s+([^\\s\\?]+)[^\\s]*\\s+HTTP/.*|");
            if (match == Match.Empty)
            {
                strArray1[0] = "";
                return strArray1;
            }
            else
            {
                strArray1[0] = match.Groups[1].Value;
                strArray1[0] = Uri.UnescapeDataString(strArray1[0]);
                string[] accInfo = Utilits.getAccInfo(str);
                string cookieAuth = Utilits.getCookieAuth(str);
                string loginWeb = Utilits.getLoginWeb(str);
                strArray1[1] = accInfo[0];
                strArray1[2] = accInfo[1];
                strArray1[3] = cookieAuth;
                strArray1[4] = loginWeb;
                string[] strArray2 = str.Split(new string[1]
        {
          "players="
        }, StringSplitOptions.None);
                if (strArray2.Length > 1)
                {
                    string[] strArray3 = strArray2[1].Split(new string[1]
          {
            "\r\n"
          }, StringSplitOptions.None)[0].Split(new string[1]
          {
            "%0D%0A"
          }, StringSplitOptions.None);
                    for (int index = 0; index < strArray3.Length; ++index)
                        strArray1[6 + index] = strArray3[index];
                    strArray1[5] = strArray3.Length.ToString();
                }
                if (strArray1[0].IndexOf("..") >= 0)
                    return strArray1;
                CLogger.getInstance().info("GET " + strArray1[0]);
                return strArray1;
            }
        }

        public void Send(TcpClient Client, string Cookes, string data, string type)
        {
            string str = "<html><body><meta charset='utf-8'>" + data + "</body></html>";
            byte[] bytes = Encoding.Default.GetBytes("HTTP/1.1 200 OK\nContent-type: " + (object)type + "\nSet-Cookie: Cookies=INIT\nSet-Cookie:" + Cookes + "\nContent-Length:" + (string)(object)data.Length + "\n\n" + data);
            Client.GetStream().Write(bytes, 0, bytes.Length);
            Client.Close();
        }

        private void SendError(TcpClient Client, int Code)
        {
            if (!this._tcpClient.Client.Connected)
                return;
            string str1 = Code.ToString() + " " + ((object)(HttpStatusCode)Code).ToString();
            string str2 = "<html><body><h1>" + str1 + "</h1></body></html>";
            byte[] bytes = Encoding.ASCII.GetBytes("HTTP/1.1 " + str1 + "\nContent-type: text/html\nContent-Length:" + str2.Length.ToString() + "\n\n" + str2);
            Client.GetStream().Write(bytes, 0, bytes.Length);
            Client.Close();
        }
    }
}
