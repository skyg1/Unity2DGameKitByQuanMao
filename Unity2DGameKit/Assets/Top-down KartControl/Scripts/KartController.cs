using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
    public float mass = 5;                  // 赛车的质量
    public float bodyLength = 1;            // 车身长度
    public float normalPower = 2000;           // 起步功率
    public float upPower = 2500;               // 加速功率
    public float downPower = 1500f;          // 减速功率
    public float startVeloctiy = 100f;        // 初速度

    public GameObject physicsManager;       // 物理管理器

    private float driveForce;               // 牵引力
    private float friction;                 // 摩擦力
    private float velocity;                 // 速度
    private float acceleration;             // 加速度
    private Vector2 direction = new Vector2(0, 1);  // 行车方向

    private float gravity;                  // 重力系数
    private float frictionFactor;           // 赛道的摩擦系数

    private void Start()
    {
        gravity = physicsManager.GetComponent<PhysicsManager>().gravity;
        frictionFactor = physicsManager.GetComponent<PhysicsManager>().frictionFactor;
        velocity = startVeloctiy;
        friction = frictionFactor * mass * gravity; // 计算阻力
        driveForce = normalPower / velocity;        // 计算起步牵引力
        Debug.Log("最大速度为 " + normalPower / -friction);
        Debug.Log("起步牵引力为 " + driveForce);
        Debug.Log("阻力为 " + friction);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
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

        Debug.Log("当前速度为 " + velocity);
        Debug.Log("当前加速度为 " + acceleration);

    }

    private void StartUp()
    {// 赛车起步（恒定功率P）
        driveForce = normalPower / velocity;            // 重新计算牵引力
        if (driveForce > -friction || velocity < normalPower / -friction)
        {
            acceleration = (driveForce + friction) / mass;  // 计算加速度
            velocity = velocity + acceleration * Time.deltaTime;       // 计算速度
        }
    }

    private void AccelUp()
    {// 赛车加速（恒定功率P+）
        driveForce = upPower / velocity;
        if (driveForce > -friction || velocity < upPower / -friction)
        {
            acceleration = (driveForce + friction) / mass;  // 计算加速度
            velocity = velocity + acceleration * Time.deltaTime;       // 计算速度
        }
    }

    private void SlowUp()
    {// 赛车减速（恒定功率P-）
        driveForce = downPower / velocity;
        if (driveForce < -friction || velocity > downPower / -friction)
        {
            acceleration = (driveForce + friction) / mass;  // 计算加速度
            velocity = velocity + acceleration * Time.deltaTime;       // 计算速度
        }
    }

    private void EndUp()
    {// 停车
        if (velocity > 0)
        {
            acceleration = frictionFactor * gravity;
            velocity = velocity + acceleration * Time.deltaTime;       // 计算速度
        }
    }
}
