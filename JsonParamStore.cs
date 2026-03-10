using System;
using System.Collections.Generic;
using System.Text.Json;

public sealed class JsonParamStore : IDisposable
{
    private readonly JsonDocument _document;
    private readonly JsonElement _root;

    public JsonParamStore(string json)
    {
        _document = JsonDocument.Parse(json);
        _root = _document.RootElement;
    }

    public void Dispose()
    {
        _document.Dispose();
    }

    public bool TryGet<T>(string path, out T value)
    {
        value = default;

        if (!TryGetElement(path, out var element))
            return false;

        try
        {
            if (typeof(T) == typeof(JsonElement))
            {
                value = (T)(object)element;
                return true;
            }

            value = element.Deserialize<T>();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public T Get<T>(string path, T defaultValue = default)
    {
        if (TryGet<T>(path, out var value))
            return value;

        return defaultValue;
    }

    public JsonElement GetElement(string path)
    {
        if (!TryGetElement(path, out var element))
            throw new KeyNotFoundException($"Json path not found: {path}");

        return element;
    }

    private bool TryGetElement(string path, out JsonElement element)
    {
        element = _root;

        var parts = path.Split('.');

        foreach (var p in parts)
        {
            if (element.ValueKind != JsonValueKind.Object)
                return false;

            if (!element.TryGetProperty(p, out element))
                return false;
        }

        return true;
    }
}