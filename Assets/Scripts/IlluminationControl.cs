using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class IlluminationControl : MonoBehaviour
{
    public Material lightingMaterial, mainExtrusionShader;
    public Material pulseMat;
    public Material[] distanceOpaqueMat, glitchMaterials;
    public Material fullscreenMat, healthFullscreenEffect, deathFullscreenEffect;
    public int LUTIndex;
    public Texture2D[] LUTs;
    public Transform playerTf;


    [SerializeField] private AnimationCurve startupCurve;
    [SerializeField] private float startExtrusionFactorPrimary = -20;
    [SerializeField] private float startExtrusionFactorSecondary = 15;

    public static IlluminationControl instance { get; private set; }

    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(this); }
        
        #if UNITY_EDITOR
        // maybe ill put something here not sure
        #else
        StartCoroutine(StartSequenceCoroutine());
        #endif
        InitializeLighting();
    }

    void OnEnable()
    {   
        #if UNITY_EDITOR
        InitializeLighting();
        #endif
    }

    void InitializeLighting()
    {
        lightingMaterial.SetFloat("_boolAmbient",  1); 
        lightingMaterial.SetFloat("_boolDiffuse",  1);
        lightingMaterial.SetFloat("_boolSpecular",  1);
        lightingMaterial.SetFloat("_boolToon",  1);
    }

    void Update()
    {
        UpdateLighting();
        foreach (Material mat in distanceOpaqueMat)
        {
            mat.SetVector("_PlayerPosWS", playerTf.position);
        }
        // pulseMat.SetVector("_PlayerPosWS", Player.instance.transform.position);
    }


    void UpdateLighting()
    {
        if(Input.GetKeyDown(KeyCode.Keypad0)){lightingMaterial.SetFloat("_boolAmbient", lightingMaterial.GetFloat("_boolAmbient") == 0 ? 1 : 0);}
        if(Input.GetKeyDown(KeyCode.Keypad1)){lightingMaterial.SetFloat("_boolDiffuse", lightingMaterial.GetFloat("_boolDiffuse") == 0 ? 1 : 0);}
        if(Input.GetKeyDown(KeyCode.Keypad2)){lightingMaterial.SetFloat("_boolSpecular", lightingMaterial.GetFloat("_boolSpecular") == 0 ? 1 : 0);}
        if (Input.GetKeyDown(KeyCode.Keypad3)) { lightingMaterial.SetFloat("_boolToon", lightingMaterial.GetFloat("_boolToon") == 0 ? 1 : 0); }
        if (Input.GetKeyDown(KeyCode.O)) { GameManager.main.RestartGame(); }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            if (LUTIndex == 0) { fullscreenMat.SetFloat("_Contribution", 0); LUTIndex = -1; return; }
            fullscreenMat.SetTexture("_LUT", LUTs[0]);
            fullscreenMat.SetFloat("_Contribution", 1); LUTIndex = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            if (LUTIndex == 1) { fullscreenMat.SetFloat("_Contribution", 0); LUTIndex = -1; return; }
            fullscreenMat.SetTexture("_LUT", LUTs[1]);
            fullscreenMat.SetFloat("_Contribution", 1); LUTIndex = 1;
        }
        
        if(Input.GetKeyDown(KeyCode.Keypad6)){
            if(LUTIndex == 2){fullscreenMat.SetFloat("_Contribution", 0); LUTIndex = -1; return;}
            fullscreenMat.SetTexture("_LUT", LUTs[2]);
            fullscreenMat.SetFloat("_Contribution", 1); LUTIndex = 2;
        }
    }

    public IEnumerator StartSequenceCoroutine()
    {
        Time.timeScale = 0;
        float animTime = 2;
        float currentAnimTime = animTime;

        while (animTime > 0)
        {
            mainExtrusionShader.SetFloat("_ExtrusionFactor", startExtrusionFactorPrimary * startupCurve.Evaluate(1 - currentAnimTime / animTime));
            lightingMaterial.SetFloat("_ExtrusionFactor", startExtrusionFactorSecondary * startupCurve.Evaluate(1 - currentAnimTime / animTime));

            animTime -= Time.unscaledDeltaTime;
            yield return null;
        }

        mainExtrusionShader.SetFloat("_ExtrusionFactor", 0.1f);
        lightingMaterial.SetFloat("_ExtrusionFactor", 0);
        Time.timeScale = 1;
    }
}
