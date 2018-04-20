using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuFSM : FSMState {

    private void Awake()
    {
        stateID = StateID.GameMenu;
        AddTransition(Transition.OnStartBtnClick, StateID.GameInside);
    }

    public override void DoBeforeEntering()
    {
        controller.view.ShowMenuUI();
        controller.cameraController.ZoomIn2Menu();
    }

    public override void DoBeforeLeaving()
    {
        controller.view.HideMenuUI();
    }

    public void OnStartBtnClicked()
    {
        controller.audioController.PlayClickAC();
        fsm.PerformTransition(Transition.OnStartBtnClick);
    }

    public void OnSettingBtnClicked()
    {
        controller.audioController.PlayClickAC();
        controller.view.ShowSettingUI();
    }

    public void OnSettingUIClicked()
    {
        controller.audioController.PlayClickAC();
        controller.view.HideSettingUI();
    }

    public void OnRecordBtnClicked()
    {
        controller.audioController.PlayClickAC();
        controller.view.ShowRecordUI(controller.model.Score,controller.model.HighScore,controller.model.GameTimes, true);
    }

    public void OnRecordUIClicked()
    {
        controller.audioController.PlayClickAC();
        controller.view.ShowRecordUI(controller.model.Score, controller.model.HighScore, controller.model.GameTimes, false);
    }

    public void OnClearRecordBtnClicked()
    {
        controller.audioController.PlayClickAC();
        controller.model.ClearData();
        controller.view.ShowRecordUI(controller.model.Score, controller.model.HighScore, controller.model.GameTimes, true);
    }

    public void OnRestartBtnClicked()
    {
        controller.model.RemakeMap();
        controller.gameController.ClearShape();
        fsm.PerformTransition(Transition.OnStartBtnClick);
    }
}
