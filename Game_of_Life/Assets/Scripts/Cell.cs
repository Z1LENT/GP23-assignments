using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool alive;

    SpriteRenderer spriteRenderer;

    private Color aliveColor = Color.green;
    private Color deadColor = Color.black;
    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = alive;

        if (alive)
        {
            spriteRenderer.color = aliveColor;
        }
        else
        {
            spriteRenderer.color = deadColor;
        }
        
    }
}
