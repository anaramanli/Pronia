using System.Text;

namespace WebApplication1.Extentions
{
	public static class ListExtension
	{
		public static string Bind(this IEnumerable<string>? list, char letter)
		{
			if (list?.Count() == 0) return string.Empty;
			StringBuilder sb = new StringBuilder();
			foreach (string item in list)
			{
				sb.Append(item);
				sb.Append(letter);
			}
			sb.Remove(sb.Length - 1, 1);	
			return sb.ToString();
		}
	}
}
