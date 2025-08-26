using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloud : MonoBehaviour
{
    [SerializeField] private float timeInBetweenStrikes;
    [SerializeField] private GameObject rain;
    private GameObject rainInstance;
    private float timeInBetweenTimer;
    [SerializeField] private AK.Wwise.Event Droplet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeInBetweenTimer -= Time.deltaTime;

        if (timeInBetweenTimer <= 0)
        {
            Spawn();
            timeInBetweenTimer = timeInBetweenStrikes;
        }
    }

    private void Spawn()
    {
        rainInstance = Instantiate(rain, transform);
        rainInstance.transform.position = transform.position;
        Droplet.Post(gameObject);
    }
}
