using UnityEngine;

public class MusicalColumn : MonoBehaviour
{
    private bool cloned = false;
    private GameObject clone;
    private Vector3 spawn;

    void Awake()
    {
        spawn = transform.position;
        transform.RandomizeSize();
        clone = gameObject;
        cloned = false;

        spawn.x = Random.Range(-5.68f, 5.67f);

        if (spawn.y == 0)
            spawn.y = -5.659f;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (Random.Range(0, 9999) == 1)
        {
            Debug.Log(gameObject.name + " is now special!");
            renderer.sprite = GameEvents.Instance.specialSprite;
        }

        renderer.sortingOrder = -1;
        renderer.flipX = Random.Range(0, 2) == 0f;
    }

    void Update()
    {
        if (transform.position.y >= 4.357 && !cloned)
        {
            CreateNewInstance();
        }

        if (transform.position.y >= 6.718 && cloned)
            Destroy(gameObject);
        else
            transform.Translate(GameEvents.Instance.musicalSpeed * Time.deltaTime * Vector3.up, Space.World);
    }

    public void CreateNewInstance()
    {
        cloned = true;

        var obj = Instantiate(clone, spawn, Quaternion.identity);
        obj.name = gameObject.name;
        obj.transform.position = spawn;
    }
}
