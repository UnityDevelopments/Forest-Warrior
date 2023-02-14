using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    private float startPosX, startPosY, startPosZ;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = gameObject.transform.position.x;
        startPosY = gameObject.transform.position.y;
        startPosZ = gameObject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x + startPosX, player.transform.position.y + startPosY, player.transform.position.z + startPosZ - distance);
    }
}
