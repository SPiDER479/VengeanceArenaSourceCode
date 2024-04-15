using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] private Button a;
    [SerializeField] private GameObject[] bg;
    void Start()
    {
        GameObject g = Instantiate(bg[Random.Range(0, bg.Length)], new Vector2(0, 0), Quaternion.identity);
        g.transform.localScale = new Vector2(1.1f, 1);
        a.onClick.AddListener(press);
    }
    private void press()
    {
        SceneManager.LoadScene("Menu");
    }
}