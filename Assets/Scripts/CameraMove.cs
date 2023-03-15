using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    private float startPosX, startPosZ;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = gameObject.transform.position.x;
        startPosZ = gameObject.transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x + startPosX, transform.position.y, player.transform.position.z + startPosZ - distance);
    }
}
