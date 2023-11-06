using System;
using NongTrai;

public class FirebaseInit : Singleton<FirebaseInit>
{
    // Start is called before the first frame update
    void Start()
    {
        LogFirebase(() => { Firebase.Analytics.FirebaseAnalytics.LogEvent("Enter Game", $"LoginGame", 1); });
    }

    public void LogFirebase(Action action)
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                var app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                action.Invoke();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
}