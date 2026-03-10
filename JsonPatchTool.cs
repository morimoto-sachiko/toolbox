using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

public static class JsonPatchTool
{
    public static void ApplyCsvPatch(
        string jsonPath,
        string csvPath,
        string rootNodeName)
    {
        // JSONロード
        JsonNode root = JsonNode.Parse(File.ReadAllText(jsonPath))!;

        JsonObject table = root[rootNodeName]!.AsObject();

        // CSVロード
        var lines = File.ReadAllLines(csvPath);

        var headers = lines[0].Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');

            string id = values[0];

            if (!table.ContainsKey(id))
                continue;

            JsonObject row = table[id]!.AsObject();

            for (int c = 1; c < headers.Length; c++)
            {
                string key = headers[c];
                string value = values[c];

                row[key] = ParseValue(value);
            }
        }

        SaveJson(jsonPath, root);
    }

    static JsonNode ParseValue(string value)
    {
        if (int.TryParse(value, out var i))
            return i;

        if (float.TryParse(value, out var f))
            return f;

        if (bool.TryParse(value, out var b))
            return b;

        return value;
    }

    static void SaveJson(string path, JsonNode node)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = node.ToJsonString(options);

        File.WriteAllText(path, json);
    }
}