using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

	public static DataManager control;

//	public int total_Time;
//	public int times_Played;
//	public int high_Score;
//	public int prev_High_Score;
//	public int total_Points;
//	public int experience;

	public int x;

//	[SerializeField]
//	public Level_Data[] levels;

//	public Level_Data level;

	[SerializeField]
	public Levels_Data levels;
	[SerializeField]
	public Player_Data player;
	[SerializeField]
	public Questions_Data questions;


	int buildCount;
	int currentSceneIndex;

	void Awake(){
		currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		buildCount = 6;

		if(control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
		} else { 
			Destroy (gameObject);
		} 
		LoadFunction ();
//		Load ();
//		GameObject.Find ("Path").GetComponent<Text>().text = Application.persistentDataPath;
//		Load2();
	}

	//OLD
//	void Start() {
//		levels = new Levels_Data ();      //
//		Levels_Data data = Load2 ();      //
////
//		x = data.x;
//		for (int i = 0; i < buildCount; i++) {
//			levels.levels [i] = new Level_Data ();
//			levels.levels [i].total_Points = data.levels [i].total_Points;      //
//		}
////		print (levels.levels [0].total_Points);
//	}

	public void LoadFunction(){
		levels = new Levels_Data ();     
		player = new Player_Data();
		player.settings = new Settings ();
		player.skins = new bool[7];
		questions = new Questions_Data ();
		for (int i = 0; i < buildCount; i++) {
			levels.levels [i] = new Level_Data ();
		}
		All_Data data = Load2 ();  
		if (data != null) {
			player = data.player;
			player.currency = player.total_points - player.boughtPrice;
			for (int i = 0; i < data.player.skins.Length; i++) {
				player.skins [i] = data.player.skins [i];
			}
			for (int i = 0; i < buildCount; i++) {
				levels.levels [i] = data.levels.levels [i];      //
			}
//		print (data.custom_Questions); 
			if (data.custom_Questions != null && data.custom_Questions.questions != null) {
				if (data.custom_Questions.questions.Count > 0) {
					questions.questions = new List<Question_Data> (data.custom_Questions.questions);
				}
//			print ("Playing with new questions:   ");
			} else {
				if (CreateCustomQuestions.static_questions != null) {
					List<QuestionObject> x = new List<QuestionObject> (CreateCustomQuestions.static_questions.CustomQuestions);
					questions.questions = new List<Question_Data> ();
					for (int i = 0; i < x.Count; i++) {
						Question_Data temp = x [i].CreateData (x [i]);
						questions.questions.Add (temp);
					}
				}
//			print ("Reset back to old questions:   ");
			}
		}
	}

	void Start() {

	}

	void Update() {
//		AwakeFunction ();
	}

//	public void Save(){
//		BinaryFormatter bf = new BinaryFormatter ();
//		FileStream file = File.Create (Application.persistentDataPath + "/levelsInfo.dat");
//		Level_Data[] levels_Data = new Level_Data[buildCount];
//		CalculateExperience ();
//		for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++) {
//			levels_Data [i] = new Level_Data ();
//			levels_Data [i].total_Points = levels [i].total_Points;
//			levels_Data [i].experience = levels [i].experience; 
//		}
//		bf.Serialize (file, levels_Data);
//		file.Close ();
//	}
//
//	public void Load(){
//		if (File.Exists (Application.persistentDataPath + "/levelsInfo.dat")) {
//			BinaryFormatter bf = new BinaryFormatter ();
//			FileStream file = File.Open (Application.persistentDataPath + "/levelsInfo.dat", FileMode.Open);
//			Level_Data[] levels_Data = bf.Deserialize (file) as Level_Data[];
//			file.Close ();
//			for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++) {
//				levels [i] = new Level_Data ();
////			levels_Data [currentSceneIndex] = new Level_Data ();
//				levels [i].total_Points = levels_Data [i].total_Points;
//				levels [i].experience = levels_Data [i].experience;
//			}
//		} 
//	}

//	public void Save2(){
//		BinaryFormatter bf = new BinaryFormatter ();
//		FileStream file = File.Create (Application.persistentDataPath + "/test.dat");
//		Levels_Data levels_Data = new Levels_Data();     //
//		CalculateExperience ();
//		levels_Data.x = x + 40;     //
//		levels_Data.levels = new Level_Data[buildCount];     //
//		for (int i = 0; i < buildCount; i++) {     //
//			levels_Data.levels [i] = new Level_Data ();
//			levels_Data.levels [i].total_Points = levels.levels [i].total_Points;     //
//			levels_Data.levels [i].experience = levels.levels [i].experience;     // 
//		}
//		bf.Serialize (file, levels_Data);     //
//		file.Close ();
//	}
//
//	public Levels_Data Load2(){
//		if (File.Exists (Application.persistentDataPath + "/test.dat")) {
//			BinaryFormatter bf = new BinaryFormatter ();
//			FileStream file = File.Open (Application.persistentDataPath + "/test.dat", FileMode.Open);
//			Levels_Data levels_Data = bf.Deserialize (file) as Levels_Data;     //
//			file.Close ();
//			return levels_Data;     //
////			levels = new Levels_Data ();
////			levels.levels = new Level_Data[buildCount];
////			levels.levels [0].total_Points = levels_Data.levels [0].total_Points;
////			levels.levels [0].experience = levels_Data.levels [0].experience;
//		} else {
//			Debug.LogError ("Cant find file in " + Application.persistentDataPath + "/test.dat");
//			return null;
//		}
//	}

	public void Save2(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/allData1111.dat");
		All_Data data = new All_Data ();
		data.levels = new Levels_Data ();
		data.player = new Player_Data ();
		data.player.settings = new Settings ();
		data.player.skins = new bool[7];
		data.player = player;
		//Saves current questions by adding them all to a new list
//		print(CustomQuiz.custom_controller);
		if (CustomQuiz.custom_controller != null) {
			data.custom_Questions = new Questions_Data ();
			data.custom_Questions.questions = new List<Question_Data> ();
			List<QuestionObject> x = new List<QuestionObject> (CustomQuiz.custom_controller.CustomQuestions);
//		    List<QuestionObject> x = CustomQuiz.custom_controller.CustomQuestions;
			for (int i = 0; i < x.Count; i++) {
				Question_Data temp = x [i].CreateData (x [i]);
				data.custom_Questions.questions.Add (temp);
			}
		} else {
			data.custom_Questions = questions;
		}

		data.levels.levels = new Level_Data[buildCount];     //
		for (int i = 0; i < buildCount; i++) {     //
			data.levels.levels [i] = new Level_Data ();
			data.levels.levels [i] = levels.levels [i]; //
		}
		CalculateExperience ();
		bf.Serialize (file, data);     //
		file.Close ();

		LoadFunction ();

	}

	public All_Data Load2(){
		if (File.Exists (Application.persistentDataPath + "/allData1111.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/allData1111.dat", FileMode.Open);
			All_Data data = bf.Deserialize(file) as All_Data;
			file.Close ();
			return data;     //
		} else {
			Debug.LogError ("Cant find file in " + Application.persistentDataPath + "/allData1111.dat");
			return null;
		}
	}

	public void ClearData(){
		levels = new Levels_Data ();     
		player = new Player_Data();
		player.settings = new Settings ();
		player.skins = new bool[7];
		questions = new Questions_Data ();
		for (int i = 0; i < buildCount; i++) {
			levels.levels [i] = new Level_Data ();
		}
		Save2 ();
		LoadFunction ();
	}

//	public void Save2() {
//		BinaryFormatter bf = new BinaryFormatter ();
//		FileStream file = new FileStream (Application.persistentDataPath + "/test.dat", FileMode.Create);
//		int xData = x;
//		bf.Serialize (file, xData);
//		file.Close ();
//
//	} 
//
//	public void Load2(){
//		if (File.Exists (Application.persistentDataPath + "/test.dat")) {
//			BinaryFormatter bf = new BinaryFormatter ();
//			FileStream file = new FileStream (Application.persistentDataPath + "/test.dat", FileMode.Open);
//			int xData = bf.Deserialize (file);
//			file.Close ();
//			xData = x;
//		
//		} else {
//			Debug.LogError ("Save file not found in " + Application.persistentDataPath + "/test.dat");
//			return null;
//		}
//	}

	void CalculateExperience(){
		for (int i = 0; i < 6; i++) {
//			levels [i].experience = levels [i].total_Points * 5;
			if (DataManager.control.levels.levels [i] != null) {
				levels.levels [i].experience = levels.levels [i].total_Points * 5;     //
				levels.levels[i].CalculateExperienceNeeded(levels.levels);
				player.experience = player.total_points * 5;
			}
		}
	}

//	public void Save2(){
//		BinaryFormatter bf = new BinaryFormatter ();
//		FileStream file = File.Create (Application.persistentDataPath + "/test.dat");
//		Level_Data level_Data = new Level_Data();
//		CalculateExperience ();
//		level_Data = new Level_Data ();
//		level_Data.total_Points = level.total_Points;
//		level_Data.experience = level.experience; 
//		bf.Serialize (file, level_Data);
//		file.Close ();
//	}
//
//	public void Load2(){
//		if (File.Exists (Application.persistentDataPath + "/test.dat")) {
//			BinaryFormatter bf = new BinaryFormatter ();
//			FileStream file = File.Open (Application.persistentDataPath + "/test.dat", FileMode.Open);
//			Level_Data level_Data = bf.Deserialize (file) as Level_Data;
//			file.Close ();
//			level = new Level_Data ();
//			level.total_Points = level_Data.total_Points;
//			level.experience = level_Data.experience;
//		} 
//	}


}

[Serializable]
public class Player_Data {
	public int most_played_level;
	public int total_minutes;
	public int total_swipes;
	public int high_Score;
	public int times_Died;
	public int total_points;
	public int currency;
	public int boughtPrice;
	public int experience;
	public int longest_snake;
	public Settings settings;
	public bool[] levels_unlocked;
	public bool[] skins;
	public int currentSkin = -1;
	public int currentMap = 0;
	public bool AI_control = false;
}

[Serializable]
public class Level_Data {
	public string name;
	public int total_minutes;
	public int times_Died;
	public int high_Score;
	public int total_Points;
	public int experience;
	public int experience_needed;
	public int score_target = 20;
	public Custom_Setup custom_settings;

	public void CalculateExperienceNeeded(Level_Data[] levels){
		int index = Array.IndexOf (levels, this);
		if (index != 0) {
			this.experience_needed = index * 1000;
//			Debug.Log (experience_needed);
		}
	}
}

[Serializable]
public class Custom_Setup {
	public int foodNum;
//	public int speed;
	public int timer;
}

[Serializable]
public class Levels_Data {
	public Level_Data[] levels;
	public int x;

	public Levels_Data(){
		levels = new Level_Data[6];
	}
}

[Serializable]
public class Settings{
	public bool music;
	public bool sound = true;
}

//[Serializable]
//public class Question_Data {
//	public string question;
//	public string correct;
//	public string incorrect1;
//	public string incorrect2;
//	public string incorrect3;
//
//	public string dataAdded;
//	public string lastEdited;
//}

[Serializable]
public class Questions_Data {
	public List<Question_Data> questions;
}

//[Serializable]
//public class Question_Data {
//	public List<QuestionObject>
//}

[Serializable]
public class All_Data {
	public Levels_Data levels;
	public Player_Data player;
	public Questions_Data custom_Questions;
}

[Serializable]
public class Question_Data {
	public string question;
	public string correctAns;
	public List<string> incorrectAns;

	public Question_Data(string ques, string corr, List<string> inco){
		this.question = ques;
		this.correctAns = corr;
		this.incorrectAns = inco;
	} 

	public Question_Data() {

	}
}




