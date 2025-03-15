using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Inventory.Model;
using UnityEngine;

public class FileDataHandler 
{

private string dataDirectoryPath = "";

private string dataFileName= "";

private bool useEncryption = false;
private readonly string encryptionCodeWord = "mystical";

public FileDataHandler(string dataDirectoryPath, string dataFileName, bool useEncryption) {

this.dataFileName= dataFileName;
this.dataDirectoryPath= dataDirectoryPath;
this.useEncryption = useEncryption;


}


public SaveData Load(){

    string fullPath = Path.Combine(dataDirectoryPath, dataFileName);
    SaveData loadedData = null;
    if(File.Exists(fullPath)){
    try{
        //load the serialized data from file
        string dataToLoad = "";
        using(FileStream stream = new FileStream(fullPath, FileMode.Open)){
        using (StreamReader reader = new StreamReader(stream)){
        dataToLoad = reader.ReadToEnd();
        }
}

//decrypt data
if(useEncryption){
    dataToLoad = EncryptDecrypt(dataToLoad);
}

//deserialize data from json back to C# object 

loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);


}

catch(Exception e){
    Debug.LogError("Error when trying to load the file : " + fullPath +  "\n" + e);
    
}

    }
    return loadedData;
}


public void Save(SaveData data){
    string fullPath = Path.Combine(dataDirectoryPath, dataFileName);

try{

//create the directory where the file will be stored when there isn't one
Directory.CreateDirectory(Path.GetDirectoryName(fullPath));


//serialization of c# save data to json 
string dataToStore = JsonUtility.ToJson(data,true);

//encrypt data
if(useEncryption){
    dataToStore = EncryptDecrypt(dataToStore);
}

//write the serialized data to file
using(FileStream stream = new FileStream(fullPath, FileMode.Create)){
    using (StreamWriter writer = new StreamWriter(stream)){
        writer.Write(dataToStore);
    }
}

}
catch(Exception e){
    Debug.LogError("Error when trying to save the file : " + fullPath +  "\n" + e);
    
}

}

private string EncryptDecrypt(string data){

string modifiedData = "";
for ( int i =0; i<data.Length; i++){
    modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
}

return modifiedData;


}


}
