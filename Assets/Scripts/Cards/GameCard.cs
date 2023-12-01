using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Reflection;
using UnityEngine.Rendering.HighDefinition;

public class GameCard : CountCard
{
    public TextMeshPro title;
    public TextMeshPro credit;
    public TextMeshPro description;
    public TextMeshPro play;
    public TextMeshPro instructionsLeft;
    public TextMeshPro instructionsRight;
    public Material playButtonMaterial;
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

    override public void UpdateColor(Color newColor)
    {
        title.color = newColor;
        credit.color = newColor;
        description.color = newColor;
        count.color = newColor;
        play.color = newColor;
        instructionsLeft.color = newColor;
        instructionsRight.color = newColor;
        playButtonMaterial.SetColor("_BaseColor", newColor);
    }

    override public void UpdateFont(TMP_FontAsset font)
    {
        title.font = font;
        credit.font = font;
        description.font = font;
        count.font = font;
        play.font = font;
        instructionsLeft.font = font;
        instructionsRight.font = font;
    }
}
