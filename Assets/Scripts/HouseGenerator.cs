using UnityEngine;

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
        GenerateHouse();
    }

    public void GenerateHouse()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int houseCount = Random.Range(minHouses, maxHouses + 1);

        float totalWidth = (houseCount - 1) * spacing;
        float startOffset = -totalWidth / 2;

        for (int i = 0; i < houseCount; i++)
        {
            Vector3 position = new Vector3(startOffset + i * spacing, topY, 0);
            GameObject house = Instantiate(housePrefab, position, Quaternion.identity);
            house.transform.parent = transform;

            Transform wall = house.transform.Find("Wall");
            
            if(wall != null)
            {
                Color color = GetRandomColor();
                var renderers = wall.GetComponentsInChildren<SpriteRenderer>();
                foreach (var renderer in renderers)
                {
                    renderer.color = color;
                }
            }

            Transform roof = house.transform.Find("HouseOverlays");

            if(roof != null)
            {
                Color color = GetRandomColor();
                var renderers = roof.GetComponentsInChildren<SpriteRenderer>();
                foreach (var renderer in renderers)
                {
                    renderer.color = color;
                }
            }

            Transform curtains = house.transform.Find("Curtains");
            if(curtains != null)
            {
                Color color = GetRandomColor();
                var renderers = curtains.GetComponentsInChildren<SpriteRenderer>();
                foreach (var renderer in renderers)
                {
                    renderer.color = color;
                }
            }

        }
        if (barrier == null)
        {
            barrier = new GameObject("Barrier");
            BoxCollider2D col = barrier.AddComponent<BoxCollider2D>();
            col.isTrigger = false;
        }

        barrier.transform.position = new Vector3(0, barrierY, 0);
        barrier.transform.localScale = new Vector3(totalWidth + 5, .5f, 1);
    }

    Color GetRandomColor()
    {
        return new Color(
            Random.Range(.5f, 1f),
            Random.Range(.5f, 1f),
            Random.Range(.5f, 1f)
            );
    }

}
