using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCard : SelectableCard
{
    public TextMeshPro title;
    public TextMeshPro credit;
    public TextMeshPro description;

    private Game game;

    public void Setup(Game game)
    {
        this.game = game;

        title.text = game.title;
        credit.text = game.credit;
        description.text = game.description;
    }

    override public void Select()
    {
        SelectManager.Instance.CardSelected();
        Launcher.Instance.LaunchGame(game.executable);
    }
}
