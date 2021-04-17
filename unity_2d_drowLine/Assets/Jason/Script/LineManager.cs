using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LineManager : MonoBehaviour
{   
    [Header("lineRender預置物")]
    public GameObject linePrefeb;

    [Header("當前畫的lineRender")]
    [SerializeField]
    LineRenderer line_Render;
    [SerializeField]
    [Header("當下畫的lineRender中的EdgeCollider2D")]
    EdgeCollider2D edgCollider;
    [SerializeField]
    List<Vector2> fingerpointsPos;//點擊的位置
    public Color m_Color;
    //public Rigidbody2D playerRig;
    //public Toggle checkUsegravity;
    //public Toggle checkUsePhysicsMaterial;
    //public PhysicsMaterial2D m_PhysicsMaterial2D;
    //public Button ReturnScence;
    //[Range(0, 1.5f)]
    //public float PhysicsMaterialRange;
    //public Slider m_PhysicsMaterialRange;
    //public Text SendPhysicsMaterial_text;
   
    // Update is called once per frame
    void Update()
    {
        GameLoop();
    }
    void GameLoop()
    {
        LineRenderUpdate();
        
    }
    #region LinRenderDraw
    void CreateLIne()
    {

        GameObject _Line = Instantiate(linePrefeb, Vector3.zero, Quaternion.identity);
        line_Render = _Line.GetComponent<LineRenderer>();
        edgCollider = _Line.GetComponent<EdgeCollider2D>();
        line_Render.material.color = m_Color;        
        fingerpointsPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerpointsPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));//要兩次因為 line_Render.SetPosition 是兩個座標 所以先放入兩個相同的不然 下一行outofRange
        line_Render.SetPosition(0, fingerpointsPos[0]);
        line_Render.SetPosition(1, fingerpointsPos[1]);
        edgCollider.points = fingerpointsPos.ToArray();
    }
    void UpdateLine(Vector2 newFingerPos)
    {
        fingerpointsPos.Add(newFingerPos);
        line_Render.positionCount++;
        line_Render.SetPosition(line_Render.positionCount - 1, newFingerPos);
        edgCollider.points = fingerpointsPos.ToArray();

    }
    void LineRenderUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            fingerpointsPos.Clear();//假如List中有資料先清除
            CreateLIne();//先創第一點
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 nowfingerpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(nowfingerpoint, fingerpointsPos[fingerpointsPos.Count - 1]) > .1f) //距離上一點的距離
                UpdateLine(nowfingerpoint);
        }

    }
    #endregion



}
