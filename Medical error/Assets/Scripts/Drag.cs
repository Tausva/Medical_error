using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector2 startPosition;
    private bool dragging = false;
    private float distance;
    private bool isInPlace = false;
    private GameObject target;
    private bool isDraggable = true;
    private bool isVenusBonus = false;
    public bool isVenusEdible = false;

    private void Start()
    {
        startPosition = transform.position;
    }

    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (isDraggable)
        {
            dragging = true;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        } else
        {
            if (isInPlace)
            {
                target.GetComponent<Mixing>().AddComponent(gameObject.name);
                isInPlace = false;
            } else if (isVenusBonus && isVenusEdible)
            {
                target.GetComponent<FlytrapManager>().Eat();
                isVenusBonus = false;
            }
            transform.position = startPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "MixingTable")
        {
            isInPlace = true;
            target = col.gameObject;
        } else if (col.gameObject.tag == "Venus")
        {
            target = col.gameObject;
            isVenusBonus = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "MixingTable")
            isInPlace = false;
        else if (col.gameObject.tag == "Venus")
            isVenusBonus = false;
    }

    public void EnableDragging()
    {
        isDraggable = true;
    }

    public void DisableDragging()
    {
        isDraggable = false;
    }
}
