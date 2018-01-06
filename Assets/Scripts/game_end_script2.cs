using UnityEngine;
using System.Collections;
using System.Data;

using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
/* 運動評価 */
using System.IO;/* ファイルの読み込みに */

public class game_end_script2 : MonoBehaviour {
	public int point;
	public int f;
	public GUIStyle style;
	private AudioSource sound01;
	private AudioSource sound02;
	public float timer=0;
	// Use this for initialization
	void Start () {
		//FileStream file = new FileStream("Assets/motion.csv",FileMode.Open,FileAccess.Read);
		Controller.gui_set=0;
		string fname = "";
		int smooth = 0;
		
		fname = "Assets/motion.csv";
		CalcSmooth(fname, ref smooth);
		
		point = smooth;
	
		//point=87;
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		sound01.PlayOneShot(sound01.clip);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 
		if(f==0&&timer>5.5f){
			sound02.PlayOneShot(sound02.clip);
			f=1;
		}
	}

	//Button
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;

		style.fontSize=sh/18;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.UpperLeft;

		Rect field = new Rect(sw/20,sh/8,sw/2,sh/8);
		//if (point == 0)	point = 57;
		GUI.Box(field,"おつかれさま！\n  \n\nまた一緒に\nトレーニングをしようね！\n\n",style);

		//go to mayukko
		Rect rect6 = new Rect(0, 0,150, sh/9);
		bool isClicked_main = GUI.Button(rect6, "はじめにもどる");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("title");
		}

		Rect rect_game = new Rect(sw - 150, sh - sh/8,150, sh/8);
		bool isClicked_game = GUI.Button(rect_game, "トレーニング");
		if (isClicked_game){
			Debug.Log("mini_game!!");
			Application.LoadLevelAsync("mono_idou_tutorial");
		}

		//back to 
		Rect rect_back = new Rect(sw - 300, sh - sh/8, 150, sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("mayukko");
		}

		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevelAsync("mayukko");
		}
	}

	/**********************************************/
	/* 関数一覧 */
	/**********************************************/
	//int CalcSmooth(string FileName, int Smooth);
	//void ReadFile(string fname);
	//void MakeStrokeData();
	//void SearchPeakTop(int DataStart, int DataEnd);
	//void SearchPeakStart(int DataStart, int DataEnd);
	//void SearchPeakEnd(int DataStart, int DataEnd);
	//int CalcStrokeTimeRate();
	//int CalcStrokeTimeVar();
	//int CalcAreaRate();
	//int CalcAreaVar();
	//int CalcstrokeTime();
	
	/**********************************************/
	/* 変数一覧 */
	/**********************************************/
	private struct Motion/* 運動データ */
	{
		private int accel_x;/* 加速度 x軸 */
		public int accel_y;/* 加速度 y軸 */
		public int accel_z;/* 加速度 z軸 */
		public int gyro_x;/* 角速度 x軸 */
		public int gyro_y;/* 角速度 y軸 */
		public int gyro_z;/* 角速度 z軸 */
		public int mag_x;/* 磁気 x軸 */
		public int mag_y;/* 磁気 x軸 */
		public int mag_z;/* 磁気 x軸 */
		//public int press;
		public float Move_x;/* 座標 x軸 */
		public float  Move_y;/* 座標 y軸 */
		public float  MoveLength;/* 解析データ */
	}
	private struct Peak/* ピーク情報 */
	{
		public int PeakTopNo;/* ピーク頂点の配列添え字 */
		public int PeakStartNo;/* ピーク始まりの配列添え字 */
		public int PeakEndNo;/* ピーク終わりの配列添え字 */
	}
	private Motion[] MotionData = new Motion[6500];/* 運動に関する情報 */
	private Peak[] PeakData = new Peak[100];/* @ピークに関する */
	private int[] Block = new int[11];/* 1動作のデータの始まりの添え字 */
	private int MaxPeakNo = 0;/* 全ピーク数 */
	private int MaxDataNo = 0;/* データの全行数*/
	private int[] JadgePara = new int[5];
	
	/* 配点テーブル */
	private int[,] StrokeTimeRateTable = new int[10, 2] /* ストローク時間の平均 */
	{{47,53},/* 20 */
		{45,55},/* 18 */
		{43,57},
		{41,59},
		{39,61},
		{37,63},
		{35,65},
		{33,67},
		{31,69},
		{30,70} 
	};
	private int[,] StrokeTimeVarTable = new int[10, 2] /* ストロークのバラつき */
	{{0,100},/* 20 */
		{101,200},/* 18 */
		{201,300},
		{301,400},
		{401,500},
		{501,600},
		{601,700},
		{701,800},
		{801,900},
		{901,1000} 
	};
	private int[,] CalcAreaRateTable = new int[10, 2] /* 面積の平均 */
	{{47,53},/* 20 */
		{45,55},/* 18 */
		{43,57},
		{41,59},
		{39,61},
		{37,63},
		{35,65},
		{33,67},
		{31,69},
		{30,70} 
	};
	private int[,] CalcAreaVarTable = new int[10, 2] /* 面積のばらつき */
	{{0,200},/* 20 */
		{201,400},/* 18 */
		{401,600},
		{601,800},
		{801,1000},
		{1001,1200},
		{1201,1400},
		{1401,1600},
		{1601,1800},
		{1,2} 
	};
	private int[,] trokeTimeTable = new int[10, 2] /* ストローク時間比較 */
	{{96,104},/* 20 */
		{92,108},/* 18 */
		{88,112},
		{84,116},
		{80,120},
		{76,124},
		{72,128},
		{68,132},
		{64,136},
		{62,140} 
	};
	
	/**********************************************/
	/* スムーズ度の採点 */
	/**********************************************/
	int CalcSmooth(string FileName,  ref int Smooth)
	{
		int i;
		int point = 0;
		//int process = 0;
		
		ReadFile(FileName);/* ファイルの読み込み */
		MakeStrokeData();/* 波形の作成 */
		for (i = 0; i < MaxPeakNo; i++) /* ピーク情報の取得 */
		{
			SearchPeakInfo(Block[i], Block[i +1]-1, i);
		}
		
		/* 各項目の値を計算する */
		JadgePara[0] = CalcStrokeTimeRate();
		JadgePara[1] = CalcStrokeTimeVar();
		JadgePara[2] = CalcAreaRate();
		JadgePara[3] = CalcAreaVar();
		JadgePara[4] = CalcstrokeTime();
		
		/* 点数化する */
		/* ストローク時間の平均値 */
		if (StrokeTimeRateTable[0, 0] <= JadgePara[0] && JadgePara[0] <= StrokeTimeRateTable[0, 1])
		{
			point += 20;
		}
		else if (StrokeTimeRateTable[1, 0] <= JadgePara[0] && JadgePara[0] <= StrokeTimeRateTable[1, 1])
		{
			point += 18;
		}
		else if (StrokeTimeRateTable[2, 0] <= JadgePara[0] && JadgePara[0] <= StrokeTimeRateTable[2, 1])
		{
			point += 16;
		}
		else if (StrokeTimeRateTable[3, 0] <= JadgePara[0] && JadgePara[0] <= StrokeTimeRateTable[3, 1])
		{
			point += 14;
		}
		else if (StrokeTimeRateTable[4, 0] <= JadgePara[0] && JadgePara[0] <= StrokeTimeRateTable[4, 1])
		{
			point += 12;
		}
		else if (StrokeTimeRateTable[5, 0] <= JadgePara[0] && JadgePara[0] <= StrokeTimeRateTable[5, 1])
		{
			point += 10;
		}
		else if (StrokeTimeRateTable[6, 0] <= JadgePara[0] && JadgePara[0] <= StrokeTimeRateTable[6, 1])
		{
			point += 8;
		}
		else if (StrokeTimeRateTable[7, 0] <= JadgePara[0] && JadgePara[0] <= StrokeTimeRateTable[7, 1])
		{
			point += 6;
		}
		else if (StrokeTimeRateTable[8, 0] <= JadgePara[0] && JadgePara[0] <= StrokeTimeRateTable[8, 1])
		{
			point += 4;
		}
		else
		{
			point += 2;
		}
		
		/* ストローク時間のバラつき */
		if (StrokeTimeVarTable[0, 0] <= JadgePara[1] && JadgePara[1] <= StrokeTimeVarTable[0, 1])
		{
			point += 20;
		}
		else if (StrokeTimeVarTable[1, 0] <= JadgePara[1] && JadgePara[1] <= StrokeTimeVarTable[1, 1])
		{
			point += 18;
		}
		else if (StrokeTimeVarTable[2, 0] <= JadgePara[1] && JadgePara[1] <= StrokeTimeVarTable[2, 1])
		{
			point += 16;
		}
		else if (StrokeTimeVarTable[3, 0] <= JadgePara[1] && JadgePara[1] <= StrokeTimeVarTable[3, 1])
		{
			point += 14;
		}
		else if (StrokeTimeVarTable[4, 0] <= JadgePara[1] && JadgePara[1] <= StrokeTimeVarTable[4, 1])
		{
			point += 12;
		}
		else if (StrokeTimeVarTable[5, 0] <= JadgePara[1] && JadgePara[1] <= StrokeTimeVarTable[5, 1])
		{
			point += 10;
		}
		else if (StrokeTimeVarTable[6, 0] <= JadgePara[1] && JadgePara[1] <= StrokeTimeVarTable[6, 1])
		{
			point += 8;
		}
		else if (StrokeTimeVarTable[7, 0] <= JadgePara[1] && JadgePara[1] <= StrokeTimeVarTable[7, 1])
		{
			point += 6;
		}
		else if (StrokeTimeVarTable[8, 0] <= JadgePara[1] && JadgePara[1] <= StrokeTimeVarTable[8, 1])
		{
			point += 4;
		}
		else
		{
			point += 2;
		}
		/* 面積の平均値 */
		if (CalcAreaRateTable[0, 0] <= JadgePara[2] && JadgePara[2] <= CalcAreaRateTable[0, 1])
		{
			point += 20;
		}
		else if (CalcAreaRateTable[1, 0] <= JadgePara[2] && JadgePara[2] <= CalcAreaRateTable[1, 1])
		{
			point += 18;
		}
		else if (CalcAreaRateTable[2, 0] <= JadgePara[2] && JadgePara[2] <= CalcAreaRateTable[2, 1])
		{
			point += 16;
		}
		else if (CalcAreaRateTable[3, 0] <= JadgePara[2] && JadgePara[2] <= CalcAreaRateTable[3, 1])
		{
			point += 14;
		}
		else if (CalcAreaRateTable[4, 0] <= JadgePara[2] && JadgePara[2] <= CalcAreaRateTable[4, 1])
		{
			point += 12;
		}
		else if (CalcAreaRateTable[5, 0] <= JadgePara[2] && JadgePara[2] <= CalcAreaRateTable[5, 1])
		{
			point += 10;
		}
		else if (CalcAreaRateTable[6, 0] <= JadgePara[2] && JadgePara[2] <= CalcAreaRateTable[6, 1])
		{
			point += 8;
		}
		else if (CalcAreaRateTable[7, 0] <= JadgePara[2] && JadgePara[2] <= CalcAreaRateTable[7, 1])
		{
			point += 6;
		}
		else if (CalcAreaRateTable[8, 0] <= JadgePara[2] && JadgePara[2] <= CalcAreaRateTable[8, 1])
		{
			point += 4;
		}
		else
		{
			point += 2;
		}
		/* 面積のバラつき */
		if (CalcAreaVarTable[0, 0] <= JadgePara[3] && JadgePara[3] <= CalcAreaVarTable[0, 1])
		{
			point += 20;
		}
		else if (CalcAreaVarTable[1, 0] <= JadgePara[3] && JadgePara[3] <= CalcAreaVarTable[1, 1])
		{
			point += 18;
		}
		else if (CalcAreaVarTable[2, 0] <= JadgePara[3] && JadgePara[3] <= CalcAreaVarTable[2, 1])
		{
			point += 16;
		}
		else if (CalcAreaVarTable[3, 0] <= JadgePara[3] && JadgePara[3] <= CalcAreaVarTable[3, 1])
		{
			point += 14;
		}
		else if (CalcAreaVarTable[4, 0] <= JadgePara[3] && JadgePara[3] <= CalcAreaVarTable[4, 1])
		{
			point += 12;
		}
		else if (CalcAreaVarTable[5, 0] <= JadgePara[3] && JadgePara[3] <= CalcAreaVarTable[5, 1])
		{
			point += 10;
		}
		else if (CalcAreaVarTable[6, 0] <= JadgePara[3] && JadgePara[3] <= CalcAreaVarTable[6, 1])
		{
			point += 8;
		}
		else if (CalcAreaVarTable[7, 0] <= JadgePara[3] && JadgePara[3] <= CalcAreaVarTable[7, 1])
		{
			point += 6;
		}
		else if (CalcAreaVarTable[8, 0] <= JadgePara[3] && JadgePara[3] <= CalcAreaVarTable[8, 1])
		{
			point += 4;
		}
		else
		{
			point += 2;
		}
		/* ストローク時間 */
		if (trokeTimeTable[0, 0] <= JadgePara[4] && JadgePara[4] <= trokeTimeTable[0, 1])
		{
			point += 20;
		}
		else if (trokeTimeTable[1, 0] <= JadgePara[4] && JadgePara[4] <= trokeTimeTable[1, 1])
		{
			point += 18;
		}
		else if (trokeTimeTable[2, 0] <= JadgePara[4] && JadgePara[4] <= trokeTimeTable[2, 1])
		{
			point += 16;
		}
		else if (trokeTimeTable[3, 0] <= JadgePara[4] && JadgePara[4] <= trokeTimeTable[3, 1])
		{
			point += 14;
		}
		else if (trokeTimeTable[4, 0] <= JadgePara[4] && JadgePara[4] <= trokeTimeTable[4, 1])
		{
			point += 12;
		}
		else if (trokeTimeTable[5, 0] <= JadgePara[4] && JadgePara[4] <= trokeTimeTable[5, 1])
		{
			point += 10;
		}
		else if (trokeTimeTable[6, 0] <= JadgePara[4] && JadgePara[4] <= trokeTimeTable[6, 1])
		{
			point += 8;
		}
		else if (trokeTimeTable[7, 0] <= JadgePara[4] && JadgePara[4] <= trokeTimeTable[7, 1])
		{
			point += 6;
		}
		else if (trokeTimeTable[8, 0] <= JadgePara[4] && JadgePara[4] <= trokeTimeTable[8, 1])
		{
			point += 4;
		}
		else
		{
			point += 2;
		}
		
		Smooth = point;
		return 0;
	}
	/**********************************************/
	/* スムーズ度の採点 */
	/**********************************************/
	/* 運動ファイルの読み込み */
	void ReadFile(string fname)
	{
		int BlockCount = 0;
		//try
		//{
		//string filePath = @"c:\mh\www\csharp\sample01.csv";/* ファイルパス sample */
		string filePath = fname;
		StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("Shift_JIS"));
		int i = 0;
		string[] cols = reader.ReadLine().Split(',');/* 先頭行の読み込み */
		MaxPeakNo = 0;
		
		while (reader.Peek() >= 0)
		{
			cols = reader.ReadLine().Split(',');
			
			for (int n = 0; n < cols.Length; n++)
			{
				if (cols.Length < 2)/* タグをReadしたら*/
				{
					cols = reader.ReadLine().Split(',');
					Block[BlockCount] = i;
					BlockCount++;
					MaxPeakNo++;
				}
				MotionData[i].Move_y= float.Parse(cols[0]);
				MotionData[i].Move_x = float.Parse(cols[2]);
			}
			i++;
		}
		MaxDataNo = i;
		reader.Close();
		//}
		//catch(SystemException e)
		//{
		//}
	}
	/**********************************************/
	/* 解析用の波形の作成 */
	/**********************************************/
	void MakeStrokeData()
	{
		int i;
		for (i = 0; i < MaxDataNo; i++)
		{
			//MotionData[i].MoveLength = MotionData[i].Move_y + Math.Abs(MotionData[i].Move_x);
			MotionData[i].MoveLength = (MotionData[i].Move_y + (float)90) + Mathf.Abs (MotionData[i].Move_x);
			//MotionData[i].MoveLength = (MotionData[i].Move_y ) + Math.Abs(MotionData[i].Move_x);/* debug用　tanasaki */
			
		}
	}
	
	/**********************************************/
	/* ピークの情報を検索 */
	/**********************************************/
	void SearchPeakInfo(int DataStart, int DataEnd, int PeakNo)
	{
		int i, j = 0;
		float sum = 0;
		int[] ave = new int[18];
		int topgroup, startgroup, endgroup;
		int DataKazu;
		
		DataKazu = (int)((DataEnd-DataStart  )/ 10);
		
		/* 180データの10点平均を求める */
		//for (i = 0; i < 18; i++)
		for (i = 0; i < DataKazu; i++)
		{
			/* 10点の平均値を求める */
			for (j = DataStart+i*10; j < DataStart+i*10+10; j++)
			{
				sum += MotionData[j].MoveLength;
			}
			ave[i] = (int)sum / 10;
			sum = 0;
		}
		
		/* ピークトップ*//////////////////////////
		/* 平均値の比較 */
		topgroup = 0;
		//for (i = 1; i < 18; i++)
		for (i = 0; i < DataKazu; i++)
		{
			if (ave[topgroup] < ave[i])
			{
				topgroup = i;
			}
		}
		/* 最大値の抽出 */
		j = DataStart + (topgroup * 10);
		for (i = DataStart + (topgroup * 10) + 1; i < DataStart + (topgroup * 10) + 10; i++)
		{
			if (MotionData[j].MoveLength < MotionData[i].MoveLength)
			{
				j = i;
			}
		}
		/* ピークトップの配列添え字の取得 */
		PeakData[PeakNo].PeakTopNo = j;
		
		/* ピーク始まり*//////////////////////////
		/* 平均値の比較 */
		startgroup = 0;
		for (i = 1; i < topgroup; i++)
		{
			if (ave[startgroup] > ave[i])
			{
				startgroup = i;
			}
		}
		/* 最小値の抽出 */
		j = DataStart + (startgroup * 10);
		for (i = DataStart + (startgroup * 10) + 1; i < DataStart + (startgroup * 10) + 10; i++)
		{
			if (MotionData[j].MoveLength > MotionData[i].MoveLength)
			{
				j = i;
			}
		}
		/* ピークトップの配列添え字の取得 */
		PeakData[PeakNo].PeakStartNo = j;
		
		/* ピーク終わり*//////////////////////////
		/* 平均値の比較 */
		endgroup = topgroup;
		//for (i = topgroup; i < 18; i++)
		for (i = topgroup; i < DataKazu; i++)
		{
			if (ave[endgroup] > (ave[i] + 10))
			{
				endgroup = i;
			}
		}
		/* 最小値の抽出 */
		j = DataStart + (endgroup * 10);
		for (i = DataStart + (endgroup * 10) + 1; i < DataStart + (endgroup * 10) + 10; i++)
		{
			if (MotionData[j].MoveLength > (MotionData[i].MoveLength))
			{
				j = i;
			}
		}
		/* ピークトップの配列添え字の取得 */
		PeakData[PeakNo].PeakEndNo = j;
	}
	
	/**********************************************/
	/* ピーク本数*/
	/**********************************************/
	int GetPeakNumber()
	{
		int value;
		if (MaxPeakNo == 6)
		{
			value = MaxPeakNo-1;
		}
		else
		{
			value = MaxPeakNo;
		}
		
		return value;
	}
	
	
	/**********************************************/
	/* 判定1　ストローク時間（比率）の平均値 */
	/**********************************************/
	int CalcStrokeTimeRate()
	{
		int result;
		int sum = 0;
		int n = GetPeakNumber();/* ピーク数 */
		int i;
		
		/* 平均の算出 */
		for (i = 0; i < n; i++)
		{
			if (PeakData[i].PeakEndNo - PeakData[i].PeakStartNo != 0)
			{
				sum += (PeakData[i].PeakTopNo - PeakData[i].PeakStartNo) * 100 / (PeakData[i].PeakEndNo - PeakData[i].PeakStartNo);/*ストローク時間比率を算出*/
			}
		}
		result = sum / n;
		
		return result;
	}
	/**********************************************/
	/* 判定2　ストローク時間（比率）のバラつき */
	/**********************************************/
	int CalcStrokeTimeVar()
	{
		int result;
		int[] para = new int[10];
		int sum = 0;
		int ave = 0;
		int n = GetPeakNumber();/* ピーク数 */
		int i;
		
		/* 平均の算出 */
		for (i = 0; i < n; i++)
		{
			if (PeakData[i].PeakEndNo - PeakData[i].PeakStartNo != 0)
			{
				//para[i] = (PeakData[i].PeakTopNo - PeakData[i].PeakStartNo) * 100 / (PeakData[i].PeakEndNo - PeakData[i].PeakStartNo);
				para[i] = (PeakData[i].PeakTopNo - PeakData[i].PeakStartNo)*100;/* 上昇時間を算出 */
				sum += para[i];
			}
		}
		ave = sum / n;
		//ave = JadgePara[0];
		
		/* バラつき算出 */
		sum = 0;
		for (i = 0; i < n; i++)
		{
			sum += (int)Mathf.Pow((float)(para[i] - ave),(float)2);
		}
		result = (int)Mathf.Sqrt(sum / n) ;
		
		return result;
	}
	/**********************************************/
	/* 判定3　ストローク面積（比率）の平均値 */
	/**********************************************/
	private struct menseki
	{
		public int mae;
		public int ushiro;
	}
	int CalcAreaRate()
	{
		int result;
		int[] para = new int[10];
		int sum = 0;
		int i,j;
		int n = GetPeakNumber();/* ピーク数 */
		menseki[] MensekiData = new menseki[10];
		int hen,hosei;
		
		/* 各ピークの上りと下りの面積を求める。 */
		for (i = 0; i < n; i++)
		{
			/* 上昇面積 */
			for (j = PeakData[i].PeakStartNo; j < PeakData[i].PeakTopNo; j++)
			{
				MensekiData[i].mae += (int)(MotionData[j].MoveLength + MotionData[j+1].MoveLength)*100/2;
			}
			/*面積補正*/
			hen = (int)(MotionData[PeakData[i].PeakStartNo].MoveLength + (MotionData[PeakData[i].PeakEndNo].MoveLength-MotionData[PeakData[i].PeakStartNo].MoveLength)/2);
			hosei = (int)((MotionData[PeakData[i].PeakStartNo].MoveLength +hen)*(PeakData[i].PeakTopNo -PeakData[i].PeakStartNo)*100 /2);
			MensekiData[i].mae = MensekiData[i].mae - hosei;
			/*下降面積*/
			for (j = PeakData[i].PeakTopNo; j < PeakData[i].PeakEndNo; j++)
			{
				MensekiData[i].ushiro += (int)(MotionData[j].MoveLength + MotionData[j + 1].MoveLength) * 100 / 2;
			}
			/*面積補正*/
			hosei = (int)((MotionData[PeakData[i].PeakEndNo].MoveLength + hen) * (PeakData[i].PeakEndNo - PeakData[i].PeakTopNo) * 100 / 2);
			MensekiData[i].ushiro = MensekiData[i].ushiro - hosei;
			
		}
		
		/* 平均の算出 */
		for (i = 0; i < n; i++)
		{
			if (MensekiData[i].mae + MensekiData[i].ushiro != 0)
			{
				sum += (MensekiData[i].mae) * 100 / (MensekiData[i].mae + MensekiData[i].ushiro);
			}
		}
		result = sum / n;
		
		return result;
	}
	/**********************************************/
	/* 判定4　ストローク面積（比率）のばらつき */
	/**********************************************/
	int CalcAreaVar()
	{
		int result;
		int[] para = new int[10];
		int sum = 0;
		long longsum = 0;
		int  ave = 0;
		int i, j;
		int n = GetPeakNumber();/* ピーク数 */
		menseki[] MensekiData = new menseki[10];
		int hen, hosei;
		
		/* 各ピークの上りと下りの面積を求める。 */
		for (i = 0; i < n; i++)
		{
			/* 上昇面積*/
			for (j = PeakData[i].PeakStartNo; j < PeakData[i].PeakTopNo; j++)
			{
				//MensekiData[i].mae += (int)(MotionData[j].MoveLength + MotionData[j + 1].MoveLength) * 100 / 2;
				MensekiData[i].mae += (int)(MotionData[j].MoveLength + MotionData[j + 1].MoveLength)  / 2;
			}
			/*面積補正*/
			hen = (int)(MotionData[PeakData[i].PeakStartNo].MoveLength + (MotionData[PeakData[i].PeakEndNo].MoveLength - MotionData[PeakData[i].PeakStartNo].MoveLength) / 2);
			//hosei = (int)((MotionData[PeakData[i].PeakStartNo].MoveLength + hen) * (PeakData[i].PeakTopNo - PeakData[i].PeakStartNo) * 100 / 2);
			hosei = (int)((MotionData[PeakData[i].PeakStartNo].MoveLength + hen) * (PeakData[i].PeakTopNo - PeakData[i].PeakStartNo) / 2);
			MensekiData[i].mae = MensekiData[i].mae - hosei;
			
			
			/* 下降面積 */
			for (j = PeakData[i].PeakTopNo; j < PeakData[i].PeakEndNo; j++)
			{
				//MensekiData[i].ushiro += (int)(MotionData[j].MoveLength + MotionData[j + 1].MoveLength) * 100 / 2;
				MensekiData[i].ushiro += (int)(MotionData[j].MoveLength + MotionData[j + 1].MoveLength) / 2;
			}
			/*面積補正*/
			//hosei = (int)((MotionData[PeakData[i].PeakEndNo].MoveLength + hen) * (PeakData[i].PeakEndNo - PeakData[i].PeakTopNo) * 100 / 2);
			hosei = (int)((MotionData[PeakData[i].PeakEndNo].MoveLength + hen) * (PeakData[i].PeakEndNo - PeakData[i].PeakTopNo) / 2);
			MensekiData[i].ushiro = MensekiData[i].ushiro - hosei;
			
		}
		
		/* 平均の算出 */
		for (i = 0; i < n; i++)
		{
			if (MensekiData[i].mae + MensekiData[i].ushiro != 0)
			{
				//para[i] = (MensekiData[i].mae) * 100 / (MensekiData[i].mae + MensekiData[i].ushiro);
				para[i] = (MensekiData[i].mae);
				sum += para[i];
			}
		}
		ave = sum / n;
		//ave = JadgePara[2];
		
		/* バラつき */
		longsum = 0;
		for (i = 0; i < n; i++)
		{
			longsum += (int)Mathf.Pow((float)(para[i] - ave), (float)2);
		}
		result = (int)Mathf.Sqrt(longsum / n); 
		
		return result;
	}
	/**********************************************/
	/* 判定5　ストローク時間の判定 */
	/**********************************************/
	int CalcstrokeTime()
	{
		int result;
		int sum = 0;
		int ave = 0;
		int i;
		int n = GetPeakNumber();
		
		/* ストローク時間の平均値を算出 */
		for (i = 0; i < n; i++)
		{
			sum += (PeakData[i].PeakEndNo - PeakData[i].PeakStartNo);
		}
		ave = sum / n;
		/* ストローク時間の算出 */
		result = ave * 100 / (160);/* ストローク時間(実際) ÷ ストローク時間(理想) */
		
		return result;
	}
}

