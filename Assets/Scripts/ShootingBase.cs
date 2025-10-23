using UnityEngine;

public class ShootingBase : MonoBehaviour
{
    public KeyCode shootKey = KeyCode.Mouse0;
    public Transform cameraTf;
    public bool safety;
    public bool canHold;
    public float timer;
    public GunInfo gun;
    
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
        if (canHold ? Input.GetKey(shootKey) : Input.GetKeyDown(shootKey))
        {
            if (!safety && timer <= 0) { Shoot(); }
        }

        if (timer > 0) { timer -= Time.deltaTime; }
    }

    void SetGun(GunInfo gunInfo){
        gun = gunInfo;
    }
    
    
    void Shoot()
    {
        if (Physics.Raycast(cameraTf.position, cameraTf.forward, out RaycastHit hit))
        {
            if (hit.collider.gameObject.GetComponent<Health>() != null)
            {
                hit.collider.gameObject.GetComponent<Health>().Damage(gun.damage);
            }
        }

        timer = gun.fireInterval;
    }
}
