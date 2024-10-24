using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Contracts.v1;
public class ErrorResponse
{
    public List<string> Errors { get; set; } = new();

    public static ErrorResponse FromResult(ResultBase result) =>
        new() { Errors = ExtractErrorMessages(result.Errors) };

    private static List<string> ExtractErrorMessages(IEnumerable<IError> errors)
    {
        List<string> messages = new();

        foreach (var error in errors)
        {
            if (!string.IsNullOrWhiteSpace(error.Message))
            {
                messages.Add(error.Message);
            }

            if (error.Reasons != null && error.Reasons.Count != 0)
            {
                messages.AddRange(ExtractErrorMessages(error.Reasons));
            }
        }

        return messages;
    }
}