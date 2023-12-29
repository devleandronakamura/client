namespace Client.Util
{
    public static class Functions
    {
        public static string MapOnlyNumbers(string input)
            => new string(input.Where(char.IsDigit).ToArray());
    }
}