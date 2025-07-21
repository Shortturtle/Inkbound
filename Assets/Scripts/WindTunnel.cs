using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    private float lifeTime;
    [SerializeField] private bool buttonActivated;
    private bool buttonPress;
   

    private void OnValidate()
    {
        distance = Mathf.Abs(boxSize.y/2 + 2);
        boxCollider.offset = new Vector2(0, distance);
        boxCollider.size = boxSize;
        lifeTime = (boxSize.y / WindParticles.main.startSpeed.constant);
        psMain = WindParticles.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Artist").GetComponent<CircleCollider2D>(), boxCollider);
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Drawing").GetComponent<CircleCollider2D>(), boxCollider);
        psMain.startLifetime = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {

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
                switch (orientation)
                {
                    case Orientation.Up:
                        {
                            if (collision != null)
                            {
                                float ratio = Mathf.Abs((collision.gameObject.transform.position.y / (transform.position.y - 2)) + 1);
                                Debug.Log(ratio);
                                float counterGravity = (-Physics2D.gravity.y * collision.GetComponent<Rigidbody2D>().gravityScale);

                                float force;

                                force = counterGravity + (windForce + (-collision.gameObject.GetComponent<Rigidbody2D>().velocity.y * ratio));

                                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Force);
                            }
                            break;
                        }
                    case Orientation.Down:
                        {
                            if (collision != null)
                            {
                                float ratio = Mathf.Abs((collision.gameObject.transform.position.y / (transform.position.y - 2)) + 1);
                                Debug.Log(ratio);
                                float counterGravity = (-Physics2D.gravity.y * collision.GetComponent<Rigidbody2D>().gravityScale);

                                float force;

                                force = counterGravity + (windForce + (-collision.gameObject.GetComponent<Rigidbody2D>().velocity.y * ratio));

                                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -force), ForceMode2D.Force);
                            }
                            break;
                        }
                    case Orientation.Left:
                        {
                            if (collision != null)
                            {
                                float ratio = Mathf.Abs((collision.gameObject.transform.position.x / (transform.position.x - 2)) + 1);
                                Debug.Log(ratio);
                                float counterGravity = (-Physics2D.gravity.y * collision.GetComponent<Rigidbody2D>().gravityScale);

                                float force;

                                force = counterGravity + (windForce + (-collision.gameObject.GetComponent<Rigidbody2D>().velocity.y * ratio));

                                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-force, 0), ForceMode2D.Force);
                            }
                            break;
                        }
                    case Orientation.Right:
                        {
                            if (collision != null)
                            {
                                float ratio = Mathf.Abs((collision.gameObject.transform.position.x / (transform.position.y - 2)) + 1);
                                Debug.Log(ratio);
                                float counterGravity = (-Physics2D.gravity.y * collision.GetComponent<Rigidbody2D>().gravityScale);

                                float force;

                                force = counterGravity + (windForce + (-collision.gameObject.GetComponent<Rigidbody2D>().velocity.x * ratio));

                                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0), ForceMode2D.Force);
                            }
                            break;
                        }
                }
            }
        }

        else
        {
            switch (orientation)
            {
                case Orientation.Up:
                    {
                        if (collision != null)
                        {
                            float ratio = Mathf.Abs((collision.gameObject.transform.position.y / (transform.position.y - 2)) + 1);
                            Debug.Log(ratio);
                            float counterGravity = (-Physics2D.gravity.y * collision.GetComponent<Rigidbody2D>().gravityScale);

                            float force;

                            force = counterGravity + (windForce + (-collision.gameObject.GetComponent<Rigidbody2D>().velocity.y * ratio));

                            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Force);
                        }
                        break;
                    }
                case Orientation.Down:
                    {
                        if (collision != null)
                        {
                            float ratio = Mathf.Abs((collision.gameObject.transform.position.y / (transform.position.y - 2)) + 1);
                            Debug.Log(ratio);
                            float counterGravity = (-Physics2D.gravity.y * collision.GetComponent<Rigidbody2D>().gravityScale);

                            float force;

                            force = counterGravity + (windForce + (-collision.gameObject.GetComponent<Rigidbody2D>().velocity.y * ratio));

                            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -force), ForceMode2D.Force);
                        }
                        break;
                    }
                case Orientation.Left:
                    {
                        if (collision != null)
                        {
                            float ratio = Mathf.Abs((collision.gameObject.transform.position.x / (transform.position.x - 2)) + 1);
                            Debug.Log(ratio);
                            float counterGravity = (-Physics2D.gravity.y * collision.GetComponent<Rigidbody2D>().gravityScale);

                            float force;

                            force = counterGravity + (windForce + (-collision.gameObject.GetComponent<Rigidbody2D>().velocity.y * ratio));

                            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-force, 0), ForceMode2D.Force);
                        }
                        break;
                    }
                case Orientation.Right:
                    {
                        if (collision != null)
                        {
                            float ratio = Mathf.Abs((collision.gameObject.transform.position.x / (transform.position.y - 2)) + 1);
                            Debug.Log(ratio);
                            float counterGravity = (-Physics2D.gravity.y * collision.GetComponent<Rigidbody2D>().gravityScale);

                            float force;

                            force = counterGravity + (windForce + (-collision.gameObject.GetComponent<Rigidbody2D>().velocity.x * ratio));

                            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0), ForceMode2D.Force);
                        }
                        break;
                    }
            }
        }
    }

    public void OnButtonPress()
    {
        buttonPress = true;
        WindParticles.Stop();
    }

    public void OnButtonRelease()
    {
        buttonPress = false;
        WindParticles.Play();
    }
}
