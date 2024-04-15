using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplosin : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject hitbox;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (sr.sprite.name == "Explosion_2")
            hitbox.SetActive(false);
        else if (sr.sprite.name == "Explosion_8")
            StartCoroutine(end());
    }
    IEnumerator end()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
