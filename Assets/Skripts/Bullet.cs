using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float maxSecondsBulletsLife = 2;
    Timer deathTimer;

    //метод задает движение пули
    public void ApplyForce(Vector2 direction)
    {
        const int force = 500;
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(force * direction);

    }

    // Start is called before the first frame update
    void Start()
    {
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = maxSecondsBulletsLife;
        deathTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer.Finished)
        {
            Destroy(gameObject);
        }
    }
}
