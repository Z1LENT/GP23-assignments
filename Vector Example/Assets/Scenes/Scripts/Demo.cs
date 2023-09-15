using System.Collections;
using System.Collections.Generic;
using ProcessingLite;
using UnityEngine;

public class Demo : ProcessingLite.GP21
{
    public Vector2 circlePosition;
    public Vector2 distance;
    public float maxspeed = 0.5f;


    void Start()
    {
        circlePosition = new Vector2(Width / 2, Height / 2);
    }


    void Update()
    {

        Background(0);
        Circle(circlePosition.x, circlePosition.y, 1);

        if (Input.GetMouseButtonDown(0))
        {
            circlePosition.x = MouseX;
            circlePosition.y = MouseY;
            distance = Vector2.zero;
        }

        if (Input.GetMouseButton(0))
        {
            Line(circlePosition.x, circlePosition.y, MouseX, MouseY);
        }

        if (Input.GetMouseButtonUp(0))
        {
            distance = (new Vector2(MouseX, MouseY) - circlePosition) * 0.01f;
        }

        if(distance.magnitude > maxspeed)
        {
            distance.Normalize();
            distance *= maxspeed;
        }

        if (circlePosition.x > Width || circlePosition.x < 0)
        {
            distance.x *= -1;
        }

        if (circlePosition.y > Height || circlePosition.y < 0)
        {
            distance.y *= -1;
        }

        circlePosition += distance;
        Circle(circlePosition.x, circlePosition.y, 1);
    }
}