using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private GameObject page;
    [SerializeField] private GameObject sections;

    public void OpenPage()
    {
        page.SetActive(true);
        sections.SetActive(false);
    }
}
