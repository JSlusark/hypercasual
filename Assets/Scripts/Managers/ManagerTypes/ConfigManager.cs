using System.Collections.Generic;
using UnityEngine;

/*
 *
 * Meant to hold configuration files (AKA Scriptable objects).
 * 
 * 
 */

public class ConfigManager : Manager<ConfigManager>
{
    [Header("Character CharacterCatalogue")]
    [SerializeField] public DatabaseConfig databaseConfig;
    
    /*
     * dance session config
     * wallet config
    */
    
}