using Moq.Language;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebPruebasUnitarias
{
    public static class Extensiones
    {
        #region Extensiones para Moq
        public delegate void OutAction<TOut>(out TOut outVal);
        public delegate void OutAction<TOut, in T1>(out TOut outVal, T1 arg1);
        public delegate void OutAction<TOut, in T1, in T2>(out TOut outVal, T1 arg1, T2 arg2);
        public delegate void OutAction<TOut, in T1, in T2, in T3>(out TOut outVal, T1 arg1, T2 arg2, T3 arg3);
        public delegate void OutAction<TOut, in T1, in T2, in T3, in T4>(out TOut outVal, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        public static IReturnsThrows<TMock, TReturn> OutCallback<TMock, TReturn, TOut>(this ICallback<TMock, TReturn> mock, OutAction<TOut> action)
            where TMock : class
        {
            return OutCallbackInternal(mock, action);
        }

        public static IReturnsThrows<TMock, TReturn> OutCallback<TMock, TReturn, TOut, T1>(this ICallback<TMock, TReturn> mock, OutAction<TOut, T1> action)
            where TMock : class
        {
            return OutCallbackInternal(mock, action);
        }

        public static IReturnsThrows<TMock, TReturn> OutCallback<TMock, TReturn, TOut, T1, T2>(this ICallback<TMock, TReturn> mock, OutAction<TOut, T1, T2> action)
            where TMock : class
        {
            return OutCallbackInternal(mock, action);
        }

        public static IReturnsThrows<TMock, TReturn> OutCallback<TMock, TReturn, TOut, T1, T2, T3>(this ICallback<TMock, TReturn> mock, OutAction<TOut, T1, T2, T3> action)
            where TMock : class
        {
            return OutCallbackInternal(mock, action);
        }

        public static IReturnsThrows<TMock, TReturn> OutCallback<TMock, TReturn, TOut, T1, T2, T3, T4>(this ICallback<TMock, TReturn> mock, OutAction<TOut, T1, T2, T3, T4> action)
            where TMock : class
        {
            return OutCallbackInternal(mock, action);
        }

        private static IReturnsThrows<TMock, TReturn> OutCallbackInternal<TMock, TReturn>(ICallback<TMock, TReturn> mock, object action)
            where TMock : class
        {
            mock.GetType()
                .Assembly.GetType("Moq.MethodCall")
                .InvokeMember("SetCallbackWithArguments", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, mock,
                    new[] { action });
            return mock as IReturnsThrows<TMock, TReturn>;
        }
        #endregion
    }
}
