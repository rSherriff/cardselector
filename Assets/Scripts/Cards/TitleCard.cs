using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Reflection;
using Unity.VisualScripting;

public class TitleCard : Card
{
    public TextMeshPro title;
    public TextMeshPro instructions;
    public void Setup(string title)
    {
        this.title.text = title;
    }

    override public void UpdateColor(Color newColor)
    {
        title.color = newColor;
        instructions.color = newColor;
    }

    override public void UpdateFont(TMP_FontAsset font)
    {
        title.font = font;
        instructions.font = font;
    }
}
