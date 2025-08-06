using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class WindTunnel : MonoBehaviour
{
    public enum Orientation
    {
        Left,
        Right,
        Up,
        Down
    }

    public Orientation orientation;
    [SerializeField] private Vector2 boxSize;
    private float distance;
    [SerializeField] private float windForce;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private ParticleSystem WindParticles;
    [SerializeField] private BoxCollider2D boxCollider;
    private ParticleSystem.MainModule psMain;
    private ParticleSystem.ShapeModule psShape;
    private float lifeTime;
    [SerializeField] private bool buttonActivated;
    private bool buttonPress;
    [SerializeField] private AK.Wwise.Event fanSound;
   

    private void OnValidate()
    {
        distance = Mathf.Abs(boxSize.y/2 + 2);
        boxCollider.offset = new Vector2(0, distance);
        boxCollider.size = boxSize;
        lifeTime = (boxSize.y / WindParticles.main.startSpeed.constant);
        psMain = WindParticles.main;
        psShape = WindParticles.shape;
    }
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Artist").GetComponent<CircleCollider2D>(), boxCollider);
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Drawing").GetComponent<CircleCollider2D>(), boxCollider);
        psMain.startLifetime = lifeTime;
        psShape.scale = new Vector3(boxSize.x, WindParticles.shape.scale.y, WindParticles.shape.scale.z);
        if(orientation == Orientation.Left || orientation == Orientation.Right)
        {
            psMain.startSizeX = 2;
            psMain.startSizeY = 0.1f;
        }

        else if (orientation == Orientation.Up || orientation == Orientation.Down)
        {
            psMain.startSizeX = 0.1f;
            psMain.startSizeY = 2;
        }

        fanSound.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonActivated)
        {
            if (buttonPress)
            {
                WindParticles.Stop();
            }

            else if (!WindParticles.isPlaying)
            {
                WindParticles.Play();
            }
        }

        else if (!WindParticles.isPlaying)
        {
            WindParticles.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (buttonActivated)
        {
            if (buttonPress)
            {

            }

            else
            {
                WindActive(collision);
            }
        }

        else
        {
            WindActive(collision);
        }
    }

    public void OnButtonPress()
    {
        buttonPress = true;
    }

    public void OnButtonRelease()
    {
        buttonPress = false;
    }

    private void WindActive(Collider2D collision)
    {
        switch (orientation)
        {
            case Orientation.Up:
                {
                    if (collision != null)
                    {
                        float counterGravity = (-Physics2D.gravity.y * collision.GetComponent<Rigidbody2D>().gravityScale);

                        float force;

                        force = counterGravity + (windForce + (-collision.gameObject.GetComponent<Rigidbody2D>().velocity.y));
                        Debug.Log(force);

                        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Force);
                    }
                    break;
                }
            case Orientation.Down:
                {
                    if (collision != null)
                    {

                        float force;

                        force = (windForce * 2);

                        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -force), ForceMode2D.Force);
                    }
                    break;
                }
            case Orientation.Left:
                {
                    if (collision != null)
                    {

                        float force;

                        force = (windForce * 2);

                        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-force, 0), ForceMode2D.Force);
                    }
                    break;
                }
            case Orientation.Right:
                {
                    if (collision != null)
                    {

                        float force;

                        force = (windForce * 2);

                        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0), ForceMode2D.Force);
                    }
                    break;
                }
        }
    }
}
