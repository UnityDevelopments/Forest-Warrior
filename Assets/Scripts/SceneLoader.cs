using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static IEnumerator LoadScene(int id, Scrollbar scrollbar = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(id);
        while (!operation.isDone)
        {
            if(scrollbar != null) scrollbar.size = operation.progress / 0.9f;
            yield return null;
        }
    }
}
