using System;

namespace Fixtures.Maybe
{
    public static class Maybe
    {
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class where TInput : class
        {
            return o == null ? null : evaluator(o);
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult defaultValue)
            where TResult : class where TInput : class
        {
            return o == null ? defaultValue : evaluator(o);
        }
    }
}