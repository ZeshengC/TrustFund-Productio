using Core.Common.Utils;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Core
{
    public class ObjectBase:INotifyPropertyChanged,IDataErrorInfo
    {
        public ObjectBase()
        {
            _Validator = GetValidator();
            Validate();
        }

        private event PropertyChangedEventHandler _propertyChanged;
        private List<PropertyChangedEventHandler> PropertyChangedSubscribers = new List<PropertyChangedEventHandler>();
        protected IValidator _Validator = null;
        protected IEnumerable<ValidationFailure> _ValidationErrors = null;
        public static CompositionContainer Container{get;set;}
        private bool _IsDirty;
        public bool IsDirty 
        { 
            get
            {
                return _IsDirty;
            }
            set
            {
                _IsDirty = value;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if(!PropertyChangedSubscribers.Contains(value))
                {
                    _propertyChanged += value;
                    PropertyChangedSubscribers.Add(value);
                }
            }
            remove
            {
                _propertyChanged -= value;
                PropertyChangedSubscribers.Remove(value);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName, true);
        }

        protected virtual void OnPropertyChanged(string propertyName, bool makeDirty)
        {
            if(_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            if(makeDirty)
            {
                _IsDirty = true;
            }
            Validate();
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }

        protected virtual IValidator GetValidator()
        {
            return null;
        }
        public IEnumerable<ValidationFailure> ValidationErrors
        {
            get { return _ValidationErrors; }
            set { }
        }

        public void Validate()
        {
            if(_Validator != null)
            {
                ValidationResult results = _Validator.Validate(this);
                _ValidationErrors = results.Errors;
            }
        }

        public virtual bool IsValid
        {
            get
            {
                if (_ValidationErrors != null && _ValidationErrors.Count() > 0)
                    return false;
                else
                    return true;
            }
        }
        public string this[string columnName]
        {
            get
            {
                StringBuilder errors = new StringBuilder();
                if(_ValidationErrors != null && _ValidationErrors.Count() > 0)
                {
                    foreach(ValidationFailure validationError in _ValidationErrors)
                    {
                        if (validationError.PropertyName == columnName)
                            errors.AppendLine(validationError.ErrorMessage);
                    }
                }
                return errors.ToString();
            }
        }
        public List<ObjectBase> GetDirtyObjects()
        {
            List<ObjectBase> dirtyObejcts = new List<ObjectBase>();
            WalkObjectGraph(o =>
                {
                    if (o.IsDirty)
                        dirtyObejcts.Add(o);
                    return false;
                }, coll => { });
            return dirtyObejcts;
        }
        public void CleanAll()
        {
            WalkObjectGraph(o =>
                {
                    if (o.IsDirty)
                        o.IsDirty = false;
                    return false;
                }, coll => { });

        }
        public virtual bool IsAnythingDirty()
        {
            bool isDirty = false;
            WalkObjectGraph((o) =>
                {
                    if (o.IsDirty)
                    {
                        IsDirty = true;
                        return true;
                    }
                    else
                        return false;
                }, coll => { });
            return IsDirty;
        }
        protected void WalkObjectGraph(Func<ObjectBase,bool> snippetForObject,Action<IList<object>> snippetForCollection, params string[] exemptProperties)
        {
            List<ObjectBase> visited = new List<ObjectBase>();
            Action<ObjectBase> walk = null;

            walk = (o) =>
            {
                if (o != null && !visited.Contains(o))
                {
                    visited.Add(o);

                    bool exitWalk = snippetForObject.Invoke(o);
                    if (!exitWalk)
                    {
                        PropertyInfo[] properties = o.GetType().GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            if (property.PropertyType.IsSubclassOf(typeof(ObjectBase)))
                            {
                                ObjectBase obj = (ObjectBase)property.GetValue(o, null);
                                walk(obj);
                            }
                            else
                            {
                                IList<object> coll = property.GetValue(o, null) as IList<object>;
                                if (coll != null)
                                {
                                    foreach (object item in coll)
                                    {
                                        snippetForCollection.Invoke(coll);
                                        if (item is ObjectBase)
                                        {
                                            walk((ObjectBase)item);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            walk(this);
        }


        public string Error
        {
            get { return string.Empty; }
        }
    }
}
