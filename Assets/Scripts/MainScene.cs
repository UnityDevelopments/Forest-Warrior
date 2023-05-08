using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public Scrollbar loadScrollbar;
    public GameObject loadScene;
    public GameObject buttonPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPlay()
    {
        buttonPlay.SetActive(false);
        loadScene.SetActive(true);
        StartCoroutine(SceneLoader.LoadScene(1, loadScrollbar));
    }
}
