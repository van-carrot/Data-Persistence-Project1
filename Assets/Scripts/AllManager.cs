using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class AllManager : MonoBehaviour
{
    public static AllManager Instance;
    public string PlayerName;
    public InputField InputText;

    public int HighScore = 0;
    public string ScoreMaker;

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        LoadHighScore();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Enter()
    {
        SceneManager.LoadScene(1);
    }

    public void GetName()
    {
        PlayerName = InputText.text;
    }


    [System.Serializable]
    class SaveData 
    {
        public int score;
        public string name;
    }


    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.score = HighScore;
        data.name = ScoreMaker;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.score;
            ScoreMaker = data.name;
        }

    }
}
