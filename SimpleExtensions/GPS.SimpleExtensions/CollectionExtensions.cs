/*
 * Copyright 2009, Payton Byrd
 * Copyright 2017, Gateway Programming School
 * Licensed Under the Microsoft Public License (MS-PL)
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace GPS.SimpleExtensions
{
    // Payton Byrd - 2010/01/19
    // All commented code will be removed for version 1.0.0

    /// <summary>
    /// A collection of extension methods
    /// used to perform operations on 
    /// collections.
    /// </summary>
    public static class CollectionExtensions
    {
        #region First Element of Collection

        ///// <summary>
        ///// Gets the first item of the List.
        ///// </summary>
        ///// <typeparam name="T">Type of items
        ///// in the list.</typeparam>
        ///// <param name="collection">The list containing
        ///// the desired item.</param>
        ///// <returns>Null if empty, instance of T
        ///// if at least one item exists.</returns>
        //public static T FirstItem<T>(
        //    this List<T> collection)
        //{
        //    collection.AssertParameterNotNull(
        //        "The collection cannot be null.",
        //        "collection");

        //    if (collection.IsNotEmpty())
        //    {
        //        return collection[0];
        //    }

        //    return default(T);
        //}

        ///// <summary>
        ///// Gets the first item of the
        ///// array.
        ///// </summary>
        ///// <param name="array">The array
        ///// containing the desired item.</param>
        ///// <returns>Null if the array
        ///// is empty, the first item if 
        ///// available.</returns>
        //public static object FirstItem(
        //    this Array array)
        //{
        //    array.AssertParameterNotNull(
        //        "The array cannot be null.",
        //        "array");

        //    if (array.IsNotEmpty())
        //    {
        //        return array.GetValue(0);
        //    }

        //    return null;
        //}

        /// <summary>
        /// Gets the first <see cref="System.Data.DataRow"/>
        /// (as T) of the DataTable.
        /// </summary>
        /// <typeparam name="T">The type of
        /// DataRow to return.</typeparam>
        /// <param name="table">The DataTable containing
        /// the desired row.</param>
        /// <returns>Null if the DataTable is empty,
        /// instance of DR if at least one DataRow
        /// exists.</returns>
        public static T FirstRow<T>(
            this DataTable table)
            where T : DataRow
        {
            if (table.IsNotEmpty())
            {
                return (T) table.Rows[0];
            }

            return null;
        }

        #endregion

        #region Subject to elimination, tests, or incomplete

        //#region Convert to DataTable
        //public static DataTable AsDataTable(this Array source)
        //{
        //    DataTable result = new DataTable();

        //    return result;
        //}
        //#endregion

        //#region Convert to DataRow
        //private class TestClass
        //{
        //    public string ColumnA { get; set; }
        //    public int ColumnB { get; set; }
        //    public decimal ColumnC { get; set; }
        //    public DateTime ColumnD { get; set; }
        //}

        //private class TestClass2 : TestClass
        //{
        //    public char ColumnE { get; set; }
        //}
        //public static void Test()
        //{
        //    DataTable table = new DataTable();

        //    var testObject = new TestClass
        //    {
        //        ColumnA = "Test",
        //        ColumnB = 3,
        //        ColumnC = 4.0M,
        //        ColumnD = DateTime.MaxValue
        //    };

        //    testObject.AsDataRow<DataRow>(table);

        //    var testObject2 = new TestClass2
        //    {
        //        ColumnA = "Test",
        //        ColumnB = 3,
        //        ColumnC = 4.0M,
        //        ColumnD = DateTime.MaxValue,
        //        ColumnE = 'A'
        //    };

        //    testObject2.AsDataRow<DataRow>(table);

        //    if (table.IsEmpty())
        //    {
        //        Console.WriteLine("Table is empty.");
        //    }
        //    else
        //    {
        //        DataRow row = table.FirstRow<DataRow>();

        //        row["ColumnA"].AssertEquals<Exception>(
        //            "Test",
        //            "ColumnA contains \""
        //                + Convert.ToString(row["ColumnA"])
        //                + "\", expected \"Test\".");

        //        row["ColumnB"].AssertEquals<Exception>(
        //            3,
        //            "ColumnA contains "
        //                + Convert.ToString(row["ColumnB"])
        //                + ", expected 3.");

        //        row["ColumnC"].AssertEquals<Exception>(
        //            4.0M,
        //            "ColumnA contains "
        //                + Convert.ToString(row["ColumnC"])
        //                + ", expected 4.0.");

        //        row["ColumnD"].AssertEquals<Exception>(
        //            DateTime.MaxValue,
        //            "ColumnA contains \""
        //                + Convert.ToString(row["ColumnD"])
        //                + "\", expected \"" 
        //                + DateTime.MaxValue + "\".");

        //        table.Rows.Remove(row);

        //        row = table.FirstRow<DataRow>();

        //        row["ColumnA"].AssertEquals<Exception>(
        //            "Test",
        //            "ColumnA contains \""
        //                + Convert.ToString(row["ColumnA"])
        //                + "\", expected \"Test\".");

        //        row["ColumnB"].AssertEquals<Exception>(
        //            3,
        //            "ColumnA contains "
        //                + Convert.ToString(row["ColumnB"])
        //                + ", expected 3.");

        //        row["ColumnC"].AssertEquals<Exception>(
        //            4.0M,
        //            "ColumnA contains "
        //                + Convert.ToString(row["ColumnC"])
        //                + ", expected 4.0.");

        //        row["ColumnD"].AssertEquals<Exception>(
        //            DateTime.MaxValue,
        //            "ColumnA contains \""
        //                + Convert.ToString(row["ColumnD"])
        //                + "\", expected \""
        //                + DateTime.MaxValue + "\".");

        //        row["ColumnE"].AssertEquals<Exception>(
        //            'A',
        //            "ColumnE contains '"
        //                + Convert.ToString(row["ColumnE"])
        //                + "', expected 'A'");
        //    }
        //}

        #endregion

        /// <summary>
        /// Creates a new DataRow and adds it
        /// to the source DataTable for the supplied
        /// object.
        /// </summary>
        /// <typeparam name="T">Typed DataRow</typeparam>
        /// <param name="source">Object to add to the DataTable.</param>
        /// <param name="table">DataTable to add the row to.</param>
        /// <returns></returns>
        public static T AsDataRow<T>(
            this object source,
            DataTable table)
            where T : DataRow
        {
            source.AssertParameterNotNull(
                "Cannot convert a null source to a DataRow.",
                "source");

            table.AssertParameterNotNull(
                "Cannot add a DataRow to a null DataTable.",
                "table");

            T result = null;

            List<DataColumn> columns =
                MakeDataColumns(source);

            if (columns.IsNotEmpty())
            {
                table.AddColumnsToDataTable(columns);
                result = (T) table.NewRow();
                result.PopulateDataRow(source, columns);
                table.Rows.Add(result);
            }

            return result;
        }

        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="row"></param>
        /// <param name="source"></param>
        /// <param name="columns"></param>
        private static void PopulateDataRow(
            this DataRow row,
            object source,
            List<DataColumn> columns)
        {
            row.AssertParameterNotNull(
                "Cannot populate a null DataRow",
                "row");

            source.AssertParameterNotNull(
                "Cannot add values to a DataRow from a null source.",
                "source");

            columns.AssertParameterNotNull(
                "The list of DataColumns is null.",
                "columns");

            if (columns.IsEmpty())
            {
                throw new ArgumentOutOfRangeException(
                    "columns",
                    "The list of DataColumns is empty.");
            }

            foreach (DataColumn column in columns)
            {
                string propertyName = column.Caption;
                bool readOnly = column.ReadOnly;

                column.ReadOnly = false;

                if (propertyName.IsNullOrEmpty())
                {
                    propertyName = column.ColumnName;
                }

                PropertyInfo property =
                    source.GetProperty(propertyName,
                                       BindingFlags.Instance |
                                       BindingFlags.Public);

                if (property != null
                    && property.CanRead)
                {
                    row[column] = property.GetValue(
                        source, new object[0]);
                }

                column.ReadOnly = readOnly;
            }
        }

        /// <summary>
        /// Adds a list of columns to a DataTable.
        /// </summary>
        /// <param name="table">The DataTable to add
        /// the columns to.</param>
        /// <param name="columns">The list of columns
        /// to add to the table.</param>
        /// <remarks>
        /// This method will add unmatched columns,
        /// skip redundant columns (same name and type)
        /// and add additional columns with a counter
        /// for mismatched naming collisions.
        /// </remarks>
        public static void AddColumnsToDataTable(
            this DataTable table,
            List<DataColumn> columns)
        {
            table.AssertParameterNotNull(
                "Cannot add columns to a null DataTable.",
                "table");

            columns.AssertParameterNotNull(
                "The list of DataColumns is null.",
                "columns");

            if (columns.IsEmpty())
            {
                throw new ArgumentOutOfRangeException(
                    "columns",
                    "The list of DataColumns is empty.");
            }

            List<DataColumn> tableColumns =
                table.Columns.ToList();

            var replaceList =
                new Dictionary<DataColumn, DataColumn>();

            foreach (DataColumn column in columns)
            {
                List<DataColumn> matchingColumns =
                    (from tableColumn in tableColumns
                     where tableColumn.ColumnName.StartsWith(column.ColumnName)
                           && tableColumn.DataType.Equals(column.DataType)
                     select tableColumn).ToList();

                List<DataColumn> misMatchedColumns =
                    (from tableColumn in tableColumns
                     where tableColumn.ColumnName.StartsWith(column.ColumnName)
                           && !tableColumn.DataType.Equals(column.DataType)
                     select tableColumn).ToList();

                if (matchingColumns.IsEmpty() &&
                    misMatchedColumns.IsEmpty())
                {
                    tableColumns.Add(column);
                    table.Columns.Add(new DataColumn(
                                          column.ColumnName, column.DataType, column.Expression));
                }
                else if (misMatchedColumns.IsNotEmpty())
                {
                    var newColumn = new DataColumn(
                        column.ColumnName, column.DataType, column.Expression)
                                        {
                                            Caption = column.ColumnName,
                                            ColumnName = column.ColumnName + misMatchedColumns.Count
                                        };

                    tableColumns.Add(column);
                    table.Columns.Add(newColumn);
                }
                else
                {
                    replaceList.Add(column,
                                    matchingColumns.FirstOrDefault());
                }
            }

            foreach (DataColumn column in replaceList.Keys)
            {
                columns.Remove(column);
                columns.Add(replaceList[column]);
            }
        }

        /// <summary>
        /// Helper method.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static List<DataColumn> MakeDataColumns(object source)
        {
            source.AssertParameterNotNull(
                "Cannot make DataColumns from a null source.",
                "source");

            var result = new List<DataColumn>();

            IEnumerable<PropertyInfo> properties =
                from propertyInfo in source.GetType().GetProperties(
                    BindingFlags.Public | BindingFlags.Instance)
                where propertyInfo.CanRead
                select propertyInfo;

            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead)
                {
                    var column = new DataColumn(
                        property.Name, property.PropertyType);

                    column.ReadOnly = !property.CanWrite;

                    result.Add(column);
                }
            }

            return result;
        }

        // Redundant to the .Net ToList() extension method.
        // Payton Byrd - 2010-01-19
        // Will be deleted in version 1.0.0
        //
        //public static List<T> ToList<T>(
        //    this IEnumerable source)
        //{
        //    List<T> result = new List<T>();

        //    foreach (T item in source)
        //    {
        //        result.Add(item);
        //    }

        //    return result;
        //}

        /// <summary>
        /// Mades a List&lt;DataColumn&gt; of the 
        /// DataColumns of the DataColumnCollection.
        /// </summary>
        /// <param name="source">The list of columns to convert.</param>
        /// <returns>Strongly typed list of DataColumn objects.</returns>
        /// <example>
        /// <code>
        /// var table = SomeMethodThatReturnsTable();
        /// 
        /// // List&lt;DataColumn&gt;
        /// var columnsList = table.Columns.ToList();
        /// </code>
        /// </example>
        public static List<DataColumn> ToList(
            this DataColumnCollection source)
        {
            var tableColumns =
                new List<DataColumn>();

            foreach (DataColumn tableColumn in source)
            {
                tableColumns.Add(tableColumn);
            }

            return tableColumns;
        }
    }
}