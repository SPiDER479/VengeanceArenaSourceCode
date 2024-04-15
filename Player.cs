using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Player : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] private AudioSource swordSwish;
    [SerializeField] private AudioSource walkSound;
    private bool isWalking, isDead;
    [SerializeField] private GameObject attackHitBox;
    private float moveSpeed;
    [SerializeField] private GameObject hpBar;
    [SerializeField] private GameObject xpBar;

    [SerializeField] private GameObject[] sideAttack;
    void Start()
    {
        moveSpeed = Manager.playerMoveSpeed;
        anim.SetFloat("Speed", 0.0f);
        anim.SetBool("Attacking", false);
        attackHitBox.SetActive(false);
    }
    void Update()
    {
        if (!Manager.pause)
        {
            if (Manager.playerHP <= 0)
            {
                isDead = true;
                StreamReader r = new StreamReader("coin.txt");
                int l = int.Parse(r.ReadLine());
                l = int.Parse(r.ReadLine());
                r.Close();
                StreamWriter w = new StreamWriter("coin.txt", false);
                w.WriteLine(Manager.gold.ToString());
                w.Write(l);
                w.Close();
                Destroy(this.gameObject);
                SceneManager.LoadScene("Menu");
            }
            else
            {
                hpBar.transform.localScale = new Vector3(Manager.playerHP/Manager.playerMaxHP, 0.05f, 0);
                xpBar.transform.localScale = new Vector3(Manager.playerXP/Manager.levelupXP, 0.05f, 0);
                if (!anim.GetBool("Attacking"))
                {
                     attackHitBox.SetActive(false);
                    if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
                    {
                        isWalking = true;
                        if (Input.GetKey("d"))
                        {
                            anim.SetFloat("Speed", 1.0f);
                            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        }
                        else if (Input.GetKey("a"))
                        {
                            anim.SetFloat("Speed", 1.0f);
                            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
                            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                        }

                        if (Input.GetKey("w"))
                        {
                            anim.SetFloat("Speed", 1.0f);
                            transform.position += new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f);
                        }
                        else if (Input.GetKey("s"))
                        {
                            anim.SetFloat("Speed", 1.0f);
                            transform.position += new Vector3(0.0f, -moveSpeed * Time.deltaTime, 0.0f);
                        }
                    }
                    else
                    {
                        isWalking = false;
                        anim.SetFloat("Speed", 0.0f);
                    }

                    if (isWalking && !walkSound.isPlaying)
                        walkSound.Play();
                    else if (!isWalking)
                        walkSound.Stop();
                }
                else
                    attackHitBox.SetActive(true);
                if (Input.GetButton("Fire1"))
                {
                    swordSwish.Play();
                    anim.SetBool("Attacking", true);
                }
                
                if (Input.GetButton("Fire2") && Manager.secondaryTimer == 0 && Manager.playerSecondary != -1)
                {
                    Manager.secondaryTimer = 15;
                    Instantiate(sideAttack[Manager.playerSecondary], transform.position, transform.rotation);
                }
                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isDead)
        {
            if (other.gameObject.CompareTag("GoblinAttack"))
                Manager.playerHP -= 2;
            else if (other.gameObject.CompareTag("SkeletonAttack"))
                Manager.playerHP -= 5;
            else if (other.gameObject.CompareTag("SkeletonSonAttack"))
                Manager.playerHP -= 1;
            else if (other.gameObject.CompareTag("MushroomAttack"))
                Manager.playerHP -= 10;
            else if (other.gameObject.CompareTag("FlyingEyeAttack"))
                Manager.playerHP -= 20;
            else if (other.gameObject.CompareTag("FlyingEyeSonAttack"))
            {
                Debug.Log("yes");
                Manager.playerHP -=15;
            }
        }
    }
}