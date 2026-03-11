#pragma once

#ifdef JSONFORMATTER_EXPORTS
#define JSON_API __declspec(dllexport)
#else
#define JSON_API __declspec(dllimport)
#endif

extern "C"
{
    JSON_API int FormatJsonFile(const char* inputPath, const char* outputPath);
}