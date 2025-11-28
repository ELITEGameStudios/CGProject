using UnityEngine;

[ExecuteAlways]
public class IlluminationControl : MonoBehaviour
{
    public Material lightingMaterial;
    public Material pulseMat;
    public Material fullscreenMat;
    public int LUTIndex;
    public Texture2D[] LUTs;

    void Awake()
    {
        InitializeLighting();
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
        pulseMat.SetVector("_PlayerPosWS", Player.instance.transform.position);
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
}
