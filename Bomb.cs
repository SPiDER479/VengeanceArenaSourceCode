using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    void Start()
    {
        StartCoroutine(ttl());
        if (transform.rotation.y < 0)
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 5), ForceMode2D.Impulse);
        else
            GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 5), ForceMode2D.Impulse);
    }
    IEnumerator ttl()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
