using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    Rigidbody2D rigidBody2D;
    CircleCollider2D circleCollider2D;

    public Camera camera;

    //Camera camera;
    public float velocityMin;

    Vector2 LastPos;

    public bool isCutting;

    public GameObject LinePrefab;
    private GameObject currentLine;

    // Start is called before the first frame update
    void Start()
    {
        //camera = Camera.main;
        
        rigidBody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCut();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCut();
        }

        if (isCutting)
        {
            UpdateCut();
        }
        
    }

    void UpdateCut()
    {
        Vector2 newPos = camera.ScreenToWorldPoint(Input.mousePosition);
        rigidBody2D.position = newPos;

        float velocity = (newPos - LastPos).magnitude * Time.deltaTime;

        if (velocity > velocityMin)
        {
            circleCollider2D.enabled = true;
        }else
        {
            circleCollider2D.enabled = false;
        }

        LastPos = newPos;
    }

    void StartCut()
    {
        isCutting = true;


        currentLine = Instantiate(LinePrefab, transform);


        circleCollider2D.enabled = true;
        LastPos = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void StopCut()
    {
        isCutting = false;
        currentLine.transform.SetParent(null);
        Destroy(currentLine, 2f);
        circleCollider2D.enabled = false;
    }
}
