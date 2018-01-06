using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Threading;
using UnityEngine;

namespace GoldfishScoping {
    public class ControllerManager : MonoBehaviour {
        // Use this for thread
        Thread thread;// = new Thread();//(new ThreadStart(tanasaki));//スレッド
        bool tushin = false;//スレッド用
        //raw data
        public static float ax, ay, az, gx, gy, gz, cx, cy, cz;
        //input data
        public static int fl1, fl2, fl3, fl4, fl5, fr1, fr2, fr3, fr4, fr5;
        public static float pitch, roll, yaw;
        public float pitch_m, roll_m, yaw_m;
        public static int flag;
        int gui_set=0;
        float pitch0;

        public Vector3 resetPos;
        public static Vector3 benchmark=Vector3.zero;
        protected Vector3 eulerAngles;
        float pitch_degree;
        float yaw_degree;
        //public Vector3 rotateAxis=new Vector3(0,1.3f,-2.6f);

        protected void UpdateData () {
            //Debug Info
            if (Input.GetKeyDown (KeyCode.C)) {
                FileStream cf = new FileStream ("Assets/config.txt", FileMode.Create, FileAccess.Write);
                StreamWriter writer3 = new StreamWriter (cf);
                writer3.WriteLine (gx); writer3.WriteLine (gy); writer3.WriteLine (gz);
                writer3.WriteLine (cx); writer3.WriteLine (cy); writer3.WriteLine (cz);
                writer3.Close ();
                Debug.Log ("config file create");
            }
            string c1, c2, c3;
            float cgx = 0, cgy = 0, cgz = 0;
            float gx1, gy1, gz1;
            FileStream config = new FileStream ("Assets/config.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader3 = new StreamReader (config);
            c1 = reader3.ReadLine ();
            c2 = reader3.ReadLine ();
            c3 = reader3.ReadLine ();
            cgx = float.Parse (c1);
            cgy = float.Parse (c2);
            cgz = float.Parse (c3);
            gx1 = gx - cgx; gy1 = gy - cgy; gz1 = gz - cgz;
            reader3.Close ();



            
            if (az > ay) pitch = ay * (float)0.045;
            else pitch = (2750 - az) * (float)0.045;
            roll = ax * (float)0.045;

            pitch_degree = pitch_m * 180 / Mathf.PI;
            pitch_m = pitch * Mathf.PI / 180;
            roll_m = roll * Mathf.PI / 180;
            //pitch_degree = ((gx-cgx)*Mathf.Sin(roll_m)-(gy-cgy)*Mathf.Sin(pitch_m)*Mathf.Cos(roll_m)-(gz-cgz)*Mathf.Cos(pitch_m)*Mathf.Cos(roll_m))*(float)0.00002;
            yaw_degree = -((gx1) * Mathf.Sin (roll_m) - (gy1) * Mathf.Sin (pitch_m) * Mathf.Cos (roll_m) - (gz1) * Mathf.Cos (pitch_m) * Mathf.Cos (roll_m)) * 0.00001f * 180 / Mathf.PI;
            yaw_m += yaw_degree * 2;

            pitch_degree = pitch - pitch_degree;


            //mini_game
            //transform.eulerAngles = eulerAngles;

            pitch_degree = az > 0 ? ay * 0.045f - pitch_degree : pitch_degree - ay * 0.045f;
            eulerAngles = new Vector3 (ay * 0.045f, yaw_m, ax * 0.045f + 90);




            //Debug Info
            if (Input.GetKey (KeyCode.Z)) {
                Debug.Log ("RESET");
                benchmark.Set (pitch, 0, roll);
                transform.position = resetPos;
                yaw_m = 0;
            }

            if (fl1 == 0 && fl2 == 0 && fl3 == 0 && fl4 == 0 && fl5 == 0 && fr1 == 0 && fr2 == 0 && fr3 == 0 && fr4 == 0 && fr5 == 0) flag = 0;

            //motion data
            if (az > 0) pitch = pitch_m * 180 / Mathf.PI;
            else if (az <= 0) pitch = (Mathf.PI - pitch_m) * 180 / Mathf.PI;
            roll = roll_m * 180 / Mathf.PI;
            yaw = yaw_m * 2;
            if (Input.GetKey (KeyCode.W)) gui_set = 0;
            if (Input.GetKey (KeyCode.E)) gui_set = 1;
            if (Input.GetKey (KeyCode.R)) gui_set = 2;

            //start 
            if (DllIsStart != true) {
                DllStart ("COM3", 38400, 8, 0, 1);
                Debug.Log ("dllStart");
            } else if (DllIsStart == true && DataTrsmt != true) {
                DataGetStart ();
                Debug.Log ("getStart");
                thread = new Thread (new ThreadStart (tanasaki));//スレッド
                tushin = true;
                thread.Start ();
            }
        }

        //DEBUG INFO
        void OnGUI () {
            int sw = Screen.width;
            int sh = Screen.height;
            GUI.color = Color.black;
            if (gui_set == 2) {
                GUI.Label (new Rect (sw - 120, 10, 120, 20), "accx = " + ax.ToString ());
                GUI.Box (new Rect (sw / 2 - 200, sh / 2, 10, -ax / 10), "");
                GUI.Label (new Rect (sw - 120, 30, 120, 20), "accy = " + ay.ToString ());
                GUI.Box (new Rect (sw / 2 - 170, sh / 2, 10, -ay / 10), "");
                GUI.Label (new Rect (sw - 120, 50, 120, 20), "accz = " + az.ToString ());
                GUI.Box (new Rect (sw / 2 - 140, sh / 2, 10, -az / 10), "");

                GUI.Label (new Rect (sw - 120, 70, 120, 20), "gyrx = " + gx.ToString ());
                GUI.Box (new Rect (sw / 2 - 30, sh / 2, 10, -gx / 10), "");
                GUI.Label (new Rect (sw - 120, 90, 120, 20), "gyry = " + gy.ToString ());
                GUI.Box (new Rect (sw / 2, sh / 2, 10, -gy / 10), "");
                GUI.Label (new Rect (sw - 120, 110, 120, 20), "gyrz = " + gz.ToString ());
                GUI.Box (new Rect (sw / 2 + 30, sh / 2, 10, -gz / 10), "");

                GUI.Label (new Rect (sw - 120, 130, 120, 20), "cmpx = " + cx.ToString ());
                GUI.Box (new Rect (sw / 2 + 140, sh / 2, 10, -cx), "");
                GUI.Label (new Rect (sw - 120, 150, 120, 20), "cmpy = " + cy.ToString ());
                GUI.Box (new Rect (sw / 2 + 170, sh / 2, 10, -cy), "");
                GUI.Label (new Rect (sw - 120, 170, 120, 20), "cmpz = " + cz.ToString ());
                GUI.Box (new Rect (sw / 2 + 200, sh / 2, 10, -cz), "");

                GUI.Label (new Rect (sw - 120, 190, 120, 20), "fingL1 = " + fl5.ToString ());
                GUI.Box (new Rect (sw / 2 - 200, sh - 10, 10, -fl5 / 10), "");
                GUI.Label (new Rect (sw - 120, 210, 120, 20), "fingL2 = " + fl4.ToString ());
                GUI.Box (new Rect (sw / 2 - 170, sh - 10, 10, -fl4 / 10), "");
                GUI.Label (new Rect (sw - 120, 230, 120, 20), "fingL3 = " + fl3.ToString ());
                GUI.Box (new Rect (sw / 2 - 140, sh - 10, 10, -fl3 / 10), "");
                GUI.Label (new Rect (sw - 120, 250, 120, 20), "fingL4 = " + fl2.ToString ());
                GUI.Box (new Rect (sw / 2 - 110, sh - 10, 10, -fl2 / 10), "");
                GUI.Label (new Rect (sw - 120, 270, 120, 20), "fingL5 = " + fl1.ToString ());
                GUI.Box (new Rect (sw / 2 - 80, sh - 10, 10, -fl1 / 10), "");
                GUI.Label (new Rect (sw - 120, 290, 120, 20), "fingR1 = " + fr1.ToString ());
                GUI.Box (new Rect (sw / 2 + 80, sh - 10, 10, -fr1 / 10), "");
                GUI.Label (new Rect (sw - 120, 310, 120, 20), "fingR2 = " + fr2.ToString ());
                GUI.Box (new Rect (sw / 2 + 110, sh - 10, 10, -fr2 / 10), "");
                GUI.Label (new Rect (sw - 120, 330, 120, 20), "fingR3 = " + fr3.ToString ());
                GUI.Box (new Rect (sw / 2 + 140, sh - 10, 10, -fr3 / 10), "");
                GUI.Label (new Rect (sw - 120, 350, 120, 20), "fingR4 = " + fr4.ToString ());
                GUI.Box (new Rect (sw / 2 + 170, sh - 10, 10, -fr4 / 10), "");
                GUI.Label (new Rect (sw - 120, 370, 120, 20), "fingR5 = " + fr5.ToString ());
                GUI.Box (new Rect (sw / 2 + 200, sh - 10, 10, -fr5 / 10), "");

                //GUI.Label(new Rect(sw-120,10,120,20),"accx = "+ax.ToString());
                GUI.Label (new Rect (sw - 120, 400, 120, 20), "pitch = " + pitch.ToString ());
                GUI.Label (new Rect (sw - 120, 430, 120, 20), "roll = " + roll.ToString ());
                GUI.Label (new Rect (sw - 120, 460, 120, 20), "yaw = " + yaw.ToString ());
            }
            //****************service start***********************
            if (gui_set > 0) {
                Rect rect = new Rect (sw - 160, 0, 40, 30);
                bool isClicked_start = GUI.Button (rect, "Start");
                if (isClicked_start) {
                    DllStart ("COM3", 38400, 8, 0, 1);
                    Debug.Log ("dllStart");
                }
                //if (DllIsStart == true) GUI.Box (rect, "OK");
                //*****************service stop***********************
                Rect rect1 = new Rect (sw - 120, 0, 40, 30);
                bool isClicked_stop = GUI.Button (rect1, "Stop");
                if (isClicked_stop) {
                    //if(Input.GetKeyDown(KeyCode.D)){
                    Dllend ();
                    Debug.Log ("dllEnd");
                }
                //*******************get start*****************************
                Rect rect2 = new Rect (sw - 80, 0, 40, 30);
                bool isClicked_get_start = GUI.Button (rect2, "GetStart");
                if (isClicked_get_start) {
                    DataGetStart ();
                    Debug.Log ("getStart");
                    thread = new Thread (new ThreadStart (tanasaki));//スレッド
                    tushin = true;
                    thread.Start ();
                    gui_set = 0;
                }
                //if (DataTrsmt == true) GUI.Box (rect2, "開始");
                //**********************get end*************************
                Rect rect3 = new Rect (sw - 40, 0, 40, 30);
                bool isClicked_get_end = GUI.Button (rect3, "EndStart");
                if (isClicked_get_end) {
                    //if (Input.GetKeyDown(KeyCode.F)){
                    DataGetEnd ();
                    tushin = false;
                    Debug.Log ("getEnd");
                }
            }
        }

        void OnApplicationQuit () {
            Debug.Log ("APPEND");
            tushin = false;
            Resources.UnloadUnusedAssets ();
            Destroy (this.gameObject);
        }

        public static void SensorData (ref int accx, ref int accy, ref int accz, ref int gyrx, ref int gyry, ref int gyrz, ref int cmpx, ref int cmpy, ref int cmpz, ref int fing1, ref int fing2, ref int fing3, ref int fing4, ref int fing5, ref int fing6, ref int fing7, ref int fing8, ref int fing9, ref int fing10) {
            //data = accx.ToString() + "," + accy.ToString() + "," + accz.ToString() + "," + gyrx.ToString() + "," + gyry.ToString() + "," + gyrz.ToString() + "," + cmpx.ToString() + "," + cmpy.ToString() + "," + cmpz.ToString() + "\r\n";

            float ax0, ay0, az0, gx0, gy0, gz0, cx0, cy0, cz0;
            ax0 = ax; ay0 = ay; az0 = az; gx0 = gx; gy0 = gy; gz0 = gz; cx0 = cx; cy0 = cy; cz0 = cz;
            if (accx < 5000) ax = ax0 * (float)0.8 + accx * (float)0.2; else if (accx > 60000) ax = ax0 * (float)0.8 + (accx - 65535) * (float)0.2;
            if (accy < 5000) ay = ay0 * (float)0.8 + accy * (float)0.2; else if (accy > 60000) ay = ay0 * (float)0.8 + (accy - 65535) * (float)0.2;
            if (accz < 5000) az = az0 * (float)0.8 + accz * (float)0.2; else if (accz > 60000) az = az0 * (float)0.8 + (accz - 65535) * (float)0.2;
            if (gyrx < 5000) gx = gx0 * (float)0.8 + gyrx * (float)0.2; else if (gyrx > 60000) gx = gx0 * (float)0.8 + (gyrx - 65535) * (float)0.2;
            if (gyry < 5000) gy = gy0 * (float)0.8 + gyry * (float)0.2; else if (gyry > 60000) gy = gy0 * (float)0.8 + (gyry - 65535) * (float)0.2;
            if (gyrz < 5000) gz = gz0 * (float)0.8 + gyrz * (float)0.2; else if (gyrz > 60000) gz = gz0 * (float)0.8 + (gyrz - 65535) * (float)0.2;
            if (cmpx < 5000) cx = cx0 * (float)0.8 + cmpx * (float)0.2; else if (cmpx > 60000) cx = cx0 * (float)0.8 + (cmpx - 65535) * (float)0.2;
            if (cmpy < 5000) cy = cy0 * (float)0.8 + cmpy * (float)0.2; else if (cmpy > 60000) cy = cy0 * (float)0.8 + (cmpy - 65535) * (float)0.2;
            if (cmpz < 5000) cz = cz0 * (float)0.8 + cmpz * (float)0.2; else if (cmpz > 60000) cz = cz0 * (float)0.8 + (cmpz - 65535) * (float)0.2;
            fr5 = fing1; fr4 = fing2; fr3 = fing3; fr2 = fing4; fr1 = fing5; fl1 = fing6; fl2 = fing7; fl3 = fing8; fl4 = fing9; fl5 = fing10;
        }

        public delegate void DataEventEventHandler (int AccX, int AccY, int AccZ,
                                                   int GyrX, int GyrY, int GyrZ,
                                                   int CmpX, int CmpY, int Cmp,
                                                   int ThumbR, int fing1R, int fing2R, int fing3R, int fing4R,
                                                   int ThumbL, int fing1L, int fing2L, int fing3L, int fing4L
                                                   ); //'Byval rcvstr as string)

        public event DataEventEventHandler DataEvent;
        public SerialPort Rs = new SerialPort ();

        //'Private Bgw As New BackgroundWorker
        private bool DllIsStart = false;
        private bool DataTrsmt = false;
        private string RcvData = "";
        private string nya;
        //' 戻り値
        public enum RetVal {
            NoErr = 0,       //'正常終了
            ErrEnd = -1,     //'エラー終了
            NoStart = -2,    //'サービスが開始されていない
            Started = -3,    //'サービスは既に開始されている
            ParaErr = -4,    //'パラメータエラー
            TimOv = -5,      //'通信タイムアウト
            CkSmErr = -6    //'チェックサムエラー
        }

        #region 非公開共通制御群
        private const char STX = (char)0x02;
        private const char ETX = (char)0x03;

        private void tanasaki () {
            string RcvData;
            bool check;

            Debug.Log ("tanasaki");
            while (tushin) {
                //Debug.Log(Rs.ReadChar ());
                check = false;
                //try
                //{
                Rs.ReadTimeout = 1000;
                RcvData = Rs.ReadTo (STX.ToString ());
                RcvData = Rs.ReadTo (ETX.ToString ());
                check = CheckCheckSum (" " + RcvData + " ");
                //}
                //catch(IOException)
                //{
                //}
                if (check) {
                    int[] SnsData = new int[20];
                    /* 加速度 */
                    SnsData[1] = int.Parse (RcvData.Substring (1, 5)); //'Acc
                    SnsData[2] = int.Parse (RcvData.Substring (7, 5));
                    SnsData[3] = int.Parse (RcvData.Substring (13, 5));
                    /* 角速度 */
                    SnsData[4] = int.Parse (RcvData.Substring (19, 5)); //'Gyr 
                    SnsData[5] = int.Parse (RcvData.Substring (25, 5));
                    SnsData[6] = int.Parse (RcvData.Substring (31, 5));
                    /*磁気*/
                    SnsData[7] = int.Parse (RcvData.Substring (38, 4));//'cmp
                    if (RcvData.Substring (37, 1) == "-") SnsData[7] = SnsData[7] * -1;
                    SnsData[8] = int.Parse (RcvData.Substring (44, 4));
                    if (RcvData.Substring (43, 1) == "-") SnsData[8] = SnsData[8] * -1;
                    SnsData[9] = int.Parse (RcvData.Substring (50, 4));
                    if (RcvData.Substring (49, 1) == "-") SnsData[9] = SnsData[9] * -1;
                    /* 圧力 */
                    SnsData[10] = int.Parse (RcvData.Substring (55, 4));//'right
                    SnsData[11] = int.Parse (RcvData.Substring (60, 4));
                    SnsData[12] = int.Parse (RcvData.Substring (65, 4));
                    SnsData[13] = int.Parse (RcvData.Substring (70, 4));
                    SnsData[14] = int.Parse (RcvData.Substring (75, 4));
                    SnsData[15] = int.Parse (RcvData.Substring (80, 4)); //'left
                    SnsData[16] = int.Parse (RcvData.Substring (85, 4));
                    SnsData[17] = int.Parse (RcvData.Substring (90, 4));
                    SnsData[18] = int.Parse (RcvData.Substring (95, 4));
                    SnsData[19] = int.Parse (RcvData.Substring (100, 4));

                    SensorData (ref SnsData[1], ref SnsData[2], ref SnsData[3],
                                ref SnsData[4], ref SnsData[5], ref SnsData[6],
                                ref SnsData[7], ref SnsData[8], ref SnsData[9],
                                ref SnsData[10], ref SnsData[11], ref SnsData[12], ref SnsData[13], ref SnsData[14],
                                ref SnsData[15], ref SnsData[16], ref SnsData[17], ref SnsData[18], ref SnsData[19]);
                }
            }
        }

        /*---------------------------*/
        //'チェックサムの計算
        /*---------------------------*/
        private byte CalcCheckSum (string BaseData) {
            int i;
            int len = BaseData.Length;
            int Temp = 0;
            if (len > 0) {
                for (i = 0; i < len; i++) {
                    if ((i != 0) && (i != len - 4) && (i != len - 3) && (i != len - 1)) {
                        //Temp = Temp ^ Asc(BaseData.Substring(i, 1));//c#：intにキャスト、VB:Asc関数
                        Temp = Temp ^ (int)BaseData[i];//c#：intにキャスト、VB:Asc関数
                    }
                }
            }
            return (byte)Temp;
        }

        /*---------------------------*/
        //'チェックサムの挿入
        /*---------------------------*/
        private string SetCheckSum (string cmnd) {
            byte Temp;
            string checksum;
            string sumcmnd;
            //int len;

            //len = cmnd.Length;
            Temp = CalcCheckSum (cmnd);
            checksum = string.Format ("{0,2:X}", Temp);
            sumcmnd = cmnd.Replace ("ss", checksum);//チェックサムセットできてない
            return sumcmnd;
        }
        /*未使用*/
        /*---------------------------*/
        //'チェックサムの抽出
        /*---------------------------*/
        private byte GetCheckSum (string cmnd) {
            int Temp = 0;
            int Length = cmnd.Length;
            Temp = Change1Value (cmnd[Length - 4]);
            Temp = Temp * 0x10;
            Temp = Temp + Change1Value (cmnd[Length - 3]);
            //Temp = (int)cmnd[Length - 2];
            //Temp = (int)(Temp * 0x10);
            //Temp = (int)(Temp + (int)cmnd[Length - 1]);
            return (byte)Temp;
        }
        /*---------------------------*/
        //'チェックサムの確認
        /*---------------------------*/
        private bool CheckCheckSum (string BaseData) {
            byte Temp1 = 0;
            byte Temp2 = 0;
            //int Length;
            //Length = BaseData.Length;
            Temp1 = CalcCheckSum (BaseData);
            ////Temp2 = Change1Value(BaseData.Substring (Length - 3, 1)) * 0x10;
            //Temp2 = Change1Value(BaseData[Length - 3]);
            //Temp2 = (byte)(Temp2 * 0x10);
            //Temp2 = (byte)(Temp2 + Change1Value(BaseData[Length - 2]));
            Temp2 = GetCheckSum (BaseData);
            if (Temp1 == Temp2) return true;
            return false;
        }
        //未使用
        /*---------------------------*/
        //'数値０～１５に対する１バイトの１６進文字を返す
        /*---------------------------*/
        private string Change1Str (int BaseValue) {
            if (BaseValue >= 0 && BaseValue <= 9) {
                //Change1Str = Chr(BaseValue | 0x30);
            } else if (BaseValue >= 10 && BaseValue <= 15) {
                //Change1Str = Chr((BaseValue - 9) | 0x40);
            } else {
                return "0";
            }
            return "";
        }
        /*---------------------------*/
        //'文字 "0"～"9"および"A"～"F"にたいする数値０～１５を返す
        /*---------------------------*/
        private byte Change1Value (char BaseValue) {
            byte result;
            if (((int)BaseValue >= 0x30) && ((int)BaseValue <= 0x39)) {
                result = (byte)((int)BaseValue & 0x0F);
                return result;
            } else if (((int)BaseValue >= 0x41) && ((int)BaseValue <= 0x46)) {
                result = (byte)((int)BaseValue - 7 & 0x0F);
                return result;
            } else if (((int)(BaseValue) >= 0x61) && ((int)(BaseValue)) <= 0x66) {
                result = (byte)((int)BaseValue - 7 & 0x0F);
                return result;
            } else {
                return 0;
            }
        }
        /*---------------------------*/
        //'データ送信
        /*---------------------------*/
        private void SerialSendData (string SndData) {
            try {
                //if(DllIsStart == true)
                //{
                RcvData = "";
                Rs.Write (SndData);
                //}
            } catch (IOException) {
            }
        }
        /*---------------------------*/
        //'データ受信
        /*---------------------------*/
        //'Private OKString As String = Chr(2) + ":OK:ss:" + Chr(3)
        //'Private ERString As String = Chr(2) + ":ER:ss:" + Chr(3)
        private void SerialReceiveData (System.Object sender, System.EventArgs e) {
            //'Dim RcvEnd As Boolean = False
            bool checksum = true;
            //Debug.Log (checksum);
            Debug.Log ("data receive");
            if (DataTrsmt == false) return;
            try {
                Rs.ReadTimeout = 1000;
                RcvData = Rs.ReadTo (STX.ToString ());
                RcvData = Rs.ReadTo (ETX.ToString ());
                //'RcvData = Rs.ReadExisting()
            } catch (IOException) {
                //MessageBox.Show("受信失敗です");
                return;
            }

            /* チェックサムの確認 */
            checksum = CheckCheckSum (" " + RcvData + " ");//STX,ETXのかわりにスペースを付加
                                                           //checksum = CheckCheckSum(RcvData);
            if (checksum == false) {
                return;
            }

            /* センサデータの抽出 */
            /* C#では先頭は0～、VBでは先頭は1～ */
            int[] SnsData = new int[20];
            /* 加速度 */
            SnsData[1] = int.Parse (RcvData.Substring (1, 5)); //'Acc
            SnsData[2] = int.Parse (RcvData.Substring (7, 5));
            SnsData[3] = int.Parse (RcvData.Substring (13, 5));
            /* 角速度 */
            SnsData[4] = int.Parse (RcvData.Substring (19, 5)); //'Gyr 
            SnsData[5] = int.Parse (RcvData.Substring (25, 5));
            SnsData[6] = int.Parse (RcvData.Substring (31, 5));
            /*磁気*/
            SnsData[7] = int.Parse (RcvData.Substring (38, 4));//'cmp
            if (RcvData.Substring (37, 1) == "-") SnsData[7] = SnsData[7] * -1;
            SnsData[8] = int.Parse (RcvData.Substring (44, 4));
            if (RcvData.Substring (43, 1) == "-") SnsData[8] = SnsData[8] * -1;
            SnsData[9] = int.Parse (RcvData.Substring (50, 4));
            if (RcvData.Substring (49, 1) == "-") SnsData[9] = SnsData[9] * -1;
            /* 圧力 */
            SnsData[10] = int.Parse (RcvData.Substring (55, 4));//'right
            SnsData[11] = int.Parse (RcvData.Substring (60, 4));
            SnsData[12] = int.Parse (RcvData.Substring (65, 4));
            SnsData[13] = int.Parse (RcvData.Substring (70, 4));
            SnsData[14] = int.Parse (RcvData.Substring (75, 4));
            SnsData[15] = int.Parse (RcvData.Substring (80, 4)); //'left
            SnsData[16] = int.Parse (RcvData.Substring (85, 4));
            SnsData[17] = int.Parse (RcvData.Substring (90, 4));
            SnsData[18] = int.Parse (RcvData.Substring (95, 4));
            SnsData[19] = int.Parse (RcvData.Substring (100, 4));
            /* イベント発生 */
            DataEvent (SnsData[1], SnsData[2], SnsData[3],
                      SnsData[4], SnsData[5], SnsData[6],
                      SnsData[7], SnsData[8], SnsData[9],
                      SnsData[10], SnsData[11], SnsData[12], SnsData[13], SnsData[14],
                      SnsData[15], SnsData[16], SnsData[17], SnsData[18], SnsData[19]
                      ); //'RcvData _
        }
        #endregion
        #region 公開関数群
        /*---------------------------*/
        //' サービスの開始
        /*---------------------------*/
        public int DllStart (string PortName, int BaudRate, int DataLength, int Parity, int SBits) {
            if (DllIsStart == true) {
                return (int)RetVal.Started;
            }
            try {
                Rs.PortName = PortName;
                Rs.BaudRate = BaudRate;
                Rs.DataBits = DataLength;
                //Rs.Parity = Parity;
                //{
                //}
                //Rs.Parity = Parity;
                //Rs.StopBits = SBits;
                Rs.Open ();
                Debug.Log (Rs.IsOpen);
            } catch (IOException) {
                Debug.Log ("Port check 2");
                //MessageBox(Exceotion.Message);
                return (int)RetVal.ErrEnd;
            }
            DllIsStart = true;
            if (DllIsStart == true) Debug.Log ("Port open check");

            return (int)RetVal.NoErr;
        }
        /*---------------------------*/
        //' サービスの終了
        /*---------------------------*/
        public int Dllend () {
            if (DllIsStart == false) {
                return (int)RetVal.NoStart;
            }
            try {
                Rs.Close ();//シリアルポートのクローズ
            } catch (IOException) {
                return (int)RetVal.ErrEnd;
            }
            Debug.Log ("end");
            DllIsStart = false;
            Rs.DataReceived -= new SerialDataReceivedEventHandler (SerialReceiveData);
            return (int)RetVal.NoErr;
        }
        /*---------------------------*/
        //'データ取得開始
        /*---------------------------*/
        private string DataGetStartCmnd = STX + ":LS:ss:" + ETX;
        public int DataGetStart () {
            //if(DllIsStart == false)  return (int)RetVal.NoStart;
            string cmnd = DataGetStartCmnd;
            //string Rcv;
            cmnd = SetCheckSum (cmnd);
            Debug.Log (" check ");
            try {
                //Rcv = Rs.ReadExisting(); //'バッファのクリア
                Debug.Log ("command");
                SerialSendData (cmnd);
            } catch (IOException) {
                return (int)RetVal.ErrEnd;
            }

            DataTrsmt = true;
            return (int)RetVal.NoErr;
        }
        /*---------------------------*/
        //'データ取得終了
        /*---------------------------*/
        private string DataGetEndCmnd = STX + ":LE:ss:" + ETX;
        public int DataGetEnd () {
            if (DllIsStart == false) return (int)RetVal.NoStart;
            string cmnd = DataGetEndCmnd;
            //'Dim Rcv As String
            cmnd = SetCheckSum (cmnd);
            try {
                SerialSendData (cmnd);
            } catch (IOException) {
                return (int)RetVal.ErrEnd;
            }

            DataTrsmt = false;
            return (int)RetVal.NoErr;
        }

        /*---------------------------*/
        //'センサ感度設定
        /*---------------------------*/
        private string SensorSetup = STX + ":DR:p:p:p:ss:" + ETX;
        //									 
        public int SensorDetectionRangeSetup (int AccSetup, int GyrSetup, int CmpSetup) {
            if (DllIsStart == false) return (int)RetVal.NoStart;
            if (DataTrsmt == true) return (int)RetVal.ErrEnd;
            string cmnd = SensorSetup;
            //8/28:kanndo:3,3,0
            //Mid(cmnd, 6, 1) = Trim(Str(AccSetup));

            //Mid(cmnd, 8, 1) = Trim(Str(GyrSetup));

            //Mid(cmnd, 10, 1) = Trim(Str(CmpSetup));

            cmnd = SetCheckSum (cmnd);
            try {
                SerialSendData (cmnd);
            } catch (IOException) {
                return (int)RetVal.ErrEnd;
            }
            return (int)RetVal.NoErr;
        }
        #endregion
    }
}
