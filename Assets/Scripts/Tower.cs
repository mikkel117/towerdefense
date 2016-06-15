using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{

    public float AttackInterval = 1f;
    public int AttackDamage = 1;
    public int Price;


    public Projectile.EffectTypes AttackEffect;
    public float SplashRadius = 1;
    public float SlowingEffectTime = 1f;
    public float SlowEffectModifier = 0.5f;

    public GameObject Projectile;
    public float ProjectileSpeed = 20;

    private GameObject _targetEnemy;

    private bool _shoot = false;

    void Start()
    {
        StartCoroutine(Shoot());
    }

  

    void Update()
    {
        if (_targetEnemy != null)
        {
            _shoot = true;
        }
        else
        {
            _shoot = false;
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (_shoot && _targetEnemy != null)
            {
                SpawnBullet();
                yield return new WaitForSeconds(AttackInterval);
            }
            yield return null;
        }
    }


    void OnTriggerStay2D(Collider2D Other)
    {
        if (Other.tag == "Enemy")
        {
            if (_targetEnemy == null)
            {
     

                    _targetEnemy = Other.gameObject;

               
                
            }
            else
            {
                if(AttackEffect == global::Projectile.EffectTypes.Slowing)
                {
                    if(_targetEnemy.GetComponent<Enemy>().isSlowed)
                    {
                        _targetEnemy = null;
                    }
                }
            }


       
        }
    }

    void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.tag == "Enemy")
        {
            if (Other.gameObject == _targetEnemy)
            {
                _targetEnemy = null;
                _shoot = false;
            }
        }
    }

    void SpawnBullet()
    {
        // spawn bullet
        GameObject bullet = Instantiate(Projectile) as GameObject;
        bullet.transform.position = transform.position;

        Projectile p = bullet.GetComponent<Projectile>();
        p.Target = _targetEnemy;
        p.BulletDamage = AttackDamage;
        p.BulletEffect = AttackEffect;
        p.Speed = ProjectileSpeed;

        p.SplashRadius = 1;
        p.SlowingEffectTime = 1f;
        p.SlowEffectModifier = 0.5f;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, GetComponent<CircleCollider2D>().radius * transform.localScale.x);
    }
}
