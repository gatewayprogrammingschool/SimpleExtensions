/*
 * Copyright 2009, Payton Byrd
 * Copyright 2017, Gateway Programming School
 * Licensed Under the Microsoft Public License (MS-PL)
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace GPS.SimpleExtensions
{
    /// <summary>
    /// A collection of Extension Methods
    /// relating to validation of values.
    /// </summary>
    public static class ValidationExtensions
    {
        #region Generic
        /// <summary>
        /// Throws an <see cref="System.ArgumentNullException"/> 
        /// if the the value is null.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        [DebuggerNonUserCode]
        public static void AssertParameterNotNull<TValue>(
            this TValue value, string message, string name)
            where TValue : class
        {
            if (value == null)
                throw new ArgumentNullException(name, message);
        }

        /// <summary>
        /// Throws an <see cref="System.ArgumentNullException"/> 
        /// if the the value is default.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="message">The message to display if the value is default.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        [DebuggerNonUserCode]
        public static void AssertParameterNotDefault<TValue>(
            this TValue value, string message, string name)
        {
            if (value.Equals(default(TValue)))
                throw new ArgumentNullException(name, message);
        }

        /// <summary>
        /// Throws an <see cref="System.ArgumentException"/>
        /// if the string value is empty.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        [DebuggerNonUserCode]
        public static void AssertParameterNotEmpty(
            this string value, string message, string name)
        {
            value?.ToCharArray().AssertNotNullOrEmpty<ArgumentException>(message, name);
        }

        /// <summary>
        /// Throws an <see cref="System.ArgumentException"/>
        /// if the string value is empty.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        [DebuggerNonUserCode]
        public static void AssertParameterNotEmpty(
            this ICollection value, string message, string name)
        {
            value.AssertNotNullOrEmpty<ArgumentException>(message, name);
        }

        /// <summary>
        /// Throws the specified exception if the
        /// values are not equal.
        /// </summary>
        /// <typeparam name="TExceptionType">
        /// The type of exception to throw.</typeparam>
        /// <param name="value">The value to test.</param>
        /// <param name="compareTo">The expected value.</param>
        /// <param name="message">The message to display if the value is null.</param>
        [DebuggerNonUserCode]
        public static void AssertEquals<TExceptionType>(
            this object value, object compareTo, string message = null)
            where TExceptionType : Exception
        {
            if (!value.Equals(compareTo))
            {
                if (message.IsNullOrEmpty())
                {
                    message = $"{value} does not equal expected value of {compareTo}";
                }

                var e =
                    typeof(TExceptionType).CreateInstance<TExceptionType>(
                        new object[] { message });

                if (e == null)
                {
                    throw new Exception(
                        "Cannot instantiate the expected exception.");
                }

                throw e;
            }
        }

        /// <summary>
        /// Throws the specified exception if the
        /// values are equal.
        /// </summary>
        /// <typeparam name="TExceptionType">
        /// The type of exception to throw.</typeparam>
        /// <param name="value">The value to test.</param>
        /// <param name="compareTo">The expected value.</param>
        /// <param name="message">The message to display if the value is null.</param>
        [DebuggerNonUserCode]
        public static void AssertNotEquals<TExceptionType>(
            this object value, object compareTo, string message = null)
            where TExceptionType : Exception
        {
            if (value.Equals(compareTo))
            {
                if (message.IsNullOrEmpty())
                {
                    message = $"{value} does not equal expected value of {compareTo}";
                }

                var e =
                    typeof(TExceptionType).CreateInstance<TExceptionType>(
                        new object[] { message });

                if (e == null)
                {
                    throw new Exception(
                        "Cannot instantiate the expected exception.");
                }

                throw e;
            }
        }

        /// <summary>
        /// Throws the specified exception if the
        /// value is null.
        /// </summary>
        /// <typeparam name="TExceptionType">
        /// The type of exception to throw.</typeparam>
        /// <param name="value">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        [DebuggerNonUserCode]
        public static void AssertNotNull<TNullable, TExceptionType>(
            this TNullable value, string message = null)
            where TNullable : class
            where TExceptionType : Exception
        {
            if (value == null)
            {
                if (message.IsNullOrEmpty())
                {
                    message = $"Value is null.";
                }

                var e =
                    typeof(TExceptionType).CreateInstance<TExceptionType>(
                        new object[] { message });

                if (e == null)
                {
                    throw new Exception(
                        "Cannot instantiate the expected exception.");
                }

                throw e;
            }
        }

        /// <summary>
        /// Throws the specified exception if the
        /// value is default.
        /// </summary>
        /// <typeparam name="TExceptionType">
        /// The type of exception to throw.</typeparam>
        /// <param name="value">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        [DebuggerNonUserCode]
        public static void AssertNotDefault<TValue, TExceptionType>(
            this TValue value, string message = null)
            where TExceptionType : Exception
        {
            if (value.Equals(default(TValue)))
            {
                if (message.IsNullOrEmpty())
                {
                    message = $"Value is default.";
                }

                var e =
                    typeof(TExceptionType).CreateInstance<TExceptionType>(
                        new object[] { message });

                if (e == null)
                {
                    throw new Exception(
                        "Cannot instantiate the expected exception.");
                }

                throw e;
            }
        }

        /// <summary>
        /// Throws the specified exception if the
        /// <see cref="ICollection"/> is null or empty.
        /// </summary>
        /// <typeparam name="TExceptionType">
        /// The type of exception to throw.</typeparam>
        /// <param name="value">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        [DebuggerNonUserCode]
        public static void AssertNotNullOrEmpty<TExceptionType>(
            this ICollection value, string message = null, string name = null)
            where TExceptionType : Exception
        {
            if (value == null || value.Count == 0)
            {
                if (message.IsNullOrEmpty())
                {
                    message = $"Value is null or empty.";
                }

                if(typeof(TExceptionType).Equals(typeof(ArgumentException)))
                {
                    throw new ArgumentException(name, message);
                }

                if(typeof(TExceptionType).Equals(typeof(ArgumentNullException)))
                {
                    throw new ArgumentException(message, name);
                }

                var e =
                    typeof(TExceptionType).CreateInstance<TExceptionType>(
                        new object[] { message });

                if (e == null)
                {
                    throw new Exception(
                        "Cannot instantiate the expected exception.");
                }

                throw e;
            }
        }
       
        /// <summary>
        /// Throws the specified exception if the
        /// <see cref="ICollection"> does not have the minimum number of elements.
        /// </summary>
        /// <typeparam name="TExceptionType">
        /// The type of exception to throw.</typeparam>
        /// <param name="value">The value to test.</param>
        /// <param name="minimumLength">The expected value.</param>
        /// <param name="message">The message to display if the value is null.</param>
        [DebuggerNonUserCode]
        public static void AssertMinimumLength<TExceptionType>(
            this ICollection value, long minimumLength, string message = null)
            where TExceptionType : Exception
        {
            value.AssertParameterNotNull("Must supply an ICollection.", nameof(value));

            if (minimumLength < 0)
                throw new ArgumentException(
                    nameof(minimumLength), "Minimum length must be zero or greater.");
            
            if (value.Count < minimumLength)
            {
                if (message.IsNullOrEmpty())
                {
                    message = $"{value.Count} is less than expected value of {minimumLength}";
                }

                var e =
                    typeof(TExceptionType).CreateInstance<TExceptionType>(
                        new object[] { message });

                if (e == null)
                {
                    throw new Exception(
                        "Cannot instantiate the expected exception.");
                }

                throw e;
            }
        }

        #endregion

        #region Strings

        /// <summary>
        /// Tests if the string is empty.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>True if the string is empty.</returns>
        [DebuggerNonUserCode]
        public static bool IsEmpty(this string value)
        {
            return value.Trim().Length == 0;
        }

        /// <summary>
        /// Tests if the string is not empty.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>True if the string is not empty.</returns>
        [DebuggerNonUserCode]
        public static bool IsNotEmpty(this string value)
        {
            return value.Trim().Length > 0;
        }

        /// <summary>
        /// Tests if the string is null or empty.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>True if the string is null or empty.</returns>
        [DebuggerNonUserCode]
        public static bool IsNullOrEmpty(this string value)
        {
            return value == null || value.Trim().Length == 0;
        }

        #endregion Strings

        #region Collections

        #region IsEmpty

        /// <summary>
        /// Tests if the collection is empty.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        [DebuggerNonUserCode]
        public static bool IsEmpty(this ICollection collection)
        {
            collection.AssertParameterNotNull(
                "The collection cannot be null.",
                "collection");

            return collection.Count == 0;
        }

        /// <summary>
        /// Tests if the collection is empty.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        [DebuggerNonUserCode]
        public static bool IsEmpty(this IDictionary collection)
        {
            collection.AssertParameterNotNull(
                "The collection cannot be null.",
                "collection");

            return collection.Count == 0;
        }

        /// <summary>
        /// Tests if the IDictionary is empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key of 
        /// the IDictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values
        /// of the IDictionary.</typeparam>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        [DebuggerNonUserCode]
        public static bool IsEmpty<TKey, TValue>(
            this IDictionary<TKey, TValue> collection)
        {
            collection.AssertParameterNotNull(
                "The collection cannot be null.",
                "collection");

            return collection.Count == 0;
        }

        #endregion IsEmpty

        #region IsNotEmpty

        /// <summary>
        /// Tests if the collection is not empty.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is not empty.</returns>
        [DebuggerNonUserCode]
        public static bool IsNotEmpty(this ICollection collection)
        {
            collection.AssertParameterNotNull(
                "The collection cannot be null.",
                "collection");

            return collection.Count > 0;
        }

        /// <summary>
        /// Tests if the collection is not empty.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is not empty.</returns>
        [DebuggerNonUserCode]
        public static bool IsNotEmpty(this IDictionary collection)
        {
            collection.AssertParameterNotNull(
                "The collection cannot be null.",
                "collection");

            return collection.Count > 0;
        }

        /// <summary>
        /// Tests if the IDictionary is not empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key of 
        /// the IDictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values
        /// of the IDictionary.</typeparam>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is not empty.</returns>
        [DebuggerNonUserCode]
        public static bool IsNotEmpty<TKey, TValue>(
            this IDictionary<TKey, TValue> collection)
        {
            collection.AssertParameterNotNull(
                "The collection cannot be null.",
                "collection");

            return collection.Count > 0;
        }

        #endregion IsNotEmpty

        #endregion Collections
    }
}