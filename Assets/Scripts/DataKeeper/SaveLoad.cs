using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void Save() {
		BinaryFormatter fileFormat;
		fileFormat = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/saveData.dat");

		SaveData data = new SaveData ();
		data.playerPosition = player.transform.position;
		data.scene = SceneManager.GetActiveScene();
		//data.soul.Add ();

		fileFormat.Serialize (file, data);
		file.Close ();
	}
	public void Load() {
		if (File.Exists(Application.persistentDataPath + "/saveData.dat")) {
			BinaryFormatter fileFormat;
			fileFormat = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/saveData.dat",FileMode.Open);
			SaveData data = (SaveData) fileFormat.Deserialize(file);
			file.Close ();

			SceneManager.LoadScene (data.scene.name);
			player.transform.position = data.playerPosition;
			//data.soul.Add ();
		}
	}
}

[System.Serializable]
class SaveData {
	public Vector2 playerPosition;
	public Scene scene;
	public List<Soul> soul;
}