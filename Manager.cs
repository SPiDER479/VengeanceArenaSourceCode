using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    public static float playerMoveSpeed;
    public static float playerX, playerY;
    public static float playerHP;
    public static float playerXP;
    public static float levelupXP;
    public static int playerLevel;
    
    [SerializeField] private GameObject Goblin;
    [SerializeField] private GameObject Skeleton;
    [SerializeField] private GameObject FlyingEye;
    [SerializeField] private GameObject Mushroom;
    public static int enemyCount;
    [SerializeField] private Text waveDisplay;
    public static int waveNumber;
    [SerializeField] private Canvas waveNumberCanvas;
    [SerializeField] private Canvas levelupCanvas;
    public static bool choicePicked;
    public static bool pause;
    
    public static int playerSecondary;
    public static int playerDamage2;
    public static int secondaryTimer;
    private bool timerStarted;
    [SerializeField] private Text timerDisplay;
    
    public static int playerDamage;
    public static int playerHealOnKill;
    public static float playerMaxHP;
    public static int playerCritChance;
    public static float playerCritDamage;
    public static float playerExtraXP;
    public static int gold;

    [SerializeField] private AudioSource normalMusic;
    [SerializeField] private AudioSource bossMusic;
    void Start()
    {
        StreamReader r = new StreamReader("coin.txt");
        gold = int.Parse(r.ReadLine());
        r.Close();
        
        r = new StreamReader("baseStats.txt");
        playerDamage = int.Parse(r.ReadLine());
        playerCritChance = int.Parse(r.ReadLine());
        playerCritDamage = int.Parse(r.ReadLine());
        playerHealOnKill = int.Parse(r.ReadLine());
        playerMaxHP = int.Parse(r.ReadLine());
        playerExtraXP = int.Parse(r.ReadLine());
        r.Close();
        
        r = new StreamReader("secondary.txt");
        playerSecondary = int.Parse(r.ReadLine());
        playerDamage2 = int.Parse(r.ReadLine());
        r.Close();
        if (playerSecondary == -1)
        {
            timerDisplay.text = "";
            secondaryTimer = 16;
        }
        else
            secondaryTimer = 15;
        timerStarted = false;

        playerHP = playerMaxHP;
        waveNumber = 0;
        enemyCount = 0;
        playerXP = 0;
        levelupXP = 10;
        playerLevel = 1;
        
        levelupCanvas.enabled = false;
        Player = Instantiate(Player, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(0, 0, 0));
        playerMoveSpeed = 5;
    }
    public static void spawnEnemy(GameObject enemy)
    {
        enemyCount++;
        int x = Random.Range(0, 2);
        int y = Random.Range(0, 2);
        if (x == 0 && y == 0)
        {
            Instantiate(enemy, new Vector3(10.3f + playerX, Random.Range(-5.5f, 5.5f) + playerY, 0.0f), Quaternion.Euler(0, 0, 0));
        }
        else if (x == 0 && y == 1)
        {
            Instantiate(enemy, new Vector3(-10.3f + playerX, Random.Range(-5.5f, 5.5f) + playerY, 0.0f), Quaternion.Euler(0, 0, 0));
        }
        else if (x == 1 && y == 0)
        {
            Instantiate(enemy, new Vector3(Random.Range(-10.3f, 10.3f) + playerX, 5.5f + playerY, 0.0f), Quaternion.Euler(0, 0, 0));
        }
        else if (x == 1 && y == 1)
        {
            Instantiate(enemy, new Vector3(Random.Range(-10.3f, 10.3f) + playerX, -5.5f + playerY, 0.0f), Quaternion.Euler(0, 0, 0));
        }
    }
    public static void respawnEnemy(GameObject enemy, Transform pos)
    {
        enemyCount++;
        Instantiate(enemy, pos.position, pos.rotation);
    }
    void Update()
    {
        if (secondaryTimer == 15 && !timerStarted)
        {
            timerDisplay.text = "Secondary Cooldown: 15";
            timerDisplay.color = Color.green;
            timerStarted = true;
            StartCoroutine(timer());
        }
        if (playerHP > 0)
        {
            playerX = Player.transform.position.x;
            playerY = Player.transform.position.y;
        }
        if (enemyCount == 0)
        {
            waveDisplay.text = "Wave: " + ++waveNumber;
            if (waveNumber == 15)
            {
                normalMusic.Stop();
                bossMusic.Play();
                spawnEnemy(FlyingEye);
            }
            if (waveNumber == 16)
            {
                waveDisplay.text = "Wave: " + 15;
                enemyCount = -1;
                StartCoroutine(won());
            }
            else
            {
                for (int i = 1; i <= waveNumber; i++)
                    spawnEnemy(Goblin);
                if (waveNumber % 2 == 0)
                {
                    for (int i = 1; i <= waveNumber/2; i++)
                        spawnEnemy(Skeleton);
                }
                if (waveNumber % 3 == 0)
                {
                    for (int i = 1; i <= waveNumber/3; i++)
                        spawnEnemy(Mushroom);
                }
            }
        }
        if (playerXP >= levelupXP)
        {
            pause = true;
            timerDisplay.text = "";
            levelupChoices.change();
            levelupCanvas.enabled = true;
            playerLevel++;
            playerXP -= levelupXP;
            levelupXP *= 1.5f;
        }
        if (choicePicked)
        {
            pause = false;
            choicePicked = false;
            levelupCanvas.enabled = false;
            if (secondaryTimer == 0)
                timerDisplay.text = "Secondary Ready";
        }
    }
    IEnumerator won()
    {
        yield return new WaitForSeconds(5);
        StreamReader r = new StreamReader("coin.txt");
        int l = int.Parse(r.ReadLine());
        l = int.Parse(r.ReadLine());
        r.Close();
        StreamWriter w = new StreamWriter("coin.txt", false);
        w.WriteLine(Manager.gold.ToString());
        w.Write(l);
        w.Close();
        SceneManager.LoadScene("Won");
    }
    IEnumerator timer()
    {
        yield return new WaitForSeconds(1);
        if (!pause)
        {
            if (secondaryTimer != 0)
            {
                timerDisplay.text = "Secondary Cooldown: " + --secondaryTimer;
                StartCoroutine(timer());
            }
            else
            {
                timerDisplay.text = "Secondary Ready";
                timerDisplay.color = Color.red;
                timerStarted = false;
            }
        }
        else
            StartCoroutine(timer());
    }
}
