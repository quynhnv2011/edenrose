using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Vks.Common
{
    public class ObjectUtils
    {
        public static void CopyObject<T>(object sourceObject, ref T destObject, bool bolNoList)
        {
            //	If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //	Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            //	Loop through the source properties
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //	Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //	If there is none, skip
                if (targetObj == null)
                    continue;
                if (p.PropertyType.Namespace.Contains("eSport5.ActionService"))
                    continue;
                if (bolNoList)
                {
                    if (p.PropertyType.Namespace.Contains("System.Collections.Generic"))
                        continue;
                }
                try
                {
                    if (p.CanWrite)
                        targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
                }
                catch (Exception)
                {
                }

            }
        }

      
        public static string ObjectToString(object obj,HashSet<string> exceptProperties)
        {
            if (obj == null) return string.Empty;
            Type t = obj.GetType();
            var props = t.GetProperties().OrderByDescending(q => q.Name).ToList();

            var strResult = string.Empty;
            foreach (PropertyInfo prp in props)
            {
                if (!exceptProperties.Contains(prp.Name) 
                    && !prp.PropertyType.Namespace.Contains("System.Collections.Generic")
                    && !prp.PropertyType.Namespace.Contains("eSport5.ActionService"))
                {
                    object value = prp.GetValue(obj, new object[] { });
                    if (value == null) value = string.Empty;

                    strResult = string.Format("{0}-{1}:{2}", strResult, prp.Name, value);
                }
            }
            return strResult;
        }

        public static List<string> InvalidJsonElements;
        //public static IList<T> DeserializeToList<T>(string jsonString)
        //{
        //    InvalidJsonElements = null;
        //    var array = JArray.Parse(jsonString);
        //    IList<T> objectsList = new List<T>();
        //    foreach (var item in array)
        //    {
        //        try
        //        {
        //            objectsList.Add(item.ToObject<T>());
        //        }
        //        catch (Exception ex)
        //        {
        //            InvalidJsonElements = InvalidJsonElements ?? new List<string>();
        //            InvalidJsonElements.Add(item.ToString());
        //        }
        //    }
        //    return objectsList;
        //}

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataTable ConvertToDatatable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
