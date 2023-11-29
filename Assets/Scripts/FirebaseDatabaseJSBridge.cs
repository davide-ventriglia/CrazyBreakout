using System.Runtime.InteropServices;
public static class FirebaseDatabaseJSBridge
{
        /// <summary>
        /// Gets JSON from a specified path
        /// Will return a snapshot of the JSON in the callback output
        /// </summary>
        /// <param name="path"> Database path </param>
        /// <param name="objectName"> Name of the gameobject to call the callback/fallback of </param>
        /// <param name="callback"> Name of the method to call when the operation was successful. Method must have signature: void Method(string output) </param>
        /// <param name="fallback"> Name of the method to call when the operation was unsuccessful. Method must have signature: void Method(string output). Will return a serialized FirebaseError object </param>
        [DllImport("__Internal")]
        public static extern void GetData(string path, string objectName, string callback, string fallback);

        /// <summary>
        /// Save a record to a specified path
        /// Will return nothing
        /// Take as input the new record to save
        /// </summary>
        /// <param name="path"> Database path </param>
        /// <param name="objectName"> Name of the gameobject to call the callback/fallback of </param>
        /// <param name="fallback"> Name of the method to call when the operation was unsuccessful. Method must have signature: void Method(string output). Will return a serialized FirebaseError object </param>
        [DllImport("__Internal")]
        public static extern void SaveRecord(string path, string value, string objectName, string fallback);
}
