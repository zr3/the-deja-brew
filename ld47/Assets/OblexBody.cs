using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OblexBody : MonoBehaviour
{
    public SkinnedMeshRenderer bodyRenderer;
    private enum Shapes { Glorb1, Glorb2, Glorb3, Shrunken }
    private bool growing = true;
    private bool bouncing = false;
    private float g1, g2, g3, s;

    void Start()
    {
        s = 100;
    }

    void Update()
    {
        if (growing)
        {
            if (!bouncing)
            {
                s -= Time.deltaTime * 200;
            } else
            {
                s += Time.deltaTime * 100;
            }
            if (s <= 0) {
                bouncing = true;
            }
            if (bouncing == true && s >= 10)
            {
                growing = false;
                s = 10;
            }
            bodyRenderer.SetBlendShapeWeight((int)Shapes.Shrunken, s);
        }
        else
        {
            g1 = (Mathf.Sin(Time.time) + 1) * 50;
            g2 = (Mathf.Sin(Time.time * 3.2f) + 1) * 50;
            g3 = (Mathf.Sin(Time.time * 2.3f) + 1) * 50;

            bodyRenderer.SetBlendShapeWeight((int)Shapes.Glorb1, g1);
            bodyRenderer.SetBlendShapeWeight((int)Shapes.Glorb2, g2);
            bodyRenderer.SetBlendShapeWeight((int)Shapes.Glorb3, g3);
        }
    }
}
