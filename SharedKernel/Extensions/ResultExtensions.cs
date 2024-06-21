using FluentResults;

using System.Text;

namespace SharedKernel.Extensions;

/// <summary>Contains extension methods for the Result class</summary>
public static class ResultExtensions
{
    /// <summary>Aggregates result errors into a single error message</summary>
    /// <param name="result">The result object</param>
    /// <returns>A single error message string</returns>
    public static string GetErrorMessage(this Result result)
    {
        if (result.IsSuccess)
        {
            return string.Empty;
        }

        StringBuilder errorMessageBuilder = new();

        foreach (IError? error in result.Errors)
        {
            errorMessageBuilder.AppendLine(error.Message);

            if (error.Reasons.Count > 0)
            {
                foreach (IError? reason in error.Reasons)
                {
                    errorMessageBuilder.AppendLine($"    {reason.Message}");
                }
            }
        }

        return errorMessageBuilder.ToString().Trim();
    }
}
