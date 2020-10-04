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
        StartCoroutine(DrunkSequence());
    }

    IEnumerator DrunkSequence()
    {
        GameConductor.CameraStateTrigger("drunk");
        GameConductor.FreezePlayer();
        yield return new WaitForSeconds(2);
        Juicer.ShakeCamera(0.3f);
        Juicer.PlaySound(0);
        passedOutGuy.transform.position = transform.position;
        transform.parent.position = otherStart;
        yield return new WaitForSeconds(2);
        GameConductor.UnfreezePlayer();
        GameConductor.CameraStateTrigger("FocusPlayer");
    }

    void ResetState()
    {
        // undo PassOut
        transform.parent.position = myStart;
        passedOutGuy.transform.position = otherStart;
    }
}
