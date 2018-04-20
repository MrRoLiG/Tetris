using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour {

    private Camera mainCamera;

    void Awake()
    {
        mainCamera=Camera.main;
    }

    public void ZoomIn2Game()
    {
        mainCamera.DOOrthoSize(13.0f, 0.5f);
    }

    public void ZoomIn2Menu()
    {
        mainCamera.DOOrthoSize(20.0f, 0.5f);
    }
}
