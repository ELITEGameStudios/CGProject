using System.Collections;
using UnityEngine;

public class ShootingBase : MonoBehaviour
{
    public KeyCode shootKey = KeyCode.Mouse0;
    public Transform cameraTf;
    public AnimationCurve curve;
    public bool safety;
    public bool canHold, setRenderer = true;
    public bool needsPlayerInput, reloading;
    public float timer, reloadTime;
    public GunInfo gun;
    public ParticleSystem particleEffect, hitSurfaceParticleEffect;
    public Material[] gunModelMat, formerMaterials;
    public Renderer matRenderer;
    public Animator anim;
    
    [System.Serializable]
    public class GunInfo
    {
        public int damage;
        public int maxBullets = 5, bullets;
        public float fireRate;
        public float fireInterval { get { return 1 / fireRate; } }
        public bool raycasted = true;
    }

    void Awake()
    {
        if(setRenderer) matRenderer = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if (!safety && timer <= 0) {
            if (needsPlayerInput)
            {
                if (canHold ? Input.GetKey(shootKey) : Input.GetKeyDown(shootKey))
                {
                    Shoot();
                }

            }
            else{
                Shoot(); 
            }
        
        }

        if (timer > 0) { timer -= Time.deltaTime; }

        foreach (Material material in gunModelMat)
        {
            material.SetFloat("_Power", Mathf.Lerp(20, 1, curve.Evaluate(timer / gun.fireInterval)));
        }
    }

    void SetGun(GunInfo gunInfo){
        gun = gunInfo;
    }


    void Shoot()
    {
        if(gun.bullets > 0 || gun.maxBullets == 0)
        {
            if (Physics.Raycast(cameraTf.position, cameraTf.forward, out RaycastHit hit))
            {
                if (needsPlayerInput ? hit.collider.gameObject.GetComponent<Health>() != null : hit.collider.gameObject.GetComponent<Player>() != null)
                {
                    if (needsPlayerInput){
                        hit.collider.gameObject.GetComponent<Health>().Damage(gun.damage);
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<Player>().Damage(gun.damage);
                    }

                }

                if(hit.collider.gameObject.tag == "Tars"){
                    hit.collider.gameObject.GetComponent<Tars>().Kill();
                }

                
                hitSurfaceParticleEffect.transform.localScale = Vector3.one;
                hitSurfaceParticleEffect.transform.SetParent(null);
                hitSurfaceParticleEffect.transform.position = hit.point;
                hitSurfaceParticleEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
                hitSurfaceParticleEffect.Play();


            }
            if(particleEffect != null) { particleEffect.Play(); }
            timer = gun.fireInterval;
            if(anim != null){anim.SetTrigger("GunShoot");}

            OnConsumeBullet();
        }
        else
        {
            OnReloadingShootAttempt();
        }
    }

    void OnConsumeBullet()
    {
        if(gun.maxBullets > 0)
        {
            gun.bullets--;
            if(gun.bullets == 0)
            {
                StartCoroutine(RechargeCoroutine());
            }
        }
        else
        {
            //Nothing really
        }
    }

    void OnReloadingShootAttempt()
    {
        
    }
    
    void OnDisable(){
        timer = 0;
    }

    public IEnumerator RechargeCoroutine()
    {
        reloading = true;
        formerMaterials = matRenderer.materials;
        matRenderer.materials = IlluminationControl.instance.glitchMaterials;
        yield return new WaitForSeconds(reloadTime);
        gun.bullets = gun.maxBullets;
        matRenderer.materials = formerMaterials;
        reloading = false;
    }
}
