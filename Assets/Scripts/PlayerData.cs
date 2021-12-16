using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{

    [SerializeField] private string playerName;
    [SerializeField] private int bestScore;
    

    public PlayerData(string playerName, int bestScore)
    {
        this.playerName = playerName;
        this.bestScore = bestScore;
    }

    public string getPlayerName()
    {
        return this.playerName;
    }

    public int getBestScore()
    {
        return this.bestScore;
    }

    public void setBestScore(int val)
    {
        this.bestScore = val;
    }

    public void setPlayerName(string name)
    {
        this.playerName = name;
    }
}
