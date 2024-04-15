using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Skeleton : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] private GameObject hpBar;
    [SerializeField] private AudioSource swordSwish;
    [SerializeField] private GameObject attackHitBox;
    [SerializeField] private GameObject xpGem;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject son;
    private float moveSpeed;
    private float hp;
    private bool deathTimer = false;
    private SpriteRenderer sr;
    void Start()
    {
        anim.SetBool("Hit", false);
        anim.SetBool("Dead", false);
        hp = 50;
        hpBar.transform.localScale = new Vector3(hp * 0.005f, 0.05f, 0.0f);
        moveSpeed = Random.Range(1f, 2f);
        attackHitBox.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (transform.position.x > 48)
            transform.position = new Vector2(34, transform.position.y);
        else if (transform.position.x < -42)
            transform.position = new Vector2(-27, transform.position.y);
        if (transform.position.y > 22)
            transform.position = new Vector2(transform.position.x, 8);
        else if (transform.position.y < -21)
            transform.position = new Vector2(transform.position.x, -7);
        if (!Manager.pause)
        {
            if (!anim.GetBool("Dead"))
            {
                if (!anim.GetBool("Hit"))
                {
                    if (transform.position.x > Manager.playerX)
                    {
                        transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                        transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
                    }
                    if (transform.position.y > Manager.playerY)
                        transform.position += new Vector3(0.0f, -moveSpeed * Time.deltaTime, 0.0f);
                    else
                        transform.position += new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f);
                    if (Math.Abs(Manager.playerX - transform.position.x) <= 2.0f && Math.Abs(Manager.playerY - transform.position.y) <= 2.0f
                        && !anim.GetBool("playerClose"))
                    {
                        anim.SetBool("playerClose", true);
                        StartCoroutine(endAttack());
                    }
                    else if (sr.sprite.name == "Attack_6")
                    {
                        swordSwish.Play();
                        attackHitBox.SetActive(true);
                    }
                    else if (sr.sprite.name == "Attack_0")
                        attackHitBox.SetActive(false);
                }
            }
            else if (!deathTimer)
            {
                deathTimer = true;
                StartCoroutine(bodyEvap()); 
            }
        }
    }
    private void OnTriggerExit2D (Collider2D other)
    {
        if (!anim.GetBool("Dead"))
        {
            if (other.gameObject.CompareTag("PlayerAttack"))
            {
                anim.SetBool("Hit", true);
                if (transform.rotation.y == 0.0f)
                    transform.position += new Vector3(-2.0f, 0.0f, 0.0f);
                else
                    transform.position += new Vector3(2.0f, 0.0f, 0.0f);
                if (Random.Range(1, 101) <= Manager.playerCritChance)
                    hp -= (Manager.playerCritDamage/100) * Manager.playerDamage;
                hp -= Manager.playerDamage;
                hpBar.transform.localScale = new Vector3(hp * 0.007f, 0.05f, 0.0f);
                if (hp <= 0)
                    dead();
            }
            if (other.gameObject.CompareTag("FireballExplosion"))
            {
                anim.SetBool("Hit", true);
                if (transform.rotation.y == 0.0f)
                    transform.position += new Vector3(-2.0f, 0.0f, 0.0f);
                else
                    transform.position += new Vector3(2.0f, 0.0f, 0.0f);
                hp -= 5;
                hpBar.transform.localScale = new Vector3(hp * 0.007f, 0.05f, 0.0f);
                if (hp <= 0)
                    dead();
            }
            if (other.gameObject.CompareTag("BombExplosion"))
            {
                anim.SetBool("Hit", true);
                if (transform.rotation.y == 0.0f)
                    transform.position += new Vector3(-2.0f, 0.0f, 0.0f);
                else
                    transform.position += new Vector3(2.0f, 0.0f, 0.0f);
                hp -= Manager.playerDamage2;
                hpBar.transform.localScale = new Vector3(hp * 0.007f, 0.05f, 0.0f);
                if (hp <= 0)
                    dead();
            }
        }
    }
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (!anim.GetBool("Dead"))
        {
            if (other.gameObject.CompareTag("PlayerAttack2"))
            {
                Destroy(other.gameObject);
                anim.SetBool("Hit", true);
                if (transform.rotation.y == 0.0f)
                    transform.position += new Vector3(-2.0f, 0.0f, 0.0f);
                else
                    transform.position += new Vector3(2.0f, 0.0f, 0.0f);
                hp -= Manager.playerDamage2;
                hpBar.transform.localScale = new Vector3(hp * 0.007f, 0.05f, 0.0f);
                if (hp <= 0)
                    dead();
            }
        }
    }
    void dead()
    {
        anim.SetBool("Dead", true);
        sr.sortingOrder = 98;
        Instantiate(xpGem, transform.position, Quaternion.Euler(0, 0, 0));
        if (Random.Range(0, 3) == 0)
            Instantiate(coin, new Vector2(transform.position.x + 0.2f, transform.position.y + 0.5f), 
                Quaternion.Euler(0, 0, 0));
        Manager.respawnEnemy(son, transform);
        Manager.respawnEnemy(son, transform);
        Manager.respawnEnemy(son, transform);
        Destroy(hpBar);
        Manager.enemyCount--;
        Manager.playerHP += Manager.playerHealOnKill;
        if (Manager.playerHP > Manager.playerMaxHP)
            Manager.playerHP = Manager.playerMaxHP;
    }
    IEnumerator bodyEvap()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.GameObject());
    }
    IEnumerator endAttack()
    {
        yield return new WaitForSeconds(0.7f);
        anim.SetBool("playerClose", false);
    }
}
