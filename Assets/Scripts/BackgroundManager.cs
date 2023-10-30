using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color defaultBackgroundColor;
    public AnimationCurve colorTransitionCurve;
    public float colorTransitionSpeed;
    public Material backgroundMaterial;

    private void Start()
    {
        backgroundMaterial.SetColor("_Color", defaultBackgroundColor);
    }

    public void BackgroundUpdate()
    {
        StartCoroutine(ChangeColour());
    }

    IEnumerator ChangeColour()
    {
        Color oldColor = backgroundMaterial.GetColor("_Color");

        float h, s, v;
        Color.RGBToHSV(oldColor, out h, out s, out v);

        Color newColor = Color.HSVToRGB((h + 0.16f) % 1, s, v);

        float t = 0;
        while (t < 1)
        {
            backgroundMaterial.SetColor("_Color", Color.HSVToRGB(Mathf.Lerp(h, (h + 0.16f), colorTransitionCurve.Evaluate(t)),s,v));
            t += Time.deltaTime * colorTransitionSpeed;
            yield return null;
        }
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

        float t = 0;
        while (t < 1)
        {
            backgroundMaterial.SetColor("_Color", Color.HSVToRGB(h,s,Mathf.Lerp(v, 0, colorTransitionCurve.Evaluate(t))));
            t += Time.deltaTime * colorTransitionSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(5);

        backgroundMaterial.SetColor("_Color", Color.HSVToRGB(h, s, v));
    }
}
