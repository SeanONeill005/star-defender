using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public float speed = 2.0f;
    public float stopHeight = 0.0f;

    void Update()
    {
        Vector3 position = transform.position;
        position.y -= speed * Time.deltaTime;
        if (position.y < stopHeight)
        {
            position.y = stopHeight;
        }
        transform.position = position;
    }
}
