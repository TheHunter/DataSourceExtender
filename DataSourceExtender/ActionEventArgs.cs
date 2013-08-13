using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataSourceExtender
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TParam"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class ActionEventArgs<TParam, TResult>
        : CancelEventArgs
        where TParam : class 
        
    {
        private readonly TParam parameter;
        private readonly Func<TParam, TResult> function;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="function"></param>
        public ActionEventArgs(TParam parameter, Func<TParam, TResult> function)
        {
            this.parameter = parameter;
            this.function = function;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TResult Execute()
        {
            return this.function.Invoke(this.parameter);
        }
    }
}
