using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextController : MonoBehaviour
{
    [SerializeField] private EventChannelWaveData waveAnnounceChannel;
    [SerializeField] private GameObject nextImagePrefab;
    [SerializeField] private Transform poolParent;

    private ObjectPool nextImagePool;

    private void Awake()
    {
        nextImagePool = new ObjectPool(nextImagePrefab, poolParent, 0);
    }

    private void OnEnable()
    {
        waveAnnounceChannel.Subscribe(HandleAnnounce);
    }

    private void OnDisable()
    {
        waveAnnounceChannel.Unsubscribe(HandleAnnounce);
    }

    private void HandleAnnounce(WaveData data)
    {
        List<GameObject> existingImages = nextImagePool.GetAllExistingObjects();
        foreach (GameObject nextImage in existingImages)
            nextImage.SetActive(false);

        List<EnemyWaveData> enemyData = new();

        for (int i = 0; i < data.Actions_1.Length; i++)
            if (!enemyData.Contains(data.Actions_1[i].Enemy))
                enemyData.Add(data.Actions_1[i].Enemy);

        for (int i = 0; i < data.Actions_2.Length; i++)
            if (!enemyData.Contains(data.Actions_2[i].Enemy))
                enemyData.Add(data.Actions_2[i].Enemy);

        for (int i = 0; i < data.Actions_3.Length; i++)
            if (!enemyData.Contains(data.Actions_3[i].Enemy))
                enemyData.Add(data.Actions_3[i].Enemy);

        for (int i = 0; i < enemyData.Count; i++)
        {
            GameObject imageObject = nextImagePool.GetObject();
            imageObject.GetComponent<Image>().sprite = enemyData[i].Sprite;
            imageObject.transform.SetSiblingIndex(i);
            imageObject.SetActive(true);
        }
    }
}
