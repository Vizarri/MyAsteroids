using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject HUD;

    // скрипт отвечает за движение корабля и его уничтожение
    Rigidbody2D rb2d;
    Vector2 thrustDirection= new Vector2(1,0);
    const int ThrustForce = 10;
    const int rotateDegreesPerSecond = 180;
    [SerializeField]
    GameObject prefabBullet;
    // доп поле для поддержки метода невыхода корабля за пределы камеры
    float circleRadius;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationInput= Input.GetAxis("Rotate");
        if (rotationInput!=0)
        {
            // рассчет величины вращения и применение вращения
            float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // изменяет направление движения в соответствии с вращением корабля
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }
        // Выстрел пуль
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AudioManager.Play(AudioClipName.PlayerShot);
            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);
        }
    }

    // Дает толчок объекту , при активации оси "Thrust"" 
    private void FixedUpdate()
    {
        if (Input.GetAxis("Thrust")!=0)
        {
            rb2d.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collision2D myCollision)
    {
        
        // если объект столкновения имеет тэг "Asteroid" - то взрыв корабля,остановка таймера жизни,и звуковой эффект
        if (myCollision.gameObject.CompareTag("Asteroid"))
        {
            AudioManager.Play(AudioClipName.PlayerDeath);
            HUD.GetComponent<HUD>().StopGameTimer();
            GameObject.Destroy(gameObject);         
        }
    }
}


