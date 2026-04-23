using System.Collections.Generic;
using UnityEngine;

/*
 *
 * Meant to hold configuration files (AKA Scriptable objects).
 * 
 * 
 */

public class ConfigManager : Singleton<ConfigManager>
{
    [Header("Character Database")]
    [SerializeField] public DatabaseConfig databaseConfig;
    
    /*
     * dance session config
     * wallet config
    */
    
}