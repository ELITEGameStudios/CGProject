using UnityEngine;

public class Player : Health
{
    public static Player instance {get; private set;}
    public Health healthScript;
    public ShootingBase gunScript;
    public Transform gunTargetTf;
    public Transform gunTf;
    public Material lightingMaterial;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(this); }
    }

    void FixedUpdate()
    {
        gunTf.transform.position = Vector3.Lerp(gunTf.transform.position, gunTargetTf.transform.position, 0.3f);
        gunTf.transform.rotation = Quaternion.Lerp(gunTf.transform.rotation, gunTargetTf.transform.rotation, 0.3f);
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad0)){lightingMaterial.SetInteger("_BoolAmbient", lightingMaterial.GetInteger("_BoolAmbient") == 0 ? 1 : 0);}
        if(Input.GetKeyDown(KeyCode.Keypad1)){lightingMaterial.SetInteger("_BoolDiffuse", lightingMaterial.GetInteger("_BoolDiffuse") == 0 ? 1 : 0);}
        if(Input.GetKeyDown(KeyCode.Keypad2)){lightingMaterial.SetInteger("_BoolSpecular", lightingMaterial.GetInteger("_BoolSpecular") == 0 ? 1 : 0);}
        if(Input.GetKeyDown(KeyCode.Keypad3)){lightingMaterial.SetInteger("_BoolToon", lightingMaterial.GetInteger("_BoolToon") == 0 ? 1 : 0);}
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
