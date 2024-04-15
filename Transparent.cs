using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    private SpriteRenderer sr;
    private float r, g, b;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        r = sr.color.r;
        g = sr.color.g;
        b = sr.color.b;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        sr.color = new Color(r, g, b, 0.3f);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        sr.color = new Color(r, g, b, 1);
    }
}
