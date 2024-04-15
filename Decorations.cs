using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorations : MonoBehaviour
{
    [SerializeField] private GameObject[] deco;
    void Start()
    {
        if (Random.Range(0, 3) <= 1)
        {
            Instantiate(deco[Random.Range(0, deco.Length)], new Vector2(transform.position.x + Random.Range(-6f, 6f), 
                transform.position.y + Random.Range(-1.5f, 1.5f)), Quaternion.identity, transform);
        }
    }
}
