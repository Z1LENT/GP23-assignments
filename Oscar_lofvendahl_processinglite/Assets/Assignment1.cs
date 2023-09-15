using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignment1 : ProcessingLite.GP21
{
    public float spaceBetweenLines = 0.2f;

    [Range(1f,255f)] public int redColorValue;
    [Range(1f, 255f)] public int greenColorValue;
    [Range(1f, 255f)] public int blueColorValue;
    [Range(1f, 255f)] public int alphaValue;
    [Range(0f, 255f)] public int CircleX;
    [Range(0f, 255f)] public int CircleY;
    [Range(0f, 255f)] public int CircleDiameter;

    // Start is called before the first frame update
    void Start()
    {
        Line(4, 7, 4, 3);
        Line(4, 5, 6, 5);
        Line(6, 7, 6, 3);

        Line(8, 5.5f, 8, 3);
        Line(8, 7, 8, 6.8f);
        Debug.Log(Height + "height");
    }


    void Update()
    {
        //Clear background
        Background(redColorValue, greenColorValue, blueColorValue);

        //Draw our art/stuff, or in this case a rectangle
        Rect(1, 1, 3, 3);
        Circle(CircleX, CircleY, CircleDiameter);

        //Prepare our stroke settings
        Stroke(128, 128, 128, 64);
        StrokeWeight(0.5f);

        //Draw our scan lines
        for (int i = 0; i < Height / spaceBetweenLines; i++)
        {
            //Increase y-cord each time loop run
            float y = i * spaceBetweenLines;

            //Draw a line from left side of screen to the right
            Line(0, y, Width, y);
        }
    }
}
