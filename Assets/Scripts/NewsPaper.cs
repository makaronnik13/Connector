using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsPaper : Singleton<NewsPaper> {

    private NewsVariant lastNews;
    public TextMeshProUGUI NewsText;
    public Image NewsImage;
    public Animator NewNewsAnimator;

	public void SetNews(NewsVariant variant)
    {
        lastNews = variant;
        Debug.Log(variant.text);
        GetComponent<PolygonCollider2D>().enabled = true;
        SoundController.Instance.PlaySound(7);
        NewNewsAnimator.SetTrigger("Show");
    }

    public void ShowNewsPaper()
    {
        Debug.Log("show");
        NewsText.text = lastNews.text;
        NewsImage.sprite = lastNews.img;
        SoundController.Instance.PlaySound(8);
        GetComponent<Animator>().SetTrigger("Open");
        CameraController.Instance.SetCameraView(5);
        gameObject.layer = 2;
        
    }

    public void HideNewspaper()
    {
        SoundController.Instance.PlaySound(5);
        GetComponent<Animator>().SetTrigger("Close");
        CameraController.Instance.SetCameraView(0);
        gameObject.layer = 5;
    }
}
