using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField][Min(0)] private float nextShotTime;
    private bool _canShoot = true;

    public override void TryShoot()
    {
        if (_canShoot)
        {
            base.TryShoot();
            StartCoroutine(WaitForNextShot());
        }
    }

    private IEnumerator WaitForNextShot()
    {
        _canShoot = false;
        yield return new WaitForSeconds(nextShotTime);  
        _canShoot = true;
    }
}

