using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floatingtext : MonoBehaviour
{
    [SerializeField] GameObject parent;

    public void DestroyPlease()
    {
        Destroy(parent);
    }
}
