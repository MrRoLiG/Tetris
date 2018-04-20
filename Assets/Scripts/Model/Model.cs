using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

    public const int NORMAL_ROWS = 20;
    public const int MAX_ROWS = 23;
    //private int ExtraRows = 3;
    //额外3列用来判断是否超出地图空间，游戏结束
    public const int MAX_COLLUMS = 12;

    private int score = 0;
    private int highScore = 0;
    private int gameTimes = 0;

    public bool isUpdateData = false;

    private Transform[,] map = new Transform[MAX_COLLUMS, MAX_ROWS];

    public int Score { get { return score; } }
    public int HighScore { get { return highScore; } }
    public int GameTimes { get { return gameTimes; } }

    void Awake()
    {
        LoadData();
    }

    public bool IsValidMapPosition(Transform t)
    {
        foreach(Transform child in t)
        {
            if ("Block" != child.tag)
                continue;

            Vector2 pos = child.position.Round();
            if (!IsInMap(pos))
                return false;
            if (null != map[(int)pos.x, (int)pos.y])
                return false;
        }

        return true;
    }

    private bool IsInMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < MAX_COLLUMS;
    }

    public bool FillShape(Transform t)
    {
        foreach(Transform child in t)
        {
            if ("Block" != child.tag)
                continue;
            Vector2 pos = child.position.Round();
            map[(int)pos.x, (int)pos.y] = child;
        }

        return ClearCompleteRows();
    }

    public bool ClearCompleteRows()
    {
        int count = 0;
        for(int i=0;i<NORMAL_ROWS;i++)
        {
            if(IsRowFull(i))
            {
                count++;
                ClearFullRow(i);
                MoveAboveRowsDown(i + 1);
                i--;
            }
        }

        if(count>0)
        {
            score += (count) * 100;
            if(score > highScore)
            { 
                highScore = score;
            }
            isUpdateData = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsRowFull(int rowIndex)
    {
        for(int i = 0; i < MAX_COLLUMS; i++)
        {
            if (null == map[i, rowIndex])
                return false;
        }

        return true;
    }

    private void ClearFullRow(int rowIndex)
    {
        for (int i = 0; i < MAX_COLLUMS; i++)
        {
            Destroy(map[i, rowIndex].gameObject);
            map[i, rowIndex] = null;
        }
    }

    private void MoveAboveRowsDown(int rowIndex)
    {
        for (int i = rowIndex; i < NORMAL_ROWS; i++)
        {
            MoveRowDown(i);
        }
    }

    private void MoveRowDown(int rowIndex)
    {
        for(int i=0;i<MAX_COLLUMS;i++)
        {
            if (null!=map[i, rowIndex])
            {
                map[i, rowIndex - 1] = map[i, rowIndex];
                map[i, rowIndex] = null;
                map[i, rowIndex - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    private void LoadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        gameTimes = PlayerPrefs.GetInt("GameTimes", 0);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("GameTimes", gameTimes);
    }

    public void ClearData()
    {
        highScore = 0;
        gameTimes = 0;
        SaveData();
    }

    public bool IsGameOver() 
    {
        for (int i = NORMAL_ROWS; i < MAX_ROWS; i++)
        {
            for(int j=0; j < MAX_COLLUMS; j++)
            {
                if(null != map[j,i])
                {
                    gameTimes++;
                    SaveData();
                    return true;
                }
            }
        }

        return false;
    }

    public void RemakeMap()
    {
        for(int i=0;i<MAX_COLLUMS;i++)
        {
            for(int j=0;j<MAX_ROWS;j++)
            {
                if(null!=map[i,j])
                {
                    Destroy(map[i, j].gameObject);
                    map[i, j] = null;
                }
            }
        }
        score = 0;
    }
}
