using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Diagnostics.CodeAnalysis;

namespace Mini_Twitter.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public Error? Error { get; set; }
        protected Result(bool isSuccess, Error? error)
        {
            if (isSuccess && error != null)
                throw new ArgumentException("Invalid Argument.", nameof(error));
            if (!isSuccess && error == null)
                throw new ArgumentException("Invalid Argument.", nameof(error));
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, null);
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, null);
        public static Result Failure(Error error) => new Result(false, error);
        public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, error);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;
        public Result(TValue? value, bool isSuccess, Error? error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        //[NotNull]
        //public TValue? Value => IsSuccess
        //    ? _value!
        //    : throw new InvalidOperationException("The value of failure results cannot be accessed.");

        [NotNull]
        public TValue? Value => IsSuccess ? _value! : default!;

        public static implicit operator Result<TValue>(TValue? value)
            => value != null ? Success(value) : Failure<TValue>(Error.Null);
    }

    public sealed class PaginatedResult<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public int TotalPages =>
            (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;

        public PaginatedResult(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
