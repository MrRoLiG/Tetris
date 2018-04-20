using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;
    [HideInInspector]
    public CameraController cameraController;
    [HideInInspector]
    public GameController gameController;
    [HideInInspector]
    public AudioController audioController;

    //构建有效状态机
    private FSMSystem fsm;

    private void Awake()
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        cameraController = GetComponent<CameraController>();
        gameController = GetComponent<GameController>();
        audioController = GetComponent<AudioController>();
    }

	// Use this for initialization
	private void Start () {
        MakeFSM();
	}

    void MakeFSM()
    {
        fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
        foreach(FSMState state in states)
        {
            fsm.AddState(state,this);
        }
        GameMenuFSM gameMenu = GetComponentInChildren<GameMenuFSM>();
        fsm.SetCurrentState(gameMenu);
    }
}
