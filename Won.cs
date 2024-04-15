using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Won : MonoBehaviour
{
    [SerializeField] private Button b;
    [SerializeField] private GameObject[] bg;
    void Start()
    {
        GameObject g = Instantiate(bg[Random.Range(0, bg.Length)], new Vector3(0, 0, 100), Quaternion.identity);
        g.transform.localScale = new Vector2(1.1f, 1);
        b.onClick.AddListener(press);
    }
    private void press()
    {
        SceneManager.LoadScene("Menu");
    }
}
