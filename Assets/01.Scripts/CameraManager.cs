using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera zoomCamera;

    //public CAMERA_STATE state;

    private static CameraManager instance = null;
    private void Awake()
    {
        mainCamera.enabled = true;
        zoomCamera.enabled = false;

        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(instance);
    }

    public static CameraManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    public void SetCamera(CAMERA_STATE _state)
    {
        switch(_state)
        {
            case CAMERA_STATE.MAIN:
                mainCamera.enabled = true;
                zoomCamera.enabled = false;
                break;

            case CAMERA_STATE.ZOOM:
                mainCamera.enabled = false;
                zoomCamera.enabled = true;
                break;
        }
    }

    public Camera GetCamera()
    {
        if (true == mainCamera.enabled)
            return mainCamera;
        else
            return zoomCamera;
    }


}
