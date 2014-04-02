using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace CH01.CustomMarkupExtension
{
    public class RandomExtension : MarkupExtension
    {
        readonly int _from, _to;
        public RandomExtension(int from, int to)
        {
            _from = from; _to = to;
        }
        public RandomExtension(int to)
            : this(0, to)
        {

        }
        static readonly Random _rnd = new Random(); // access property
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return (double)_rnd.Next(_from,_to);

            // return the flexible target data type 
            //int value = _rnd.Next(_from, _to);
            //Type targetType = null;
            //if (serviceProvider != null)
            //{
            //    var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            //    if (target != null)
            //    {
            //        var clrProp = target.TargetProperty as PropertyInfo;
            //        if (clrProp != null)
            //        {
            //            var dp = target.TargetProperty as DependencyProperty;
            //            if (dp != null)
            //            {
            //                targetType = dp.PropertyType;
            //            }
            //        }
            //    }
            //}
            //return targetType != null ?
            //        Convert.ChangeType(value, targetType) :
            //        value.ToString();// Return string type by default.
        }

    }
}
