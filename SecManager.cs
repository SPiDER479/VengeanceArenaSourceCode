using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SecManager : MonoBehaviour
{
    private int chosen, coins, fireballPrice, bombPrice;
    private bool fireballBought, bombBought;
    [SerializeField] private Button[] b;
    [SerializeField] private Button[] choices;
    [SerializeField] private GameObject[] bg;

    [SerializeField] private AudioSource au;
    [SerializeField] private Text coinDisplay;
    [SerializeField] private Text fireballBoughtDisplay;
    [SerializeField] private Text bombBoughtDisplay;
    void Start()
    {
        GameObject g = Instantiate(bg[Random.Range(0, bg.Length)], new Vector2(0, 0), Quaternion.identity);
        g.transform.localScale = new Vector2(1.1f, 1);

        fireballPrice = 20;
        bombPrice = 50;
        
        StreamReader r = new StreamReader("coin.txt");
        coins = int.Parse(r.ReadLine());
        r.Close();
        coinDisplay.text = "Gold: " + coins;
        
        r = new StreamReader("secondary.txt");
        chosen = int.Parse(r.ReadLine());
        int.Parse(r.ReadLine());
        fireballBought = bool.Parse(r.ReadLine());
        bombBought = bool.Parse(r.ReadLine());
        r.Close();

        b[0].enabled = false;
        if (fireballBought)
        {
            fireballBoughtDisplay.text = "Bought";
            b[1].enabled = false;
        }
        if (bombBought)
        {
            bombBoughtDisplay.text = "Bought";
            b[2].enabled = false;
        }
        choices[chosen + 1].enabled = false;
    }
    public void fireballBuy()
    {
        if (coins >= fireballPrice && !fireballBought)
        {
            au.Play();
            coins -= fireballPrice;
            fireballBought = true;
            coinDisplay.text = "Gold: " + coins;
            fireballBoughtDisplay.text = "Bought";
            b[1].enabled = false;

            StreamReader r = new StreamReader("coin.txt");
            int.Parse(r.ReadLine());
            int p = int.Parse(r.ReadLine());
            r.Close();
            
            StreamWriter w = new StreamWriter("coin.txt");
            w.WriteLine(coins);
            w.Write(p);
            w.Close();
            
            r = new StreamReader("secondary.txt");
            int x = int.Parse(r.ReadLine());
            int y = int.Parse(r.ReadLine());
            bool.Parse(r.ReadLine());
            bool q = bool.Parse(r.ReadLine());
            r.Close();
            
            w = new StreamWriter("secondary.txt");
            w.WriteLine(x);
            w.WriteLine(y);
            w.WriteLine(fireballBought);
            w.Write(bombBought);
            w.Close();
        }
    }
    public void bombBuy()
    {
        if (coins >= bombPrice && !bombBought)
        {
            au.Play();
            coins -= bombPrice;
            bombBought = true;
            coinDisplay.text = "Gold: " + coins;
            bombBoughtDisplay.text = "Bought";
            b[2].enabled = false;

            StreamReader r = new StreamReader("coin.txt");
            int.Parse(r.ReadLine());
            int p = int.Parse(r.ReadLine());
            r.Close();
            
            StreamWriter w = new StreamWriter("coin.txt");
            w.WriteLine(coins);
            w.Write(p);
            w.Close();
            
            r = new StreamReader("secondary.txt");
            int x = int.Parse(r.ReadLine());
            int y = int.Parse(r.ReadLine());
            bool.Parse(r.ReadLine());
            bool q = bool.Parse(r.ReadLine());
            r.Close();
            
            w = new StreamWriter("secondary.txt");
            w.WriteLine(x);
            w.WriteLine(y);
            w.WriteLine(fireballBought);
            w.Write(bombBought);
            w.Close();
        }
    }
    public void none()
    {
        au.Play();
        StreamWriter w = new StreamWriter("secondary.txt");
        w.WriteLine(-1);
        w.WriteLine(0);
        w.WriteLine(fireballBought);
        w.Write(bombBought);
        w.Close();

        choices[0].enabled = false;
        choices[1].enabled = true;
        choices[2].enabled = true;
    }
    public void fireball()
    {
        if (fireballBought)
        {
            au.Play();
            StreamWriter w = new StreamWriter("secondary.txt");
            w.WriteLine(0);
            w.WriteLine(20);
            w.WriteLine(fireballBought);
            w.Write(bombBought);
            w.Close();

            choices[0].enabled = true;
            choices[1].enabled = false;
            choices[2].enabled = true;
        }
    }
    public void bomb()
    {
        if (bombBought)
        {
            au.Play();
            StreamWriter w = new StreamWriter("secondary.txt");
            w.WriteLine(1);
            w.WriteLine(10);
            w.WriteLine(fireballBought);
            w.Write(bombBought);
            w.Close();

            choices[0].enabled = true;
            choices[1].enabled = true;
            choices[2].enabled = false;
        }
    }
    public void shop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void start()
    {
        SceneManager.LoadScene("Forest");
    }
}
