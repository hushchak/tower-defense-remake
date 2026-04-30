using System;
using System.IO;
using UnityEngine;

public class FileStorage<T> where T : class
{
    private readonly string filePath;
    private readonly Func<T> createDefault;

    public FileStorage(string filePath, Func<T> createDefault)
    {
        this.filePath = filePath;
        this.createDefault = createDefault;
    }

    public T Read()
    {
        try
        {
            EnsureDirectory();
            if (!File.Exists(filePath))
                Write(createDefault());

            return JsonUtility.FromJson<T>(File.ReadAllText(filePath)) ?? Reset();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return null;
        }
    }

    public void Write(T data)
    {
        try
        {
            EnsureDirectory();
            File.WriteAllText(filePath, JsonUtility.ToJson(data, true));
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public T Reset()
    {
        var data = createDefault();
        Write(data);
        return data;
    }

    private void EnsureDirectory() =>
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
}
