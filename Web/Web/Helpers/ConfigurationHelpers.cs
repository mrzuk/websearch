using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.View;

namespace Web.Helpers
{
    public static class ConfigurationHelpers
    {
        public static List<PropertyValue> GetConfigurationValues(this ConfigurationView data)
        {
            return typeof(ConfigurationView)
           .GetProperties()
           .Where(pi => pi.GetGetMethod() != null)
           .Select(pi => new PropertyValue
           {
               Name = pi.Name,
               Value = pi.GetGetMethod().Invoke(data, null).ToString()
           }).ToList();
            
        }
    }

    public class PropertyValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}