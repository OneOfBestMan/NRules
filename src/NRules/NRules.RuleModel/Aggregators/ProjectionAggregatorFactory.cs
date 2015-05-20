using System;
using System.Linq.Expressions;

namespace NRules.RuleModel.Aggregators
{
    /// <summary>
    /// Aggregator factory for projection aggregator.
    /// </summary>
    /// <typeparam name="TSource">Type of source element.</typeparam>
    /// <typeparam name="TResult">Type of result element.</typeparam>
    internal class ProjectionAggregatorFactory<TSource, TResult> : IAggregatorFactory, IEquatable<ProjectionAggregatorFactory<TSource, TResult>>
    {
        private readonly Expression<Func<TSource, TResult>> _selectorExpression;
        private readonly Func<TSource, TResult> _selector;

        public ProjectionAggregatorFactory(Expression<Func<TSource, TResult>> selectorExpression)
        {
            _selectorExpression = selectorExpression;
            _selector = selectorExpression.Compile();
        }

        public IAggregator Create()
        {
            return new ProjectionAggregator<TSource, TResult>(_selector);
        }

        public bool Equals(ProjectionAggregatorFactory<TSource, TResult> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _selectorExpression.Equals(other._selectorExpression);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProjectionAggregatorFactory<TSource, TResult>)obj);
        }

        public override int GetHashCode()
        {
            return _selectorExpression.GetHashCode();
        }
    }
}