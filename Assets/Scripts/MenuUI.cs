using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;


public class MenuUI : MonoBehaviour
{

    [SerializeField] PlayerData playerInfo;
    [SerializeField] string playerName;
    [SerializeField] int points;
    public static MenuUI Instance;
    [SerializeField] TMP_InputField inputField;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    private void Start()
    {
        inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        if (MenuUI.Instance != null)
        {
            this.playerName = MenuUI.Instance.getName();
            this.points = MenuUI.Instance.getPoints();
        }

    }
    public string getName()
    {
        return playerInfo.getPlayerName();
    }
    public int getPoints()
    {
        return playerInfo.getBestScore();
    }

    public void setPoints(int val)
    {
        this.playerInfo.setBestScore(val);
    }
    public void ValueChangeCheck()
    {
        this.playerName = inputField.text;
    }

    public void startGame()
    {
        bool ret = LoadData(playerName);

        //if there is not anyone player with this name
        if (!ret)
            playerInfo = new PlayerData(playerName, 0);

        SceneManager.LoadScene(1);
        
    }

    public void quitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    
    public void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/" + data.getPlayerName() + ".json", json);
    }

    public bool LoadData(string playerName)
    {
        string path = Application.persistentDataPath + "/" + playerName + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData pd = JsonUtility.FromJson<PlayerData>(json);
            playerInfo.setBestScore(pd.getBestScore());
            playerInfo.setPlayerName(pd.getPlayerName());
            return true;
        }
        return false;
    }
}
