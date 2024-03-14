using MetricsAPI.Domain.Common.Core.Primitives;

namespace MetricsAPI.Application.Core.Errors;

/// <summary>
/// Contains the validation errors.
/// </summary>
public static class ValidationErrors
{
    /// <summary>
    /// Contains the login errors.
    /// </summary>
    internal static class Login
    {
        internal static Error EmailIsRequired => new Error("Login.EmailIsRequired", "The emailAddress is required.");

        internal static Error PasswordIsRequired => new Error("Login.PasswordIsRequired", "The password is required.");
    }
    
    /// <summary>
    /// Contains the create message errors.
    /// </summary>
    public static class CreateMessage
    {
        public static Error DescriptionIsRequired => 
            new("CreateMessage.DescriptionIsRequired", "The description is required.");

        public static Error RecipientIdIsRequired => 
            new("CreateMessage.RecipientIdIsRequired", "The recipient identifier is required.");
    }
    
    /// <summary>
    /// Contains the create message errors.
    /// </summary>
    public static class CreateCounter
    {
        public static Error DescriptionIsRequired => 
            new("CreateCounter.DescriptionIsRequired", "The description is required.");

        public static Error NameIsRequired => 
            new("CreateCounter.NameIsRequired", "The name is required.");
    }
}