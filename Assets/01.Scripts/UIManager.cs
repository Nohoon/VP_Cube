using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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

    public void OnClickBack()
    {

    }
}
