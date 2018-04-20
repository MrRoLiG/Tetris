using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //当前游戏是否处于暂停
    private bool isGamePause=true;
    private ShapeController currentShape = null;
    private Controller controller;

    public ShapeController[] shapes;
    public Color[] shapeColors;

    private Transform blockHolder;

    void Awake()
    {
        controller = GetComponent<Controller>();
        blockHolder = transform.Find("BlockHolder");
    }
	
	// Update is called once per frame
	void Update () {

        if (isGamePause)
            return;
        if(null==currentShape)
        {
            SpawnShape();
        }

	}

    void SpawnShape()
    {
        int shapeIndex = Random.Range(0, shapes.Length);
        int shapeColorIndex = Random.Range(0, shapeColors.Length);
        currentShape = GameObject.Instantiate(shapes[shapeIndex]);
        currentShape.transform.parent = blockHolder;
        currentShape.InitColor(shapeColors[shapeColorIndex],controller,this);
    }

    public void StartGame()
    {
        isGamePause = false;
        if (null != currentShape)
        {
            currentShape.ResumeFall();
        }
    }

    public void PauseGame()
    {
        isGamePause = true;
        if(null!=currentShape)
        {
            currentShape.PauseFall();
        }
    }

    public void HasFallDown()
    {
        currentShape = null;
        if(controller.model.isUpdateData)
        {
            controller.view.UpdateInGameUI(controller.model.Score, controller.model.HighScore);
        }

        foreach(Transform t in blockHolder)
        {
            if(t.childCount<=1)
            {
                Destroy(t.gameObject);
            }
        }

        if(controller.model.IsGameOver())
        {
            PauseGame();
            controller.view.ShowGameOverUI(controller.model.Score);
        }
    }

    public void ClearShape()
    {
        if(null!=currentShape)
        {
            Destroy(currentShape.gameObject);
            currentShape = null;
        }
    }
}
