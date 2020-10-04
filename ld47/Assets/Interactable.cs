using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public List<string> texts;
    public List<string> secondaryTexts;
    public string cameraTrigger;
    public bool used;
    public bool cleared;
    public Notification notification;

    public void OnSelect()
    {
        if (!used && !cleared)
        {
            notification?.ShowFresh();
        } else if (used && !cleared)
        {
            notification?.ShowMore();
        } else if (used && cleared)
        {
            notification?.ShowRepeat();
        }
    }

    public void OnDeselect()
    {
        notification?.Clear();
    }

    public void OnInteract()
    {
        Juicer.PlaySound(2);
        GameConductor.FreezePlayer();
        List<string> selectedTexts = used
            ? secondaryTexts
            : texts;
        if (!used)
        {
            GameConductor.EnqueueReset(() =>
            {
                used = false;
                cleared = false;
            });
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
                        cleared = true;
                    } else
                    {
                        OnPrimaryActionFinished();
                        used = true;
                    }
                    Player.DeselectInteractable();
                });
            }
        }
    }

    public virtual void OnPrimaryActionFinished() { }
    public virtual void OnSecondaryActionFinished() { }
}
