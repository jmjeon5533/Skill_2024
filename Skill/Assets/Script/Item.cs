using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    Vector3 curPos;
    Transform mesh;
    bool isUse;
    public enum ItemType
    {
        gold1,
        gold2,
        gold3,
        shortSpeed,
        longSpeed
    }
    private void Start()
    {
        curPos = transform.position;
        mesh = transform.GetChild(0);
    }
    void Update()
    {
        var sin = Mathf.Sin(Time.time);
        mesh.position = curPos + new Vector3(0, sin + 0.5f, 0);
        mesh.Rotate(new Vector3(1, 1, 1) * rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isUse) return;
        if (other.CompareTag("Player"))
        {
            isUse = true;
            mesh.gameObject.SetActive(false);
            StartCoroutine(ItemUse(Random.Range(0, 6)));
        }
    }
    IEnumerator ItemUse(int index)
    {
        print(index);
        switch ((ItemType)index)
        {
            case ItemType.gold1:
                SceneManager.Instance.money += 1;
                break;
            case ItemType.gold2:
                SceneManager.Instance.money += 5;
                break;
            case ItemType.gold3:
                SceneManager.Instance.money += 10;
                break;
            case ItemType.shortSpeed:
                {
                    GameManager.Instance.player.Booster(20);
                }
                break;
            case ItemType.longSpeed:
                {
                    GameManager.Instance.player.Booster(40);
                }
                break;
        }
        yield return new WaitForSeconds(3);
        isUse = false;
        mesh.gameObject.SetActive(true);
    }
}
