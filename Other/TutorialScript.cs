using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    const float INCREMENT = 0.05f;
    private float opacity;
    private bool isActive;

    private void Start()
    {
        opacity = 0f;
        isActive = false;
    }
    private void Update()
    {
        if (isActive)
        {
            if (opacity < 1f)
            {
                opacity += INCREMENT;
            }
        }
        else
        {
            if (opacity > 0f)
            {
                opacity -= INCREMENT;
            }
        }
        gameObject.GetComponent<SpriteRenderer>().color =
            new Color(1f, 1f, 1f, opacity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = false;
        }
    }
}
