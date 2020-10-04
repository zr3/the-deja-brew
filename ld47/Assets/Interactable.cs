using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public List<string> texts;
    public List<string> secondaryTexts;
    public string cameraTrigger;
    public bool used;

    public void OnInteract()
    {
        GameConductor.FreezePlayer();
        List<string> selectedTexts = used
            ? secondaryTexts
            : texts;
        if (!used)
        {
            GameConductor.EnqueueReset(() => used = false);
        }
        for (int i = 0; i < selectedTexts.Count; ++i)
        {
            if (!string.IsNullOrWhiteSpace(cameraTrigger))
            {
                GameConductor.CameraStateTrigger(cameraTrigger);
            }
            if (i < selectedTexts.Count - 1)
            {
                MessageController.AddMessage(selectedTexts[i]);
            } else
            {
                MessageController.AddMessage(selectedTexts[i], postAction: () =>
                {
                    GameConductor.UnfreezePlayer();
                    if (!string.IsNullOrWhiteSpace(cameraTrigger))
                    {
                        GameConductor.CameraStateTrigger("FocusPlayer");
                    }
                    if (used)
                    {
                        OnSecondaryActionFinished();
                    } else
                    {
                        used = true;
                    }
                });
            }
        }
    }

    public virtual void OnSecondaryActionFinished() { }
}
