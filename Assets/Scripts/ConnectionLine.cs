using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectionLine : Singleton<ConnectionLine> {

    public float coef = 1;

    private Material material;
    private RectTransform startTransform;
    private LineRenderer line;
    private LineRenderer Line
    {
        get
        {
            if (!line)
            {
                line = GetComponent<LineRenderer>();
            }
            return line;
        }
    }

    public State From
    {
        get
        {
            if (startTransform)
            {
                if (startTransform.GetComponent<Task>())
                {
                    return startTransform.GetComponent<Task>().state;
                }

                if (startTransform.GetComponent<ConnectionPoint>())
                {
                    return startTransform.GetComponent<ConnectionPoint>().state;
                }
            }
            return null;
        }
    }

    #region Lifecycle
    private void Update()
    {

        if (Line.enabled)
        {
            Vector3 start = new Vector3(startTransform.position.x, startTransform.position.y, -5);
            Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

            List<Vector3> points = new List<Vector3>();

            for (int i = 0; i<10;i++)
            {
                points.Add(new Vector3(Mathf.Lerp(start.x, end.x, (i+.0f)/10), Mathf.Lerp(start.y, end.y, (float)Math.Pow((double)((i + .0f) / 10),(double)2f)), start.z));
            }

            points.Add(end);

            points = LineSmoother.SmoothLine(points.ToArray(), 0.3f).ToList();
            Line.positionCount = points.Count;

            Line.SetPositions(points.ToArray());
            material.SetFloat("_frequency", coef * Vector3.Distance(start, end));
        }
        if (Input.GetMouseButtonUp(0))
        {
            Hide();
        }
    }
    private void Start()
    {
        material = GetComponent<LineRenderer>().material;
        Hide();
    }
    #endregion

    public void Hide()
    {
        startTransform = null;
        Line.enabled = false;
    }
    public void SetStart(RectTransform start)
    {

        if(startTransform!=start)
        {
            startTransform = start;
            Line.enabled = true;
        }
        
    }

	

   
}
