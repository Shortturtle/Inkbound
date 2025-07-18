using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCloud : MonoBehaviour
{
    [SerializeField] private float timeInBetweenStrikes;
    [SerializeField] private float lifeTimeOfLightning;
    [SerializeField] private GameObject lightning;
    private GameObject lightningInstance;
    private float timeInBetweenTimer;
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
        lightningInstance = Instantiate(lightning, transform);
        lightningInstance.transform.position = transform.position;
        lightningInstance.GetComponent<LightningHandler>().SetTime(lifeTimeOfLightning);
    }
}
