using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UALDevice : MonoBehaviour
{
    public SpriteRenderer top;
    public SpriteRenderer middle;
    public SpriteRenderer bottom;

    public void UpdateColors(Color newColor)
    {
        top.color = newColor;
        middle.color = newColor;
        bottom.color = newColor;
    }
   
}
