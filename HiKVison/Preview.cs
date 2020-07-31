using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;

using System.Data;
using System.Text;
using System.IO;

using System.Runtime.InteropServices;

namespace HiKVison
{
    /// <summary>
    /// Form1 ��ժҪ˵����
    /// </summary>
    public class Preview
    {
        private uint iLastErr = 0;
        private Int32 m_lUserID = -1;
        private bool m_bInitSDK = false;
        private bool m_bRecord = false;
        private bool m_bTalk = false;
        private Int32 m_lRealHandle = -1;
        private int lVoiceComHandle = -1;
        private string str;

        CHCNetSDK.REALDATACALLBACK RealData = null;
        public CHCNetSDK.NET_DVR_PTZPOS m_struPtzCfg;

        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Preview()
        {
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                //MessageBox.Show("NET_DVR_Init error!");
                //return;
            }
            else
            {
                //����SDK��־ To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
            }
            //
            // TODO: �� InitializeComponent ���ú������κι��캯������
            //
        }
         
        ///// <summary>
        ///// ������������ʹ�õ���Դ��
        ///// </summary>
        protected void Dispose(bool disposing)
        {
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
            }
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
            }
            if (m_bInitSDK == true)
            {
                CHCNetSDK.NET_DVR_Cleanup();
            }
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            //base.Dispose(disposing);
        }

        //#region Windows ������������ɵĴ���
        ///// <summary>
        ///// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        ///// �˷��������ݡ�
        ///// </summary>
        //private void InitializeComponent()
        //      {
        //          this.label1 = new System.Windows.Forms.Label();
        //          this.label2 = new System.Windows.Forms.Label();
        //          this.label3 = new System.Windows.Forms.Label();
        //          this.label4 = new System.Windows.Forms.Label();
        //          this.btnLogin = new System.Windows.Forms.Button();
        //          this.btnPreview = new System.Windows.Forms.Button();
        //          this.RealPlayWnd = new System.Windows.Forms.PictureBox();
        //          this.textBoxIP = new System.Windows.Forms.TextBox();
        //          this.textBoxPort = new System.Windows.Forms.TextBox();
        //          this.textBoxUserName = new System.Windows.Forms.TextBox();
        //          this.textBoxPassword = new System.Windows.Forms.TextBox();
        //          this.label5 = new System.Windows.Forms.Label();
        //          this.label6 = new System.Windows.Forms.Label();
        //          this.label7 = new System.Windows.Forms.Label();
        //          this.label8 = new System.Windows.Forms.Label();
        //          this.label9 = new System.Windows.Forms.Label();
        //          this.label10 = new System.Windows.Forms.Label();
        //          this.btnBMP = new System.Windows.Forms.Button();
        //          this.btnJPEG = new System.Windows.Forms.Button();
        //          this.label11 = new System.Windows.Forms.Label();
        //          this.label12 = new System.Windows.Forms.Label();
        //          this.label13 = new System.Windows.Forms.Label();
        //          this.textBoxChannel = new System.Windows.Forms.TextBox();
        //          this.btnRecord = new System.Windows.Forms.Button();
        //          this.label14 = new System.Windows.Forms.Label();
        //          this.btn_Exit = new System.Windows.Forms.Button();
        //          this.btnVioceTalk = new System.Windows.Forms.Button();
        //          this.label16 = new System.Windows.Forms.Label();
        //          this.label17 = new System.Windows.Forms.Label();
        //          this.textBoxID = new System.Windows.Forms.TextBox();
        //          this.PreSet = new System.Windows.Forms.Button();
        //          this.label23 = new System.Windows.Forms.Label();
        //          ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).BeginInit();
        //          this.SuspendLayout();
        //          // 
        //          // label1
        //          // 
        //          this.label1.Location = new System.Drawing.Point(12, 14);
        //          this.label1.Name = "label1";
        //          this.label1.Size = new System.Drawing.Size(74, 18);
        //          this.label1.TabIndex = 0;
        //          this.label1.Text = "Device IP";
        //          // 
        //          // label2
        //          // 
        //          this.label2.Location = new System.Drawing.Point(12, 59);
        //          this.label2.Name = "label2";
        //          this.label2.Size = new System.Drawing.Size(62, 16);
        //          this.label2.TabIndex = 0;
        //          this.label2.Text = "User Name";
        //          // 
        //          // label3
        //          // 
        //          this.label3.Location = new System.Drawing.Point(234, 58);
        //          this.label3.Name = "label3";
        //          this.label3.Size = new System.Drawing.Size(63, 16);
        //          this.label3.TabIndex = 0;
        //          this.label3.Text = "Password";
        //          // 
        //          // label4
        //          // 
        //          this.label4.Location = new System.Drawing.Point(234, 14);
        //          this.label4.Name = "label4";
        //          this.label4.Size = new System.Drawing.Size(79, 17);
        //          this.label4.TabIndex = 0;
        //          this.label4.Text = "Device Port";
        //          // 
        //          // btnLogin
        //          // 
        //          this.btnLogin.Location = new System.Drawing.Point(435, 38);
        //          this.btnLogin.Name = "btnLogin";
        //          this.btnLogin.Size = new System.Drawing.Size(78, 50);
        //          this.btnLogin.TabIndex = 1;
        //          this.btnLogin.Text = "Login";
        //          this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
        //          // 
        //          // btnPreview
        //          // 
        //          this.btnPreview.Location = new System.Drawing.Point(17, 571);
        //          this.btnPreview.Name = "btnPreview";
        //          this.btnPreview.Size = new System.Drawing.Size(76, 34);
        //          this.btnPreview.TabIndex = 7;
        //          this.btnPreview.Text = "Live View";
        //          this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
        //          // 
        //          // RealPlayWnd
        //          // 
        //          this.RealPlayWnd.BackColor = System.Drawing.SystemColors.WindowText;
        //          this.RealPlayWnd.Location = new System.Drawing.Point(18, 104);
        //          this.RealPlayWnd.Name = "RealPlayWnd";
        //          this.RealPlayWnd.Size = new System.Drawing.Size(495, 405);
        //          this.RealPlayWnd.TabIndex = 4;
        //          this.RealPlayWnd.TabStop = false;
        //          // 
        //          // textBoxIP
        //          // 
        //          this.textBoxIP.Location = new System.Drawing.Point(78, 24);
        //          this.textBoxIP.Name = "textBoxIP";
        //          this.textBoxIP.Size = new System.Drawing.Size(114, 21);
        //          this.textBoxIP.TabIndex = 2;
        //          this.textBoxIP.Text = "192.168.0.106";
        //          // 
        //          // textBoxPort
        //          // 
        //          this.textBoxPort.Location = new System.Drawing.Point(308, 24);
        //          this.textBoxPort.Name = "textBoxPort";
        //          this.textBoxPort.Size = new System.Drawing.Size(112, 21);
        //          this.textBoxPort.TabIndex = 3;
        //          this.textBoxPort.Text = "8000";
        //          // 
        //          // textBoxUserName
        //          // 
        //          this.textBoxUserName.Location = new System.Drawing.Point(78, 70);
        //          this.textBoxUserName.Name = "textBoxUserName";
        //          this.textBoxUserName.Size = new System.Drawing.Size(114, 21);
        //          this.textBoxUserName.TabIndex = 4;
        //          this.textBoxUserName.Text = "admin";
        //          // 
        //          // textBoxPassword
        //          // 
        //          this.textBoxPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
        //          this.textBoxPassword.Location = new System.Drawing.Point(308, 70);
        //          this.textBoxPassword.Name = "textBoxPassword";
        //          this.textBoxPassword.PasswordChar = '*';
        //          this.textBoxPassword.Size = new System.Drawing.Size(112, 21);
        //          this.textBoxPassword.TabIndex = 5;
        //          this.textBoxPassword.Text = "1q2w3e4r";
        //          // 
        //          // label5
        //          // 
        //          this.label5.AutoSize = true;
        //          this.label5.Location = new System.Drawing.Point(12, 33);
        //          this.label5.Name = "label5";
        //          this.label5.Size = new System.Drawing.Size(41, 12);
        //          this.label5.TabIndex = 9;
        //          this.label5.Text = "�豸IP";
        //          // 
        //          // label6
        //          // 
        //          this.label6.AutoSize = true;
        //          this.label6.Location = new System.Drawing.Point(234, 33);
        //          this.label6.Name = "label6";
        //          this.label6.Size = new System.Drawing.Size(53, 12);
        //          this.label6.TabIndex = 10;
        //          this.label6.Text = "�豸�˿�";
        //          // 
        //          // label7
        //          // 
        //          this.label7.AutoSize = true;
        //          this.label7.Location = new System.Drawing.Point(14, 79);
        //          this.label7.Name = "label7";
        //          this.label7.Size = new System.Drawing.Size(41, 12);
        //          this.label7.TabIndex = 11;
        //          this.label7.Text = "�û���";
        //          // 
        //          // label8
        //          // 
        //          this.label8.AutoSize = true;
        //          this.label8.Location = new System.Drawing.Point(236, 79);
        //          this.label8.Name = "label8";
        //          this.label8.Size = new System.Drawing.Size(29, 12);
        //          this.label8.TabIndex = 12;
        //          this.label8.Text = "����";
        //          // 
        //          // label9
        //          // 
        //          this.label9.AutoSize = true;
        //          this.label9.Location = new System.Drawing.Point(18, 550);
        //          this.label9.Name = "label9";
        //          this.label9.Size = new System.Drawing.Size(29, 12);
        //          this.label9.TabIndex = 13;
        //          this.label9.Text = "Ԥ��";
        //          // 
        //          // label10
        //          // 
        //          this.label10.AutoSize = true;
        //          this.label10.Location = new System.Drawing.Point(442, 19);
        //          this.label10.Name = "label10";
        //          this.label10.Size = new System.Drawing.Size(29, 12);
        //          this.label10.TabIndex = 14;
        //          this.label10.Text = "��¼";
        //          // 
        //          // btnBMP
        //          // 
        //          this.btnBMP.Location = new System.Drawing.Point(110, 572);
        //          this.btnBMP.Name = "btnBMP";
        //          this.btnBMP.Size = new System.Drawing.Size(79, 34);
        //          this.btnBMP.TabIndex = 8;
        //          this.btnBMP.Text = "Capture BMP ";
        //          this.btnBMP.UseVisualStyleBackColor = true;
        //          this.btnBMP.Click += new System.EventHandler(this.btnBMP_Click);
        //          // 
        //          // btnJPEG
        //          // 
        //          this.btnJPEG.Location = new System.Drawing.Point(208, 571);
        //          this.btnJPEG.Name = "btnJPEG";
        //          this.btnJPEG.Size = new System.Drawing.Size(97, 34);
        //          this.btnJPEG.TabIndex = 9;
        //          this.btnJPEG.Text = "Capture JPEG";
        //          this.btnJPEG.UseVisualStyleBackColor = true;
        //          this.btnJPEG.Click += new System.EventHandler(this.btnJPEG_Click);
        //          // 
        //          // label11
        //          // 
        //          this.label11.AutoSize = true;
        //          this.label11.Location = new System.Drawing.Point(113, 550);
        //          this.label11.Name = "label11";
        //          this.label11.Size = new System.Drawing.Size(47, 12);
        //          this.label11.TabIndex = 17;
        //          this.label11.Text = "BMPץͼ";
        //          // 
        //          // label12
        //          // 
        //          this.label12.AutoSize = true;
        //          this.label12.Location = new System.Drawing.Point(214, 550);
        //          this.label12.Name = "label12";
        //          this.label12.Size = new System.Drawing.Size(53, 12);
        //          this.label12.TabIndex = 18;
        //          this.label12.Text = "JPEGץͼ";
        //          // 
        //          // label13
        //          // 
        //          this.label13.AutoSize = true;
        //          this.label13.Location = new System.Drawing.Point(17, 521);
        //          this.label13.Name = "label13";
        //          this.label13.Size = new System.Drawing.Size(83, 12);
        //          this.label13.TabIndex = 19;
        //          this.label13.Text = "Ԥ��/ץͼͨ��";
        //          // 
        //          // textBoxChannel
        //          // 
        //          this.textBoxChannel.Location = new System.Drawing.Point(107, 517);
        //          this.textBoxChannel.Name = "textBoxChannel";
        //          this.textBoxChannel.Size = new System.Drawing.Size(100, 21);
        //          this.textBoxChannel.TabIndex = 6;
        //          this.textBoxChannel.Text = "33";
        //          // 
        //          // btnRecord
        //          // 
        //          this.btnRecord.Location = new System.Drawing.Point(319, 571);
        //          this.btnRecord.Name = "btnRecord";
        //          this.btnRecord.Size = new System.Drawing.Size(100, 34);
        //          this.btnRecord.TabIndex = 10;
        //          this.btnRecord.Text = "Start Record";
        //          this.btnRecord.UseVisualStyleBackColor = true;
        //          this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
        //          // 
        //          // label14
        //          // 
        //          this.label14.AutoSize = true;
        //          this.label14.Location = new System.Drawing.Point(320, 550);
        //          this.label14.Name = "label14";
        //          this.label14.Size = new System.Drawing.Size(65, 12);
        //          this.label14.TabIndex = 22;
        //          this.label14.Text = "�ͻ���¼��";
        //          // 
        //          // btn_Exit
        //          // 
        //          this.btn_Exit.Location = new System.Drawing.Point(438, 615);
        //          this.btn_Exit.Name = "btn_Exit";
        //          this.btn_Exit.Size = new System.Drawing.Size(75, 32);
        //          this.btn_Exit.TabIndex = 11;
        //          this.btn_Exit.Tag = "";
        //          this.btn_Exit.Text = "�˳� Exit";
        //          this.btn_Exit.UseVisualStyleBackColor = true;
        //          this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
        //          // 
        //          // btnVioceTalk
        //          // 
        //          this.btnVioceTalk.Location = new System.Drawing.Point(18, 641);
        //          this.btnVioceTalk.Name = "btnVioceTalk";
        //          this.btnVioceTalk.Size = new System.Drawing.Size(75, 34);
        //          this.btnVioceTalk.TabIndex = 25;
        //          this.btnVioceTalk.Text = "Start Talk";
        //          this.btnVioceTalk.UseVisualStyleBackColor = true;
        //          this.btnVioceTalk.Click += new System.EventHandler(this.btnVioceTalk_Click);
        //          // 
        //          // label16
        //          // 
        //          this.label16.AutoSize = true;
        //          this.label16.Location = new System.Drawing.Point(18, 621);
        //          this.label16.Name = "label16";
        //          this.label16.Size = new System.Drawing.Size(53, 12);
        //          this.label16.TabIndex = 26;
        //          this.label16.Text = "�����Խ�";
        //          // 
        //          // label17
        //          // 
        //          this.label17.AutoSize = true;
        //          this.label17.Location = new System.Drawing.Point(238, 520);
        //          this.label17.Name = "label17";
        //          this.label17.Size = new System.Drawing.Size(29, 12);
        //          this.label17.TabIndex = 27;
        //          this.label17.Text = "��ID";
        //          // 
        //          // textBoxID
        //          // 
        //          this.textBoxID.Location = new System.Drawing.Point(277, 516);
        //          this.textBoxID.Name = "textBoxID";
        //          this.textBoxID.Size = new System.Drawing.Size(225, 21);
        //          this.textBoxID.TabIndex = 28;
        //          // 
        //          // PreSet
        //          // 
        //          this.PreSet.Location = new System.Drawing.Point(115, 641);
        //          this.PreSet.Name = "PreSet";
        //          this.PreSet.Size = new System.Drawing.Size(97, 33);
        //          this.PreSet.TabIndex = 31;
        //          this.PreSet.Text = "PTZ Control";
        //          this.PreSet.UseVisualStyleBackColor = true;
        //          this.PreSet.Click += new System.EventHandler(this.PreSet_Click);
        //          // 
        //          // label23
        //          // 
        //          this.label23.AutoSize = true;
        //          this.label23.Location = new System.Drawing.Point(119, 621);
        //          this.label23.Name = "label23";
        //          this.label23.Size = new System.Drawing.Size(53, 12);
        //          this.label23.TabIndex = 32;
        //          this.label23.Text = "��̨����";
        //          // 
        //          // Preview
        //          // 
        //          this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
        //          this.ClientSize = new System.Drawing.Size(531, 687);
        //          this.Controls.Add(this.label23);
        //          this.Controls.Add(this.PreSet);
        //          this.Controls.Add(this.textBoxID);
        //          this.Controls.Add(this.label17);
        //          this.Controls.Add(this.label16);
        //          this.Controls.Add(this.btnVioceTalk);
        //          this.Controls.Add(this.btn_Exit);
        //          this.Controls.Add(this.label14);
        //          this.Controls.Add(this.btnRecord);
        //          this.Controls.Add(this.textBoxChannel);
        //          this.Controls.Add(this.label13);
        //          this.Controls.Add(this.label12);
        //          this.Controls.Add(this.label11);
        //          this.Controls.Add(this.btnJPEG);
        //          this.Controls.Add(this.btnBMP);
        //          this.Controls.Add(this.label10);
        //          this.Controls.Add(this.label9);
        //          this.Controls.Add(this.label8);
        //          this.Controls.Add(this.label7);
        //          this.Controls.Add(this.label6);
        //          this.Controls.Add(this.label5);
        //          this.Controls.Add(this.textBoxPassword);
        //          this.Controls.Add(this.textBoxUserName);
        //          this.Controls.Add(this.textBoxPort);
        //          this.Controls.Add(this.textBoxIP);
        //          this.Controls.Add(this.RealPlayWnd);
        //          this.Controls.Add(this.btnPreview);
        //          this.Controls.Add(this.btnLogin);
        //          this.Controls.Add(this.label1);
        //          this.Controls.Add(this.label2);
        //          this.Controls.Add(this.label3);
        //          this.Controls.Add(this.label4);
        //          this.Name = "Preview";
        //          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        //          this.Text = "Preview";
        //          this.Load += new System.EventHandler(this.Preview_Load);
        //          ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).EndInit();
        //          this.ResumeLayout(false);
        //          this.PerformLayout();

        //}
        //#endregion

        ///// <summary>
        ///// Ӧ�ó��������ڵ㡣
        ///// </summary>
        //[STAThread]
        //static void Main() 
        //{
        //	Application.Run(new Preview());
        //}

        //private void textBox1_TextChanged(object sender, System.EventArgs e)
        //{

        //}

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            if (textBoxIP.Text == "" || textBoxPort.Text == "" ||
                textBoxUserName.Text == "" || textBoxPassword.Text == "")
            {
                //MessageBox.Show("Please input IP, Port, User name and Password!");
                return;
            }
            if (m_lUserID < 0)
            {
                string DVRIPAddress = textBoxIP.Text; //�豸IP��ַ��������
                Int16 DVRPortNumber = Int16.Parse(textBoxPort.Text);//�豸����˿ں�
                string DVRUserName = textBoxUserName.Text;//�豸��¼�û���
                string DVRPassword = textBoxPassword.Text;//�豸��¼����

                CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

                //��¼�豸 Login the device
                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (m_lUserID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Login_V30 failed, error code= " + iLastErr; //��¼ʧ�ܣ���������
                    //MessageBox.Show(str);
                    return;
                }
                else
                {
                    //��¼�ɹ�
                    //MessageBox.Show("Login Success!");
                    btnLogin.Text = "Logout";
                }

            }
            else
            {
                //ע����¼ Logout the device
                if (m_lRealHandle >= 0)
                {
                    //MessageBox.Show("Please stop live view firstly");
                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Logout failed, error code= " + iLastErr;
                    //MessageBox.Show(str);
                    return;
                }
                m_lUserID = -1;
                btnLogin.Text = "Login";
            }
            return;
        }

        private void btnPreview_Click(object sender, System.EventArgs e)
        {
            if (m_lUserID < 0)
            {
                //MessageBox.Show("Please login the device firstly");
                return;
            }

            if (m_lRealHandle < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd.Handle;//Ԥ������
                lpPreviewInfo.lChannel = Int16.Parse(textBoxChannel.Text);//Ԥte�����豸ͨ��
                lpPreviewInfo.dwStreamType = 0;//�������ͣ�0-��������1-��������2-����3��3-����4���Դ�����
                lpPreviewInfo.dwLinkMode = 0;//���ӷ�ʽ��0- TCP��ʽ��1- UDP��ʽ��2- �ಥ��ʽ��3- RTP��ʽ��4-RTP/RTSP��5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- ������ȡ����1- ����ȡ��
                lpPreviewInfo.dwDisplayBufNum = 1; //���ſⲥ�Ż�������󻺳�֡��
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;


                if (textBoxID.Text != "")
                {
                    lpPreviewInfo.lChannel = -1;
                    byte[] byStreamID = System.Text.Encoding.Default.GetBytes(textBoxID.Text);
                    lpPreviewInfo.byStreamID = new byte[32];
                    byStreamID.CopyTo(lpPreviewInfo.byStreamID, 0);
                }


                if (RealData == null)
                {
                    RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//Ԥ��ʵʱ���ص�����
                }

                IntPtr pUser = new IntPtr();//�û�����

                //��Ԥ�� Start live view 
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, RealData, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr; //Ԥ��ʧ�ܣ���������
                    //MessageBox.Show(str);
                    return;
                }
                else
                {
                    //Ԥ���ɹ�
                    btnPreview.Text = "Stop Live View";
                }
            }
            else
            {
                //ֹͣԤ�� Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopRealPlay failed, error code= " + iLastErr;
                    //MessageBox.Show(str);
                    return;
                }
                m_lRealHandle = -1;
                btnPreview.Text = "Live View";

            }
            return;
        }

        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            if (dwBufSize > 0)
            {
                byte[] sData = new byte[dwBufSize];
                Marshal.Copy(pBuffer, sData, 0, (Int32)dwBufSize);

                string str = "ʵʱ������.ps";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sData, 0, iLen);
                fs.Close();
            }
        }

        private void btnBMP_Click(object sender, EventArgs e)
        {
            string sBmpPicFileName;
            //ͼƬ����·�����ļ��� the path and file name to save
            sBmpPicFileName = "BMP_test.bmp";

            //BMPץͼ Capture a BMP picture
            if (!CHCNetSDK.NET_DVR_CapturePicture(m_lRealHandle, sBmpPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_CapturePicture failed, error code= " + iLastErr;
                //MessageBox.Show(str);
                return;
            }
            else
            {
                str = "Successful to capture the BMP file and the saved file is " + sBmpPicFileName;
                //MessageBox.Show(str); 
            }
            return;
        }

        private void btnJPEG_Click(object sender, EventArgs e)
        {
            string sJpegPicFileName;
            //ͼƬ����·�����ļ��� the path and file name to save
            sJpegPicFileName = "JPEG_test.jpg";

            int lChannel = Int16.Parse(textBoxChannel.Text); //ͨ���� Channel number

            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0; //ͼ������ Image quality
            lpJpegPara.wPicSize = 0xff; //ץͼ�ֱ��� Picture size: 2- 4CIF��0xff- Auto(ʹ�õ�ǰ�����ֱ���)��ץͼ�ֱ�����Ҫ�豸֧�֣�����ȡֵ��ο�SDK�ĵ�

            //JPEGץͼ Capture a JPEG picture
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID, lChannel, ref lpJpegPara, sJpegPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_CaptureJPEGPicture failed, error code= " + iLastErr;
                //MessageBox.Show(str);
                return;
            }
            else
            {
                str = "Successful to capture the JPEG file and the saved file is " + sJpegPicFileName;
                //MessageBox.Show(str);
            }
            return;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            //¼�񱣴�·�����ļ��� the path and file name to save
            string sVideoFileName;
            sVideoFileName = "Record_test.mp4";

            if (m_bRecord == false)
            {
                //ǿ��I֡ Make a I frame
                int lChannel = Int16.Parse(textBoxChannel.Text); //ͨ���� Channel number
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID, lChannel);

                //��ʼ¼�� Start recording
                if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, sVideoFileName))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_SaveRealData failed, error code= " + iLastErr;
                    //MessageBox.Show(str);
                    return;
                }
                else
                {
                    btnRecord.Text = "Stop Record";
                    m_bRecord = true;
                }
            }
            else
            {
                //ֹͣ¼�� Stop recording
                if (!CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopSaveRealData failed, error code= " + iLastErr;
                    //MessageBox.Show(str);
                    return;
                }
                else
                {
                    str = "Successful to stop recording and the saved file is " + sVideoFileName;
                    //MessageBox.Show(str);
                    btnRecord.Text = "Start Record";
                    m_bRecord = false;
                }
            }

            return;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            //ֹͣԤ�� Stop live view 
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
                m_lRealHandle = -1;
            }

            //ע����¼ Logout the device
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
                m_lUserID = -1;
            }

            CHCNetSDK.NET_DVR_Cleanup();

            //Application.Exit();
        }

        private void btnPTZ_Click(object sender, EventArgs e)
        {

        }

        public void VoiceDataCallBack(int lVoiceComHandle, IntPtr pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, System.IntPtr pUser)
        {
            byte[] sString = new byte[dwBufSize];
            Marshal.Copy(pRecvDataBuffer, sString, 0, (Int32)dwBufSize);

            if (byAudioFlag == 0)
            {
                //�������������Ƶ����д���ļ� save the data into a file
                string str = "PC�ɼ���Ƶ�ļ�.pcm";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sString, 0, iLen);
                fs.Close();
            }
            if (byAudioFlag == 1)
            {
                //�������������Ƶ����д���ļ� save the data into a file
                string str = "�豸��Ƶ�ļ�.pcm";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sString, 0, iLen);
                fs.Close();
            }

        }

        private void btnVioceTalk_Click(object sender, EventArgs e)
        {
            if (m_bTalk == false)
            {
                //��ʼ�����Խ� Start two-way talk
                CHCNetSDK.VOICEDATACALLBACKV30 VoiceData = new CHCNetSDK.VOICEDATACALLBACKV30(VoiceDataCallBack);//Ԥ��ʵʱ���ص�����

                lVoiceComHandle = CHCNetSDK.NET_DVR_StartVoiceCom_V30(m_lUserID, 1, true, VoiceData, IntPtr.Zero);
                //bNeedCBNoEncData [in]��Ҫ�ص��������������ͣ�0- �������������ݣ�1- ����ǰ��PCMԭʼ����

                if (lVoiceComHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StartVoiceCom_V30 failed, error code= " + iLastErr;
                    //MessageBox.Show(str);
                    return;
                }
                else
                {
                    btnVioceTalk.Text = "Stop Talk";
                    m_bTalk = true;
                }
            }
            else
            {
                //ֹͣ�����Խ� Stop two-way talk
                if (!CHCNetSDK.NET_DVR_StopVoiceCom(lVoiceComHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopVoiceCom failed, error code= " + iLastErr;
                    //MessageBox.Show(str);
                    return;
                }
                else
                {
                    btnVioceTalk.Text = "Start Talk";
                    m_bTalk = false;
                }
            }
        }




    }
}