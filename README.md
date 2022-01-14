# SLIDDES-Unity-Networking
A collection for networking in Unity.
SLIDDES © 2022

## About
Hello and thank you for using SLIDDES Software.
SLIDDES Unity Networking is a collection for networking in Unity. It currently contains:
- WebRequest, for fetching json data

## Installation
You can install it as a package for Unity.

For more information on how to install it:
https://docs.unity3d.com/Manual/upm-ui-giturl.html

## Example
using SLIDDES.Networking.Web

string result = "";
yield return StartCoroutine(WebRequest.GetJson("url", x => result = x));

## Other
For more information or contact, go to https://sliddes.com/