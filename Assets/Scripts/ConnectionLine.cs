using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectionLine : Singleton<ConnectionLine> {

    public float coef = 1;

    public Color[] colors;

    public Person startPerson;

    private Material material;
    private List<Vector3> points = new List<Vector3>();

	public Wire Drop(RectTransform endTransform, Person endPerson)
    {
        GameObject newWire = new GameObject();

        newWire.AddComponent<Wire>().Init(startPerson, endPerson);

        newWire.transform.SetParent(transform);
        LineRenderer lr = newWire.AddComponent<LineRenderer>();
        lr.material = new Material(material);
        lr.useWorldSpace = true;
        lr.SetWidth(GetComponent<LineRenderer>().startWidth, GetComponent<LineRenderer>().endWidth);

        Vector3 start = new Vector3(startTransform.position.x, startTransform.position.y, -5);
        Vector3 end = new Vector3(endTransform.position.x, endTransform.position.y, -5);
        points.Clear();
        for (int i = 0; i < 10; i++)
        {
            points.Add(new Vector3(Mathf.Lerp(start.x, end.x, (i + .0f) / 10), Mathf.Lerp(start.y, end.y, (float)Math.Pow((double)((i + .0f) / 10), (double)2f)), start.z));
        }
        points.Add(end);
        points = LineSmoother.SmoothLine(points.ToArray(), 0.3f).ToList();

        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
		return newWire.GetComponent<Wire> ();
    }

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


    #region Lifecycle
    private void Update()
    {

        if (Line.enabled)
        {
            Vector3 start = new Vector3(startTransform.position.x, startTransform.position.y, -5);
            Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

            points.Clear();

            for (int i = 0; i<10;i++)
            {
                points.Add(new Vector3(Mathf.Lerp(start.x, end.x, (i+.0f)/10), Mathf.Lerp(start.y, end.y, (float)Math.Pow((double)((i + .0f) / 10),(double)2f)), start.z));
            }

            points.Add(end);

            points = LineSmoother.SmoothLine(points.ToArray(), 0.3f).ToList();
            Line.positionCount = points.Count;

            Line.SetPositions(points.ToArray());
        }
        if (Input.GetMouseButtonUp(0))
        {
			Invoke("Hide", 0.1f);
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
    public void SetStart(RectTransform start, Person person)
    {
        if(start == startTransform)
        {
            return;
        }
			
        startPerson = person;

        Color c = colors[UnityEngine.Random.Range(0, colors.Count() - 1)];
        material.color = c;

        if (startTransform!=start)
        {
            startTransform = start;
            Line.enabled = true;
        }
        
    }

	

   
}
