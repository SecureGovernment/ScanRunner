using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SecureGovernment.Domain.Tests
{
    public sealed class Utils
    {
        public static T Init<T>() where T : new() => Inject(new T(), GetValidProperties<T>().Select(MakeMockProperty).ToArray());

        private static T Inject<T>(T classToInjectInto, object[] properties)
        {
            var classProperties = GetValidProperties<T>();

            foreach (var property in properties.Where(x => x != null))
            {
                var mockObject = property;

                if (typeof(Mock).IsAssignableFrom(property.GetType()))
                    mockObject = ((Mock)mockObject).Object;

                var prop = classProperties.Where(x => x.PropertyType.IsAssignableFrom(mockObject.GetType())).Single();
                if (prop == null) Assert.Fail("Cannot find matching property to inject into!");

                prop.SetValue(classToInjectInto, mockObject);
            }

            return classToInjectInto;
        }

        private static List<PropertyInfo> GetValidProperties<T>() => typeof(T).GetProperties().Where(x => x.CanWrite).ToList();

        private static object MakeMockProperty(PropertyInfo property)
        {
            var moqGeneric = typeof(Mock<>).MakeGenericType(property.PropertyType);
            return moqGeneric.GetConstructor(new[] { typeof(MockBehavior) }).Invoke(new object[] { MockBehavior.Strict });
        }
    }
}
