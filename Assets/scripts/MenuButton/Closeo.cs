using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closeo : MonoBehaviour
{
    public void CloseGongGao() {
        GameObject.Find("Canvas/Panel/gonggao").SetActive(false);
    }

}
