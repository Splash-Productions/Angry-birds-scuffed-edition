//for this script i used this tutorials: https://www.youtube.com/watch?v=Tsha7rp58LI&list=WL&index=4&ab_channel=MuddyWolf & https://www.youtube.com/watch?v=3DUmpVi82q8

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragShoot : MonoBehaviour
{
    public float power;
    public Vector2 minPower;
    public Vector2 maxPower;
    Rigidbody2D rb;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    public GameObject pointPrefab;
    public GameObject[] points;
    public int numberOfPoints;
    bool isDragging = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        points = new GameObject[numberOfPoints];
        for(int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
        
    }
    private void Update()
    {
        if (isDragging)
        {
            onDrag();
        }
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 10;
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 10;
            isDragging = false;
            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            rb.AddForce(force * power, ForceMode2D.Impulse);
            GameManager.isInGame = true;
        }
        
        
    }
    Vector2 PointPos(float t)
    {
        Vector2 currentPointPos = (Vector2)transform.position + (force.normalized * t *  power * 0.8f) + 0.1f * Physics2D.gravity * (t*t);
        return currentPointPos;
    }
    void onDrag()
    {
        endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endPoint.z = 10;
        force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position = PointPos(i * 0.1f);
        }
    }
}
