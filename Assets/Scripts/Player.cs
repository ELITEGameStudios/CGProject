using UnityEngine;

public class Player : Health
{
    public static Player instance {get; private set;}
    public Health healthScript;
    public PlayerMovement moveScript;
    public ShootingBase gunScript;
    public Transform gunTargetTf;
    public Transform gunTf;
    public Material lightingMaterial;
    public Material fullscreenMat;
    public int LUTIndex;
    public Texture2D[] LUTs;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(this); }

        // if (Input.GetKeyDown(KeyCode.Keypad0)) { lightingMaterial.SetFloat("_boolAmbient",  1); }
        // if(Input.GetKeyDown(KeyCode.Keypad1)){lightingMaterial.SetFloat("_boolDiffuse",  1);}
        // if(Input.GetKeyDown(KeyCode.Keypad2)){lightingMaterial.SetFloat("_boolSpecular",  1);}
        // if(Input.GetKeyDown(KeyCode.Keypad3)){lightingMaterial.SetFloat("_boolToon",  1);}
    }

    void FixedUpdate()
    {
        gunTf.transform.position = Vector3.Lerp(gunTf.transform.position, gunTargetTf.transform.position, 0.3f);
        gunTf.transform.rotation = Quaternion.Lerp(gunTf.transform.rotation, gunTargetTf.transform.rotation, 0.3f);
    }
    
    void Update()
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

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Lava"){ Die(); }
    }

    public override void Die()
    {
        // Trigger death sequence
        GameManager.main.RestartGame();   
    }
}
