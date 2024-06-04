using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        GameObject musicObject =  GameObject.Find(this.gameObject.name);
        if (musicObject != null && musicObject != this.gameObject)
            Destroy(this.gameObject);
    }
}
