using System.Collections;
using UnityEngine;

public class IlluminationControl : MonoBehaviour
{
    public Material lightingMaterial, mainExtrusionShader;
    public Material[] distanceOpaqueMat, glitchMaterials;
    public Material[] allMaterials;
    public Material fullscreenMat, healthFullscreenEffect, deathFullscreenEffect, tarsMat;
    public int LUTIndex;
    public Texture2D[] LUTs;
    public Transform playerTf;
    public bool textures = true;
    public Texture[] allTextures;
    public Texture tarsTex;


    [SerializeField] private AnimationCurve startupCurve;
    [SerializeField] private float startExtrusionFactorPrimary = -20;
    [SerializeField] private float startExtrusionFactorSecondary = 15;

    public static IlluminationControl instance { get; private set; }

    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(this); }
        textures = true;
    
        StartCoroutine(StartSequenceCoroutine());
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
        
        // #if !UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T))
        {
            UpdateTextures();
        }
        // #endif

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

    void UpdateTextures()
    {
        if (textures)
        {
            allTextures = new Texture[allMaterials.Length];
            for (int i = 0; i < allMaterials.Length; i++)
            {
                Material mat = allMaterials[i];
                try
                {
                    allTextures[i] = mat.GetTexture("_MainTex");
                    mat.SetTexture("_MainTex", null);
                }
                catch{continue;}
            }

            tarsTex = tarsMat.GetTexture("_EmissionTex");
            tarsMat.SetTexture("_EmissionTexture", null);
            textures = false;
        }
        else {
            // allTextures = new Texture2D[allMaterials.Length];
            for (int i = 0; i < allMaterials.Length; i++)
            {
                Material mat = allMaterials[i];
                try
                {
                    mat.SetTexture("_MainTex", allTextures[i]);
                    // allTextures[i] = (Texture2D)mat.GetTexture("_MainTex");
                }
                catch{continue;}
            }

            tarsMat.SetTexture("_EmissionTexture", tarsTex);
            // tarsTex = (Texture2D)tarsMat.GetTexture("_EmissionTex");
            textures = true;
            
        }
    }

    public IEnumerator StartSequenceCoroutine()
    {
        // Time.timeScale = 1;
        float animTime = 2;
        float currentAnimTime = animTime;
        while (currentAnimTime > 0)
        {
            mainExtrusionShader.SetFloat("_ExtrusionFactor", startExtrusionFactorSecondary * startupCurve.Evaluate(1 - currentAnimTime / animTime));
            lightingMaterial.SetFloat("_ExtrusionFactor", startExtrusionFactorPrimary * startupCurve.Evaluate(1 - currentAnimTime / animTime));

            Debug.Log("Tick");
            currentAnimTime -= Time.deltaTime;
            yield return null;
        }

        mainExtrusionShader.SetFloat("_ExtrusionFactor", 0.1f);
        lightingMaterial.SetFloat("_ExtrusionFactor", 0);
        Time.timeScale = 1;
    }
}
