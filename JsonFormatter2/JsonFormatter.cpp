#include <fstream>
#include <iostream>
#include <nlohmann/json.hpp>

using json = nlohmann::json;

int main(int argc, char* argv[])
{
    if (argc < 3)
    {
        std::cout << "Usage: JsonFormatter input.json output.json\n";
        return 1;
    }

    const char* inputPath = argv[1];
    const char* outputPath = argv[2];

    try
    {
        std::ifstream in(inputPath);

        if (!in.is_open())
        {
            std::cout << "Cannot open input\n";
            return 2;
        }

        json j;
        in >> j;

        std::ofstream out(outputPath, std::ios::binary);

        out << j.dump(4) << std::endl;

        std::cout << "Formatted\n";
    }
    catch (std::exception& e)
    {
        std::cout << "Error: " << e.what() << std::endl;
        return 3;
    }

    return 0;
}