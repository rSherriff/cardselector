using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UALBackground2023 : MonoBehaviour
{ 
    public Gradient backgroundGradient;
    public Gradient textGradient;
    public Gradient deviceGradient;

    public AnimationCurve colorTransitionCurve;
    public float colorTransitionSpeed;
    public Material backgroundMaterial;
    public UALDevice device;

    private float currentGradientValue = 0f;
    public float gradientPhaseLength;


    public void OnCardsLoaded()
    {
        backgroundMaterial.SetColor("_Color", backgroundGradient.Evaluate(0));

        device.UpdateColors(deviceGradient.Evaluate(0));

        Card currentCard = SelectManager.Instance.GetCurrentCard();
        currentCard.UpdateColor(textGradient.Evaluate(0));
    }

    public void BackgroundUpdate()
    {
        StartCoroutine(ChangeColour());
    }

    IEnumerator ChangeColour()
    {
        currentGradientValue %= 1;
        float t = currentGradientValue; 

        Card lastCard = SelectManager.Instance.GetLastCard();
        Card currentCard = SelectManager.Instance.GetCurrentCard();

        while (t <= currentGradientValue + gradientPhaseLength)
        {
            Color backgroundColor = backgroundGradient.Evaluate(t);
            backgroundMaterial.SetColor("_Color", backgroundColor);

            Color textColor = textGradient.Evaluate(t);
            lastCard.UpdateColor(textColor);
            currentCard.UpdateColor(textColor);

            device.UpdateColors(deviceGradient.Evaluate(t));

            t += Time.deltaTime * colorTransitionSpeed;
            yield return null;
        }

        currentGradientValue += gradientPhaseLength;
        backgroundMaterial.SetColor("_Color", backgroundGradient.Evaluate(currentGradientValue));
        lastCard.UpdateColor(textGradient.Evaluate(currentGradientValue));
        currentCard.UpdateColor(textGradient.Evaluate(currentGradientValue));
        device.UpdateColors(deviceGradient.Evaluate(currentGradientValue));
    }

    public void BackgroundFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color oldColor = backgroundMaterial.GetColor("_Color");

        float h, s, v;
        Color.RGBToHSV(oldColor, out h, out s, out v);

        Card lastCard = SelectManager.Instance.GetLastCard();
        Card currentCard = SelectManager.Instance.GetCurrentCard();

        float t = 0;
        while (t < 1)
        {
            Color newColor = Color.HSVToRGB(h, s, Mathf.Lerp(v, 0, colorTransitionCurve.Evaluate(t)));

            //backgroundMaterial.SetColor("_Color", newColor);
            //lastCard.UpdateColor(newColor);
            //currentCard.UpdateColor(newColor);

            t += Time.deltaTime * colorTransitionSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(5);

        backgroundMaterial.SetColor("_Color", Color.HSVToRGB(h, s, v));
    }
}
