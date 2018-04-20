using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour {

    private Controller controller;
    private GameController gameController;
    private Transform pivot;

    private bool isShapePause = false;
    private bool isShapeSpeed = false;

    private float timer = 0;
    private float stepTime = 0.8f;
    private int speed = 15;


    void Awake()
    {
        pivot = transform.Find("Pivot");
    }

    public void InitColor(Color color, Controller controller, GameController gameController)
    {
        foreach(Transform t in transform)
        {
            if(t.tag=="Block")
            {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }
        this.controller = controller;
        this.gameController = gameController;
    }
    
    void Update()
    {
        if(isShapePause)
            return;

        timer += Time.deltaTime;
        if(timer>stepTime)
        {
            timer = 0;
            Fall();
        }
        MoveController();
    }

    private void Fall()
    {
        Vector3 currentPos = transform.position;
        currentPos.y -= 1;
        transform.position = currentPos;

        if(!controller.model.IsValidMapPosition(this.transform))
        {
            currentPos.y += 1;
            transform.position = currentPos;
            isShapePause = true;
            if(controller.model.FillShape(this.transform))
            {
                controller.audioController.PlayClearRowAC();
            }
            gameController.HasFallDown();
            return;
        }

        controller.audioController.PlayShapeDropAC();
    }

    public void PauseFall()
    {
        isShapePause = true;
    }

    public void ResumeFall()
    {
        isShapePause = false;
    }

    private void MoveController()
    {

//        if (isShapeSpeed)
//            return;

        float h = 0;

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            h = -1;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            h = 1;
        }

        if(0 != h)
        {
            Vector3 currentPos = transform.position;
            currentPos.x += h;
            transform.position = currentPos;

            if(!controller.model.IsValidMapPosition(this.transform))
            {
                currentPos.x -= h;
                transform.position = currentPos;
            }
            else
            {
                controller.audioController.PlayControllerAC();
            }
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(pivot.position, Vector3.forward, -90.0f);
            if(!controller.model.IsValidMapPosition(this.transform))
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90.0f);
            }
            else
            {
                controller.audioController.PlayControllerAC();
            }
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            isShapeSpeed = true;
            stepTime /= speed;
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            isShapeSpeed = false;
            stepTime = 0.8f;
        }
    }
}
