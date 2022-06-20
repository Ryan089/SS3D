using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SS3D.Core.Networking.Lobby.UI_Helper;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;
using System;

public class LobbyInitializationTests
{
    private bool SHOW_DEBUG_LOG = false;

    [SetUp]
    public void SetUp()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Lobby.unity");
    }

    [Test]
    public void CorrectlyInitializedLobbyTabsUIHelper()
    {
        string[] fields = { "_categoryUi" };
        CheckComponentVariablesAreNotNull<LobbyTabsUIHelper>(fields);
    }

    [Test]
    public void CorrectlyInitializedPlayerUsernameUIHelper()
    {
        string[] fields = { "_nameLabel"};
        CheckComponentVariablesAreNotNull<PlayerUsernameUIHelper>(fields);
    }

    [Test]
    public void CorrectlyInitializedGenericTabUIHelper()
    {
        string[] fields = { "_panelUI", "_tabButton" };
        CheckComponentVariablesAreNotNull<GenericTabUIHelper>(fields);
    }

    private void CheckComponentVariablesAreNotNull<T>(string[] fieldsToCheck) where T : MonoBehaviour
    {
        FieldInfo field;
        BindingFlags flags = GetBindingFlags();

        // Check that the script actually contains the fields you are looking for
        foreach (string s in fieldsToCheck)
        {
            field = typeof(T).GetField(s, flags);
            Assert.IsNotNull(field, "{0} does not appear to have a field for {1}.", typeof(T).ToString(), s);
        }

        // Check that all the components have the required fields set
        T[] components = UnityEngine.Object.FindObjectsOfType<T>(true);
        foreach (T component in components)
        {
            foreach (string s in fieldsToCheck)
            {
                field = typeof(T).GetField(s, flags);
                if (SHOW_DEBUG_LOG) Debug.Log(component.name + " checked for " + s);
                Assert.IsNotNull(field.GetValue(component), "{0} did not have {1} set.", component.name, s);
            }
        }
    }

    private BindingFlags GetBindingFlags()
    {
        BindingFlags flags = BindingFlags.Public |
                             BindingFlags.Instance |
                             BindingFlags.NonPublic;
        return flags;
    }


}
