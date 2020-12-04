using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour , IPointerDownHandler , IPointerUpHandler , IDragHandler
{
    public RectTransform rect_Background;
    public RectTransform rect_Joystick;
    public GameObject cube;
    public float moveSpeed;

    private float radius;

    private bool isTouch = false;
    private Vector3 movePosition;

    // Start is called before the first frame update
    void Start()
    {
        radius = rect_Background.rect.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

               
    }
    private void FixedUpdate()
    {
        if (isTouch)
            cube.transform.position += movePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rect_Background.position;
        value = Vector2.ClampMagnitude(value, radius);
        rect_Joystick.localPosition = value;

        float dir = Vector2.Distance(rect_Background.position, rect_Joystick.position) / radius;

        value = value.normalized;
        movePosition = new Vector3(
            value.x * moveSpeed * Time.deltaTime,
            0f,
            value.y * moveSpeed * Time.deltaTime);


        //transform.LookAt(movePosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_Joystick.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
    }
}
