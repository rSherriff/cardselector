using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    bool active;

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
}

public class SelectableCard : Card
{
    virtual public void Select()
    { }
}
