using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseListView : MonoBehaviour {

    public static FirebaseListView Instance;
    private DatabaseReference dbReference;

    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private GameObject content;

    private List<Profile> currentProfile;

    void Awake()
    {
        currentProfile = new List<Profile>();
        InitialState();
    }

    public void InitContent()
    {
        for (var i = 0; i <= currentProfile.Count; i++)
        {
            if (currentProfile[i] != null)
            {
                content.GetComponent<ContentView>().mProfile = currentProfile[i];
                Instantiate(content, parent.transform);
            }
        }
    }

    public void ResetContent()
    {
        if (parent.transform.childCount > 0)
        {
            for (var i = 0; i <= parent.transform.childCount; i++)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }
        }
    }

    public void RefreshButton()
    {
        if (parent.transform.childCount < 1)
        {
            InitialState();
        }
        else
        {
            return;
        }
    }

    public void InitialState()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tesint-tech-everidea.firebaseio.com/");
        dbReference = FirebaseDatabase.DefaultInstance.GetReference("list_view");
        dbReference.ValueChanged += OnDataUpdateStok;
    }

    private void OnDataUpdateStok(object sender, ValueChangedEventArgs e)
    {
        ResetContent();
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        if (e.Snapshot == null || e.Snapshot.Value == null)
        {
            Debug.Log("KOSONG");
        }
        else
        {            currentProfile.Clear();
            foreach (DataSnapshot barang in e.Snapshot.Children)
            {
                IDictionary dic = (IDictionary)barang.Value;

                var image_source = (string) dic["image_source"];
                var title = (string) dic["title"];
                var description = (string) dic["description"];

                Profile profile = new Profile(image_source,title,description);

                currentProfile.Add(profile);
            }
            InitContent();
        }
    }
}
