using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public GameObject Fresh;
    public GameObject More;
    public GameObject Repeat;

    public void ShowFresh()
    {
        Fresh.SetActive(true);
    }
    public void ShowMore()
    {
        More.SetActive(true);
    }
    public void ShowRepeat()
    {
        Repeat.SetActive(true);
    }
    public void Clear()
    {
        Fresh.SetActive(false);
        More.SetActive(false);
        Repeat.SetActive(false);
    }
}
