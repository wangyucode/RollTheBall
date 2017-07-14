using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public GameObject gameManager;

    public float speedZ = 1;

    public float speedX = 1;

    private int powerZ = 1;

    private Rigidbody rb;
    private MeshRenderer render;

    private Vector2 downPosition;
    private Vector2 currentPosition;

    public Material mMetal;
    public Material mWood;

    private float xAxis;

    private float speedZOriginal;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        render = GetComponent<MeshRenderer>();

        speedZOriginal = speedZ;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.currentState != GameState.State.PLAY)
        {
            return;
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                downPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                currentPosition = Input.mousePosition;
                xAxis = currentPosition.x - downPosition.x;
            }

        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    downPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    currentPosition = touch.position;
                    xAxis = currentPosition.x - downPosition.x;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameState.currentState != GameState.State.PLAY)
        {
            return;
        }
        rb.AddForce(new Vector3(xAxis * speedX, -150, speedZ*powerZ), ForceMode.Force);
        //rb.velocity = new Vector3(speedX, rb.velocity.y, speedZ);
    }

    //public void changeDirection()
    //{

    //    rb.velocity = new Vector3(rb.velocity.x * -1, rb.velocity.y, rb.velocity.z);
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        string tag = other.gameObject.tag;
        if (tag.Equals("FloorMetal"))
        {
            render.material = mMetal;
            rb.mass = 20;
            powerZ = 2;
            other.gameObject.SetActive(false);
        }
        else if (tag.Equals("FloorWood"))
        {
            render.material = mWood;
            rb.mass = 5;
            powerZ = 1;
            other.gameObject.SetActive(false);
        }
        else if (tag.Equals("FloorLarger"))
        {
            transform.localScale = transform.localScale + Vector3.one * 0.1f;
            other.gameObject.SetActive(false);
        }
        else if (tag.Equals("FloorSmaller"))
        {
            transform.localScale = transform.localScale - Vector3.one * 0.1f;
            other.gameObject.SetActive(false);
        }

        else if (tag.Equals("FloorFaster"))
        {
            speedZ += 10;
            other.gameObject.SetActive(false);
        }
        else if (tag.Equals("FloorSlower"))
        {
            speedZ -= 10;
            other.gameObject.SetActive(false);
        }
    }

    public void resetBall()
    {
        //重置材质
        render.material = mWood;
        rb.mass = 5;
        powerZ = 1;
        //重置位置速度
        transform.position = new Vector3(0, 1, 0);
        rb.velocity = Vector3.zero;
        downPosition = Vector3.zero;
        currentPosition = Vector3.zero;
        xAxis = 0;
        //重置大小
        transform.localScale = Vector3.one * 0.5f;
        //重置Z轴力量大小
        speedZ = speedZOriginal;
    }

   
}
