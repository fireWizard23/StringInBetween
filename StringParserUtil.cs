namespace StringInBetween;

public static class Utils
{
    public static string Parse(
        string text, 
        string startSymbol, 
        string endSymbol, 
        Func<string, string> callback)
    {
        for (int start_i = 0; start_i < text.Length; start_i++)
        {
            char currentChar = text[start_i];
            if (currentChar != startSymbol[0])
            {
                continue;
            }
            // Check the rest of the char if matching
            bool valid = true;
            for (int a = 1; a < startSymbol.Length; a++)
            {
                char startSChar = startSymbol[a];
                if (startSChar != text[start_i + a])
                {
                    valid = false;
                }
            }
            if (!valid) { continue; }
            int startSymbolIndex = start_i + (startSymbol.Length > 1 ? startSymbol.Length : 0);

            // Look for the end symbol
            for (int end_i = startSymbolIndex; end_i < text.Length; end_i++)
            {
                char currentEndChar = text[end_i];
                if (currentEndChar != endSymbol[0])
                {
                    continue;
                }
                bool endValid = true;
                for (int a = 1; a < startSymbol.Length; a++)
                {
                    char endSChar = endSymbol[a];
                    if (endSChar != text[end_i + a])
                    {
                        endValid = false;
                    }
                }
                if (!endValid) { continue; }

                int endSymbolIndex = end_i;
                var range = startSymbolIndex..endSymbolIndex;
                string textBetweenSymbols = text[range];
                var newTextBetween = callback(textBetweenSymbols);
                var leftHalf = text[0..(startSymbolIndex - startSymbol.Length)];
                var rightHalf = text[(endSymbolIndex + endSymbol.Length)..text.Length];
                text = string.Concat(leftHalf, newTextBetween, rightHalf);
                break;
            }
            // throw new Exception("No terminating symbol found.");
        }
        return text;
    }
}
