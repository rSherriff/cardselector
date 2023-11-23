using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    protected bool active;

    virtual public void Setup()
    { }

    public void Enable()
    {
        active = true;
    }

    public void Disable()
    {
        active = false;
    }

    virtual public void UpdateColor(Color newColor)
    { }

    virtual public void UpdateFont(TMP_FontAsset font)
    { }
}

public class SelectableCard : Card
{
    virtual public void Select()
    { }
}

public class CountCard : SelectableCard
{
    int index;
    int numCards;
    public TextMeshPro count;

    public void SetupCount(int index, int numCards)
    {
        this.index = index;

        string countText = "";
        if (numCards >= 10 && index < 10)
        {
            countText = "0" + index.ToString();
        }
        else
        {
            countText = index.ToString();
        }
        count.text = countText + "/" + numCards;
    }
}
