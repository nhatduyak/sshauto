using ManagedWinapi.Windows;
using MaxMind.GeoIP2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using NodaTime;
using NodaTime.TimeZones;
using System.Drawing;
using System.Net;
using xNet.Net;
using System.IO.Compression;


namespace SSHCHANGERFREE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TimeZoneInfo tzi; TimeSpan offset;
        List<string> SSH = new List<string>();
        string sshIP = "", sshUsername = "", sshPassword = "", IP = "", User = "", Pass = "", ssh = "", html = "", _ssh = "";
        int totalSsh = 0, sshlive = 0, sshdie = 0, port, TimeoutSeconds = 9, loop = 0;
        string programfilesx86dir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        string importsshbyfilepath = "SSH.txt";
        bool flagstop = false, hasimportsshlink = false, threadrunning = false, thread247running = false, threadscheduledrunning = false;
        static readonly object lockObj1 = new object();
        Thread threadconnect, thread247, threadscheduled;
        Form2 myform2 = new Form2();
        Match match;
        Random r = new Random();
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                KillALL();
                if (IsRunning("Proxifier"))
                {
                    ptbProxifier.Image = Properties.Resources.ProxifierOn;
                }
                else
                {
                    ptbProxifier.Image = Properties.Resources.ProxifierOff;
                }
                toolTip1.SetToolTip(ptbProxifier, "On/Off Proxifier");
                toolTip1.SetToolTip(ptbChange, "Left click -> Change SSH\nRight click -> Disconnect");
                if (File.Exists("SSHCHANGERFREE.exe.config"))
                {
                    myform2.linkssh = Properties.Settings.Default.linkssh;
                    myform2.location = Properties.Settings.Default.location;
                    myform2.loopssh = Properties.Settings.Default.loopssh;
                    myform2.autosave = Properties.Settings.Default.autosave;
                    myform2.changeby247 = Properties.Settings.Default.changeby247;
                    myform2.changebymanual = Properties.Settings.Default.changebymanual;
                    myform2.changebyscheduled = Properties.Settings.Default.changebyscheduled;
                    myform2.importsshfromfile = Properties.Settings.Default.importsshfromfile;
                    myform2.randomssh = Properties.Settings.Default.randomssh;
                    myform2.scheduleddelay = Properties.Settings.Default.scheduleddelay;
                    cbChangetimezone.Checked = Properties.Settings.Default.timezone;
                    nmrPort.Value = Properties.Settings.Default.port;
                    nmrTimeout.Value = Properties.Settings.Default.timeout;
                }
                //importsshbyfilepath = Path.Combine(Application.StartupPath, "SSH.txt");
                //LoadSSH(importsshbyfilepath);
                DownloadFile(CTLConfig._pathfile, "SSH_DOWNLOAD.txt");
                LoadSSH("SSH_DOWNLOAD.txt");
                buttonChangeClick();
            }
            catch (Exception ex)
            { CTLError.WriteError("Loi Form1_load ", ex.Message); }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.linkssh = myform2.linkssh;
            Properties.Settings.Default.location = myform2.location;
            Properties.Settings.Default.loopssh = myform2.loopssh;
            Properties.Settings.Default.autosave = myform2.autosave;
            Properties.Settings.Default.changeby247 = myform2.changeby247;
            Properties.Settings.Default.changebymanual = myform2.changebymanual;
            Properties.Settings.Default.changebyscheduled = myform2.changebyscheduled;
            Properties.Settings.Default.importsshfromfile = myform2.importsshfromfile;
            Properties.Settings.Default.randomssh = myform2.randomssh;
            Properties.Settings.Default.scheduleddelay = myform2.scheduleddelay;
            Properties.Settings.Default.timezone = cbChangetimezone.Checked;
            Properties.Settings.Default.timeout = nmrTimeout.Value;
            Properties.Settings.Default.port = nmrPort.Value;
            Properties.Settings.Default.Save();

            KillALL();            
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        public static bool IsRunning(string _process)
        {
            Process[] pname = Process.GetProcessesByName(_process);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }

        private void GetSSHinfo(string _ip)
        {
            using (var reader = new DatabaseReader("GeoLite2-City.mmdb"))
            {
                var city = reader.City(_ip);
                if (city.Location.TimeZone != null)
                {
                    var _windowstimezone = IanaToWindows(city.Location.TimeZone);
                    tzi = TimeZoneInfo.FindSystemTimeZoneById(_windowstimezone);
                    offset = tzi.GetUtcOffset(DateTime.Now);
                    if (cbChangetimezone.Checked && _windowstimezone != null)
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = "TZUTIL.exe";
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = "/s \"" + _windowstimezone + "\"";
                        try
                        {
                            using (Process exeProcess = Process.Start(startInfo))
                            {
                                exeProcess.WaitForExit();
                            }
                        }
                        catch
                        {
                            // Log error.
                        }
                        this.Invoke((MethodInvoker)delegate () { lblTimezone.Text = offset.Hours + ":" + offset.Minutes; });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate () { lblTimezone.Text = "Unknow"; });
                    }
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () { lblTimezone.Text = "Unknow"; });
                }
                this.Invoke((MethodInvoker)delegate () { txtSSHinfo.Text = city.Country.Name + " | " + city.MostSpecificSubdivision.Name + " (" + city.MostSpecificSubdivision.IsoCode + ") | " + city.City.Name + " | " + city.Postal.Code; });

                //Console.WriteLine(city.Country.IsoCode); // 'US'


            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            myform2.ShowDialog();           
        }

        private void ptbChange_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (ptbConnecting.Visible == false && threadrunning == false)
                {
                    flagstop = false;
                    threadconnect = new Thread(() => ChangeSSH());
                    threadconnect.IsBackground = true;
                    threadconnect.Start();
                    Application.DoEvents();
                }
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                flagstop = true;
                threadconnect.Abort();
                threadrunning = false;
                KillALL();
                Showstatus("Disconnected");
                if (thread247running) { thread247.Abort(); }
                if (threadscheduledrunning) { threadscheduled.Abort(); }
            }
        }
        public void buttonChangeClick()
        {
            if (ptbConnecting.Visible == false && threadrunning == false)
                {
                    flagstop = false;
                    threadconnect = new Thread(() => ChangeSSH());
                    threadconnect.IsBackground = true;
                    threadconnect.Start();
                    Application.DoEvents();
                }
          
        }

        private void btnWhoer_Click(object sender, EventArgs e)
        {
            if (btnWhoer.Text == ">>")
            {
                //380*289
                this.Size = new System.Drawing.Size(622, 289);
                btnWhoer.Text = "<<";
            }
            else
            {
                this.Size = new System.Drawing.Size(380, 289);
                btnWhoer.Text = ">>";
            }
        }

        private void ptbProxifier_Click(object sender, EventArgs e)
        {
            if (IsRunning("Proxifier"))
            {
                try
                {
                    foreach (Process p in Process.GetProcessesByName("Proxifier"))
                    {
                        p.Kill();
                        p.Dispose();
                    }
                }
                catch
                {
                    //
                }
                ptbProxifier.Image = Properties.Resources.ProxifierOff;
            }
            else
            {
                Process.Start(programfilesx86dir + @"\Proxifier\Proxifier.exe");
                ptbProxifier.Image = Properties.Resources.ProxifierOn;
            }
        }

        private void LoadSSH(string _sshpath = "")
        {
            if (File.Exists(_sshpath))
            {
                SSH.Clear();
                sshdie = 0;
                sshlive = 0;
                foreach (var line in File.ReadAllLines(_sshpath))
                {
                    try
                    {
                        if (line.Trim().Length != 0)
                        {
                            sshIP = Regex.Split(line, "\\|")[0].Trim();
                            sshUsername = Regex.Split(line, "\\|")[1].Trim();
                            sshPassword = Regex.Split(line, "\\|")[2].Trim();
                            if (Regex.IsMatch(sshIP.Trim(), "^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$") == true && sshIP != "" && sshUsername != "" && sshPassword != "")
                            {
                                SSH.Add(sshIP + "|" + sshUsername + "|" + sshPassword);
                            }
                        }
                    }
                    catch
                    {
                        //
                    }
                }
                totalSsh = SSH.Count;
                if(totalSsh == 0)
                {
                    MessageBox.Show("SSH file no data!\nPlease check ssh file or ssh link.", "ERROR SSH", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (myform2.loopssh)
                {
                    this.Invoke((MethodInvoker)delegate () { lblTotalssh.Text = totalSsh.ToString() + " Loop: " + loop; });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () { lblTotalssh.Text = totalSsh.ToString(); });
                }                
                this.Invoke((MethodInvoker)delegate () { lblLive.Text = "0"; });
                this.Invoke((MethodInvoker)delegate () { lblDie.Text = "0"; });
            }
            else if (myform2.importsshfromfile)
            {
                MessageBox.Show("Please check file path", "File not fund", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Please check Link SSH", "Setting error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string GetSsh()
        {
            lock (lockObj1)
            {
                if (SSH.Count == 0)
                {
                    return "";
                }
                if (myform2.randomssh)
                {
                    _ssh = SSH[r.Next(0, SSH.Count)];
                }
                else
                {
                    _ssh = SSH[0];
                }
                SSH.Remove(_ssh);
                this.Invoke((MethodInvoker)delegate ()
                {
                    lblSshinfo.Text = _ssh;
                });
                return _ssh;
            }
        }

        private void Showstatus(string _stt)
        {
            if (_stt == "Connecting")
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    ptbConnected.Visible = false;
                    ptbDisconnected.Visible = false;
                    ptbConnecting.Visible = true;
                });
            }
            else if (_stt == "Connected")
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    ptbDisconnected.Visible = false;
                    ptbConnecting.Visible = false;
                    ptbConnected.Visible = true;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    ptbConnected.Visible = false;
                    ptbConnecting.Visible = false;
                    ptbDisconnected.Visible = true;
                });
            }
        }

        private void KillALL()
        {
            try
            {
                foreach (Process p in Process.GetProcessesByName("BvSsh"))
                {
                    p.Kill();
                    p.Dispose();
                }
            }
            catch
            {
                //
            }
        }

        private void DownloadFile(string link, string filepath)
        {
            if (File.Exists(filepath)) { File.Delete(filepath); }
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(link, filepath);
            }
        }

        private string Whoer(int port)
        {
            using (var request = new HttpRequest())
            {

                this.Invoke((MethodInvoker)delegate ()
                {
                    lblwhoerstt.Visible = false;
                    ptbWhoerchecking.Visible = true;
                    ptbProxy.Visible = false;
                    ptbTor.Visible = false;
                    ptbAno.Visible = false;
                    ptbBL.Visible = false;
                    lblisp.Visible = false;
                    lblproxy.Visible = false;
                    lbltor.Visible = false;
                    lblano.Visible = false;
                    lblbl.Visible = false;
                });
                request.Proxy = new Socks5ProxyClient("127.0.0.1", port);
                //request.Cookies = new CookieDictionary();
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/6969.0.2743.116 Safari/537.36";
                try
                {
                    html = request.Get("http://whoer.net").ToString();
                }
                catch
                {
                    this.Invoke((MethodInvoker)delegate () { ptbWhoerchecking.Visible = false; lblwhoerstt.Text = "ERROR"; lblwhoerstt.Visible = true; });
                    return "ERRORSV";
                }
                match = Regex.Match(html, @"ico-holder isp[^\n]+[^>]+>([^<]+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    this.Invoke((MethodInvoker)delegate () { lblisp.Text = match.Groups[1].Value; lblisp.Visible = true; });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () { lblisp.Text = "ERROR"; lblisp.Visible = true; });
                }
                match = Regex.Match(html, "proxy : \"([^\"]+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (match.Groups[1].Value.ToString() != "0")
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            ptbProxy.Image = Properties.Resources.whoerbad;
                            lblproxy.Text = match.Groups[1].Value;
                            ptbProxy.Visible = true;
                            lblproxy.Visible = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            ptbProxy.Image = Properties.Resources.whoerok;
                            lblproxy.Text = "NO";
                            ptbProxy.Visible = true;
                            lblproxy.Visible = true;
                        });
                    }
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        ptbProxy.Image = Properties.Resources.whoerbad;
                        lblproxy.Text = "ERROR";
                        ptbProxy.Visible = true;
                        lblproxy.Visible = true;
                    });
                }

                match = Regex.Match(html, @"tor : ([^,]+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (match.Groups[1].Value.ToString() != "0")
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            ptbTor.Image = Properties.Resources.whoerbad;
                            lbltor.Text = match.Groups[1].Value;
                            ptbTor.Visible = true;
                            lbltor.Visible = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            ptbTor.Image = Properties.Resources.whoerok;
                            lbltor.Text = "NO";
                            ptbTor.Visible = true;
                            lbltor.Visible = true;
                        });
                    }
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        ptbTor.Image = Properties.Resources.whoerbad;
                        lbltor.Text = "ERROR";
                        ptbTor.Visible = true;
                        lbltor.Visible = true;
                    });
                }

                match = Regex.Match(html, @"anonymizer : ([^,]+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (match.Groups[1].Value.ToString() != "0")
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            ptbAno.Image = Properties.Resources.whoerbad;
                            lblano.Text = match.Groups[1].Value;
                            ptbAno.Visible = true;
                            lblano.Visible = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            ptbAno.Image = Properties.Resources.whoerok;
                            lblano.Text = "NO";
                            ptbAno.Visible = true;
                            lblano.Visible = true;
                        });
                    }
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        ptbAno.Image = Properties.Resources.whoerbad;
                        lblano.Text = "ERROR";
                        ptbAno.Visible = true;
                        lblano.Visible = true;
                    });
                }

                match = Regex.Match(html, @"dsbl : ([^\n]+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (match.Groups[1].Value.ToString() != "0")
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            ptbBL.Image = Properties.Resources.whoerbad;
                            lblbl.Text = match.Groups[1].Value;
                            ptbBL.Visible = true;
                            lblbl.Visible = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            ptbBL.Image = Properties.Resources.whoerok;
                            lblbl.Text = "NO";
                            ptbBL.Visible = true;
                            lblbl.Visible = true;
                        });
                    }
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        ptbBL.Image = Properties.Resources.whoerbad;
                        lblbl.Text = "ERROR";
                        ptbBL.Visible = true;
                        lblbl.Visible = true;
                    });
                }

                if (myform2.whoercheckproxy && lblproxy.Text != "NO")
                {
                    this.Invoke((MethodInvoker)delegate () { ptbWhoerchecking.Visible = false; lblwhoerstt.Text = "Proxy detect"; lblwhoerstt.Visible = true; });
                    return "BAD";
                }
                if (myform2.whoercheckbl && lblbl.Text != "NO")
                {
                    this.Invoke((MethodInvoker)delegate () { ptbWhoerchecking.Visible = false; lblwhoerstt.Text = "Blacklist detect"; lblwhoerstt.Visible = true; });
                    return "BAD";
                }
                this.Invoke((MethodInvoker)delegate () { ptbWhoerchecking.Visible = false; lblwhoerstt.Text = "OK"; lblwhoerstt.Visible = true; });
                return "OK";
            }
        }
        private void AutoSaveSSH()
        {
            File.WriteAllLines("SSH_unused.txt", SSH);
        }
        private void ChangeSSH()
        {
            threadrunning = true;            
            if (myform2.changebymanual)
            {
                if (thread247running)
                {
                    thread247.Abort();
                    thread247running = false;
                }
                if (threadscheduledrunning)
                {
                    threadscheduled.Abort();
                    threadscheduledrunning = false;
                }
            }else if (myform2.changeby247 && threadscheduledrunning)
            {
                threadscheduled.Abort();
                threadscheduledrunning = false;
            }
            else if(myform2.changebyscheduled && thread247running)
            {
                thread247.Abort();
                thread247running = false;
            }
            //if(hasimportsshlink == false && myform2.importsshfromfile == false)
            //{
            //    DownloadFile(myform2.linkssh, "SSH_DOWNLOAD.txt");
            //    LoadSSH("SSH_DOWNLOAD.txt");
            //}
            if(cbChangetimezone.Checked || myform2.location)
            {
                if (File.Exists(Path.Combine(Application.StartupPath,"GeoLite2-City.mmdb")) == false)
                {
                    DialogResult result = MessageBox.Show("File not fund: GeoLite2-City.mmdb\nDo you want to download this file now?", "ERROR File", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        this.Invoke((MethodInvoker)delegate () { lblSshinfo.Text = "Please wait for download database"; });
                        DownloadFile("http://geolite.maxmind.com/download/geoip/database/GeoLite2-City.mmdb.gz", "GeoLite2-City.mmdb.gz");
                        Ungzip("GeoLite2-City.mmdb.gz");
                        this.Invoke((MethodInvoker)delegate () { lblSshinfo.Text = "Download completed! Start change ssh"; });
                    }
                    else
                    {
                        ptbChange.Enabled = true;
                        return;
                    }
                }
            }            
            Showstatus("Connecting");
            //port = Convert.ToInt32(nmrPort.Value);
            TimeoutSeconds = Convert.ToInt32(nmrTimeout.Value);
            try
            {
                BitviseHandle.Disconnect(port);
            }
            catch
            {
                //
            }
            string[] _listport = CTLConfig._port.Split(';');
            KillALL();
            foreach (string strport in _listport)
            {
                port = Convert.ToInt32(strport);
                while (flagstop == false)
                {
                    ssh = GetSsh();
                    if (ssh == "")
                    {
                        if (myform2.loopssh)
                        {
                            loop++;
                            //if (myform2.importsshfromfile)
                            //{
                            //    LoadSSH(importsshbyfilepath);
                            //}
                            //else
                            //{
                            //    DownloadFile(myform2.linkssh, "SSH_DOWNLOAD.txt");
                            //    LoadSSH("SSH_DOWNLOAD.txt");
                            //}
                        }
                        else
                        {
                            Showstatus("Disconnected");
                            MessageBox.Show("SSH Out Of Stock");
                            break;
                        }
                    }
                    IP = Regex.Split(ssh, "\\|")[0];
                    User = Regex.Split(ssh, "\\|")[1];
                    Pass = Regex.Split(ssh, "\\|")[2];
                    Thread.Sleep(50);
                    if (BitviseHandle.Connect(IP, User, Pass, port, TimeoutSeconds, myform2.hidebvs))
                    {
                        Showstatus("Connected");
                        sshlive++;
                        this.Invoke((MethodInvoker)delegate() { lblLive.Text = sshlive.ToString(); });
                        if (myform2.location)
                        {
                            GetSSHinfo(IP);
                        }
                        if (myform2.whoershowinfo)
                        {
                            if (Whoer(port) == "BAD")
                            {
                                try
                                {
                                    BitviseHandle.Disconnect(port);
                                }
                                catch
                                {
                                    //
                                }
                                continue;
                            }
                        }
                        if (myform2.autosave)
                        {
                            AutoSaveSSH();
                        }
                        break;
                    }
                    else
                    {
                        sshdie++;
                        this.Invoke((MethodInvoker)delegate() { lblDie.Text = sshdie.ToString(); });
                        Thread.Sleep(50);
                    }
                }
            }
            threadrunning = false;
            if (myform2.changeby247)
            {               
                thread247 = new Thread(() => Change247());
                thread247.IsBackground = true;
                thread247.Start();
                Application.DoEvents();
            }else if (myform2.changebyscheduled)
            {                
                threadscheduled = new Thread(() => ChangeScheduled(Convert.ToInt32(myform2.scheduleddelay)*60000));
                threadscheduled.IsBackground = true;
                threadscheduled.Start();
                Application.DoEvents();
            }
        }
        private void Change247()
        {
            thread247running = true;
            while(flagstop == false)
            {
                if (BitviseHandle.CheckConnect(IP, port))
                {
                    Thread.Sleep(5000);                    
                }
                else
                {
                    threadconnect.Abort();
                    flagstop = false;
                    threadconnect = new Thread(() => ChangeSSH());
                    threadconnect.IsBackground = true;
                    threadconnect.Start();
                    Application.DoEvents();
                    break;
                }
            }     
        }
        private void ChangeScheduled(int delay)
        {
            threadscheduledrunning = true;
            Thread.Sleep(delay);
            threadconnect.Abort();
            flagstop = false;
            threadconnect = new Thread(() => ChangeSSH());
            threadconnect.IsBackground = true;
            threadconnect.Start();
            Application.DoEvents();
        }
        private void btnLoadssh_Click(object sender, EventArgs e)
        {
            //string path = null;
            if (myform2.importsshfromfile)
            {
                OpenFileDialog choofdlog = new OpenFileDialog();
                choofdlog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
                choofdlog.FilterIndex = 1;
                choofdlog.Multiselect = false;
                if (choofdlog.ShowDialog() == DialogResult.OK)
                {
                    importsshbyfilepath = choofdlog.FileName;
                    LoadSSH(choofdlog.FileName);
                }
            }
            else
            {
                MessageBox.Show("SSH import by URL!\nPlease change setting.");
            }

        }

        public string IanaToWindows(string ianaZoneId)
        {
            var utcZones = new[] { "Etc/UTC", "Etc/UCT", "Etc/GMT" };
            if (utcZones.Contains(ianaZoneId, StringComparer.Ordinal))
                return "UTC";

            var tzdbSource = NodaTime.TimeZones.TzdbDateTimeZoneSource.Default;

            // resolve any link, since the CLDR doesn't necessarily use canonical IDs
            var links = tzdbSource.CanonicalIdMap
                .Where(x => x.Value.Equals(ianaZoneId, StringComparison.Ordinal))
                .Select(x => x.Key);

            // resolve canonical zones, and include original zone as well
            var possibleZones = tzdbSource.CanonicalIdMap.ContainsKey(ianaZoneId)
                ? links.Concat(new[] { tzdbSource.CanonicalIdMap[ianaZoneId], ianaZoneId })
                : links;

            // map the windows zone
            var mappings = tzdbSource.WindowsMapping.MapZones;
            var item = mappings.FirstOrDefault(x => x.TzdbIds.Any(possibleZones.Contains));
            if (item == null) return null;
            return item.WindowsId;
        }
        public string WindowsToIana(string windowsZoneId)
        {
            if (windowsZoneId.Equals("UTC", StringComparison.Ordinal))
                return "Etc/UTC";

            var tzdbSource = NodaTime.TimeZones.TzdbDateTimeZoneSource.Default;
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(windowsZoneId);
            if (tzi == null) return null;
            var tzid = tzdbSource.MapTimeZoneId(tzi);
            if (tzid == null) return null;
            return tzdbSource.CanonicalIdMap[tzid];
        }

        private void Ungzip(string _path)
        {
            FileInfo gzipFileName = new FileInfo(_path);
            using (FileStream fileToDecompressAsStream = gzipFileName.OpenRead())
            {
                string decompressedFileName = "GeoLite2-City.mmdb";
                using (FileStream decompressedStream = File.Create(decompressedFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(fileToDecompressAsStream, CompressionMode.Decompress))
                    {
                        try
                        {
                            decompressionStream.CopyTo(decompressedStream);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR cmnr!\n" + ex.Message);
                        }
                    }
                }
            }
        }
    }
        public class BitviseHandle
        {
            #region Windows API - user32.dll configs
            private const int WM_CLOSE = 16;
            private const int BN_CLICKED = 245;
            private const int WM_LBUTTONDOWN = 0x0201;
            private const int WM_LBUTTONUP = 0x0202;
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int SendMessage(int hWnd, int msg, int wParam, IntPtr lParam);
            //Click - Worked Perfect
            //SendMessage((int)hwnd, WM_LBUTTONDOWN, 0, IntPtr.Zero);
            //Thread.Sleep(100);
            //SendMessage((int)hwnd, WM_LBUTTONUP, 0, IntPtr.Zero);
            //---
            //Close Window
            //SendMessage((int)hwnd, WM_CLOSE ,0 , IntPtr.Zero);
            #endregion
            private static Hashtable BitviseList = new Hashtable();
            // public static int TimeoutSeconds = 9;       
            private static int PortIndex = 1079;
            public static int GetPortAvailable()
            {
                PortIndex++;
                if (PortIndex >= 1181) PortIndex = 1079;
                Process BitviseApp = new Process();
                BitviseList.Add(PortIndex, BitviseApp);
                return PortIndex;
            }

            public static bool Connect(string Host, string User, string Pass, int ForwardPort, int TimeoutSeconds, bool hidebitvise = true)
            {
                bool Connected = false;

                //Start Bitvise - Auto Login
                ProcessStartInfo sinfo = new ProcessStartInfo();
                //sinfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "Bitvise\\BvSsh.exe";
                //sinfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "Bitvise";
                sinfo.FileName = Application.StartupPath + "\\Bitvise\\BvSsh.exe";
                sinfo.WorkingDirectory = Application.StartupPath + "\\Bitvise";
            if (hidebitvise)
            {
                sinfo.Arguments = "-noRegistry -profile=\"" + Application.StartupPath + "\\Bitvise\\Profiles\\" + ForwardPort + ".bscp\" -host=" + Host + " -user=" + User + " -password=" + Pass + " -loginOnStartup -hide=trayIcon";
            }
            else
            {
                sinfo.Arguments = "-noRegistry -profile=\"" + Application.StartupPath + "\\Bitvise\\Profiles\\" + ForwardPort + ".bscp\" -host=" + Host + " -user=" + User + " -password=" + Pass + " -loginOnStartup";
            }
                
                Process BitviseApp = Process.Start(sinfo);

                BitviseList[ForwardPort] = BitviseApp;

                Thread.Sleep(2000);
                //Bitvise Login Checking...
                for (int i = 0; i < TimeoutSeconds; i++)
                {
                    //Detect Host Key Verification
                    SystemWindow[] wins = SystemWindow.FilterToplevelWindows((SystemWindow w) => { return w.Title == "Host Key Verification"; });
                    if (wins.Length > 0)
                    {
                        SystemWindow[] wins2 = wins[0].FilterDescendantWindows(false, (SystemWindow w) => { return w.Title == "&Accept for This Session"; }); //Accept and &Save
                        if (wins2.Length > 0)
                        {
                            //Click 4 times to effected !
                            SendMessage((int)wins2[0].HWnd, WM_LBUTTONDOWN, 0, IntPtr.Zero);
                            Thread.Sleep(10);
                            SendMessage((int)wins2[0].HWnd, WM_LBUTTONUP, 0, IntPtr.Zero);

                            SendMessage((int)wins2[0].HWnd, WM_LBUTTONDOWN, 0, IntPtr.Zero);
                            Thread.Sleep(10);
                            SendMessage((int)wins2[0].HWnd, WM_LBUTTONUP, 0, IntPtr.Zero);
                        }
                    }

                    //Detect Connected
                    SystemWindow[] wins3 = SystemWindow.FilterToplevelWindows((SystemWindow w) => { return w.Title == "Bitvise SSH Client - " + ForwardPort + ".bscp - " + Host + ":22"; });
                    if (wins3.Length > 0)
                    {
                        Connected = true;
                        break;
                    }
                    Thread.Sleep(1000);
                }

                if (Connected == false)
                {
                    try
                    {
                        BitviseApp.Kill();
                        BitviseApp.Dispose();
                    }
                    catch { }
                }


                return Connected;
            }
        public static bool CheckConnect(string Host, int ForwardPort)
        {
            SystemWindow[] winscheck = SystemWindow.FilterToplevelWindows((SystemWindow w) => { return w.Title == "Bitvise SSH Client - " + ForwardPort + ".bscp - " + Host + ":22"; });
            if (winscheck.Length > 0)
            {
                return true;    
            }
            return false;
        }

            public static void Disconnect(int ForwardPort)
            {
                if (BitviseList[ForwardPort] == null) return;

                try
                {
                    Process BitviseApp = BitviseList[ForwardPort] as Process;
                    BitviseApp.Kill();
                    BitviseApp.Dispose();
                }
                catch { }
            }

            private static bool GetPort(string Host, int Port)
            {
                return true;
            }

        }
}