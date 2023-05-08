using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonScene : MonoBehaviour
{
    public Scrollbar loadScrollbar;
    public GameObject loadScene;
    public MyButton leaveButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leaveButton.isPressed)
        {
            loadScene.SetActive(true);
            StartCoroutine(SceneLoader.LoadScene(1, loadScrollbar));
        }
    }
}
