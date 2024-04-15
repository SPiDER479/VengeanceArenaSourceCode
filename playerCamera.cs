using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private void Start()
    {
        cam.orthographicSize = 1.0f;
    }
    void Update()
    {
        transform.position = new Vector3(Manager.playerX, Manager.playerY, -10.0f);
        if (cam.orthographicSize < 5.0f)
            cam.orthographicSize += 1 * Time.deltaTime;
        else
            cam.orthographicSize = 5.0f;
    }
}
