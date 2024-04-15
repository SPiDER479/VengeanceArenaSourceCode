using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    void Start()
    {
        StartCoroutine(ttl());
    }
    private void Update()
    {
        if (transform.rotation.y < 0)
            transform.position = new Vector2(transform.position.x - 2 * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x + 2 * Time.deltaTime, transform.position.y);
    }
    IEnumerator ttl()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
