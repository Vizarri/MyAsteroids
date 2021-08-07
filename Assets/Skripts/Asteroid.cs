using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{ // Скрипт рандомного движения объекта и рандомного выбора спрайта
    const float MinImpulseForce = 1f;
    const float MaxImpulseForce = 3f;
    float angle;
    [SerializeField]
    Sprite asteroid1;
    [SerializeField]
    Sprite asteroid2;
    [SerializeField]
    Sprite asteroid3;

    // Start is called before the first frame update
    void Start()
    {        //рандомное назначение префаба для астероида 
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        int random_sprite = Random.Range(0, 3);
        if (random_sprite==0)
        {
            sprite.sprite = asteroid1;
        }
        else if (random_sprite == 1)
        {
            sprite.sprite = asteroid2;
        }
        else if (random_sprite == 2)
        {
            sprite.sprite = asteroid3;
        }
    }
    public void Initialize (Direction direction,Vector3 position)
    {
        // set asteroid position
        transform.position = position;

        // set random angle based on direction
        float random_angle = Random.Range(0, 30 * Mathf.Deg2Rad);

        if (direction == Direction.Up)
        {
            angle = 75 * Mathf.Deg2Rad + random_angle;
        }
        else if (direction == Direction.Left)
        {
            angle = 165 * Mathf.Deg2Rad + random_angle;
        }
        else if (direction == Direction.Down)
        {
            angle = 255 * Mathf.Deg2Rad + random_angle;
        }
        else
        {
            angle = -15 * Mathf.Deg2Rad + random_angle;
        }
        StartMoving(angle);
    }
    void OnCollisionEnter2D(Collision2D myCollision)
    {
        
        // если объект столкновения имеет тэг "Bullet" и астероид полноразмерный, то разбивка остероида на 2 
        if (myCollision.gameObject.CompareTag("Bullet"))
        {
            AudioManager.Play(AudioClipName.AsteroidHit);
            Destroy(myCollision.gameObject);
            
            if (gameObject.transform.localScale.x > 0.5)
            {
                Vector3 skale = transform.localScale;
                skale.x /= 2;
                skale.y /= 2;
                transform.localScale = skale;
                CircleCollider2D collider = GetComponent<CircleCollider2D>();
                collider.radius /= 2;

                GameObject newAsteroid = Instantiate<GameObject>(gameObject, transform.position, Quaternion.identity);
                newAsteroid.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI));

                newAsteroid = Instantiate<GameObject>(gameObject, transform.position, Quaternion.identity);
                newAsteroid.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI));

                Destroy(gameObject);
            }
            else 
            {
                Destroy(gameObject);
            }
        }
    }
    void StartMoving (float angle)
    {
        // apply impulse force to get asteroid moving
        Vector2 moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(moveDirection * magnitude, ForceMode2D.Impulse);
    }


}
