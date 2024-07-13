using UnityEngine;

public class LaserCat : MonoBehaviour
{
    public GameObject HitEffect;
    public GameObject Laser; // 2초후 발사할 찐 레이져 

    LineRenderer lr;

    void Start ( )
    {
        lr = Laser.GetComponent<LineRenderer> ( );
        lr.enabled = false;
        HitEffect.SetActive ( false );
    }

    void Update ( )
    {
        if ( lr.enabled )
        {
            Physics.Raycast ( transform.position, transform.forward, out RaycastHit hit );

            if ( hit.transform.CompareTag ( "Player" ) || hit.transform.CompareTag ( "Wall" ) )
            {
                HitEffect.SetActive ( true );
                HitEffect.transform.position = hit.point;
                if ( hit.transform.CompareTag ( "Player" ) )
                {
                    PlayerHpBar.Single.curHp -= Time.deltaTime * 250f;// LaserDMG;
                }
            }
            else
            {
                HitEffect.SetActive ( false );
            }
        }
    }
}
