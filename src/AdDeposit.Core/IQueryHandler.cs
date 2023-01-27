﻿namespace AdDeposit.Core
{
    public interface IQuery<TResult>
    { }

    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult?> HandleAsync(TQuery query);
    }
}