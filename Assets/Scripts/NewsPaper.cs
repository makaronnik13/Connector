using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsPaper : Singleton<NewsPaper> {

    private int currentId = 0;
    public TextMeshProUGUI NewsText;
    public TextMeshProUGUI NewsTitle;
    public Image NewsImage;
    public Animator NewNewsAnimator;
    public GameObject nextButton;
    public GameObject previousButton;
    public Animator Ping;

    private List<NewsVariant> news = new List<NewsVariant>();

	public void SetNews(NewsVariant variant)
    {
        news.Add(variant);
        currentId = news.Count - 1;
        Debug.Log(variant.text);
        GetComponent<PolygonCollider2D>().enabled = true;
        SoundController.Instance.PlaySound(7);
        NewNewsAnimator.SetTrigger("Show");
        Ping.SetBool("Active", true);
    }

    public void ShowNewsPaper()
    {
        Phone.Instance.TalkingPhone = true;
        UpdateNewsCanvas();
        GetComponent<Animator>().SetTrigger("Open");
        CameraController.Instance.SetCameraView(5);
        gameObject.layer = 2;
        Ping.SetBool("Active", false);
    }

    private void UpdateNewsCanvas()
    {
        Debug.Log("show");
        NewsText.text = news[currentId].text;
        NewsTitle.text = news[currentId].title;
        NewsImage.sprite = news[currentId].img;
        NewsImage.gameObject.SetActive(news[currentId].img != null);
        SoundController.Instance.PlaySound(8);
        nextButton.SetActive(currentId!=news.Count-1);
        previousButton.SetActive(currentId != 0);
    }

    public void HideNewspaper()
    {
        Phone.Instance.TalkingPhone = false;
        SoundController.Instance.PlaySound(5);
        GetComponent<Animator>().SetTrigger("Close");
        CameraController.Instance.SetCameraView(0);
        gameObject.layer = 5;
    }

    public void Previous()
    {
        currentId--;
        UpdateNewsCanvas();
    }

    public void Next()
    {
        currentId++;
        UpdateNewsCanvas();
    }
}
