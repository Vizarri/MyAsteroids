using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{   // ������ ������� ��������� , �������� � ������ �� �������� Asteroid
    [SerializeField]
    GameObject prefabAsteroid;

    GameObject asteroid;


    // Start is called before the first frame update
    void Start()
    {
        // ������ � ������ ������
        float screenWidth = ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft;
        float screenHeight = ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom;

        // ������ �������� ������� ���������
        GameObject asteroid = GameObject.Instantiate(prefabAsteroid);
        float radius_asteroid = prefabAsteroid.GetComponent<CircleCollider2D>().radius;
        Destroy(asteroid);


        //�������� ����� � ������� ���� ������ ������
        asteroid = GameObject.Instantiate(prefabAsteroid);
        Asteroid script = asteroid.GetComponent<Asteroid>();
        script.Initialize(Direction.Up, new Vector3(ScreenUtils.ScreenLeft + (screenWidth / 2), ScreenUtils.ScreenBottom - radius_asteroid));

        //�������� ����� � �������� ���� ������ ����
        asteroid = GameObject.Instantiate(prefabAsteroid);
        script = asteroid.GetComponent<Asteroid>();
        script.Initialize(Direction.Down, new Vector3(ScreenUtils.ScreenLeft + (screenWidth / 2), ScreenUtils.ScreenTop - radius_asteroid));

        //�������� ����� � ������ ���� ������ �� �����
        asteroid = GameObject.Instantiate(prefabAsteroid);
        script = asteroid.GetComponent<Asteroid>();
        script.Initialize(Direction.Right, new Vector3(ScreenUtils.ScreenLeft-radius_asteroid, ScreenUtils.ScreenTop - (screenHeight / 2)));

        //�������� ����� � ������� ���� ������ �� ����
        asteroid = GameObject.Instantiate(prefabAsteroid);
        script = asteroid.GetComponent<Asteroid>();
        script.Initialize(Direction.Left, new Vector3(ScreenUtils.ScreenRight-radius_asteroid, ScreenUtils.ScreenTop - (screenHeight / 2)));

    }
}
