using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMagasin : MonoBehaviour
{
    private bool OpenMagas = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (OpenMagas)
            {
                OpenMagas = false;
                Debug.Log("close");
            }
            else
            {
                OpenMagas = true;
                Debug.Log("open");
            }
        }
    }

    void OnGUI()
    {
        if(OpenMagas)
        {
            SceneManager.LoadScene("Magasin", LoadSceneMode.Additive);
        }
    }
}
