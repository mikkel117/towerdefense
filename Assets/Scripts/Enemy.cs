using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{

    public List<Transform> WayPoints = new List<Transform>();
    public float RangeBeforeNextWaypoint = 0.2f;
    public int AttackPower = 1;
    public float Speed = 1;
    public int Health = 1;
    public int GoldGainedWhenKilled = 1;

    private int _currentWayPoint = 0;
    [HideInInspector]
    public bool isSlowed = false;
    private float slowtime = 0;

    // Use this for initialization
    void Start() {

    }

    public void SlowDown(float modifier, float time)
    {

        slowtime = time;
        if (!isSlowed)
        {
            print("start c");
            StartCoroutine(Slow(modifier));
        }
        isSlowed = true;
 
    
    }

    IEnumerator Slow(float modifier)
    {

        float oldSpeed = Speed;
       
        while (slowtime > 0)
        {
            slowtime -= Time.deltaTime;
            Speed = oldSpeed * modifier;
            yield return null;
        }
        Speed = oldSpeed;
        isSlowed = false;
    }

    public void TakeDamage(int dmg)
    {
        if (Health > 0)
        {
            Health -= dmg;
        }

        if (Health <= 0)
        {

            GameObject.Find("GameController").GetComponent<GameController>().ModifyGold(GoldGainedWhenKilled);
            Destroy(gameObject);
        }
    }

        // Update is called once per frame
        void Update()
        {

            transform.position = Vector2.MoveTowards(transform.position, WayPoints[_currentWayPoint].position, Time.deltaTime * Speed);
            //  Debug.Log(Vector2.Distance(transform.position, WayPoints[_currentWayPoint].position));
            if (Vector2.Distance(transform.position, WayPoints[_currentWayPoint].position) < RangeBeforeNextWaypoint)
            {
                if (_currentWayPoint + 1 == WayPoints.Count)
                {
                    // Damage player
                    GameObject.Find("GameController").GetComponent<GameController>().TakeDamage(AttackPower);
                    Destroy(gameObject);
                }
                else
                {
                    _currentWayPoint++;

                }
            }

        }
    
}
