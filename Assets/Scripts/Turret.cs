using UnityEngine;

public class Turret : Health
{

    public Transform turretBarrel, turretRotator;
    public float range;
    public ShootingBase shootingBase;
    public float timer, gain = 0.1f;
    public float warningTime = 2;
    public TurretState state;

    public enum TurretState
    {
        IDLE,
        LOCKED,
        SHOOTING
    }

    void Update()
    {

        switch (state)
        {
            case TurretState.IDLE:
                UpdateIdlePosition();
                break;

            case TurretState.LOCKED:
                UpdateLockedPosition();
                timer -= Time.deltaTime;
                if (timer <= 0) { SetToShooting(); }
                break;

            case TurretState.SHOOTING:
                UpdateLockedPosition();
                break;
        }

        Debug.DrawRay(turretBarrel.position, Player.instance.transform.position - turretBarrel.position);

        if (Physics.Raycast(turretBarrel.position, Player.instance.transform.position - turretBarrel.position, out RaycastHit hit, range))
        {
            if (hit.collider.gameObject == Player.instance.gameObject)
            {
                state = TurretState.LOCKED;
                return;
            }
        }

        if (state == TurretState.SHOOTING || state == TurretState.LOCKED)
        {
            SetToIdle();
        }
    }

    public void SetToShooting()
    {
        shootingBase.safety = false;
        state = TurretState.SHOOTING;
    }

    public void SetToIdle()
    {
        shootingBase.safety = true;
        state = TurretState.IDLE;
        timer = warningTime;
    }
    
    public void UpdateIdlePosition()
    {

        // turretBarrel.localEulerAngles = -Vector3.right * 90;
            //     gain
            // );
    }
    
    public void UpdateLockedPosition()
    {
        Vector3 PlayerPos = Player.instance.transform.position;
        Vector3 RotatorTarget = new Vector3(
            PlayerPos.x - transform.position.x,
            0,
            PlayerPos.z - transform.position.z
        ).normalized;

        Vector3 barrelTargetVector = PlayerPos - turretBarrel.position;
        float targetBarrelAngle = Mathf.Asin(barrelTargetVector.y / barrelTargetVector.magnitude) * Mathf.Rad2Deg;

        turretRotator.rotation = Quaternion.Lerp(turretRotator.rotation, Quaternion.LookRotation(Vector3.up, -RotatorTarget), gain);

        turretBarrel.localEulerAngles = new Vector3(
            -targetBarrelAngle, 0, 0
        );
    }

    public override void Die()
    {
        
    }
}
