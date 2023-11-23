using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Reflection;
using Unity.VisualScripting;

public class ModelCard : CountCard
{
    public TextMeshPro title;
    public TextMeshPro credit;

    public float rotationSpeed;
    public Transform modelGimbal;

    private Work model;

    public void Setup(Work model)
    {
        this.model = model;

        this.gameObject.name = model.title + " Card";

        title.text = model.title;
        credit.text = model.credit;
    }

    private void Update()
    {
        if(active && Input.GetMouseButton(0))
        {
            //modelGimbal.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationSpeed);
            modelGimbal.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationSpeed,Space.World);
        }
    }

    override public void UpdateColor(Color newColor)
    {
        title.color = newColor;
        credit.color = newColor;
    }

    override public void UpdateFont(TMP_FontAsset font)
    {
        title.font = font;
        credit.font = font;
    }
}
