using System;
using System.Linq.Expressions;

namespace DanaZhangCms.Helper
{
    public static class LamdaHelper
    {
        /// <summary>
        /// 以编程的方式创建Lamda表达式树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propName">模型对应的单个属性名</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression<Func<T, bool>> makeLamda<T>(string propName, object propValue)
        {
            var ori_model = Expression.Parameter(typeof(T), "model");
            var modelProp = Expression.Property(ori_model, propName);
            Type ratioType = propValue.GetType();
            var ratioValue = Expression.Constant(propValue, ratioType);
            var relationShip = Expression.Equal(modelProp, ratioValue);
            return Expression.Lambda<Func<T, bool>>(relationShip, ori_model);
        }
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }
        /// <summary>
        /// 以And方式将表达式连接到原表达式的尾部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp_left"></param>
        /// <param name="exp_right"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);
            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.And(left, right);
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
        /// <summary>
        /// 以Or方式将表达式连接到原表达式的尾部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp_left"></param>
        /// <param name="exp_right"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);
            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.Or(left, right);
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }

    /// <summary>
    /// 统一ParameterExpression
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }
        public ParameterExpression ParameterExpression { get; private set; }
        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }
        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }
}
