using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    [SerializeField] Transform bulletPoint;
    [SerializeField] Bullet bulletPreFab;
    Bullet[] bulletPool;
    [SerializeField] float coolDownTime = 0.5f;
    float timeToNext = 0;
    int currentBullet = 0;
    void Start()
    {
        ShipComponent shipComponent = GetComponent<ShipComponent>();
        shipComponent.A_OnKey += Fire;
        // shipComponent.A_OnUpdated += Update;
        SetUpPool();
    }

    void Fire()
    {
        if(timeToNext < 0)
        {
            bulletPool[currentBullet].Fire(bulletPoint);
            currentBullet++;
            if(currentBullet >= bulletPool.Length)
            {
                currentBullet = 0;
            }
            timeToNext = coolDownTime;
        }
    }

    private void Update()
    {
        timeToNext -= Time.deltaTime;
    }

    void SetUpPool()
    {
        bulletPool = new Bullet[20];
        for (int i = 0; i < bulletPool.Length; i++)
        {
            bulletPool[i] = Instantiate(bulletPreFab);
            bulletPool[i].gameObject.SetActive(false);
            DontDestroyOnLoad(bulletPool[i].gameObject);
        }
        currentBullet = 0;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < bulletPool.Length; i++)
        {
            if(bulletPool[i] != null)
            Destroy(bulletPool[i].gameObject);
        }
    }
}
