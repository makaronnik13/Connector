using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController> {

    private Animator animator;

    public enum CameraView
    {
        Default,
        Map,
        Manual,
        Panel,
        Window,
        News
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetCameraView(int view)
    {
        SetCameraView((CameraView)view);
    }

    private void SetCameraView(CameraView view)
    {
        animator.SetBool("Manual", false);
        animator.SetBool("Map", false);
        animator.SetBool("Panel", false);
        animator.SetBool("Window",false);
        animator.SetBool("News", false);

        switch (view)
        {
            case CameraView.Default:
                break;
            case CameraView.Manual:
                animator.SetBool("Manual", true);
                break;
            case CameraView.Map:
                animator.SetBool("Map", true);
                break;
            case CameraView.Panel:
                animator.SetBool("Panel", true);
                break;
            case CameraView.Window:
                animator.SetBool("Window", true);
                break;
            case CameraView.News:
                animator.SetBool("News", true);
                break;
        }
    }
}
