using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadYGenerator : MonoBehaviour
{
    public GameObject roadX;
    public Transform roadBoard;
    public Transform kart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            float dist = Random.Range(50, 100);

            Debug.Log("在纵向两侧生成横向赛道");
            Instantiate(roadX, kart.position - new Vector3(0, dist), Quaternion.identity).transform.SetParent(roadBoard);
            Instantiate(roadX, kart.position + new Vector3(0, dist), Quaternion.identity).transform.SetParent(roadBoard);
        }
    }
}
