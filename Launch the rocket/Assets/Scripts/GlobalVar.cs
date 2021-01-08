using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
	private static string ip;

	public static string IP
    {
        get
        {
            return ip;
        }
        set
        {
            ip = value;
        }
    }
    
}
