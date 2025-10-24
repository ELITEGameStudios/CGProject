using UnityEngine;

public class ShootingBase : MonoBehaviour
{
    public KeyCode shootKey = KeyCode.Mouse0;
    public Transform cameraTf;
    public AnimationCurve curve;
    public bool safety;
    public bool canHold;
    public bool needsPlayerInput;
    public float timer;
    public GunInfo gun;
    public ParticleSystem particleEffect;
    public Material[] gunModelMat;
    
    [System.Serializable]
    public class GunInfo
    {
        public int damage;
        public float fireRate;
        public float fireInterval { get { return 1 / fireRate; } }
        public bool raycasted = true;
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
        if (Physics.Raycast(cameraTf.position, cameraTf.forward, out RaycastHit hit))
        {
            if (needsPlayerInput ? hit.collider.gameObject.GetComponent<Health>() != null : hit.collider.gameObject.GetComponent<Player>() != null)
            {
                if (needsPlayerInput){
                    hit.collider.gameObject.GetComponent<Health>().Damage(gun.damage);
                    return;
                }
                hit.collider.gameObject.GetComponent<Player>().Damage(gun.damage);
            }
        }
        if(particleEffect != null) { particleEffect.Play(); }
        timer = gun.fireInterval;
    }
    
    void OnDisable(){
        timer = 0;
    }
}
