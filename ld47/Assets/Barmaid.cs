using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barmaid : MonoBehaviour
{
    public GameObject helpfulBarmaid;
    private Vector3 myStart;
    private Vector3 otherStart;
    private Vector3 otherEnd;

    void Start()
    {
        myStart = transform.position;
        otherStart = helpfulBarmaid.transform.position + Vector3.down * 10;
        otherEnd = helpfulBarmaid.transform.position;
        helpfulBarmaid.transform.position = otherStart;

        GameConductor.AddScheduleCallback((int hour, int min) =>
        {
            if (hour == 11 && min == 0)
            {
                ResetState();
            }
            else if (hour == 12 && min == 40 && !GameConductor.IsMaidAQuitter)
            {
                HelpDrunk();
            }
        });
    }

    void HelpDrunk()
    {
        // todo: play sound
        transform.parent.Translate(0, -10, 0);
        helpfulBarmaid.transform.position = otherEnd;
    }

    void ResetState()
    {
        // undo HelpDrunk
        transform.parent.position = myStart;
        helpfulBarmaid.transform.position = otherStart;
    }
}
