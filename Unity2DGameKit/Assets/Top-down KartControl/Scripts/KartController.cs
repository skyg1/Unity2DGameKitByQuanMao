using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartController : MonoBehaviour
{
    public float mass = 5;                  // 赛车的质量
    public float bodyLength = 1;            // 车身长度
    public float normalPower = 2000;           // 起步功率
    public float upPower = 2500;               // 加速功率
    public float downPower = 1500;          // 减速功率
    public float startVeloctiy = 100;        // 初速度
    public float velocityRejust = 6;        // 速度微调

    public GameObject physicsManager;       // 物理管理器
    //public GameObject roadBoard;            // 赛道
    public Text score;

    private float driveForce;               // 牵引力
    private float friction;                 // 摩擦力
    private float velocity;                 // 速度
    private float acceleration;             // 加速度
    private Vector2 direction = new Vector2(0, 1);  // 行车方向

    private float gravity;                  // 重力系数
    private float frictionFactor;           // 赛道的摩擦系数

    private Rigidbody2D rgb;

    public bool straight = true;
    public bool left = false;
    public bool right = false;

    //private Bounds roadBounds;
    private GameObject[] roadBoard;
    private bool insideRoad = false;

    private float scorePoint = 0;

    private void Start()
    {
        gravity = physicsManager.GetComponent<PhysicsManager>().gravity;
        frictionFactor = physicsManager.GetComponent<PhysicsManager>().frictionFactor;
        velocity = 0;
        friction = frictionFactor * mass * gravity; // 计算阻力
        driveForce = normalPower / startVeloctiy;        // 计算起步牵引力

        rgb = GetComponent<Rigidbody2D>();

        //roadBounds = roadBoard.GetComponent<Collider2D>().bounds;

        Debug.Log("最大速度为 " + normalPower / -friction);
        Debug.Log("起步牵引力为 " + driveForce);
        Debug.Log("阻力为 " + friction);
    }

    private void Update()
    {
        //if (!roadBounds.Contains(transform.position))   // 离开赛道GameOver
        //{// 无法形成多边形区域，因此即使离开赛道也会判定为没有离开
        //    Debug.Log("Game Over!");
        //    GetComponent<KartController>().enabled = false;
        //}
        // 每次更新判断一次赛车是否在赛道内
        insideRoad = false;
        roadBoard = GameObject.FindGameObjectsWithTag("Road");
        foreach(GameObject road in roadBoard)
        {
            Bounds roadBounds = road.GetComponent<Collider2D>().bounds;
            if (roadBounds.Contains(transform.position))
                insideRoad = true;
        }
        if (!insideRoad)
        {
            Debug.Log("离开赛道！");
            GetComponent<KartController>().enabled = false;
            Time.timeScale = 0;
        }

        if (insideRoad)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (left || right)
                {
                    straight = true;
                    left = false;
                    right = false;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (straight)
                {
                    left = true;
                    straight = false;
                    right = false;
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (straight)
                {
                    right = true;
                    straight = false;
                    left = false;
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                }
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {// 直行
                if (Input.GetKey(KeyCode.LeftShift))
                {// 加速
                    AccelUp();
                }
                else if (Input.GetKey(KeyCode.LeftControl))
                {// 减速
                    SlowUp();
                    Debug.Log("Slow");
                }
                else
                {// 起步
                    StartUp();
                }
            }
            else
            {// 停车
                EndUp();
            }

            if (straight)
                rgb.velocity = new Vector2(0, velocity / velocityRejust);
            if (left)
                rgb.velocity = new Vector2(-velocity / velocityRejust, 0);
            if (right)
                rgb.velocity = new Vector2(velocity / velocityRejust, 0);

            Debug.Log("当前速度为 " + velocity);
            Debug.Log("当前加速度为 " + acceleration);

            if (velocity > 0)
            {
                scorePoint++;
                score.text = "Score: " + scorePoint.ToString();
            }
        }
    }

    private void StartUp()
    {// 赛车起步（恒定功率P）
        if (driveForce > -friction || velocity < normalPower / -friction)
        {
            acceleration = (driveForce + friction) / mass;  // 计算加速度
            velocity = velocity + acceleration * Time.deltaTime * 2;       // 计算速度
            driveForce = normalPower / velocity;            // 重新计算牵引力
        }
    }

    private void AccelUp()
    {// 赛车加速（恒定功率P+）
        driveForce = upPower / velocity;
        if (driveForce > -friction || velocity < upPower / -friction)
        {
            acceleration = (driveForce + friction) / mass;  // 计算加速度
            velocity = velocity + acceleration * Time.deltaTime * 2;       // 计算速度
        }
    }

    private void SlowUp()
    {// 赛车减速（恒定功率P-）
        driveForce = downPower / velocity;
        if (driveForce < -friction || velocity > downPower / -friction)
        {
            acceleration = (driveForce + friction) / mass;  // 计算加速度
            velocity = velocity + acceleration * Time.deltaTime * 20;       // 计算速度
        }
    }

    private void EndUp()
    {// 停车
        if (velocity > 0.01f)
        {
            acceleration = frictionFactor * gravity;
            velocity = velocity + acceleration * Time.deltaTime * 10;       // 计算速度
        }
        if (velocity <= 0.01f)
        {
            velocity = 0;
        }
    }

    private void LeftTurn()
    {// 左转

    }

    private void RightTurn()
    {// 右转

    }

    // 用碰撞事件不能解决赛道衔接的问题
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Road")
    //    {
    //        GetComponent<KartController>().enabled = true;
    //        Time.timeScale = 1;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Road")
    //    {
    //        Debug.Log("离开赛道！");
    //        GetComponent<KartController>().enabled = false;
    //        Time.timeScale = 0;
    //    }
    //}
}
