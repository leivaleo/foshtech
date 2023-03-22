using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Backend.TechChallenge.CrossCutting.Base;

namespace Backend.TechChallenge.Api.Base
{
    public class UnitOfWorkResult
    {
        [System.Text.Json.Serialization.JsonPropertyName("StatusOk")]
        public bool StatusOk = false;
        public Exception Error = null;
        public int ProcessOk = 0;
        public int ProcessFail = 0;
        public int? Total = null;
        public List<EntityModelBase> Data = null;

        public static UnitOfWorkResult SetResultDataOk(object data)
        {
            var result = new UnitOfWorkResult();

            if (data == null)
            {
                result.Error = new NullReferenceException("Action returns a null object");
                return result;
            }

            var list = new List<EntityModelBase>();

            if (IsTupleType(data.GetType()))
            {
                var tupleItems = GetValueTupleItemObjects(data);

                list = GetListFromObject(tupleItems[0]);
                result.Total = (int)tupleItems[1];
            }
            else
            {
                list = GetListFromObject(data);
                result.Total = list.Count;
            }


            result = SetReturnOk(result);
            result.ProcessOk = list.Count;
            result.Data = list;

            return result;
        }

        private static bool IsTupleType(Type type)
        {
            return type.GetTypeInfo().IsGenericType && ValueTupleTypes.Contains(type.GetGenericTypeDefinition());
        }

        private static readonly HashSet<Type> ValueTupleTypes = new HashSet<Type>(new Type[]
        {
            typeof(ValueTuple<>),
            typeof(ValueTuple<,>),
            typeof(ValueTuple<,,>),
            typeof(ValueTuple<,,,>),
            typeof(ValueTuple<,,,,>),
            typeof(ValueTuple<,,,,,>),
            typeof(ValueTuple<,,,,,,>),
            typeof(ValueTuple<,,,,,,,>)
        });

        private static IEnumerable<object> GetItemsFromTuple(System.Runtime.CompilerServices.ITuple tuple)
        {
            for (var i = 0; i < tuple.Length; i++)
                yield return tuple[i];
        }

        private static List<object> GetValueTupleItemObjects(object tuple) =>
            GetValueTupleItemFields(tuple.GetType()).Select(f => f.GetValue(tuple)).ToList();

        private static List<Type> GetValueTupleItemTypes(Type tupleType) =>
            GetValueTupleItemFields(tupleType).Select(f => f.FieldType).ToList();

        private static List<FieldInfo> GetValueTupleItemFields(Type tupleType)
        {
            var items = new List<FieldInfo>();

            FieldInfo field;
            int nth = 1;
            while ((field = tupleType.GetRuntimeField($"Item{nth}")) != null)
            {
                nth++;
                items.Add(field);
            }

            return items;
        }

        private static bool IsCollectionType(Type type)
        {
            return type.GetInterfaces().Any(s => s.Namespace == "System.Collections.Generic"
                                                 && (s.Name == "ICollection" || s.Name.StartsWith("ICollection`")));
        }

        private static List<EntityModelBase> GetListFromObject(object obj)
        {
            if (IsCollectionType(obj.GetType()))
                return ((IEnumerable)obj).Cast<EntityModelBase>().ToList();
            else
                return new List<EntityModelBase>() { (EntityModelBase)obj };
        }

        private static UnitOfWorkResult SetReturnOk(UnitOfWorkResult dataResult)
        {
            dataResult.StatusOk = true;
            dataResult.Error = null;
            dataResult.ProcessFail = 0;

            return dataResult;
        }
    }
}
