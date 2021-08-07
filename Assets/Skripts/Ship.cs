using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject HUD;

    // ������ �������� �� �������� ������� � ��� �����������
    Rigidbody2D rb2d;
    Vector2 thrustDirection= new Vector2(1,0);
    const int ThrustForce = 10;
    const int rotateDegreesPerSecond = 180;
    [SerializeField]
    GameObject prefabBullet;
    // ��� ���� ��� ��������� ������ �������� ������� �� ������� ������
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
            // ������� �������� �������� � ���������� ��������
            float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // �������� ����������� �������� � ������������ � ��������� �������
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }
        // ������� ����
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AudioManager.Play(AudioClipName.PlayerShot);
            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);
        }
    }

    // ���� ������ ������� , ��� ��������� ��� "Thrust"" 
    private void FixedUpdate()
    {
        if (Input.GetAxis("Thrust")!=0)
        {
            rb2d.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collision2D myCollision)
    {
        
        // ���� ������ ������������ ����� ��� "Asteroid" - �� ����� �������,��������� ������� �����,� �������� ������
        if (myCollision.gameObject.CompareTag("Asteroid"))
        {
            AudioManager.Play(AudioClipName.PlayerDeath);
            HUD.GetComponent<HUD>().StopGameTimer();
            GameObject.Destroy(gameObject);         
        }
    }
}


