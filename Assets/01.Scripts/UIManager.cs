using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Cube cube;
    public GameObject joystickCanvas;
    private static UIManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(instance);
    }

    public static UIManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }



    public void SetCubeState(CUBE_STATE state)
    {
        
        switch (state)
        {
            case CUBE_STATE.TOUCH:
                joystickCanvas.SetActive(false);
                break;

            case CUBE_STATE.JOYSTICK:
                joystickCanvas.SetActive(true);
                break;

            default:break;
        }
    }

    public void OnClickTouch()
    {
        cube.cubeState = CUBE_STATE.TOUCH;
    }

    public void OnClickJoystick()
    {
        cube.cubeState = CUBE_STATE.JOYSTICK;
    }

    public void OnClickBack()
    {

    }




}
