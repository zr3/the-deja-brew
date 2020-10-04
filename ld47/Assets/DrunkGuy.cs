using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkGuy : MonoBehaviour
{
    public GameObject passedOutGuy;
    private Vector3 myStart;
    private Vector3 otherStart;

    void Start()
    {
        myStart = transform.position;
        otherStart = passedOutGuy.transform.position;

        GameConductor.AddScheduleCallback((int hour, int min) =>
        {
            if (hour == 11 && min == 0)
            {
                ResetState();
            } else if (hour == 12 && min == 30)
            {
                PassOut();
            }
        });
    }

    void PassOut()
    {
        // swap places with other
        Juicer.ShakeCamera(0.3f);
        // todo: play sound
        passedOutGuy.transform.position = transform.position;
        transform.parent.position = otherStart;
    }

    void ResetState()
    {
        // undo PassOut
        transform.parent.position = myStart;
        passedOutGuy.transform.position = otherStart;
    }
}
