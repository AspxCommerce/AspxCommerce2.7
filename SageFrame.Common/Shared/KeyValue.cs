#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

/// <summary>
/// This class holds the properties of key value for Application.
/// </summary>
public class KeyValue
{
    private string _key;
    private string _value;
    /// <summary>
    /// Initializes a new instance of the KeyValue class.
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="value">value</param>
    public KeyValue(string key, string value)
    {
        this._key = key;
        this._value = value;
    }
    /// <summary>
    /// Get or set key.
    /// </summary>
    public string Key { get { return _key; } set { _key = value; } }
    /// <summary>
    /// Get or set value.
    /// </summary>
    public string Value { get { return _value; } set { _value = value; } }
    /// <summary>
    /// Initializes a new instance of the KeyValue class.
    /// </summary>
    public KeyValue()
    {
    }
}