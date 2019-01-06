using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Engineer.Utilities
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {            
            if (PropertyChanged != null)
            {
               PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void NotifyPropertyChanged<T>(Expression<Func<T>> exp)
        {
            MemberExpression memberExpression = (MemberExpression)exp.Body;
            string propertyName = memberExpression.Member.Name;

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
