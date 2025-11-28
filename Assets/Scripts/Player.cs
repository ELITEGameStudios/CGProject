using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Health
{
    public static Player instance {get; private set;}
    public PlayerMovement moveScript;
    public ShootingBase gunScript;
    public Transform gunTargetTf;
    public Transform gunTf;
    public float healthHitstop, deathDuration;

    public Slider healthTracker;
    public GameObject deadText;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(this); }

        healthTracker.maxValue = health;
        healthTracker.value = health;
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
    
    // void Update()
    // {
    // }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Lava"){ Die(); }
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        StartCoroutine(OnDamageEffect());

    }

    public override void Die()
    {
        // Trigger death sequence
        StartCoroutine(OnDeathEffect());
    }

    public IEnumerator OnDamageEffect()
    {
        Time.timeScale = 0;
        healthTracker.value = health;
        IlluminationControl.instance.deathFullscreenEffect.SetInteger("_Active", 1);
        yield return new WaitForSecondsRealtime(healthHitstop);
        IlluminationControl.instance.deathFullscreenEffect.SetInteger("_Active", 0);
        Time.timeScale = 1;
    }

    // Extrememly rough code but works for its purpose
    public IEnumerator OnDeathEffect()
    {
        Time.timeScale = 0;
        IlluminationControl.instance.deathFullscreenEffect.SetInteger("_Active", 1);
        yield return new WaitForSecondsRealtime(deathDuration/4);
        deadText.SetActive(true);
        yield return new WaitForSecondsRealtime(deathDuration/8);
        deadText.SetActive(false);
        yield return new WaitForSecondsRealtime(deathDuration/8);
        deadText.SetActive(true);
        yield return new WaitForSecondsRealtime(deathDuration/8);
        deadText.SetActive(false);
        yield return new WaitForSecondsRealtime(deathDuration/8);
        deadText.SetActive(true);
        yield return new WaitForSecondsRealtime(deathDuration/8);
        deadText.SetActive(false);
        yield return new WaitForSecondsRealtime(deathDuration/8);

        IlluminationControl.instance.deathFullscreenEffect.SetInteger("_Active", 0);
        Time.timeScale = 1;
        GameManager.main.RestartGame();   
    }
}
