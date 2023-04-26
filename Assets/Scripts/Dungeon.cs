using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dungeon : MonoBehaviour
{
    private MyButton openButton;
    public GameObject loadScene;
    public Scrollbar loadScrollbar;

    // Start is called before the first frame update
    void Start()
    {
        openButton = GameObject.Find("OpenButton").GetComponent<MyButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if(openButton.isPressed)
        {
            loadScene.SetActive(true);
            StartCoroutine(SceneLoader.LoadScene(2, loadScrollbar));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            openButton.gameObject.GetComponent<Image>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            openButton.gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
