using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//use binary to save data
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//use xml to save data
using System.Xml;

[System.Serializable]
public  class SaveData
{
	public float score;
}

public class GameManager : MonoBehaviour {
	
	public static GameManager _instance;

	public float score;

	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		changeScore ();
	}

	public void SaveFile()
	{
		SaveByXml (CreateSaveData ());
		//SaveByBin (CreateSaveData());
	}

	public void LoadGame()
	{
		LoadByBin ();
	}

	public void DeleteFile()
	{
		DeleteBinData ();
	}

	void Update()
	{
		
		if(Input.GetKeyDown(KeyCode.P))
		{
			SaveFile();
		}

		if(Input.GetKeyDown(KeyCode.O))
		{
			DeleteFile();
		}

		if (Input.GetKeyDown (KeyCode.I)) {
			LoadGame ();
		}

		if (Input.GetKeyDown (KeyCode.F1)) {
			score += 10;
			changeScore ();
		}

		if (Input.GetKeyDown (KeyCode.F2)) {
			score -= 10;
			changeScore ();
		}
	}

	public void changeScore()
	{
		UIManager._instance.UIscore.text = score.ToString();
	}


	private void DeleteBinData()
	{
		File.Delete (Application.dataPath + "/StreamingFile" + "/SaveByBin.txt");
		Debug.Log ("Delete successed");
	}

	private SaveData CreateSaveData(){
		SaveData data = new SaveData ();
		data.score = score;
		return data;
	}
		
	private void SaveByBin(SaveData saveData)
	{
		BinaryFormatter bf = new BinaryFormatter ();

		FileStream fileStream = File.Create (Application.dataPath + "/StreamingFile" + "/SaveByBin.txt");

		bf.Serialize (fileStream, saveData);

		fileStream.Close();

		if (File.Exists (Application.dataPath + "/StreamingFile" + "/SaveByBin.txt")) {
			Debug.Log ("Save Successed");
		}
	}

	private void LoadByBin()
	{
		if (!File.Exists (Application.dataPath + "/StreamingFile" + "/SaveByBin.txt")) {
			Debug.Log("ErrornNot found the path");
			return;
		}

		BinaryFormatter bf = new BinaryFormatter ();

		FileStream fileStream = File.Open (Application.dataPath + "/StreamingFile" + "/SaveByBin.txt",FileMode.Open);

		SaveData saveData = (SaveData)bf.Deserialize (fileStream);

		fileStream.Close ();

		SetGame (saveData);
	}

	private void SetGame(SaveData data)
	{
		
		score = data.score;

		changeScore ();

		Debug.Log ("Load Game Complete!");
	}


	////XML////

	private void SaveByXml(SaveData savedata)
	{
		string filePath = Application.dataPath + "/StreamingFile" + "/SaveByXml.txt";

		XmlDocument xmlDoc = new XmlDocument ();

		XmlElement root = xmlDoc.CreateElement ("Save");

		root.SetAttribute ("name", "saveFile1");

		XmlElement score = xmlDoc.CreateElement("Score");

		score.InnerText =  savedata.score.ToString();

		root.AppendChild (score);

		xmlDoc.AppendChild (root);

		xmlDoc.Save (filePath);

		Debug.Log("Save complete");

	}






}
