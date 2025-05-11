using System.Security.Cryptography;

namespace SurfTicket.Infrastructure.Helpers
{
    public static class GenCodeHelper
    {
        private static readonly char[] _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public static string GenerateCode(int length = 6)
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[length];
            rng.GetBytes(bytes);

            var code = new char[length];
            for (int i = 0; i < length; i++)
            {
                code[i] = _chars[bytes[i] % _chars.Length];
            }

            return new string(code);
        }
    }
}
