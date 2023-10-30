using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCard : CountCard
{
    public TextMeshPro title;
    public TextMeshPro credit;
    public TextMeshPro description;
    public Animator animator;

    public bool SelectAnimationComplete = false;

    private Game game;

    public void Setup(Game game)
    {
        this.game = game;

        this.gameObject.name = game.title + " Card";

        title.text = game.title;
        credit.text = game.credit;
        description.text = game.description;
    }

    override public void Select()
    {
        if (active)
        {
            StartCoroutine(SelectCoroutine());
        }
        else
        {
            Debug.LogWarning("Attempting to select a non-active card! " + this.gameObject.name);
        }
    }

    public IEnumerator SelectCoroutine()
    {
        SelectManager.Instance.CardSelected();
        animator.SetTrigger("SelectTrigger");

        yield return new WaitUntil(IsAnimationComplete);

        Launcher.Instance.LaunchGame(game.executable);

        SelectAnimationComplete = false;
    }

    public bool IsAnimationComplete()
    {
        return SelectAnimationComplete;
    }
}
