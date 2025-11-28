using UnityEngine;

public class Player : Health
{
    public static Player instance {get; private set;}
    public Health healthScript;
    public PlayerMovement moveScript;
    public ShootingBase gunScript;
    public Transform gunTargetTf;
    public Transform gunTf;

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
