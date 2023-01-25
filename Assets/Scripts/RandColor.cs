using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandColor : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    private Color customColor;
    private float minValueChangeColor = 0;
    private float maxValueChangeColor = 0.8f;
    private float colorMultiplier = 100;
    private float red = 0;
    private float green = 0;
    private float blue = 0;
    private float alpha = 1.0f;

    void Start()
    {
        red = randColorValue();
        green = randColorValue();
        blue = randColorValue();
    }

    float randColorValue()
    {
        float value = Random.Range(minValueChangeColor, maxValueChangeColor);
        return value;
    }

    public void changeColorSphere(float distance)
    {
        float newRed = red * distance;
        float newGreen = green * distance;
        float newBlue = blue * distance;
        customColor = new Color(newRed, newGreen, newBlue, alpha);
        renderer.material.SetColor("_Color", customColor);
    }


}
