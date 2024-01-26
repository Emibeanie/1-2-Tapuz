using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dustTrail : MonoBehaviour
{
    [SerializeField] GameObject dustParticle;

    private float _timeBtwSpawns;
    public float _startTimeBtwSpawns;
    private Rigidbody2D RB;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        {
            if(RB.velocity.x > 0.01 || RB.velocity.y > 0.01)
            if (_timeBtwSpawns <= 0)
            {
                GameObject instance = (GameObject)Instantiate(dustParticle, transform.position, Quaternion.identity);
                Destroy(instance, 4f);
                _timeBtwSpawns = _startTimeBtwSpawns;
            }
            else
                _timeBtwSpawns -= Time.deltaTime;
        }
    }
}
