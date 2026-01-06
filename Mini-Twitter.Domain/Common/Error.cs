using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Twitter.Domain.Common
{
    public sealed class Error
    {
        public string Code { get; set; }
        public string? Message { get; set; }

        private Error(string code, string? message = null)
        {
            Code = code;
            Message = message;
        }

        public static Error NotFound(string message) => new Error("NotFound", message);
        public static Error Validation(string message) => new Error("Validation", message);
        public static Error Unauthorized(string message) => new Error("Unauthorized", message);
        public static Error Conflict(string message) => new Error("Conflict", message);
        public static Error Unexpected(string message) => new Error("Unexpected", message);
        public static Error None => new Error(string.Empty);
        public static Error Null => new Error("NullValue", "Value is Null.");
    }
}
