using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline
{
    /// <summary>
    /// Special type of converter to allow correct changes of struct-like objects variables in a PropertyGrid
    /// Taken from : https://stackoverflow.com/questions/15746897/modifying-structure-property-in-a-propertygrid
    /// </summary>
    internal class ValueTypeTypeConverter : System.ComponentModel.ExpandableObjectConverter
    {
        public override bool GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override object CreateInstance(System.ComponentModel.ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues");

            object boxed = Activator.CreateInstance(context.PropertyDescriptor.PropertyType);
            foreach (System.Collections.DictionaryEntry entry in propertyValues)
            {
                System.Reflection.PropertyInfo pi = context.PropertyDescriptor.PropertyType.GetProperty(entry.Key.ToString());
                if ((pi != null) && (pi.CanWrite))
                {
                    pi.SetValue(boxed, Convert.ChangeType(entry.Value, pi.PropertyType), null);
                }
            }
            return boxed;
        }
    }
}
