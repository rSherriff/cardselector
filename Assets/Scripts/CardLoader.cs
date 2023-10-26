using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardLoader : Singleton<CardLoader>
{
    public GameObject gameCard;
    public GameObject titleCard;

    private bool createdAllCards;

    public void Start()
    {
        StartCoroutine(loadGamesJson());
    }

    private IEnumerator loadGamesJson()
    {
        createdAllCards = false;

        string path = Application.dataPath + "/games.json";
        Debug.Log("Loading games from " + path);

        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            Games gamesInJson = JsonUtility.FromJson<Games>(reader.ReadToEnd());

            TitleCard newTitleCard = Instantiate(titleCard).GetComponent<TitleCard>();
            newTitleCard.Setup(gamesInJson.title);
            SelectManager.Instance.AddSelectObject(newTitleCard);

            foreach (Game game in gamesInJson.games)
            {
                Debug.Log(game.title);
                GameCard newCard = Instantiate(gameCard).GetComponent<GameCard>();
                newCard.Setup(game);
                SelectManager.Instance.AddSelectObject(newCard);
            }

            reader.Close();
        }
        else
        {
            Debug.LogError("Path does not exist!");
        }

        createdAllCards = true;

        yield return null;
    }

    public bool DoneCreatingCards()
    {
        return createdAllCards;
    }
}
