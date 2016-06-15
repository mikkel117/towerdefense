using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public enum EffectTypes { NoEffect, Slowing, Splash }

    [HideInInspector]
    public EffectTypes BulletEffect;
    [HideInInspector]
    public GameObject Target;
    [HideInInspector]
    public float Speed = 20f;
    [HideInInspector]
    public int BulletDamage = 1;

    [HideInInspector]
    public float SplashRadius = 1;
    [HideInInspector]
    public float SlowingEffectTime = 1f;
    [HideInInspector]
    public float SlowEffectModifier = 0.5f;

    // Update is called once per frame
    void Update()
    {

        if (Target == null)
        {
            print("Bullet can't find Target, destroying bullet");
            Destroy(gameObject);
        }
        else
        {
            
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * Speed);
            if (Vector2.Distance(transform.position, Target.transform.position) < 0.2f)
            {
                if (BulletEffect == EffectTypes.NoEffect)
                {
                    Target.GetComponent<Enemy>().TakeDamage(BulletDamage);
                }
                else if(BulletEffect == EffectTypes.Splash)
                {
                    Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, SplashRadius);
                    foreach(Collider2D c in cols)
                    {
                        if(c.tag =="Enemy")
                        {
                            Target.GetComponent<Enemy>().TakeDamage(BulletDamage);
                        }
                    }
                }
                else if(BulletEffect == EffectTypes.Slowing)
                {
                    Target.GetComponent<Enemy>().TakeDamage(BulletDamage);
                    Target.GetComponent<Enemy>().SlowDown(SlowEffectModifier, SlowingEffectTime);
                }


                Destroy(gameObject);
            }
        }



    }
}
