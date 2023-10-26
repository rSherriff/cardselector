using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SelectManager : Singleton<SelectManager>
{
    public Transform cardDisplayPoint;
    public Transform cardInitialPoint;

    public Transform cardLeftPoint;
    public Transform cardRightPoint;

    [SerializeField] private int selectedItemIndex;
    [SerializeField] private List<Card> cards = new List<Card>();

    //Curves
    public float cardMovementSpeed;
    public AnimationCurve cardInCurve;
    public AnimationCurve cardOutCurve;
    float cardProgress = 0;

    //Audio
    public AudioSource menuSounds;
    public AudioClip menuMoveSound;
    public AudioClip menuSelectSound;

    //Text
    public TextMeshProUGUI title;
    public TextMeshProUGUI subtitle;
    public TextMeshProUGUI filepath;

    private bool transitioningCards = false;

    private void Start()
    {
        selectedItemIndex = 0;
    }

    public void AddSelectObject(Card newCard)
    {
        newCard.gameObject.transform.position = cardInitialPoint.position;
        newCard.gameObject.transform.rotation = Quaternion.identity;
        cards.Add(newCard.GetComponent<Card>());
        RefreshDisplay();
    }    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Card selectedCard = cards[selectedItemIndex];
            if (selectedCard.GetType().IsSubclassOf(typeof(SelectableCard)))
            {
                ((SelectableCard)selectedCard).Select();
            }
            }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !transitioningCards)
            {
                StartCoroutine(TransistionCards(-1));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !transitioningCards)
            {
                StartCoroutine(TransistionCards(1));
            }
        }
  
    }

    private IEnumerator TransistionCards(int movement)
    {
        transitioningCards = true;

        Card oldCard = cards[selectedItemIndex];
        oldCard.Disable();

        //Move Old Card
        Vector3 startPosition = oldCard.gameObject.transform.position;
        Vector3 finalPosition = movement < 0 ? cardLeftPoint.position : cardRightPoint.position;

        StartCoroutine(MoveCard(oldCard, startPosition, finalPosition, cardInCurve));

        yield return new WaitUntil(StartCardIn);

        //Update selected card
        selectedItemIndex += movement;
        selectedItemIndex = selectedItemIndex < 0 ? cards.Count -1 : selectedItemIndex; //Have we dropped below 0?
        selectedItemIndex = selectedItemIndex >= cards.Count ? 0 : selectedItemIndex; //Have we gone above max?

        Card newCard = cards[selectedItemIndex];

        //Move New Card
        newCard.transform.position = movement < 0 ? cardRightPoint.position : cardLeftPoint.position;

        startPosition = newCard.transform.position;
        finalPosition = cardDisplayPoint.position;

        StartCoroutine(MoveCard(newCard, startPosition, finalPosition, cardOutCurve));

        newCard.Enable();

        transitioningCards = false;
    }

    private IEnumerator MoveCard( Card card, Vector3 startPosition, Vector3 finalPosition, AnimationCurve curve)
    {
        cardProgress = 0;
        float t = 0;
        while (t < 1)
        {
            card.gameObject.transform.position = Vector3.Lerp(startPosition, finalPosition, curve.Evaluate(t));
            t += Time.deltaTime * cardMovementSpeed;
            cardProgress = t;
            yield return null;
        }
    }

    private bool StartCardIn()
    {
        return cardProgress >= 0.5f;
    }


    public void RefreshDisplay()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            if(i == selectedItemIndex)
            {
                cards[i].gameObject.transform.position = cardDisplayPoint.position;
                cards[i].Enable();
            }
            else
            {
                cards[i].gameObject.transform.position = cardInitialPoint.position;
            }
        }
    }

    public void CardSelected()
    {
        StartCoroutine(FadeMusicOut());
        PlayMenuSound(menuSelectSound);
    }

    private void PlayMenuSound(AudioClip clip)
    {
        menuSounds.clip = clip;
        menuSounds.Play();
    }

    private IEnumerator FadeMusicOut()
    {
        AudioSource source = GetComponent<AudioSource>();
        float fadeDuration = 3.0f;
        float fadeSpeed = Mathf.Abs(source.volume) / fadeDuration;

        while (!Mathf.Approximately(source.volume, 0))
        {
            source.volume = Mathf.MoveTowards(source.volume, 0, fadeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
