using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadXGenerator : MonoBehaviour
{
    public GameObject roadY;
    public Transform roadBoard;
    public Transform kart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            float dist = Random.Range(50, 100);

            Debug.Log("在水平两侧生成纵向赛道");
            Instantiate(roadY, kart.position - new Vector3(dist, 0), Quaternion.identity).transform.SetParent(roadBoard);
            Instantiate(roadY, kart.position + new Vector3(dist, 0), Quaternion.identity).transform.SetParent(roadBoard);
        }
    }
}
