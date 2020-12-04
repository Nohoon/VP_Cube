using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

public class Cube : MonoBehaviour
{
    public GameObject arrivalPoint;

    [HideInInspector]
    public CUBE_STATE cubeState;   // 조이스틱모드,터치모드 상태
    [HideInInspector]
    public TOUCH_STATE touchState; // 터치모드일때의 상태

    private Vector3 destination;
    private Vector3 firstPoint;
    private Vector3 secondPoint;


    public float acceleration; 
    public float maxSpeed;
    public float scaleSpeed; 
    public float rotationSpeed; 
    private float xAngle;
    private float yAngle;
    private float xAngleTemp;
    private float yAngleTemp;

    private bool isMove = false;
    



    // Start is called before the first frame update
    void Start()
    {
        cubeState = CUBE_STATE.TOUCH;
        touchState = TOUCH_STATE.TOUCH_MOVE;
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.SetCubeState(cubeState);

        switch (cubeState)
        {
            case CUBE_STATE.TOUCH:
                Touch();
                break;

            case CUBE_STATE.JOYSTICK:
                Joystick();
                break;
            default: break;
        }
        
    }

    //  터치방식
    private void Touch()
    {
        //  터치 했을때 
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;

            if (TOUCH_STATE.TOUCH_ZOOM != touchState && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit) )
            {
                if(raycastHit.collider.CompareTag("Player"))    //  큐브 클릭했을때
                {
                    Debug.Log("맞았땅");
                    touchState = TOUCH_STATE.TOUCH_ZOOM;
                    Zoom();
                }
                else
                    SetDestination(new Vector3(raycastHit.point.x,0f, raycastHit.point.z));
                
                
               
            }
        }

        if (TOUCH_STATE.TOUCH_MOVE == touchState)
            Move();

        else if (TOUCH_STATE.TOUCH_ZOOM == touchState && Input.touchCount > 0)
        {
            switch (Input.touchCount)
            {
                case 1:
                    CubeRotation();
                    break;

                case 2:
                    CubeScale();
                    break;

                default: break;
            }
        }
            
    }

    //  큐브 확대 모드일때 크기 조절
    private void CubeScale()
    {
        Touch firstTouch = Input.GetTouch(0);   // 처음 누른 손가락
        Touch secondTouch = Input.GetTouch(1);  // 두번째로 누른 손가락

        // 각 손가락의 현재 프레임 이전 터치 위치
        Vector2 firstPreviousPosition = firstTouch.position - firstTouch.deltaPosition;
        Vector2 secondPreviousPosition = secondTouch.position - secondTouch.deltaPosition;

        //  이전 프레임에서의 두손가락 거리 값
        float previousPositionDistance = (firstPreviousPosition - secondPreviousPosition).magnitude;
        //  현재 프레임에서의 두손가락 거리 값
        float currentPositionDistance = (firstTouch.position - secondTouch.position).magnitude;

        // 프레임 이전의 위치랑 현재 위치의 변화량 (크기조절값)
        float scaleValue = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * scaleSpeed;

        //  크기 증가
        if (previousPositionDistance < currentPositionDistance)
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

        //  크기 감소
        else if(previousPositionDistance > currentPositionDistance)
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

        if (1f >= transform.localScale.x)
            transform.localScale = Vector3.one;
    }

    //  큐브 확대 모드일때 회전
    private void CubeRotation()
    {
        // 처음 터치했을때
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            firstPoint = Input.GetTouch(0).position;
            xAngleTemp = xAngle;
            yAngleTemp = yAngle;


        }

        //  터치 중일때
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            secondPoint = Input.GetTouch(0).position;
            //xAngle = xAngleTemp + (secondPoint.x - firstPoint.x) * 180 / Screen.width;
            //yAngle = yAngleTemp + (secondPoint.y - firstPoint.y) * 90 / Screen.height;
            xAngle = xAngleTemp + (secondPoint.x - firstPoint.x);
            yAngle = yAngleTemp + (secondPoint.y - firstPoint.y);
            transform.rotation = Quaternion.Euler(-yAngle, -xAngle, 0.0f);
        }

        //  터치 끝날때
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            
        }        
    }

    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        isMove = true;
    }

    //  큐브 이동
    private void Move()
    {
        CameraManager.Instance.SetCamera(CAMERA_STATE.MAIN);

        if (isMove)
        {
            if(0.1f >= Vector3.Distance(destination,transform.position))
            {
                isMove = false;
                return;
            }

            Vector3 dir = destination - transform.position;
            transform.forward = dir.normalized;
            transform.position += dir.normalized * Time.deltaTime * 5f;
        }
    }

    //  큐브 클릭시 카메라 변경 및 큐브 초기 위치 회전값 변경
    private void Zoom()
    {
        CameraManager.Instance.SetCamera(CAMERA_STATE.ZOOM);

        Vector3 vec3 = CameraManager.Instance.GetCamera().ScreenToWorldPoint(new Vector3(
            CameraManager.Instance.GetCamera().pixelWidth, 
            CameraManager.Instance.GetCamera().pixelHeight, 
            CameraManager.Instance.GetCamera().nearClipPlane));

        transform.position = new Vector3(vec3.x, vec3.y, vec3.z + 6.5f);
        transform.rotation = Quaternion.identity;
    }

    private void Joystick()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
