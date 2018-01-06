using UnityEngine;
using System.Collections;

using System.IO.Ports; 
using System.IO;

using System;

using System.Threading;//スレッド用

public class CapsuleBehaviour_mayukko_flight : MonoBehaviour {
	// Use this for initialization
	Thread thread;// = new Thread();//(new ThreadStart(tanasaki));//スレッド
	bool tushin = false;//スレッド用
	//public static string data;
	public static float ax,ay,az,gx,gy,gz,cx,cy,cz;
	public static int fl1,fl2,fl3,fl4,fl5,fr1,fr2,fr3,fr4,fr5;
	public static float pitch,roll,yaw;
	public float pitch_m,roll_m,yaw_m;
	public static int flag;
	public int gui_set;
	public float pitch0;
	void Start () {
		gui_set=0;

	}
	
	// Update is called once per frame
	void Update () {


		if(Application.loadedLevelName=="game_end")Destroy(gameObject);
		DontDestroyOnLoad (this.gameObject);
		if(Application.loadedLevelName=="mini_game")return;
		if (Input.GetKeyDown(KeyCode.C)){
			FileStream cf = new FileStream("Assets/config.txt", FileMode.Create, FileAccess.Write);
			StreamWriter writer3 = new StreamWriter(cf);
			writer3.WriteLine(gx); writer3.WriteLine(gy); writer3.WriteLine(gz);
			writer3.WriteLine(cx); writer3.WriteLine(cy); writer3.WriteLine(cz);
			writer3.Close();
			Debug.Log("config file create");
		}
		string c1,c2,c3;
		float cgx=0, cgy=0, cgz=0;
		float gx1,gy1,gz1;
		FileStream config = new FileStream("Assets/config.txt", FileMode.Open, FileAccess.Read);
		StreamReader reader3 = new StreamReader(config);
		c1 = reader3.ReadLine();
		c2 = reader3.ReadLine();
		c3 = reader3.ReadLine();
		cgx = float.Parse(c1);
		cgy = float.Parse(c2);
		cgz = float.Parse(c3);
		gx1=gx-cgx;gy1=gy-cgy;gz1=gz-cgz;
		reader3.Close();

		Vector3 temp;
		Vector3 mayumi;

		temp.x=0;
		temp.y=180;
		temp.z=90;
		transform.eulerAngles = temp;
		//training
		if(az>ay)pitch = ay*(float)0.045;
		else pitch = (2800-az)*(float)0.035;
		roll=ax*(float)0.045;
		transform.rotation=Quaternion.AngleAxis(pitch,Vector3.left)*transform.rotation;
		transform.rotation=Quaternion.AngleAxis(roll,Vector3.forward)*transform.rotation;
		mayumi.x=-roll/100;
		mayumi.y=(float)1;
		mayumi.z=-(float)9.3;
		transform.position= mayumi;
		if(fl1==0&&fl2==0&&fl3==0&&fl4==0&&fl5==0&&fr1==0&&fr2==0&&fr3==0&&fr4==0&&fr5==0)flag=0;

		if(Input.GetKey(KeyCode.W))gui_set=0;
		if(Input.GetKey(KeyCode.E))gui_set=1;
		if(Input.GetKey(KeyCode.R))gui_set=2;

		//start 
		if(DllIsStart != true){
			DllStart("COM3",38400,8,0,1);
			Debug.Log ("dllStart");
		}else if(DllIsStart==true && DataTrsmt != true){
			DataGetStart();
			Debug.Log("getStart");
			thread = new Thread(new ThreadStart(Recieveloop));//スレッド
			tushin = true;
			thread.Start();
		}
	}

	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		GUI.color = Color.black;
		if(gui_set==2){
		GUI.Label(new Rect(sw-120,10,120,20),"accx = "+ax.ToString());
		GUI.Box(new Rect(sw/2-200,sh/2,10,-ax/10),"");
		GUI.Label(new Rect(sw-120,30,120,20),"accy = "+ay.ToString());
		GUI.Box(new Rect(sw/2-170,sh/2,10,-ay/10),"");
		GUI.Label(new Rect(sw-120,50,120,20),"accz = "+az.ToString());
		GUI.Box(new Rect(sw/2-140,sh/2,10,-az/10),"");
		
		GUI.Label(new Rect(sw-120,70,120,20),"gyrx = "+gx.ToString());
		GUI.Box(new Rect(sw/2-30,sh/2,10,-gx/10),"");
		GUI.Label(new Rect(sw-120,90,120,20),"gyry = "+gy.ToString());
		GUI.Box(new Rect(sw/2,sh/2,10,-gy/10),"");
		GUI.Label(new Rect(sw-120,110,120,20),"gyrz = "+gz.ToString());
		GUI.Box(new Rect(sw/2+30,sh/2,10,-gz/10),"");
		
		GUI.Label(new Rect(sw-120,130,120,20),"cmpx = "+cx.ToString());
		GUI.Box(new Rect(sw/2+140,sh/2,10,-cx),"");
		GUI.Label(new Rect(sw-120,150,120,20),"cmpy = "+cy.ToString());
		GUI.Box(new Rect(sw/2+170,sh/2,10,-cy),"");
		GUI.Label(new Rect(sw-120,170,120,20),"cmpz = "+cz.ToString());
		GUI.Box(new Rect(sw/2+200,sh/2,10,-cz),"");
		
		GUI.Label(new Rect(sw-120,190,120,20),"fingL1 = "+fl5.ToString());
		GUI.Box(new Rect(sw/2-200,sh-10,10,-fl5/10),"");
		GUI.Label(new Rect(sw-120,210,120,20),"fingL2 = "+fl4.ToString());
		GUI.Box(new Rect(sw/2-170,sh-10,10,-fl4/10),"");
		GUI.Label(new Rect(sw-120,230,120,20),"fingL3 = "+fl3.ToString());
		GUI.Box(new Rect(sw/2-140,sh-10,10,-fl3/10),"");
		GUI.Label(new Rect(sw-120,250,120,20),"fingL4 = "+fl2.ToString());
		GUI.Box(new Rect(sw/2-110,sh-10,10,-fl2/10),"");
		GUI.Label(new Rect(sw-120,270,120,20),"fingL5 = "+fl1.ToString());
		GUI.Box(new Rect(sw/2-80,sh-10,10,-fl1/10),"");
		GUI.Label(new Rect(sw-120,290,120,20),"fingR1 = "+fr1.ToString());
		GUI.Box(new Rect(sw/2+80,sh-10,10,-fr1/10),"");
		GUI.Label(new Rect(sw-120,310,120,20),"fingR2 = "+fr2.ToString());
		GUI.Box(new Rect(sw/2+110,sh-10,10,-fr2/10),"");
		GUI.Label(new Rect(sw-120,330,120,20),"fingR3 = "+fr3.ToString());
		GUI.Box(new Rect(sw/2+140,sh-10,10,-fr3/10),"");
		GUI.Label(new Rect(sw-120,350,120,20),"fingR4 = "+fr4.ToString());
		GUI.Box(new Rect(sw/2+170,sh-10,10,-fr4/10),"");
		GUI.Label(new Rect(sw-120,370,120,20),"fingR5 = "+fr5.ToString());
		GUI.Box(new Rect(sw/2+200,sh-10,10,-fr5/10),"");
		
		//GUI.Label(new Rect(sw-120,10,120,20),"accx = "+ax.ToString());
		GUI.Label(new Rect(sw-120,400,120,20),"pitch = "+pitch.ToString());
		GUI.Label(new Rect(sw-120,430,120,20),"roll = "+roll.ToString());
		GUI.Label(new Rect(sw-120,460,120,20),"yaw = "+yaw.ToString());
		}
		//****************service start***********************
		if(gui_set>0){
			Rect rect = new Rect(sw-160, 0, 40, 30);
			bool isClicked_start = GUI.Button(rect, "STA");
			if (isClicked_start){
			//if (Input.GetKeyDown(KeyCode.S)){
				DllStart("COM3",38400,8,0,1);
				//SensorDetectionRangeSetup(0,0,0);
				Debug.Log ("dllStart");
			}if(DllIsStart == true)GUI.Box(rect,"OK");
			//*****************service stop***********************
			Rect rect1 = new Rect (sw-120,0,40,30);
			bool isClicked_stop = GUI.Button(rect1, "STO");
			if (isClicked_stop){
			//if(Input.GetKeyDown(KeyCode.D)){
				Dllend();
				Debug.Log("dllEnd");
			}
			//*******************get start*****************************
			Rect rect2 = new Rect (sw-80,0,40,30);
			bool isClicked_get_start = GUI.Button(rect2, "GET");
			if (isClicked_get_start){
			//if (Input.GetKeyDown(KeyCode.G)){
				DataGetStart();
				Debug.Log("getStart");
				//Thread thread = new Thread(new ThreadStart(tanasaki));//スレッド
				thread = new Thread(new ThreadStart(Recieveloop));//スレッド
				tushin = true;
				thread.Start();
				gui_set=0;
			}if(DataTrsmt==true) GUI.Box(rect2,"開始");
			//**********************get end*************************
			Rect rect3 = new Rect (sw-40,0,40,30);
			bool isClicked_get_end = GUI.Button(rect3, "END");
			if (isClicked_get_end){
			//if (Input.GetKeyDown(KeyCode.F)){
				DataGetEnd();
				tushin = false;
				Debug.Log("getEnd");
			}
		}
	}
	
	void OnApplicationQuit(){
		Debug.Log ("APPEND");
		tushin = false;
		Resources.UnloadUnusedAssets();
		Destroy (this.gameObject);
	}

	public static void SensorData(ref int accx,ref int accy,ref int accz,ref int gyrx,ref int gyry,ref int gyrz,ref int cmpx,ref int cmpy,ref int cmpz,ref int fing1,ref int fing2,ref int fing3,ref int fing4,ref int fing5,ref int fing6,ref int fing7,ref int fing8,ref int fing9,ref int fing10)
	{
		//data = accx.ToString() + "," + accy.ToString() + "," + accz.ToString() + "," + gyrx.ToString() + "," + gyry.ToString() + "," + gyrz.ToString() + "," + cmpx.ToString() + "," + cmpy.ToString() + "," + cmpz.ToString() + "\r\n";

		float ax0,ay0,az0,gx0,gy0,gz0,cx0,cy0,cz0;
        ax0=ax;ay0=ay;az0=az;gx0=gx;gy0=gy;gz0=gz;cx0=cx;cy0=cy;cz0=cz;
		if(accx<5000)ax=ax0*(float)0.8+accx*(float)0.2;else if(accx>60000)ax=ax0*(float)0.8+(accx-65535)*(float)0.2;
		if(accy<5000)ay=ay0*(float)0.8+accy*(float)0.2;else if(accy>60000)ay=ay0*(float)0.8+(accy-65535)*(float)0.2;
		if(accz<5000)az=az0*(float)0.8+accz*(float)0.2;else if(accz>60000)az=az0*(float)0.8+(accz-65535)*(float)0.2;
		if(gyrx<5000)gx=gx0*(float)0.8+gyrx*(float)0.2;else if(gyrx>60000)gx=gx0*(float)0.8+(gyrx-65535)*(float)0.2;
		if(gyry<5000)gy=gy0*(float)0.8+gyry*(float)0.2;else if(gyry>60000)gy=gy0*(float)0.8+(gyry-65535)*(float)0.2;
		if(gyrz<5000)gz=gz0*(float)0.8+gyrz*(float)0.2;else if(gyrz>60000)gz=gz0*(float)0.8+(gyrz-65535)*(float)0.2;
		if(cmpx<5000)cx=cx0*(float)0.8+cmpx*(float)0.2;else if(cmpx>60000)cx=cx0*(float)0.8+(cmpx-65535)*(float)0.2;
		if(cmpy<5000)cy=cy0*(float)0.8+cmpy*(float)0.2;else if(cmpy>60000)cy=cy0*(float)0.8+(cmpy-65535)*(float)0.2;
		if(cmpz<5000)cz=cz0*(float)0.8+cmpz*(float)0.2;else if(cmpz>60000)cz=cz0*(float)0.8+(cmpz-65535)*(float)0.2;
		fr5=fing1;fr4=fing2;fr3=fing3;fr2=fing4;fr1=fing5;fl1=fing6;fl2=fing7;fl3=fing8;fl4=fing9;fl5=fing10;
	}

	public delegate void DataEventEventHandler(int AccX,int AccY, int AccZ, 
	                                           int GyrX, int GyrY, int GyrZ,
	                                           int CmpX, int CmpY, int Cmp, 
	                                           int ThumbR, int fing1R, int fing2R, int fing3R, int fing4R, 
	                                           int ThumbL, int fing1L, int fing2L, int fing3L, int fing4L  
	                                           ); //'Byval rcvstr as string)
	
	public event DataEventEventHandler DataEvent;
	//public event SerialDataReceivedEventHandler DataReceive;
	
	public SerialPort Rs = new SerialPort();
	private bool DllIsStart = false;
	private bool DataTrsmt = false;
	private bool RecoedRecieve = false;
	//' 戻り値
	public enum RetVal
	{
		NoErr = 0,       //'正常終了
		ErrEnd = -1,     //'エラー終了
		NoStart = -2,    //'サービスが開始されていない
		Started = -3,    //'サービスは既に開始されている
		ParaErr = -4,    //'パラメータエラー
		TimOv = -5,      //'通信タイムアウト
		CkSmErr = -6    //'チェックサムエラー
	}
	public struct LogData
	{
		public int acc_x, acc_y, acc_z;
		public int gyr_x, gyr_y, gyr_z;
		public int mag_x, mag_y, mag_z;
		public int fing_1, fing_2, fing_3, fing_4, fing_5, fing_6, fing_7, fing_8, fing_9, fing_10;
	}
	private LogData[] Memdata = new LogData[200];
	private const char STX = (char)0x02;
	private const char ETX = (char)0x03;
	private const char ACK = (char)0x06;
	private const char NACK = (char)0x15;
	//'-----------------------------------------------------------------------------
	//'公開関数群
	//'-----------------------------------------------------------------------------
	/*---------------------------*/
	//' サービスの開始
	/*---------------------------*/
	public int DllStart(string PortName, int BaudRate, int DataLength, int Parity, int SBits)
	{
		if (DllIsStart == true) return (int)RetVal.Started;
		try
		{
			Rs.PortName = PortName;
			Rs.BaudRate = BaudRate;
			Rs.DataBits = DataLength;
			Rs.Open();
			/*受信スレッド作成*/
			thread = new Thread(new ThreadStart(Recieveloop));//スレッド
			tushin = true;
			thread.Start();
			//Debug.Log(Rs.IsOpen);
		}
		catch (IOException)
		{
			//Debug.Log ("Port check 2");
			//MessageBox(Exceotion.Message);
			return (int)RetVal.ErrEnd;
		}
		DllIsStart = true;
		return (int)RetVal.NoErr;
	}
	/*---------------------------*/
	//' サービスの終了
	/*---------------------------*/
	public int Dllend()
	{
		if (DllIsStart == false) return (int)RetVal.NoStart;
		try
		{
			Rs.Close();//シリアルポートのクローズ
			thread.Suspend();//スレッドの停止と削除
			DllIsStart = false;
		}
		catch (IOException)
		{
			return (int)RetVal.ErrEnd;
		}
		//Debug.Log("end");
		return (int)RetVal.NoErr;
	}
	/*---------------------------*/
	//'データ取得開始
	/*---------------------------*/
	private string DataGetStartCmnd = STX + ":LS:ss:" + ETX;
	public int DataGetStart()
	{
		if(DllIsStart == false)  return (int)RetVal.NoStart;
		
		string cmnd = DataGetStartCmnd;
		cmnd = SetCheckSum(cmnd);
		//Debug.Log(" check ");
		try
		{
			//Rcv = Rs.ReadExisting(); //'バッファのクリア
			//Debug.Log("command");
			SerialSendData(cmnd);
		}
		catch (IOException)
		{
			return (int)RetVal.ErrEnd;
		}
		
		DataTrsmt = true;
		return (int)RetVal.NoErr;
	}
	/*---------------------------*/
	//'データ取得終了
	/*---------------------------*/
	private string DataGetEndCmnd = STX + ":LE:ss:" + ETX;
	public int DataGetEnd()
	{
		if (DllIsStart == false) return (int)RetVal.NoStart;
		string cmnd = DataGetEndCmnd;
		//'Dim Rcv As String
		cmnd = SetCheckSum(cmnd);
		try
		{
			SerialSendData(cmnd);
		}
		catch (IOException)
		{
			return (int)RetVal.ErrEnd;
		}
		DataTrsmt = false;
		return (int)RetVal.NoErr;
	}
	/*---------------------------*/
	//'センサ感度設定
	/*---------------------------*/
	private string SensorSetup = STX + ":DR:a:g:m:ss:" + ETX;
	public int SensorDetectionRangeSetup(int AccSetup, int GyrSetup, int CmpSetup)
	{
		if (DllIsStart == false) return (int)RetVal.NoStart;
		if (DataTrsmt == true) return (int)RetVal.ErrEnd;
		string cmnd = SensorSetup;
		cmnd = cmnd.Replace("a",AccSetup.ToString());
		cmnd = cmnd.Replace("g", GyrSetup.ToString());
		cmnd = SetCheckSum(cmnd);
		try
		{
			SerialSendData(cmnd);
		}
		catch (IOException)
		{
			return (int)RetVal.ErrEnd;
		}
		return (int)RetVal.NoErr;
	}
	/*---------------------------*/
	//'記録開始
	/*---------------------------*/
	private string SensorMemStart = STX + ":MW:ss:" + ETX;
	public int SensorRecordStart()
	{
		if (DllIsStart == false) return (int)RetVal.NoStart;
		string cmnd = SensorMemStart;
		cmnd = SetCheckSum(cmnd);
		try
		{
			SerialSendData(cmnd);
		}
		catch (IOException)
		{
			return (int)RetVal.ErrEnd;
		}
		return (int)RetVal.NoErr;
	}
	/*---------------------------*/
	//'記録取得
	/*---------------------------*/
	private string SensorMemGet = STX + ":MG:ss:" + ETX;
	public int SensorRecordGet()
	{
		if (DllIsStart == false) return (int)RetVal.NoStart;
		string cmnd = SensorMemGet;
		cmnd = SetCheckSum(cmnd);
		try
		{
			SerialSendData(cmnd);
			RecoedRecieve = true;
		}
		catch (IOException)
		{
			return (int)RetVal.ErrEnd;
		}
		return (int)RetVal.NoErr;
	}
	//'-----------------------------------------------------------------------------
	//'非公開共通制御群
	//'-----------------------------------------------------------------------------
	/*---------------------------*/
	//'受信処理（スレッド）
	/*---------------------------*/
	private void Recieveloop()
	{
		string RcvData = "";/*受信した一ブロックのデータ*/
		//string RcvBuff = "";/*受信バッファ*/
		bool check;         /*チェックサム*/
		string[] log = new string[200];
		int count = 0;
		int headpos;
		
		//Debug.Log("Recieveloop");
		while(tushin) 
		{
			//Debug.Log(Rs.ReadChar ());
			check = false;
			Rs.ReadTimeout = 1000;
			try
			{
			RcvData = Rs.ReadTo (STX.ToString());
			RcvData = Rs.ReadTo (ETX.ToString());
			//check = CheckCheckSum (" "+ RcvData + " ");
			}
			catch(TimeoutException e)
			{
			}

				check = CheckCheckSum(" " + RcvData + " ");
				
				if (RecoedRecieve == true)
				{
					if(RcvData == ":END:")
					{
						RecoedRecieve = false;
						int i = 0,j = 0;
						int Rcvid;
						int dataid = 0;
						for (Rcvid = 0; Rcvid < count; Rcvid++)//1ブロック数処理する
						{
							for (j = 0; j < 3; j++)//1ブロックからデータを分割する。
							{
								headpos = j * 104 + 1;
								if (log[Rcvid] != "")
								{
									if (dataid < 200)
									{
										Memdata[dataid].acc_x = int.Parse(log[Rcvid].Substring(headpos, 5)); //'Acc
										Memdata[dataid].acc_y = int.Parse(log[Rcvid].Substring(headpos + 6, 5));
										Memdata[dataid].acc_z = int.Parse(log[Rcvid].Substring(headpos + 12, 5));
										/* 角速度 */
										Memdata[dataid].gyr_x = int.Parse(log[Rcvid].Substring(headpos + 18, 5)); //'Gyr 
										Memdata[dataid].gyr_y = int.Parse(log[Rcvid].Substring(headpos + 24, 5));
										Memdata[dataid].gyr_z = int.Parse(log[Rcvid].Substring(headpos + 30, 5));
										/*磁気*/
										Memdata[dataid].mag_x = int.Parse(log[Rcvid].Substring(headpos + 37, 4));//'cmp
										if (log[Rcvid].Substring(headpos + 36, 1) == "-") Memdata[dataid].mag_x = Memdata[dataid].mag_x * -1;
										Memdata[dataid].mag_y = int.Parse(log[Rcvid].Substring(headpos + 43, 4));
										if (log[Rcvid].Substring(headpos + 42, 1) == "-") Memdata[dataid].mag_y = Memdata[dataid].mag_y * -1;
										Memdata[dataid].mag_z = int.Parse(log[Rcvid].Substring(headpos + 49, 4));
										if (log[Rcvid].Substring(headpos + 48, 1) == "-") Memdata[dataid].mag_z = Memdata[dataid].mag_z * -1;
										/* 圧力 */
										Memdata[dataid].fing_1 = int.Parse(log[Rcvid].Substring(headpos + 54, 4));//'right
										Memdata[dataid].fing_2 = int.Parse(log[Rcvid].Substring(headpos + 59, 4));
										Memdata[dataid].fing_3 = int.Parse(log[Rcvid].Substring(headpos + 64, 4));
										Memdata[dataid].fing_4 = int.Parse(log[Rcvid].Substring(headpos + 69, 4));
										Memdata[dataid].fing_5 = int.Parse(log[Rcvid].Substring(headpos + 74, 4));
										Memdata[dataid].fing_6 = int.Parse(log[Rcvid].Substring(headpos + 79, 4)); //'left
										Memdata[dataid].fing_7 = int.Parse(log[Rcvid].Substring(headpos + 84, 4));
										Memdata[dataid].fing_8 = int.Parse(log[Rcvid].Substring(headpos + 89, 4));
										Memdata[dataid].fing_9 = int.Parse(log[Rcvid].Substring(headpos + 94, 4));
										Memdata[dataid].fing_10 = int.Parse(log[Rcvid].Substring(headpos + 99, 4));
										dataid++;
									}
								}
							}
						}
						
						for (i = dataid; i < 200; i++)
						{
							Memdata[dataid].acc_x = 0; //'Acc
							Memdata[dataid].acc_y = 0;
							Memdata[dataid].acc_z = 0;
							/* 角速度 */
							Memdata[dataid].gyr_x = 0; //'Gyr 
							Memdata[dataid].gyr_y = 0;
							Memdata[dataid].gyr_z = 0;
							/*磁気*/
							Memdata[dataid].mag_x = 0;//'cmp
							Memdata[dataid].mag_y = 0;
							Memdata[dataid].mag_z = 0;
							/* 圧力 */
							Memdata[dataid].fing_1 = 0;//'right
							Memdata[dataid].fing_2 = 0;
							Memdata[dataid].fing_3 = 0;
							Memdata[dataid].fing_4 = 0;
							Memdata[dataid].fing_5 = 0;
							Memdata[dataid].fing_6 = 0; //'left
							Memdata[dataid].fing_7 = 0;
							Memdata[dataid].fing_8 = 0;
							Memdata[dataid].fing_9 = 0;
							Memdata[dataid].fing_10 = 0;
						}
						count = 0;
						
						//logdataSave();
					}
					else if(check)/*データ受信と応答*/
					{
						log[count] = RcvData;
						count++;
						SerialSendData(ACK.ToString());
					}
					else
					{
						SerialSendData(NACK.ToString());
					}
				}
				else
				{
					if (check)/*データの表示*/
					{
						int[] SnsData = new int[20];
						/* 加速度 */
						SnsData[1] = int.Parse(RcvData.Substring(1, 5)); //'Acc
						SnsData[2] = int.Parse(RcvData.Substring(7, 5));
						SnsData[3] = int.Parse(RcvData.Substring(13, 5));
						/* 角速度 */
						SnsData[4] = int.Parse(RcvData.Substring(19, 5)); //'Gyr 
						SnsData[5] = int.Parse(RcvData.Substring(25, 5));
						SnsData[6] = int.Parse(RcvData.Substring(31, 5));
						/*磁気*/
						SnsData[7] = int.Parse(RcvData.Substring(38, 4));//'cmp
						if (RcvData.Substring(37, 1) == "-") SnsData[7] = SnsData[7] * -1;
						SnsData[8] = int.Parse(RcvData.Substring(44, 4));
						if (RcvData.Substring(43, 1) == "-") SnsData[8] = SnsData[8] * -1;
						SnsData[9] = int.Parse(RcvData.Substring(50, 4));
						if (RcvData.Substring(49, 1) == "-") SnsData[9] = SnsData[9] * -1;
						/* 圧力 */
						SnsData[10] = int.Parse(RcvData.Substring(55, 4));//'right
						SnsData[11] = int.Parse(RcvData.Substring(60, 4));
						SnsData[12] = int.Parse(RcvData.Substring(65, 4));
						SnsData[13] = int.Parse(RcvData.Substring(70, 4));
						SnsData[14] = int.Parse(RcvData.Substring(75, 4));
						SnsData[15] = int.Parse(RcvData.Substring(80, 4)); //'left
						SnsData[16] = int.Parse(RcvData.Substring(85, 4));
						SnsData[17] = int.Parse(RcvData.Substring(90, 4));
						SnsData[18] = int.Parse(RcvData.Substring(95, 4));
						SnsData[19] = int.Parse(RcvData.Substring(100, 4));
						/*データ表示*/
						SensorData(ref SnsData[1], ref SnsData[2], ref SnsData[3], ref SnsData[4], ref SnsData[5], ref SnsData[6], ref SnsData[7], ref SnsData[8], ref SnsData[9],
						           ref SnsData[10], ref SnsData[11], ref SnsData[12], ref SnsData[13], ref SnsData[14], ref SnsData[15], ref SnsData[16], ref SnsData[17], ref SnsData[18], ref SnsData[19]);
					}
					else
					{
					}
					
				}//受信内容
			}//受信したら
		}

	/*---------------------------*/
	//'データ送信
	/*---------------------------*/
	private void SerialSendData(string SndData)
	{
		try
		{
			//RcvData = "";
			Rs.Write(SndData);
		}
		catch (IOException)
		{
		}
	}
	/*---------------------------*/
	//'チェックサムの計算
	/*---------------------------*/
	private byte CalcCheckSum(string BaseData)
	{
		int i;
		int len = BaseData.Length;
		int Temp = 0;
		if (len > 0)
		{
			for(i=0;i<len;i++)
			{
				if ((i != 0) && (i != len - 4) && (i != len - 3) && (i != len - 1))
				{
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
	private string SetCheckSum(string cmnd)
	{
		byte Temp;
		string checksum;
		string sumcmnd;
		
		Temp = CalcCheckSum(cmnd);
		checksum = string.Format("{0,2:X}",Temp);
		sumcmnd = cmnd.Replace("ss",checksum);
		return sumcmnd;
	}
	/*---------------------------*/
	//'チェックサムの抽出
	/*---------------------------*/
	private byte GetCheckSum(string cmnd)
	{
		int Temp = 0;
		int Length = cmnd.Length;
		Temp = Change1Value(cmnd[Length - 4]);
		Temp = Temp * 0x10;
		Temp = Temp + Change1Value(cmnd[Length - 3]);
		return (byte)Temp;
	}
	/*---------------------------*/
	//'チェックサムの確認
	/*---------------------------*/
	private bool CheckCheckSum(string BaseData)
	{
		byte Temp1 = 0;
		byte Temp2 = 0;
		Temp1 = CalcCheckSum(BaseData);
		Temp2 = GetCheckSum(BaseData);
		if(Temp1 == Temp2 ) return true;
		return false;
	}
	//未使用
	/*---------------------------*/
	//'数値０～１５に対する１バイトの１６進文字を返す
	/*---------------------------*/
	private string Change1Str(int BaseValue)
	{
		if( BaseValue >= 0 && BaseValue <= 9 )
		{
			//Change1Str = Chr(BaseValue | 0x30);
		}
		else if( BaseValue >= 10 && BaseValue <= 15)
		{
			//Change1Str = Chr((BaseValue - 9) | 0x40);
		}
		else
		{
			return "0";
		}
		return "";
	}
	/*---------------------------*/
	//'文字 "0"～"9"および"A"～"F"にたいする数値０～１５を返す
	/*---------------------------*/
	private byte Change1Value(char BaseValue) 
	{
		byte result;
		if (((int)BaseValue >= 0x30) && ((int)BaseValue <= 0x39))
		{
			result =  (byte)((int)BaseValue & 0x0F);
			return result;
		}
		else if (((int)BaseValue >= 0x41) && ((int)BaseValue <= 0x46))
		{
			result = (byte)((int)BaseValue -7 & 0x0F);
			return result;
		}
		else if (((int)(BaseValue) >= 0x61) && ((int)(BaseValue)) <= 0x66)
		{
			result = (byte)((int)BaseValue - 7 & 0x0F);
			return result;
		}
		else
		{
			return 0;
		}
	}
}