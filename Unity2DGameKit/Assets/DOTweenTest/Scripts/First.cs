using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class First : MonoBehaviour
{
    public RectTransform playButton;
    public RectTransform settingsButton;
    public RectTransform exitButton;

    private void Start()
    {
        playButton.DOAnchorPosX(0, 1);
        settingsButton.DOAnchorPosX(0, 1);
        exitButton.DOAnchorPosY(-200, 1);
    }
}
