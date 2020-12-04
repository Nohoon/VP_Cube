using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickJoystick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickJoystick()
    {
        Debug.Log("조이스틱");
    }
}
