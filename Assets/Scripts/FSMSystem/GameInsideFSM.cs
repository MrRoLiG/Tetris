using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInsideFSM : FSMState {

	private void Awake()
    {
        stateID = StateID.GameInside;
        AddTransition(Transition.OnPauseBtnClick, StateID.GameMenu);
    }

    public override void DoBeforeEntering()
    {
        controller.view.ShowInGameUI(controller.model.Score,controller.model.HighScore);
        controller.cameraController.ZoomIn2Game();
        controller.gameController.StartGame();
    }

    public override void DoBeforeLeaving()
    {
        controller.view.HideInGameUI();
        controller.view.ShowRestartBtn();
        controller.gameController.PauseGame();
    }

    public void OnPauseBtnClicked()
    {
        controller.audioController.PlayClickAC();
        fsm.PerformTransition(Transition.OnPauseBtnClick);
    }

    public void OnHomeBtnClicked()
    {
        controller.audioController.PlayClickAC();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public void OnRestartBtnClicked()
    //{
    //    controller.audioController.PlayClickAC();
    //    controller.view.HideGameOverUI();
    //    controller.model.RemakeMap();
    //    controller.gameController.StartGame();
    //    controller.view.UpdateInGameUI(controller.model.Score,controller.model.HighScore);
    //}
}
