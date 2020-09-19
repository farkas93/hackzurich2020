using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public static class API
{
    
    public static int datapointID;
    public static bool showDescription = false;
    public static bool showTimestamp = false;
    public static string preText = "";
    public static string afterText = "";
    public static float updateRate = 2;
    public static bool trimDescription = false;

    static string username;
    static string password;

    static string base_url;
    static string realtime_url;
    static string datapoint_collection_url;
    static string all_datapoint_collection_url;
    static string info_url;
    // time format 2018-11-04T23:00:00.000Z
    static string timeline_url;

    public static Dictionary<int, Datapoint> datapoints;

    public static void Initialize()
    {
        datapoints = new Dictionary<int, Datapoint>();
        username = LoadConfiguration.ApiConfig.GetValue("username").ToString();
        password = LoadConfiguration.ApiConfig.GetValue("password").ToString();
        base_url = LoadConfiguration.ApiConfig.GetValue("base_url").ToString();
        realtime_url = LoadConfiguration.ApiConfig.GetValue("realtime_url").ToString();
        info_url = LoadConfiguration.ApiConfig.GetValue("info_url").ToString();
        datapoint_collection_url = LoadConfiguration.ApiConfig.GetValue("datapoint_collection_url").ToString();
        all_datapoint_collection_url = LoadConfiguration.ApiConfig.GetValue("all_datapoint_collection_url").ToString();
        timeline_url = LoadConfiguration.ApiConfig.GetValue("timeline_url").ToString();
    }

    /// <summary>
    /// Download String with Webclient from given URL
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    static string DownloadString(string url)
    {
        CredentialCache myCache = new CredentialCache
        {
            { new Uri(url), "NTLM", new NetworkCredential(username, password) }
        };

        WebClient WebClient = new WebClient
        {
            Credentials = myCache
        };

        return WebClient.DownloadString(new Uri(url));
    }

    /// <summary>
    /// Gets info of single datapoint
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static dynamic GetDatapointInfo(dynamic id)
    {
        string url = GetUrl(info_url + id);

        string Result = DownloadString(url);
        return JObject.Parse(Result);
    }

    /// <summary>
    /// Gets timeline of single datapoint default dates
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    static string GetTimelineData(dynamic id)
    {
        // TODO: make date range customizable
        string yesterday = DateTime.UtcNow.AddDays(-1).ToString("o"); // get yesterday
        string tomorrow = DateTime.UtcNow.AddDays(+1).ToString("o"); // get tomorrow

        string url = GetUrl(timeline_url.Replace("<startDate>", yesterday).Replace("<endDate>", tomorrow).Replace("<id>", id.ToString()));

        string Result = DownloadString(url);
        return Result;
    }

    /// <summary>
    /// Gets timeline of single datapoint with defined start and enddate
    /// </summary>
    /// <param name="id"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
   public static string GetTimelineData(dynamic id, string startDate, string endDate)
    {

        string url = GetUrl(timeline_url.Replace("<startDate>", startDate).Replace("<endDate>", endDate).Replace("<id>", id.ToString()));

        string Result = DownloadString(url);
        return Result;
    }

    /// <summary>
    /// Creates datapoint object 
    /// Includes datapoint info and timeline
    /// </summary>
    /// <param name="id"></param>
    public static void CreateDatapoint(dynamic id)
    {
        Datapoint dp;
        dynamic attrs = new JObject();
        dp = new Datapoint();
        foreach(string lang in dp.description.Keys.ToList())
        {
            // add language parameter
            attrs = GetDatapointInfo(id+ "?lng=" + lang);
            dp.description[lang] = (string)attrs["description"];
        }
        dp.numericId = (string)attrs["numericId"];
        dp.unit = (string)attrs["unit"];
        dp.data = GetTimelineData(id);
        datapoints[id] = dp;
    }

    /// <summary>
    /// Creates datapoint objects from saved datapoint ids.
    /// </summary>
    static void CreateDatapoints()
    {
        var arrayOfKeys = datapoints.Keys.ToArray();
        for (int i = 0; i < arrayOfKeys.Length; i++)
        {
            var id = arrayOfKeys[i];
            CreateDatapoint(id);
        }
    }

    /// <summary>
    /// Updates datapoints with single value
    /// </summary>
    /// <param name="id"></param>
    public static IEnumerator UpdateDatapoint(dynamic id)
    {
        string url = GetUrl(realtime_url + id);
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null && www.isDone)
        {
            dynamic d = JArray.Parse(www.text)[0];
            Datapoint dp = datapoints[id];
            dp.timestamp = (DateTime)d["timestamp"];
            dp.value = (float)d["value"];
            //UpdateTimeline(dp, www.text);
        }
    }

    /// <summary>
    /// Gets datapoint items from saved Datapoint collection
    /// </summary>
    public static JArray GetDatapointCollectionItems(string id)
    {
        string url = GetUrl(all_datapoint_collection_url+ id);

        string Result = DownloadString(url);
        dynamic d = JObject.Parse(Result);
        return d["items"];

    }

    /// <summary>
    /// Get all available datapoint collections from user
    /// </summary>
    public static dynamic GetDatapointCollections()
    {
        string url = GetUrl(all_datapoint_collection_url);

        string Result = DownloadString(url);
        dynamic d = JArray.Parse(Result);
        return d;
    }

    /// <summary>
    /// Update all datapoints
    /// </summary>
    public static void UpdateDatapoints()
    {
        var arrayOfKeys = datapoints.Keys.ToArray();
        for (int i = 0; i < arrayOfKeys.Length; i++)
        {
            UpdateDatapoint(arrayOfKeys[i]);
        }
    }


    /// <summary>
    /// Builds url with saved base_url
    /// </summary>
    /// <param name="suffix"></param>
    /// <returns></returns>
    static string GetUrl(string suffix)
    {
        return base_url + suffix;
    }


    private static void UpdateTimeline(Datapoint dp, String value)
    {
       // dp.data.Add(value);
        Debug.Log(dp.data.Count());
    }

    private static String TripDescription(String trim, String description)
    {
       string result = string.Empty;
        int i = description.IndexOf(trim);
        if ( i >= 0)
        {
            result = description.Remove(i, trim.Length);
        }
     
        return result.Substring(0,result.IndexOf(' ') + 1);
    }



}
/// <summary>
/// Datapoint class
/// </summary>
public class Datapoint
{
    // single value at timestamp
    public DateTime timestamp;
    public float value;

    public string numericId;
    public Dictionary<string, string> description = new Dictionary<string, string>{
        { "en", "" },
        { "de", "" }
    };
    public string unit;

    // for timeline with range
    public string data;

}
