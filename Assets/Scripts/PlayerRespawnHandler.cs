using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawnHandler : MonoBehaviour
{
    [SerializeField] private float timeToRespawn;
    private bool isRespawning = false;
    private ArtistDeath artistDeath;
    private DrawingDeath drawingDeath;
    [SerializeField] private GameObject artistDeathParticles;
    [SerializeField] private GameObject drawingDeathParticles;
    // Start is called before the first frame update
    void Start()
    {
        artistDeath = GameObject.FindGameObjectWithTag("Artist").GetComponent<ArtistDeath>();
        drawingDeath = GameObject.FindGameObjectWithTag("Drawing").GetComponent<DrawingDeath>();

    }

    // Update is called once per frame
    void Update()
    {
        if(drawingDeath != null && drawingDeath.drawingDead)
        {
            DrawingRespawn();
            isRespawning = true;
        }

        if(artistDeath != null && artistDeath.artistDead)
        {
            ArtistRespawn();
            isRespawning = true;
        }
        
    }

    private void DrawingRespawn()
    {
        if (!isRespawning)
        {
            if (drawingDeath.drawingDead)
            {
                StartCoroutine("DrawingRespawnCo");
            }
        }
    }

    public IEnumerator DrawingRespawnCo()
    {
        Instantiate(drawingDeathParticles, drawingDeath.gameObject.transform.position, drawingDeath.gameObject.transform.rotation); // spawns the death particles
        drawingDeath.gameObject.SetActive(false); // turns player off

        yield return new WaitForSeconds(timeToRespawn); // gives time for player to respawn so respawn is not so sudden

        isRespawning = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    private void ArtistRespawn()
    {
        if (!isRespawning)
        {
            if (artistDeath.artistDead)
            {
                StartCoroutine("ArtistRespawnCo");
            }
        }
    }

    public IEnumerator ArtistRespawnCo()
    {
        Instantiate(artistDeathParticles, artistDeath.gameObject.transform.position, artistDeath.gameObject.transform.rotation); // spawns the death particles
        artistDeath.gameObject.SetActive(false); // turns player off

        yield return new WaitForSeconds(timeToRespawn); // gives time for player to respawn so respawn is not so sudden

        isRespawning = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }
}
