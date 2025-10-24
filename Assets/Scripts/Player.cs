using UnityEngine;

public class Player : Health
{
    public static Player instance {get; private set;}
    public Health healthScript;
    public ShootingBase gunScript;
    public Transform gunTargetTf;
    public Transform gunTf;

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
