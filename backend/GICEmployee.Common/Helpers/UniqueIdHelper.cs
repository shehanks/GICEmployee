namespace GICEmployee.Common.Helpers
{
    public static class UniqueIdHelper
    {
        private static readonly Random Random = new();

        public static string GenerateEmployeeId()
        {
            const string prefix = "UI";
            const string alphanumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int idLength = 7;
            var idBody = new char[idLength];

            for (var i = 0; i < idLength; i++)
            {
                idBody[i] = alphanumericChars[Random.Next(alphanumericChars.Length)];
            }

            return prefix + new string(idBody);
        }
    }
}
