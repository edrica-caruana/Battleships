using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Storage;
using System;

//2
[Serializable] //So we can convert to JSON
public class GameLobby
{
    public string _player1, _player2;

    //Constructor
    public GameLobby(string player1, string player2)
    {
        this._player1 = player1;
        this._player2 = player2;
    }

    //Dictionary conatins a combination of the key and value
    //Each key is unique - UniqueIdentifier
    //Thus, we can get staff easier when compared to list
    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["player1"] = _player1;
        result["player2"] = _player2;
        result["Dog"] = "Not a human being";

        return result;
    }
}

public class FirebaseController : MonoBehaviour
{
    //1
    private static DatabaseReference dbRef;
    private void Start()
    {
        //Instantiated the Database Reference
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public static IEnumerator CreateInstance()
    {
        //2

        //Create a unique identifier
        //Go into our database and search for the child
        //If it is not there, it will create since we did push
        string key = dbRef.Child("GameLobbies").Push().Key;

        //Instantiating the GameLobby
        GameLobby lobby = new GameLobby("Tom", "Joan");
        //Create a dictionary of type string for the player names
        Dictionary<string, System.Object> data = lobby.ToDictionary();

        //Saving these into firebase
        Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
        childUpdates["/Games/" + key] = data;
        yield return dbRef.UpdateChildrenAsync(childUpdates);

        //Debug.Log(key);
        //yield return null;
    }

    public static IEnumerator CreateInstance2()
    {
        //2

        //Create a unique identifier
        //Go into our database and search for the child
        //If it is not there, it will create since we did push
        string key = dbRef.Child("GameLobbies").Push().Key;

        //Instantiating the GameLobby
        GameLobby lobby = new GameLobby("Edward", "Chrizana");

        yield return dbRef.Child("Games").Child(key).SetRawJsonValueAsync(JsonUtility.ToJson(lobby));
    }

    public static IEnumerator SaveFirebase()
    {
        yield return null;
    }

    public static IEnumerator GetFirebase()
    {
        yield return null;
    }

    public static IEnumerator DownloadAsset()
    {
        yield return null;
    }

    public static IEnumerator UploadAsser()
    {
        yield return null;
    }
}
