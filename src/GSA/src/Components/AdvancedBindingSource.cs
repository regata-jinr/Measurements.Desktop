using System;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Components
{
    public class AdvancedBindingSource<T>
    {
        private readonly BindingSource _bindingSource;
        private readonly List<T> _sourceList;
        private readonly Type _basicType;
        private readonly PropertyInfo[] _propertiesList;
        private DataTable _datatable;
        private readonly DataSet _dataset;

        public BindingSource GetBindingSource()
        {
            return _bindingSource;
        }

        public AdvancedBindingSource(List<T> list)
        {
            try
            {
                _sourceList = list;
                _bindingSource = new BindingSource();

                _basicType = typeof(T);
                _propertiesList = _basicType.GetProperties();
                _dataset = new DataSet();
                _datatable = new DataTable();
                CreateColumns();
                FillData();

                _bindingSource.DataSource = _dataset;
            }
            catch (Exception ex)
            {
                //MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
            }

        }

        private void CreateColumns()
        {
            try
            {
                _datatable = _dataset.Tables.Add(_basicType.Name);

                foreach (var pi in _propertiesList)
                    _datatable.Columns.Add(pi.Name, Nullable.GetUnderlyingType(
                pi.PropertyType) ?? pi.PropertyType);

                _bindingSource.DataMember = _datatable.TableName;
            }
            catch (Exception ex)
            {
                //MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
            }
        }

        private void FillData()
        {
            try
            {
                foreach (var ent in _sourceList)
                {
                    object[] newrow = new object[_propertiesList.Length];

                    for (int i = 0; i < _propertiesList.Length; ++i)
                        newrow[i] = _propertiesList[i].GetValue(ent);

                    _datatable.Rows.Add(newrow);
                }
            }
            catch (Exception ex)
            {
                //MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() {exception = ex, Level = ExceptionLevel.Error });
            }
        }
    }
}
