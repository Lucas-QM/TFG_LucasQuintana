using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingBulletScript : MonoBehaviour
{
    private float speed;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<Enemy>().speed;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (target.transform.position.x < transform.position.x)
        {
            transform.localScale *= new Vector2(-1, transform.localScale.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
