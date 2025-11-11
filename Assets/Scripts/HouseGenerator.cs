using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class HouseGenerator : MonoBehaviour
{
    [Header("House Settings")]
    public GameObject housePrefab;
    public int minHouses = 2;
    public int maxHouses = 8;
    public float spacing = 2f;

    [Header("Position Settings")]
    public float topY = 4f;
    public float startX = 0;
    public float barrierY = 3f;

    private GameObject barrier;

    void Start()
    {
        if (barrier == null)
            barrier = GameObject.FindWithTag("Barrier");

        if (GameManager.Instance.barrierData != null && barrier != null)
        {
            barrier.transform.position = GameManager.Instance.barrierData.position;
            barrier.transform.localScale = GameManager.Instance.barrierData.scale;
        }

        if (GameManager.Instance.houseList.Count > 0)
        {
            foreach (var data in GameManager.Instance.houseList)
            {
                GameObject house = Instantiate(housePrefab, data.position, Quaternion.identity);
                house.transform.parent = transform;

                ApplyColorToChild(house, "Wall", data.wallColor);
                ApplyColorToChild(house, "HouseOverlays", data.roofColor);
                ApplyColorToChild(house, "Curtains", data.curtainColor);

                Door door = house.GetComponentInChildren<Door>();
                if (door != null)
                {
                    door.customerName = data.customerType;
                }
            }
        }
        else
        {
            GenerateHouse();
        }

        if (GameManager.Instance.barrierData != null && GameManager.Instance.barrierData.scale != Vector3.zero)
        {
            if (barrier == null)
            {
                barrier = new GameObject("Barrier");
                BoxCollider2D col = barrier.AddComponent<BoxCollider2D>();
                col.isTrigger = false;

                SpriteRenderer sr = barrier.AddComponent<SpriteRenderer>();
                sr.color = new Color(0, 0, 0, 0.3f);
                sr.sprite = Resources.GetBuiltinResource<Sprite>("Sprites/Default.psd");
            }

            barrier.transform.position = GameManager.Instance.barrierData.position;
            barrier.transform.localScale = GameManager.Instance.barrierData.scale;
        }

    }

    public void GenerateHouse()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        List<HouseData> houseDataList = new List<HouseData>();

        int houseCount = Random.Range(minHouses, maxHouses + 1);
        float totalWidth = (houseCount - 1) * spacing;
        float startOffset = -totalWidth / 2;
        
        for (int i = 0; i < houseCount; i++)
        {
            Vector3 position = new Vector3(startOffset + i * spacing, topY, 0);
            GameObject house = Instantiate(housePrefab, position, Quaternion.identity);
            house.transform.parent = transform;

            Color wallColor = GetRandomColor();
            Color roofColor = GetRandomColor();
            Color curtainColor = GetRandomColor();

            
            ApplyColorToChild(house, "Wall", wallColor);
            ApplyColorToChild(house, "HouseOverlays", roofColor);
            ApplyColorToChild(house, "Curtains", curtainColor);

            string[] customerTypes = { "normal_customer", "angry_customer", "eccentric_customer", "goth_customer", "the_wizard" };
            string randomCustomer = customerTypes[Random.Range(0, customerTypes.Length)];

            Door door = house.GetComponentInChildren<Door>();
            if (door != null)
            {
                door.customerName = randomCustomer;
            }

            houseDataList.Add(new HouseData
            {
                position = position,
                customerType = randomCustomer,
                wallColor = wallColor,
                roofColor = roofColor,
                curtainColor = curtainColor,
                visited = false
            });
        }

        GameManager.Instance.houseList = houseDataList;

        if (barrier == null)
        {
            barrier = new GameObject("Barrier");
            BoxCollider2D col = barrier.AddComponent<BoxCollider2D>();
            col.isTrigger = false;
        }

        barrier.transform.position = new Vector3(0, barrierY, 0);
        barrier.transform.localScale = new Vector3(totalWidth + 5, .5f, 1);

        GameManager.Instance.barrierData = new BarrierData
        {
            position = barrier.transform.position,
            scale = barrier.transform.localScale
        };
    }

        Color GetRandomColor()
        {
            return new Color(
                Random.Range(.5f, 1f),
                Random.Range(.5f, 1f),
                Random.Range(.5f, 1f)
                );
        }

        void ApplyColorToChild(GameObject parent, string childName, Color color)
    {
        Transform child = parent.transform.Find(childName);
        if (child == null) return;

        foreach (var renderer in child.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.color = color;
        }
    }

    }

