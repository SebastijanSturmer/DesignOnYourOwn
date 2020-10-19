using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class for various utilities.
/// </summary>
public static class Utilities
{
    private static List<string> _GUIDList = new List<string>();

    #region Public functions
    /// <summary>
    /// Prints a log to the console if the logs are locally enabled.
    /// </summary>
    /// <param name="enabled">
    /// If true print the log, don't print otherwise.
    /// </param>
    /// <param name="log">
    /// Output string for the console.
    /// </param>
    public static void DebugLog(bool enabled, string log)
    {
        if (enabled)
        {
            Debug.Log($"<color=green> {log} </color>");
        }
    }


    /// <summary>
    /// Prints a log to the console if the logs are locally enabled.
    /// </summary>
    /// <param name="enabled">
    /// If true print the log, don't print otherwise.
    /// </param>
    /// <param name="log">
    /// Output string for the console.
    /// </param>
    /// /// <param name="args">
    /// Output objects for the console.
    /// </param>
    public static void DebugLogFormat(bool enabled, string log, params object[] args)
    {
        if (enabled)
        {
            Debug.LogFormat($"<color=green> {log} </color>", args);
        }
    }

    /// <summary>
    /// Generates a new GUID and adds it to a list of existing GUIDs locally to prevent 2 same GUID generations.
    /// </summary>
    /// <returns>
    /// Returns the generated GUID string.
    /// </returns>
    public static string GetGUID()
    {
        var id = Guid.NewGuid().ToString();

        if(_GUIDList.Contains(id))
        {
            Debug.Log($"<color=blue> Google says it is more likely that you will get hit by a meteor than this piece of code executing. </color>");
            return GetGUID();
        }

        _GUIDList.Add(id);
        return id;
    }
    #endregion
}
