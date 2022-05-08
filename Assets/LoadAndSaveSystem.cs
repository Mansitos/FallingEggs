using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadAndSaveSystem : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(this.gameObject.tag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    { 
    }

    void Update()
    {
    }

    public void saveUserData(UserData data)
    {
        PlayerPrefs.SetInt("scoreRecord", data.scoreRecord);
        PlayerPrefs.SetInt("coinBalance", data.coinBalance);
    }

    public UserData loadUserData()
    {
        int scoreRecord = PlayerPrefs.GetInt("scoreRecord", 0);
        int coinBalance = PlayerPrefs.GetInt("coinBalance", 0);
        UserData userdata = new UserData(scoreRecord, coinBalance);
        return userdata;
    }
}
