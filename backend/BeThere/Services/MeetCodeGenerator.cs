using System.Security.Cryptography;

namespace MeetCG.Services;
public class MeetCodeGenerator
{
    private const string Characters =
        "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    public string Generate()
    {
        char[] code = new char[4];

        for (int i = 0; i < code.Length; i++)
        {
            int index = RandomNumberGenerator.GetInt32(Characters.Length);
            code[i] = Characters[index];
        }

        return new string(code);
    }
}