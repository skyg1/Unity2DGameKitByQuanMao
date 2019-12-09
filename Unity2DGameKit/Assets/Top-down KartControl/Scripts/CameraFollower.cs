using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject kart;

    private Vector3 offset;

    private void Start()
    {
        offset = kart.transform.position - transform.position;
    }

    private void Update()
    {
        transform.position = kart.transform.position - offset;
        // 其实可以直接将Kart的(x,y)赋给相机
    }
}
