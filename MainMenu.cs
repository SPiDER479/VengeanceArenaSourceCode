using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource au;
    public void Start()
    {
        if (!File.Exists("baseStats.txt") || !File.Exists("coin.txt") || !File.Exists("secondary.txt"))
            reset();
    }
    public void start()
    {
        SceneManager.LoadScene("Forest");
    }
    public void shop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void res()
    {
        au.Play();
        reset();
    }
    public void how()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void bestiary()
    {
        SceneManager.LoadScene("Bestiary");
    }
    public void quit()
    {
        Application.Quit();
    }
    private void reset()
    {
        StreamWriter w = new StreamWriter("baseStats.txt");
        w.WriteLine(10);
        w.WriteLine(0);
        w.WriteLine(0);
        w.WriteLine(10);
        w.WriteLine(100);
        w.Write(0);
        w.Close();

        w = new StreamWriter("coin.txt");
        w.WriteLine(0);
        w.Write(2);
        w.Close();
        
        w = new StreamWriter("secondary.txt");
        w.WriteLine(-1);
        w.WriteLine(0);
        w.WriteLine(false);
        w.Write(false);
        w.Close();
    }
}