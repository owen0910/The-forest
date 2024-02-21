using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    public float attack = 0;

    private List<WolfBaby> wolfList = new List<WolfBaby>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="enemy")
        {
            WolfBaby baby = other.GetComponent<WolfBaby>();
            int index = wolfList.IndexOf(baby);
            if (index==-1)
            {
                baby.Hurt((int)attack);
                wolfList.Add(baby);
            }
        }
    }
}
