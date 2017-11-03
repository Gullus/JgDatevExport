using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JgDatvExportAnzeige
{
    public partial class FormEditWert : Window
    {

        public FormEditWert(Type MyType, object MyObject, string FeldName, bool IstEnum = false, string[] EnumDaten = null)
        {
            InitializeComponent();

            if (IstEnum)
            {
                var myBinding = new Binding(FeldName);
                myBinding.Source = MyObject;

                tbFeldVorgabe.Visibility = Visibility.Collapsed;
                cbFeldVorgabe.ItemsSource = EnumDaten;
                cbFeldVorgabe.SetBinding(ComboBox.TextProperty, myBinding);

                //Binding myBinding = new Binding();
                //myBinding.Path = new PropertyPath(FeldName);
                //myBinding.Mode = BindingMode.TwoWay;
                //myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                //BindingOperations.SetBinding(cbFeldVorgabe, ComboBox.SelectedValueProperty, myBinding);
                //SetBindingForComboBoxWithEnumItems(cbFeldVorgabe, obj.GetType().GetProperty(FeldName).GetType());
            }
            else
            {
                var myBinding = new Binding(FeldName);
                myBinding.Source = MyObject;

                cbFeldVorgabe.Visibility = Visibility.Collapsed;
                tbFeldVorgabe.SetBinding(TextBox.TextProperty, myBinding);
            }
        }

        private void SetBindingForComboBoxWithEnumItems(ComboBox cmb, Type enumType)
        {
            List<KeyValuePair<object, string>> values = new List<KeyValuePair<object, string>>();
            List<Tuple<object, string, int>> enumList = Enum.GetValues(enumType).Cast<object>().Select(x => Tuple.Create(x, x.ToString(), (int)x)).ToList();
            foreach (var idx in enumList)
            {
                values.Add(new KeyValuePair<object, string>(idx.Item1, idx.Item2));
            }
            cmb.ItemsSource = values;
            cmb.DisplayMemberPath = "Value";
            cmb.SelectedValuePath = "Key";
        }

    }
}
