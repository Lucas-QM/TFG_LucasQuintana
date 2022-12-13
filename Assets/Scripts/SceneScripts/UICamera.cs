using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    public Transform player;
    public float xpos, ypos, zpos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.position.x + xpos, player.position.y + ypos, zpos);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + xpos, player.position.y + ypos, zpos);
    }
}
