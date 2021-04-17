using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    

    public Color m_Color;
    public Rigidbody2D playerRig;
    public Toggle checkUsegravity;
    public Toggle checkUsePhysicsMaterial;
    public PhysicsMaterial2D m_PhysicsMaterial2D;
    public Button ReturnScence;
    [Range(0, 1.5f)]
    public float PhysicsMaterialRange;
    public Slider m_PhysicsMaterialRange;
    public Text SendPhysicsMaterial_text;
    private void Start()
    {
        ReturnScence.onClick.AddListener(()=>ReturnOnclick());
    }
    // Update is called once per frame
    void Update()
    {
        GameLoop();
    }
    void GameLoop()
    {
        LineRenderUpdate();
        PhysicUpdate();
    }
    #region LinRenderDraw
    void CreateLIne()
    {

        currentLine = Instantiate(linePrefeb, Vector3.zero, Quaternion.identity);
        line_Render = currentLine.GetComponent<LineRenderer>();
        edgCollider = currentLine.GetComponent<EdgeCollider2D>();
        line_Render.material.color = m_Color;

        fingerpointsPos.Clear();
        fingerpointsPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerpointsPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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


    #region PhysicsMaterial
    void ReturnOnclick()
    {
        SceneManager.LoadScene("LineDraw");
    }
    void PhysicUpdate()
    {
        if (checkUsegravity.isOn)
            playerRig.simulated = true;
        else
            playerRig.simulated = false;
        if (checkUsePhysicsMaterial.isOn)
            playerRig.sharedMaterial = m_PhysicsMaterial2D;
        else
            playerRig.sharedMaterial = null;
        PhysicsMaterialRange = m_PhysicsMaterialRange.value * 1.5f;
        SendPhysicsMaterial_text.text = string.Format("彈力指數: {0}", PhysicsMaterialRange.ToString("0.0"));
        m_PhysicsMaterial2D.bounciness = PhysicsMaterialRange;


    }
    #endregion


}
