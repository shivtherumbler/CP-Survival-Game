using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject Prefab;
    public int Count;
    private GameObject[] BulletList;
    public static BulletPool Reference
    {
        get;
        set;
    }
    void Awake()
    {
        Reference = this;
        BulletList = new GameObject[Count];
        for (int i = 0; i < Count; i++)
        {
            BulletList[i] = Instantiate(Prefab, transform);
            BulletList[i].gameObject.SetActive(false);
        }
        //BulletList = transform.GetComponentsInChildren<Bullet>(true);
    }
    public GameObject GetBulletFromPool()
    {
        foreach (GameObject g in BulletList)
        {
            if (!g.transform.gameObject.activeInHierarchy)
                return g;
        }
        return null;
    }
}
