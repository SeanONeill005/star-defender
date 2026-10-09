using NUnit.Framework.Constraints;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float movingCooldown = 3.0f;
    private float timer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > movingCooldown)
        {
            timer = 0.0f;
            int direction = Random.Range(0, 3);
            Vector3 pos = gameObject.transform.position;
            switch (direction)
            {
                case 0:
                    pos.x -= 1.0f;
                    break;
                case 1:
                    pos.x += 1.0f;
                    break;
                case 2:
                    pos.y -= 1.0f;
                    break;
            }
            gameObject.transform.position = pos;
        }
    }
}
