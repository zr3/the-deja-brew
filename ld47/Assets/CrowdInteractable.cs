using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdInteractable : Interactable
{
    public Vector3 startLocation;
    public Quaternion startRotation;
    public Transform TargetDrinkLocation;
    public int CrowdN = 1;
    private void Start()
    {
        startLocation = transform.position;
        startRotation = transform.rotation;
    }
    public override void OnPrimaryActionFinished()
    {
        TryMoveForDrink();
    }
    public override void OnSecondaryActionFinished()
    {
    }
    private void TryMoveForDrink()
    {
        if (!GameConductor.IsMaidAQuitter || GameConductor.IsBarflyHelping)
        {
            transform.parent.SetPositionAndRotation(TargetDrinkLocation.position, TargetDrinkLocation.rotation);
            switch (CrowdN)
            {
                case 1:
                    GameConductor.IsCrowd1Drinking = true;
                    GameConductor.EnqueueReset(() => GameConductor.IsCrowd1Drinking = false);
                    break;
                case 2:
                    GameConductor.IsCrowd2Drinking = true;
                    GameConductor.EnqueueReset(() => GameConductor.IsCrowd2Drinking = false);
                    break;
                case 3:
                    GameConductor.IsCrowd3Drinking = true;
                    GameConductor.EnqueueReset(() => GameConductor.IsCrowd3Drinking = false);
                    break;
            }
            GameConductor.EnqueueReset(() => transform.parent.SetPositionAndRotation(startLocation, startRotation));
        }
    }
}
