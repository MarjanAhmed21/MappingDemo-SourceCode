using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform attachedPlayer;
    Camera thisCamera;
    public float blendAmount = 1f;

    // Use this for initialization
    void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player = attachedPlayer.transform.position;
        Vector3 newCamPos = player * blendAmount + transform.position * (1f - blendAmount);
        transform.position = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);
    }
}