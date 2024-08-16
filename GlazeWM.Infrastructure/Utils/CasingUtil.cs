using System.Globalization;
using System.Text.RegularExpressions;

namespace GlazeWM.Infrastructure.Utils;

public static partial class CasingUtil
{
  private static readonly Regex _regex = GetRegex();

  /// <summary>
  /// Converts pascal case to snake case (eg. `FirstName` -> `first_name`).
  /// </summary>
  public static string PascalToSnake(string input)
  {
    return _regex.Replace(input, "$1_$2").ToLower(
      CultureInfo.InvariantCulture
    );
  }

  [GeneratedRegex("([a-z])([A-Z])")]
  private static partial Regex GetRegex();
}
