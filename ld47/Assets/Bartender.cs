using UnityEngine;

public class Bartender : MonoBehaviour
{
    public GameObject helpfulBartender;
    private Vector3 myStart;
    private Vector3 otherStart;
    private Vector3 otherEnd;

    void Start()
    {
        myStart = transform.position;
        otherStart = helpfulBartender.transform.position + Vector3.down * 10;
        otherEnd = helpfulBartender.transform.position;
        helpfulBartender.transform.position = otherStart;

        GameConductor.AddScheduleCallback((int hour, int min) =>
        {
            if (hour == 11 && min == 0)
            {
                ResetState();
            }
            else if (hour == 12 && min == 50 && GameConductor.IsMaidAQuitter)
            {
                HelpDrunk();
            }
            else if (hour == 2 && min == 0 && !GameConductor.IsOblexRevealed)
            {
                GameConductor.FreezePlayer();
                MessageController.AddMessage("(you hear the bartender's voice)");
                MessageController.AddMessage("Oy! Last call for drinks! The show's over!");
                MessageController.AddMessage("Nothing more to see!", postAction: () => GameConductor.UnfreezePlayer());
            }
        });
    }

    void HelpDrunk()
    {
        // todo: play sound
        transform.parent.Translate(0, -10, 0);
        helpfulBartender.transform.position = otherEnd;
    }

    void ResetState()
    {
        // undo HelpDrunk
        transform.parent.position = myStart;
        helpfulBartender.transform.position = otherStart;
    }
}
