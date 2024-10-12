using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManagement : MonoBehaviour
{
    public Button GalleryButton;
    public Button CollectionButton;
    public GameObject GalleryPanel;
    public GameObject CollectionPanel;
    void Start()
    {
        GalleryButton.onClick.AddListener(OnGalleryButtonClick);
        CollectionButton.onClick.AddListener(OnCollectionButtonClick);
    }

    void Update()
    {
        
    }
    public void OnGalleryButtonClick(){
        GalleryPanel.SetActive(true);
        CollectionPanel.SetActive(false);
    }
    public void OnCollectionButtonClick(){
        GalleryPanel.SetActive(false);
        CollectionPanel.SetActive(true);
    }
}
