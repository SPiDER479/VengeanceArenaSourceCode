using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    private int playerDamage;
    private int playerHealOnKill;
    private float playerMaxHP;
    private int playerCritChance;
    private float playerCritDamage;
    private float playerExtraXP;
    private int gold;
    private int goldReq;

    private int maxPD;
    private int maxPHOK;
    private int maxPMHP;
    private int maxPCC;
    private int maxPCD;
    private int maxPEXP;
    
    [SerializeField] private Text goldAmount;
    [SerializeField] private Text goldReqAmount;
    [SerializeField] private Text[] t = new Text[6];
    [SerializeField] private AudioSource a;
    [SerializeField] private GameObject[] go = new GameObject[2];
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject[] bg;
    void Start()
    {
        GameObject g = Instantiate(bg[Random.Range(0, bg.Length)], new Vector2(0, 0), Quaternion.identity);
        g.transform.localScale = new Vector2(1.1f, 1);
        
        maxPD = 20;
        maxPHOK = 25;
        maxPMHP = 200;
        maxPCC = 25;
        maxPCD = 50;
        maxPEXP = 50;
        
        StreamReader r = new StreamReader("coin.txt");
        gold = int.Parse(r.ReadLine());
        goldReq = int.Parse(r.ReadLine());
        r.Close();
        
        r = new StreamReader("baseStats.txt");
        playerDamage = int.Parse(r.ReadLine());
        playerCritChance = int.Parse(r.ReadLine());
        playerCritDamage = int.Parse(r.ReadLine());
        playerHealOnKill = int.Parse(r.ReadLine());
        playerMaxHP = int.Parse(r.ReadLine());
        playerExtraXP = int.Parse(r.ReadLine());
        r.Close();
        read();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Forest");
    }
    public void Secondary()
    {
        SceneManager.LoadScene("Secondary");
    }
    public void choice0()
    {
        if (gold >= goldReq && playerDamage < maxPD)
        {
            playerDamage += 2;
            write();
            read();
        }
    }
    public void choice1()
    {
        if (gold >= goldReq && playerCritChance < maxPCC)
        {
            playerCritChance += 5;
            write();
            read();
        }
    }
    public void choice2()
    {
        if (gold >= goldReq && playerCritDamage < maxPCD)
        {
            playerCritDamage += 10;
            write();
            read();
        }
    }
    public void choice3()
    {
        if (gold >= goldReq && playerHealOnKill < maxPHOK)
        {
            playerHealOnKill += 3;
            write();
            read();
        }
    }
    public void choice4()
    {
        if (gold >= goldReq && playerMaxHP < maxPMHP)
        {
            playerMaxHP += 20;
            write();
            read();
        }
    }
    public void choice5()
    {
        if (gold >= goldReq && playerExtraXP < maxPEXP)
        {
            playerExtraXP += 10;
            write();
            read();
        }
    }
    public void write()
    {
        gold -= goldReq;
        goldReq = (int)(goldReq * 1.5f);
        a.Play();
        StreamWriter w = new StreamWriter("coin.txt");
        w.WriteLine(gold);
        w.Write(goldReq);
        w.Close();
        w = new StreamWriter("baseStats.txt");
        w.WriteLine(playerDamage);
        w.WriteLine(playerCritChance);
        w.WriteLine(playerCritDamage);
        w.WriteLine(playerHealOnKill);
        w.WriteLine(playerMaxHP);
        w.Write(playerExtraXP);
        w.Close();
    }
    public void read()
    {
        goldAmount.text = "Gold: " + gold;
        goldReqAmount.text = "Required: " + goldReq;
        StreamReader r = new StreamReader("baseStats.txt");
        t[0].text = int.Parse(r.ReadLine()).ToString();
        t[1].text = int.Parse(r.ReadLine()).ToString();
        t[2].text = int.Parse(r.ReadLine()).ToString();
        t[3].text = int.Parse(r.ReadLine()).ToString();
        t[4].text = int.Parse(r.ReadLine()).ToString();
        t[5].text = int.Parse(r.ReadLine()).ToString();
        r.Close();
        pips();
    }
    public void pips()
    {
        int[] m = new int[6];
        m[0] = playerDamage - 10;
        m[1] = playerCritChance;
        m[2] = (int)playerCritDamage;
        m[3] = playerHealOnKill - 10;
        m[4] = (int)playerMaxHP - 100;
        m[5] = (int)playerExtraXP;
        foreach (Transform child in container.transform)
            Destroy(child.gameObject);
        for (int i = 0; i < 5; i++, m[0] -= 2, m[1] -= 5, m[2] -= 10, m[3] -= 3, m[4] -= 20, m[5] -= 10)
        {
            GameObject g;
            
            if (m[0] > 0)
                g = Instantiate(go[0], new Vector2(-7 + (i * 1.1f), -0.7f), Quaternion.identity, container.transform);
            else
                g = Instantiate(go[1], new Vector2(-7 + (i * 1.1f), -0.7f), Quaternion.identity, container.transform);
            g.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            
            if (m[1] > 0)
                g = Instantiate(go[0], new Vector2(-7 + (i * 1.1f), -2.1f), Quaternion.identity, container.transform);
            else
                g = Instantiate(go[1], new Vector2(-7 + (i * 1.1f), -2.1f), Quaternion.identity, container.transform);
            g.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            
            if (m[2] > 0)
                g = Instantiate(go[0], new Vector2(-7 + (i * 1.1f), -3.6f), Quaternion.identity, container.transform);
            else
                g = Instantiate(go[1], new Vector2(-7 + (i * 1.1f), -3.6f), Quaternion.identity, container.transform);
            g.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            
            if (m[3] > 0)
                g = Instantiate(go[0], new Vector2(3 + (i * 1.1f), -0.7f), Quaternion.identity, container.transform);
            else
                g = Instantiate(go[1], new Vector2(3 + (i * 1.1f), -0.7f), Quaternion.identity, container.transform);
            g.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            
            if (m[4] > 0)
                g = Instantiate(go[0], new Vector2(3 + (i * 1.1f), -2.1f), Quaternion.identity, container.transform);
            else
                g = Instantiate(go[1], new Vector2(3 + (i * 1.1f), -2.1f), Quaternion.identity, container.transform);
            g.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            
            if (m[5] > 0)
                g = Instantiate(go[0], new Vector2(3 + (i * 1.1f), -3.6f), Quaternion.identity, container.transform);
            else
                g = Instantiate(go[1], new Vector2(3 + (i * 1.1f), -3.6f), Quaternion.identity, container.transform);
            g.GetComponent<SpriteRenderer>().sortingOrder = 1000;
        }
    }
}
