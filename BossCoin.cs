using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoin : MonoBehaviour
{
    [SerializeField] private AudioSource coinGainedAudio;
    private float moveSpeed = 0.1f;
    void Update()
    {
        if (!Manager.pause)
        {
            if (Math.Abs(Math.Abs(Manager.playerY) - Math.Abs(transform.position.y)) <= 5.0f &&
             Math.Abs(Math.Abs(Manager.playerX) - Math.Abs(transform.position.x)) <= 5.0f)
            {
                if (transform.position.x > Manager.playerX)
                    transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
                else
                    transform.position += new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
                if (transform.position.y > Manager.playerY)
                    transform.position += new Vector3(0.0f, -moveSpeed * Time.deltaTime, 0.0f);
                else
                    transform.position += new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f);
                moveSpeed += 0.01f;
            }
            if (Math.Abs(Math.Abs(Manager.playerY) - Math.Abs(transform.position.y)) <= 0.5f &&
                Math.Abs(Math.Abs(Manager.playerX) - Math.Abs(transform.position.x)) <= 0.5f && !coinGainedAudio.isPlaying)
            {
                coinGainedAudio.Play();
                Manager.gold += 10;
            }
            if (coinGainedAudio.time >= 0.35f)
                Destroy(this.gameObject);
        }
    }
}
