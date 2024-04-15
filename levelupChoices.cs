using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class levelupChoices : MonoBehaviour
{
    [SerializeField] private Button[] bt = new Button[6];
    private CanvasRenderer[] cr = new CanvasRenderer[6];
    
    [SerializeField] private Text[] t = new Text[6];
    private CanvasRenderer[] crt = new CanvasRenderer[6];

    [SerializeField] private Text[] stats = new Text[9];
    
    private static int a, b, c;
    private static bool changed;
    private void Start()
    {
        changed = true;
        bt[0].onClick.AddListener(choice0);
        bt[1].onClick.AddListener(choice1);
        bt[2].onClick.AddListener(choice2);
        bt[3].onClick.AddListener(choice3);
        bt[4].onClick.AddListener(choice4);
        bt[5].onClick.AddListener(choice5);
        for (int i = 0; i < 6; i++)
        {
            cr[i] = bt[i].GetComponent<CanvasRenderer>();
            crt[i] = t[i].GetComponent<CanvasRenderer>();
        }
    }
    public static void change()
    {
        a = Random.Range(0, 3);
        b = Random.Range(3, 6);
        changed = false;
    }
    void Update()
    {
        if (!changed)
        {
            stats[0].text = "HP: " + Manager.playerHP;
            stats[1].text = "Gold: " + Manager.gold;
            stats[2].text = "Level " + Manager.playerLevel;
            stats[3].text = "Damage: " + Manager.playerDamage;
            stats[4].text = "Crit Chance: " + Manager.playerCritChance + "%";
            stats[5].text = "Crit Damage: " + Manager.playerCritDamage + "%";
            stats[6].text = "Heal on Kill: " + Manager.playerHealOnKill;
            stats[7].text = "Max HP: " + Manager.playerMaxHP;
            stats[8].text = "Extra XP Gain: " + Manager.playerExtraXP + "%";
            for (int i = 0; i < 6; i++)
                deactivate(i);
            activate(a);
            activate(b);
            changed = true;
        }
    }
    void activate(int i)
    {
        bt[i].enabled = true;
        t[i].enabled = true;
        cr[i].SetAlpha(1);
        crt[i].SetAlpha(1);
    }
    void deactivate(int i)
    {
        bt[i].enabled = false;
        t[i].enabled = false;
        cr[i].SetAlpha(0);
        crt[i].SetAlpha(0);
    }
    private void choice0()
    {
        Manager.choicePicked = true;
        Manager.playerDamage += 5;
    }
    private void choice1()
    {
        Manager.choicePicked = true;
        Manager.playerCritChance += 5;
    }
    private void choice2()
    {
        Manager.choicePicked = true;
        Manager.playerCritDamage += 10;
    }
    private void choice3()
    {
        Manager.choicePicked = true;
        Manager.playerHealOnKill += 5;
    }
    private void choice4()
    {
        Manager.choicePicked = true;
        Manager.playerMaxHP += 50;
    }
    private void choice5()
    {
        Manager.choicePicked = true;
        Manager.playerExtraXP += 10;
    }
}