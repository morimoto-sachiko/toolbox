#include "JsonFormatter.h"

#include <fstream>
#include <string>
#include <nlohmann/json.hpp>

using json = nlohmann::json;

int FormatJsonFile(const char* inputPath, const char* outputPath)
{
    try
    {
        std::ifstream in(inputPath);

        if (!in.is_open())
            return -1;

        json j;
        in >> j;

        std::ofstream out(outputPath, std::ios::binary);

        if (!out.is_open())
            return -2;

        out << j.dump(4) << std::endl;

        return 0;
    }
    catch (...)
    {
        return -3;
    }
}