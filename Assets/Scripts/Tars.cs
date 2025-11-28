using UnityEngine;

public class Tars : MonoBehaviour
{
    float distance => (transform.position - Player.instance.transform.position).magnitude;
    public float targetDistance = 10;
    public float moveSpeed = 3;
    public ParticleSystem explosionParticles;
    public Material tarsMat;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Kill()
    {
        explosionParticles.transform.SetParent(null);
        explosionParticles.transform.localScale = Vector3.one;
        explosionParticles.Play();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(distance <= targetDistance){
            tarsMat.SetInteger("_Active", 1);
            transform.rotation = Quaternion.LookRotation(Vector3.up, (transform.position - Player.instance.transform.position).normalized);
            transform.localEulerAngles = new Vector3(-90, transform.localEulerAngles.y, transform.localEulerAngles.z);
            transform.position -= transform.up * moveSpeed * Time.fixedDeltaTime;
        }   
        tarsMat.SetInteger("_Active", 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player.instance.Damage(2);
        }
    }
}
