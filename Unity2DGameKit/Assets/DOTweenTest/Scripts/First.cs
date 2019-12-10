using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class First : MonoBehaviour
{
    public RectTransform playButton;
    public RectTransform settingsButton;
    public RectTransform exitButton;

    public Rigidbody2D obj;

    private void Start()
    {
        // 全局设置
        DOTween.defaultEaseType = Ease.Linear;

        playButton.DOAnchorPosX(0, 1);
        settingsButton.DOAnchorPosX(0, 1);
        exitButton.DOAnchorPosY(-200, 1);

        // 如果用From，物体原本在场景中的位置就是目标位置
        // 如果snapping为true，只取整数值
        obj.DOMove(new Vector2(0, 3), 5).From();
        //obj.DOJump(new Vector2(5, 0), 3, 3, 5).SetEase(Ease.Linear);
        obj.DORotate(360, 5);
        Invoke("Jump", 5);
    }

    private void Jump()
    {
        obj.DOJump(new Vector2(5, 0), 3, 3, 5);
    }
}
