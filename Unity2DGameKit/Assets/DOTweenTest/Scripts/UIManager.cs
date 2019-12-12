using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject loginPage;
    public GameObject firstPage;
    public RectTransform infomation;
    public RectTransform sleepMode;
    public Text sleepModeHint;
    public RectTransform collection;
    public RectTransform forum;

    private bool infomationIsOpened = false;
    private bool sleepModeIsOpened = false;
    private bool collectionIsOpened = false;
    private bool forumIsOpened = false;

    public void Login()
    {
        loginPage.SetActive(false);
        firstPage.SetActive(true);
    }

    public void Infomation()
    {
        infomationIsOpened = !infomationIsOpened;   // 开关
        if (infomationIsOpened)
            infomation.DOAnchorPosX(-160, 0.3f).SetEase(Ease.OutQuart);
        else
            infomation.DOAnchorPosX(-960, 0.3f).SetEase(Ease.OutQuart);
    }

    public void SleepMode()
    {
        sleepModeIsOpened = !sleepModeIsOpened;
        if (sleepModeIsOpened)
        {
            sleepMode.DOAnchorPosY(0, 0.3f).SetEase(Ease.OutQuart);
            sleepModeHint.DOColor(new Color(0.38f, 0.56f, 0.7f), 1.5f);
        }
    }

    public void Collection()
    {
        collectionIsOpened = !collectionIsOpened;
        if (collectionIsOpened)
        {
            forum.DOAnchorPosX(1200, 0.3f).SetEase(Ease.OutQuart);
            collection.DOAnchorPosX(0, 0.3f).SetEase(Ease.OutQuart);
        }
    }

    public void Forum()
    {
        forumIsOpened = !forumIsOpened;
        if (forumIsOpened)
        {
            collection.DOAnchorPosX(-1200, 0.3f).SetEase(Ease.OutQuart);
            forum.DOAnchorPosX(0, 0.3f).SetEase(Ease.OutQuart);
        }
    }

}
