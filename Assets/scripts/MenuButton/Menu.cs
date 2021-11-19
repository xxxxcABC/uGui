using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void CloseGongGao() {
        GameObject.Find("Canvas/Panel/gonggao").SetActive(false);
    }
    public void ShowGongGao() {
        GameObject.Find("Canvas/Panel/gonggao").SetActive(true);
    }
    public void ShowTask() {
        GameObject.Find("Canvas/Panel/Task").SetActive(true);
    }
    public void CloseTask() {
        GameObject.Find("Canvas/Panel/Task").SetActive(false);
    }

}
