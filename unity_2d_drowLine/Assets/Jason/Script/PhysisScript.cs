using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PhysisScript : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        ReturnScence.onClick.AddListener(() => ReturnOnclick());
    }

    // Update is called once per frame
    void Update()
    {
        PhysicUpdate();
    }
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
