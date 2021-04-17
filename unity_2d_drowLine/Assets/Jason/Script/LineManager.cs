using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{   

    public GameObject linePrefeb;
    [SerializeField]
     GameObject currentLine;
    [SerializeField]
    LineRenderer line_Render;
    [SerializeField]
    EdgeCollider2D edgCollider;
    [SerializeField]
    List<Vector2> fingerpointsPos;

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateLIne();//先創第一點
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 nowfingerpoint= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(nowfingerpoint, fingerpointsPos[fingerpointsPos.Count-1])>.1f) //距離上一點的距離
                UpdateLine(nowfingerpoint);
        }
    }
    void CreateLIne() {

        currentLine = Instantiate(linePrefeb, Vector3.zero, Quaternion.identity);
        line_Render = currentLine.GetComponent<LineRenderer>();
        edgCollider = currentLine.GetComponent<EdgeCollider2D>();
         
        fingerpointsPos.Clear();
        fingerpointsPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerpointsPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        line_Render.SetPosition(0, fingerpointsPos[0]);
        line_Render.SetPosition(1, fingerpointsPos[1]);
        edgCollider.points= fingerpointsPos.ToArray();
    }
    void UpdateLine(Vector2 newFingerPos) {
        fingerpointsPos.Add(newFingerPos);
        line_Render.positionCount++;
        line_Render.SetPosition(line_Render.positionCount - 1, newFingerPos);
        edgCollider.points = fingerpointsPos.ToArray();

    }
}
