using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

public class Cube : MonoBehaviour
{

    TOUCH_STATE touchState;

    public float acceleration;
    public float maxSpeed;
    public GameObject arrivalPoint;
    

    private float moveSpeed;
    private float xAngle;
    private float yAngle;
    private float xAngleTemp;
    private float yAngleTemp;
    private bool isMove = false;

    private Vector3 destination;
    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private Quaternion oldRotation;


    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(arrivalPoint);
        //arrivalPoint.SetActive(false);
        touchState = TOUCH_STATE.TOUCH_MOVE;

    }

    // Update is called once per frame
    void Update()
    {
        Touch();
    }

    //  터치방식
    private void Touch()
    {
        //  터치 했을때 
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
            {
                if(raycastHit.collider.CompareTag("Player"))
                {
                    Debug.Log("맞았땅");
                    touchState = TOUCH_STATE.TOUCH_ZOOM;
                    Zoom();
                }
                else
                {
                   //touchState = TOUCH_STATE.TOUCH_MOVE;
                    SetDestination(new Vector3(raycastHit.point.x,0f, raycastHit.point.z));
                }
                
               
            }
        }

        if (TOUCH_STATE.TOUCH_MOVE == touchState)
            Move();

        else if (TOUCH_STATE.TOUCH_ZOOM == touchState && Input.touchCount > 0)
        {
            switch(Input.touchCount)
            {
                case 1:
                    CubeRotation();
                    break;

                default: break;
            }
        }
            CubeRotation();
    }

    private void CubeRotation()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            firstPoint = Input.GetTouch(0).position;
            xAngleTemp = xAngle;
            yAngleTemp = yAngle;
            transform.rotation = oldRotation;

        }

        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            secondPoint = Input.GetTouch(0).position;
            xAngle = xAngleTemp + (secondPoint.x - firstPoint.x) * 180 / Screen.width;
            yAngle = yAngleTemp + (secondPoint.y - firstPoint.y) * 90 / Screen.height;
            transform.rotation = Quaternion.Euler(yAngle, xAngle * -1, 0.0f);
            
        }
    }

    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        isMove = true;
    }

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

            //transform.rotation = Quaternion.LookRotation(destination);
            Vector3 dir = destination - transform.position;
            transform.forward = dir.normalized;
            transform.position += dir.normalized * Time.deltaTime * 5f;
        }
    }

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
