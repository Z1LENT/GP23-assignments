using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name : ProcessingLite.GP21
{
    [Range(0f, 100f)] public int LineOne;
    [Range(0f, 100f)] public int LineTwo;
    [Range(0f, 100f)] public int LineThree;
    [Range(0f, 100f)] public int LineFour;

    float frame = 0;

    public float spaceBetweenLines = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Height + "height");
    }

    // Update is called once per frame
    void Update()
    {

        frame += 0.5f;

        if(frame % 3 == 0)
        {
            Background(255, 0, 0);
        }
        else
        {
            Background(139, 0, 0);
        }

        StrokeWeight(1 + frame % 5);

        Line(LineOne, LineTwo, LineThree, LineFour);
        Circle(2, 7, 3);

        Line(6, 8, 4, 7);
        Line(6, 6, 4, 7);
        Line(6, 6, 4, 5);
        Line(9, 8, 7, 6);
        Line(7, 6, 9, 5);
        Line(13, 5, 12, 8);
        Line(10, 5, 12, 8);
        Line(13, 6, 10, 6);
        Line(14, 8, 14, 5);
        Line(14, 8, 15, 7);
        Line(15, 7, 14, 6);
        Line(14, 6, 16, 5);

        Line(0, 10, 1, 0);
        Line(0, 9, 2, 0);
        Line(0, 8, 3, 0);
        Line(0, 7, 4, 0);
        Line(0, 6, 5, 0);
        Line(0, 5, 6, 0);
        Line(0, 4, 7, 0);
        Line(0, 3, 8, 0);
        Line(0, 2, 9, 0);
        Line(0, 1, 10, 0);



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
